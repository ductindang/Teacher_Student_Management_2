using DAL.Data;
using DAL.Models;
using DAL.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class TeacherRepository : GenericRepository<Teacher>, ITeacherRepository
    {
        private AppDbContext _context;
        public TeacherRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Teacher> GetTeacherByUserId(int userId)
        {
            var data = await _context.Teachers.FirstOrDefaultAsync(st => st.UserId == userId);
            return data;
        }
    }
}
