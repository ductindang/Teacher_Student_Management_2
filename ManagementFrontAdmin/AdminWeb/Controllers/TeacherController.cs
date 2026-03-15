using BLL.DTOs;
using BLL.IServices;
using BLL.Response;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AdminWeb.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly ITeacherReviewService _reviewService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public TeacherController(ITeacherService teacherService, ITeacherReviewService reviewService, IUserService userService, IRoleService roleService)
        {
            _teacherService = teacherService;
            _reviewService = reviewService;
            _userService = userService;
            _roleService = roleService;
        }

        public async Task<IActionResult> Index()
        {
            var teachers = await _teacherService.GetAllTeachers();
            return View(teachers);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Teacher obj)
        {
            string resultMessage = "Lỗi không thể thêm giáo viên này";
            try
            {
                var teacher = await _teacherService.InsertTeacher(obj);
                if (teacher != null)
                {
                    TempData["Success"] = "Thêm mới giáo viên thành công";
                    return RedirectToAction("Index");
                }
                TempData["Error"] = resultMessage;
                return View(teacher);
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
            var teacher = await _teacherService.GetTeacherById(id);
            if (teacher == null)
            {
                TempData["Error"] = "Đã xảy ra lỗi khi truy cập giáo viên này";
                return RedirectToAction("Index");
            }
            var teacherReviews = await _reviewService.GetByTeacher(id);
            var teacherAccount = await _userService.GetUserById(teacher.UserId);
            ViewBag.TeacherReviews = teacherReviews;
            ViewBag.TeacherAccount = teacherAccount;
            return View(teacher);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Teacher obj)
        {
            string resultMessage = "Lỗi không thể sửa giáo viên này";
            try
            {
                var teacher = await _teacherService.UpdateTeacher(obj);
                if (teacher != null)
                {
                    TempData["Success"] = "Chỉnh sửa giáo viên thành công";
                    return RedirectToAction("Index");
                }
                TempData["Error"] = resultMessage;
                return View(teacher);
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
            var resultMessage = "Xóa giáo viên không thành công";
            try
            {
                var teacher = await _teacherService.DeleteTeacher(id);
                if (teacher.Id != id)
                {
                    TempData["Error"] = "Không thể xóa vì dữ liệu đang được sử dụng";
                    return View();
                }
                if (teacher != null)
                {
                    TempData["Success"] = "Xóa giáo viên thành công";
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
        public async Task<IActionResult> UpdateAccountId(int teacherId, int userId)
        {
            var result = await _teacherService.UpdateAccount(teacherId, userId);

            if (result == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Gán tài khoản cho giáo viên thất bại"
                });
            }

            return Json(new
            {
                success = true,
                message = "Gán tài khoản cho giáo viên thành công"
            });
        }


        private async Task<IEnumerable<TeacherReviewResponse>> GetTeacherReviewData(int teacherId)
        {
            var teacherReviews = await _reviewService.GetByTeacher(teacherId);
            return teacherReviews;
        }
    }
}
