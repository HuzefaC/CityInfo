using CityInfo.API.DbContexts;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Services;

public class CityInfoRepository : ICityInfoRepository
{
    private readonly CityInfoContext _context;

    public CityInfoRepository(CityInfoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<City>> GetCitiesAsync()
    {
        return await _context.Cities.Include(c => c.PointsOfInterest).OrderBy(c => c.Name).ToListAsync();
    }

    public async Task<IEnumerable<City>> GetCitiesAsync(string? name, string? searchValue)
    {
        if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(searchValue))
        {
            return await GetCitiesAsync();
        }

        var contextCities = _context.Cities as IQueryable<City>;

        if (!string.IsNullOrWhiteSpace(name))
        {
            name = name.Trim();
            contextCities = contextCities.Where(city => city.Name == name);
        }

        if (!string.IsNullOrWhiteSpace(searchValue))
        {
            searchValue = searchValue.Trim();
            contextCities = contextCities.Where(a =>
                a.Name.Contains(searchValue) || (a.Description != null && a.Description.Contains(searchValue) ||
                                                 a.PointsOfInterest.Any(poi =>
                                                     poi.Name.Contains(searchValue) ||
                                                     poi.Description.Contains(searchValue))));
        }

        return await contextCities
            .Include(c => c.PointsOfInterest)
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    public async Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest)
    {
        if (includePointsOfInterest)
            return await _context.Cities.Include(c => c.PointsOfInterest).Where(c => c.Id == cityId)
                .FirstOrDefaultAsync();

        return await _context.Cities.Where(c => c.Id == cityId)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestAsync(int cityId)
    {
        return await _context.PointOfInterests.Where(p => p.CityId == cityId).OrderBy(p => p.Name).ToListAsync();
    }

    public async Task<PointOfInterest?> GetPointOfInterestAsync(int cityId, int pointOfInterestId)
    {
        return await _context.PointOfInterests.Where(p => p.CityId == cityId && p.Id == pointOfInterestId)
            .FirstOrDefaultAsync();
    }

    public Task<bool> CityExistsAsync(int cityId)
    {
        return _context.Cities.AnyAsync(c => c.Id == cityId);
    }

    public async Task AddPointOfInterestAsync(PointOfInterest pointOfInterest, int cityId)
    {
        var cityAsync = await GetCityAsync(cityId, true);
        if (cityAsync != null) cityAsync.PointsOfInterest.Add(pointOfInterest);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() >= 0;
    }

    public void DeletePointOfInterest(PointOfInterest pointOfInterestAsync)
    {
        _context.PointOfInterests.Remove(pointOfInterestAsync);
    }
}