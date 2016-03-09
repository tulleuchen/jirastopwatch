using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StopWatch
{
    internal interface IRestRequestFactory
    {
        IRestRequest Create(string url, Method method);
    }
}
