using Lockit.Models;
using Microsoft.EntityFrameworkCore;

namespace Lockit.Data.Models;

public class LockitDbContext : DbContext
{
	public DbSet<Locker> Lockers { get; set; }
	public DbSet<Student> Students { get; set; }
	public DbSet<Location> Locations { get; set; }

	public LockitDbContext(DbContextOptions<LockitDbContext> options) : base(options)
	{
	}
}
