using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelos = Agenda.Dominio.Modelos;

namespace Agenda.Infraestrutura.Mapeamentos
{
  public class Contato : IEntityTypeConfiguration<Modelos.Contato>
  {
    public void Configure(EntityTypeBuilder<Modelos.Contato> builder)
    {
      builder.ToTable("Contatos");

      builder.HasKey(contato => contato.Id);
      builder.Property(contato => contato.Nome).IsRequired();
      builder.Property(contato => contato.Telefone);
      builder.Property(contato => contato.Celular);
      builder.Property(contato => contato.Email);
      builder.HasOne(contato => contato.Usuario);
    }
  }
}