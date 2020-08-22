using Agenda.Dominio.Erros;

namespace Agenda.Infraestrutura.Erros
{
  public class ErroObjetoNaoPossuiObjetosVinculados : Erro
  {
    public string Mensagem { get; set; }

    public int StatusCode { get; set; }

    public ErroObjetoNaoPossuiObjetosVinculados(string objetoA, string objetosB)
    {
      this.Mensagem = $"Este {objetoA} não possui {objetosB}.";
      this.StatusCode = 400;
    }
  }
}