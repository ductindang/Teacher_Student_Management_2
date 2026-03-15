using BLL.DTOs.Enum;

namespace BLL.Request
{
    public class AttendanceRequest
    {
        public int ScheduleId { get; set; }
        public int ClassId { get; set; }
        public List<StudentAttendanceDto> Students { get; set; }
    }

    public class StudentAttendanceDto
    {
        public int StudentId { get; set; }

        public EAttendanceStatus Status { get; set; }
    }
}
