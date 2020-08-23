using System;
using System.Collections.Generic;

namespace Agenda.Dominio.DTOs
{
  public struct Usuario
  {
    public Guid Id { get; set; }

    public string Login { get; set; }

    public string Senha { get; set; }

    public string Token { get; set; }

    public string Nome { get; set; }

    public IEnumerable<Modelos.Contato> Contatos { get; set; }
  }
}