using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agenda.Dominio.Servicos
{
  public interface Usuario
  {
    Task<Resposta<Modelos.Usuario>> Salvar(DTOs.NovoUsuario dadosUsuario);

    Task<List<Modelos.Usuario>> Listar();

    Task<Resposta<Modelos.Usuario>> ObterPorId(int id);

    Task<Resposta<Modelos.Usuario>> Deletar(int id);

    Task<Resposta<DTOs.Usuario>> Autenticar(DTOs.NovoUsuario dadosUsuario);

    Task<bool> ValidarToken(string token);
  }
}