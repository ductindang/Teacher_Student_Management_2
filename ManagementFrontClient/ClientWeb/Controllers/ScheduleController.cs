using BLL.DTOs;
using BLL.DTOs.Enum;
using BLL.IServices;
using BLL.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientWeb.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;

        public ScheduleController(IScheduleService scheduleService, ITeacherService teacherService, IStudentService studentService)
        {
            _scheduleService = scheduleService;
            _teacherService = teacherService;
            _studentService = studentService;
        }

        public async Task<IActionResult> Index(DateTime? weekStart)
        {
            var userJson = HttpContext.Session.GetString("UserLogin");
            if (string.IsNullOrEmpty(userJson))
                return View();

            var user = JsonConvert.DeserializeObject<User>(userJson);

            // Lấy tuần hiện tại nếu chưa chọn
            var startOfWeek = weekStart ?? GetStartOfWeek(DateTime.Now);

            // Giới hạn 2 tháng trước và sau
            var minDate = DateTime.Now.AddMonths(-2);
            var maxDate = DateTime.Now.AddMonths(2);

            if (startOfWeek < minDate || startOfWeek > maxDate)
                return RedirectToAction("Index");

            IEnumerable<ClassDetailResponse> schedules;

            if (user.RoleId == (int)ERoleName.Teacher)
            {
                var teacher = await _teacherService.GetTeacherByUserId(user.Id);
                schedules = await _scheduleService.GetScheduleByTeacherId(teacher.Id);
            }
            else
            {
                var student = await _studentService.GetStudentByUserId(user.Id);
                schedules = await _scheduleService.GetScheduleByStudentId(student.Id);
            }

            ViewBag.WeekStart = startOfWeek;

            return View(schedules.ToList());
        }

        private DateTime GetStartOfWeek(DateTime date)
        {
            int diff = date.DayOfWeek - DayOfWeek.Monday;
            if (diff < 0) diff += 7;
            return date.AddDays(-1 * diff).Date;
        }
    }
}
