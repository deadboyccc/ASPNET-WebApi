using AutoMapper;
using BGwalks.API.Models.Domain;
using BGwalks.API.Models.DTO;

namespace BGwalks.API.Mapings;
class AutoMapperProfiles : Profile
{
  public AutoMapperProfiles()
  {
    // CreateMap <source, destination>() 
    CreateMap<RegionDomain, RegionDto>().ReverseMap();
    CreateMap<RegionDomain, regionCreateDto>().ReverseMap();
    CreateMap<RegionDomain, RegionUpdateDto>().ReverseMap();

  }

}