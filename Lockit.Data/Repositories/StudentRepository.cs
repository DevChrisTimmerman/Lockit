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
		return await _context.Students.FindAsync(studentId);
	}
}
