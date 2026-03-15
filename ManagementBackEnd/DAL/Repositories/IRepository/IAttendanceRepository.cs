using DAL.Models;
using DAL.Response;

namespace DAL.Repositories.IRepository
{
    public interface IAttendanceRepository : IGenericRepository<Attendance>
    {
        Task<Attendance> GetAttendanceByClass(int classId);
        Task<Attendance> GetAttendanceBySchedule(int scheduleId);
        Task<Attendance> GetAttendanceByStudent(int studentId);
        public Task<IEnumerable<AttendanceScheduleClassResponse>> GetAttendanceDetails(int studentId, int classId);
    }
}
