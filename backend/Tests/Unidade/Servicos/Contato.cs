using Moq;
using Agenda.Dominio.Repositorios;
using Xunit;
using System.Threading.Tasks;
using DTOs = Agenda.Dominio.DTOs;
using Modelos = Agenda.Dominio.Modelos;
using System;
using FluentAssertions;
using Agenda.Infraestrutura.Erros;

namespace Agenda.Tests.Unidade.Servicos
{
  public class Contato
  {
    private readonly Agenda.Servicos.Contato _servico;

    private readonly Mock<Contatos> _contatos;

    public Contato()
    {
      _contatos = new Mock<Contatos>();
      _servico = new Agenda.Servicos.Contato(_contatos.Object);
    }

    [Fact]
    public async Task Deve_Cadastrar_Um_Contato_Quando_Enviar_Dados_Certos()
    {
      var contato = new DTOs.Contato
      {
        Nome = "Contato",
        Telefone = "11 45873214",
        Celular = "11 985478521",
        Email = "contato@live.com"
      };

      _contatos.Setup(repositorio => repositorio.Salvar(It.IsAny<Modelos.Contato>())).Returns(Task.FromResult(Guid.NewGuid()));

      var resposta = await _servico.Salvar(contato);

      var contatoCadastrado = resposta.Resultado;

      contatoCadastrado.Id.Should().NotBeEmpty();
      contatoCadastrado.Nome.Should().Be("Contato");
      contatoCadastrado.Telefone.Should().Be("11 45873214");
      contatoCadastrado.Celular.Should().Be("11 985478521");
      contatoCadastrado.Email.Should().Be("contato@live.com");
    }

    [Fact]
    public async Task Deve_Retornar_Um_Contato_Por_Id()
    {
      _contatos.Setup(repositorio => repositorio.ObterPorId(It.IsAny<Guid>()))
        .Returns(Task.FromResult(new Modelos.Contato("Contato", "11 985478521", "11 45873214", "contato@live.com")));

      var resposta = await _servico.ObterPorId(Guid.Parse("1246a68e-755e-4c18-bc7c-49845507691e"));

      var contato = resposta.Resultado;

      contato.Nome.Should().Be("Contato");
      contato.Celular.Should().Be("11 985478521");
      contato.Telefone.Should().Be("11 45873214");
      contato.Email.Should().Be("contato@live.com");
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Buscar_Um_Contato_Inexistente()
    {
      var id = Guid.NewGuid();
      var resposta = await _servico.ObterPorId(id);

      resposta.Erro.Mensagem.Should().Be("Contato não encontrado(a)!");
      resposta.Erro.StatusCode.Should().Be(404);
      resposta.Erro.GetType().Should().Be(typeof(ErroObjetoNaoEncontrado));
    }

    [Fact]
    public async Task Deve_Atualizar_Um_Contato()
    {
      var contato = new DTOs.Contato
      {
        Nome = "Contato",
        Telefone = "11 45873214",
        Celular = "11 985478521",
        Email = "contato@live.com",
        Id = Guid.Parse("1246a68e-755e-4c18-bc7c-49845507691e")
      };

      _contatos.Setup(repositorio => repositorio.ObterPorId(It.IsAny<Guid>()))
        .Returns(Task.FromResult(new Modelos.Contato("Contato", "11 985478521", "11 45873214", "contato@live.com")));

      var resposta = await _servico.Salvar(contato);

      resposta.TemErro().Should().BeFalse();
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Atualizar_Um_Contato_Inexistente()
    {
      var contato = new DTOs.Contato
      {
        Nome = "Contato",
        Telefone = "11 45873214",
        Celular = "11 985478521",
        Email = "contato@live.com",
        Id = Guid.NewGuid()
      };

      var resposta = await _servico.Salvar(contato);

      resposta.Erro.Mensagem.Should().Be("Contato não encontrado(a)!");
      resposta.Erro.StatusCode.Should().Be(404);
      resposta.Erro.GetType().Should().Be(typeof(ErroObjetoNaoEncontrado));
    }

    [Fact]
    public async Task Deve_Deletar_Um_Contato()
    {
      _contatos.Setup(repositorio => repositorio.ObterPorId(It.IsAny<Guid>()))
        .Returns(Task.FromResult(new Modelos.Contato("Contato", "11 985478521", "11 45873214", "contato@live.com")));

      var resposta = await _servico.Deletar(Guid.Parse("1246a68e-755e-4c18-bc7c-49845507691e"));

      resposta.TemErro().Should().BeFalse();
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Deletar_Um_Contato_Inexistente()
    {
      var id = Guid.NewGuid();
      var resposta = await _servico.Deletar(id);

      resposta.Erro.Mensagem.Should().Be("Contato não encontrado(a)!");
      resposta.Erro.StatusCode.Should().Be(404);
      resposta.Erro.GetType().Should().Be(typeof(ErroObjetoNaoEncontrado));
    }
  }
}