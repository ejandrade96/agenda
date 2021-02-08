using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestrutura.Migrations
{
  public partial class InserirUsuarios : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql("INSERT INTO Usuarios VALUES ('usuario.xpto (teste)', 'AqVCHGAnDeTAkiTkkhKXYQ==.+1pI2/3+tEqM4PZ2VA++fas36DFKEbbmOHkDH0CeOy4=', '43f1818c-d5bf-46f1-8391-fe619d01653c', 'usuário nome')");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(name: "Usuarios");
    }
  }
}