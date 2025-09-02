using Lockit.Models;

namespace Lockit.Models.Services;

public class CsvProcessingResult
{
	public List<Student> UniqueStudents { get; set; } = new();
	public int TotalDuplicatesRemoved { get; set; }
	public List<List<Student>> DuplicateGroups { get; set; } = new();
}

public class LockerAssignmentResult
{
	public bool IsSuccess { get; set; }
	public string ErrorMessage { get; set; } = string.Empty;
	public Dictionary<Student, Locker> ProposedAssignments { get; set; } = new();
	public List<Student> UnassignedStudents { get; set; } = new();
}

public class CommitAssignmentResult
{
	public bool IsSuccess { get; set; }
	public int SuccessCount { get; set; }
	public int ErrorCount { get; set; }
	public List<string> Errors { get; set; } = new();
}

public class PrefDropItem
{
	public int Year { get; set; }
	public int Index { get; set; }
	public int LocationID { get; set; } = -1;
}