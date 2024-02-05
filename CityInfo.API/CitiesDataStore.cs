using CityInfo.API.Models;

namespace CityInfo.API;

public class CitiesDataStore
{
    public CitiesDataStore()
    {
        Cities = new List<CityDto>
        {
            new()
            {
                Id = 1,
                Name = "London",
                Description = "In England",
                PointsOfInterest = new List<PointOfInterestDto>
                {
                    new()
                    {
                        Id = 1,
                        Name = "The British Museum",
                        Description = "A treasure trove of art and antiquities from around the world."
                    },
                    new()
                    {
                        Id = 2,
                        Name = "Buckingham Palace",
                        Description = "The official residence of the British monarch."
                    }
                }
            },
            new()
            {
                Id = 2,
                Name = "Madrid",
                Description = "In Spain",
                PointsOfInterest = new List<PointOfInterestDto>
                {
                    new()
                    {
                        Id = 3,
                        Name = "Prado Museum",
                        Description = "One of the world's greatest art galleries, featuring European art from the 12th to early 20th century."
                    },
                    new()
                    {
                        Id = 4,
                        Name = "Santiago Bernabéu Stadium",
                        Description = "Home to Real Madrid, one of the world's most famous football clubs."
                    }
                }
            },
            new()
            {
                Id = 3,
                Name = "Munich",
                Description = "In Germany",
                PointsOfInterest = new List<PointOfInterestDto>
                {
                    new()
                    {
                        Id = 5,
                        Name = "Neuschwanstein Castle",
                        Description = "A fairy-tale castle in the Bavarian Alps."
                    },
                    new()
                    {
                        Id = 6,
                        Name = "Oktoberfest",
                        Description = "The world's largest beer festival, held annually."
                    }
                }
            },
            new()
            {
                Id = 4,
                Name = "Paris",
                Description = "In France",
                PointsOfInterest = new List<PointOfInterestDto>
                {
                    new()
                    {
                        Id = 7,
                        Name = "Eiffel Tower",
                        Description = "The iconic iron tower, a symbol of Paris."
                    },
                    new()
                    {
                        Id = 8,
                        Name = "Notre-Dame Cathedral",
                        Description = "A masterpiece of French Gothic architecture."
                    }
                }
            },
            new()
            {
                Id = 5,
                Name = "Milan",
                Description = "In Italy",
                PointsOfInterest = new List<PointOfInterestDto>
                {
                    new()
                    {
                        Id = 9,
                        Name = "Duomo di Milano",
                        Description = "One of the largest cathedrals in the world, known for its intricate Gothic architecture."
                    },
                    new()
                    {
                        Id = 10,
                        Name = "La Scala Opera House",
                        Description = "One of the world's most famous opera houses."
                    }
                }
            }
        };
    }

    public List<CityDto> Cities { get; set; }
    // public static CitiesDataStore Current { get; set; } = new();
}