using Agenda.Dominio.Erros;

namespace Agenda.Dominio.Servicos
{
  public interface Resposta<T>
  {
    T Resultado { get; set; }

    Erro Erro { get; set; }

    bool TemErro();
  }
}