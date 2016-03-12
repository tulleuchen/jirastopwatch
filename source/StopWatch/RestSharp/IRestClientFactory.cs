using RestSharp;

namespace StopWatch
{
    internal interface IRestClientFactory
    {
        string BaseUrl { get; set; }

        IRestClient Create(bool invalidateCookies = false);
    }
}
