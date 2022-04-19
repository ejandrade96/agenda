using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Api.Extensoes
{
    public static class DependencyInjection
    {
        public static void Register(this IServiceCollection services)
        {
            services.AddServicos();
            services.AddRepositorios();
            services.AddSwagger();
        }
    }
}
