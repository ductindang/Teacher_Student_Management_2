using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("Teacher")]
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Degree { get; set; }
        public int Experience { get; set; }
    }
}
