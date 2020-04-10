using Anuncios.Domain.Commands;
using Anuncios.Domain.Handlers;
using Anuncios.Domain.Repositories.Read;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharedKernel.Domain.Commands;
using System.Threading.Tasks;

namespace Anuncios.Api.Controllers
{
    [ApiController]
    [Route("api/anuncio")]
    public class AnuncioController : BaseController
    {
        private readonly ILogger<AnuncioController> _logger;
        public AnuncioController(ILogger<AnuncioController> logger) : base()
        {
            _logger = logger;
        }
        [Route("consultar")]
        [HttpPost]
        public async Task<IActionResult> Consultar([FromBody] ConsultarCommand command, 
                                    [FromServices] IAnuncioRepository _anuncioRepository)
        {
            var result = await _anuncioRepository.ConsultarAsync(command);
            return Response(result);
        }

        [Route("criar")]
        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarAnuncioCommand command,
            [FromServices] AnuncioHandler handler)
        {
            if (ModelState.IsValid)
            {
                var result = await handler.Handle<CriarAnuncioCommand>(command);
                return await Response(result, handler);
            }
            else
                return InvalidModelState();
        }

        [Route("alterar")]
        [HttpPut]
        public async Task<IActionResult> Alterar([FromBody] AlterarAnuncioCommand command,
            [FromServices] AnuncioHandler handler)
        {
            if (ModelState.IsValid)
            {
                var result = await handler.Handle<AlterarAnuncioCommand>(command);
                return await Response(result, handler);
            }
            else
                return InvalidModelState();
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> ObterPeloId(int id,
            [FromServices] IAnuncioRepository _anuncioRepository)
        {
            if (ModelState.IsValid)
            {
                var result = await _anuncioRepository.ObterPeloIdAsync(id);
                return Response(result);
            }
            else
                return InvalidModelState();
        }

        [Route("excluir")]
        [HttpDelete]
        public async Task<IActionResult> Delete(ExcluirCommand command,
            [FromServices] AnuncioHandler handler)
        {
            var result = await handler.Handle<ExcluirCommand>(command);
            return await Response(result, handler);
        }
    }
}
