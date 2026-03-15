using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs.Enum
{
    public enum EAccountStatus
    {
        [Display(Name = "Ngưng hoạt động")]
        InActive = 0,

        [Display(Name = "Đang hoạt động")]
        Active = 1
    }
}
