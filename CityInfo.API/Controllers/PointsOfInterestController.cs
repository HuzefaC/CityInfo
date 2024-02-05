using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers;

[ApiController]
[Route("api/cities/{cityId}/pointsofinterest")]
public class PointsOfInterestController : ControllerBase
{
    private readonly ICityInfoRepository _cityInfoRepository;
    private readonly ILogger<PointsOfInterestController> _logger;
    private readonly IMailService _mailService;
    private readonly IMapper _mapper;

    public PointsOfInterestController(ILogger<PointsOfInterestController> logger, IMailService mailService,
        ICityInfoRepository cityInfoRepository, IMapper mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsOfInterest(int cityId)
    {
        var cityExistsAsync = await _cityInfoRepository.CityExistsAsync(cityId);

        if (!cityExistsAsync) return NotFound();

        var pointsOfInterestAsync = await _cityInfoRepository.GetPointsOfInterestAsync(cityId);

        return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterestAsync));
    }

    [HttpGet("{pointOfInterestId}", Name = "GetPointOfInterest")]
    public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(int cityId, int pointOfInterestId)
    {
        var cityExistsAsync = await _cityInfoRepository.CityExistsAsync(cityId);

        if (!cityExistsAsync) return NotFound();

        var pointOfInterestAsync = await _cityInfoRepository.GetPointOfInterestAsync(cityId, pointOfInterestId);

        if (pointOfInterestAsync == null) return NotFound();

        return Ok(_mapper.Map<PointOfInterestDto>(pointOfInterestAsync));
    }

    [HttpPost]
    public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(int cityId,
        PointOfInterestForCreationDto dto)
    {
        var cityExistsAsync = await _cityInfoRepository.CityExistsAsync(cityId);

        if (!cityExistsAsync) return NotFound();

        var pointOfInterest = _mapper.Map<PointOfInterest>(dto);

        await _cityInfoRepository.AddPointOfInterestAsync(pointOfInterest!, cityId);
        await _cityInfoRepository.SaveChangesAsync();
        var pointOfInterestDto = _mapper.Map<PointOfInterestDto>(pointOfInterest);

        return CreatedAtRoute("GetPointOfInterest", new
        {
            cityId,
            pointOfInterestId = pointOfInterestDto!.Id
        }, pointOfInterestDto);
    }

    [HttpPut("{pointOfInterestId}")]
    public async Task<ActionResult> UpdatePointOfInterest(int cityId, int pointOfInterestId,
        PointOfInterestForUpdateDto dto)
    {
        var cityExistsAsync = await _cityInfoRepository.CityExistsAsync(cityId);

        if (!cityExistsAsync) return NotFound();
        
        var pointOfInterestAsync = await _cityInfoRepository.GetPointOfInterestAsync(cityId, pointOfInterestId);
        if (pointOfInterestAsync == null)
        {
            return NotFound();
        }

        _mapper.Map(dto, pointOfInterestAsync);
        
        await _cityInfoRepository.SaveChangesAsync();
    
        return NoContent();
    }
    
    [HttpPatch("{pointOfInterestId}")]
    public async Task<ActionResult> PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId,
        JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
    {
        var cityExistsAsync = await _cityInfoRepository.CityExistsAsync(cityId);

        if (!cityExistsAsync) return NotFound();
    
        var pointOfInterestAsync = await _cityInfoRepository.GetPointOfInterestAsync(cityId, pointOfInterestId);
        if (pointOfInterestAsync == null)
        {
            return NotFound();
        }

        var pointOfInterestForUpdateDto = _mapper.Map<PointOfInterestForUpdateDto>(pointOfInterestAsync);
    
        patchDocument.ApplyTo(pointOfInterestForUpdateDto!, ModelState);
    
        if (!ModelState.IsValid) return BadRequest(ModelState);
    
        if (!TryValidateModel(pointOfInterestForUpdateDto!)) return BadRequest(ModelState);

        _mapper.Map(pointOfInterestForUpdateDto, pointOfInterestAsync);

        await _cityInfoRepository.SaveChangesAsync();
    
        return NoContent();
    }
    
    [HttpDelete("{pointOfInterestId}")]
    public async Task<ActionResult> DeletePointOfInterest(int cityId, int pointOfInterestId)
    {
        var cityExistsAsync = await _cityInfoRepository.CityExistsAsync(cityId);

        if (!cityExistsAsync) return NotFound();
    
        var pointOfInterestAsync = await _cityInfoRepository.GetPointOfInterestAsync(cityId, pointOfInterestId);
        if (pointOfInterestAsync == null)
        {
            return NotFound();
        }

        _cityInfoRepository.DeletePointOfInterest(pointOfInterestAsync);

        await _cityInfoRepository.SaveChangesAsync();

        _mailService.Send("Point of interest deleted",
            $"Point of interest {pointOfInterestAsync.Name} with id {pointOfInterestId} was deleted");
        
        return NoContent();
    }
}