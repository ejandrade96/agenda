namespace Agenda.Dominio.Fabricas
{
  public class Contato
  {
    private int _id;
    private string _nome;
    private string _celular;
    private string _telefone;
    private string _email;
    private Modelos.Usuario _usuario;

    public Contato Id(int id)
    {
      _id = id;
      return this;
    }

    public Contato Nome(string nome)
    {
      _nome = nome;
      return this;
    }

    public Contato Celular(string celular)
    {
      _celular = celular;
      return this;
    }

    public Contato Telefone(string telefone)
    {
      _telefone = telefone;
      return this;
    }

    public Contato Email(string email)
    {
      _email = email;
      return this;
    }

    public Contato Usuario(Modelos.Usuario usuario)
    {
      _usuario = usuario;
      return this;
    }

    public Modelos.Contato Criar()
    {
      return new Modelos.Contato
      (
        _nome,
        _celular,
        _telefone,
        _email,
        _usuario
      )
      { Id = _id };
    }
  }
}