using BLL.DTOs;
using BLL.IServices;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdminWeb.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IUserService _userService;

        public StudentController(IStudentService studentService, IUserService userService)
        {
            _studentService = studentService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _studentService.GetAllStudents();
            return View(students);
        }

        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await _studentService.GetStudentById(id);
            return Ok(student);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student obj)
        {
            string resultMessage = "Lỗi không thể thêm học viên này";
            try
            {
                var student = await _studentService.InsertStudent(obj);
                if (student != null)
                {
                    TempData["Success"] = "Thêm mới học viên thành công";
                    return RedirectToAction("Index");
                }
                TempData["Error"] = resultMessage;
                return View(student);
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
            var student = await _studentService.GetStudentById(id);
            if (student == null)
            {
                TempData["Error"] = "Đã xảy ra lỗi khi truy cập học viên này";
                return RedirectToAction("Index");
            }
            var studentAccount = await _userService.GetUserById(student.UserId);
            ViewBag.StudentAccount = studentAccount;
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student obj)
        {
            string resultMessage = "Lỗi không thể sửa học viên này";
            try
            {
                var student = await _studentService.UpdateStudent(obj);
                if (student != null)
                {
                    TempData["Success"] = "Chỉnh sửa học viên thành công";
                    return RedirectToAction("Index");
                }
                TempData["Error"] = resultMessage;
                return View(student);
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
            var resultMessage = "Xóa học viên không thành công";
            try
            {
                var student = await _studentService.DeleteStudent(id);
                if (student.Id != id)
                {
                    TempData["Error"] = "Không thể xóa vì dữ liệu đang được sử dụng";
                    return View();
                }
                if (student != null)
                {
                    TempData["Success"] = "Xóa học viên thành công";
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

        [HttpPost]
        public async Task<IActionResult> UpdateAccountId(int studentId, int userId)
        {
            var result = await _studentService.UpdateAccount(studentId, userId);

            if (result == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Gán tài khoản cho học viên thất bại"
                });
            }

            return Json(new
            {
                success = true,
                message = "Gán tài khoản cho học viên thành công"
            });
        }
    }
}
