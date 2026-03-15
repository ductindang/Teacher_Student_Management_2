using BLL.DTOs.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Response
{
    public class StudentEnrollmentRequest
    {
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public string Fullname { get; set; }
        public EGender Gender { get; set; }
        public DateTime EnrollDate { get; set; }
        public EEnrollStatus EnrollStatus { get; set; }
    }
}
