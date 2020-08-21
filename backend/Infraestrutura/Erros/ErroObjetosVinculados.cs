using Agenda.Dominio.Erros;

namespace Agenda.Infraestrutura.Erros
{
  public class ErroObjetosVinculados : Erro
  {
    public string Mensagem { get; set; }

    public int StatusCode { get; set; }

    public ErroObjetosVinculados(string objetoA, string objetosB)
    {
      this.Mensagem = $"Não é possível deletar {objetoA} contendo {objetosB} vinculados.";
      this.StatusCode = 400;
    }
  }
}