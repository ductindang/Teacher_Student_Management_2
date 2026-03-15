using DAL.Models.Enum;

namespace DAL.Response
{
    public class StudentEnrollmentResponse
    {
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public string Fullname { get; set; }
        public EGender Gender { get; set; }
        public DateTime EnrollDate { get; set; }
        public EEnrollStatus EnrollStatus { get; set; }

    }
}
