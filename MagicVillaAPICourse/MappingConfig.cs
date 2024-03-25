using AutoMapper;
using MagicVillaAPICourse.Models;
using MagicVillaAPICourse.Models.Dto;

namespace MagicVillaAPICourse
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        { 
            CreateMap<Villa,VillaDto>();
            CreateMap<VillaDto,Villa>();
            CreateMap<Villa,VillaCreateDTO>().ReverseMap();
            CreateMap<Villa,VillaUpdateDTO>().ReverseMap();
        }
    }
}
