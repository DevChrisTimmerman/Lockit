using Lockit.Models;

namespace Lockit.Data.Repositories;

public interface IStudentRepository
{
	Task<List<Student>> GetAllStudentsAsync();
	Task<Student?> GetStudentByIdAsync(int studentId);
	Task<Student?> AddStudentAsync(Student student);
	Task<IEnumerable<Student>?> AddStudentBatchAsync(IEnumerable<Student> students);
	//Task<Student?> UpdateStudentAsync(int studentId, Student student);
	//Task<bool> DeleteStudentAsync(int studentId);
	Task DeleteAllStudentsAsync();
}
