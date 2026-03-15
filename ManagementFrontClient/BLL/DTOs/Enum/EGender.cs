using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.Enum
{
    public enum EGender
    {
        [Display(Name = "Nam")]
        Male = 0,

        [Display(Name = "Nữ")]
        Female = 1,

        [Display(Name = "Khác")]
        Differ = 2,
    }
}
