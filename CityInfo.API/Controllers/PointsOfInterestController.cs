using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers;

[ApiController]
[Route("api/cities/{cityId}/pointsofinterest")]
public class PointsOfInterestController : ControllerBase
{
    private readonly ILogger<PointsOfInterestController> _logger;
    private readonly IMailService _mailService;
    private readonly CitiesDataStore _citiesDataStore;

    public PointsOfInterestController(ILogger<PointsOfInterestController> logger, IMailService mailService, CitiesDataStore citiesDataStore)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        _citiesDataStore = citiesDataStore ?? throw new ArgumentNullException(nameof(citiesDataStore));
    }

    [HttpGet]
    public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
    {
        try
        {
            var city = GetCity(cityId);

            if (city == null)
            {
                _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
                return NotFound();
            }

            return Ok(city.PointOfInterest);
        }
        catch (Exception e)
        {
            _logger.LogCritical($"Exception while getting points of interest for city with id {cityId}", e);
            return StatusCode(500, "A problem occured while handling your request");
        }
    }

    private CityDto? GetCity(int cityId)
    {
        var city = _citiesDataStore.Cities.FirstOrDefault(city => city.Id == cityId);
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

        var maxPointOfInterestId = _citiesDataStore.Cities.SelectMany(c => c.PointOfInterest).Max(p => p.Id);

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

        var pointOfInterest =
            city.PointOfInterest.FirstOrDefault(pointOfInterest => pointOfInterest.Id == pointOfInterestId);
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

        var pointOfInterestFromStore =
            city.PointOfInterest.FirstOrDefault(pointOfInterest => pointOfInterest.Id == pointOfInterestId);
        if (pointOfInterestFromStore == null) return NotFound();

        var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
        {
            Name = pointOfInterestFromStore.Name,
            Description = pointOfInterestFromStore.Description
        };

        patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!TryValidateModel(pointOfInterestToPatch)) return BadRequest(ModelState);

        pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;
        pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;

        return NoContent();
    }

    [HttpDelete("{pointOfInterestId}")]
    public ActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
    {
        var city = GetCity(cityId);
        if (city == null) return NotFound();

        var pointOfInterestFromStore =
            city.PointOfInterest.FirstOrDefault(pointOfInterest => pointOfInterest.Id == pointOfInterestId);
        if (pointOfInterestFromStore == null) return NotFound();

        city.PointOfInterest.Remove(pointOfInterestFromStore);
        _mailService.Send("Point of interest deleted",
            $"Point of interest {pointOfInterestFromStore.Name} with id {pointOfInterestId} was deleted");
        return NoContent();
    }
}