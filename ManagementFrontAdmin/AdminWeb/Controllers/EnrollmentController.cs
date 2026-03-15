using BLL.DTOs;
using BLL.IServices;
using Microsoft.AspNetCore.Mvc;

namespace AdminWeb.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly IClassService _classService;
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;

        public EnrollmentController(
            IEnrollmentService enrollmentService,
            IClassService classService,
            IStudentService studentService,
            ICourseService courseService)
        {
            _enrollmentService = enrollmentService;
            _classService = classService;
            _studentService = studentService;
            _courseService = courseService;
        }

        public async Task<IActionResult> Index()
        {
            var enrollments = await _enrollmentService.GetAllEnrollments();
            return View(enrollments);
        }

        public async Task<IActionResult> Create()
        {
            await LoadData();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Enrollment obj)
        {
            if (!ModelState.IsValid)
            {
                await LoadData();
                return View(obj);
            }
            string resultMessage = "Lỗi không thể thêm đăng ký lớp này";
            try
            {
                var enrollment = await _enrollmentService.InsertEnrollment(obj);
                if (enrollment != null)
                {
                    TempData["Success"] = "Thêm mới đăng ký lớp thành công";
                    return RedirectToAction("Index");
                }
                await LoadData();
                TempData["Error"] = resultMessage;
                return View(enrollment);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                TempData["Error"] = resultMessage;
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var enrollment = await _enrollmentService.GetEnrollmentById(id);
            if (enrollment == null)
            {
                TempData["Error"] = "Đã xảy ra lỗi khi truy cập đăng ký này";
                return RedirectToAction("Index");
            }
            await LoadData();
            return View(enrollment);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Enrollment obj)
        {
            string resultMessage = "Lỗi không thể sửa đăng ký này";
            if (!ModelState.IsValid)
            {
                await LoadData();
                return View(obj);
            }
            try
            {
                var enrollment = await _enrollmentService.UpdateEnrollment(obj);
                if (enrollment != null)
                {
                    TempData["Success"] = "Chỉnh sửa đăng ký thành công";
                    return RedirectToAction("Index");
                }
                await LoadData();
                TempData["Error"] = resultMessage;
                return View(enrollment);
            }
            catch (Exception ex)
            {
                TempData["Error"] = resultMessage;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var resultMessage = "Xóa đăng ký không thành công";
            try
            {
                var enrollment = await _enrollmentService.DeleteEnrollment(id);
                if (enrollment.Id != id)
                {
                    TempData["Error"] = "Không thể xóa vì dữ liệu đang được sử dụng";
                    return View();
                }
                if (enrollment != null)
                {
                    TempData["Success"] = "Xóa đăng ký thành công";
                    return RedirectToAction("Index");
                }
                TempData["Error"] = resultMessage;
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = resultMessage;
                return View();
            }
        }

        private async Task LoadData()
        {
            ViewBag.Classes = await _classService.GetAllClasses();
            ViewBag.Students = await _studentService.GetAllStudents();
        }

    }
}
