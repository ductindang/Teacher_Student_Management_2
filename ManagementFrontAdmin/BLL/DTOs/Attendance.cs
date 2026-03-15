namespace BLL.DTOs
{
    public class Attendance
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public int StudentId { get; set; }
        public string Status { get; set; }
    }
}
