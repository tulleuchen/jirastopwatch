using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StopWatch
{
    internal class JiraApiRequester : IJiraApiRequester
    {

        public JiraApiRequester(IRestClientFactory restClientFactory, IRestRequestFactory restRequestFactory)
        {
            this.restClientFactory = restClientFactory;
            this.restRequestFactory = restRequestFactory;
        }


        public IRestRequest CreateRequest(string url, Method method)
        {
            return restRequestFactory.Create(url, method);
        }


        public bool Authenticate(string username, string password)
        {
            this.username = username;
            this.password = password;

            if (!ReAuthenticate())
                return false;

            return true;
        }


        public T DoAuthenticatedRequest<T>(IRestRequest request)
            where T : new()
        {

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
            return true;
        }


        private IRestClientFactory restClientFactory;
        private IRestRequestFactory restRequestFactory;

        private string username;
        private string password;
    }
}
