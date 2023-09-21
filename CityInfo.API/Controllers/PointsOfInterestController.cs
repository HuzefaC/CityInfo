using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers;

[Route("api/cities/{cityId}/pointsofinterest")]
public class PointsOfInterestController : ControllerBase
{

    [HttpGet]
    public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
    {
        var city = GetCity(cityId);

        if (city == null)
        {
            return NotFound();
        }
        
        return Ok(city.PointOfInterest);
    }

    private static CityDto? GetCity(int cityId)
    {
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(city => city.Id == cityId);
        return city;
    }

    [HttpGet("{pointOfInterestId}")]
    public ActionResult<PointOfInterestDto> GetPointOfInterest(int cityId, int pointOfInterestId)
    {
        var city = GetCity(cityId);
        if (city == null)
        {
            return NotFound();
        }

        var pointOfInterest = city.PointOfInterest.FirstOrDefault(pointOfInterest => pointOfInterest.Id == pointOfInterestId);

        if (pointOfInterest == null)
        {
            return NotFound();
        }
        
        return Ok(pointOfInterest);
    }
   
}