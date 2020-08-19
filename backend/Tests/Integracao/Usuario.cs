using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Agenda.Tests.Integracao
{
  public class Usuario : IntegracaoBase
  {
    [Fact]
    public async Task Deve_Cadastrar_Um_Usuario_Quando_Enviar_Dados_Certos()
    {
      var dadosUsuario = new Dictionary<string, string>();
      dadosUsuario.Add("login", "usuario.xpto");
      dadosUsuario.Add("senha", "123456");

      var retorno = await _api.PostAsync("/usuarios", CodificarUrl(dadosUsuario));
      var usuarioEmJson = await retorno.Content.ReadAsStringAsync();
      var usuario = Converter<Dictionary<string, string>>(usuarioEmJson);

      retorno.StatusCode.Should().Be(HttpStatusCode.Created);
      retorno.Headers.Location.ToString().Contains("/usuarios/").Should().BeTrue();
      usuario["id"].Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Deve_Retornar_Todos_Os_Usuarios_Cadastrados()
    {
      var retorno = await _api.GetAsync("/usuarios");
      var usuariosEmJson = await retorno.Content.ReadAsStringAsync();
      var usuarios = Converter<List<Dictionary<string, object>>>(usuariosEmJson);

      retorno.StatusCode.Should().Be(HttpStatusCode.OK);
      usuarios.Should().HaveCountGreaterThan(0);
      usuarios.ForEach((usuario) =>
      {
        usuario.Should().ContainKey("id");
        usuario.Should().ContainKey("login");
        usuario.Should().ContainKey("senha");
        usuario.Should().ContainKey("contatos");
        usuario["id"].ToString().Should().NotBeNullOrWhiteSpace();
        usuario["login"].ToString().Should().NotBeNullOrWhiteSpace();
        usuario["senha"].ToString().Should().NotBeNullOrWhiteSpace();
        usuario["contatos"].ToString().Should().NotBeNullOrWhiteSpace();
      });
    }

    [Fact]
    public async Task Deve_Retornar_Um_Usuario_Por_Id()
    {
      var retorno = await _api.GetAsync("/usuarios/4337E5B1-138E-45C0-B6AC-3F1EBE3C133B");
      var usuarioEmJson = await retorno.Content.ReadAsStringAsync();
      var usuario = Converter<Dictionary<string, object>>(usuarioEmJson);

      retorno.StatusCode.Should().Be(HttpStatusCode.OK);
      usuario["login"].Should().Be("usuario.xpto");
      usuario["senha"].ToString().Should().NotBeNullOrWhiteSpace();
      usuario["contatos"].ToString().Should().NotBeNullOrWhiteSpace();
    }
  }
}