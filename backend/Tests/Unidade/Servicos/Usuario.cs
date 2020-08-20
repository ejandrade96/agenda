using Agenda.Dominio.Repositorios;
using Moq;
using Xunit;
using Modelos = Agenda.Dominio.Modelos;
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
      var usuario = new Modelos.Usuario("", "");

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
    public async Task Deve_Retornar_Um_Usuario_Por_Id()
    {
      var usuario = new Modelos.Usuario("usuario.xpto", "123456");

      _usuarios.Setup(repositorio => repositorio.ObterPorId(It.IsAny<Guid>()))
               .Returns(Task.FromResult(usuario));

      var resposta = await _servico.ObterPorId(Guid.Parse("149944ca-6d46-4357-bcdc-bdeebbe66377"));

      var usuarioEncontrado = resposta.Resultado;

      usuarioEncontrado.Login.Should().Be("usuario.xpto");
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
  }
}