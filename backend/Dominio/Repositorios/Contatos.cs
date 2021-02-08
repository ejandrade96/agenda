using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agenda.Dominio.Repositorios
{
  public interface Contatos : Generico<Modelos.Contato>
  {
    Task<Modelos.Contato> ObterPorId(int id);

    Task<List<Modelos.Contato>> Listar(string nome);
  }
}