using DAL.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("Schedule")]
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        public int ClassId { get; set; }
        public EDateOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Room {  get; set; }
    }
}
