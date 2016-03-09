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
        public T DoAuthenticatedRequest<T>(IRestRequest request);

    }
}
