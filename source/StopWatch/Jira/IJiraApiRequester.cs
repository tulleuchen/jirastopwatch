using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StopWatch
{
    interface IJiraApiRequester
    {
        T DoAuthenticatedRequest<T>(IRestRequest request)
            where T : new();
    }
}
