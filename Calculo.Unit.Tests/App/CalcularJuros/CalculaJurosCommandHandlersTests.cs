using Calculo.Api.App;
using Calculo.Api.App.CalcularJuros;
using FluentAssertions;
using MediatR;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace Calculo.Unit.Tests.App.CalcularJuros
{
    [TestFixture]
    public class CalculaJurosCommandHandlersTests
    {
        [TestCase(100, 5, 0.01, 105.10)]
        public async Task Calcula_taxa_de_juros_com_sucesso(decimal valorInicial, int quantidadeMeses, decimal taxaJuros, decimal resultadoCalculado)
        {
            //Arranges
            var command = CalculaJurosCommand.Factory.Create(valorInicial, quantidadeMeses, taxaJuros);

            //Setup's
            var mockMediator = new Mock<IMediator>();
            var mockNotificationHandler = new Mock<DomainNotificationHandler>();

            //Act
            var commandHandlers = new CalculaJurosCommandHandlers(mockMediator.Object, mockNotificationHandler.Object);
            var result = await commandHandlers.Handle(command, CancellationToken.None);

            //Asserts
            result.Should().BeGreaterThan(0);
            result.Should().Be(105.10M);
            mockMediator.Verify(x => x.Publish(It.IsAny<DomainNotification>(), CancellationToken.None), Times.Never());
        }

        [TestCase(10, -10, 0)]
        public async Task Calcula_taxa_de_juros(decimal valorInicial, int quantidadeMeses, decimal taxaJuros)
        {
            //Arranges
            var command = CalculaJurosCommand.Factory.Create(valorInicial, quantidadeMeses, taxaJuros);

            //Setup's
            var mockMediator = new Mock<IMediator>();
            var mockNotificationHandler = new Mock<DomainNotificationHandler>();
            mockNotificationHandler.SetupGet(x => x.HasNotifications).Returns(true);

            //Act
            var commandHandlers = new CalculaJurosCommandHandlers(mockMediator.Object, mockNotificationHandler.Object);
            var result = await commandHandlers.Handle(command, CancellationToken.None);

            //Asserts
            mockMediator.Verify(x => x.Publish(It.IsAny<DomainNotification>(), CancellationToken.None), Times.Once);
            result.Should().Be(0);
        }
    }
}