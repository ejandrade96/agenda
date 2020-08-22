using Agenda.Dominio.Erros;

namespace Agenda.Infraestrutura.Erros
{
  public class ErroObjetoPossuiObjetosVinculados : Erro
  {
    public string Mensagem { get; set; }

    public int StatusCode { get; set; }

    public ErroObjetoPossuiObjetosVinculados(string objetoA, string objetosB)
    {
      this.Mensagem = $"Erro! Este {objetoA} possui {objetosB} vinculados.";
      this.StatusCode = 400;
    }
  }
}