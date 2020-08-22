using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestrutura.Migrations
{
  public partial class InserirDadosNaTabelaUsuarios2 : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql("INSERT INTO Usuarios VALUES ('5d8f714c-9e87-476e-a475-c420dbe807ff', 'usuario.xpto', 'AqVCHGAnDeTAkiTkkhKXYQ==.+1pI2/3+tEqM4PZ2VA++fas36DFKEbbmOHkDH0CeOy4=')");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
               name: "Usuarios");
    }
  }
}
