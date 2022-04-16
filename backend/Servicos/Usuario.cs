using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Infraestrutura.Erros;
using DTOs = Agenda.Dominio.DTOs;
using Modelos = Agenda.Dominio.Modelos;

namespace Agenda.Servicos
{
    public class Usuario : Dominio.Servicos.Usuario
    {
        private readonly Dominio.Repositorios.Usuarios _usuarios;

        public Usuario(Dominio.Repositorios.Usuarios usuarios)
        {
            _usuarios = usuarios;
        }

        public async Task<Dominio.Servicos.Resposta<DTOs.Usuario>> Autenticar(DTOs.NovoUsuario dadosUsuario)
        {
            var resposta = new Resposta<DTOs.Usuario>();
            var senha = new Modelos.Senha();

            var usuario = await _usuarios.ObterPorLogin(dadosUsuario.Login);

            if (usuario == null)
            {
                resposta.Erro = new ErroAtributoInvalido("Login");
                return resposta;
            }

            bool senhaEhValida = senha.Validar(usuario.Senha, dadosUsuario.Senha);

            if (senhaEhValida)
            {
                usuario.AdicionarToken(Guid.NewGuid().ToString());

                await _usuarios.Salvar(usuario);

                resposta.Resultado = new DTOs.Usuario
                {
                    Id = usuario.Id,
                    Login = usuario.Login,
                    Token = usuario.Token,
                    Nome = usuario.Nome
                };
            }

            else
                resposta.Erro = new ErroAtributoInvalido("Senha");

            return resposta;
        }

        public async Task<Dominio.Servicos.Resposta<Modelos.Usuario>> Deletar(int id)
        {
            var resposta = new Resposta<Modelos.Usuario>();

            var usuario = await _usuarios.ObterPorId(id);

            if (usuario == null)
                resposta.Erro = new ErroObjetoNaoEncontrado("Usuário");

            else if (ExisteContatosVinculados(usuario))
                resposta.Erro = new ErroObjetoPossuiObjetosVinculados("usuário", "contatos");

            else
                await _usuarios.Deletar(id);

            return resposta;
        }

        public Task<List<Modelos.Usuario>> Listar() => _usuarios.Listar();

        public async Task<Dominio.Servicos.Resposta<Modelos.Usuario>> ObterPorId(int id)
        {
            var resposta = new Resposta<Modelos.Usuario>();

            var usuario = await _usuarios.ObterPorId(id);

            if (usuario == null)
                resposta.Erro = new ErroObjetoNaoEncontrado("Usuário");

            else
                resposta.Resultado = usuario;

            return resposta;
        }

        public async Task<Dominio.Servicos.Resposta<Modelos.Usuario>> Salvar(DTOs.NovoUsuario dadosUsuario)
        {
            var resposta = new Resposta<Modelos.Usuario>();
            var senha = new Modelos.Senha();

            if (string.IsNullOrWhiteSpace(dadosUsuario.Nome))
                resposta.Erro = new ErroAtributoEmBranco("nome");

            else if (string.IsNullOrWhiteSpace(dadosUsuario.Login))
                resposta.Erro = new ErroAtributoEmBranco("login");

            else if (string.IsNullOrWhiteSpace(dadosUsuario.Senha))
                resposta.Erro = new ErroAtributoEmBranco("senha");

            else
            {
                var usuario = new Modelos.Usuario(dadosUsuario.Login, senha.GerarHash(dadosUsuario.Senha), dadosUsuario.Nome);

                usuario.Id = await _usuarios.Salvar(usuario);

                resposta.Resultado = usuario;
            }

            return resposta;
        }

        public async Task<bool> ValidarToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token) || token.Contains("Bearer") == false)
                return false;

            return await _usuarios.ValidarToken(token);
        }

        private bool ExisteContatosVinculados(Modelos.Usuario usuario) => usuario.Contatos.Count() > 0;
    }
}