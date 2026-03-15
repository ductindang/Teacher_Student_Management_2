using DAL.Data;
using DAL.Models;
using DAL.Repositories.IRepository;
using DAL.Response;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class TeacherReviewRepository : GenericRepository<TeacherReview>, ITeacherReviewRepository
    {
        private readonly AppDbContext _context;
        public TeacherReviewRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<TeacherReviewResponse>> GetByTeacherId(int teacherId)
        {
            var data = from tr in _context.TeachersReviews
                join st in _context.Students on tr.StudentId equals st.Id
                where tr.TeacherId == teacherId
                select new TeacherReviewResponse
                {
                    Id = tr.Id,
                    TeacherId = teacherId,
                    StudentId = tr.StudentId,
                    StudentName = st.FullName,
                    ClassId = tr.ClassId,
                    Rating = tr.Rating,
                    Comment = tr.Comment,
                    CreatedAt = tr.CreatedAt
                };
            return await data.ToListAsync();
        }

        public async Task<TeacherReview> GetByTeacherStudentClass(int studentId, int classId)
        {
            return await _context.TeachersReviews.FirstOrDefaultAsync(re => re.StudentId == studentId && re.ClassId == classId);
        }

        public async Task<TeacherReview> GetByStudent(int studentId)
        {
            return await _context.TeachersReviews.FirstOrDefaultAsync(tear => tear.StudentId == studentId);
        }
        public async Task<TeacherReview> GetByClass(int classId)
        {
            return await _context.TeachersReviews.FirstOrDefaultAsync(tear => tear.ClassId == classId);
        }
    }
}
