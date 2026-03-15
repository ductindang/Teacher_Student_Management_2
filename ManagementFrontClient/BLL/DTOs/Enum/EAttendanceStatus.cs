using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs.Enum
{
    public enum EAttendanceStatus
    {
        [Display(Name = "Có mặt")]
        Present = 1,
        [Display(Name = "Vắng mặt")]
        Absent = 2,
        [Display(Name = "Vào trễ")]
        Late = 3
    }
}
