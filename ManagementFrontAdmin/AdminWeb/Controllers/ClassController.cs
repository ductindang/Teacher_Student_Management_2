using AdminWeb.Models;
using BLL.DTOs;
using BLL.IServices;
using BLL.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Threading.Tasks;

namespace AdminWeb.Controllers
{
    public class ClassController : Controller
    {
        private readonly IClassService _classService;
        private readonly ICourseService _courseService;
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;
        private readonly IScheduleService _scheduleService;

        public ClassController(
            IClassService classService,
            ICourseService courseService,
            ITeacherService teacherService,
            IStudentService studentService,
            IScheduleService scheduleService)
        {
            _classService = classService;
            _courseService = courseService;
            _teacherService = teacherService;
            _studentService = studentService;
            _scheduleService = scheduleService;
        }

        public async Task<IActionResult> Index()
        {
            var classes = await _classService.GetAllClasses();
            return View(classes);
        }

        public async Task<IActionResult> GetClassById(int id)
        {
            var classObj = await _classService.GetClassById(id);
            return Ok(classObj);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Courses = await _courseService.GetAllCourses();
            ViewBag.Teachers = await _teacherService.GetAllTeachers();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Class obj)
        {
            string resultMessage = "Lỗi không thể thêm lớp học này";

            if (obj.EndDate <= obj.StartDate)
            {
                ModelState.AddModelError("EndDate", "Ngày kết thúc phải lớn hơn ngày bắt đầu");
            }

            if (!ModelState.IsValid)
            {
                TempData["Error"] = resultMessage;
                ViewBag.Courses = await _courseService.GetAllCourses();
                ViewBag.Teachers = await _teacherService.GetAllTeachers();
                return View(obj);
            }
            try
            {
                var classObj = await _classService.InsertClass(obj);
                if (classObj != null)
                {
                    TempData["Success"] = "Thêm mới lớp học thành công";
                    return RedirectToAction("Index");
                }
                TempData["Error"] = resultMessage;
                return View(classObj);
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
            var classObj = await _classService.GetClassById(id);
            if (classObj == null)
            {
                TempData["Error"] = "Đã xảy ra lỗi khi truy cập lớp học này";
                return RedirectToAction("Index");
            }
            var students = await _studentService.GetStudentsByClass(id);
            var schedules = await _scheduleService.GetByClass(id);
            var viewModel = new ClassEditViewModel
            {
                Class = classObj,
                Students = students,
                Schedules = schedules
            };
            ViewBag.Courses = await _courseService.GetAllCourses();
            ViewBag.Teachers = await _teacherService.GetAllTeachers();
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ClassEditViewModel model)
        {
            var validForm = model.Class != null
                && !string.IsNullOrWhiteSpace(model.Class.Name)
                && model.Class.CourseId > 0
                && model.Class.TeacherId > 0
                && model.Class.MaxStudents > 0
                && model.Class.StartDate != default
                && model.Class.EndDate != default
                && model.Class.EndDate > model.Class.StartDate;

            if (!validForm)
            {
                ViewBag.Courses = await _courseService.GetAllCourses();
                ViewBag.Teachers = await _teacherService.GetAllTeachers();
                TempData["Error"] = "Dữ liệu không hợp lệ";
                return View(model);
            }
            var classModel = new Class
            {
                Id = model.Class.Id,
                CourseId = model.Class.CourseId,
                TeacherId = model.Class.TeacherId,
                Name = model.Class.Name,
                StartDate = model.Class.StartDate,
                EndDate = model.Class.EndDate,
                MaxStudents = model.Class.MaxStudents,
                Description = model.Class.Description
            };

            var result = await _classService.UpdateClass(classModel);

            if (result != null)
            {
                TempData["Success"] = "Chỉnh sửa lớp học thành công";
                return RedirectToAction("Index");
            }

            ViewBag.Courses = await _courseService.GetAllCourses();
            ViewBag.Teachers = await _teacherService.GetAllTeachers();
            TempData["Error"] = "Lỗi không thể sửa lớp học này";
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var resultMessage = "Xóa lớp học không thành công";
            try
            {
                var classObj = await _classService.DeleteClass(id);
                if (classObj.Id != id)
                {
                    TempData["Error"] = "Không thể xóa vì dữ liệu đang được sử dụng";
                    return View();
                }
                if(classObj != null)
                {
                    TempData["Success"] = "Xóa lớp học thành công";
                    return RedirectToAction("Index");
                }
                TempData["Error"] = resultMessage;
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = resultMessage;
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditClassImage(IFormFile file, int classId)
        {
            if (file != null)
            {
                using var ms = new MemoryStream();
                await file.CopyToAsync(ms);

                var imageClass = new ClassImage
                {
                    Id = 0,
                    ClassId = classId,
                    ClassImg = ms.ToArray()
                };
                var classimg = await _classService.UpdateClassImage(imageClass);
                if( classimg != null)
                {
                    TempData["Success"] = "Cập nhật ảnh lớp học thành công";
                }
                else
                {
                    TempData["Success"] = "Lỗi khi cập nhật ảnh này";
                }
            }

            return RedirectToAction("Edit", new { id = classId });
        }
    }
}
