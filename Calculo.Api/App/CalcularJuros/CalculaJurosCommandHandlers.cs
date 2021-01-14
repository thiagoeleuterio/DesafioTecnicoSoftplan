using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Calculo.Api.App.CalcularJuros
{
    /// <summary>
    /// 
    /// </summary>
    public class CalculaJurosCommandHandlers : IRequestHandler<CalculaJurosCommand, decimal>
    {
        private readonly IMediator _mediator;
        private readonly DomainNotificationHandler _notificationHandler;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="notificationHandler"></param>
        public CalculaJurosCommandHandlers(IMediator mediator, INotificationHandler<DomainNotification> notificationHandler)
        {
            _mediator = mediator;
            _notificationHandler = (DomainNotificationHandler)notificationHandler;
        }

        /// <summary>
        /// Calcula juros compostos
        /// </summary>
        /// <param name="message"><see cref="CancellationToken"/></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<decimal> Handle(CalculaJurosCommand message, CancellationToken cancellationToken)
        {
            if (message.QuantidadeMeses < 1 || message.QuantidadeMeses > 12)
                await _mediator.Publish(DomainNotification.Factory.Create(message, $"Quantidade de meses:{message.QuantidadeMeses} incorreto."), cancellationToken);

            if (_notificationHandler.HasNotifications) return 0;
            var potencia = (decimal)Math.Pow(1 + ((double)message.TaxaJuros), message.QuantidadeMeses);
            var valorCheio = Convert.ToDecimal(message.ValorInicial * potencia);
            var valorCalculado = Math.Round(valorCheio, 2);
            return valorCalculado;
        }
    }
}