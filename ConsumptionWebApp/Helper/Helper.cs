using System;
using System.Net.Http;

namespace ConsumptionWebApp.Helper
{
    public class UserAPI
    {
        public HttpClient Initial()
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:44306/");

            return client;
        }
    }
}
