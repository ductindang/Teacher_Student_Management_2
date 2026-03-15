using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface ICourseService
    {
        public Task<IEnumerable<Course>> GetAllCourses();
        public Task<Course> GetCourseById(int id);
        public Task<Course> InsertCourse(Course obj);
        public Task<Course> UpdateCourse(Course obj);
        public Task<Course> DeleteCourse(int id);
    }
}
