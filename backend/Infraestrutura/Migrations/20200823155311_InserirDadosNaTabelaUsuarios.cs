using Microsoft.EntityFrameworkCore.Migrations;

namespace Agenda.Infraestrutura.Migrations
{
  public partial class InserirDadosNaTabelaUsuarios : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql("INSERT INTO Usuarios VALUES ('4337e5b1-138e-45c0-b6ac-3f1ebe3c133b', 'usuario.xpto (teste)', 'AqVCHGAnDeTAkiTkkhKXYQ==.+1pI2/3+tEqM4PZ2VA++fas36DFKEbbmOHkDH0CeOy4=', '43f1818c-d5bf-46f1-8391-fe619d01653c', 'usuário nome')");
      migrationBuilder.Sql("INSERT INTO Usuarios VALUES ('5d8f714c-9e87-476e-a475-c420dbe807ff', 'usuario.xpto (teste2)', 'J4TF7VBJLElQdfkyGUxeeA==.bu6n3Mhd92VwRnshULj6+St4H8ulnYF49UZ4vyQDpqM=', '8bd715c8-1d89-43ae-af17-c89a56c9cfa7', 'usuário nome2')");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
               name: "Usuarios");
    }
  }
}