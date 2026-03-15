using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.Enum
{
    public enum ECourseStatus
    {
        [Display(Name = "Ngưng hoạt động")]
        InActive = 0,

        [Display(Name = "Đang hoạt động")]
        Active = 1
    }
}
