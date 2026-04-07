using StudentManagementAPI.DTOs;
using StudentManagementAPI.Models;
using StudentManagementAPI.Repositories;

namespace StudentManagementAPI.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            var student = await _repository.GetByIdAsync(id);
            if (student == null)
                throw new KeyNotFoundException($"Student with Id {id} not found.");
            return student;
        }

        public async Task AddAsync(StudentDto dto)
        {
            var student = new Student
            {
                Name = dto.Name,
                Email = dto.Email,
                Age = dto.Age,
                Course = dto.Course
            };
            await _repository.AddAsync(student);
        }

        public async Task UpdateAsync(int id, StudentDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Student with Id {id} not found.");

            existing.Name = dto.Name;
            existing.Email = dto.Email;
            existing.Age = dto.Age;
            existing.Course = dto.Course;

            await _repository.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Student with Id {id} not found.");

            await _repository.DeleteAsync(id);
        }
    }
}
