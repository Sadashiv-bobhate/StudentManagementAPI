using StudentManagementAPI.DTOs;
using StudentManagementAPI.Models;

namespace StudentManagementAPI.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(int id);
        Task AddAsync(StudentDto dto);
        Task UpdateAsync(int id, StudentDto dto);
        Task DeleteAsync(int id);
    }
}
