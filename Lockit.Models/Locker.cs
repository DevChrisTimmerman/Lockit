using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lockit.Models;

public class Locker
{
	[Key]
	public int ID { get; set; }
	public string Number { get; set; }
	public int StudentID { get; set; }
	public int LocationID { get; set; }
	public Enums.LockerStatus Status { get; set; }

	[ForeignKey("LocationID")]
	public Location Location { get; set; }
	[ForeignKey("StudentID")]
	public Student Student { get; set; }

	public Locker()
	{
		Location = new Location();
		Student = new Student();
	}
}