using Store.Interfaces.Communication;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Tiny.RestClient;

namespace Store.Common.Communication
{
    public class RestClient : TinyRestClient, IRestClient
    {
        public RestClient(HttpClient httpClient, string serverAddress)
            : base(httpClient, serverAddress) { }
    }
}
