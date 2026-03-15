using BLL.Request;
using BLL.Response;

namespace BLL.IServices
{
    public interface IAttendanceService
    {
        Task<IEnumerable<StudentAttendanceResponse>> GetStudentsBySchedule(int scheduleId);
        Task<bool> TakeAttendance(AttendanceRequest request);
        Task<IEnumerable<AttendanceScheduleClassResponse>> GetAllAttendanceDetails(int studentId, int classId);
    }
}
