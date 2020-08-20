using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Agenda.Dominio.DTOs
{
  public struct Usuario
  {
    public Guid Id { get; set; }

    [JsonProperty("login")]
    public string Login { get; set; }

    public string Senha { get; set; }

    public IEnumerable<Modelos.Contato> Contatos { get; set; }
  }
}