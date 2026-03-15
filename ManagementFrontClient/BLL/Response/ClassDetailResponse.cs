using BLL.DTOs.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Response
{
    public class ClassDetailResponse
    {
        public int Id { get; set; }
        public int? CourseId { get; set; }
        public string? CourseName { get; set; }
        public decimal? CoursePrice { get; set; }
        public int? TeacherId { get; set; }
        public string? TeacherName { get; set; }
        public string? Name { get; set; }
        public byte[]? ClassImage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? MaxStudents { get; set; }
        public string? Description { get; set; }

        public int? ScheduleId { get; set; }
        public EDateOfWeek? DayOfWeek { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string? Room { get; set; }
    }
}
