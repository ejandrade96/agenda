using Agenda.Api.Extensoes;
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
    private IWebHostEnvironment _appHost;

    public Startup(IConfiguration configuration, IWebHostEnvironment appHost)
    {
      Configuration = configuration;
      _appHost = appHost;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddCors();
      services.AddControllers().AddNewtonsoftJson(options =>
      options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
      );
      services.AddServicos();
      services.AddRepositorios();
      services.AddDbContext<Contextos.MyContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AgendaDBContext")));
      services.AddAutoMapper(typeof(Mapeamentos.Contato));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Contextos.MyContext dataContext)
    {
      app.UseCors(options => options.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader());

      dataContext.Database.Migrate();

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseHttpsRedirection();
      }

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
