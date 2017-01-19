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
    internal class RequestDeniedException : Exception
    {
    }


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
            IRestClient client = restClientFactory.Create();

            _logger.Log(string.Format("Request: {0}", client.BuildUri(request)));
            IRestResponse<T> response = client.Execute<T>(request);
            _logger.Log(string.Format("Response: {0} - {1}", response.StatusCode, StringHelpers.Truncate(response.Content, 100)));

            // If login session has expired, try to login, and then re-execute the original request
            if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.BadRequest)
            {
                _logger.Log("Try to re-authenticate");
                if (!ReAuthenticate())
                    throw new RequestDeniedException();

                _logger.Log(string.Format("Authenticated. Resend request: {0}", client.BuildUri(request)));
                response = client.Execute<T>(request);
                _logger.Log(string.Format("Response: {0} - {1}", response.StatusCode, StringHelpers.Truncate(response.Content, 100)));
            }

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Created)
            {
                ErrorMessage = response.ErrorMessage;
                throw new RequestDeniedException();
            }

            ErrorMessage = "";
            return response.Data;
        }


        protected bool ReAuthenticate()
        {
            IRestRequest request;

            try
            {
                request = jiraApiRequestFactory.CreateReAuthenticateRequest();
            }
            catch (AuthenticateNotYetCalledException)
            {
                return false;
            }

            var client = restClientFactory.Create(true);
            _logger.Log(string.Format("Request: {0}", client.BuildUri(request)));
            IRestResponse response = client.Execute(request);
            _logger.Log(string.Format("Response: {0} - {1}", response.StatusCode, StringHelpers.Truncate(response.Content, 100)));

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                ErrorMessage = "Invalid username or password";
                return false;
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                ErrorMessage = response.ErrorMessage;
                return false;
            }

            ErrorMessage = "";
            return true;
        }


        private Logger _logger = Logger.Instance;

        private IRestClientFactory restClientFactory;
        private IJiraApiRequestFactory jiraApiRequestFactory;
    }
}
