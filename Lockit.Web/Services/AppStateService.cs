using System.Net.NetworkInformation;

namespace Lockit.Web.Services;

public class AppStateService
{
	private readonly HttpClient _httpClient;

	public AppStateService(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	// Lockit state properties
	public bool IsLocationSetupComplete { get; private set; }

	public event Action? OnStateChange;

	public async Task InitializeAsync()
	{
		//var response = await _httpClient.GetAsync("api/appstate");
		//if (response.IsSuccessStatusCode)
		//{
		//	var state = await response.Content.ReadFromJsonAsync<AppState>();
		//	IsLocationSetupComplete = state.IsLocationSetupComplete;
	
		//	NotifyStateChanged();
		//}
	}

	public void MarkLocationSetupComplete()
	{
		IsLocationSetupComplete = true;
		NotifyStateChanged();
	}

	private void NotifyStateChanged() => OnStateChange?.Invoke();
}
