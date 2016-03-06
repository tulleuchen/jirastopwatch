/**************************************************************************
Copyright 2015 Carsten Gehling

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
using System.Linq;
using System.Net;

namespace StopWatch
{
    internal class JiraClient
    {
        public string BaseUrl { get; set; }

        public bool SessionValid { get; private set; }

        #region public methods
        public JiraClient(IRestClientFactory restClientFactory, IRestRequestFactory restRequestFactory)
        {
            this.restClientFactory = restClientFactory;
            this.restRequestFactory = restRequestFactory;

            SessionValid = false;
        }


        public bool Authenticate(string username, string password)
        {
            SessionValid = false;

            this.username = username;
            this.password = password;

            if (!ReAuthenticate())
                return false;

            return true;
        }


        public void OpenIssueInBrowser(string key)
        {
            if (string.IsNullOrEmpty(this.BaseUrl))
                return;

            string url = this.BaseUrl;
            if (url.Last() != '/')
                url += "/";
            url += "browse/";
            url += key;
            System.Diagnostics.Process.Start(url);
        }


        public bool ValidateSession()
        {
            SessionValid = false;

            if (string.IsNullOrEmpty(this.BaseUrl))
                return false;

            var client = restClientFactory.Create(BaseUrl);

            var request = restRequestFactory.Create("/rest/auth/1/session", Method.GET);

            IRestResponse response;

            response = client.Execute(request);

            // If login session has expired, try to login, and then re-execute the original request
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                if (!ReAuthenticate())
                    return false;

                response = client.Execute(request);
            }

            if (response.StatusCode != HttpStatusCode.OK)
                return false;

            SessionValid = true;

            return true;
        }


        public List<Filter> GetFavoriteFilters()
        {
            if (string.IsNullOrEmpty(this.BaseUrl))
                return null;

            var client = restClientFactory.Create(BaseUrl);

            var request = restRequestFactory.Create("/rest/api/2/filter/favourite", Method.GET);

            IRestResponse<List<Filter>> response;

            response = client.Execute<List<Filter>>(request);

            // If login session has expired, try to login, and then re-execute the original request
            if (response.StatusCode == HttpStatusCode.Unauthorized) {
                if (!ReAuthenticate())
                    return null;

                response = client.Execute<List<Filter>>(request);
            }

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return response.Data;
        }
        

        public List<Issue> GetIssuesByJQL(string jql)
        {
            if (string.IsNullOrEmpty(this.BaseUrl))
                return null;

            var client = restClientFactory.Create(BaseUrl);

            var request = restRequestFactory.Create(String.Format("/rest/api/2/search?jql={0}", jql), Method.GET);

            IRestResponse<SearchResult> response;
            response = client.Execute<SearchResult>(request);

            // If login session has expired, try to login, and then re-execute the original request
            if (response.StatusCode == HttpStatusCode.Unauthorized) {
                if (!ReAuthenticate())
                    return null;

                response = client.Execute<SearchResult>(request);
            }

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return response.Data.Issues;
        }


        public string GetIssueSummary(string key)
        {
            if (string.IsNullOrEmpty(this.BaseUrl))
                return "";

            var client = restClientFactory.Create(BaseUrl);

            var request = restRequestFactory.Create(String.Format("/rest/api/2/issue/{0}", key), Method.GET);

            IRestResponse<Issue> response;
            response = client.Execute<Issue>(request);

            // If login session has expired, try to login, and then re-execute the original request
            if (response.StatusCode == HttpStatusCode.Unauthorized) {
                if (!ReAuthenticate())
                    return "";

                response = client.Execute<Issue>(request);
            }

            if (response.StatusCode != HttpStatusCode.OK)
                return "";

            return response.Data.Fields.Summary;
        }


        public bool PostWorklog(string key, TimeSpan time, string comment)
        {
            if (string.IsNullOrEmpty(this.BaseUrl))
                return false;

            var client = restClientFactory.Create(BaseUrl);

            var request = restRequestFactory.Create(String.Format("/rest/api/2/issue/{0}/worklog", key), Method.POST);
            request.RequestFormat = DataFormat.Json;

            request.AddBody(new
                {
                    timeSpent = JiraTimeHelpers.TimeSpanToJiraTime(time),
                    comment = comment
                }
            );

            IRestResponse response;

            response = client.Execute(request);

            // If login session has expired, try to login, and then re-execute the original request
            if (response.StatusCode == HttpStatusCode.Unauthorized) {
                if (!ReAuthenticate())
                    return false;

                response = client.Execute(request);
            }

            if (response.StatusCode != HttpStatusCode.Created)
                return false;

            return true;
        }


        public bool PostComment(string key, string comment)
        {
            if (string.IsNullOrEmpty(this.BaseUrl))
                return false;

            var client = restClientFactory.Create(BaseUrl);

            var request = restRequestFactory.Create(String.Format("/rest/api/2/issue/{0}/worklog", key), Method.POST);
            request.RequestFormat = DataFormat.Json;

            request.AddBody(new
                {
                    body = comment
                }
            );

            IRestResponse response;

            response = client.Execute(request);

            // If login session has expired, try to login, and then re-execute the original request
            if (response.StatusCode == HttpStatusCode.Unauthorized) {
                if (!ReAuthenticate())
                    return false;

                response = client.Execute(request);
            }

            if (response.StatusCode != HttpStatusCode.Created)
                return false;

            return true;
        }
        #endregion


        #region private methods
        private bool ReAuthenticate()
        {
            if (string.IsNullOrEmpty(this.BaseUrl))
                return false;

            var client = restClientFactory.Create(BaseUrl, true);

            var request = restRequestFactory.Create("/rest/auth/1/session", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new {
                username = this.username,
                password = this.password
            });

            IRestResponse response = client.Execute<Issue>(request);
            if (response.StatusCode != HttpStatusCode.OK)
                return false;

            return true;
        }
        #endregion


        #region private members
        private string username;
        private string password;

        private IRestClientFactory restClientFactory;
        private IRestRequestFactory restRequestFactory;
        #endregion
    }
}
