using BLL.DTOs;
using BLL.DTOs.Enum;
using BLL.IServices;
using BLL.Response;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientWeb.Controllers
{
    public class TeacherReviewController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IClassService _classService;
        private readonly ITeacherReviewService _reviewService;

        public TeacherReviewController(IStudentService studentService, IClassService classService, ITeacherReviewService reviewService)
        {
            _studentService = studentService;
            _classService = classService;
            _reviewService = reviewService;
        }

        public async Task<IActionResult> Index()
        {
            var userJson = HttpContext.Session.GetString("UserLogin");
            var user = new User();

            if (!string.IsNullOrEmpty(userJson))
            {
                user = JsonConvert.DeserializeObject<User>(userJson);
                IEnumerable<TeacherClassReviewResponse> classDetails = null;
                var student = await _studentService.GetStudentByUserId(user.Id);
                classDetails = await _classService.GetAllClassDetailAndReviewByStudentIdReview(student.Id);
                return View(classDetails);
            }
            return View();
        }

        public async Task<IActionResult> Insert(TeacherReview obj)
        {
            string resultMessage = "Lỗi không thể nhận xét cho lớp học này";
            obj.CreatedAt = DateTime.Now;
            if (!ModelState.IsValid)
            {
                TempData["Error"] = resultMessage;
                return RedirectToAction("Details", "Class", new { id = obj.ClassId });
            }
            try
            {
                var review = await _reviewService.InsertTeacherReview(obj);
                if (review != null)
                {
                    TempData["Success"] = "Thêm nhận xét cho lớp học thành công";
                    return RedirectToAction("Details", "Class", new { id = obj.ClassId });
                }
                TempData["Error"] = resultMessage;
                return RedirectToAction("Details", "Class", new { id = obj.ClassId });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                TempData["Error"] = resultMessage;
                return RedirectToAction("Details", "Class", new { id = obj.ClassId });
            }
        }

        public async Task<IActionResult> Update(TeacherReview obj)
        {
            var resultMessage = "Cập nhật nhận xét không thành công";
            try
            {
                var review = await _reviewService.UpdateTeacherReview(obj);
                if (review != null)
                {
                    TempData["Success"] = "Cập nhật nhận xét thành công thành công";
                    return RedirectToAction("Details", "Class", new { id = obj.ClassId });
                }
                TempData["Error"] = resultMessage;
                return RedirectToAction("Details", "Class", new { id = obj.ClassId });
            }
            catch (Exception ex)
            {
                TempData["Error"] = resultMessage;
                return RedirectToAction("Details", "Class", new { id = obj.ClassId });
            }
        }
    }
}
