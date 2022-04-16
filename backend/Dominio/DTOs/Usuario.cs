using System.Collections.Generic;

namespace Agenda.Dominio.DTOs
{
    /// <summary>
    /// Objeto utilizado para retornar um usuário
    /// </summary>
    public struct Usuario
    {
        /// <summary>
        /// Id do usuário
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Nome do usuário
        /// </summary>
        /// <example>John Doe</example>
        public string Nome { get; set; }

        /// <summary>
        /// Login do usuário
        /// </summary>
        /// <example>jdoe</example>
        public string Login { get; set; }

        /// <summary>
        /// Token de autorização do usuário
        /// </summary>
        /// <example>604b7403-8b0d-4e2c-9ba0-90f049568737</example>
        public string Token { get; set; }

        /// <summary>
        /// Contatos do usuário
        /// </summary>
        /// <example></example>
        public IEnumerable<Contato> Contatos { get; set; }
    }
}