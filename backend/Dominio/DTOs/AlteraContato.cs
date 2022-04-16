namespace Agenda.Dominio.DTOs
{
    /// <summary>
    /// Objeto utilizado para atualizar um contato
    /// </summary>
    public class AlteraContato : NovoContato
    {
        [System.Text.Json.Serialization.JsonIgnore]
        public int Id { get; set; }
    }
}
