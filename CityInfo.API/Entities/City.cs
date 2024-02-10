﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.API.Entities;

public class City
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    [Required] [MaxLength(50)] public string Name { get; set; }


    [MaxLength(200)] public string? Description { get; init; }

    public ICollection<PointOfInterest> PointsOfInterest { get; set; } = new List<PointOfInterest>();

    public City(string name)
    {
        Name = name;
    }
}