using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Agenda.Infraestrutura.Contextos
{
  public class Fabrica : IDesignTimeDbContextFactory<MyContext>
  {
    public MyContext CreateDbContext(string[] args)
    {
      var conexao = "Data Source=/home/elton/Documentos/agenda/backend/Api/agenda.db";
      var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
      optionsBuilder.UseSqlite(conexao);

      return new MyContext(optionsBuilder.Options);
    }
  }
}