using CityInfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers;

[ApiController]
[Route("api/cities/{cityId}/pointsofinterest")]
public class PointsOfInterestController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
    {
        var city = GetCity(cityId);

        if (city == null) return NotFound();

        return Ok(city.PointOfInterest);
    }

    private static CityDto? GetCity(int cityId)
    {
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(city => city.Id == cityId);
        return city;
    }

    [HttpGet("{pointOfInterestId}", Name = "GetPointOfInterest")]
    public ActionResult<PointOfInterestDto> GetPointOfInterest(int cityId, int pointOfInterestId)
    {
        var city = GetCity(cityId);
        if (city == null) return NotFound();

        var pointOfInterest =
            city.PointOfInterest.FirstOrDefault(pointOfInterest => pointOfInterest.Id == pointOfInterestId);

        if (pointOfInterest == null) return NotFound();

        return Ok(pointOfInterest);
    }

    [HttpPost]
    public ActionResult<PointOfInterestDto> CreatePointOfInterest(int cityId, PointOfInterestForCreationDto dto)
    {
        var city = GetCity(cityId);
        if (city == null) return NotFound();

        var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(c => c.PointOfInterest).Max(p => p.Id);

        var pointOfInterestDto = new PointOfInterestDto
        {
            Id = ++maxPointOfInterestId,
            Name = dto.Name,
            Description = dto.Description
        };

        city.PointOfInterest.Add(pointOfInterestDto);

        return CreatedAtRoute("GetPointOfInterest", new
        {
            cityId,
            pointOfInterestId = pointOfInterestDto.Id
        }, pointOfInterestDto);
    }

    [HttpPut("{pointOfInterestId}")]
    public ActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId,
        PointOfInterestForUpdateDto dto)
    {
        var city = GetCity(cityId);
        if (city == null) return NotFound();

        var pointOfInterest = city.PointOfInterest.FirstOrDefault(pointOfInterest => pointOfInterest.Id == pointOfInterestId);
        if (pointOfInterest == null) return NotFound();

        pointOfInterest.Description = dto.Description;
        pointOfInterest.Name = dto.Name;

        return NoContent();
    }

    [HttpPatch("{pointOfInterestId}")]
    public ActionResult PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId,
        JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
    {
        var city = GetCity(cityId);
        if (city == null) return NotFound();

        var pointOfInterestFromStore = city.PointOfInterest.FirstOrDefault(pointOfInterest => pointOfInterest.Id == pointOfInterestId);
        if (pointOfInterestFromStore == null) return NotFound();

        var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
        {
            Name = pointOfInterestFromStore.Name,
            Description = pointOfInterestFromStore.Description
        };
        
        patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!TryValidateModel(pointOfInterestToPatch))
        {
            return BadRequest(ModelState);
        }
        
        pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;
        pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;

        return NoContent();
    }

    [HttpDelete("{pointOfInterestId}")]
    public ActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
    {
        var city = GetCity(cityId);
        if (city == null) return NotFound();

        var pointOfInterestFromStore = city.PointOfInterest.FirstOrDefault(pointOfInterest => pointOfInterest.Id == pointOfInterestId);
        if (pointOfInterestFromStore == null) return NotFound();

        city.PointOfInterest.Remove(pointOfInterestFromStore);
        return NoContent();
    }
}