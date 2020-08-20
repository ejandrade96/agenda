using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Infraestrutura.Repositorios
{
  public class Contatos : Generico<Contato>, Dominio.Repositorios.Contatos
  {
    private readonly Contextos.MyContext _context;

    public Contatos(Contextos.MyContext context) : base(context)
    {
      _context = context;
      _dataset = _context.Set<Contato>();
    }

    public async Task<List<Contato>> Listar(string nome)
    {
      var contatos = await _dataset.Include(c => c.Usuario).ToListAsync();

      if (string.IsNullOrWhiteSpace(nome))
        return contatos;

      return contatos.Where((c) => c.Nome.Contains(nome)).ToList();
    }

    public async Task<Contato> ObterPorId(Guid id)
    {
      var contatos = await _dataset.Include(c => c.Usuario).ToListAsync();

      return contatos.FirstOrDefault((c) => c.Id.Equals(id));
    }
  }
}