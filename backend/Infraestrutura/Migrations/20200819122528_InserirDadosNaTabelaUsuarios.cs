using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestrutura.Migrations
{
  public partial class InserirDadosNaTabelaUsuarios : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql("INSERT INTO Usuarios VALUES ('4337e5b1-138e-45c0-b6ac-3f1ebe3c133b', 'usuario.xpto', 'AqVCHGAnDeTAkiTkkhKXYQ==.+1pI2/3+tEqM4PZ2VA++fas36DFKEbbmOHkDH0CeOy4=')");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
               name: "Usuarios");
    }
  }
}