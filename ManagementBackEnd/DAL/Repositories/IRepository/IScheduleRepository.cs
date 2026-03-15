using DAL.Models;
using DAL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.IRepository
{
    public interface IScheduleRepository : IGenericRepository<Schedule>
    {
        Task<IEnumerable<Schedule>> InsertMany(IEnumerable<Schedule> schedules);
        Task<List<Schedule>> GetByClassId(int classId);
        public Task<IEnumerable<ClassDetailResponse>> GetAllScheduleByTeacher(int teacherId);
        public Task<IEnumerable<ClassDetailResponse>> GetAllScheduleByStudent(int studentId);
    }
}
