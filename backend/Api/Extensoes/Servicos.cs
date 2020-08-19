using Interface = Agenda.Dominio.Servicos;
using Implementacao = Agenda.Servicos;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Api.Extensoes
{
  public static class Servicos
  {
    public static void AddServicos(this IServiceCollection services)
    {
      services.AddTransient<Interface.Contato, Implementacao.Contato>();
      services.AddTransient<Interface.Usuario, Implementacao.Usuario>();
    }
  }
}
