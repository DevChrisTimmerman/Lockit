using Lockit.Models;
using Lockit.Models.Services;
using MudBlazor;

namespace Lockit.Web.Services;

public class LockerAssignmentService
{
	private readonly LockerService _lockerService;
	private readonly StudentService _studentService;
	private readonly CsvProcessService _csvProcessService;
	private readonly IDialogService _dialogService;

	public LockerAssignmentService(
		LockerService lockerService, 
		StudentService studentService,
		CsvProcessService csvProcessService,
		IDialogService dialogService)
	{
		_lockerService = lockerService;
		_studentService = studentService;
		_csvProcessService = csvProcessService;
		_dialogService = dialogService;
	}

	public async Task<LockerAssignmentResult> AutoAssignLockersAsync(
		List<Student> students, 
		List<Location> locations, 
		List<PrefDropItem> preferences)
	{
		if (students == null || !students.Any())
		{
			throw new InvalidOperationException("No students to assign lockers to.");
		}

		var result = new LockerAssignmentResult();

		try
		{
			// 1. Group students by year level and sort by priority (6th->1st years)
			var studentsByYear = students
				.GroupBy(student => _csvProcessService.ParseStudentYear(student.Class))
				.OrderByDescending(g => g.Key) // Higher years get priority
				.ToDictionary(g => g.Key, g => g.ToList());

			// 2. Get all available lockers and group by location
			var allLockers = await _lockerService.GetAllLockersAsync();
			var availableLockersByLocation = allLockers
				.Where(l => l.Status == Enums.LockerStatus.Available)
				.GroupBy(l => l.LocationID)
				.ToDictionary(g => g.Key, g => g.OrderBy(l => CalculateLockerPriority(l, locations.First(loc => loc.ID == l.LocationID))).ToList());

			// 3. Process each year group (in priority order)
			foreach (var yearGroup in studentsByYear.OrderByDescending(kv => kv.Key))
			{
				var year = yearGroup.Key;
				var studentsInYear = yearGroup.Value;

				// 4. Find preferences for this year group (ordered by priority)
				var yearPreferences = preferences
					.Where(p => p.Year == year)
					.OrderBy(p => p.Index)
					.ToList();

				// 5. Try to assign entire year group to preferred locations
				bool yearFullyAssigned = false;

				foreach (var preference in yearPreferences)
				{
					if (availableLockersByLocation.ContainsKey(preference.LocationID) && 
						availableLockersByLocation[preference.LocationID].Count >= studentsInYear.Count)
					{
						// We can fit the entire year group in this location
						var lockersAtLocation = availableLockersByLocation[preference.LocationID].Take(studentsInYear.Count).ToList();
						
						for (int i = 0; i < studentsInYear.Count; i++)
						{
							var student = studentsInYear[i];
							var locker = lockersAtLocation[i];
							
							// Track assignment (don't commit to database yet)
							result.ProposedAssignments[student] = locker;
							
							// Remove from available list
							availableLockersByLocation[preference.LocationID].Remove(locker);
						}
						
						yearFullyAssigned = true;
						break;
					}
				}

				// 6. If we couldn't fit the entire year in preferred locations, try to group as many as possible
				if (!yearFullyAssigned)
				{
					var remainingStudents = studentsInYear.ToList();

					// First pass: Fill up preferred locations with as many students as possible
					foreach (var preference in yearPreferences)
					{
						if (!remainingStudents.Any()) break;

						if (availableLockersByLocation.ContainsKey(preference.LocationID) && 
							availableLockersByLocation[preference.LocationID].Any())
						{
							var availableCount = availableLockersByLocation[preference.LocationID].Count;
							var studentsToAssign = remainingStudents.Take(availableCount).ToList();

							foreach (var student in studentsToAssign)
							{
								var locker = availableLockersByLocation[preference.LocationID].First();
								
								// Track assignment (don't commit to database yet)
								result.ProposedAssignments[student] = locker;
								
								// Remove from available lists
								availableLockersByLocation[preference.LocationID].Remove(locker);
								remainingStudents.Remove(student);
							}
						}
					}

					// Second pass: Try to group remaining students in any available location with sufficient capacity
					if (remainingStudents.Any())
					{
						// Find the location with the most available lockers that can fit the most remaining students
						var bestLocation = availableLockersByLocation
							.Where(kv => kv.Value.Any())
							.OrderByDescending(kv => Math.Min(kv.Value.Count, remainingStudents.Count))
							.FirstOrDefault();

						if (bestLocation.Value != null && bestLocation.Value.Any())
						{
							var studentsToAssign = remainingStudents.Take(bestLocation.Value.Count).ToList();

							foreach (var student in studentsToAssign)
							{
								var locker = availableLockersByLocation[bestLocation.Key].First();
								
								// Track assignment (don't commit to database yet)
								result.ProposedAssignments[student] = locker;
								
								// Remove from available lists
								availableLockersByLocation[bestLocation.Key].Remove(locker);
								remainingStudents.Remove(student);
							}
						}

						// Third pass: Assign any remaining students to any available locker
						foreach (var student in remainingStudents)
						{
							var anyAvailableLocker = availableLockersByLocation.Values
								.SelectMany(lockers => lockers)
								.OrderBy(l => CalculateLockerPriority(l, locations.First(loc => loc.ID == l.LocationID)))
								.FirstOrDefault();

							if (anyAvailableLocker != null)
							{
								// Track assignment (don't commit to database yet)
								result.ProposedAssignments[student] = anyAvailableLocker;
								
								availableLockersByLocation[anyAvailableLocker.LocationID].Remove(anyAvailableLocker);
							}
							else
							{
								result.UnassignedStudents.Add(student);
							}
						}
					}
				}
			}

			result.IsSuccess = true;
		}
		catch (Exception ex)
		{
			result.IsSuccess = false;
			result.ErrorMessage = ex.Message;
		}

		return result;
	}

	public async Task<CommitAssignmentResult> CommitAssignmentsAsync(Dictionary<Student, Locker> proposedAssignments)
	{
		if (proposedAssignments?.Any() != true)
		{
			throw new InvalidOperationException("No assignments to commit.");
		}

		var result = new CommitAssignmentResult();

		try
		{
			// Add students in batch and get the results with IDs
			var studentsToAdd = proposedAssignments.Select(u => u.Key).Distinct().Where(u => u.ID == 0).ToList();
			var addedStudents = await _studentService.AddStudentsBatchAsync(studentsToAdd);

			// Update the student IDs in the original objects
			foreach (var addedStudent in addedStudents)
			{
				var originalStudent = studentsToAdd.FirstOrDefault(s => 
					s.Name == addedStudent.Name && 
					s.LastName == addedStudent.LastName && 
					s.Email == addedStudent.Email);
				if (originalStudent != null)
				{
					originalStudent.ID = addedStudent.ID;
				}
			}

			foreach (var assignment in proposedAssignments)
			{
				try
				{
					var student = assignment.Key;
					var locker = assignment.Value;

					// Update locker with student assignment
					locker.StudentID = student.ID;
					locker.Status = Enums.LockerStatus.Occupied;
					
					await _lockerService.UpdateLockerAsync(locker);
					result.SuccessCount++;
				}
				catch (Exception ex)
				{
					result.ErrorCount++;
					result.Errors.Add($"Failed to assign {assignment.Key.Name} {assignment.Key.LastName}: {ex.Message}");
				}
			}

			result.IsSuccess = result.ErrorCount == 0;
		}
		catch (Exception ex)
		{
			result.IsSuccess = false;
			result.Errors.Add($"An error occurred during commit: {ex.Message}");
		}

		return result;
	}

	public int CalculateLockerPriority(Locker locker, Location location)
	{
		// Parse locker number to get its index
		var numberPart = locker.Number;
		if (!string.IsNullOrEmpty(location.Prefix))
		{
			numberPart = locker.Number.Replace(location.Prefix, "");
		}

		if (int.TryParse(numberPart, out int lockerIndex))
		{
			// Convert to 0-based index
			lockerIndex = lockerIndex - 1;
			
			// Calculate position in grid
			int row = lockerIndex / location.LockersPerColumn;
			int col = lockerIndex % location.LockersPerColumn;
			
			// Priority calculation for horizontal distribution:
			// Distribute across columns first, then rows
			// Formula: col * totalRows + row
			// This gives us horizontal distribution: 1, 4, 7 (first row), then 2, 5, 8 (second row), etc.
			int totalRows = (int)Math.Ceiling((double)location.LockerCount / location.LockersPerColumn);
			int priority = col * totalRows + row;
			
			return priority;
		}

		return int.MaxValue; // If unable to parse, put at end
	}

	public Color GetYearPriorityColor(int year)
	{
		return year switch
		{
			6 => Color.Error,      // Highest priority - red
			5 => Color.Warning,    // High priority - orange  
			4 => Color.Info,       // Medium-high priority - blue
			3 => Color.Primary,    // Medium priority - purple
			2 => Color.Success,    // Lower priority - green
			1 => Color.Secondary,  // Lowest priority - grey
			_ => Color.Default
		};
	}

	public string GetYearPriorityText(int year)
	{
		return year switch
		{
			6 => "6th Year (Highest)",
			5 => "5th Year (High)",
			4 => "4th Year (Med-High)",
			3 => "3rd Year (Medium)",
			2 => "2nd Year (Low)",
			1 => "1st Year (Lowest)",
			_ => $"{year}th Year"
		};
	}

	public string GetPriorityBackgroundColor(int year)
	{
		return year switch
		{
			6 => "rgba(244, 67, 54, 0.1)",    // Light red background
			5 => "rgba(255, 152, 0, 0.1)",    // Light orange background
			4 => "rgba(33, 150, 243, 0.1)",   // Light blue background
			3 => "rgba(156, 39, 176, 0.1)",   // Light purple background
			2 => "rgba(76, 175, 80, 0.1)",    // Light green background
			1 => "rgba(158, 158, 158, 0.1)",  // Light grey background
			_ => "var(--mud-palette-surface)"
		};
	}

	public string GetLockerNumber(Location location, int index)
	{
		var prefix = string.IsNullOrEmpty(location.Prefix) ? "" : location.Prefix;
		return $"{prefix}{index:D3}";
	}
}
