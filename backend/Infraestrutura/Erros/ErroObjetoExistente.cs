using Agenda.Dominio.Erros;

namespace Agenda.Infraestrutura.Erros
{
  public class ErroObjetoExistente : Erro
  {
    public string Mensagem { get; set; }

    public int StatusCode { get; set; }

    public ErroObjetoExistente(string objeto, string atributo)
    {
      this.Mensagem = $"{objeto} já cadastrado(a) com este {atributo}!";
      this.StatusCode = 400;
    }
  }
}