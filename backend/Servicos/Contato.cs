using System;
using System.Threading.Tasks;
using DTOs = Agenda.Dominio.DTOs;
using Modelos = Agenda.Dominio.Modelos;
using Agenda.Dominio.Repositorios;
using Agenda.Infraestrutura.Erros;
using System.Collections.Generic;

namespace Agenda.Servicos
{
  public class Contato : Dominio.Servicos.Contato
  {
    private readonly Contatos _contatos;

    public Contato(Contatos contatos)
    {
      _contatos = contatos;
    }

    public async Task<List<Modelos.Contato>> Listar(string nome) => await _contatos.Listar(nome);

    public async Task<Dominio.Servicos.Resposta<Modelos.Contato>> ObterPorId(Guid id)
    {
      var resposta = new Resposta<Modelos.Contato>();

      var contato = await _contatos.ObterPorId(id);

      if (contato == null)
        resposta.Erro = new ErroObjetoNaoEncontrado("Contato");

      else
        resposta.Resultado = contato;

      return resposta;
    }

    public async Task<Dominio.Servicos.Resposta<Modelos.Contato>> Salvar(DTOs.Contato dadosContato)
    {
      var resposta = new Resposta<Modelos.Contato>();

      if (TemId(dadosContato) && await ContatoNaoEncontrado(dadosContato.Id))
        resposta.Erro = new ErroObjetoNaoEncontrado("Contato");

      else
      {
        var contato = new Modelos.Contato(dadosContato.Nome, dadosContato.Celular, dadosContato.Telefone, dadosContato.Email)
        {
          Id = dadosContato.Id
        };

        contato.Id = await _contatos.Salvar(contato);
        resposta.Resultado = contato;
      }

      return resposta;
    }

    public async Task<Dominio.Servicos.Resposta<Modelos.Contato>> Deletar(Guid id)
    {
      var resposta = new Resposta<Modelos.Contato>();

      if (await ContatoNaoEncontrado(id))
        resposta.Erro = new ErroObjetoNaoEncontrado("Contato");

      else
        await _contatos.Deletar(id);

      return resposta;
    }

    private bool TemId(DTOs.Contato dadosContato) => dadosContato.Id != Guid.Empty;

    private async Task<bool> ContatoNaoEncontrado(Guid id) => await _contatos.ObterPorId(id) == null;
  }
}