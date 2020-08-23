using System.Collections.Generic;

namespace Agenda.Dominio.Modelos
{
  public class Usuario : ModeloBase
  {
    public string Login { get; protected set; }

    public string Senha { get; protected set; }

    public string Token { get; protected set; }

    public string Nome { get; protected set; }

    public IEnumerable<Contato> Contatos { get; protected set; }

    protected Usuario()
    {
    }

    public Usuario(string login, string senha, string nome)
    {
      Login = login;
      Senha = senha;
      Nome = nome;
      Contatos = new List<Contato>();
    }

    public void AdicionarContato(Contato contato) => (Contatos as List<Contato>).Add(contato);

    public void AdicionarToken(string token) => Token = token;
  }
}