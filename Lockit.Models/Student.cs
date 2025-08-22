using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lockit.Models;

public class Student
{
	[Key] public int ID { get; set; }
	public string Name { get; set; }
	public string LastName { get; set; }
	public string Email { get; set; }
	public string Class { get; set; }

	//Smart school unique identifier
	public string? SCUID { get; set; }
}