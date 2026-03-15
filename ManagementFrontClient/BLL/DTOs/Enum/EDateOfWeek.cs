using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.Enum
{
    public enum EDateOfWeek
    {
        [Display(Name = "Chủ nhật")]
        Sunday = 0,
        [Display(Name = "Thứ 2")]
        Monday = 1,
        [Display(Name = "Thứ 3")]
        Tuesday = 2,
        [Display(Name = "Thứ 4")]
        Wednesday = 3,
        [Display(Name = "Thứ 5")]
        Thursday = 4,
        [Display(Name = "Thứ 6")]
        Friday = 5,
        [Display(Name = "Thứ 7")]
        Saturday = 6,
    }
}
