/**************************************************************************
Copyright 2016 Carsten Gehling

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
**************************************************************************/
using RestSharp;
using StopWatch.Logging;
using System;
using System.Net;

namespace StopWatch
{
    internal class JiraApiRequester : IJiraApiRequester
    {
        public string ErrorMessage { get; private set; }

        public JiraApiRequester(IRestClientFactory restClientFactory, IJiraApiRequestFactory jiraApiRequestFactory)
        {
            this.restClientFactory = restClientFactory;
            this.jiraApiRequestFactory = jiraApiRequestFactory;
            ErrorMessage = "";
        }

        public T DoAuthenticatedRequest<T>(IRestRequest request)
            where T : new()
        {
            AddAuthHeader(request);

            IRestClient client = restClientFactory.Create();

            _logger.Log(string.Format("Request: {0}", client.BuildUri(request)));
            IRestResponse<T> response = client.Execute<T>(request);
            _logger.Log(string.Format("Response: {0} - {1}", response.StatusCode, StringHelpers.Truncate(response.Content, 100)));

            // If login session has expired, try to login, and then re-execute the original request
            if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new RequestDeniedException();
            }

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Created)
            {
                ErrorMessage = response.ErrorMessage;
                throw new RequestDeniedException();
            }

            ErrorMessage = "";
            return response.Data;
        }

        public void SetAuthentication(string username, string apiToken)
        {
            _username = username;
            _apiToken = apiToken;
        }

        private void AddAuthHeader(IRestRequest request)
        {
            if (string.IsNullOrEmpty(_username) || string.IsNullOrEmpty(_apiToken))
            {
                throw new UsernameAndApiTokenNotSetException();
            }
            request.AddHeader("Authorization", "Basic " + System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{_username}:{_apiToken}")));
        }

        private Logger _logger = Logger.Instance;

        private IRestClientFactory restClientFactory;
        private IJiraApiRequestFactory jiraApiRequestFactory;
        private string _username;
        private string _apiToken;
    }

    internal class RequestDeniedException : Exception
    {
    }

    internal class UsernameAndApiTokenNotSetException : Exception
    {
    }
}
