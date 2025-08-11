using System.ComponentModel.DataAnnotations;

namespace Lockit.Models;
public class Location
{
	[Key]
	public int ID { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
}
