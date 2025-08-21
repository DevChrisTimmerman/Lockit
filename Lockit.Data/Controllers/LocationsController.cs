using Lockit.Data.Repositories;
using Lockit.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lockit.Data.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocationsController : Controller
{
	private readonly ILocationRepository _locationRepository;

	public LocationsController(ILocationRepository locationRepository)
	{
		_locationRepository = locationRepository;
	}

	[HttpGet]
	public async Task<IActionResult> GetAllLocations()
	{
		var locations = await _locationRepository.GetAllLocationsAsync();
		return Ok(locations);
	}

	[HttpGet("{locationId}")]
	public async Task<IActionResult> GetLocationById(int locationId)
	{
		var location = await _locationRepository.GetLocationByIdAsync(locationId);
		if (location == null)
		{
			return NotFound();
		}
		return Ok(location);
	}

	[HttpPost]
	public async Task<IActionResult> AddLocation(Location location)
	{
		if (location == null)
		{
			return BadRequest("Location data is null.");
		}
		var createdLocation = await _locationRepository.AddLocationAsync(location);
		return CreatedAtAction(nameof(GetLocationById), new { locationId = createdLocation.ID }, createdLocation);
	}

	[HttpPut("{locationId}")]
	public async Task<IActionResult> UpdateLocation(int locationId, Location location)
	{
		if (location == null || location.ID != locationId)
		{
			return BadRequest("Location data is invalid.");
		}
		var updatedLocation = await _locationRepository.UpdateLocationAsync(location);
		return Ok(updatedLocation);
	}

	[HttpDelete("{locationId}")]
	public async Task<IActionResult> DeleteLocation(int locationId)
	{
		var locationToDelete = await _locationRepository.GetLocationByIdAsync(locationId);
		if (locationToDelete == null) return NotFound($"Location with id = {locationId} was not found!");
		await _locationRepository.DeleteLocationAsync(locationId);
		return Ok(locationToDelete);
	}
}
