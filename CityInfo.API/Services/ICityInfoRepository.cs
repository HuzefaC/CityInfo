using CityInfo.API.Entities;

namespace CityInfo.API.Services;

public interface ICityInfoRepository
{
    Task<IEnumerable<City>> GetCitiesAsync();
    Task<IEnumerable<City>> GetCitiesAsync(string? name, string? searchValue);
    Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest);
    Task<IEnumerable<PointOfInterest>> GetPointsOfInterestAsync(int cityId);
    Task<PointOfInterest?> GetPointOfInterestAsync(int cityId, int pointOfInterestId);
    Task<bool> CityExistsAsync(int cityId);
    Task AddPointOfInterestAsync(PointOfInterest pointOfInterest, int cityId);
    Task<bool> SaveChangesAsync();
    void DeletePointOfInterest(PointOfInterest pointOfInterestAsync);
}