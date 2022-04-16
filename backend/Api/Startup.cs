using Agenda.Api.Extensoes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Agenda John's",
                        Version = "v1",
                        Description = "API da aplicação agenda john's",
                        Contact = new OpenApiContact
                        {
                            Name = "Elton Andrade",
                            Email = "eltonfernandesandrade@hotmail.com",
                            Url = new Uri("https://github.com/ejandrade96")
                        },
                        License = new OpenApiLicense
                        {
                            Name = "OSD",
                            Url = new Uri("https://opensource.org/osd")
                        },
                        TermsOfService = new Uri("https://opensource.org/osd")
                    });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                xmlPath = Path.Combine(AppContext.BaseDirectory, "Dominio.xml");
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Contextos.MyContext dataContext)
        {
            app.UseCors(options => options.WithOrigins("http://localhost:3000").WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader());

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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("./swagger/v1/swagger.json", "AJ V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
