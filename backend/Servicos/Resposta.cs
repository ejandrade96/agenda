using Agenda.Dominio.Erros;

namespace Agenda.Servicos
{
  public class Resposta<T> : Dominio.Servicos.Resposta<T>
  {
    public T Resultado { get; set; }

    public Erro Erro { get; set; }

    public bool TemErro() => Erro != null;
  }
}