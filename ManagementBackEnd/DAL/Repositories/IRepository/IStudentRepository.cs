using DAL.Models;
using DAL.Response;

namespace DAL.Repositories.IRepository
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        public Task<Student> GetStudentByUserId(int userId);
        public Task<IEnumerable<StudentEnrollmentResponse>> GetAllStudentByClass(int classId);
    }
}
