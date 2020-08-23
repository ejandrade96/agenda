using System;
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
      dadosUsuario.Add("nome", "usuário nome");

      var retorno = await _api.PostAsync("/usuarios", CodificarUrl(dadosUsuario));
      var usuarioEmJson = await retorno.Content.ReadAsStringAsync();
      var usuario = Converter<Dictionary<string, string>>(usuarioEmJson);

      retorno.StatusCode.Should().Be(HttpStatusCode.Created);
      retorno.Headers.Location.ToString().Contains("/usuarios/").Should().BeTrue();
      usuario["id"].Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Cadastrar_Um_Usuario_Com_Campo_Login_Em_Branco()
    {
      var dadosUsuario = new Dictionary<string, string>();
      dadosUsuario.Add("login", "    ");
      dadosUsuario.Add("senha", "123456");
      dadosUsuario.Add("nome", "usuário nome");

      var retorno = await _api.PostAsync("/usuarios", CodificarUrl(dadosUsuario));
      var mensagem = await retorno.Content.ReadAsStringAsync();

      retorno.StatusCode.Should().Be(HttpStatusCode.BadRequest);
      retorno.StatusCode.Should().Be(400);
      mensagem.Should().Contain("Favor preencher o campo login.");
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Cadastrar_Um_Usuario_Com_Campo_Nome_Em_Branco()
    {
      var dadosUsuario = new Dictionary<string, string>();
      dadosUsuario.Add("login", "usuario.xpto");
      dadosUsuario.Add("senha", "123456");
      dadosUsuario.Add("nome", "   ");

      var retorno = await _api.PostAsync("/usuarios", CodificarUrl(dadosUsuario));
      var mensagem = await retorno.Content.ReadAsStringAsync();

      retorno.StatusCode.Should().Be(HttpStatusCode.BadRequest);
      retorno.StatusCode.Should().Be(400);
      mensagem.Should().Contain("Favor preencher o campo nome.");
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Cadastrar_Um_Usuario_Com_Campo_Senha_Em_Branco()
    {
      var dadosUsuario = new Dictionary<string, string>();
      dadosUsuario.Add("login", "usuario.xpto");
      dadosUsuario.Add("senha", "  ");
      dadosUsuario.Add("nome", "usuário nome");

      var retorno = await _api.PostAsync("/usuarios", CodificarUrl(dadosUsuario));
      var mensagem = await retorno.Content.ReadAsStringAsync();

      retorno.StatusCode.Should().Be(HttpStatusCode.BadRequest);
      retorno.StatusCode.Should().Be(400);
      mensagem.Should().Contain("Favor preencher o campo senha.");
    }


    [Fact]
    public async Task Deve_Autenticar_Um_Usuario()
    {
      var dadosUsuario = new Dictionary<string, string>();
      dadosUsuario.Add("login", "usuario.xpto (teste)");
      dadosUsuario.Add("senha", "123456789");

      var retorno = await _api.PostAsync("/login", CodificarUrl(dadosUsuario));
      var usuarioEmJson = await retorno.Content.ReadAsStringAsync();
      var usuario = Converter<Dictionary<string, string>>(usuarioEmJson);

      retorno.StatusCode.Should().Be(HttpStatusCode.OK);
      usuario["id"].Should().NotBe(Guid.Empty.ToString());
      usuario["login"].Should().Be("usuario.xpto (teste)");
      usuario["nome"].Should().Be("usuário nome");
      usuario.ContainsKey("token");
      usuario["token"].Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Autenticar_Um_Usuario_Com_Senha_Invalida()
    {
      var dadosUsuario = new Dictionary<string, string>();
      dadosUsuario.Add("login", "usuario.xpto (teste)");
      dadosUsuario.Add("senha", "754sdaw");

      var retorno = await _api.PostAsync("/login", CodificarUrl(dadosUsuario));
      var erroEmJson = await retorno.Content.ReadAsStringAsync();
      var erro = Converter<Dictionary<string, string>>(erroEmJson);

      retorno.StatusCode.Should().Be(HttpStatusCode.BadRequest);
      retorno.StatusCode.Should().Be(400);
      erro["mensagem"].Should().Be("Senha inválido(a)!");
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Autenticar_Um_Usuario_Inexistente()
    {
      var dadosUsuario = new Dictionary<string, string>();
      dadosUsuario.Add("login", "usuario");
      dadosUsuario.Add("senha", "754sdaw");

      var retorno = await _api.PostAsync("/login", CodificarUrl(dadosUsuario));
      var erroEmJson = await retorno.Content.ReadAsStringAsync();
      var erro = Converter<Dictionary<string, string>>(erroEmJson);

      retorno.StatusCode.Should().Be(HttpStatusCode.BadRequest);
      retorno.StatusCode.Should().Be(400);
      erro["mensagem"].Should().Be("Login inválido(a)!");
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
        usuario.Should().ContainKey("nome");
        usuario.Should().ContainKey("contatos");
        usuario["id"].ToString().Should().NotBeNullOrWhiteSpace();
        usuario["login"].ToString().Should().NotBeNullOrWhiteSpace();
        usuario["nome"].ToString().Should().NotBeNullOrWhiteSpace();
      });
    }

    [Fact]
    public async Task Deve_Retornar_Um_Usuario_Por_Id()
    {
      var retorno = await _api.GetAsync("/usuarios/4337E5B1-138E-45C0-B6AC-3F1EBE3C133B");
      var usuarioEmJson = await retorno.Content.ReadAsStringAsync();
      var usuario = Converter<Dictionary<string, object>>(usuarioEmJson);

      retorno.StatusCode.Should().Be(HttpStatusCode.OK);
      usuario["login"].Should().Be("usuario.xpto (teste)");
      usuario["nome"].Should().Be("usuário nome");
      usuario.ContainsKey("token");
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Buscar_Um_Usuario_Inexistente()
    {
      var id = Guid.NewGuid();
      var retorno = await _api.GetAsync($"/usuarios/{id}");
      var erroEmJson = await retorno.Content.ReadAsStringAsync();
      var erro = Converter<Dictionary<string, string>>(erroEmJson);

      retorno.StatusCode.Should().Be(HttpStatusCode.NotFound);
      retorno.StatusCode.Should().Be(404);
      erro["mensagem"].Should().Be("Usuário não encontrado(a)!");
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Deletar_Um_Usuario_Inexistente()
    {
      var id = Guid.NewGuid();
      var retorno = await _api.DeleteAsync($"/usuarios/{id}");
      var erroEmJson = await retorno.Content.ReadAsStringAsync();
      var erro = Converter<Dictionary<string, string>>(erroEmJson);

      retorno.StatusCode.Should().Be(HttpStatusCode.NotFound);
      retorno.StatusCode.Should().Be(404);
      erro["mensagem"].Should().Be("Usuário não encontrado(a)!");
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Deletar_Um_Usuario_Com_Contatos_Vinculados_A_Ele()
    {
      var retorno = await _api.DeleteAsync($"/usuarios/4337e5b1-138e-45c0-b6ac-3f1ebe3c133b");
      var erroEmJson = await retorno.Content.ReadAsStringAsync();
      var erro = Converter<Dictionary<string, string>>(erroEmJson);

      retorno.StatusCode.Should().Be(HttpStatusCode.BadRequest);
      retorno.StatusCode.Should().Be(400);
      erro["mensagem"].Should().Be("Erro! Este usuário possui contatos vinculados.");
    }
  }
}