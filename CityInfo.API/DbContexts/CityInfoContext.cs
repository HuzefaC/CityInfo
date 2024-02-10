using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DbContexts;

public class CityInfoContext : DbContext
{
    public DbSet<City> Cities { get; set; } = null!;

    public DbSet<PointOfInterest> PointOfInterests { get; set; } = null!;

    public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
    {
    }
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlite("connectionString");
    //     base.OnConfiguring(optionsBuilder);
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>().HasData(new City("London")
            {
                Id = 1,
                Description = "In England"
            },
            new City("Madrid")
            {
                Id = 2,

                Description = "In Spain"
            },
            new City("Munich")
            {
                Id = 3,
                Description = "In Germany"
            },
            new City("Paris")
            {
                Id = 4,
                Description = "In France"
            },
            new City("Milan")
            {
                Id = 5,
                Description = "In Italy"
            });

        modelBuilder.Entity<PointOfInterest>().HasData(
            new PointOfInterest("The British Museum", "A treasure trove of art and antiquities from around the world.")
            {
                Id = 1,
                CityId = 1,
            },
            new PointOfInterest("Buckingham Palace", "The official residence of the British monarch.")
            {
                Id = 2,
                CityId = 1,
            }, new PointOfInterest("Prado Museum",
                "One of the world's greatest art galleries, featuring European art from the 12th to early 20th century.")
            {
                Id = 3,
                CityId = 2,
            },
            new PointOfInterest("Santiago Bernabéu Stadium",
                "Home to Real Madrid, one of the world's most famous football clubs.")
            {
                Id = 4,
                CityId = 2,
            }, new PointOfInterest("Neuschwanstein Castle", "A fairy-tale castle in the Bavarian Alps.")
            {
                Id = 5,
                CityId = 3,
            },
            new PointOfInterest("Oktoberfest", "The world's largest beer festival, held annually.")
            {
                Id = 6,
                CityId = 3,
            }, new PointOfInterest("Eiffel Tower", "The iconic iron tower, a symbol of Paris.")
            {
                Id = 7,
                CityId = 4,
            },
            new PointOfInterest("Notre-Dame Cathedral", "A masterpiece of French Gothic architecture.")
            {
                Id = 8,
                CityId = 4,
            }, new PointOfInterest("Duomo di Milano",
                "One of the largest cathedrals in the world, known for its intricate Gothic architecture.")
            {
                Id = 9,
                CityId = 5,
            },
            new PointOfInterest("La Scala Opera House", "One of the world's most famous opera houses.")
            {
                Id = 10,
                CityId = 5,
            });
        base.OnModelCreating(modelBuilder);
    }
}