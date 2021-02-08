using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agenda.Dominio.Servicos
{
  public interface Contato
  {
    Task<Resposta<Modelos.Contato>> Salvar(DTOs.Contato dadosContato);

    Task<Resposta<Modelos.Contato>> ObterPorId(int id);

    Task<List<Modelos.Contato>> Listar(string nome);

    Task<Resposta<List<Modelos.Contato>>> ListarPorUsuarioId(int usuarioId);

    Task<Resposta<Modelos.Contato>> Deletar(int id);

    Task<Resposta<Modelos.Contato>> Atualizar(DTOs.Contato dadosContato);
  }
}