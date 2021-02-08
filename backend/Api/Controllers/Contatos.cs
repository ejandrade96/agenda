using Microsoft.AspNetCore.Mvc;
using DTOs = Agenda.Dominio.DTOs;
using Agenda.Dominio.Servicos;
using System.Threading.Tasks;
using System;
using AutoMapper;
using System.Collections.Generic;

namespace Agenda.Api.Controllers
{
  [ApiController]
  [Route("contatos")]
  public class Contatos : ControllerBase
  {
    private readonly Contato _servico;

    private readonly Usuario _servicoUsuario;

    private readonly IMapper _mapper;

    public Contatos(Contato servico, Usuario servicoUsuario, IMapper mapper)
    {
      _servico = servico;
      _servicoUsuario = servicoUsuario;
      _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId([FromHeader(Name = "Authorization")] string token, int id)
    {
      var tokenEhValido = await _servicoUsuario.ValidarToken(token);

      if (tokenEhValido)
      {
        var resposta = await _servico.ObterPorId(id);

        if (resposta.TemErro())
        {
          return StatusCode(resposta.Erro.StatusCode, new { Mensagem = resposta.Erro.Mensagem });
        }

        var dadosContato = _mapper.Map<DTOs.Contato>(resposta.Resultado);

        return Ok(dadosContato);
      }

      return Unauthorized();
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string nome)
    {
      var contatos = await _servico.Listar(nome);

      var dadosContatos = _mapper.Map<List<DTOs.Contato>>(contatos);

      return Ok(dadosContatos);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromHeader(Name = "Authorization")] string token, int id)
    {
      var tokenEhValido = await _servicoUsuario.ValidarToken(token);

      if (tokenEhValido)
      {
        var resposta = await _servico.Deletar(id);

        if (resposta.TemErro())
        {
          return StatusCode(resposta.Erro.StatusCode, new { Mensagem = resposta.Erro.Mensagem });
        }

        return NoContent();
      }

      return Unauthorized();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromHeader(Name = "Authorization")] string token, [FromBody] DTOs.Contato dadosContato, int id)
    {
      var tokenEhValido = await _servicoUsuario.ValidarToken(token);

      if (tokenEhValido)
      {
        dadosContato.Id = id;
        var resposta = await _servico.Atualizar(dadosContato);

        if (resposta.TemErro())
        {
          return StatusCode(resposta.Erro.StatusCode, new { Mensagem = resposta.Erro.Mensagem });
        }

        return NoContent();
      }

      return Unauthorized();
    }
  }
}