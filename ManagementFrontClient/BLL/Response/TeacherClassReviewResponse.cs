namespace BLL.Response
{
    public class TeacherClassReviewResponse
    {
        public int Id { get; set; }
        public int TeacherReviewId { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public string ClassName {  get; set; }
        public byte[]? ClassImage { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MaxStudents { get; set; }
        public string Description { get; set; }
    }
}
