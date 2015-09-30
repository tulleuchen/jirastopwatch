using RestSharp;
using System;
using System.Linq;
using System.Net;

namespace StopWatch
{
    internal class JiraClient
    {
        public string BaseUrl { get; set; }

        #region public methods
        public JiraClient()
        {
            this.cookieContainer = new CookieContainer();
        }


        public bool Authenticate(string username, string password)
        {
            var client = new RestClient(BaseUrl);
            client.CookieContainer = this.cookieContainer;

            var request = new RestRequest("/rest/auth/1/session", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new {
                username = username,
                password = password
            });
            
            IRestResponse response = client.Execute<Issue>(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return false;

            return true;
        }


        public string GetIssueSummary(string key)
        {
            var client = new RestClient(BaseUrl);
            client.CookieContainer = this.cookieContainer;

            var request = new RestRequest(String.Format("/rest/api/2/issue/{0}", key), Method.GET);

            IRestResponse<Issue> response = client.Execute<Issue>(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return "";

            return response.Data.Fields.Summary;
        }


        public void OpenIssueInBrowser(string key)
        {
            string url = this.BaseUrl;
            if (url.Last() != '/')
                url += "/";
            url += "browse/";
            url += key;
            System.Diagnostics.Process.Start(url);
        }
        #endregion


        #region private members
        private CookieContainer cookieContainer;
        #endregion
    }
}
