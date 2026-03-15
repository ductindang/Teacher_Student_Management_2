using DAL.Models.Enum;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string? PasswordHash { get; set; }
        public string Phone { get; set; }
        public EAccountStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public int RoleId { get; set; }

    }
}
