using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Agenda.Dominio.DTOs
{
  public struct Contato
  {
    [JsonProperty("nome")]
    [Required(ErrorMessage = "Favor preencher o nome.", AllowEmptyStrings = false)]
    public string Nome { get; set; }

    [MinLength(10, ErrorMessage = "Telefone inválido!")]
    [MaxLength(11, ErrorMessage = "Telefone inválido!")]
    [Phone(ErrorMessage = "Telefone inválido!")]
    [JsonProperty("telefone")]
    public string Telefone { get; set; }

    [MinLength(11, ErrorMessage = "Celular inválido!")]
    [MaxLength(12, ErrorMessage = "Celular inválido!")]
    [Phone(ErrorMessage = "Celular inválido!")]
    [JsonProperty("celular")]
    public string Celular { get; set; }

    [EmailAddress(ErrorMessage = "E-mail inválido!")]
    [JsonProperty("email")]
    public string Email { get; set; }

    public Guid Id { get; set; }

    public Guid UsuarioId { get; set; }
  }
}