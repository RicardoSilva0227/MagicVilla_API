using AutoMapper;
using MagicVillaAPICourse.Models;
using MagicVillaAPICourse.Models.Dto;

namespace MagicVillaAPICourse
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            #region Villa
            CreateMap<Villa,VillaDto>();
            CreateMap<VillaDto,Villa>();
            CreateMap<Villa,VillaCreateDTO>().ReverseMap();
            CreateMap<Villa,VillaUpdateDTO>().ReverseMap();
            #endregion

            #region VillaNumber
            CreateMap<VillaNumber, VillaNumberDTO>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberCreateDTO>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberUpdateDTO>().ReverseMap();
            #endregion
        }
    }
}
