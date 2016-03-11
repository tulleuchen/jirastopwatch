using RestSharp;

namespace StopWatch
{
    interface IJiraApiRequester
    {
        T DoAuthenticatedRequest<T>(IRestRequest request)
            where T : new();
    }
}
