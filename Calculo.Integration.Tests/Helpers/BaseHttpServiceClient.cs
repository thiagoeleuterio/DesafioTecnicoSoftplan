using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Calculo.Integration.Tests.Helpers
{
    public abstract class BaseHttpServiceClient
    {
        private readonly HttpClient _client;
        private readonly int _port = 44350;

        internal BaseHttpServiceClient(TestServer client)
        {
            this._client = client.CreateClient();
            this._client.DefaultRequestHeaders.Accept.Clear();
            this._client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<HttpResponseMessage> GetAsync(string route)
            => await _client
                .SendAsync(CreateRequestMessage(HttpMethod.Get, $"http://localhost:{this._port}/api/v1/{route}"));

        private static HttpRequestMessage CreateRequestMessage(HttpMethod method, string route)
            => new HttpRequestMessage { Method = method, RequestUri = new Uri(route) };
    }
}
