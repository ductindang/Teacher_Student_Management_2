using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class ClassImage
    {
        [Key]
        public int Id { get; set; }
        public int ClassId { get; set; }
        [Column("ClassImg", TypeName = "image")]
        public byte[]? ClassImg { get; set; }
    }
}
