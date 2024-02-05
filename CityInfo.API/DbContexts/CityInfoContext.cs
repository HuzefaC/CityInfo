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

        modelBuilder.Entity<PointOfInterest>().HasData(new PointOfInterest("The British Museum")
            {
                Id = 1,
                CityId = 1,
                Description = "A treasure trove of art and antiquities from around the world."
            },
            new PointOfInterest("Buckingham Palace")
            {
                Id = 2,
                CityId = 1,
                Description = "The official residence of the British monarch."
            }, new PointOfInterest("Prado Museum")
            {
                Id = 3,
                CityId = 2,
                Description =
                    "One of the world's greatest art galleries, featuring European art from the 12th to early 20th century."
            },
            new PointOfInterest("Santiago Bernabéu Stadium")
            {
                Id = 4,
                CityId = 2,
                Description = "Home to Real Madrid, one of the world's most famous football clubs."
            }, new PointOfInterest("Neuschwanstein Castle")
            {
                Id = 5,
                CityId = 3,
                Description = "A fairy-tale castle in the Bavarian Alps."
            },
            new PointOfInterest("Oktoberfest")
            {
                Id = 6,
                CityId = 3,
                Description = "The world's largest beer festival, held annually."
            }, new PointOfInterest("Eiffel Tower")
            {
                Id = 7,
                CityId = 4,
                Description = "The iconic iron tower, a symbol of Paris."
            },
            new PointOfInterest("Notre-Dame Cathedral")
            {
                Id = 8,
                CityId = 4,
                Description = "A masterpiece of French Gothic architecture."
            }, new PointOfInterest("Duomo di Milano")
            {
                Id = 9,
                CityId = 5,
                Description = "One of the largest cathedrals in the world, known for its intricate Gothic architecture."
            },
            new PointOfInterest("La Scala Opera House")
            {
                Id = 10,
                CityId = 5,
                Description = "One of the world's most famous opera houses."
            });
        base.OnModelCreating(modelBuilder);
    }
}