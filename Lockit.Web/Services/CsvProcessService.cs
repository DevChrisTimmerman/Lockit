using Lockit.Models;
using MudBlazor;
using System.Text.RegularExpressions;
using Lockit.Models.Services;

namespace Lockit.Web.Services;

public class CsvProcessService
{
	private readonly IDialogService _dialogService;

	public CsvProcessService(IDialogService dialogService)
	{
		_dialogService = dialogService;
	}

	public async Task<CsvProcessingResult> ProcessStudentRecords(IEnumerable<Student> allRecords)
	{
		// Group to find duplicates and track them
		var groupedRecords = allRecords
			.GroupBy(s => new { 
				Email = s.Email.ToLowerInvariant(),
				Name = s.Name.ToLowerInvariant().Trim(),
				LastName = s.LastName.ToLowerInvariant().Trim()
			})
			.ToList();

		// Get unique records (first occurrence of each group)
		var uniqueRecords = groupedRecords
			.Select(g => g.First())
			.ToList();

		// Find duplicates (groups with more than one record)
		var duplicateGroups = groupedRecords
			.Where(g => g.Count() > 1)
			.ToList();

		// Show detailed information if duplicates were found
		if (duplicateGroups.Any())
		{
			var totalDuplicates = duplicateGroups.Sum(g => g.Count() - 1); // Total removed duplicates
		
			// Build detailed message
			var messageBuilder = new System.Text.StringBuilder();
			messageBuilder.AppendLine($"Removed {totalDuplicates} duplicate student entries. Students were matched by email, name, and last name.");
			messageBuilder.AppendLine();
			messageBuilder.AppendLine("Duplicate students removed:");
		
			foreach (var group in duplicateGroups)
			{
				var firstStudent = group.First();
				var duplicateCount = group.Count() - 1;
				messageBuilder.Append($"• {firstStudent.Name} {firstStudent.LastName} ");
				if (string.IsNullOrEmpty(firstStudent.Class) == false)
				{
					messageBuilder.AppendLine($"({firstStudent.Class})");
				}
			}

			await _dialogService.ShowMessageBox("Duplicates Removed", messageBuilder.ToString());
		}

		return new CsvProcessingResult
		{
			UniqueStudents = uniqueRecords,
			TotalDuplicatesRemoved = duplicateGroups.Sum(g => g.Count() - 1),
			DuplicateGroups = duplicateGroups.Select(g => g.ToList()).ToList()
		};
	}

	public int ParseStudentYear(string studentClass)
	{
		if (string.IsNullOrWhiteSpace(studentClass))
			return 1;

		// Try to extract numeric year from various formats like "1A", "2B", "3rd year", etc.
		var match = Regex.Match(studentClass, @"(\d+)");
		if (match.Success && int.TryParse(match.Groups[1].Value, out int year))
		{
			return Math.Max(1, Math.Min(6, year)); // Clamp between 1-6
		}

		// Fallback: try to parse ordinal numbers
		if (studentClass.Contains("1st", StringComparison.OrdinalIgnoreCase)) return 1;
		if (studentClass.Contains("2nd", StringComparison.OrdinalIgnoreCase)) return 2;
		if (studentClass.Contains("3rd", StringComparison.OrdinalIgnoreCase)) return 3;
		if (studentClass.Contains("4th", StringComparison.OrdinalIgnoreCase)) return 4;
		if (studentClass.Contains("5th", StringComparison.OrdinalIgnoreCase)) return 5;
		if (studentClass.Contains("6th", StringComparison.OrdinalIgnoreCase)) return 6;

		return 1; // Default to 1st year if unable to parse
	}
}