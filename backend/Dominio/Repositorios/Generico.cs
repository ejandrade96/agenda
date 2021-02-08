using System;
using System.Threading.Tasks;
using Agenda.Dominio.Modelos;

namespace Agenda.Dominio.Repositorios
{
  public interface Generico<T> where T : ModeloBase
  {
    Task<int> Salvar(T entidade);

    Task Deletar(int id);
  }
}