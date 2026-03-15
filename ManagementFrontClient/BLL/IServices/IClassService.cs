using BLL.DTOs;
using BLL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface IClassService
    {
        public Task<IEnumerable<ClassResponse>> GetAllClasses();
        public Task<ClassResponse> GetClassById(int id);
        public Task<IEnumerable<ClassDetailResponse>> GetClassDetailTodayByTeacherId(int teacherId);
        public Task<IEnumerable<ClassDetailResponse>> GetAllClassDetailByTeacherIdInProgress(int teacherId);
        Task<IEnumerable<ClassDetailResponse>> GetAllClassDetailByTeacherIdUpcoming(int teacherId);
        Task<IEnumerable<ClassDetailResponse>> GetAllClassDetailByTeacherIdFinished(int teacherId);

        public Task<IEnumerable<ClassDetailResponse>> GetClassDetailTodayByStudentId(int studentId);
        public Task<IEnumerable<ClassDetailResponse>> GetAllClassDetailByStudentIdInProgress(int studentId);
        Task<IEnumerable<ClassDetailResponse>> GetAllClassDetailByStudentIdUpcoming(int studentId);
        Task<IEnumerable<ClassDetailResponse>> GetAllClassDetailByStudentIdFinished(int studentId);
        Task<IEnumerable<TeacherClassReviewResponse>> GetAllClassDetailAndReviewByStudentIdReview(int studentId);

        Task<IEnumerable<ClassDetailResponse>> GetAllClassDetailUpcoming();

        public Task<Class> InsertClass(Class obj);
        public Task<Class> UpdateClass(Class obj);
        public Task<Class> DeleteClass(int id);
    }
}
