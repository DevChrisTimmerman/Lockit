using Lockit.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Lockit.Data.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : Controller
{
	private readonly IStudentRepository _studentRepository;
	public StudentsController(IStudentRepository studentRepository)
	{
		_studentRepository = studentRepository;
	}

	[HttpGet]
	public async Task<IActionResult> GetAllStudents()
	{
		var students = await _studentRepository.GetAllStudentsAsync();
		return Ok(students);
	}

	[HttpGet("{studentId}")]
	public async Task<IActionResult> GetStudentById(int studentId)
	{
		var student = await _studentRepository.GetStudentByIdAsync(studentId);
		if (student == null)
		{
			return NotFound();
		}
		return Ok(student);
	}
}
