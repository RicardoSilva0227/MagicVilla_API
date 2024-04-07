using AutoMapper;
using MagicVillaWebProject.Models.Dto;

namespace MagicVillaWebProject
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            #region Villa
            CreateMap<VillaDto,VillaCreateDTO>().ReverseMap();
            CreateMap<VillaDto,VillaUpdateDTO>().ReverseMap();
            #endregion

            #region VillaNumber
            CreateMap<VillaNumberDTO, VillaNumberCreateDTO>().ReverseMap();
            CreateMap<VillaNumberDTO, VillaNumberUpdateDTO>().ReverseMap();
            #endregion
        }
    }
}
