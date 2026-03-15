using DAL.Data;
using DAL.Models;
using DAL.Repositories.IRepository;
using DAL.Response;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        private readonly AppDbContext _context;
        public StudentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Student> GetStudentByUserId(int userId)
        {
            var data = await _context.Students.FirstOrDefaultAsync(st => st.UserId == userId);
            return data;
        }

        public async Task<IEnumerable<StudentEnrollmentResponse>> GetAllStudentByClass(int classId)
        {
            var data = from e in _context.Enrollments
                       join std in _context.Students on e.StudentId equals std.Id
                       where e.ClassId == classId
                       select new StudentEnrollmentResponse
                       {
                           EnrollmentId = e.Id,
                           StudentId = std.Id,
                           Fullname = std.FullName,
                           Gender = std.Gender,
                           EnrollDate = e.EnrollDate,
                           EnrollStatus = e.Status
                       };
            return await data.ToListAsync();
        }
    }
}
