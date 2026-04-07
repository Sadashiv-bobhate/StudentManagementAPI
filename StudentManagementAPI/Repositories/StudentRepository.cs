using Microsoft.Data.SqlClient;
using StudentManagementAPI.Models;
using System.Linq;

namespace StudentManagementAPI.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string _connectionString;
        public StudentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            var students = new List<Student>();
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SELECT * FROM Students", con);
            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                students.Add(new Student
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"] == DBNull.Value ? "" : reader["Name"].ToString(),
                    Email = reader["Email"] == DBNull.Value ? "" : reader["Email"].ToString(),
                    Age = reader["Age"] == DBNull.Value ? 0 : (int)reader["Age"],
                    Course = reader["Course"] == DBNull.Value ? "" : reader["Course"].ToString(),
                    CreatedDate = reader["CreatedDate"] == DBNull.Value ? DateTime.Now : (DateTime)reader["CreatedDate"]
                });
            }
            return students;
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SELECT * FROM Students WHERE Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Student
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"] == DBNull.Value ? "" : reader["Name"].ToString(),
                    Email = reader["Email"] == DBNull.Value ? "" : reader["Email"].ToString(),
                    Age = reader["Age"] == DBNull.Value ? 0 : (int)reader["Age"],
                    Course = reader["Course"] == DBNull.Value ? "" : reader["Course"].ToString(),
                    CreatedDate = reader["CreatedDate"] == DBNull.Value ? DateTime.Now : (DateTime)reader["CreatedDate"]
                };
            }
            return null; // caller will handle not found
        }

        public async Task AddAsync(Student student)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(
                "INSERT INTO Students (Name, Email, Age, Course, CreatedDate) VALUES (@Name, @Email, @Age, @Course, @CreatedDate)", con);
            cmd.Parameters.AddWithValue("@Name", student.Name);
            cmd.Parameters.AddWithValue("@Email", student.Email);
            cmd.Parameters.AddWithValue("@Age", student.Age);
            cmd.Parameters.AddWithValue("@Course", student.Course);
            cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(Student student)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(
                "UPDATE Students SET Name=@Name, Email=@Email, Age=@Age, Course=@Course WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Name", student.Name);
            cmd.Parameters.AddWithValue("@Email", student.Email);
            cmd.Parameters.AddWithValue("@Age", student.Age);
            cmd.Parameters.AddWithValue("@Course", student.Course);
            cmd.Parameters.AddWithValue("@Id", student.Id);
            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("DELETE FROM Students WHERE Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
