using System.ComponentModel.DataAnnotations;

namespace Lockit.Models;
public class Location
{
	[Key]
	public int ID { get; set; }
	public string Name { get; set; }
	public string? Prefix { get; set; }
	public string? Description { get; set; }

	[Range(1, int.MaxValue, ErrorMessage = "Locker count must be greater than 0")]
	public int LockerCount { get; set; }

	[Range(1, int.MaxValue, ErrorMessage = "Lockers per column must be greater than 0")]
	public int LockersPerColumn { get; set; }
}
