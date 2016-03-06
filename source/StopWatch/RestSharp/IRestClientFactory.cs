using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StopWatch
{
    interface IRestClientFactory
    {
        IRestClient Create(string baseUrl, bool invalidateCookies = false);
    }
}
