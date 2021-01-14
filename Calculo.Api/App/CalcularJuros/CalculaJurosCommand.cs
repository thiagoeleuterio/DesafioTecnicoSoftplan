using MediatR;

namespace Calculo.Api.App.CalcularJuros
{
    /// <summary>
    /// 
    /// </summary>
    public class CalculaJurosCommand : IRequest<decimal>
    {
        /// <summary>
        /// 
        /// </summary>
        public decimal ValorInicial { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int QuantidadeMeses { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal TaxaJuros { get; private set; }

        #region Factory

        /// <summary>
        /// 
        /// </summary>
        public static class Factory
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="valorInicial"></param>
            /// <param name="quantidadeMeses"></param>
            /// <param name="taxaJuros"></param>
            /// <returns></returns>
            public static CalculaJurosCommand Create(decimal valorInicial, int quantidadeMeses, decimal taxaJuros)
            {
                return new CalculaJurosCommand()
                {
                    ValorInicial = valorInicial,
                    QuantidadeMeses = quantidadeMeses,
                    TaxaJuros = taxaJuros
                };
            }
        }

        #endregion

    }
}