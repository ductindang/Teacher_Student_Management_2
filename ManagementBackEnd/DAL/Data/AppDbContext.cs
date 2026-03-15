using DAL.Models;
using DAL.Models.Enum;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassImage> ClassImages { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<TeacherReview> TeachersReviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Name = "Lập trình C# cơ bản", Description = "Học lập trình C# từ cơ bản", Duration = 30, Price = 3000000, Status = ECourseStatus.Active },
                new Course { Id = 2, Name = "Lập trình C# nâng cao", Description = "Các kỹ thuật C# nâng cao", Duration = 45, Price = 5000000, Status = ECourseStatus.Active },
                new Course { Id = 3, Name = "Phát triển Web ASP.NET Core", Description = "Xây dựng Web API", Duration = 40, Price = 4500000, Status = ECourseStatus.Active },
                new Course { Id = 4, Name = "Cơ sở dữ liệu SQL Server", Description = "Quản lý và truy vấn dữ liệu", Duration = 25, Price = 2500000, Status = ECourseStatus.Active },
                new Course { Id = 5, Name = "Clean Architecture trong .NET", Description = "Thiết kế kiến trúc phần mềm", Duration = 20, Price = 4000000, Status = ECourseStatus.InActive }
            );

            modelBuilder.Entity<Teacher>().HasData(
                new Teacher { Id = 1, UserId = 0, FullName = "Nguyễn Văn An", Degree = "Thạc sĩ Công nghệ thông tin", Experience = 5 },
                new Teacher { Id = 2, UserId = 0, FullName = "Trần Thị Bích", Degree = "Tiến sĩ Khoa học máy tính", Experience = 8 },
                new Teacher { Id = 3, UserId = 0, FullName = "Lê Văn Cường", Degree = "Cử nhân Công nghệ thông tin", Experience = 3 },
                new Teacher { Id = 4, UserId = 0, FullName = "Phạm Thị Dung", Degree = "Thạc sĩ Hệ thống thông tin", Experience = 6 },
                new Teacher { Id = 5, UserId = 0, FullName = "Hoàng Văn Hiếu", Degree = "Tiến sĩ Công nghệ phần mềm", Experience = 10 }
            );

            modelBuilder.Entity<Class>().HasData(
                new Class { Id = 1, CourseId = 1, TeacherId = 1, Name = "C# cơ bản - Lớp sáng", StartDate = new DateTime(2026, 1, 1), EndDate = new DateTime(2026, 2, 1), MaxStudents = 30, Description = "Lớp học cung cấp kiến thức cơ bản về lập trình C# bao gồm biến, kiểu dữ liệu, vòng lặp, câu lệnh điều kiện và các khái niệm lập trình hướng đối tượng." },

                new Class { Id = 2, CourseId = 2, TeacherId = 2, Name = "C# nâng cao - Lớp tối", StartDate = new DateTime(2026, 1, 10), EndDate = new DateTime(2026, 3, 1), MaxStudents = 25, Description = "Khóa học giúp học viên nâng cao kỹ năng lập trình với LINQ, async/await, dependency injection và các design pattern thường dùng." },

                new Class { Id = 3, CourseId = 3, TeacherId = 3, Name = "Lập trình Web ASP.NET Core", StartDate = new DateTime(2026, 2, 1), EndDate = new DateTime(2026, 3, 15), MaxStudents = 35, Description = "Học xây dựng ứng dụng web hiện đại với ASP.NET Core, MVC, Razor Pages và Web API." },

                new Class { Id = 4, CourseId = 4, TeacherId = 4, Name = "Quản trị SQL Server", StartDate = new DateTime(2026, 1, 5), EndDate = new DateTime(2026, 2, 5), MaxStudents = 40, Description = "Học thiết kế cơ sở dữ liệu, viết truy vấn SQL, sử dụng join, stored procedure và tối ưu truy vấn." },

                new Class { Id = 5, CourseId = 5, TeacherId = 5, Name = "Clean Architecture", StartDate = new DateTime(2026, 3, 1), EndDate = new DateTime(2026, 4, 1), MaxStudents = 20, Description = "Giới thiệu nguyên tắc Clean Architecture và cách áp dụng trong các dự án .NET thực tế." },
                new Class { Id = 6, CourseId = 1, TeacherId = 2, Name = "C# cơ bản - Lớp 2024", StartDate = new DateTime(2024, 1, 1), EndDate = new DateTime(2024, 2, 1), MaxStudents = 30, Description = "Lớp học C# cơ bản đã hoàn thành." },

                new Class { Id = 7, CourseId = 3, TeacherId = 3, Name = "ASP.NET Core - Lớp hiện tại", StartDate = new DateTime(2026, 3, 1), EndDate = new DateTime(2026, 4, 15), MaxStudents = 30, Description = "Lớp ASP.NET Core đang diễn ra." },

                new Class { Id = 8, CourseId = 5, TeacherId = 4, Name = "Clean Architecture - Lớp tương lai", StartDate = new DateTime(2026, 6, 1), EndDate = new DateTime(2026, 7, 1), MaxStudents = 20, Description = "Lớp Clean Architecture sẽ mở trong tương lai." },
                new Class { Id = 9, CourseId = 1, TeacherId = 1, Name = "C# cơ bản - Lớp chiều", StartDate = new DateTime(2026, 2, 1), EndDate = new DateTime(2026, 5, 1), MaxStudents = 30, Description = "Lớp học C# cơ bản dành cho ca chiều." },

                new Class { Id = 10, CourseId = 1, TeacherId = 1, Name = "C# cơ bản - Lớp tối", StartDate = new DateTime(2026, 3, 10), EndDate = new DateTime(2026, 4, 10), MaxStudents = 30, Description = "Lớp học C# cơ bản ca tối cho người đi làm." },

                new Class { Id = 11, CourseId = 2, TeacherId = 1, Name = "C# nâng cao - Lớp chuyên đề", StartDate = new DateTime(2026, 4, 1), EndDate = new DateTime(2026, 5, 10), MaxStudents = 25, Description = "Chuyên đề nâng cao về LINQ, async await và performance." },

                new Class { Id = 12, CourseId = 3, TeacherId = 1, Name = "ASP.NET Core API", StartDate = new DateTime(2026, 5, 1), EndDate = new DateTime(2026, 6, 10), MaxStudents = 30, Description = "Xây dựng RESTful API với ASP.NET Core." },

                new Class { Id = 13, CourseId = 1, TeacherId = 1, Name = "C# cơ bản - Lớp cuối tuần", StartDate = new DateTime(2026, 6, 15), EndDate = new DateTime(2026, 7, 20), MaxStudents = 35, Description = "Lớp học cuối tuần cho người bận rộn." }
            );

            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, UserId = 0, FullName = "Nguyễn Minh Đức", Gender = EGender.Male, Address = "Hà Nội", DateOfBirth = new DateTime(2000, 1, 1) },
                new Student { Id = 2, UserId = 0, FullName = "Trần Ngọc Anh", Gender = EGender.Female, Address = "TP Hồ Chí Minh", DateOfBirth = new DateTime(2001, 2, 2) },
                new Student { Id = 3, UserId = 0, FullName = "Lê Hoàng Nam", Gender = EGender.Male, Address = "Đà Nẵng", DateOfBirth = new DateTime(1999, 3, 3) },
                new Student { Id = 4, UserId = 0, FullName = "Phạm Thu Trang", Gender = EGender.Female, Address = "Huế", DateOfBirth = new DateTime(2002, 4, 4) },
                new Student { Id = 5, UserId = 0, FullName = "Đỗ Văn Phúc", Gender = EGender.Male, Address = "Cần Thơ", DateOfBirth = new DateTime(2000, 5, 5) },
                new Student { Id = 6, UserId = 0, FullName = "Nguyễn Văn Thiện", Gender = EGender.Male, Address = "Hà Nội", DateOfBirth = new DateTime(2001, 6, 10) },
                new Student { Id = 7, UserId = 0, FullName = "Trần Thị Bé", Gender = EGender.Female, Address = "Hải Phòng", DateOfBirth = new DateTime(2000, 7, 11) },
                new Student { Id = 8, UserId = 0, FullName = "Lê Văn Cảnh", Gender = EGender.Male, Address = "Đà Nẵng", DateOfBirth = new DateTime(1998, 8, 12) },
                new Student { Id = 9, UserId = 0, FullName = "Phạm Thị Dung", Gender = EGender.Female, Address = "Cần Thơ", DateOfBirth = new DateTime(2002, 9, 13) },
                new Student { Id = 10, UserId = 0, FullName = "Hoàng Văn Ôn", Gender = EGender.Male, Address = "Huế", DateOfBirth = new DateTime(1999, 10, 14) },
                new Student { Id = 11, UserId = 0, FullName = "Đặng Thị Huệ", Gender = EGender.Female, Address = "Quảng Ninh", DateOfBirth = new DateTime(2003, 11, 15) },
                new Student { Id = 12, UserId = 0, FullName = "Bùi Văn Giang", Gender = EGender.Male, Address = "Bình Dương", DateOfBirth = new DateTime(2001, 12, 16) }
            );

            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment { Id = 1, StudentId = 1, ClassId = 1, EnrollDate = new DateTime(2025, 1, 1), Status = EEnrollStatus.Active },
                new Enrollment { Id = 2, StudentId = 2, ClassId = 2, EnrollDate = new DateTime(2025, 1, 5), Status = EEnrollStatus.Active },
                new Enrollment { Id = 3, StudentId = 3, ClassId = 3, EnrollDate = new DateTime(2025, 2, 1), Status = EEnrollStatus.Cancel },
                new Enrollment { Id = 4, StudentId = 4, ClassId = 4, EnrollDate = new DateTime(2025, 1, 10), Status = EEnrollStatus.Active },
                new Enrollment { Id = 5, StudentId = 5, ClassId = 5, EnrollDate = new DateTime(2025, 3, 1), Status = EEnrollStatus.Active },
                new Enrollment { Id = 6, StudentId = 6, ClassId = 1, EnrollDate = new DateTime(2024, 12, 20), Status = EEnrollStatus.Active },
                new Enrollment { Id = 7, StudentId = 7, ClassId = 1, EnrollDate = new DateTime(2024, 12, 22), Status = EEnrollStatus.Active },
                new Enrollment { Id = 8, StudentId = 8, ClassId = 1, EnrollDate = new DateTime(2024, 12, 25), Status = EEnrollStatus.Active },

                new Enrollment { Id = 9, StudentId = 9, ClassId = 2, EnrollDate = new DateTime(2025, 1, 3), Status = EEnrollStatus.Active },
                new Enrollment { Id = 10, StudentId = 10, ClassId = 2, EnrollDate = new DateTime(2025, 1, 4), Status = EEnrollStatus.Active },

                new Enrollment { Id = 11, StudentId = 11, ClassId = 3, EnrollDate = new DateTime(2025, 2, 5), Status = EEnrollStatus.Active },
                new Enrollment { Id = 12, StudentId = 12, ClassId = 3, EnrollDate = new DateTime(2025, 2, 6), Status = EEnrollStatus.Cancel },

                new Enrollment { Id = 13, StudentId = 6, ClassId = 4, EnrollDate = new DateTime(2025, 1, 15), Status = EEnrollStatus.Active },
                new Enrollment { Id = 14, StudentId = 7, ClassId = 4, EnrollDate = new DateTime(2025, 1, 16), Status = EEnrollStatus.Active },

                new Enrollment { Id = 15, StudentId = 8, ClassId = 5, EnrollDate = new DateTime(2025, 3, 5), Status = EEnrollStatus.Active },
                new Enrollment { Id = 16, StudentId = 1, ClassId = 9, EnrollDate = new DateTime(2026, 1, 10), Status = EEnrollStatus.Active },
                new Enrollment { Id = 17, StudentId = 2, ClassId = 9, EnrollDate = new DateTime(2026, 1, 11), Status = EEnrollStatus.Active },
                new Enrollment { Id = 18, StudentId = 3, ClassId = 9, EnrollDate = new DateTime(2026, 1, 12), Status = EEnrollStatus.Active },
                new Enrollment { Id = 19, StudentId = 4, ClassId = 9, EnrollDate = new DateTime(2026, 1, 13), Status = EEnrollStatus.Active },

                new Enrollment { Id = 20, StudentId = 5, ClassId = 10, EnrollDate = new DateTime(2026, 2, 1), Status = EEnrollStatus.Active },
                new Enrollment { Id = 21, StudentId = 6, ClassId = 10, EnrollDate = new DateTime(2026, 2, 2), Status = EEnrollStatus.Active },
                new Enrollment { Id = 22, StudentId = 7, ClassId = 10, EnrollDate = new DateTime(2026, 2, 3), Status = EEnrollStatus.Active },
                new Enrollment { Id = 23, StudentId = 8, ClassId = 10, EnrollDate = new DateTime(2026, 2, 4), Status = EEnrollStatus.Active },

                new Enrollment { Id = 24, StudentId = 9, ClassId = 11, EnrollDate = new DateTime(2026, 3, 5), Status = EEnrollStatus.Active },
                new Enrollment { Id = 25, StudentId = 10, ClassId = 11, EnrollDate = new DateTime(2026, 3, 6), Status = EEnrollStatus.Active },
                new Enrollment { Id = 26, StudentId = 11, ClassId = 11, EnrollDate = new DateTime(2026, 3, 7), Status = EEnrollStatus.Active },
                new Enrollment { Id = 27, StudentId = 12, ClassId = 11, EnrollDate = new DateTime(2026, 3, 8), Status = EEnrollStatus.Active },

                new Enrollment { Id = 28, StudentId = 1, ClassId = 12, EnrollDate = new DateTime(2026, 4, 1), Status = EEnrollStatus.Active },
                new Enrollment { Id = 29, StudentId = 2, ClassId = 12, EnrollDate = new DateTime(2026, 4, 2), Status = EEnrollStatus.Active },
                new Enrollment { Id = 30, StudentId = 3, ClassId = 12, EnrollDate = new DateTime(2026, 4, 3), Status = EEnrollStatus.Active },

                new Enrollment { Id = 31, StudentId = 4, ClassId = 13, EnrollDate = new DateTime(2026, 5, 1), Status = EEnrollStatus.Active },
                new Enrollment { Id = 32, StudentId = 5, ClassId = 13, EnrollDate = new DateTime(2026, 5, 2), Status = EEnrollStatus.Active },
                new Enrollment { Id = 33, StudentId = 6, ClassId = 13, EnrollDate = new DateTime(2026, 5, 3), Status = EEnrollStatus.Active },
                new Enrollment { Id = 34, StudentId = 7, ClassId = 13, EnrollDate = new DateTime(2026, 5, 4), Status = EEnrollStatus.Active },
                new Enrollment { Id = 35, StudentId = 8, ClassId = 13, EnrollDate = new DateTime(2026, 5, 5), Status = EEnrollStatus.Active }
            );

            modelBuilder.Entity<Schedule>().HasData(
                new Schedule { Id = 1, ClassId = 1, DayOfWeek = EDateOfWeek.Monday, StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(10, 0, 0), Room = "A101" },
                new Schedule { Id = 2, ClassId = 2, DayOfWeek = EDateOfWeek.Tuesday, StartTime = new TimeSpan(18, 0, 0), EndTime = new TimeSpan(20, 0, 0), Room = "B202" },
                new Schedule { Id = 3, ClassId = 3, DayOfWeek = EDateOfWeek.Wednesday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(11, 0, 0), Room = "C303" },
                new Schedule { Id = 4, ClassId = 4, DayOfWeek = EDateOfWeek.Thursday, StartTime = new TimeSpan(14, 0, 0), EndTime = new TimeSpan(16, 0, 0), Room = "D404" },
                new Schedule { Id = 5, ClassId = 5, DayOfWeek = EDateOfWeek.Friday, StartTime = new TimeSpan(19, 0, 0), EndTime = new TimeSpan(21, 0, 0), Room = "E505" },
                new Schedule { Id = 6, ClassId = 1, DayOfWeek = EDateOfWeek.Wednesday, StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(10, 0, 0), Room = "A101" },

                new Schedule { Id = 7, ClassId = 2, DayOfWeek = EDateOfWeek.Thursday, StartTime = new TimeSpan(18, 0, 0), EndTime = new TimeSpan(20, 0, 0), Room = "B202" },

                new Schedule { Id = 8, ClassId = 3, DayOfWeek = EDateOfWeek.Friday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(11, 0, 0), Room = "C303" },

                new Schedule { Id = 9, ClassId = 7, DayOfWeek = EDateOfWeek.Monday, StartTime = new TimeSpan(13, 0, 0), EndTime = new TimeSpan(15, 0, 0), Room = "D404" },

                new Schedule { Id = 10, ClassId = 7, DayOfWeek = EDateOfWeek.Wednesday, StartTime = new TimeSpan(13, 0, 0), EndTime = new TimeSpan(15, 0, 0), Room = "D404" },
                new Schedule { Id = 11, ClassId = 6, DayOfWeek = EDateOfWeek.Tuesday, StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(10, 0, 0), Room = "A102" },
                new Schedule { Id = 12, ClassId = 6, DayOfWeek = EDateOfWeek.Thursday, StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(10, 0, 0), Room = "A102" },

                new Schedule { Id = 13, ClassId = 8, DayOfWeek = EDateOfWeek.Saturday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(11, 0, 0), Room = "E101" },
                new Schedule { Id = 14, ClassId = 8, DayOfWeek = EDateOfWeek.Sunday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(11, 0, 0), Room = "E101" },

                new Schedule { Id = 15, ClassId = 9, DayOfWeek = EDateOfWeek.Monday, StartTime = new TimeSpan(14, 0, 0), EndTime = new TimeSpan(16, 0, 0), Room = "A201" },
                new Schedule { Id = 16, ClassId = 9, DayOfWeek = EDateOfWeek.Wednesday, StartTime = new TimeSpan(14, 0, 0), EndTime = new TimeSpan(16, 0, 0), Room = "A201" },

                new Schedule { Id = 17, ClassId = 10, DayOfWeek = EDateOfWeek.Tuesday, StartTime = new TimeSpan(19, 0, 0), EndTime = new TimeSpan(21, 0, 0), Room = "B301" },
                new Schedule { Id = 18, ClassId = 10, DayOfWeek = EDateOfWeek.Thursday, StartTime = new TimeSpan(19, 0, 0), EndTime = new TimeSpan(21, 0, 0), Room = "B301" },

                new Schedule { Id = 19, ClassId = 11, DayOfWeek = EDateOfWeek.Saturday, StartTime = new TimeSpan(8, 30, 0), EndTime = new TimeSpan(11, 30, 0), Room = "C401" },

                new Schedule { Id = 20, ClassId = 12, DayOfWeek = EDateOfWeek.Monday, StartTime = new TimeSpan(18, 30, 0), EndTime = new TimeSpan(20, 30, 0), Room = "D501" },
                new Schedule { Id = 21, ClassId = 12, DayOfWeek = EDateOfWeek.Wednesday, StartTime = new TimeSpan(18, 30, 0), EndTime = new TimeSpan(20, 30, 0), Room = "D501" },

                new Schedule { Id = 22, ClassId = 13, DayOfWeek = EDateOfWeek.Saturday, StartTime = new TimeSpan(14, 0, 0), EndTime = new TimeSpan(17, 0, 0), Room = "E601" },
                new Schedule { Id = 23, ClassId = 13, DayOfWeek = EDateOfWeek.Sunday, StartTime = new TimeSpan(14, 0, 0), EndTime = new TimeSpan(17, 0, 0), Room = "E601" }
            );

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = ERoleName.Student },
                new Role { Id = 2, Name = ERoleName.Teacher },
                new Role { Id = 3, Name = ERoleName.Admin }
            );

            modelBuilder.Entity<Course>()
                .Property(x => x.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Payment>()
                .Property(x => x.Amount)
                .HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);
        }
    }
}
