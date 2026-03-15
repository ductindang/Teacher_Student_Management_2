using DAL.Models.Enum;

namespace DAL.Response
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
