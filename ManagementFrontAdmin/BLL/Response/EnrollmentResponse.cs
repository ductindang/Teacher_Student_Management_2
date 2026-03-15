using BLL.DTOs.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Response
{
    public class EnrollmentResponse
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public DateTime EnrollDate { get; set; }
        public EEnrollStatus Status { get; set; }
    }
}
