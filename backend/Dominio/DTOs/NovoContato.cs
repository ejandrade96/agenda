using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Agenda.Dominio.DTOs
{
    /// <summary>
    /// Objeto utilizado para cadastrar um contato
    /// </summary>
    public class NovoContato
    {
        /// <summary>
        /// Nome do contato
        /// </summary>
        /// <example>John Doe</example>
        [JsonProperty("nome")]
        [Required(ErrorMessage = "Favor preencher o nome.", AllowEmptyStrings = false)]
        public string Nome { get; set; }

        /// <summary>
        /// Número de telefone do contato
        /// </summary>
        /// <example>11 45872534</example>
        [MinLength(10, ErrorMessage = "Telefone inválido!")]
        [MaxLength(11, ErrorMessage = "Telefone inválido!")]
        [Phone(ErrorMessage = "Telefone inválido!")]
        [JsonProperty("telefone")]
        public string Telefone { get; set; }

        /// <summary>
        /// Número de celular do contato
        /// </summary>
        /// <example>11 958742136</example>
        [MinLength(11, ErrorMessage = "Celular inválido!")]
        [MaxLength(12, ErrorMessage = "Celular inválido!")]
        [Phone(ErrorMessage = "Celular inválido!")]
        [JsonProperty("celular")]
        public string Celular { get; set; }

        /// <summary>
        /// E-mail do contato
        /// </summary>
        /// <example>johndoe@live.com</example>
        [EmailAddress(ErrorMessage = "E-mail inválido!")]
        [JsonProperty("email")]
        public string Email { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public int UsuarioId { get; set; }
    }
}
