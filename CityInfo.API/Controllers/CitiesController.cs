using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;
using static CityInfo.API.CitiesDataStore;

namespace CityInfo.API.Controllers;

[ApiController]
[Route("api/cities")]
public class CitiesController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerator<CityDto>> GetCities()
    {
        return Ok(Current.Cities);
    }

    [HttpGet("{id:int}")]
    public ActionResult<CityDto> GetCity(int id)
    {
        var city = Current.Cities.Find(city => city.Id == id);

        if (city == null) return NotFound();

        return Ok(city);
    }
}