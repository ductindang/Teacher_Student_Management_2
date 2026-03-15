using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Response
{
    public class ClassResponse
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string Name { get; set; }
        public byte[]? ClassImage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MaxStudents { get; set; }
        public string Description { get; set; }
    }
}
