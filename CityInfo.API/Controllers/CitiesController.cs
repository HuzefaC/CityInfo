using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers;

[ApiController]
[Route("api/cities")]
public class CitiesController : ControllerBase
{
    private readonly ICityInfoRepository _cityInfoRepository;
    private readonly IMapper _mapper;

    public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
    {
        _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CityDto>>> GetCities([FromQuery] string? name, [FromQuery] string? searchValue)
    {
        var cities = await _cityInfoRepository.GetCitiesAsync(name, searchValue);
        return Ok(_mapper.Map<IEnumerable<CityDto>>(cities));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCity(int id)
    {
        var city = await _cityInfoRepository.GetCityAsync(id, true);

        if (city == null) return NotFound();

        return Ok(_mapper.Map<CityDto>(city));
    }
}