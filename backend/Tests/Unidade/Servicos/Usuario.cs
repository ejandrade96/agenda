using Agenda.Dominio.Repositorios;
using Moq;
using Xunit;
using Modelos = Agenda.Dominio.Modelos;
using DTOs = Agenda.Dominio.DTOs;
using System.Threading.Tasks;
using System;
using FluentAssertions;
using Agenda.Infraestrutura.Erros;

namespace Agenda.Tests.Unidade.Servicos
{
  public class Usuario
  {
    private readonly Dominio.Servicos.Usuario _servico;

    private readonly Mock<Usuarios> _usuarios;

    public Usuario()
    {
      _usuarios = new Mock<Usuarios>();
      _servico = new Agenda.Servicos.Usuario(_usuarios.Object);
    }

    [Fact]
    public async Task Deve_Deletar_Um_Usuario()
    {
      var usuario = new Modelos.Usuario("", "", "");

      _usuarios.Setup(repositorio => repositorio.ObterPorId(It.IsAny<Guid>()))
               .Returns(Task.FromResult(usuario));

      var resposta = await _servico.Deletar(Guid.Parse("149944ca-6d46-4357-bcdc-bdeebbe66377"));

      resposta.TemErro().Should().BeFalse();
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Deletar_Um_Usuario_Inexistente()
    {
      var resposta = await _servico.Deletar(Guid.NewGuid());

      resposta.Erro.Mensagem.Should().Be("Usuário não encontrado(a)!");
      resposta.Erro.StatusCode.Should().Be(404);
      resposta.Erro.GetType().Should().Be(typeof(ErroObjetoNaoEncontrado));
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Deletar_Um_Usuario_Com_Contatos_Vinculados_A_Ele()
    {
      var usuario = new Modelos.Usuario("xpto", "123", "usuario nome");

      var contato = new Modelos.Contato("Contato", "11 985478521", "11 45873214", "contato@live.com", usuario);
      usuario.AdicionarContato(contato);

      _usuarios.Setup(repositorio => repositorio.ObterPorId(It.IsAny<Guid>()))
               .Returns(Task.FromResult(usuario));

      var resposta = await _servico.Deletar(Guid.Parse("149944ca-6d46-4357-bcdc-bdeebbe66377"));

      resposta.Erro.Mensagem.Should().Be("Erro! Este usuário possui contatos vinculados.");
      resposta.Erro.StatusCode.Should().Be(400);
      resposta.Erro.GetType().Should().Be(typeof(ErroObjetoPossuiObjetosVinculados));
    }

    [Fact]
    public async Task Deve_Retornar_Um_Usuario_Por_Id()
    {
      var usuario = new Modelos.Usuario("usuario.xpto", "123456", "usuário nome");

      _usuarios.Setup(repositorio => repositorio.ObterPorId(It.IsAny<Guid>()))
               .Returns(Task.FromResult(usuario));

      var resposta = await _servico.ObterPorId(Guid.Parse("149944ca-6d46-4357-bcdc-bdeebbe66377"));

      var usuarioEncontrado = resposta.Resultado;

      usuarioEncontrado.Login.Should().Be("usuario.xpto");
      usuarioEncontrado.Nome.Should().Be("usuário nome");
      usuarioEncontrado.Senha.Should().Be("123456");
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Buscar_Um_Usuario_Inexistente()
    {
      var resposta = await _servico.ObterPorId(Guid.NewGuid());

      resposta.Erro.Mensagem.Should().Be("Usuário não encontrado(a)!");
      resposta.Erro.StatusCode.Should().Be(404);
      resposta.Erro.GetType().Should().Be(typeof(ErroObjetoNaoEncontrado));
    }

    [Fact]
    public async Task Deve_Autenticar_Um_Usuario()
    {
      var usuario = new Modelos.Usuario(
        "usuario login",
        "pMt6WXGnAFrN1o13CIDRGw==.Bc8/fYrDFfyw576GfZnlEgnYIqZfszuKEErs2agPgRA=",
        "usuario nome")
      {
        Id = Guid.NewGuid(),
      };
      usuario.AdicionarToken(Guid.NewGuid().ToString());

      var dadosUsuario = new DTOs.Usuario
      {
        Login = "usuario login",
        Senha = "123456"
      };

      _usuarios.Setup(repositorio => repositorio.ObterPorLogin(It.IsAny<string>()))
                   .Returns(Task.FromResult(usuario));

      var resposta = await _servico.Autenticar(dadosUsuario);

      var usuarioEncontrado = resposta.Resultado;

      usuarioEncontrado.Id.Should().NotBeEmpty();
      usuarioEncontrado.Login.Should().Be("usuario login");
      usuarioEncontrado.Token.Should().NotBeNullOrWhiteSpace();
      usuarioEncontrado.Nome.Should().Be("usuario nome");
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Autenticar_Um_Usuario_Inexistente()
    {
      var dadosUsuario = new DTOs.Usuario
      {
        Login = "usuario login",
        Senha = "123456"
      };

      var resposta = await _servico.Autenticar(dadosUsuario);

      resposta.Erro.Mensagem.Should().Be("Login inválido(a)!");
      resposta.Erro.StatusCode.Should().Be(400);
      resposta.Erro.GetType().Should().Be(typeof(ErroAtributoInvalido));
    }


    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Autenticar_Um_Usuario_Com_Senha_Invalida()
    {
      var usuario = new Modelos.Usuario(
        "usuario login",
        "bHBlUVj1rFG48+Bd+4+yGA==.7KQGnYFMukLhkfSqhbfGhtqtqELUntz4AbGhrrqspLs=",
        "usuario nome");

      var dadosUsuario = new DTOs.Usuario
      {
        Login = "usuario login",
        Senha = "123456"
      };

      _usuarios.Setup(repositorio => repositorio.ObterPorLogin(It.IsAny<string>()))
                         .Returns(Task.FromResult(usuario));

      var resposta = await _servico.Autenticar(dadosUsuario);

      resposta.Erro.Mensagem.Should().Be("Senha inválido(a)!");
      resposta.Erro.StatusCode.Should().Be(400);
      resposta.Erro.GetType().Should().Be(typeof(ErroAtributoInvalido));
    }

    [Fact]
    public async Task Deve_Validar_Token_No_Banco_De_Dados()
    {
      _usuarios.Setup(repositorio => repositorio.ValidarToken(It.IsAny<string>()))
                         .Returns(Task.FromResult(true));

      var tokenEhValido = await _servico.ValidarToken("Bearer 43f1818c-d5bf-46f1-8391-fe619d01653c");

      tokenEhValido.Should().BeTrue();
    }

    [Fact]
    public async Task Deve_Retornar_Falso_Se_O_Token_For_Invalido()
    {
      var tokenEhValido = await _servico.ValidarToken("Bearer 04c380ef-5470-4408-b2ea-76809b2e160e");

      tokenEhValido.Should().BeFalse();
    }

    [Fact]
    public async Task Deve_Retornar_Falso_Se_O_Token_Estiver_Em_Branco()
    {
      _usuarios.Setup(repositorio => repositorio.ValidarToken(It.IsAny<string>()))
                         .Returns(Task.FromResult(true));

      var tokenEhValido = await _servico.ValidarToken("");

      tokenEhValido.Should().BeFalse();
    }

    [Fact]
    public async Task Deve_Retornar_Falso_Se_O_Token_Estiver_Incompleto()
    {
      _usuarios.Setup(repositorio => repositorio.ValidarToken(It.IsAny<string>()))
                         .Returns(Task.FromResult(true));

      var tokenEhValido = await _servico.ValidarToken("04c380ef-5470-4408-b2ea-76809b2e160e");

      tokenEhValido.Should().BeFalse();
    }
  }
}