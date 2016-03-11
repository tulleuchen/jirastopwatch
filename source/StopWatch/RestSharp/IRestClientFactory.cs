using RestSharp;

namespace StopWatch
{
    internal interface IRestClientFactory
    {
        IRestClient Create(bool invalidateCookies = false);
    }
}
