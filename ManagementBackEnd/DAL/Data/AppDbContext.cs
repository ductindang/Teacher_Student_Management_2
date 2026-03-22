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
                 new Course { Id = 1, Name = "Lập trình Python cơ bản", Description = "Khóa học nhập môn Python dành cho người mới bắt đầu, bao gồm cú pháp, cấu trúc dữ liệu và lập trình cơ bản.", Duration = 30, Price = 3000000, Status = ECourseStatus.Active },

                 new Course { Id = 2, Name = "Phân tích dữ liệu với Excel", Description = "Sử dụng Excel để xử lý, phân tích dữ liệu và xây dựng báo cáo chuyên nghiệp.", Duration = 35, Price = 3200000, Status = ECourseStatus.Active },

                 new Course { Id = 3, Name = "Thiết kế đồ họa với Photoshop", Description = "Học chỉnh sửa hình ảnh và thiết kế ấn phẩm truyền thông bằng Photoshop.", Duration = 40, Price = 4500000, Status = ECourseStatus.Active },

                 new Course { Id = 4, Name = "Nguyên lý kế toán doanh nghiệp", Description = "Cung cấp kiến thức nền tảng về kế toán, báo cáo tài chính và quản lý chi phí.", Duration = 25, Price = 2800000, Status = ECourseStatus.Active },

                 new Course { Id = 5, Name = "Digital Marketing tổng thể", Description = "Tổng quan về marketing số bao gồm SEO, quảng cáo và xây dựng thương hiệu online.", Duration = 30, Price = 4000000, Status = ECourseStatus.Active }
             );

            modelBuilder.Entity<Teacher>().HasData(
                new Teacher { Id = 1, UserId = 0, FullName = "Nguyễn Minh Hoàng", Degree = "Thạc sĩ Khoa học dữ liệu", Experience = 6 },

                new Teacher { Id = 2, UserId = 0, FullName = "Trần Thị Ngọc Lan", Degree = "Thạc sĩ Tài chính - Ngân hàng", Experience = 7 },

                new Teacher { Id = 3, UserId = 0, FullName = "Lê Quang Huy", Degree = "Cử nhân Thiết kế đồ họa", Experience = 4 },

                new Teacher { Id = 4, UserId = 0, FullName = "Phạm Thu Hà", Degree = "Thạc sĩ Marketing", Experience = 8 },

                new Teacher { Id = 5, UserId = 0, FullName = "Đặng Quốc Trung", Degree = "Tiến sĩ Công nghệ thông tin", Experience = 10 }
            );

            modelBuilder.Entity<Class>().HasData(
                new Class { Id = 1, CourseId = 1, TeacherId = 1, Name = "Lập trình Python cơ bản", StartDate = new DateTime(2026, 1, 1), EndDate = new DateTime(2026, 2, 1), MaxStudents = 30, Description = "Khóa học nhập môn Python với các kiến thức về biến, vòng lặp, hàm và xử lý dữ liệu cơ bản." },

                new Class { Id = 2, CourseId = 2, TeacherId = 2, Name = "Phân tích dữ liệu với Excel", StartDate = new DateTime(2026, 1, 10), EndDate = new DateTime(2026, 2, 20), MaxStudents = 25, Description = "Học cách sử dụng Excel để phân tích dữ liệu, sử dụng hàm, PivotTable và biểu đồ." },

                new Class { Id = 3, CourseId = 3, TeacherId = 3, Name = "Thiết kế đồ họa với Photoshop", StartDate = new DateTime(2026, 2, 1), EndDate = new DateTime(2026, 3, 10), MaxStudents = 35, Description = "Hướng dẫn sử dụng Photoshop để chỉnh sửa ảnh và thiết kế banner, poster chuyên nghiệp." },

                new Class { Id = 4, CourseId = 4, TeacherId = 4, Name = "Nguyên lý kế toán cơ bản", StartDate = new DateTime(2026, 1, 5), EndDate = new DateTime(2026, 2, 5), MaxStudents = 40, Description = "Trang bị kiến thức nền tảng về kế toán, sổ sách và báo cáo tài chính." },

                new Class { Id = 5, CourseId = 5, TeacherId = 5, Name = "Digital Marketing tổng thể", StartDate = new DateTime(2026, 3, 1), EndDate = new DateTime(2026, 4, 1), MaxStudents = 20, Description = "Tìm hiểu về SEO, quảng cáo Facebook, Google Ads và xây dựng chiến lược marketing." },

                new Class { Id = 6, CourseId = 1, TeacherId = 2, Name = "Python cơ bản - Khóa 2024", StartDate = new DateTime(2024, 1, 1), EndDate = new DateTime(2024, 2, 1), MaxStudents = 30, Description = "Khóa học Python dành cho người mới bắt đầu đã hoàn thành." },

                new Class { Id = 7, CourseId = 3, TeacherId = 3, Name = "Thiết kế UI/UX cơ bản", StartDate = new DateTime(2026, 3, 1), EndDate = new DateTime(2026, 4, 15), MaxStudents = 30, Description = "Học cách thiết kế giao diện và trải nghiệm người dùng với Figma." },

                new Class { Id = 8, CourseId = 5, TeacherId = 4, Name = "Content Marketing thực chiến", StartDate = new DateTime(2026, 6, 1), EndDate = new DateTime(2026, 7, 1), MaxStudents = 20, Description = "Kỹ năng viết content thu hút và xây dựng thương hiệu trên mạng xã hội." },

                new Class { Id = 9, CourseId = 2, TeacherId = 1, Name = "Excel nâng cao cho doanh nghiệp", StartDate = new DateTime(2026, 2, 1), EndDate = new DateTime(2026, 5, 1), MaxStudents = 30, Description = "Áp dụng Excel trong quản lý dữ liệu và báo cáo doanh nghiệp." },

                new Class { Id = 10, CourseId = 4, TeacherId = 1, Name = "Phân tích tài chính doanh nghiệp", StartDate = new DateTime(2026, 3, 10), EndDate = new DateTime(2026, 4, 10), MaxStudents = 30, Description = "Phân tích báo cáo tài chính và đánh giá hiệu quả kinh doanh." },

                new Class { Id = 11, CourseId = 5, TeacherId = 1, Name = "Quảng cáo Facebook Ads chuyên sâu", StartDate = new DateTime(2026, 4, 1), EndDate = new DateTime(2026, 5, 10), MaxStudents = 25, Description = "Thiết lập và tối ưu chiến dịch quảng cáo Facebook hiệu quả." },

                new Class { Id = 12, CourseId = 1, TeacherId = 1, Name = "Lập trình Python cho AI cơ bản", StartDate = new DateTime(2026, 5, 1), EndDate = new DateTime(2026, 6, 10), MaxStudents = 30, Description = "Giới thiệu Python trong trí tuệ nhân tạo và xử lý dữ liệu." },

                new Class { Id = 13, CourseId = 3, TeacherId = 1, Name = "Thiết kế đồ họa - Lớp cuối tuần", StartDate = new DateTime(2026, 6, 15), EndDate = new DateTime(2026, 7, 20), MaxStudents = 35, Description = "Lớp học cuối tuần dành cho người đi làm yêu thích thiết kế." }
            );

            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, UserId = 0, FullName = "Phan Quốc Bảo", Gender = EGender.Male, Address = "Hà Nội", DateOfBirth = new DateTime(1998, 2, 14) },
                new Student { Id = 2, UserId = 0, FullName = "Ngô Thị Mai", Gender = EGender.Female, Address = "TP Hồ Chí Minh", DateOfBirth = new DateTime(2000, 6, 21) },
                new Student { Id = 3, UserId = 0, FullName = "Trương Gia Huy", Gender = EGender.Male, Address = "Đà Nẵng", DateOfBirth = new DateTime(1997, 9, 5) },
                new Student { Id = 4, UserId = 0, FullName = "Võ Thị Thanh Tâm", Gender = EGender.Female, Address = "Huế", DateOfBirth = new DateTime(2003, 1, 19) },
                new Student { Id = 5, UserId = 0, FullName = "Dương Minh Khôi", Gender = EGender.Male, Address = "Cần Thơ", DateOfBirth = new DateTime(1999, 11, 30) },
                new Student { Id = 6, UserId = 0, FullName = "Lý Văn Tuấn", Gender = EGender.Male, Address = "Hà Nội", DateOfBirth = new DateTime(2002, 4, 8) },
                new Student { Id = 7, UserId = 0, FullName = "Đinh Thị Hồng", Gender = EGender.Female, Address = "Hải Phòng", DateOfBirth = new DateTime(2001, 7, 25) },
                new Student { Id = 8, UserId = 0, FullName = "Tạ Quang Vinh", Gender = EGender.Male, Address = "Đà Nẵng", DateOfBirth = new DateTime(1996, 12, 2) },
                new Student { Id = 9, UserId = 0, FullName = "Mai Thị Ngọc", Gender = EGender.Female, Address = "Cần Thơ", DateOfBirth = new DateTime(2003, 3, 17) },
                new Student { Id = 10, UserId = 0, FullName = "Phùng Văn Hòa", Gender = EGender.Male, Address = "Huế", DateOfBirth = new DateTime(1998, 8, 9) },
                new Student { Id = 11, UserId = 0, FullName = "Hồ Thị Lan Anh", Gender = EGender.Female, Address = "Quảng Ninh", DateOfBirth = new DateTime(2004, 5, 12) },
                new Student { Id = 12, UserId = 0, FullName = "Cao Văn Sơn", Gender = EGender.Male, Address = "Bình Dương", DateOfBirth = new DateTime(2000, 10, 27) }
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
