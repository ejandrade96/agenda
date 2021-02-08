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

      _api.DefaultRequestHeaders.Add("Authorization", "Bearer 43f1818c-d5bf-46f1-8391-fe619d01653c");
      var retorno = await _api.PostAsync("/usuarios/1/contatos", ConverterParaJSON<Object>(contato));

      retorno.StatusCode.Should().Be(HttpStatusCode.Created);
      retorno.Headers.Location.ToString().Contains("/usuarios/").Should().BeTrue();
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Cadastrar_Um_Contato_Com_O_Token_Do_Usuario_Invalido()
    {
      var contato = new
      {
        nome = "Contato",
        telefone = "11 45872534",
        celular = "11 958742136",
        email = "contato@live.com"
      };

      _api.DefaultRequestHeaders.Add("Authorization", "Bearer 04c380ef-5470-4408-b2ea-76809b2e160e");
      var retorno = await _api.PostAsync("/usuarios/1/contatos", ConverterParaJSON<Object>(contato));

      retorno.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
      retorno.StatusCode.Should().Be(401);
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

      _api.DefaultRequestHeaders.Add("Authorization", "Bearer 43f1818c-d5bf-46f1-8391-fe619d01653c");
      var retorno = await _api.PostAsync($"/usuarios/{0}/contatos", ConverterParaJSON<Object>(contato));
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

      _api.DefaultRequestHeaders.Add("Authorization", "Bearer 43f1818c-d5bf-46f1-8391-fe619d01653c");
      var retorno = await _api.PostAsync("/usuarios/1/contatos", ConverterParaJSON<Object>(contato));
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

      _api.DefaultRequestHeaders.Add("Authorization", "Bearer 43f1818c-d5bf-46f1-8391-fe619d01653c");
      var retorno = await _api.PostAsync("/usuarios/1/contatos", ConverterParaJSON<Object>(contato));
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

      _api.DefaultRequestHeaders.Add("Authorization", "Bearer 43f1818c-d5bf-46f1-8391-fe619d01653c");
      var retorno = await _api.PostAsync("/usuarios/1/contatos", ConverterParaJSON<Object>(contato));
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

      _api.DefaultRequestHeaders.Add("Authorization", "Bearer 43f1818c-d5bf-46f1-8391-fe619d01653c");
      var retorno = await _api.PostAsync("/usuarios/1/contatos", ConverterParaJSON<Object>(contato));
      var mensagem = await retorno.Content.ReadAsStringAsync();

      retorno.StatusCode.Should().Be(HttpStatusCode.BadRequest);
      mensagem.Should().Contain("Favor preencher o nome.");
    }

    [Fact]
    public async Task Deve_Retornar_Um_Contato_Por_Id()
    {
      _api.DefaultRequestHeaders.Add("Authorization", "Bearer 43f1818c-d5bf-46f1-8391-fe619d01653c");
      var retorno = await _api.GetAsync("/contatos/1");
      var contatoEmJson = await retorno.Content.ReadAsStringAsync();
      var contato = Converter<Dictionary<string, string>>(contatoEmJson);

      retorno.StatusCode.Should().Be(HttpStatusCode.OK);
      contato["nome"].Should().Be("Contato 1");
      contato["telefone"].Should().Be("11 49782534");
      contato["celular"].Should().Be("11 958742136");
      contato["email"].Should().Be("contato1@live.com");
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Buscar_Um_Contato_Inexistente()
    {
      _api.DefaultRequestHeaders.Add("Authorization", "Bearer 43f1818c-d5bf-46f1-8391-fe619d01653c");
      var retorno = await _api.GetAsync($"/contatos/{0}");
      var erroEmJson = await retorno.Content.ReadAsStringAsync();
      var erro = Converter<Dictionary<string, string>>(erroEmJson);

      retorno.StatusCode.Should().Be(HttpStatusCode.NotFound);
      retorno.StatusCode.Should().Be(404);
      erro["mensagem"].Should().Be("Contato não encontrado(a)!");
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Buscar_Um_Contato_De_Um_Usuario_Com_Token_Invalido()
    {
      _api.DefaultRequestHeaders.Add("Authorization", "Bearer 04c380ef-5470-4408-b2ea-76809b2e160e");
      var retorno = await _api.GetAsync("/contatos/1");

      retorno.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
      retorno.StatusCode.Should().Be(401);
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
      _api.DefaultRequestHeaders.Add("Authorization", "Bearer 43f1818c-d5bf-46f1-8391-fe619d01653c");
      var retorno = await _api.GetAsync("/usuarios/1/contatos");
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
      _api.DefaultRequestHeaders.Add("Authorization", "Bearer 43f1818c-d5bf-46f1-8391-fe619d01653c");
      var retorno = await _api.GetAsync($"/usuarios/{0}/contatos");
      var erroEmJson = await retorno.Content.ReadAsStringAsync();
      var erro = Converter<Dictionary<string, string>>(erroEmJson);

      retorno.StatusCode.Should().Be(HttpStatusCode.NotFound);
      retorno.StatusCode.Should().Be(404);
      erro["mensagem"].Should().Be("Usuário não encontrado(a)!");
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Buscar_Todos_Os_Contatos_De_Um_Usuario_Com_O_Token_Invalido()
    {
      _api.DefaultRequestHeaders.Add("Authorization", "Bearer 04c380ef-5470-4408-b2ea-76809b2e160e");
      var retorno = await _api.GetAsync("/usuarios/1/contatos");

      retorno.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
      retorno.StatusCode.Should().Be(401);
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

      _api.DefaultRequestHeaders.Add("Authorization", "Bearer 43f1818c-d5bf-46f1-8391-fe619d01653c");
      var retorno = await _api.PutAsync("/contatos/1", ConverterParaJSON<Object>(contato));

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

      _api.DefaultRequestHeaders.Add("Authorization", "Bearer 43f1818c-d5bf-46f1-8391-fe619d01653c");
      var retorno = await _api.PutAsync("/contatos/1", ConverterParaJSON<Object>(contato));

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

      _api.DefaultRequestHeaders.Add("Authorization", "Bearer 43f1818c-d5bf-46f1-8391-fe619d01653c");
      var retorno = await _api.PutAsync("/contatos/1", ConverterParaJSON<Object>(contato));

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

      _api.DefaultRequestHeaders.Add("Authorization", "Bearer 43f1818c-d5bf-46f1-8391-fe619d01653c");
      var retorno = await _api.PutAsync("/contatos/1", ConverterParaJSON<Object>(contato));

      retorno.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Atualizar_Um_Contato_Inexistente()
    {
      var contato = new
      {
        nome = "Contato 1",
        telefone = "11 45872534",
        celular = "11 958742136",
        email = "contato10@live.com"
      };

      _api.DefaultRequestHeaders.Add("Authorization", "Bearer 43f1818c-d5bf-46f1-8391-fe619d01653c");
      var retorno = await _api.PutAsync($"/contatos/{0}", ConverterParaJSON<Object>(contato));
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

      _api.DefaultRequestHeaders.Add("Authorization", "Bearer 43f1818c-d5bf-46f1-8391-fe619d01653c");
      var retorno = await _api.PutAsync("/contatos/1", ConverterParaJSON<Object>(contato));
      var mensagem = await retorno.Content.ReadAsStringAsync();

      retorno.StatusCode.Should().Be(HttpStatusCode.BadRequest);
      retorno.StatusCode.Should().Be(400);
      mensagem.Should().Contain("E-mail inválido!");
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Atualizar_Um_Contato_De_Um_Usuario_Com_O_Token_Invalido()
    {
      var contato = new
      {
        nome = "Contato 1",
        telefone = "11 45872534",
        celular = "11 958742136",
        email = "contato10@live.com"
      };

      _api.DefaultRequestHeaders.Add("Authorization", "Bearer 04c380ef-5470-4408-b2ea-76809b2e160e");
      var retorno = await _api.PutAsync("/contatos/1", ConverterParaJSON<Object>(contato));

      retorno.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
      retorno.StatusCode.Should().Be(401);
    }

    [Fact]
    public async Task Deve_Deletar_Um_Contato()
    {
      _api.DefaultRequestHeaders.Add("Authorization", "Bearer 43f1818c-d5bf-46f1-8391-fe619d01653c");
      var retorno = await _api.DeleteAsync("/contatos/1");

      retorno.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Deletar_Um_Contato_Inexistente()
    {
      _api.DefaultRequestHeaders.Add("Authorization", "Bearer 43f1818c-d5bf-46f1-8391-fe619d01653c");
      var retorno = await _api.DeleteAsync($"/contatos/{0}");
      var erroEmJson = await retorno.Content.ReadAsStringAsync();
      var erro = Converter<Dictionary<string, string>>(erroEmJson);

      retorno.StatusCode.Should().Be(HttpStatusCode.NotFound);
      retorno.StatusCode.Should().Be(404);
      erro["mensagem"].Should().Be("Contato não encontrado(a)!");
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Tentar_Deletar_Um_Contato_De_Um_Usuario_Com_O_Token_Invalido()
    {
      _api.DefaultRequestHeaders.Add("Authorization", "Bearer 04c380ef-5470-4408-b2ea-76809b2e160e");
      var retorno = await _api.DeleteAsync("/contatos/1");

      retorno.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
      retorno.StatusCode.Should().Be(401);
    }
  }
}
