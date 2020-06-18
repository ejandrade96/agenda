using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Agenda.Dominio.DTOs
{
  public struct Contato
  {
    [JsonProperty("nome")]
    public string Nome { get; set; }

    [JsonProperty("telefone")]
    public string Telefone { get; set; }

    [JsonProperty("celular")]
    public string Celular { get; set; }

    [EmailAddress(ErrorMessage = "E-mail inválido!")]
    [JsonProperty("email")]
    public string Email { get; set; }

    public Guid Id { get; set; }
  }
}