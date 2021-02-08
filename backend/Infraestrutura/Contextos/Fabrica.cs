using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Agenda.Infraestrutura.Contextos
{
  public class Fabrica : IDesignTimeDbContextFactory<MyContext>
  {
    public MyContext CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
                                         .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Api/"))
                                         .AddJsonFile("appsettings.json")
                                         .Build();

      var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
      var connectionString = configuration.GetConnectionString("AgendaDBContext");
      optionsBuilder.UseSqlServer(connectionString);

      return new MyContext(optionsBuilder.Options);
    }
  }
}