using Lockit.Data.Models;
using Lockit.Models;
using Microsoft.EntityFrameworkCore;

namespace Lockit.Data.Repositories;

public class StudentRepository : IStudentRepository
{
	private LockitDbContext _context;

	public StudentRepository(LockitDbContext context)
	{
		_context = context;
	}

	public async Task<List<Student>> GetAllStudentsAsync()
	{
		return await _context.Students.ToListAsync();
	}

	public async Task<Student?> GetStudentByIdAsync(int studentId)
	{
		return await _context.Students.FirstOrDefaultAsync(u => u.ID == studentId);
	}

	public async Task<Student?> AddStudentAsync(Student student)
	{
		_context.Students.Add(student);
		await _context.SaveChangesAsync();
		return student;
	}

	public async Task<IEnumerable<Student>?> AddStudentBatchAsync(IEnumerable<Student> students)
	{
		_context.Students.AddRange(students);
		await _context.SaveChangesAsync();
		return students;
	}

	public async Task DeleteAllStudentsAsync()
	{
		// Unassign lockers (set StudentID to null) before deleting students.
		var lockersToUpdate = await _context.Lockers.Where(l => l.StudentID != null).ToListAsync();
		foreach (var locker in lockersToUpdate)
		{
			locker.StudentID = null;
			locker.Status = Enums.LockerStatus.Available;
		}
		
		// Save the locker updates first
		await _context.SaveChangesAsync();

		// Now safe to delete students
		_context.Students.RemoveRange(_context.Students);
		await _context.SaveChangesAsync();
	}
}
