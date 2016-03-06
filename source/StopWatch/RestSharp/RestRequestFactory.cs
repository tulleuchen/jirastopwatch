using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
