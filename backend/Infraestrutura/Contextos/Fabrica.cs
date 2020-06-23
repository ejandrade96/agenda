using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Agenda.Infraestrutura.Contextos
{
  public class Fabrica : IDesignTimeDbContextFactory<MyContext>
  {
    public MyContext CreateDbContext(string[] args)
    {
      var conexao = "Data Source=" + Path.Combine(Directory.GetCurrentDirectory(), "../Api/agenda.db");
      var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
      optionsBuilder.UseSqlite(conexao);

      return new MyContext(optionsBuilder.Options);
    }
  }
}