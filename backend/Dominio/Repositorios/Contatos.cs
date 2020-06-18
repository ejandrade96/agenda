using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agenda.Dominio.Repositorios
{
  public interface Contatos
  {
    Task<Guid> Salvar(Modelos.Contato contato);

    Task<Modelos.Contato> ObterPorId(Guid id);

    Task<List<Modelos.Contato>> Listar(string nome);

    Task Deletar(Guid id);
  }
}