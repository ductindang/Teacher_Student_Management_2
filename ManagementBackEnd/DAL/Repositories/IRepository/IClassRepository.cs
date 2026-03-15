using DAL.Models;
using DAL.Response;

namespace DAL.Repositories.IRepository
{
    public interface IClassRepository : IGenericRepository<Class>
    {
        public Task<IEnumerable<ClassResponse>> GetAllClassDetail();
        public Task<ClassResponse> GetClassById(int id);
        public Task<Class> GetClassByCourse(int courseId);
        Task<Class> GetClassByTeacher(int teacherId);

        public Task<IEnumerable<ClassDetailResponse>> GetClassDetailTodayByTeacherId(int teacherId);
        public Task<IEnumerable<ClassDetailResponse>> GetAllClassDetailByTeacherIdInProgress(int teacherId);
        Task<IEnumerable<ClassDetailResponse>> GetAllClassDetailByTeacherIdUpcomming(int teacherId);
        Task<IEnumerable<ClassDetailResponse>> GetAllClassDetailByTeacherIdFinished(int teacherId);

        public Task<IEnumerable<ClassDetailResponse>> GetClassDetailTodayByStudentId(int studentId);
        public Task<IEnumerable<ClassDetailResponse>> GetAllClassDetailByStudentIdInProgress(int studentId);
        Task<IEnumerable<ClassDetailResponse>> GetAllClassDetailByStudentIdUpcomming(int studentId);
        Task<IEnumerable<ClassDetailResponse>> GetAllClassDetailByStudentIdFinished(int studentId);
        Task<IEnumerable<TeacherClassReviewResponse>> GetAllClassAndReviewDetailByStudentId(int studentId);

        Task<IEnumerable<ClassDetailResponse>> GetAllClassDetailUpcomming();
    }
}
