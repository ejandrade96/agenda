using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Infraestrutura.Repositorios
{
  public class Contatos : Dominio.Repositorios.Contatos
  {
    private readonly Contextos.MyContext _context;

    public DbSet<Contato> _dataset;

    public Contatos(Contextos.MyContext context)
    {
      _context = context;
      _dataset = _context.Set<Contato>();
    }

    public async Task<Guid> Salvar(Contato contato)
    {

      if (contato.Id == Guid.Empty)
        _dataset.Add(contato);

      else
        _context.Entry(await ObterPorId(contato.Id)).CurrentValues.SetValues(contato);

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException ex)
      {
        ex.Entries.SingleOrDefault().Reload();

        await _context.SaveChangesAsync();
      }

      return contato.Id;
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

    public async Task Deletar(Guid id)
    {
      var contato = await ObterPorId(id);

      _dataset.Remove(contato);

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException ex)
      {
        ex.Entries.SingleOrDefault().Reload();

        await _context.SaveChangesAsync();
      }
    }
  }
}