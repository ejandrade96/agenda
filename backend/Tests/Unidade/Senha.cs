using FluentAssertions;
using Xunit;
using Modelos = Agenda.Dominio.Modelos;

namespace Agenda.Tests.Unidade
{
  public class Senha
  {
    [Fact]
    public void Deve_Gerar_Hash_De_Senha()
    {
      var senha = new Modelos.Senha();

      var hash = senha.GerarHash("123456");

      hash.Should().NotBe("123456");
      hash.Should().NotBeEmpty();
    }

    [Fact]
    public void Deve_Retornar_True_Quando_Passar_Um_Hash_Que_Corresponde_A_Uma_Senha()
    {
      var senha = new Modelos.Senha();

      var ehValida = senha.Validar("pMt6WXGnAFrN1o13CIDRGw==.Bc8/fYrDFfyw576GfZnlEgnYIqZfszuKEErs2agPgRA=", "123456");

      ehValida.Should().BeTrue();
    }
  }
}