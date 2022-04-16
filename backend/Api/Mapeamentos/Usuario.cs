using AutoMapper;
using DTOs = Agenda.Dominio.DTOs;
using Modelos = Agenda.Dominio.Modelos;

namespace Agenda.Api.Mapeamentos
{
    public class Usuario : Profile
    {
        public Usuario()
        {
            CreateMap<Modelos.Usuario, DTOs.Usuario>()
            .ReverseMap();
        }
    }
}