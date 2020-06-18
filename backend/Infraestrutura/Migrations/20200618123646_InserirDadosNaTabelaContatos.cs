using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestrutura.Migrations
{
  public partial class InserirDadosNaTabelaContatos : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql("INSERT INTO Contatos VALUES ('181fbe3a-d3ec-43a5-8791-9db5c3841cef', 'Contato 1', '11 47549874', '11 985471254', 'contato1@live.com')");
      migrationBuilder.Sql("INSERT INTO Contatos VALUES ('417a542d-00c4-4e8a-b6c0-a070dfb71823', 'Contato 2', '11 42365547', '11 987421452', 'contato2@live.com')");
      migrationBuilder.Sql("INSERT INTO Contatos VALUES ('93d6916b-74be-4acf-8e40-071f1fa98b28', 'Contato 3', '11 45216987', '11 998523647', 'contato3@live.com')");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "Contatos");
    }
  }
}