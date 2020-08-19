using Interface = Agenda.Dominio.Repositorios;
using Implementacao = Agenda.Infraestrutura.Repositorios;

using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Api.Extensoes
{
  public static class Repositorios
  {
    public static void AddRepositorios(this IServiceCollection services)
    {
      services.AddTransient<Interface.Contatos, Implementacao.Contatos>();
      services.AddTransient<Interface.Usuarios, Implementacao.Usuarios>();
    }
  }
}
