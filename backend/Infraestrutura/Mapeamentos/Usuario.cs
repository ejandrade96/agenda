using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelos = Agenda.Dominio.Modelos;

namespace Agenda.Infraestrutura.Mapeamentos
{
  public class Usuario : IEntityTypeConfiguration<Modelos.Usuario>
  {
    public void Configure(EntityTypeBuilder<Modelos.Usuario> builder)
    {
      builder.ToTable("Usuarios");

      builder.HasKey(usuario => usuario.Id);
      builder.Property(usuario => usuario.Login).IsRequired();
      builder.Property(usuario => usuario.Senha).IsRequired();
      builder.Property(usuario => usuario.Nome).IsRequired();
      builder.Property(usuario => usuario.Token);
      builder.HasMany(usuario => usuario.Contatos);
    }
  }
}