using RestSharp;

namespace StopWatch
{
    internal class RestRequestFactory : IRestRequestFactory
    {
        public IRestRequest Create(string url, Method method)
        {
            return new RestRequest(url, method);
        }
    }
}
