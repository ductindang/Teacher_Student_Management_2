using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("TeacherReview")]
    public class TeacherReview
    {
        [Key]
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
