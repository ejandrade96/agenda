using Agenda.Dominio.Erros;

namespace Agenda.Infraestrutura.Erros
{
  public class ErroAtributoEmBranco : Erro
  {
    public string Mensagem { get; set; }

    public int StatusCode { get; set; }

    public ErroAtributoEmBranco(string atributo)
    {
      this.Mensagem = $"Erro! Campo {atributo} está em branco.";
      this.StatusCode = 400;
    }
  }
}