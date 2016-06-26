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
using System.Collections.Generic;

namespace StopWatch
{
    internal class JiraClient
    {
        public bool SessionValid { get; private set; }

        #region public methods
        public JiraClient(IJiraApiRequestFactory jiraApiRequestFactory, IJiraApiRequester jiraApiRequester)
        {
            this.jiraApiRequestFactory = jiraApiRequestFactory;
            this.jiraApiRequester = jiraApiRequester;

            SessionValid = false;
        }


        public bool Authenticate(string username, string password)
        {
            SessionValid = false;

            var request = jiraApiRequestFactory.CreateAuthenticateRequest(username, password);
            try
            {
                jiraApiRequester.DoAuthenticatedRequest<object>(request);
                return true;
            }
            catch (RequestDeniedException)
            {
                return false;
            }
        }


        public bool ValidateSession()
        {
            SessionValid = false;

            var request = jiraApiRequestFactory.CreateValidateSessionRequest();
            try
            {
                jiraApiRequester.DoAuthenticatedRequest<object>(request);
                SessionValid = true;
                return true;
            }
            catch (RequestDeniedException)
            {
                return false;
            }
        }


        public List<Filter> GetFavoriteFilters()
        {
            var request = jiraApiRequestFactory.CreateGetFavoriteFiltersRequest();
            try
            {
                return jiraApiRequester.DoAuthenticatedRequest<List<Filter>>(request);
            }
            catch (RequestDeniedException)
            {
                return null;
            }
        }
        

        public SearchResult GetIssuesByJQL(string jql)
        {
            var request = jiraApiRequestFactory.CreateGetIssuesByJQLRequest(jql);
            try
            {
                return jiraApiRequester.DoAuthenticatedRequest<SearchResult>(request);
            }
            catch (RequestDeniedException)
            {
                return null;
            }
        }


        public string GetIssueSummary(string key)
        {
            var request = jiraApiRequestFactory.CreateGetIssueSummaryRequest(key);
            try
            {
                return jiraApiRequester.DoAuthenticatedRequest<Issue>(request).Fields.Summary;
            }
            catch (RequestDeniedException)
            {
                return "";
            }
        }


        public bool PostWorklog(string key, TimeSpan time, string comment)
        {
            var request = jiraApiRequestFactory.CreatePostWorklogRequest(key, time, comment);
            try
            {
                jiraApiRequester.DoAuthenticatedRequest<object>(request);
                return true;
            }
            catch (RequestDeniedException)
            {
                return false;
            }
        }


        public bool PostComment(string key, string comment)
        {
            var request = jiraApiRequestFactory.CreatePostCommentRequest(key, comment);
            try
            {
                jiraApiRequester.DoAuthenticatedRequest<object>(request);
                return true;
            }
            catch (RequestDeniedException)
            {
                return false;
            }
        }
        #endregion


        #region protected methods
        protected object DoAuthenticatedRequest(IRestRequest request)
        {
            return DoAuthenticatedRequest<object>(request);
        }

        protected T DoAuthenticatedRequest<T>(IRestRequest request)
            where T : new()
        {
            /*
            if (string.IsNullOrEmpty(this.BaseUrl))
                return default(T);

            var client = restClientFactory.Create(BaseUrl);

            IRestResponse<T> response = client.Execute<T>(request);

            // If login session has expired, try to login, and then re-execute the original request
            if (response.StatusCode == HttpStatusCode.Unauthorized) {
                if (!ReAuthenticate())
                    return default(T);

                response = client.Execute<T>(request);
            }

            if (response.StatusCode != HttpStatusCode.OK)
                return default(T);

            return response.Data;
            */
            return default(T);
        }


        protected bool ReAuthenticate()
        {
            /*
            if (string.IsNullOrEmpty(this.BaseUrl))
                return false;

            var client = restClientFactory.Create(BaseUrl, true);

            var request = restRequestFactory.Create("/rest/auth/1/session", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new {
                username = this.username,
                password = this.password
            });

            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
                return false;

            return true;
            */
            return false;
        }
        #endregion


        #region private members
        private IJiraApiRequestFactory jiraApiRequestFactory;
        private IJiraApiRequester jiraApiRequester;
        #endregion
    }
}
