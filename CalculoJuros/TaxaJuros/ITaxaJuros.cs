using System.Threading.Tasks;

namespace CalculoJuros.Juros
{
    public interface ITaxaJuros
    {
        Task<decimal> ObterTaxaDeJuros();
    }
}