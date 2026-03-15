using DAL.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("Attendance")]
    public class Attendance
    {
        [Key]
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public EAttendanceStatus Status { get; set; }
    }
}
