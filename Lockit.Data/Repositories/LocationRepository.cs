using Lockit.Data.Models;
using Lockit.Models;
using Microsoft.EntityFrameworkCore;

namespace Lockit.Data.Repositories;

public class LocationRepository : ILocationRepository
{
	private readonly LockitDbContext _context;

	public LocationRepository(LockitDbContext context)
	{
		_context = context;
	}

	public async Task<List<Location>> GetAllLocationsAsync()
	{
		return await _context.Locations.ToListAsync();
	}

	public async Task<Location> AddLocationAsync(Location location)
	{
		_context.Locations.Add(location);
		await _context.SaveChangesAsync();
		return location;
	}

	public async Task<Location?> GetLocationByIdAsync(int locationId)
	{
		return await _context.Locations.FindAsync(locationId);
	}

	public async Task<Location> UpdateLocationAsync(Location location)
	{
		_context.Locations.Update(location);
		await _context.SaveChangesAsync();
		return location;
	}

	public async Task<Location> DeleteLocationAsync(int locationId)
	{
		var location = await _context.Locations.FindAsync(locationId);
		if (location != null)
		{
			_context.Locations.Remove(location);
			await _context.SaveChangesAsync();
			return location;
		}

		return null;
	}
}
