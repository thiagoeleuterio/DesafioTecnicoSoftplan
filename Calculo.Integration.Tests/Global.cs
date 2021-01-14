using Calculo.Integration.Tests.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.IO;

namespace Calculo.Integration.Tests
{
    [SetUpFixture]
    public partial class Global
    {
        private TestServer _serverCalc;

        [OneTimeSetUp]
        public void Setup()
        {
            _serverCalc = CreateInMemoryTestServerCalc();

            var service = NativeInjectorBootStrapper.RegisterAll();
            service.AddSingleton(new HttpServiceClient(_serverCalc));
        }

        private static TestServer CreateInMemoryTestServerCalc()
        {
            var projectName = typeof(Api.Startup).Assembly.GetName().Name;
            var pathWebApi = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", projectName));
            var webHostBuilder = CreateTestServer(pathWebApi)
                .UseStartup<Api.Startup>();
            return new TestServer(webHostBuilder);
        }

        private static IWebHostBuilder CreateTestServer(string pathWebApi)
            => new WebHostBuilder()
                .UseEnvironment("IntegrationTest")
                .UseContentRoot(pathWebApi)
                .UseConfiguration(new ConfigurationBuilder()
                    .SetBasePath(pathWebApi)
                    .Build());
    }
}