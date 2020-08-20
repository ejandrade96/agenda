using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agenda.Infraestrutura.Erros;
using DTOs = Agenda.Dominio.DTOs;
using Modelos = Agenda.Dominio.Modelos;

namespace Agenda.Servicos
{
  public class Usuario : Dominio.Servicos.Usuario
  {
    private readonly Dominio.Repositorios.Usuarios _usuarios;

    public Usuario(Dominio.Repositorios.Usuarios usuarios)
    {
      _usuarios = usuarios;
    }

    public async Task<Dominio.Servicos.Resposta<Modelos.Usuario>> Deletar(Guid id)
    {
      var resposta = new Resposta<Modelos.Usuario>();

      if (await UsuarioNaoEncontrado(id))
        resposta.Erro = new ErroObjetoNaoEncontrado("Usuário");

      else
        await _usuarios.Deletar(id);

      return resposta;
    }

    public Task<List<Modelos.Usuario>> Listar() => _usuarios.Listar();

    public async Task<Dominio.Servicos.Resposta<Modelos.Usuario>> ObterPorId(Guid id)
    {
      var resposta = new Resposta<Modelos.Usuario>();

      var usuario = await _usuarios.ObterPorId(id);

      if (usuario == null)
        resposta.Erro = new ErroObjetoNaoEncontrado("Usuário");

      else
        resposta.Resultado = usuario;

      return resposta;
    }

    public async Task<Guid> Salvar(DTOs.Usuario dadosUsuario)
    {
      var senha = new Modelos.Senha();

      var usuario = new Modelos.Usuario(dadosUsuario.Login, senha.GerarHash(dadosUsuario.Senha));

      var id = await _usuarios.Salvar(usuario);

      return id;
    }

    private async Task<bool> UsuarioNaoEncontrado(Guid id) => await _usuarios.ObterPorId(id) == null;
  }
}