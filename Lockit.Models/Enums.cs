namespace Lockit.Models;

public class Enums
{
	public enum LockerStatus
	{
		Available = 0,
		Occupied = 1,
		Maintenance = 2,
		Damaged = 3
	}

	public enum SchoolYear
	{
		First = 1,
		Second = 2,
		Third = 3,
		Fourth = 4,
		Fifth = 5,
		Sixth = 6,
	}
}


// Questions, connection string issue, fixed by putting in both projects?
// Question design time factory, why is it needed?
// Question, Blazor auto vs Blazor Server

