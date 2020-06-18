using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Agenda.Api;
using Newtonsoft.Json;
using System.Text;
using System;
using FluentAssertions;
using System.Net;
using System.Collections.Generic;

namespace Agenda.Tests.Integracao
{
  public class Contato
  {
    protected HttpClient _api;

    public Contato()
    {
      var appFactory = new WebApplicationFactory<Startup>()
           .WithWebHostBuilder(builder =>
           {
             builder.ConfigureServices(services =>
             {
             });
           });

      _api = appFactory.CreateClient();
    }

    protected HttpContent ConverterParaJSON<T>(T valor) => new StringContent(JsonConvert.SerializeObject(valor), Encoding.UTF8, "application/json");

    protected T Converter<T>(string json) => JsonConvert.DeserializeObject<T>(json);

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

      var retorno = await _api.PostAsync("/contatos", ConverterParaJSON<Object>(contato));

      retorno.StatusCode.Should().Be(HttpStatusCode.Created);
      retorno.Headers.Location.ToString().Contains("/contatos/").Should().BeTrue();
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

      var retorno = await _api.PostAsync("/contatos", ConverterParaJSON<Object>(contato));
      var mensagem = await retorno.Content.ReadAsStringAsync();

      retorno.StatusCode.Should().Be(HttpStatusCode.BadRequest);
      retorno.StatusCode.Should().Be(400);
      mensagem.Should().Contain("E-mail inválido!");
    }

    [Fact]
    public async Task Deve_Retornar_Um_Contato_Por_Id()
    {
      var retorno = await _api.GetAsync("/contatos/181fbe3a-d3ec-43a5-8791-9db5c3841cef");
      var contatoEmJson = await retorno.Content.ReadAsStringAsync();
      var contato = Converter<Dictionary<string, string>>(contatoEmJson);

      retorno.StatusCode.Should().Be(HttpStatusCode.OK);
      contato["nome"].Should().Be("Contato 1");
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

      var retorno = await _api.PutAsync("/contatos/1246A68E-755E-4C18-BC7C-49845507691E", ConverterParaJSON<Object>(contato));
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
