using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models;

namespace CityInfo.API.Profiles;

public class CityProfiles : Profile
{
    public CityProfiles()
    {
        CreateMap<City, CityDto>().ReverseMap();
    }
}