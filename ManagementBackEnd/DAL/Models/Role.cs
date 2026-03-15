using DAL.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("Role")]
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public ERoleName Name { get; set; }
    }
}
