using DAL.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("Student")]
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public EGender Gender { get; set; }
        public string Address { get; set; }
    }
}
