using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestrutura.Migrations
{
  public partial class InserirDadosNaTabelaContatos : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql("INSERT INTO Contatos VALUES ('181fbe3a-d3ec-43a5-8791-9db5c3841cef', 'Contato 1', '11 47549874', '11 985471254', 'contato1@live.com', '4337e5b1-138e-45c0-b6ac-3f1ebe3c133b')");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
               name: "Contatos");
    }
  }
}