using Calculo.Api.App.CalcularJuros;
using Calculo.Api.Controllers;
using CalculoJuros.Juros;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Calculo.Api.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v1/[controller]"), ApiController]
    public class CalcularJurosController : ControllerBase
    {
        #region Private members

        private readonly IMediator _mediator;
        private readonly ITaxaJuros _taxaQueries;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="taxaQueries"></param>
        public CalcularJurosController(IMediator mediator, ITaxaJuros taxaQueries)
        {
            _mediator = mediator;
            _taxaQueries = taxaQueries;
        }

        #endregion

        /// <summary>
        /// Cálculo em memória de juros compostos
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("calculajuros/{valorInicial:decimal}/{quantidadeMeses:int}")]
        [ProducesResponseType(typeof(ResultMessageResponse<decimal>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CalculaJuros(decimal valorInicial, int quantidadeMeses)
        {
            var taxaJuros = await _taxaQueries.ObterTaxaDeJuros();
            ResultMessageResponse<decimal> result = await _mediator.Send(CalculaJurosCommand.Factory.Create(valorInicial, quantidadeMeses, taxaJuros));
            return Ok(result);
        }

        /// <summary>
        /// Show me the code
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("showmethecode")]
        [ProducesResponseType(typeof(ResultMessageResponse<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ShowMeTheCode()
        {
            return Ok(new ResultMessageResponse<string>("https://github.com/ArturRibeiro/SoftplanCalcTest"));
        }
    }
}