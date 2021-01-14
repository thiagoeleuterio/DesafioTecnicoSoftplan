using Microsoft.AspNetCore.TestHost;

namespace Calculo.Integration.Tests.Helpers
{
    public class HttpServiceClient : BaseHttpServiceClient
    {
        public HttpServiceClient(TestServer client) : base(client)
        {
        }
    }
}