using BLL.DTOs.Enum;

namespace BLL.DTOs
{
    public class Schedule
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public EDateOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Room { get; set; }
    }
}
