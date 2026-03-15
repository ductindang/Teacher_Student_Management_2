using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserByEmailPass(string email, string password);
        Task<User> InsertUser(User user);
        Task<User> UpdateUser(User user);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(int id);
    }
}
