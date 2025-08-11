using System.Net;
using Lockit.Models;

namespace Lockit.Web.Services;

public class LockerService
{
	private readonly HttpClient _httpClient;
	public LockerService(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	public async Task<List<Locker>> GetAllLockersAsync()
	{
		var response = await _httpClient.GetAsync("api/lockers");
		switch (response.StatusCode)
		{
			case HttpStatusCode.OK:
				return await response.Content.ReadFromJsonAsync<List<Locker>>();
			default:
				throw new Exception("An error occurred while fetching all lockers.");
		}
	}

	public async Task<Locker?> GetLockerByIdAsync(int lockerId)
	{
		var response = await _httpClient.GetAsync($"api/lockers/{lockerId}");
		switch (response.StatusCode)
		{
			case HttpStatusCode.OK:
				return await response.Content.ReadFromJsonAsync<Locker>();
			default:
				throw new Exception("An error occurred while fetching the locker.");
		}
	}

	public async Task<Locker?> GetLockerByUserIdAsync(int userId)
	{
		var response = await _httpClient.GetAsync($"api/lockers/user/{userId}");
		switch (response.StatusCode)
		{
			case HttpStatusCode.OK:
				return await response.Content.ReadFromJsonAsync<Locker>();
			default:
				throw new Exception("An error occurred while fetching the locker by user ID.");
		}
	}

	public async Task<Locker?> AddLockerAsync(Locker locker)
	{
		var response = await _httpClient.PostAsJsonAsync("api/lockers", locker);
		switch (response.StatusCode)
		{
			case HttpStatusCode.Created:
				return await response.Content.ReadFromJsonAsync<Locker>();
			default:
				throw new Exception("An error occurred while adding a locker.");
		}
	}

	public async Task<Locker?> UpdateLockerAsync(Locker locker)
	{
		var response = await _httpClient.PutAsJsonAsync($"api/lockers/{locker.ID}", locker);
		switch (response.StatusCode)
		{
			case HttpStatusCode.OK:
				return await response.Content.ReadFromJsonAsync<Locker>();
			default:
				throw new Exception("An error occurred while updating the locker.");
		}
	}

	public async Task<Locker> DeleteLockerAsync(int lockerId)
	{
		var response = await _httpClient.DeleteAsync($"api/lockers/{lockerId}");
		switch (response.StatusCode)
		{
			case HttpStatusCode.OK:
				return await response.Content.ReadFromJsonAsync<Locker>();
			default:
				throw new Exception("An error occurred while deleting the locker.");
		}
	}
}
