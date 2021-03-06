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

    public async Task<Usuario> ObterPorId(int id)
    {
      var usuarios = await _dataset.Include(u => u.Contatos).ToListAsync();

      // var usuario = await _dataset.FirstOrDefaultAsync(u => u.Id.Equals(id));

      // return usuario;

      return usuarios.FirstOrDefault((u) => u.Id.Equals(id));
    }

    public Task<Usuario> ObterPorLogin(string login) => Task.FromResult(_dataset.FirstOrDefault((u) => u.Login.Equals(login)));

    public Task<bool> ValidarToken(string token)
    {
      var usuario = _dataset.FirstOrDefault((u) => u.Token.Equals(token.Replace("Bearer ", "")));

      return Task.FromResult(usuario != null);
    }
  }
}