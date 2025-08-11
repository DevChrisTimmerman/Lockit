using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Lockit.Data.Models
{
	public class LockitDbContextFactory : IDesignTimeDbContextFactory<LockitDbContext>
	{
		public LockitDbContext CreateDbContext(string[] args)
		{
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			var optionsBuilder = new DbContextOptionsBuilder<LockitDbContext>();
			optionsBuilder.UseSqlServer(configuration.GetConnectionString("LockitConnection"));

			return new LockitDbContext(optionsBuilder.Options);
		}
	}
}
