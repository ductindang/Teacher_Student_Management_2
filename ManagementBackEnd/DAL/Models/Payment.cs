using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("Payment")]
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
    }
}
