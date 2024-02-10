using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.API.Entities;

public class PointOfInterest
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    [Required] [MaxLength(50)] public string Name { get; } = null!;

    [ForeignKey("CityId")] public City? City { get; set; }

    public int CityId { get; init; }
    
    [MaxLength(200)]
    public string Description { get; init; } = null!;

    public PointOfInterest(string name, string description)
    {
        Name = name;
        Description = description;
    }

    private PointOfInterest()
    {
    }
}