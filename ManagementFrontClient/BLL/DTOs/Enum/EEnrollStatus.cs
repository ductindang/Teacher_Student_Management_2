using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs.Enum
{
    public enum EEnrollStatus
    {
        [Display(Name = "Chưa hoạt động")]
        InActive = 0,

        [Display(Name = "Đang hoạt động")]
        Active = 1,

        [Display(Name = "Đã hủy")]
        Cancel = 2,

    }
}
