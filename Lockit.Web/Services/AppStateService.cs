using System.Net.NetworkInformation;

namespace Lockit.Web.Services;

public class AppStateService
{
	private readonly HttpClient _httpClient;

	public AppStateService(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	//public bool IsLocationSetupComplete { get; private set; }

	//public event Action? OnStateChange;

	//public void MarkLocationSetupComplete()
	//{
	//	IsLocationSetupComplete = true;
	//	NotifyStateChanged();
	//}

	//private void NotifyStateChanged() => OnStateChange?.Invoke();
}
