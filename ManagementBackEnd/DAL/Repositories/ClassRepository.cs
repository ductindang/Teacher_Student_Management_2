using DAL.Data;
using DAL.Models;
using DAL.Models.Enum;
using DAL.Repositories.IRepository;
using DAL.Response;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ClassRepository : GenericRepository<Class>, IClassRepository
    {
        private readonly AppDbContext _context;

        public ClassRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Class> GetClassByCourse(int courseId)
        {
            return await _context.Classes.FirstOrDefaultAsync(cls => cls.CourseId == courseId);
        }
        public async Task<Class> GetClassByTeacher(int teacherId)
        {
            return await _context.Classes.FirstOrDefaultAsync(cls => cls.TeacherId == teacherId);
        }

        public async Task<IEnumerable<ClassResponse>> GetAllClassDetail()
        {
            var data = from c in _context.Classes
                       join course in _context.Courses on c.CourseId equals course.Id
                       join teacher in _context.Teachers on c.TeacherId equals teacher.Id
                       select new ClassResponse
                       {
                           Id = c.Id,
                           Name = c.Name,
                           CourseId = c.CourseId,
                           CourseName = course.Name,
                           CoursePrice = course.Price,
                           TeacherId = c.TeacherId,
                           TeacherName = teacher.FullName,
                           StartDate = c.StartDate,
                           EndDate = c.EndDate,
                           MaxStudents = c.MaxStudents,
                           Description = c.Description
                       };

            return await data.ToListAsync();
        }

        public async Task<ClassResponse?> GetClassById(int id)
        {
            var data = await (
                from c in _context.Classes
                where c.Id == id

                join course in _context.Courses
                    on c.CourseId equals course.Id

                join teacher in _context.Teachers
                    on c.TeacherId equals teacher.Id

                join classImg in _context.ClassImages
                    on c.Id equals classImg.ClassId
                    into imgGroup
                from classImg in imgGroup.DefaultIfEmpty() // LEFT JOIN

                select new ClassResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    ClassImage = classImg != null ? classImg.ClassImg : null,
                    CourseId = c.CourseId,
                    CourseName = course.Name,
                    CoursePrice = course.Price,
                    TeacherId = c.TeacherId,
                    TeacherName = teacher.FullName,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    MaxStudents = c.MaxStudents,
                    Description = c.Description
                }
            ).FirstOrDefaultAsync();

            return data;
        }

        public async Task<IEnumerable<ClassDetailResponse>> GetClassDetailTodayByTeacherId(int teacherId)
        {
            var today = DateTime.Today;

            // Convert System.DayOfWeek -> EDateOfWeek
            var todayEnum = (EDateOfWeek)today.DayOfWeek;

            var data = from c in _context.Classes
                       join course in _context.Courses on c.CourseId equals course.Id
                       join teacher in _context.Teachers on c.TeacherId equals teacher.Id
                       join schedule in _context.Schedules
                            on c.Id equals schedule.ClassId into scheduleGroup
                            from schedule in scheduleGroup.DefaultIfEmpty()
                       where teacher.Id == teacherId
                             && today >= c.StartDate.Date
                             && today <= c.EndDate.Date
                             && schedule.DayOfWeek == todayEnum
                       select new ClassDetailResponse
                       {
                           Id = c.Id,
                           Name = c.Name,
                           CourseId = c.CourseId,
                           CourseName = course.Name,
                           CoursePrice = course.Price,
                           TeacherId = c.TeacherId,
                           TeacherName = teacher.FullName,
                           StartDate = c.StartDate,
                           EndDate = c.EndDate,
                           MaxStudents = c.MaxStudents,
                           Description = c.Description,

                           ScheduleId = schedule.Id,
                           DayOfWeek = schedule.DayOfWeek,
                           StartTime = schedule.StartTime,
                           EndTime = schedule.EndTime,
                           Room = schedule.Room
                       };

            return await data.ToListAsync();
        }

        public async Task<IEnumerable<ClassDetailResponse>> GetAllClassDetailByTeacherIdInProgress(int teacherId)
        {
            var today = DateTime.Today;

            var data = await (
                from c in _context.Classes
                join course in _context.Courses on c.CourseId equals course.Id
                join teacher in _context.Teachers on c.TeacherId equals teacher.Id
                join schedule in _context.Schedules
                    on c.Id equals schedule.ClassId into scheduleGroup
                    from schedule in scheduleGroup.DefaultIfEmpty()
                join classImg in _context.ClassImages on c.Id equals classImg.ClassId into imgGroup
                from classImg in imgGroup.DefaultIfEmpty()
                where teacher.Id == teacherId
                      && c.StartDate.Date <= today
                      && c.EndDate.Date >= today
                      && course.Status == ECourseStatus.Active
                select new ClassDetailResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    ClassImage = classImg != null ? classImg.ClassImg : null,
                    CourseId = c.CourseId,
                    CourseName = course.Name,
                    CoursePrice = course.Price,
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
                })
                .GroupBy(x => x.Id)
                .Select(g => g.First())
                .ToListAsync();

            return data;
        }

        public async Task<IEnumerable<ClassDetailResponse>> GetAllClassDetailByTeacherIdUpcomming(int teacherId)
        {
            var today = DateTime.Today;

            var data = await (
                from c in _context.Classes
                join course in _context.Courses on c.CourseId equals course.Id
                join teacher in _context.Teachers on c.TeacherId equals teacher.Id
                join schedule in _context.Schedules
                    on c.Id equals schedule.ClassId into scheduleGroup
                    from schedule in scheduleGroup.DefaultIfEmpty()
                join classImg in _context.ClassImages on c.Id equals classImg.ClassId into imgGroup
                from classImg in imgGroup.DefaultIfEmpty()
                where teacher.Id == teacherId
                      && c.StartDate.Date > today
                      && course.Status == ECourseStatus.Active
                select new ClassDetailResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    ClassImage = classImg != null ? classImg.ClassImg : null,
                    CourseId = c.CourseId,
                    CourseName = course.Name,
                    CoursePrice = course.Price,
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
                })
                .GroupBy(x => x.Id)
                .Select(g => g.First())
                .ToListAsync();

            return data;
        }

        public async Task<IEnumerable<ClassDetailResponse>> GetAllClassDetailByTeacherIdFinished(int teacherId)
        {
            var today = DateTime.Today;

            var data = await (
                from c in _context.Classes
                join course in _context.Courses on c.CourseId equals course.Id
                join teacher in _context.Teachers on c.TeacherId equals teacher.Id
                join schedule in _context.Schedules
                    on c.Id equals schedule.ClassId into scheduleGroup
                    from schedule in scheduleGroup.DefaultIfEmpty()
                join classImg in _context.ClassImages on c.Id equals classImg.ClassId into imgGroup
                from classImg in imgGroup.DefaultIfEmpty()
                where teacher.Id == teacherId
                      && c.EndDate.Date < today
                select new ClassDetailResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    ClassImage = classImg != null ? classImg.ClassImg : null,
                    CourseId = c.CourseId,
                    CourseName = course.Name,
                    CoursePrice = course.Price,
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
                })
                .GroupBy(x => x.Id)
                .Select(g => g.First())
                .ToListAsync();

            return data;
        }

        public async Task<IEnumerable<ClassDetailResponse>> GetClassDetailTodayByStudentId(int studentId)
        {
            var today = DateTime.Today;

            // Convert System.DayOfWeek -> EDateOfWeek
            var todayEnum = (EDateOfWeek)today.DayOfWeek;

            var data = from enroll in _context.Enrollments
                       join c in _context.Classes on enroll.ClassId equals c.Id
                       join course in _context.Courses on c.CourseId equals course.Id
                       join teacher in _context.Teachers on c.TeacherId equals teacher.Id
                       join schedule in _context.Schedules
                            on c.Id equals schedule.ClassId into scheduleGroup
                            from schedule in scheduleGroup.DefaultIfEmpty()
                       where enroll.StudentId == studentId
                             && today >= c.StartDate.Date
                             && today <= c.EndDate.Date
                             && schedule.DayOfWeek == todayEnum
                             && course.Status == ECourseStatus.Active
                             && enroll.Status == EEnrollStatus.Active
                       select new ClassDetailResponse
                       {
                           Id = c.Id,
                           Name = c.Name,
                           CourseId = c.CourseId,
                           CourseName = course.Name,
                           CoursePrice = course.Price,
                           TeacherId = c.TeacherId,
                           TeacherName = teacher.FullName,
                           StartDate = c.StartDate,
                           EndDate = c.EndDate,
                           MaxStudents = c.MaxStudents,
                           Description = c.Description,

                           ScheduleId = schedule.Id,
                           DayOfWeek = schedule.DayOfWeek,
                           StartTime = schedule.StartTime,
                           EndTime = schedule.EndTime,
                           Room = schedule.Room
                       };

            return await data
                .OrderBy(x => x.StartTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClassDetailResponse>> GetAllClassDetailByStudentIdInProgress(int studentId)
        {
            var today = DateTime.Today;

            var data = await (
                from enroll in _context.Enrollments
                join c in _context.Classes on enroll.ClassId equals c.Id
                join course in _context.Courses on c.CourseId equals course.Id
                join teacher in _context.Teachers on c.TeacherId equals teacher.Id
                join schedule in _context.Schedules
                    on c.Id equals schedule.ClassId into scheduleGroup
                    from schedule in scheduleGroup.DefaultIfEmpty()
                join classImg in _context.ClassImages on c.Id equals classImg.ClassId into imgGroup
                from classImg in imgGroup.DefaultIfEmpty()
                where enroll.StudentId == studentId
                      && c.StartDate.Date <= today
                      && c.EndDate.Date >= today
                      && course.Status == ECourseStatus.Active
                      && enroll.Status == EEnrollStatus.Active
                select new ClassDetailResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    ClassImage = classImg != null ? classImg.ClassImg : null,
                    CourseId = c.CourseId,
                    CourseName = course.Name,
                    CoursePrice = course.Price,
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
                })
                .GroupBy(x => x.Id)
                .Select(g => g.First())
                .ToListAsync();

            return data;
        }

        public async Task<IEnumerable<ClassDetailResponse>> GetAllClassDetailByStudentIdUpcomming(int studentId)
        {
            var today = DateTime.Today;

            var data = await (
                from enroll in _context.Enrollments
                join c in _context.Classes on enroll.ClassId equals c.Id
                join course in _context.Courses on c.CourseId equals course.Id
                join teacher in _context.Teachers on c.TeacherId equals teacher.Id
                join schedule in _context.Schedules
                    on c.Id equals schedule.ClassId into scheduleGroup
                    from schedule in scheduleGroup.DefaultIfEmpty()
                join classImg in _context.ClassImages on c.Id equals classImg.ClassId into imgGroup
                from classImg in imgGroup.DefaultIfEmpty()
                where enroll.StudentId == studentId
                      && c.StartDate.Date > today
                      && course.Status == ECourseStatus.Active
                      && enroll.Status == EEnrollStatus.Active
                select new ClassDetailResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    ClassImage = classImg != null ? classImg.ClassImg : null,
                    CourseId = c.CourseId,
                    CourseName = course.Name,
                    CoursePrice = course.Price,
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
                })
                .GroupBy(x => x.Id)
                .Select(g => g.First())
                .ToListAsync();

            return data;
        }

        public async Task<IEnumerable<ClassDetailResponse>> GetAllClassDetailByStudentIdFinished(int studentId)
        {
            var today = DateTime.Today;

            var data = await (
                from enroll in _context.Enrollments
                join c in _context.Classes on enroll.ClassId equals c.Id
                join course in _context.Courses on c.CourseId equals course.Id
                join teacher in _context.Teachers on c.TeacherId equals teacher.Id
                join schedule in _context.Schedules
                    on c.Id equals schedule.ClassId into scheduleGroup
                    from schedule in scheduleGroup.DefaultIfEmpty()
                join classImg in _context.ClassImages on c.Id equals classImg.ClassId into imgGroup
                from classImg in imgGroup.DefaultIfEmpty()
                where enroll.StudentId == studentId
                      && c.EndDate.Date < today
                      && enroll.Status == EEnrollStatus.Active
                select new ClassDetailResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    ClassImage = classImg != null ? classImg.ClassImg : null,
                    CourseId = c.CourseId,
                    CourseName = course.Name,
                    CoursePrice = course.Price,
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
                })
                .GroupBy(x => x.Id)
                .Select(g => g.First())
                .ToListAsync();

            return data;
        }

        public async Task<IEnumerable<TeacherClassReviewResponse>> GetAllClassAndReviewDetailByStudentId(int studentId)
        {
            var today = DateTime.Today;

            var data = await (
                from enroll in _context.Enrollments
                join c in _context.Classes on enroll.ClassId equals c.Id
                join course in _context.Courses on c.CourseId equals course.Id

                // LEFT JOIN ClassImage
                join classImg in _context.ClassImages
                    on c.Id equals classImg.ClassId
                    into imgGroup
                from classImg in imgGroup.DefaultIfEmpty()

                    // LEFT JOIN TeacherReview
                join review in _context.TeachersReviews
                    on new { ClassId = c.Id, StudentId = studentId }
                    equals new { review.ClassId, review.StudentId }
                    into reviewGroup
                from review in reviewGroup.DefaultIfEmpty()

                where enroll.StudentId == studentId
                      && c.StartDate.Date < today
                      && enroll.Status == EEnrollStatus.Active

                select new TeacherClassReviewResponse
                {
                    Id = c.Id,
                    ClassId = c.Id,
                    ClassName = c.Name,
                    TeacherId = c.TeacherId,
                    StudentId = studentId,

                    ClassImage = classImg != null ? classImg.ClassImg : null,

                    TeacherReviewId = review != null ? review.Id : 0,
                    Rating = review != null ? review.Rating : 0,
                    Comment = review != null ? review.Comment : null,
                    CreatedAt = review != null ? review.CreatedAt : DateTime.MinValue,

                    CourseId = c.CourseId,
                    CourseName = c.Name,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    MaxStudents = c.MaxStudents,
                    Description = c.Description
                }
            )
            .GroupBy(x => x.ClassId)
            .Select(g => g.First())
            .ToListAsync();

            return data;
        }
        public async Task<IEnumerable<ClassDetailResponse>> GetAllClassDetailUpcomming()
        {
            var today = DateTime.Today;

            var data = await (
                from c in _context.Classes
                join course in _context.Courses on c.CourseId equals course.Id
                join teacher in _context.Teachers on c.TeacherId equals teacher.Id

                // sửa INNER JOIN -> LEFT JOIN
                join schedule in _context.Schedules on c.Id equals schedule.ClassId into scheduleGroup
                from schedule in scheduleGroup.DefaultIfEmpty()

                join classImg in _context.ClassImages on c.Id equals classImg.ClassId into imgGroup
                from classImg in imgGroup.DefaultIfEmpty()

                where c.StartDate.Date > today
                      && course.Status == ECourseStatus.Active

                select new ClassDetailResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    ClassImage = classImg != null ? classImg.ClassImg : null,
                    CourseId = c.CourseId,
                    CourseName = course.Name,
                    CoursePrice = course.Price,
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
                })
                .GroupBy(x => x.Id)
                .Select(g => g.First())
                .ToListAsync();

            return data;
        }
    }
}
