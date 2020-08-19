using System;
using System.Collections.Generic;

namespace Agenda.Dominio.Modelos
{
  public class Usuario
  {
    public Guid Id { get; set; }

    public string Login { get; protected set; }

    public string Senha { get; protected set; }

    public IEnumerable<Contato> Contatos { get; protected set; }

    protected Usuario()
    {
    }

    public Usuario(string login, string senha)
    {
      Login = login;
      Senha = senha;
    }
  }
}