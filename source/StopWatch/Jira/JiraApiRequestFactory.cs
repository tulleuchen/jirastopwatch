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
    internal class JiraApiRequestFactory : IJiraApiRequestFactory
    {
        #region public methods
        public JiraApiRequestFactory(IRestRequestFactory restRequestFactory)
        {
            this.restRequestFactory = restRequestFactory;
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
            var request = restRequestFactory.Create(String.Format("/rest/api/2/search?jql={0}&maxResults=200", jql), Method.GET);
            return request;
        }


        public IRestRequest CreateGetIssueSummaryRequest(string key)
        {
            var request = restRequestFactory.Create(String.Format("/rest/api/2/issue/{0}", key.Trim()), Method.GET);
            return request;
        }

        public IRestRequest CreateGetIssueTimetrackingRequest(string key)
        {
            var request = restRequestFactory.Create(String.Format("/rest/api/2/issue/{0}?fields=timetracking", key.Trim()), Method.GET);
            return request;
        }


        public IRestRequest CreatePostWorklogRequest(string key, DateTimeOffset started, TimeSpan time, string comment, EstimateUpdateMethods adjustmentMethod, string adjustmentValue)
        {
            var request = restRequestFactory.Create(String.Format("/rest/api/2/issue/{0}/worklog", key.Trim()), Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new
                {
                    timeSpent = JiraTimeHelpers.TimeSpanToJiraTime(time),
                    started = JiraTimeHelpers.DateTimeToJiraDateTime(started),
                    comment = comment
                }
            );
            switch(adjustmentMethod) {
                case EstimateUpdateMethods.Leave:
                    request.AddQueryParameter("adjustEstimate", "leave");
                    break;
                case EstimateUpdateMethods.SetTo:
                    request.AddQueryParameter("adjustEstimate", "new");
                    request.AddQueryParameter("newEstimate", adjustmentValue);
                    break;
                case EstimateUpdateMethods.ManualDecrease:
                    request.AddQueryParameter("adjustEstimate", "manual");
                    request.AddQueryParameter("reduceBy", adjustmentValue);
                    break;
                case EstimateUpdateMethods.Auto:
                    request.AddQueryParameter("adjustEstimate", "auto");
                    break;
            }
            return request;
        }

        public IRestRequest CreateGetConfigurationRequest()
        {
            return restRequestFactory.Create("/rest/api/2/configuration", Method.GET);
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

        public IRestRequest CreateGetAvailableTransitions(string key)
        {
            var request = restRequestFactory.Create(String.Format("/rest/api/2/issue/{0}/transitions", key.Trim()), Method.GET);
            return request;
        }

        public IRestRequest CreateDoTransition(string key, int transitionId)
        {
            var request = restRequestFactory.Create(String.Format("/rest/api/2/issue/{0}/transitions", key.Trim()), Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new
                {
                    transition = new
                    {
                        id = transitionId
                    }
                }
            );
            return request;
        }
        #endregion


        #region private members
        private IRestRequestFactory restRequestFactory;
        #endregion
    }
}
