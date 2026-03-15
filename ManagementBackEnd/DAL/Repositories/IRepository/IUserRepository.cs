using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.IRepository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByEmailPass(string email, string hashedPassword);
        Task<User> GetByEmail(string email);
    }
}
