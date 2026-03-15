using BLL.DTOs;
using BLL.DTOs.Enum;
using BLL.IServices;
using BLL.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientWeb.Controllers
{
    public class ClassController : Controller
    {
        private readonly IClassService _classService;
        private readonly IStudentService _studentService;
        private readonly ITeacherReviewService _reviewService;
        private readonly ITeacherService _teacherService;
        private readonly IEnrollmentService _enrollmentService;

        public ClassController(IClassService classService, IStudentService studentService, ITeacherReviewService reviewService, ITeacherService teacherService, IEnrollmentService enrollmentService)
        {
            _classService = classService;
            _studentService = studentService;
            _reviewService = reviewService;
            _teacherService = teacherService;
            _enrollmentService = enrollmentService;
        }

        public async Task<IActionResult> Index()
        {
            var userJson = HttpContext.Session.GetString("UserLogin");
            var user = new User();

            if (!string.IsNullOrEmpty(userJson))
            {
                user = JsonConvert.DeserializeObject<User>(userJson);
                IEnumerable<ClassDetailResponse> classDetails = null;
                if(user.RoleId == (int)ERoleName.Teacher)
                {
                    var teacher = await _teacherService.GetTeacherByUserId(user.Id);
                    classDetails = await _classService.GetAllClassDetailByTeacherIdInProgress(teacher.Id);
                }
                else if(user.RoleId == (int)ERoleName.Student)
                {
                    var student = await _studentService.GetStudentByUserId(user.Id);
                    classDetails = await _classService.GetAllClassDetailByStudentIdInProgress(student.Id);
                }

                return View(classDetails);
            }
            return View();
        }

        public async Task<IActionResult> UpcommingClass()
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
                    classDetails = await _classService.GetAllClassDetailByTeacherIdUpcoming(teacher.Id);
                }
                else if (user.RoleId == (int)ERoleName.Student)
                {
                    classDetails = await _classService.GetAllClassDetailUpcoming();
                }

                return PartialView("_ClassList", classDetails);
            }
            return PartialView("_ClassList", new List<ClassDetailResponse>());
        }

        public async Task<IActionResult> FinishedClass()
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
                    classDetails = await _classService.GetAllClassDetailByTeacherIdFinished(teacher.Id);
                }
                else if (user.RoleId == (int)ERoleName.Student)
                {
                    var student = await _studentService.GetStudentByUserId(user.Id);
                    classDetails = await _classService.GetAllClassDetailByStudentIdFinished(student.Id);
                }

                return PartialView("_ClassList", classDetails);
            }
            return PartialView("_ClassList", new List<ClassDetailResponse>());
        }

        public async Task<IActionResult> Details(int id)
        {
            var classDetail = await _classService.GetClassById(id);
            if (classDetail == null)
            {
                return NotFound();
            }

            var userJson = HttpContext.Session.GetString("UserLogin");
            var user = new User();

            if (!string.IsNullOrEmpty(userJson))
            {
                user = JsonConvert.DeserializeObject<User>(userJson);
                IEnumerable<ClassDetailResponse> classDetails = null;
                if (user.RoleId == (int)ERoleName.Teacher)
                {
                    var students = await _studentService.GetStudentsByClass(id);
                    var teacherReviews = await _reviewService.GetByTeacher(classDetail.TeacherId);
                    ViewBag.Students = students;
                    ViewBag.TeacherReviews = teacherReviews;
                }
                else if (user.RoleId == (int)ERoleName.Student)
                {
                    var student = await _studentService.GetStudentByUserId(user.Id);
                    var enrollment = await _enrollmentService.GetEnrollmentByClassStudent(id, student.Id);
                    if(enrollment != null)
                    {
                        var teaReview = await _reviewService.GetByTeacherStudentClass(student.Id, enrollment.ClassId);
                        ViewBag.TeaReview = teaReview;
                    }
                    
                    var teacherReviews = await _reviewService.GetByTeacher(id);
                    ViewBag.StudentObj = student;
                    ViewBag.Enrollment = enrollment;
                    ViewBag.TeacherReviews = teacherReviews;
                }
            } 
                
            return View(classDetail);
        }
    }
}
