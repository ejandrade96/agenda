using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Agenda.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace Agenda.Tests.Integracao
{
  public abstract class IntegracaoBase
  {
    protected HttpClient _api;

    public IntegracaoBase()
    {
      var appFactory = new WebApplicationFactory<Startup>()
           .WithWebHostBuilder(builder =>
           {
             builder.ConfigureServices(services =>
             {
             });
           });

      _api = appFactory.CreateClient();
    }

    protected HttpContent ConverterParaJSON<T>(T valor) => new StringContent(JsonConvert.SerializeObject(valor), Encoding.UTF8, "application/json");

    protected T Converter<T>(string json) => JsonConvert.DeserializeObject<T>(json);

    protected FormUrlEncodedContent CodificarUrl(IEnumerable<KeyValuePair<string, string>> valor) => new FormUrlEncodedContent(valor);
  }
}