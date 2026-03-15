using DAL.Models.Enum;

namespace DAL.Response
{
    public class AttendanceScheduleClassResponse
    {
        public int AttendanceId { get; set; }
        public int ScheduleId { get; set; }
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public DateTime? AttendanceDate { get; set; }
        public EAttendanceStatus? Status { get; set; }

        public EDateOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Room { get; set; }

        public int CourseId { get; set; }
        public int TeacherId { get; set; }
        public string ClassName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MaxStudents { get; set; }
        public string Description { get; set; }
    }
}
