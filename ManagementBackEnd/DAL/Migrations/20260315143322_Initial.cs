using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    AttendanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaxStudents = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClassImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    ClassImg = table.Column<byte[]>(type: "image", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enrollment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    EnrollDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Room = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Experience = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeacherReview",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherReview", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Class",
                columns: new[] { "Id", "CourseId", "Description", "EndDate", "MaxStudents", "Name", "StartDate", "TeacherId" },
                values: new object[,]
                {
                    { 1, 1, "Lớp học cung cấp kiến thức cơ bản về lập trình C# bao gồm biến, kiểu dữ liệu, vòng lặp, câu lệnh điều kiện và các khái niệm lập trình hướng đối tượng.", new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, "C# cơ bản - Lớp sáng", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 2, "Khóa học giúp học viên nâng cao kỹ năng lập trình với LINQ, async/await, dependency injection và các design pattern thường dùng.", new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 25, "C# nâng cao - Lớp tối", new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, 3, "Học xây dựng ứng dụng web hiện đại với ASP.NET Core, MVC, Razor Pages và Web API.", new DateTime(2026, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 35, "Lập trình Web ASP.NET Core", new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 4, 4, "Học thiết kế cơ sở dữ liệu, viết truy vấn SQL, sử dụng join, stored procedure và tối ưu truy vấn.", new DateTime(2026, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 40, "Quản trị SQL Server", new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 5, 5, "Giới thiệu nguyên tắc Clean Architecture và cách áp dụng trong các dự án .NET thực tế.", new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, "Clean Architecture", new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 6, 1, "Lớp học C# cơ bản đã hoàn thành.", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, "C# cơ bản - Lớp 2024", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 7, 3, "Lớp ASP.NET Core đang diễn ra.", new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, "ASP.NET Core - Lớp hiện tại", new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 8, 5, "Lớp Clean Architecture sẽ mở trong tương lai.", new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, "Clean Architecture - Lớp tương lai", new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 9, 1, "Lớp học C# cơ bản dành cho ca chiều.", new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, "C# cơ bản - Lớp chiều", new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 10, 1, "Lớp học C# cơ bản ca tối cho người đi làm.", new DateTime(2026, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, "C# cơ bản - Lớp tối", new DateTime(2026, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 11, 2, "Chuyên đề nâng cao về LINQ, async await và performance.", new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 25, "C# nâng cao - Lớp chuyên đề", new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 12, 3, "Xây dựng RESTful API với ASP.NET Core.", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, "ASP.NET Core API", new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 13, 1, "Lớp học cuối tuần cho người bận rộn.", new DateTime(2026, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 35, "C# cơ bản - Lớp cuối tuần", new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.InsertData(
                table: "Course",
                columns: new[] { "Id", "Description", "Duration", "Name", "Price", "Status" },
                values: new object[,]
                {
                    { 1, "Học lập trình C# từ cơ bản", 30, "Lập trình C# cơ bản", 3000000m, 1 },
                    { 2, "Các kỹ thuật C# nâng cao", 45, "Lập trình C# nâng cao", 5000000m, 1 },
                    { 3, "Xây dựng Web API", 40, "Phát triển Web ASP.NET Core", 4500000m, 1 },
                    { 4, "Quản lý và truy vấn dữ liệu", 25, "Cơ sở dữ liệu SQL Server", 2500000m, 1 },
                    { 5, "Thiết kế kiến trúc phần mềm", 20, "Clean Architecture trong .NET", 4000000m, 0 }
                });

            migrationBuilder.InsertData(
                table: "Enrollment",
                columns: new[] { "Id", "ClassId", "EnrollDate", "Status", "StudentId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 2, 2, new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2 },
                    { 3, 3, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3 },
                    { 4, 4, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 4 },
                    { 5, 5, new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 5 },
                    { 6, 1, new DateTime(2024, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 6 },
                    { 7, 1, new DateTime(2024, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 7 },
                    { 8, 1, new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 8 },
                    { 9, 2, new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 9 },
                    { 10, 2, new DateTime(2025, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 10 },
                    { 11, 3, new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 11 },
                    { 12, 3, new DateTime(2025, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 12 },
                    { 13, 4, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 6 },
                    { 14, 4, new DateTime(2025, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 7 },
                    { 15, 5, new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 8 },
                    { 16, 9, new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 17, 9, new DateTime(2026, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2 },
                    { 18, 9, new DateTime(2026, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3 },
                    { 19, 9, new DateTime(2026, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 4 },
                    { 20, 10, new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 5 },
                    { 21, 10, new DateTime(2026, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 6 },
                    { 22, 10, new DateTime(2026, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 7 },
                    { 23, 10, new DateTime(2026, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 8 },
                    { 24, 11, new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 9 },
                    { 25, 11, new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 10 },
                    { 26, 11, new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 11 },
                    { 27, 11, new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 12 },
                    { 28, 12, new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 29, 12, new DateTime(2026, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2 },
                    { 30, 12, new DateTime(2026, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3 },
                    { 31, 13, new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 4 },
                    { 32, 13, new DateTime(2026, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 5 },
                    { 33, 13, new DateTime(2026, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 6 },
                    { 34, 13, new DateTime(2026, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 7 },
                    { 35, 13, new DateTime(2026, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 8 }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, 0 },
                    { 2, 1 },
                    { 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "Schedule",
                columns: new[] { "Id", "ClassId", "DayOfWeek", "EndTime", "Room", "StartTime" },
                values: new object[,]
                {
                    { 1, 1, 1, new TimeSpan(0, 10, 0, 0, 0), "A101", new TimeSpan(0, 8, 0, 0, 0) },
                    { 2, 2, 2, new TimeSpan(0, 20, 0, 0, 0), "B202", new TimeSpan(0, 18, 0, 0, 0) },
                    { 3, 3, 3, new TimeSpan(0, 11, 0, 0, 0), "C303", new TimeSpan(0, 9, 0, 0, 0) },
                    { 4, 4, 4, new TimeSpan(0, 16, 0, 0, 0), "D404", new TimeSpan(0, 14, 0, 0, 0) },
                    { 5, 5, 5, new TimeSpan(0, 21, 0, 0, 0), "E505", new TimeSpan(0, 19, 0, 0, 0) },
                    { 6, 1, 3, new TimeSpan(0, 10, 0, 0, 0), "A101", new TimeSpan(0, 8, 0, 0, 0) },
                    { 7, 2, 4, new TimeSpan(0, 20, 0, 0, 0), "B202", new TimeSpan(0, 18, 0, 0, 0) },
                    { 8, 3, 5, new TimeSpan(0, 11, 0, 0, 0), "C303", new TimeSpan(0, 9, 0, 0, 0) },
                    { 9, 7, 1, new TimeSpan(0, 15, 0, 0, 0), "D404", new TimeSpan(0, 13, 0, 0, 0) },
                    { 10, 7, 3, new TimeSpan(0, 15, 0, 0, 0), "D404", new TimeSpan(0, 13, 0, 0, 0) },
                    { 11, 6, 2, new TimeSpan(0, 10, 0, 0, 0), "A102", new TimeSpan(0, 8, 0, 0, 0) },
                    { 12, 6, 4, new TimeSpan(0, 10, 0, 0, 0), "A102", new TimeSpan(0, 8, 0, 0, 0) },
                    { 13, 8, 6, new TimeSpan(0, 11, 0, 0, 0), "E101", new TimeSpan(0, 9, 0, 0, 0) },
                    { 14, 8, 0, new TimeSpan(0, 11, 0, 0, 0), "E101", new TimeSpan(0, 9, 0, 0, 0) },
                    { 15, 9, 1, new TimeSpan(0, 16, 0, 0, 0), "A201", new TimeSpan(0, 14, 0, 0, 0) },
                    { 16, 9, 3, new TimeSpan(0, 16, 0, 0, 0), "A201", new TimeSpan(0, 14, 0, 0, 0) },
                    { 17, 10, 2, new TimeSpan(0, 21, 0, 0, 0), "B301", new TimeSpan(0, 19, 0, 0, 0) },
                    { 18, 10, 4, new TimeSpan(0, 21, 0, 0, 0), "B301", new TimeSpan(0, 19, 0, 0, 0) },
                    { 19, 11, 6, new TimeSpan(0, 11, 30, 0, 0), "C401", new TimeSpan(0, 8, 30, 0, 0) },
                    { 20, 12, 1, new TimeSpan(0, 20, 30, 0, 0), "D501", new TimeSpan(0, 18, 30, 0, 0) },
                    { 21, 12, 3, new TimeSpan(0, 20, 30, 0, 0), "D501", new TimeSpan(0, 18, 30, 0, 0) },
                    { 22, 13, 6, new TimeSpan(0, 17, 0, 0, 0), "E601", new TimeSpan(0, 14, 0, 0, 0) },
                    { 23, 13, 0, new TimeSpan(0, 17, 0, 0, 0), "E601", new TimeSpan(0, 14, 0, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "Id", "Address", "DateOfBirth", "FullName", "Gender", "UserId" },
                values: new object[,]
                {
                    { 1, "Hà Nội", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nguyễn Minh Đức", 0, 0 },
                    { 2, "TP Hồ Chí Minh", new DateTime(2001, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Trần Ngọc Anh", 1, 0 },
                    { 3, "Đà Nẵng", new DateTime(1999, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lê Hoàng Nam", 0, 0 },
                    { 4, "Huế", new DateTime(2002, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Phạm Thu Trang", 1, 0 },
                    { 5, "Cần Thơ", new DateTime(2000, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đỗ Văn Phúc", 0, 0 },
                    { 6, "Hà Nội", new DateTime(2001, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nguyễn Văn Thiện", 0, 0 },
                    { 7, "Hải Phòng", new DateTime(2000, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Trần Thị Bé", 1, 0 },
                    { 8, "Đà Nẵng", new DateTime(1998, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lê Văn Cảnh", 0, 0 },
                    { 9, "Cần Thơ", new DateTime(2002, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Phạm Thị Dung", 1, 0 },
                    { 10, "Huế", new DateTime(1999, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hoàng Văn Ôn", 0, 0 },
                    { 11, "Quảng Ninh", new DateTime(2003, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đặng Thị Huệ", 1, 0 },
                    { 12, "Bình Dương", new DateTime(2001, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bùi Văn Giang", 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "Teacher",
                columns: new[] { "Id", "Degree", "Experience", "FullName", "UserId" },
                values: new object[,]
                {
                    { 1, "Thạc sĩ Công nghệ thông tin", 5, "Nguyễn Văn An", 0 },
                    { 2, "Tiến sĩ Khoa học máy tính", 8, "Trần Thị Bích", 0 },
                    { 3, "Cử nhân Công nghệ thông tin", 3, "Lê Văn Cường", 0 },
                    { 4, "Thạc sĩ Hệ thống thông tin", 6, "Phạm Thị Dung", 0 },
                    { 5, "Tiến sĩ Công nghệ phần mềm", 10, "Hoàng Văn Hiếu", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "Class");

            migrationBuilder.DropTable(
                name: "ClassImages");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Enrollment");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Teacher");

            migrationBuilder.DropTable(
                name: "TeacherReview");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
