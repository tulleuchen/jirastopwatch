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
using System;
using System.Net;

namespace StopWatch
{
    internal class RequestDeniedException : Exception
    {
    }


    internal class JiraApiRequester : IJiraApiRequester
    {
        public JiraApiRequester(IRestClientFactory restClientFactory, IJiraApiRequestFactory jiraApiRequestFactory)
        {
            this.restClientFactory = restClientFactory;
            this.jiraApiRequestFactory = jiraApiRequestFactory;
        }


        public T DoAuthenticatedRequest<T>(IRestRequest request)
            where T : new()
        {
            IRestResponse<T> response = restClientFactory.Create().Execute<T>(request);

            // If login session has expired, try to login, and then re-execute the original request
            if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.BadRequest)
            {
                if (!ReAuthenticate())
                    throw new RequestDeniedException();

                response = restClientFactory.Create().Execute<T>(request);
            }

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Created)
                throw new RequestDeniedException();

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
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
                return false;

            return true;
        }


        private IRestClientFactory restClientFactory;
        private IJiraApiRequestFactory jiraApiRequestFactory;
    }
}
