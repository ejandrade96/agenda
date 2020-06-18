using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agenda.Dominio.Servicos
{
  public interface Contato
  {
    Task<Resposta<Modelos.Contato>> Salvar(DTOs.Contato dadosContato);

    Task<Resposta<Modelos.Contato>> ObterPorId(Guid id);

    Task<List<Modelos.Contato>> Listar(string nome);

    Task<Resposta<Modelos.Contato>> Deletar(Guid id);
  }
}