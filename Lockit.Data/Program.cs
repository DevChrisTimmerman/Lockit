using Lockit.Data.Models;
using Lockit.Data.Repositories;
using Lockit.Models;
using Microsoft.EntityFrameworkCore;

namespace Lockit.Data;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.

		builder.Services.AddControllers();
		builder.Services.AddDbContext<LockitDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("LockitConnection")));
		// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
		builder.Services.AddOpenApi();
		builder.Services.AddTransient<ILockerRepository, LockerRepository>();
		builder.Services.AddTransient<ILocationRepository, LocationRepository>();
		builder.Services.AddTransient<IStudentRepository, StudentRepository>();

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.MapOpenApi();
			app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "Lockit.Data"));
		}

		app.UseHttpsRedirection();

		app.UseAuthorization();


		app.MapControllers();

		app.Run();
	}
}
