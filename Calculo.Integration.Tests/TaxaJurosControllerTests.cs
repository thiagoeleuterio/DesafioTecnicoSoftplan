using Calculo.Integration.Tests.Helpers;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

namespace Calculo.Integration.Tests
{
    [TestFixture]
    public class TaxaJurosControllerTests
    {
        private readonly HttpServiceClient _serviceClient;

        public TaxaJurosControllerTests()
        {
            _serviceClient = NativeInjectorBootStrapper.GetInstance<HttpServiceClient>();
            _serviceClient.Should().NotBeNull();
        }

        [Test]
        public async Task Obter_taxa_de_juros_com_sucesso()
        {
            var response = await _serviceClient.GetAsync("TaxaJuros/taxaJuros");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = response.ConvertResponseMessageAsType<decimal>();
            result.Data.Should().BeGreaterThan(0);
            result.Errors.Should().BeNull();
            result.Success.Should().BeTrue();
        }
    }
}