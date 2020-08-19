using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    public async Task Deletar(Guid id) => await _usuarios.Deletar(id);

    public Task<List<Modelos.Usuario>> Listar() => _usuarios.Listar();

    public Task<Modelos.Usuario> ObterPorId(Guid id) => _usuarios.ObterPorId(id);

    public async Task<Guid> Salvar(DTOs.Usuario dadosUsuario)
    {
      var senha = new Modelos.Senha();

      var usuario = new Modelos.Usuario(dadosUsuario.Login, senha.GerarHash(dadosUsuario.Senha));

      var id = await _usuarios.Salvar(usuario);

      return id;
    }
  }
}