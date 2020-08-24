using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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

    private readonly IMapper _mapper;

    public Usuarios(Dominio.Servicos.Usuario servico, Dominio.Servicos.Contato servicoContato, IMapper mapper)
    {
      _servico = servico;
      _servicoContato = servicoContato;
      _mapper = mapper;
    }

    [HttpPost]
    [Consumes("application/x-www-form-urlencoded")]
    public async Task<IActionResult> Post([FromForm] IFormCollection usuario)
    {
      var dadosUsuario = new DTOs.Usuario()
      {
        Login = usuario["login"],
        Senha = usuario["senha"],
        Nome = usuario["nome"]
      };

      var resposta = await _servico.Salvar(dadosUsuario);

      if (resposta.TemErro())
      {
        return StatusCode(resposta.Erro.StatusCode, new { Mensagem = resposta.Erro.Mensagem });
      }

      var id = resposta.Resultado.Id;

      return Created($"/usuarios/{id}", new { Id = id });
    }

    [HttpPost]
    [Consumes("application/x-www-form-urlencoded")]
    [Route("/login")]
    public async Task<IActionResult> Login([FromForm] IFormCollection usuario)
    {
      var dadosUsuario = new DTOs.Usuario()
      {
        Login = usuario["login"],
        Senha = usuario["senha"]
      };

      var resposta = await _servico.Autenticar(dadosUsuario);

      if (resposta.TemErro())
      {
        return StatusCode(resposta.Erro.StatusCode, new { Mensagem = resposta.Erro.Mensagem });
      }

      return Ok(resposta.Resultado);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var usuarios = await _servico.Listar();

      var dadosUsuarios = _mapper.Map<List<DTOs.Usuario>>(usuarios);

      return Ok(dadosUsuarios);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
      var resposta = await _servico.ObterPorId(id);

      if (resposta.TemErro())
      {
        return StatusCode(resposta.Erro.StatusCode, new { Mensagem = resposta.Erro.Mensagem });
      }

      var dadosUsuario = _mapper.Map<DTOs.Usuario>(resposta.Resultado);

      return Ok(dadosUsuario);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
      var resposta = await _servico.Deletar(id);

      if (resposta.TemErro())
      {
        return StatusCode(resposta.Erro.StatusCode, new { Mensagem = resposta.Erro.Mensagem });
      }

      return NoContent();
    }

    [HttpPost("/usuarios/{usuarioId}/contatos")]
    public async Task<IActionResult> Post([FromBody] DTOs.Contato dadosContato, Guid usuarioId)
    {
      dadosContato.UsuarioId = usuarioId;
      var resposta = await _servicoContato.Salvar(dadosContato);

      if (resposta.TemErro())
      {
        return StatusCode(resposta.Erro.StatusCode, new { Mensagem = resposta.Erro.Mensagem });
      }

      var id = resposta.Resultado.Id;

      return Created($"/usuarios/{usuarioId}/contatos/{id}", new { Id = id });
    }

    [HttpGet("/usuarios/{usuarioId}/contatos")]
    public async Task<IActionResult> GetByUserId(Guid usuarioId)
    {
      var resposta = await _servicoContato.ListarPorUsuarioId(usuarioId);

      if (resposta.TemErro())
      {
        return StatusCode(resposta.Erro.StatusCode, new { Mensagem = resposta.Erro.Mensagem });
      }

      var contatos = _mapper.Map<List<DTOs.Contato>>(resposta.Resultado);

      return Ok(contatos);
    }
  }
}