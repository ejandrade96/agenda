using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestrutura.Migrations
{
  public partial class InserirContatos : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql("INSERT INTO Contatos VALUES ('Contato 1', '11 49782534', '11 958742136', 'contato1@live.com', 1)");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(name: "Contatos");
    }
  }
}