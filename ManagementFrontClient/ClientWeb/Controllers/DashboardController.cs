using BLL.DTOs;
using BLL.DTOs.Enum;
using BLL.IServices;
using BLL.Response;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientWeb.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IClassService _classService;
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;

        public DashboardController(IClassService classService, ITeacherService teacherService, IStudentService studentService)
        {
            _classService = classService;
            _teacherService = teacherService;
            _studentService = studentService;
        }

        public async Task<IActionResult> Index()
        {
            var userJson = HttpContext.Session.GetString("UserLogin");
            var user = new User();

            if (!string.IsNullOrEmpty(userJson))
            {
                user = JsonConvert.DeserializeObject<User>(userJson);
                IEnumerable<ClassDetailResponse> classDetails = null;
                if (user.RoleId == (int)ERoleName.Teacher)
                {
                    var teacher = await _teacherService.GetTeacherByUserId(user.Id);
                    classDetails = await _classService.GetClassDetailTodayByTeacherId(teacher.Id);
                }
                else if(user.RoleId == (int)ERoleName.Student)
                {
                    var student = await _studentService.GetStudentByUserId(user.Id);
                    classDetails = await _classService.GetClassDetailTodayByStudentId(student.Id);
                }
                return View(classDetails);
            }
            
            return View(null);
        }
    }
}
