using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Calculo.Integration.Tests.Helpers;

namespace Calculo.Integration.Tests
{
    [TestFixture]
    public class CalcularControllerTests
    {
        private HttpServiceClient _serviceClient;

        public CalcularControllerTests()
        {
            _serviceClient = NativeInjectorBootStrapper.GetInstance<HttpServiceClient>();
            _serviceClient.Should().NotBeNull();
        }

        [TestCase(100, 5)]
        public async Task Calcula_juros_com_sucesso(decimal valorInicial, int meses)
        {
            //Arranger's
            var responseTaxaJuros = await _serviceClient.GetAsync("TaxaJuros/taxaJuros");

            responseTaxaJuros.StatusCode.Should().Be(HttpStatusCode.OK);
            var resultTaxaJuros = responseTaxaJuros.ConvertResponseMessageAsType<decimal>();
            resultTaxaJuros.Data.Should().BeGreaterThan(0);
            resultTaxaJuros.Errors.Should().BeNull();
            resultTaxaJuros.Success.Should().BeTrue();

            //Act
            var responseCalculaJuros = await _serviceClient.GetAsync($"CalcularJuros/calculajuros/{valorInicial}/{meses}");

            //Assent's
            responseCalculaJuros.StatusCode.Should().Be(HttpStatusCode.OK);
            var resultCalculaJuros = responseCalculaJuros.ConvertResponseMessageAsType<double>();
            resultCalculaJuros.Data.Should().BeGreaterThan(0);
            resultCalculaJuros.Errors.Should().BeNull();
            resultCalculaJuros.Success.Should().BeTrue();
        }

        [TestCase(100, 15)]
        [TestCase(100, -15)]
        public async Task Calcula_juros_passando_quantidade_meses_incorreto(decimal valorInicial, int quantidadeMeses)
        {
            //Arranger's

            //Act
            var response = await _serviceClient.GetAsync($"CalcularJuros/calculajuros/{valorInicial}/{quantidadeMeses}");

            //Assent's
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var result = response.ConvertResponseMessageAsType<string>();
            result.Data.Should().BeNullOrEmpty();
            result.Errors.Should().NotBeEmpty();
            result.Errors.Should().HaveCount(1);
            result.Success.Should().BeFalse();
        }
    }
}