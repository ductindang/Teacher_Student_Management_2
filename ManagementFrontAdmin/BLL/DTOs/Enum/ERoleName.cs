using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs.Enum
{
    public enum ERoleName
    {
        [Display(Name = "Học viên")]
        Student = 0,

        [Display(Name = "Giáo viên")]
        Teacher = 1,

        [Display(Name = "Quản trị viên")]
        Admin = 2
    }
}
