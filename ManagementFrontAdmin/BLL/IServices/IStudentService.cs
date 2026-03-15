using BLL.DTOs;
using BLL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface IStudentService
    {
        public Task<IEnumerable<Student>> GetAllStudents();
        public Task<Student> GetStudentById(int id);
        public Task<IEnumerable<StudentEnrollmentRequest>> GetStudentsByClass(int classId);
        public Task<Student> InsertStudent(Student obj);
        public Task<Student> UpdateStudent(Student obj);
        public Task<Student> UpdateAccount(int studentId, int userId);
        public Task<Student> DeleteStudent(int id);
    }
}
