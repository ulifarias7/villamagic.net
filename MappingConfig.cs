using AutoMapper;
using Magic.curso.net.modelos;
using Magic.curso.net.modelos.Dtos;


namespace Magic.curso.net
{
    public class MappingConfig : Profile
    {
        protected MappingConfig()
        {
            CreateMap<Villa, VillaDtos>();
            CreateMap<VillaDtos, Villa>();

            CreateMap<Villa,VillaCreateDtos>().ReverseMap();
            CreateMap<Villa,VillaUpdateDtos>().ReverseMap();

             
        }
    }
}
