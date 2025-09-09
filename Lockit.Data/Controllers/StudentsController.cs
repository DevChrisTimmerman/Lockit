using Lockit.Data.Repositories;
using Lockit.Models;
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

	[HttpPost]
	public async Task<IActionResult> AddStudent(Student student)
	{
		if (student == null)
		{
			return BadRequest();
		}

		var addedStudent = await _studentRepository.AddStudentAsync(student);
		return CreatedAtAction(nameof(GetStudentById), new { studentId = addedStudent.ID }, addedStudent);
	}

	[HttpPost("batch")]
	public async Task<IActionResult> AddStudentsBatch(IEnumerable<Student> students)
	{
		if (students == null || !students.Any())
		{
			return BadRequest("Student list is null or empty.");
		}
		var addedStudents = await _studentRepository.AddStudentBatchAsync(students);
		return CreatedAtAction(nameof(GetAllStudents), addedStudents);
	}

	[HttpDelete]
	public async Task<IActionResult> DeleteAllStudents()
	{
		await _studentRepository.DeleteAllStudentsAsync();
		return NoContent();
	}

}
