using AutoMapper;
using DTOs = Agenda.Dominio.DTOs;
using Modelos = Agenda.Dominio.Modelos;

namespace Agenda.Api.Mapeamentos
{
  public class Contato : Profile
  {
    public Contato()
    {
      CreateMap<DTOs.Contato, Modelos.Contato>().ReverseMap();
    }
  }
}