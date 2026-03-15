using BLL.DTOs.Enum;

namespace BLL.DTOs
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public DateTime EnrollDate { get; set; }
        public EEnrollStatus Status { get; set; }
    }
}
