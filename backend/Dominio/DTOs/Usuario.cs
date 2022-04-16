using System.Collections.Generic;

namespace Agenda.Dominio.DTOs
{
    /// <summary>
    /// Objeto utilizado para retornar um usu�rio
    /// </summary>
    public struct Usuario
    {
        /// <summary>
        /// Id do usu�rio
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Nome do usu�rio
        /// </summary>
        /// <example>John Doe</example>
        public string Nome { get; set; }

        /// <summary>
        /// Login do usu�rio
        /// </summary>
        /// <example>jdoe</example>
        public string Login { get; set; }

        /// <summary>
        /// Token de autoriza��o do usu�rio
        /// </summary>
        /// <example>604b7403-8b0d-4e2c-9ba0-90f049568737</example>
        public string Token { get; set; }

        /// <summary>
        /// Contatos do usu�rio
        /// </summary>
        /// <example></example>
        public IEnumerable<Contato> Contatos { get; set; }
    }
}