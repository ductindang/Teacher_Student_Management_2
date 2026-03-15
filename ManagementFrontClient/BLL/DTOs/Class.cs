namespace BLL.DTOs
{
    public class Class
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MaxStudents { get; set; }
        public string Description { get; set; }
    }
}
