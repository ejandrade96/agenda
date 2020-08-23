using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agenda.Dominio.Repositorios
{
  public interface Usuarios : Generico<Modelos.Usuario>
  {
    Task<List<Modelos.Usuario>> Listar();

    Task<Modelos.Usuario> ObterPorId(Guid id);

    Task<Modelos.Usuario> ObterPorLogin(string login);
  }
}