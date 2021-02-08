using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agenda.Dominio.Servicos
{
  public interface Usuario
  {
    Task<Resposta<Modelos.Usuario>> Salvar(DTOs.Usuario dadosUsuario);

    Task<List<Modelos.Usuario>> Listar();

    Task<Resposta<Modelos.Usuario>> ObterPorId(int id);

    Task<Resposta<Modelos.Usuario>> Deletar(int id);

    Task<Resposta<DTOs.Usuario>> Autenticar(DTOs.Usuario dadosUsuario);

    Task<bool> ValidarToken(string token);
  }
}