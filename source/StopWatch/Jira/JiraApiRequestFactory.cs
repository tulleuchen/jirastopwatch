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

namespace StopWatch
{
    internal class AuthenticateNotYetCalledException : Exception
    {
    }

    internal class JiraApiRequestFactory : IJiraApiRequestFactory
    {
        #region public methods
        public JiraApiRequestFactory(IRestRequestFactory restRequestFactory)
        {
            this.restRequestFactory = restRequestFactory;
            this.username = "";
            this.password = "";
        }


        public IRestRequest CreateValidateSessionRequest()
        {
            var request = restRequestFactory.Create("/rest/auth/1/session", Method.GET);
            return request;
        }


        public IRestRequest CreateGetFavoriteFiltersRequest()
        {
            var request = restRequestFactory.Create("/rest/api/2/filter/favourite", Method.GET);
            return request;
        }
        

        public IRestRequest CreateGetIssuesByJQLRequest(string jql)
        {
            var request = restRequestFactory.Create(String.Format("/rest/api/2/search?jql={0}", jql), Method.GET);
            return request;
        }


        public IRestRequest CreateGetIssueSummaryRequest(string key)
        {
            var request = restRequestFactory.Create(String.Format("/rest/api/2/issue/{0}", key.Trim()), Method.GET);
            return request;
        }


        public IRestRequest CreatePostWorklogRequest(string key, TimeSpan time, string comment)
        {
            var request = restRequestFactory.Create(String.Format("/rest/api/2/issue/{0}/worklog", key.Trim()), Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new
                {
                    timeSpent = JiraTimeHelpers.TimeSpanToJiraTime(time),
                    comment = comment
                }
            );
            return request;
        }


        public IRestRequest CreatePostCommentRequest(string key, string comment)
        {
            var request = restRequestFactory.Create(String.Format("/rest/api/2/issue/{0}/comment", key.Trim()), Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new
                {
                    body = comment
                }
            );
            return request;
        }


        public IRestRequest CreateAuthenticateRequest(string username, string password)
        {
            this.username = username;
            this.password = password;

            var request = restRequestFactory.Create("/rest/auth/1/session", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new {
                username = this.username,
                password = this.password
            });
            return request;
        }

        public IRestRequest CreateReAuthenticateRequest()
        {
            if (string.IsNullOrEmpty(this.username))
                throw new AuthenticateNotYetCalledException();

            return CreateAuthenticateRequest(this.username, this.password);
        }
        #endregion


        #region private members
        private IRestRequestFactory restRequestFactory;

        private string username;
        private string password;
        #endregion
    }
}
