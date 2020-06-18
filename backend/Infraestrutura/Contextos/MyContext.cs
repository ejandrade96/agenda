using Microsoft.EntityFrameworkCore;
using Modelos = Agenda.Dominio.Modelos;

namespace Agenda.Infraestrutura.Contextos
{
  public class MyContext : DbContext
  {
    public MyContext(DbContextOptions<MyContext> options) : base(options)
    {
    }

    public DbSet<Modelos.Contato> Contatos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<Modelos.Contato>(new Mapeamentos.Contato().Configure);
    }
  }
}