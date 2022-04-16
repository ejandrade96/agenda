using Microsoft.AspNetCore.Mvc;
using DTOs = Agenda.Dominio.DTOs;
using Agenda.Dominio.Servicos;
using System.Threading.Tasks;
using AutoMapper;
using System.Collections.Generic;
using Agenda.Dominio.Erros;
using Microsoft.AspNetCore.Http;

namespace Agenda.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContatosController : ControllerBase
    {
        private readonly Contato _servico;

        private readonly Usuario _servicoUsuario;

        private readonly IMapper _mapper;

        public ContatosController(Contato servico, Usuario servicoUsuario, IMapper mapper)
        {
            _servico = servico;
            _servicoUsuario = servicoUsuario;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna um contato
        /// </summary>
        /// <param name="token" example="604b7403-8b0d-4e2c-9ba0-90f049568737">Token de autorização</param>
        /// <param name="id" example="123">Id do contato</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(DTOs.Contato), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Erro), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterPorId([FromHeader(Name = "Authorization")] string token, [FromRoute] int id)
        {
            var tokenEhValido = await _servicoUsuario.ValidarToken(token);

            if (tokenEhValido)
            {
                var resposta = await _servico.ObterPorId(id);

                if (resposta.TemErro())
                {
                    return StatusCode(resposta.Erro.StatusCode, new { resposta.Erro.Mensagem });
                }

                var dadosContato = _mapper.Map<DTOs.Contato>(resposta.Resultado);

                return Ok(dadosContato);
            }

            return Unauthorized();
        }

        /// <summary>
        /// Retorna todos os contatos
        /// </summary>
        /// <param name="nome">Parâmetro opcional</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<DTOs.Contato>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string nome)
        {
            var contatos = await _servico.Listar(nome);

            var dadosContatos = _mapper.Map<List<DTOs.Contato>>(contatos);

            return Ok(dadosContatos);
        }

        /// <summary>
        /// Remove um contato
        /// </summary>
        /// <param name="token" example="604b7403-8b0d-4e2c-9ba0-90f049568737">Token de autorização</param>
        /// <param name="id" example="123">Id do contato</param>
        /// <remarks>Ao excluir um contato o mesmo será removido permanentemente do banco de dados.</remarks>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Erro), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromHeader(Name = "Authorization")] string token, [FromRoute] int id)
        {
            var tokenEhValido = await _servicoUsuario.ValidarToken(token);

            if (tokenEhValido)
            {
                var resposta = await _servico.Deletar(id);

                if (resposta.TemErro())
                {
                    return StatusCode(resposta.Erro.StatusCode, new { resposta.Erro.Mensagem });
                }

                return NoContent();
            }

            return Unauthorized();
        }

        /// <summary>
        /// Atualiza um contato
        /// </summary>
        /// <param name="token" example="604b7403-8b0d-4e2c-9ba0-90f049568737">Token de autorização</param>
        /// <param name="dadosContato"></param>
        /// <param name="id" example="123">Id do contato</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Erro), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromHeader(Name = "Authorization")] string token, [FromBody] DTOs.AlteraContato dadosContato, [FromRoute] int id)
        {
            var tokenEhValido = await _servicoUsuario.ValidarToken(token);

            if (tokenEhValido)
            {
                dadosContato.Id = id;
                var resposta = await _servico.Atualizar(dadosContato);

                if (resposta.TemErro())
                {
                    return StatusCode(resposta.Erro.StatusCode, new { resposta.Erro.Mensagem });
                }

                return NoContent();
            }

            return Unauthorized();
        }
    }
}