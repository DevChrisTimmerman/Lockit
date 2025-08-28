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
}
