using Agenda.Dominio.Erros;

namespace Agenda.Infraestrutura.Erros
{
  public class ErroObjetoNaoEncontrado : Erro
  {
    public string Mensagem { get; set; }
    public int StatusCode { get; set; }

    public ErroObjetoNaoEncontrado(string objeto)
    {
      this.Mensagem = $"{objeto} não encontrado(a)!";
      this.StatusCode = 404;
    }
  }
}