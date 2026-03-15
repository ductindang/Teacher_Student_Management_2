using DAL.Data;
using DAL.Models;
using DAL.Repositories.IRepository;
using DAL.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ScheduleRepository : GenericRepository<Schedule>, IScheduleRepository
    {
        private readonly AppDbContext _context;
        public ScheduleRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Schedule>> InsertMany(IEnumerable<Schedule> schedules)
        {
            await _context.Schedules.AddRangeAsync(schedules);
            await _context.SaveChangesAsync();
            return schedules;
        }

        public async Task<List<Schedule>> GetByClassId(int classId)
        {
            return await _context.Schedules
                .Where(x => x.ClassId == classId)
                .OrderBy(x => x.DayOfWeek)
                .ThenBy(x => x.StartTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClassDetailResponse>> GetAllScheduleByTeacher(int teacherId)
        {
            var data = from c in _context.Classes
                       join course in _context.Courses on c.CourseId equals course.Id
                       join teacher in _context.Teachers on c.TeacherId equals teacher.Id
                       join schedule in _context.Schedules on c.Id equals schedule.ClassId
                       where teacher.Id == teacherId
                       select new ClassDetailResponse
                       {
                           Id = c.Id,
                           Name = c.Name,
                           CourseId = c.CourseId,
                           CourseName = course.Name,
                           TeacherId = c.TeacherId,
                           TeacherName = teacher.FullName,
                           StartDate = c.StartDate,
                           EndDate = c.EndDate,
                           MaxStudents = c.MaxStudents,

                           ScheduleId = schedule.Id,
                           DayOfWeek = schedule.DayOfWeek,
                           StartTime = schedule.StartTime,
                           EndTime = schedule.EndTime,
                           Room = schedule.Room
                       };

            return await data.ToListAsync();
        }

        public async Task<IEnumerable<ClassDetailResponse>> GetAllScheduleByStudent(int studentId)
        {
            var data = from schedule in _context.Schedules
                       join c in _context.Classes on schedule.ClassId equals c.Id
                       join enroll in _context.Enrollments on c.Id equals enroll.ClassId
                       join course in _context.Courses on c.CourseId equals course.Id
                       join teacher in _context.Teachers on c.TeacherId equals teacher.Id
                       where enroll.StudentId == studentId
                       select new ClassDetailResponse
                       {
                           Id = c.Id,
                           Name = c.Name,
                           CourseId = c.CourseId,
                           CourseName = course.Name,
                           TeacherId = c.TeacherId,
                           TeacherName = teacher.FullName,
                           StartDate = c.StartDate,
                           EndDate = c.EndDate,
                           MaxStudents = c.MaxStudents,

                           ScheduleId = schedule.Id,
                           DayOfWeek = schedule.DayOfWeek,
                           StartTime = schedule.StartTime,
                           EndTime = schedule.EndTime,
                           Room = schedule.Room
                       };

            return await data.ToListAsync();
        }
    }
}
