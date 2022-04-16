using System.Collections.Generic;
using System.Threading.Tasks;
using Agenda.Dominio.Erros;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTOs = Agenda.Dominio.DTOs;

namespace Agenda.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly Dominio.Servicos.Usuario _servico;

        private readonly Dominio.Servicos.Contato _servicoContato;

        private readonly IMapper _mapper;

        public UsuariosController(Dominio.Servicos.Usuario servico, Dominio.Servicos.Contato servicoContato, IMapper mapper)
        {
            _servico = servico;
            _servicoContato = servicoContato;
            _mapper = mapper;
        }

        /// <summary>
        /// Cadastra um usuário
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Erro), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> Post([FromForm] IFormCollection usuario)
        {
            var dadosUsuario = new DTOs.NovoUsuario
            {
                Login = usuario["login"],
                Senha = usuario["senha"],
                Nome = usuario["nome"]
            };

            var resposta = await _servico.Salvar(dadosUsuario);

            if (resposta.TemErro())
            {
                return StatusCode(resposta.Erro.StatusCode, new { resposta.Erro.Mensagem });
            }

            var id = resposta.Resultado.Id;

            return Created($"/usuarios/{id}", new { Id = id });
        }

        /// <summary>
        /// Autentica um usuário
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [ProducesResponseType(typeof(DTOs.Usuario), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Erro), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        [Route("/login")]
        public async Task<IActionResult> Login([FromForm] IFormCollection usuario)
        {
            var dadosUsuario = new DTOs.NovoUsuario
            {
                Login = usuario["login"],
                Senha = usuario["senha"]
            };

            var resposta = await _servico.Autenticar(dadosUsuario);

            if (resposta.TemErro())
            {
                return StatusCode(resposta.Erro.StatusCode, new { resposta.Erro.Mensagem });
            }

            return Ok(resposta.Resultado);
        }

        /// <summary>
        /// Retorna todos os usuários
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<DTOs.Usuario>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var usuarios = await _servico.Listar();

            var dadosUsuarios = _mapper.Map<List<DTOs.Usuario>>(usuarios);

            return Ok(dadosUsuarios);
        }

        /// <summary>
        /// Retorna um usuário
        /// </summary>
        /// <param name="id" example="123">Id do usuário</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(DTOs.Usuario), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Erro), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var resposta = await _servico.ObterPorId(id);

            if (resposta.TemErro())
            {
                return StatusCode(resposta.Erro.StatusCode, new { resposta.Erro.Mensagem });
            }

            var dadosUsuario = _mapper.Map<DTOs.Usuario>(resposta.Resultado);

            return Ok(dadosUsuario);
        }

        /// <summary>
        /// Remove um usuário
        /// </summary>
        /// <param name="id" example="123">Id do usuário</param>
        /// <remarks>Ao excluir um usuário o mesmo será removido permanentemente do banco de dados.</remarks>
        /// <returns></returns>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(Erro), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Erro), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resposta = await _servico.Deletar(id);

            if (resposta.TemErro())
            {
                return StatusCode(resposta.Erro.StatusCode, new { resposta.Erro.Mensagem });
            }

            return NoContent();
        }

        /// <summary>
        /// Cadastra um contato
        /// </summary>
        /// <param name="token" example="604b7403-8b0d-4e2c-9ba0-90f049568737">Token de autorização</param>
        /// <param name="dadosContato"></param>
        /// <param name="usuarioId" example="123">Id do usuário</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Erro), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("/usuarios/{usuarioId:int}/contatos")]
        public async Task<IActionResult> Post([FromHeader(Name = "Authorization")] string token, [FromBody] DTOs.NovoContato dadosContato, int usuarioId)
        {
            var tokenEhValido = await _servico.ValidarToken(token);

            if (tokenEhValido)
            {
                dadosContato.UsuarioId = usuarioId;
                var resposta = await _servicoContato.Salvar(dadosContato);

                if (resposta.TemErro())
                {
                    return StatusCode(resposta.Erro.StatusCode, new { resposta.Erro.Mensagem });
                }

                var id = resposta.Resultado.Id;

                return Created($"/contatos/{id}", new { Id = id });
            }

            return Unauthorized();
        }

        /// <summary>
        /// Retorna todos os contatos de um usuário
        /// </summary>
        /// <param name="token" example="604b7403-8b0d-4e2c-9ba0-90f049568737">Token de autorização</param>
        /// <param name="usuarioId" example="123">Id do usuário</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<DTOs.Contato>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Erro), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("/usuarios/{usuarioId:int}/contatos")]
        public async Task<IActionResult> GetByUserId([FromHeader(Name = "Authorization")] string token, int usuarioId)
        {
            var tokenEhValido = await _servico.ValidarToken(token);

            if (tokenEhValido)
            {
                var resposta = await _servicoContato.ListarPorUsuarioId(usuarioId);

                if (resposta.TemErro())
                {
                    return StatusCode(resposta.Erro.StatusCode, new { resposta.Erro.Mensagem });
                }

                var contatos = _mapper.Map<List<DTOs.Contato>>(resposta.Resultado);

                return Ok(contatos);
            }

            return Unauthorized();
        }
    }
}