using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agenda.Dominio.Repositorios
{
  public interface Usuarios
  {
    Task<Guid> Salvar(Modelos.Usuario usuario);

    Task<List<Modelos.Usuario>> Listar();

    Task<Modelos.Usuario> ObterPorId(Guid id);
  }
}