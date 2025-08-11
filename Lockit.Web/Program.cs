using Azure.Core;
using Lockit.Web.Components;
using Lockit.Web.Services;

namespace Lockit.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddTransient<LockerService>();
			builder.Services.AddTransient<LocationService>();
			builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri("http://localhost:5031/") });
			// Add services to the container.
			builder.Services.AddRazorComponents()
				.AddInteractiveServerComponents();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();

			app.UseAntiforgery();

			app.MapStaticAssets();
			app.MapRazorComponents<App>()
				.AddInteractiveServerRenderMode();

			app.Run();
		}
	}
}
