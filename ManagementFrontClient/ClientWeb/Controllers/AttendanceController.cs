using BLL.IServices;
using BLL.Request;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClientWeb.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IAttendanceService _attendanceService;
        private readonly IClassService _classService;
        private readonly ITeacherReviewService _reviewService;

        public AttendanceController(IAttendanceService attendanceService, IClassService classService, ITeacherReviewService reviewService)
        {
            _attendanceService = attendanceService;
            _classService = classService;
            _reviewService = reviewService;
        }

        public async Task<IActionResult> Index(int scheduleId, int classId)
        {
            var students = await _attendanceService.GetStudentsBySchedule(scheduleId);
            var classDetails = await _classService.GetClassById(classId);

            var teacherReviews = await _reviewService.GetByTeacher(classId);
            //ViewBag.Students = students;
            ViewBag.TeacherReviews = teacherReviews;
            ViewBag.ClassDetails = classDetails;
            ViewBag.ScheduleId = scheduleId;

            return View(students);
        }

        [HttpPost]
        public async Task<IActionResult> TakeAttendance([FromBody] AttendanceRequest request)
        {
            var result = await _attendanceService.TakeAttendance(request);

            if (result)
                return Ok();

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAttendance(int studentId, int classId)
        {
            var data = await _attendanceService.GetAllAttendanceDetails(studentId, classId);

            return Json(data);
        }
    }
}
