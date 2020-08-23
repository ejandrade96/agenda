using System.Threading.Tasks;
using Xunit;
using System;
using FluentAssertions;
using System.Net;
using System.Collections.Generic;

namespace Agenda.Tests.Integracao
{
  public class Contato : IntegracaoBase
  {
    [Fact]
    public async Task Deve_Cadastrar_Um_Contato_Quando_Enviar_Dados_Certos()
    {
      var contato = new
      {
        nome = "Contato",
        telefone = "11 45872534",
        celular = "11 958742136",
        email = "contato@live.com"
      };

      var retorno = await _api.PostAsync("/usuarios/B1ACBCDE-D53C-4D89-9F94-B2316694703F/contatos", ConverterParaJSON<Object>(contato));

      retorno.StatusCode.Should().Be(HttpStatusCode.Created);
      retorno.Headers.Location.ToString().Contains("/usuarios/").Should().BeTrue();
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Cadastrar_Um_Contato_Em_Um_Usuario_Inexistente()
    {
      var contato = new
      {
        nome = "Contato",
        telefone = "11 45872534",
        celular = "11 958742136",
        email = "contato@live.com"
      };

      var usuarioId = Guid.NewGuid();

      var retorno = await _api.PostAsync($"/usuarios/{usuarioId}/contatos", ConverterParaJSON<Object>(contato));
      var erroEmJson = await retorno.Content.ReadAsStringAsync();
      var erro = Converter<Dictionary<string, string>>(erroEmJson);

      retorno.StatusCode.Should().Be(HttpStatusCode.NotFound);
      retorno.StatusCode.Should().Be(404);
      erro["mensagem"].Should().Be("Usuário não encontrado(a)!");
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Cadastrar_Um_Contato_Com_Email_Invalido()
    {
      var contato = new
      {
        nome = "Contato",
        telefone = "11 45872534",
        celular = "11 958742136",
        email = "contato.com"
      };

      var retorno = await _api.PostAsync("/usuarios/4337e5b1-138e-45c0-b6ac-3f1ebe3c133b/contatos", ConverterParaJSON<Object>(contato));
      var mensagem = await retorno.Content.ReadAsStringAsync();

      retorno.StatusCode.Should().Be(HttpStatusCode.BadRequest);
      retorno.StatusCode.Should().Be(400);
      mensagem.Should().Contain("E-mail inválido!");
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Cadastrar_Um_Contato_Com_Telefone_Invalido()
    {
      var contato = new
      {
        nome = "Contato",
        telefone = "11 25",
        celular = "11 958742136",
        email = "contato@live.com"
      };

      var retorno = await _api.PostAsync("/usuarios/4337e5b1-138e-45c0-b6ac-3f1ebe3c133b/contatos", ConverterParaJSON<Object>(contato));
      var mensagem = await retorno.Content.ReadAsStringAsync();

      retorno.StatusCode.Should().Be(HttpStatusCode.BadRequest);
      mensagem.Should().Contain("Telefone inválido!");
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Cadastrar_Um_Contato_Com_Celular_Invalido()
    {
      var contato = new
      {
        nome = "Contato",
        telefone = "11 45872534",
        celular = "11 45ds24",
        email = "contato@live.com"
      };

      var retorno = await _api.PostAsync("/usuarios/4337e5b1-138e-45c0-b6ac-3f1ebe3c133b/contatos", ConverterParaJSON<Object>(contato));
      var mensagem = await retorno.Content.ReadAsStringAsync();

      retorno.StatusCode.Should().Be(HttpStatusCode.BadRequest);
      mensagem.Should().Contain("Celular inválido!");
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Cadastrar_Um_Contato_Com_Nome_Em_Branco()
    {
      var contato = new
      {
        nome = " ",
        telefone = "11 45872534",
        celular = "11 45ds24",
        email = "contato@live.com"
      };

      var retorno = await _api.PostAsync("/usuarios/4337e5b1-138e-45c0-b6ac-3f1ebe3c133b/contatos", ConverterParaJSON<Object>(contato));
      var mensagem = await retorno.Content.ReadAsStringAsync();

      retorno.StatusCode.Should().Be(HttpStatusCode.BadRequest);
      mensagem.Should().Contain("Favor preencher o nome.");
    }

    [Fact]
    public async Task Deve_Retornar_Um_Contato_Por_Id()
    {
      var retorno = await _api.GetAsync("/contatos/181fbe3a-d3ec-43a5-8791-9db5c3841cef");
      var contatoEmJson = await retorno.Content.ReadAsStringAsync();
      var contato = Converter<Dictionary<string, string>>(contatoEmJson);

      retorno.StatusCode.Should().Be(HttpStatusCode.OK);
      contato["nome"].Should().Be("Contato 1 (teste)");
      contato["telefone"].Should().Be("11 47549874");
      contato["celular"].Should().Be("11 985471254");
      contato["email"].Should().Be("contato1@live.com");
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Buscar_Um_Contato_Inexistente()
    {
      var id = Guid.NewGuid();
      var retorno = await _api.GetAsync($"/contatos/{id}");
      var erroEmJson = await retorno.Content.ReadAsStringAsync();
      var erro = Converter<Dictionary<string, string>>(erroEmJson);

      retorno.StatusCode.Should().Be(HttpStatusCode.NotFound);
      retorno.StatusCode.Should().Be(404);
      erro["mensagem"].Should().Be("Contato não encontrado(a)!");
    }

    [Fact]
    public async Task Deve_Retornar_Todos_Os_Contatos_Cadastrados()
    {
      var retorno = await _api.GetAsync("/contatos");
      var contatosEmJson = await retorno.Content.ReadAsStringAsync();
      var contatos = Converter<List<Dictionary<string, string>>>(contatosEmJson);

      retorno.StatusCode.Should().Be(HttpStatusCode.OK);
      contatos.Should().HaveCountGreaterThan(0);
      contatos.ForEach((contato) =>
      {
        contato.Should().ContainKey("id");
        contato.Should().ContainKey("nome");
        contato.Should().ContainKey("telefone");
        contato.Should().ContainKey("celular");
        contato.Should().ContainKey("email");
      });
    }

    [Fact]
    public async Task Deve_Retornar_Todos_Os_Contatos_De_Um_Usuario()
    {
      var retorno = await _api.GetAsync("/usuarios/4337e5b1-138e-45c0-b6ac-3f1ebe3c133b/contatos");
      var contatosEmJson = await retorno.Content.ReadAsStringAsync();
      var contatos = Converter<List<Dictionary<string, object>>>(contatosEmJson);

      retorno.StatusCode.Should().Be(HttpStatusCode.OK);
      contatos.Should().HaveCountGreaterThan(0);
      contatos.ForEach((contato) =>
      {
        contato.Should().ContainKey("id");
        contato.Should().ContainKey("nome");
        contato.Should().ContainKey("telefone");
        contato.Should().ContainKey("celular");
        contato.Should().ContainKey("email");
      });
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Buscar_Todos_Os_Contatos_De_Um_Usuario_Inexistente()
    {
      var usuarioId = Guid.NewGuid();
      var retorno = await _api.GetAsync($"/usuarios/{usuarioId}/contatos");
      var erroEmJson = await retorno.Content.ReadAsStringAsync();
      var erro = Converter<Dictionary<string, string>>(erroEmJson);

      retorno.StatusCode.Should().Be(HttpStatusCode.NotFound);
      retorno.StatusCode.Should().Be(404);
      erro["mensagem"].Should().Be("Usuário não encontrado(a)!");
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Buscar_Todos_Os_Contatos_De_Um_Usuario_Que_Nao_Possui_Contatos()
    {
      var retorno = await _api.GetAsync($"/usuarios/5d8f714c-9e87-476e-a475-c420dbe807ff/contatos");
      var erroEmJson = await retorno.Content.ReadAsStringAsync();
      var erro = Converter<Dictionary<string, string>>(erroEmJson);

      retorno.StatusCode.Should().Be(HttpStatusCode.BadRequest);
      retorno.StatusCode.Should().Be(400);
      erro["mensagem"].Should().Be("Este usuário não possui contatos.");
    }

    [Fact]
    public async Task Deve_Retornar_Contatos_Quando_Buscar_Por_Nome()
    {
      var retorno = await _api.GetAsync("/contatos?nome=ato 1");
      var contatosEmJson = await retorno.Content.ReadAsStringAsync();
      var contatos = Converter<List<Dictionary<string, string>>>(contatosEmJson);

      retorno.StatusCode.Should().Be(HttpStatusCode.OK);
      contatos.Should().HaveCountGreaterThan(0);
      contatos.ForEach((contato) =>
      {
        contato.Should().ContainKey("id");
        contato.Should().ContainKey("nome");
        contato.Should().ContainKey("telefone");
        contato.Should().ContainKey("celular");
        contato.Should().ContainKey("email");
      });
    }

    [Fact]
    public async Task Deve_Atualizar_O_Nome_De_Um_Contato()
    {
      var contato = new
      {
        nome = "Contato 10",
        telefone = "11 45872534",
        celular = "11 958742136",
        email = "contato1@live.com"
      };

      var retorno = await _api.PutAsync("/contatos/181fbe3a-d3ec-43a5-8791-9db5c3841cef", ConverterParaJSON<Object>(contato));

      retorno.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Deve_Atualizar_O_Telefone_De_Um_Contato()
    {
      var contato = new
      {
        nome = "Contato 1",
        telefone = "11 49782534",
        celular = "11 958742136",
        email = "contato1@live.com"
      };

      var retorno = await _api.PutAsync("/contatos/181fbe3a-d3ec-43a5-8791-9db5c3841cef", ConverterParaJSON<Object>(contato));

      retorno.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Deve_Atualizar_O_Celular_De_Um_Contato()
    {
      var contato = new
      {
        nome = "Contato 1",
        telefone = "11 45872534",
        celular = "11 958744125",
        email = "contato1@live.com"
      };

      var retorno = await _api.PutAsync("/contatos/181fbe3a-d3ec-43a5-8791-9db5c3841cef", ConverterParaJSON<Object>(contato));

      retorno.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Deve_Atualizar_O_Email_De_Um_Contato()
    {
      var contato = new
      {
        nome = "Contato 1",
        telefone = "11 45872534",
        celular = "11 958742136",
        email = "contato10@live.com"
      };

      var retorno = await _api.PutAsync("/contatos/181fbe3a-d3ec-43a5-8791-9db5c3841cef", ConverterParaJSON<Object>(contato));

      retorno.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Atualizar_Um_Contato_Inexistente()
    {
      var id = Guid.NewGuid();
      var contato = new
      {
        nome = "Contato 1",
        telefone = "11 45872534",
        celular = "11 958742136",
        email = "contato10@live.com"
      };

      var retorno = await _api.PutAsync($"/contatos/{id}", ConverterParaJSON<Object>(contato));
      var erroEmJson = await retorno.Content.ReadAsStringAsync();
      var erro = Converter<Dictionary<string, string>>(erroEmJson);

      retorno.StatusCode.Should().Be(HttpStatusCode.NotFound);
      retorno.StatusCode.Should().Be(404);
      erro["mensagem"].Should().Be("Contato não encontrado(a)!");
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Atualizar_Um_Contato_Com_Email_Invalido()
    {
      var contato = new
      {
        nome = "Contato",
        telefone = "11 45872534",
        celular = "11 958742136",
        email = "contato.com"
      };

      var retorno = await _api.PutAsync("/contatos/181fbe3a-d3ec-43a5-8791-9db5c3841cef", ConverterParaJSON<Object>(contato));
      var mensagem = await retorno.Content.ReadAsStringAsync();

      retorno.StatusCode.Should().Be(HttpStatusCode.BadRequest);
      retorno.StatusCode.Should().Be(400);
      mensagem.Should().Contain("E-mail inválido!");
    }

    [Fact]
    public async Task Deve_Deletar_Um_Contato()
    {
      var retorno = await _api.DeleteAsync("/contatos/181fbe3a-d3ec-43a5-8791-9db5c3841cef");

      retorno.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Deletar_Um_Contato_Inexistente()
    {
      var id = Guid.NewGuid();
      var retorno = await _api.DeleteAsync($"/contatos/{id}");
      var erroEmJson = await retorno.Content.ReadAsStringAsync();
      var erro = Converter<Dictionary<string, string>>(erroEmJson);

      retorno.StatusCode.Should().Be(HttpStatusCode.NotFound);
      retorno.StatusCode.Should().Be(404);
      erro["mensagem"].Should().Be("Contato não encontrado(a)!");
    }
  }
}
