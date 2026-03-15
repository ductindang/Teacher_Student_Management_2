using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface IScheduleService
    {
        public Task<IEnumerable<Schedule>> GetAllSchedules();
        public Task<Schedule> GetScheduleById(int id);
        public Task<IEnumerable<Schedule>> GetByClass(int classId);
        public Task<Schedule> InsertSchedule(Schedule obj);
        public Task<Schedule> UpdateSchedule(Schedule obj);
        public Task<Schedule> DeleteSchedule(int id);
    }
}
