using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface ITeacherService
    {
        public Task<IEnumerable<Teacher>> GetAllTeachers();
        public Task<Teacher> GetTeacherById(int id);
        public Task<Teacher> InsertTeacher(Teacher obj);
        public Task<Teacher> UpdateTeacher(Teacher obj);
        public Task<Teacher> UpdateAccount(int teacherId, int userId);
        public Task<Teacher> DeleteTeacher(int id);
    }
}
