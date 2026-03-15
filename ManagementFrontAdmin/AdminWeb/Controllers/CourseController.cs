using BLL.DTOs;
using BLL.IServices;
using Microsoft.AspNetCore.Mvc;

namespace AdminWeb.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _courseService.GetAllCourses();
            return View(courses);
        }

        public async Task<IActionResult> GetCourseById(int id)
        {
            var course = await _courseService.GetCourseById(id);
            return Ok(course);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Course obj)
        {
            string resultMessage = "Lỗi không thể thêm khóa học này";
            try
            {
                var course = await _courseService.InsertCourse(obj);
                if (course != null)
                {
                    TempData["Success"] = "Thêm mới khóa học thành công";
                    return RedirectToAction("Index");
                }
                TempData["Error"] = resultMessage;
                return View(course);
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
            var course = await _courseService.GetCourseById(id);
            if (course == null)
            {
                TempData["Error"] = "Đã xảy ra lỗi khi truy cập khóa học này";
                return RedirectToAction("Index");
            }
            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Course obj)
        {
            string resultMessage = "Lỗi không thể sửa khóa học này";
            try
            {
                var course = await _courseService.UpdateCourse(obj);
                if (course != null)
                {
                    TempData["Success"] = "Chỉnh sửa khóa học thành công";
                    return RedirectToAction("Index");
                }
                TempData["Error"] = resultMessage;
                return View(course);
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
            var resultMessage = "Xóa khóa học không thành công";
            try
            {
                var course = await _courseService.DeleteCourse(id);
                if (course.Id != id)
                {
                    TempData["Error"] = "Không thể xóa vì dữ liệu đang được sử dụng";
                    return View();
                }
                if (course != null)
                {
                    TempData["Success"] = "Xóa khóa học thành công";
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
    }
}
