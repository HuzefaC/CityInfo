using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models;

public class PointOfInterestForUpdateDto
{
    [Required(ErrorMessage = "You must provide a name for point of interest")]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(200, ErrorMessage = "Description cannot be more than 200 characters")]
    public string? Description { get; set; }
}