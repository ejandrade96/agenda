namespace Agenda.Dominio.Modelos
{
  public class Contato : ModeloBase
  {
    public string Nome { get; protected set; }

    public string Telefone { get; protected set; }

    public string Celular { get; protected set; }

    public string Email { get; protected set; }

    public Usuario Usuario { get; protected set; }

    protected Contato()
    {
    }

    public Contato(string nome, string celular, string telefone, string email, Usuario usuario)
    {
      Nome = nome;
      Celular = celular;
      Email = email;
      Telefone = telefone;
      Usuario = usuario;
    }
  }
}