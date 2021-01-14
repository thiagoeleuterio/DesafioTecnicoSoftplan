using CalculoJuros.Juros;
using System.Threading.Tasks;

namespace CalculoJuros
{
    public class TaxaJuros : ITaxaJuros
    {
        /// <summary>
        /// Obter valor da taxa de juros
        /// </summary>
        /// <returns></returns>
        public async Task<decimal> ObterTaxaDeJuros()
        {
            return 0.01M;
        }
    }
}