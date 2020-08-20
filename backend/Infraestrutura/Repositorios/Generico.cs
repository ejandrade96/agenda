using System;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Infraestrutura.Repositorios
{
  public abstract class Generico<T> : Dominio.Repositorios.Generico<T> where T : ModeloBase
  {
    private readonly Contextos.MyContext _context;

    protected DbSet<T> _dataset;

    public Generico(Contextos.MyContext context)
    {
      _context = context;
      _dataset = _context.Set<T>();
    }

    public async Task Deletar(Guid id)
    {
      var entidade = _dataset.ToList().FirstOrDefault((e) => e.Id.Equals(id));

      _dataset.Remove(entidade);

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

    public async Task<Guid> Salvar(T entidade)
    {
      if (entidade.Id == Guid.Empty)
        _dataset.Add(entidade);

      else
      {
        var entidadeEncontrada = _dataset.ToList().FirstOrDefault((e) => e.Id.Equals(entidade.Id));
        _context.Entry(entidadeEncontrada).CurrentValues.SetValues(entidade);
      }

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException ex)
      {
        ex.Entries.SingleOrDefault().Reload();

        await _context.SaveChangesAsync();
      }

      return entidade.Id;
    }
  }
}