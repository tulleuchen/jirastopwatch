using RestSharp;

namespace StopWatch
{
    internal interface IRestRequestFactory
    {
        IRestRequest Create(string url, Method method);
    }
}
