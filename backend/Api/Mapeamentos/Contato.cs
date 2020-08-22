using AutoMapper;
using DTOs = Agenda.Dominio.DTOs;
using Modelos = Agenda.Dominio.Modelos;

namespace Agenda.Api.Mapeamentos
{
  public class Contato : Profile
  {
    public Contato()
    {
      CreateMap<Modelos.Contato, DTOs.Contato>()
      .ForMember(dest => dest.UsuarioId, opt => opt.MapFrom(src => src.Usuario.Id))
      .ReverseMap();
    }
  }
}