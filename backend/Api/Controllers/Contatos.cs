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

    private readonly IMapper _mapper;

    public Contatos(Contato servico, IMapper mapper)
    {
      _servico = servico;
      _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] DTOs.Contato dadosContato)
    {
      var resposta = await _servico.Salvar(dadosContato);
      dadosContato.Id = resposta.Resultado.Id;

      return Created($"/contatos/{resposta.Resultado.Id}", dadosContato);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
      var resposta = await _servico.ObterPorId(id);

      if (resposta.TemErro())
      {
        return StatusCode(resposta.Erro.StatusCode, new { Mensagem = resposta.Erro.Mensagem });
      }

      var dadosContato = _mapper.Map<DTOs.Contato>(resposta.Resultado);

      return Ok(dadosContato);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string nome)
    {
      var contatos = await _servico.Listar(nome);

      var dadosContatos = _mapper.Map<List<DTOs.Contato>>(contatos);

      return Ok(dadosContatos);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] DTOs.Contato dadosContato)
    {
      dadosContato.Id = id;

      var resposta = await _servico.Salvar(dadosContato);

      if (resposta.TemErro())
      {
        return StatusCode(resposta.Erro.StatusCode, new { Mensagem = resposta.Erro.Mensagem });
      }

      return NoContent();
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
  }
}