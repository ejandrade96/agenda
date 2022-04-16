namespace Agenda.Dominio.DTOs
{
    /// <summary>
    /// Objeto utilizado para cadastrar um usuário
    /// </summary>
    public struct NovoUsuario
    {
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
        /// Senha do usuário
        /// </summary>
        /// <example>123</example>
        public string Senha { get; set; }
    }
}
