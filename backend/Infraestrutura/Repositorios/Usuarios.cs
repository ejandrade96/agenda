using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;
using Modelos = Agenda.Dominio.Modelos;

namespace Agenda.Infraestrutura.Repositorios
{
  public class Usuarios : Generico<Usuario>, Dominio.Repositorios.Usuarios
  {
    private readonly Contextos.MyContext _context;

    public Usuarios(Contextos.MyContext context) : base(context)
    {
      _context = context;
      _dataset = _context.Set<Modelos.Usuario>();
    }

    public Task<List<Usuario>> Listar() => _dataset.Include(u => u.Contatos).ToListAsync();

    public async Task<Usuario> ObterPorId(Guid id)
    {
      var usuarios = await _dataset.Include(u => u.Contatos).ToListAsync();

      return usuarios.FirstOrDefault((u) => u.Id.Equals(id));
    }
  }
}