using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Contextos = Agenda.Infraestrutura.Contextos;

namespace Agenda.Api
{
  public class Startup
  {
    private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddCors(options =>
        {
          options.AddPolicy(name: MyAllowSpecificOrigins,
                            builder =>
                            {
                              builder.WithOrigins("http://localhost:3000")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                            });
        });

      services.AddControllers();

      services.AddDbContext<Contextos.MyContext>(options => options.UseSqlite(Configuration["ConexaoSqlite:SqliteConnectionString"]));

      services.AddAutoMapper(typeof(Mapeamentos.Contato));
      services.AddTransient<Dominio.Servicos.Contato, Servicos.Contato>();
      services.AddTransient<Dominio.Repositorios.Contatos, Infraestrutura.Repositorios.Contatos>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseCors(MyAllowSpecificOrigins);

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
