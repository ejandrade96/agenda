using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Infraestrutura.Repositorios
{
  public class Contatos : Generico<Contato>, Dominio.Repositorios.Contatos
  {
    public Contatos(Contextos.MyContext context) : base(context)
    {
    }

    public async Task<List<Contato>> Listar(string nome)
    {
      var contatos = await _dataset.Include(c => c.Usuario).ToListAsync();

      if (string.IsNullOrWhiteSpace(nome))
        return contatos;

      return contatos.Where((c) => c.Nome.Contains(nome)).ToList();
    }

    public async Task<Contato> ObterPorId(int id)
    {
      var contatos = await _dataset.Include(c => c.Usuario).ToListAsync();

      return contatos.FirstOrDefault((c) => c.Id.Equals(id));
    }
  }
}