using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StopWatch
{
    class JiraApiRequestFactory
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
            var request = restRequestFactory.Create(String.Format("/rest/api/2/search?jql={0}", jql), Method.GET);
            return request;
        }


        public IRestRequest CreateGetIssueSummaryRequest(string key)
        {
            var request = restRequestFactory.Create(String.Format("/rest/api/2/issue/{0}", key), Method.GET);
            return request;
        }


        public IRestRequest CreatePostWorklogRequest(string key, TimeSpan time, string comment)
        {
            var request = restRequestFactory.Create(String.Format("/rest/api/2/issue/{0}/worklog", key), Method.POST);
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
            var request = restRequestFactory.Create(String.Format("/rest/api/2/issue/{0}/worklog", key), Method.POST);
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
            var request = restRequestFactory.Create("/rest/auth/1/session", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new {
                username = username,
                password = password
            });
            return request;
        }
        #endregion


        #region private members
        private IRestRequestFactory restRequestFactory;
        #endregion
    }
}
