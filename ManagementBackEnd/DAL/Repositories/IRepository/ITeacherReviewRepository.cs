using DAL.Models;
using DAL.Response;

namespace DAL.Repositories.IRepository
{
    public interface ITeacherReviewRepository : IGenericRepository<TeacherReview>
    {
        Task<List<TeacherReviewResponse>> GetByTeacherId(int teacherId);
        Task<TeacherReview> GetByTeacherStudentClass(int studentId, int classId);
        Task<TeacherReview> GetByStudent(int studentId);
        Task<TeacherReview> GetByClass(int classId);
    }
}
