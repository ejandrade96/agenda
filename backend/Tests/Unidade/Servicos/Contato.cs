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
        private readonly Dominio.Servicos.Contato _servico;

        private readonly Mock<Contatos> _contatos;

        private readonly Mock<Usuarios> _usuarios;

        public Contato()
        {
            _contatos = new Mock<Contatos>();
            _usuarios = new Mock<Usuarios>();
            _servico = new Agenda.Servicos.Contato(_contatos.Object, _usuarios.Object);
        }

        [Fact]
        public async Task Deve_Cadastrar_Um_Contato_Quando_Enviar_Dados_Certos()
        {
            var contato = new DTOs.NovoContato
            {
                Nome = "Contato",
                Telefone = "11 45873214",
                Celular = "11 985478521",
                Email = "contato@live.com",
                UsuarioId = 1
            };

            _usuarios.Setup(repositorio => repositorio.ObterPorId(It.IsAny<int>()))
                     .Returns(Task.FromResult(new Modelos.Usuario("", "", "")));

            _contatos.Setup(repositorio => repositorio.Salvar(It.IsAny<Modelos.Contato>()))
                     .Returns(Task.FromResult(1));

            var resposta = await _servico.Salvar(contato);

            var contatoCadastrado = resposta.Resultado;

            contatoCadastrado.Id.Should().NotBe(0);
            contatoCadastrado.Nome.Should().Be("Contato");
            contatoCadastrado.Telefone.Should().Be("11 45873214");
            contatoCadastrado.Celular.Should().Be("11 985478521");
            contatoCadastrado.Email.Should().Be("contato@live.com");
        }

        [Fact]
        public async Task Deve_Retornar_Erro_Quando_Tentar_Cadastrar_Um_Contato_Em_Um_Usuario_Inexistente()
        {
            var contato = new DTOs.NovoContato
            {
                Nome = "Contato",
                Telefone = "11 45873214",
                Celular = "11 985478521",
                Email = "contato@live.com",
                UsuarioId = 1
            };

            var resposta = await _servico.Salvar(contato);

            resposta.Erro.Mensagem.Should().Be("Usuário não encontrado(a)!");
            resposta.Erro.StatusCode.Should().Be(404);
            resposta.Erro.GetType().Should().Be(typeof(ErroObjetoNaoEncontrado));
        }

        [Fact]
        public async Task Deve_Retornar_Um_Contato_Por_Id()
        {
            var usuario = new Modelos.Usuario("", "", "");

            var contato = new Modelos.Contato(
              "Contato",
              "11 985478521",
              "11 45873214",
              "contato@live.com",
              usuario
              );

            _contatos.Setup(repositorio => repositorio.ObterPorId(It.IsAny<int>()))
                     .Returns(Task.FromResult(contato));

            var resposta = await _servico.ObterPorId(1);

            var contatoEncontrado = resposta.Resultado;

            contatoEncontrado.Nome.Should().Be("Contato");
            contatoEncontrado.Celular.Should().Be("11 985478521");
            contatoEncontrado.Telefone.Should().Be("11 45873214");
            contatoEncontrado.Email.Should().Be("contato@live.com");
        }

        [Fact]
        public async Task Deve_Retornar_Erro_Quando_Tentar_Buscar_Um_Contato_Inexistente()
        {
            var resposta = await _servico.ObterPorId(1);

            resposta.Erro.Mensagem.Should().Be("Contato não encontrado(a)!");
            resposta.Erro.StatusCode.Should().Be(404);
            resposta.Erro.GetType().Should().Be(typeof(ErroObjetoNaoEncontrado));
        }

        [Fact]
        public async Task Deve_Atualizar_Um_Contato()
        {
            var usuario = new Modelos.Usuario("usuario", "123", "usuário nome");

            var contato = new DTOs.AlteraContato
            {
                Id = 1,
                Nome = "Contato",
                Telefone = "11 45873214",
                Celular = "11 985478521",
                Email = "contato@live.com"
            };

            _contatos.Setup(repositorio => repositorio.ObterPorId(It.IsAny<int>()))
                     .Returns(Task.FromResult(new Modelos.Contato("Contato", "11 985478521", "11 45873214", "contato@live.com", usuario)));

            var resposta = await _servico.Atualizar(contato);

            resposta.TemErro().Should().BeFalse();
        }

        [Fact]
        public async Task Deve_Retornar_Erro_Quando_Tentar_Atualizar_Um_Contato_Inexistente()
        {
            var contato = new DTOs.AlteraContato
            {
                Id = 1,
                Nome = "Contato",
                Telefone = "11 45873214",
                Celular = "11 985478521",
                Email = "contato@live.com"
            };

            var resposta = await _servico.Atualizar(contato);

            resposta.Erro.Mensagem.Should().Be("Contato não encontrado(a)!");
            resposta.Erro.StatusCode.Should().Be(404);
            resposta.Erro.GetType().Should().Be(typeof(ErroObjetoNaoEncontrado));
        }

        [Fact]
        public async Task Deve_Deletar_Um_Contato()
        {
            var usuario = new Modelos.Usuario("", "", "");

            var contato = new Modelos.Contato(
              "Contato",
              "11 985478521",
              "11 45873214",
              "contato@live.com",
              usuario
              );

            _contatos.Setup(repositorio => repositorio.ObterPorId(It.IsAny<int>()))
                     .Returns(Task.FromResult(contato));

            var resposta = await _servico.Deletar(1);

            resposta.TemErro().Should().BeFalse();
        }

        [Fact]
        public async Task Deve_Retornar_Erro_Quando_Tentar_Deletar_Um_Contato_Inexistente()
        {
            var resposta = await _servico.Deletar(1);

            resposta.Erro.Mensagem.Should().Be("Contato não encontrado(a)!");
            resposta.Erro.StatusCode.Should().Be(404);
            resposta.Erro.GetType().Should().Be(typeof(ErroObjetoNaoEncontrado));
        }

        [Fact]
        public async Task Deve_Retornar_Todos_Os_Contatos_De_Um_Usuario()
        {
            var usuario = new Modelos.Usuario("usuario", "123", "usuário nome");
            var contato = new Modelos.Contato("Contato", "11 985478521", "11 45873214", "contato@live.com", usuario);
            usuario.AdicionarContato(contato);

            _usuarios.Setup(repositorio => repositorio.ObterPorId(It.IsAny<int>()))
                     .Returns(Task.FromResult(usuario));

            var resposta = await _servico.ListarPorUsuarioId(1);

            var contatos = resposta.Resultado;

            contatos.Should().HaveCountGreaterThan(0);
            contatos.ForEach((contato) =>
            {
                contato.GetType().GetProperty("Id").Should().NotBeNull();
                contato.GetType().GetProperty("Nome").Should().NotBeNull();
                contato.GetType().GetProperty("Telefone").Should().NotBeNull();
                contato.GetType().GetProperty("Celular").Should().NotBeNull();
                contato.GetType().GetProperty("Email").Should().NotBeNull();
            });
        }

        [Fact]
        public async Task Deve_Retornar_Erro_Quando_Tentar_Buscar_Todos_Os_Contatos_De_Um_Usuario_Inexistente()
        {
            var usuarioId = Guid.NewGuid();
            var resposta = await _servico.ListarPorUsuarioId(1);

            resposta.Erro.Mensagem.Should().Be("Usuário não encontrado(a)!");
            resposta.Erro.StatusCode.Should().Be(404);
            resposta.Erro.GetType().Should().Be(typeof(ErroObjetoNaoEncontrado));
        }
    }
}