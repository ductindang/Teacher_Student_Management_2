namespace DAL.Response
{
    public class TeacherReviewResponse
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int ClassId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
