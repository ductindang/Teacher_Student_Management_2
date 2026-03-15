using DAL.Data;
using DAL.Models;
using DAL.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ClassImageRepository : GenericRepository<ClassImage>, IClassImageRepository
    {
        private readonly AppDbContext _context;
        public ClassImageRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ClassImage> GetClassImageByClass(int classId)
        {
            return await _context.ClassImages.FirstOrDefaultAsync(cli => cli.ClassId == classId);
        }
    }
}
