using DAL.Data;
using DAL.Models;
using DAL.Repositories.IRepository;
using DAL.Response;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class AttendanceRepository : GenericRepository<Attendance>, IAttendanceRepository
    {
        private readonly AppDbContext _context;

        public AttendanceRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Attendance> GetAttendanceByClass(int classId)
        {
            return await _context.Attendances.FirstOrDefaultAsync(att => att.ClassId == classId);
        }
        public async Task<Attendance> GetAttendanceBySchedule(int scheduleId)
        {
            return await _context.Attendances.FirstOrDefaultAsync(att => att.ScheduleId == scheduleId);
        }
        public async Task<Attendance> GetAttendanceByStudent(int studentId)
        {
            return await _context.Attendances.FirstOrDefaultAsync(att => att.StudentId == studentId);
        }


        public async Task<IEnumerable<AttendanceScheduleClassResponse>> GetAttendanceDetails(int studentId, int classId)
        {
            var data = from sch in _context.Schedules
                       join cls in _context.Classes on sch.ClassId equals cls.Id

                       join at in _context.Attendances
                            .Where(a => a.StudentId == studentId)
                            on sch.Id equals at.ScheduleId into attGroup

                       from at in attGroup.DefaultIfEmpty()

                       where sch.ClassId == classId

                       select new AttendanceScheduleClassResponse
                       {
                           AttendanceId = at != null ? at.Id : 0,
                           ScheduleId = sch.Id,
                           StudentId = studentId,
                           ClassId = sch.ClassId,

                           AttendanceDate = at != null ? at.AttendanceDate : (DateTime?)null,
                           Status = at != null ? at.Status : null,

                           DayOfWeek = sch.DayOfWeek,
                           StartTime = sch.StartTime,
                           EndTime = sch.EndTime,
                           Room = sch.Room,

                           CourseId = cls.CourseId,
                           TeacherId = cls.TeacherId,
                           ClassName = cls.Name,
                           StartDate = cls.StartDate,
                           EndDate = cls.EndDate,
                           MaxStudents = cls.MaxStudents,
                           Description = cls.Description
                       };

            return await data.ToListAsync();
        }
    }
}
