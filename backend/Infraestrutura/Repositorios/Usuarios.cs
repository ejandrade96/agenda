using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Infraestrutura.Repositorios
{
  public class Usuarios : Generico<Usuario>, Dominio.Repositorios.Usuarios
  {
    public Usuarios(Contextos.MyContext context) : base(context)
    {
    }

    public Task<List<Usuario>> Listar() => _dataset.Include(u => u.Contatos).ToListAsync();

    public async Task<Usuario> ObterPorId(Guid id)
    {
      var usuarios = await _dataset.Include(u => u.Contatos).ToListAsync();

      return usuarios.FirstOrDefault((u) => u.Id.Equals(id));
    }
  }
}