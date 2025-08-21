using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Lockit.Models;

public class Locker
{
	[Key]
	public int ID { get; set; }
	public string Number { get; set; }
	public int? StudentID { get; set; }
	public int LocationID { get; set; }
	public Enums.LockerStatus Status { get; set; }

	[ForeignKey(nameof(LocationID))]
	public Location Location { get; set; }

	[ForeignKey(nameof(StudentID))]
	public Student? Student { get; set; } = null;
}