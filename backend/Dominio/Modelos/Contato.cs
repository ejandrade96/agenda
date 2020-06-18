using System;

namespace Agenda.Dominio.Modelos
{
  public class Contato
  {
    public Guid Id { get; set; }

    public string Nome { get; protected set; }

    public string Telefone { get; protected set; }

    public string Celular { get; protected set; }

    public string Email { get; protected set; }

    public Contato(string nome, string celular, string telefone, string email)
    {
      Nome = nome;
      Celular = celular;
      Email = email;
      Telefone = telefone;
    }
  }
}