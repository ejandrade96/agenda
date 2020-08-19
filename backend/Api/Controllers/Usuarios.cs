using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTOs = Agenda.Dominio.DTOs;

namespace Agenda.Api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class Usuarios : ControllerBase
  {
    private readonly Dominio.Servicos.Usuario _servico;

    private readonly Dominio.Servicos.Contato _servicoContato;

    public Usuarios(Dominio.Servicos.Usuario servico, Dominio.Servicos.Contato servicoContato)
    {
      _servico = servico;
      _servicoContato = servicoContato;
    }

    [HttpPost]
    [Consumes("application/x-www-form-urlencoded")]
    public async Task<IActionResult> Post([FromForm] IFormCollection usuario)
    {
      var dadosUsuario = new DTOs.Usuario()
      {
        Login = usuario["login"],
        Senha = usuario["senha"]
      };

      var id = await _servico.Salvar(dadosUsuario);

      return Created($"/usuarios/{id}", new { Id = id });
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var usuarios = await _servico.Listar();

      var dadosUsuarios = usuarios.Select((usuario) => new DTOs.Usuario
      {
        Id = usuario.Id,
        Login = usuario.Login,
        Contatos = usuario.Contatos
      });

      return Ok(dadosUsuarios);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
      var usuario = await _servico.ObterPorId(id);

      var dadosUsuario = new DTOs.Usuario
      {
        Id = usuario.Id,
        Login = usuario.Login,
        Contatos = usuario.Contatos
      };

      return Ok(dadosUsuario);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
      await _servico.Deletar(id);

      return NoContent();
    }

    [HttpPost("/usuarios/{usuarioId}/contatos")]
    public async Task<IActionResult> Post([FromBody] DTOs.Contato dadosContato, Guid usuarioId)
    {
      dadosContato.UsuarioId = usuarioId;
      var resposta = await _servicoContato.Salvar(dadosContato);
      dadosContato.Id = resposta.Resultado.Id;

      return Created($"/usuarios/{usuarioId}/contatos/{dadosContato.Id}", new { Id = dadosContato.Id });
    }

    [HttpPut("/usuarios/{usuarioId}/contatos/{contatoId}")]
    public async Task<IActionResult> Put([FromBody] DTOs.Contato dadosContato, Guid contatoId, Guid usuarioId)
    {
      dadosContato.Id = contatoId;
      dadosContato.UsuarioId = usuarioId;

      var resposta = await _servicoContato.Salvar(dadosContato);

      if (resposta.TemErro())
      {
        return StatusCode(resposta.Erro.StatusCode, new { Mensagem = resposta.Erro.Mensagem });
      }

      return NoContent();
    }
  }
}