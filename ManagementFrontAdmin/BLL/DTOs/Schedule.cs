using BLL.DTOs.Enum;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
    public class Schedule
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn thứ")]
        public EDateOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        [Required(ErrorMessage = "Phòng học không được để trống")]
        public string Room { get; set; }
    }
}
