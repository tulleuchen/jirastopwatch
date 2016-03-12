using RestSharp;
using System.Net;

namespace StopWatch
{
    internal class RestClientFactory : IRestClientFactory
    {
        public string BaseUrl { get; set; }

        public RestClientFactory()
        {
            BaseUrl = "";
            this.cookieContainer = new CookieContainer();
        }


        public IRestClient Create(bool invalidateCookies = false)
        {
            if (invalidateCookies)
                cookieContainer = new CookieContainer();

            RestClient client = new RestClient(BaseUrl);
            client.CookieContainer = cookieContainer;
            return client;
        }

        private CookieContainer cookieContainer;
    }
}
