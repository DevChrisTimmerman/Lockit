using Lockit.Models;

namespace Lockit.Data.Repositories;

public interface ILocationRepository
{
	Task<List<Location>> GetAllLocationsAsync();
	Task<Location> AddLocationAsync(Location location);
	Task<Location?> GetLocationByIdAsync(int locationId);
	Task<Location> UpdateLocationAsync(Location location);
	Task<Location> DeleteLocationAsync(int locationId);
}
