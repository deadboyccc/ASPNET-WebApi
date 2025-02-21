using AutoMapper;
using BGwalks.API.Models.Domain;
using BGwalks.API.Models.DTO;

namespace BGwalks.API.Mapings;
class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        // CreateMap <source, destination>() 

        // region resource
        CreateMap<RegionDomain, RegionGetDto>().ReverseMap();
        CreateMap<RegionDomain, regionCreateDto>().ReverseMap();
        CreateMap<RegionDomain, RegionUpdateDto>().ReverseMap();

        // walk resource
        CreateMap<WalkDomain, WalkCreateDto>().ReverseMap();
        CreateMap<WalkDomain, WalkGetDto>().ReverseMap();
        CreateMap<WalkDomain, WalkUpdateDto>().ReverseMap();

        // difficulty resource
        CreateMap<DifficultyDomain, DifficultyGetDto>().ReverseMap();

    }

}