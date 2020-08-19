using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agenda.Dominio.Servicos
{
  public interface Usuario
  {
    Task<Guid> Salvar(DTOs.Usuario dadosUsuario);

    Task<List<Modelos.Usuario>> Listar();

    Task<Modelos.Usuario> ObterPorId(Guid id);

    Task Deletar(Guid id);
  }
}