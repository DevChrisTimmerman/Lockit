using Lockit.Models;
using System.Net;
using System.Text.Json;

namespace Lockit.Web.Services;

public class StudentService
{
	private readonly HttpClient _httpClient;

	public StudentService(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	public async Task<List<Student>> GetAllStudentsAsync()
	{
		var response = await _httpClient.GetAsync("api/students");
		switch (response.StatusCode)
		{
			case HttpStatusCode.OK:
				return await response.Content.ReadFromJsonAsync<List<Student>>();
			default:
				var errorContent = await response.Content.ReadAsStringAsync();
				throw new Exception($"An error occurred while fetching all students. Status: {response.StatusCode}, Content: {errorContent}");
		}
	}

	public async Task<Student> GetStudentByIdAsync(int studentId)
	{
		var response = await _httpClient.GetAsync($"api/students/{studentId}");
		switch (response.StatusCode)
		{
			case HttpStatusCode.OK:
				return await response.Content.ReadFromJsonAsync<Student>();
			default:
				var errorContent = await response.Content.ReadAsStringAsync();
				throw new Exception($"An error occurred while fetching the student. Status: {response.StatusCode}, Content: {errorContent}");
		}
	}

	public async Task<Student> AddStudentAsync(Student student)
	{
		var response = await _httpClient.PostAsJsonAsync("api/students", student);
		switch (response.StatusCode)
		{
			case HttpStatusCode.Created:
				return await response.Content.ReadFromJsonAsync<Student>();
			default:
				var errorContent = await response.Content.ReadAsStringAsync();
				throw new Exception($"An error occurred while adding the student. Status: {response.StatusCode}, Content: {errorContent}");
		}
	}

	public async Task<List<Student>> AddStudentsBatchAsync(IEnumerable<Student> students)
	{
		var response = await _httpClient.PostAsJsonAsync("api/students/batch", students);
		switch (response.StatusCode)
		{
			case HttpStatusCode.Created:
				return await response.Content.ReadFromJsonAsync<List<Student>>();
			default:
				var errorContent = await response.Content.ReadAsStringAsync();
				throw new Exception($"An error occurred while adding students. Status: {response.StatusCode}, Content: {errorContent}");
		}
	}

	public async Task DeleteAllStudentsAsync()
	{
		var response = await _httpClient.DeleteAsync("api/students");
		switch (response.StatusCode)
		{
			case HttpStatusCode.NoContent:
				break;
			default:
				var errorContent = await response.Content.ReadAsStringAsync();
				throw new Exception($"An error occurred while deleting all students. Status: {response.StatusCode}, Content: {errorContent}");
		}
	}

}