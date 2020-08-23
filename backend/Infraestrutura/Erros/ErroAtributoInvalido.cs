using Agenda.Dominio.Erros;

namespace Agenda.Infraestrutura.Erros
{
  public class ErroAtributoInvalido : Erro
  {
    public string Mensagem { get; set; }

    public int StatusCode { get; set; }

    public ErroAtributoInvalido(string atributo)
    {
      this.Mensagem = $"{atributo} inválido(a)!";
      this.StatusCode = 400;
    }
  }
}