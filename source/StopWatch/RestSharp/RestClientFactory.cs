using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StopWatch
{
    internal class RestClientFactory : IRestClientFactory
    {
        public RestClientFactory(string baseUrl)
        {
            this.baseUrl = baseUrl;
            this.cookieContainer = new CookieContainer();
        }


        public IRestClient Create(bool invalidateCookies = false)
        {
            if (invalidateCookies)
                this.cookieContainer = new CookieContainer();

            RestClient client = new RestClient(this.baseUrl);
            client.CookieContainer = this.cookieContainer;
            return client;
        }

        private CookieContainer cookieContainer;

        private string baseUrl;
    }
}
