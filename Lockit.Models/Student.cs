using System.ComponentModel.DataAnnotations;

namespace Lockit.Models;
public class Student
{
	[Key]
	public int ID { get; set; }
	public string Name { get; set; }
	public string LastName { get; set; }
	public string Email { get; set; }
	public string Class { get; set; }
}