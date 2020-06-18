namespace Agenda.Dominio.Erros
{
  public interface Erro
  {
    string Mensagem { get; set; }

    int StatusCode { get; set; }
  }
}