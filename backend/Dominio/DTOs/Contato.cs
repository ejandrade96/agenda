namespace Agenda.Dominio.DTOs
{
    /// <summary>
    /// Objeto utilizado para retornar um contato
    /// </summary>
    public struct Contato
    {
        /// <summary>
        /// Id do contato
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Nome do contato
        /// </summary>
        /// <example>John Doe</example>
        public string Nome { get; set; }

        /// <summary>
        /// Número de telefone do contato
        /// </summary>
        /// <example>11 45872534</example>
        public string Telefone { get; set; }

        /// <summary>
        /// Número de celular do contato
        /// </summary>
        /// <example>11 958742136</example>
        public string Celular { get; set; }

        /// <summary>
        /// E-mail do contato
        /// </summary>
        /// <example>johndoe@live.com</example>
        public string Email { get; set; }

        /// <summary>
        /// Id do usuário
        /// </summary>
        /// <example>1</example>
        public int UsuarioId { get; set; }
    }
}