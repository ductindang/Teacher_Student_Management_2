using BLL.DTOs.Enum;

namespace BLL.DTOs
{
    public class Student
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public EGender Gender { get; set; }
        public string Address { get; set; }
    }
}
