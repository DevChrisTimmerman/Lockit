using System.Net;
using Lockit.Models;

namespace Lockit.Web.Services;

public class LocationService
{
	private readonly HttpClient _httpClient;
	public LocationService(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	public async Task<List<Location>> GetAllLocationsAsync()
	{
		var response = await _httpClient.GetAsync("api/locations");
		switch (response.StatusCode)
		{
			case HttpStatusCode.OK:
				return await response.Content.ReadFromJsonAsync<List<Location>>();
			default:
				throw new Exception("An error occurred while fetching all locations.");
		}
	}

	public async Task<Location?> GetLocationByIdAsync(int locationId)
	{
		var response = await _httpClient.GetAsync($"api/locations/{locationId}");
		switch (response.StatusCode)
		{
			case HttpStatusCode.OK:
				return await response.Content.ReadFromJsonAsync<Location>();
			default:
				throw new Exception("An error occurred while fetching the location.");
		}
	}

	public async Task<Location?> AddLocationAsync(Location location)
	{
		var response = await _httpClient.PostAsJsonAsync("api/locations", location);
		switch (response.StatusCode)
		{
			case HttpStatusCode.Created:
				return await response.Content.ReadFromJsonAsync<Location>();
			default:
				throw new Exception("An error occurred while adding a location.");
		}
	}

	public async Task<Location?> UpdateLocationAsync(Location location)
	{
		var response = await _httpClient.PutAsJsonAsync($"api/locations/{location.ID}", location);
		switch (response.StatusCode)
		{
			case HttpStatusCode.OK:
				return await response.Content.ReadFromJsonAsync<Location>();
			default:
				throw new Exception("An error occurred while updating the location.");
		}
	}

	public async Task<Location?> DeleteLocationAsync(int locationId)
	{
		var response = await _httpClient.DeleteAsync($"api/locations/{locationId}");
		switch (response.StatusCode)
		{
			case HttpStatusCode.OK:
				return await response.Content.ReadFromJsonAsync<Location>();
			default:
				throw new Exception("An error occurred while deleting the location.");
		}
	}

}
