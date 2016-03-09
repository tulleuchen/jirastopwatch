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
        public RestClientFactory()
        {
            this.cookieContainer = new CookieContainer();
        }


        public IRestClient Create(string baseUrl, bool invalidateCookies = false)
        {
            if (invalidateCookies)
                this.cookieContainer = new CookieContainer();

            RestClient client = new RestClient(baseUrl);
            client.CookieContainer = this.cookieContainer;
            return client;
        }

        private CookieContainer cookieContainer;
    }
}
