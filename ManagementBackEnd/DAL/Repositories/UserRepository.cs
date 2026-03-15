using DAL.Data;
using DAL.Models;
using DAL.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetByEmailPass(string email, string passwordHash)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == passwordHash);
            return user!;
        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user!;
        }
    }
}
