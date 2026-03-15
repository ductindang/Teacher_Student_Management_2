using DAL.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("Enrollment")]
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public DateTime EnrollDate { get; set; }
        public EEnrollStatus Status { get; set; }
    }
}
