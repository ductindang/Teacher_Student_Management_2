using BLL.DTOs;
using BLL.DTOs.Enum;
using BLL.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ClientWeb.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Enrollment obj)
        {
            string resultMessage = "Lỗi không thể đăng ký lớp học này";
            obj.EnrollDate = DateTime.Now;
            obj.Status = EEnrollStatus.InActive;
            if (!ModelState.IsValid)
            {
                TempData["Error"] = resultMessage;
                return RedirectToAction("Details", "Class", new { id = obj.ClassId });
            }
            try
            {
                var enrollment = await _enrollmentService.InsertEnrollment(obj);
                if (enrollment != null)
                {
                    TempData["Success"] = "Đăng ký lớp học thành công";
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

        public async Task<IActionResult> Cancel(int id)
        {
            var resultMessage = "Hủy đăng ký không thành công";
            try
            {
                var enrollmentEdit = await _enrollmentService.GetEnrollmentById(id);
                if(enrollmentEdit == null)
                {
                    resultMessage = "Bạn chưa đăng ký lớp này";
                    TempData["Success"] = resultMessage;
                    return RedirectToAction("Index", "Class");
                }
                enrollmentEdit.Status = EEnrollStatus.Cancel;
                var enrollment = await _enrollmentService.UpdateEnrollment(enrollmentEdit);
                if (enrollment != null)
                {
                    TempData["Success"] = "Hủy đăng ký thành công";
                    return RedirectToAction("Details", "Class", new { id = enrollment.ClassId });
                }
                TempData["Error"] = resultMessage;
                return RedirectToAction("Index", "Class");
            }
            catch (Exception ex)
            {
                TempData["Error"] = resultMessage;
                return RedirectToAction("Index", "Class");
            }
        }
        public async Task<IActionResult> ReEnroll(int id)
        {
            var resultMessage = "Đăng ký lại không thành công";
            try
            {
                var enrollmentEdit = await _enrollmentService.GetEnrollmentById(id);
                if(enrollmentEdit == null)
                {
                    resultMessage = "Bạn chưa đăng ký lớp này";
                    TempData["Success"] = resultMessage;
                    return RedirectToAction("Index", "Class");
                }
                enrollmentEdit.Status = EEnrollStatus.InActive;
                var enrollment = await _enrollmentService.UpdateEnrollment(enrollmentEdit);
                if (enrollment != null)
                {
                    TempData["Success"] = "Đăng ký lại thành công";
                    return RedirectToAction("Details", "Class", new { id = enrollment.ClassId });
                }
                TempData["Error"] = resultMessage;
                return RedirectToAction("Index", "Class");
            }
            catch (Exception ex)
            {
                TempData["Error"] = resultMessage;
                return RedirectToAction("Index", "Class");
            }
        }
    }
}
