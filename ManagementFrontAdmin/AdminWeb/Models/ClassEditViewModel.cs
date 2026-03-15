using BLL.DTOs;
using BLL.Response;

namespace AdminWeb.Models
{
    public class ClassEditViewModel
    {
        public ClassResponse Class { get; set; }
        public IEnumerable<StudentEnrollmentRequest> Students { get; set; }
        public IEnumerable<Schedule> Schedules { get; set; }
    }
}
