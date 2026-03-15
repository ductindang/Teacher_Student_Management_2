using DAL.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Response
{
    public class StudentAttendanceResponse
    {
        public int StudentId { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public EGender Gender { get; set; }
        public string Address { get; set; }

        public EAttendanceStatus? Status { get; set; }
    }
}
