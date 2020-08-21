using System;
using System.Threading.Tasks;
using DTOs = Agenda.Dominio.DTOs;
using Modelos = Agenda.Dominio.Modelos;
using Fabricas = Agenda.Dominio.Fabricas;
using Agenda.Dominio.Repositorios;
using Agenda.Infraestrutura.Erros;
using System.Collections.Generic;

namespace Agenda.Servicos
{
  public class Contato : Dominio.Servicos.Contato
  {
    private readonly Contatos _contatos;

    private readonly Usuarios _usuarios;

    public Contato(Contatos contatos, Usuarios usuarios)
    {
      _contatos = contatos;
      _usuarios = usuarios;
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

      var usuario = await _usuarios.ObterPorId(dadosContato.UsuarioId);

      if (usuario == null)
        resposta.Erro = new ErroObjetoNaoEncontrado("Usuário");

      else
      {
        var contato = new Fabricas.Contato().Nome(dadosContato.Nome)
                                            .Celular(dadosContato.Celular)
                                            .Telefone(dadosContato.Telefone)
                                            .Email(dadosContato.Email)
                                            .Usuario(usuario)
                                            .Id(dadosContato.Id)
                                            .Criar();

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

    public async Task<Dominio.Servicos.Resposta<Modelos.Contato>> Atualizar(DTOs.Contato dadosContato)
    {
      var resposta = new Resposta<Modelos.Contato>();

      var contatoEncontrado = await _contatos.ObterPorId(dadosContato.Id);

      if (contatoEncontrado == null)
        resposta.Erro = new ErroObjetoNaoEncontrado("Contato");

      else
      {
        var contato = new Fabricas.Contato().Nome(dadosContato.Nome)
                                            .Celular(dadosContato.Celular)
                                            .Telefone(dadosContato.Telefone)
                                            .Email(dadosContato.Email)
                                            .Usuario(contatoEncontrado.Usuario)
                                            .Id(dadosContato.Id)
                                            .Criar();

        contato.Id = await _contatos.Salvar(contato);
        resposta.Resultado = contato;
      }

      return resposta;
    }

    private async Task<bool> ContatoNaoEncontrado(Guid id) => await _contatos.ObterPorId(id) == null;
  }
}