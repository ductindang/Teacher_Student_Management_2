using BLL.DTOs;
using BLL.IServices;
using Microsoft.AspNetCore.Mvc;

namespace AdminWeb.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        public async Task<IActionResult> Index()
        {
            var schedules = await _scheduleService.GetAllSchedules();
            return View(schedules);
        }

        public async Task<IActionResult> GetScheduleById(int id)
        {
            var schedule = await _scheduleService.GetScheduleById(id);
            return Ok(schedule);
        }

        public async Task<IActionResult> GetByClass(int classId)
        {
            var schedules = await _scheduleService.GetByClass(classId);
            return Ok(schedules);
        }

        //public async Task<IActionResult> PopupCreate()
        //{
        //    return View();
        //}

        [HttpPost]
        public async Task<IActionResult> Create(Schedule obj)
        {
            if (!IsValidSchedule(obj))
            {
                TempData["Error"] = "Dữ liệu lịch học không hợp lệ";
                return RedirectToAction("Edit", "Class", new { id = obj.ClassId });
            }

            await _scheduleService.InsertSchedule(obj);

            TempData["Success"] = "Thêm mới lịch học thành công";
            return RedirectToAction("Edit", "Class", new { id = obj.ClassId });
        }

        private bool IsValidSchedule(Schedule obj)
        {
            if (obj.StartTime < new TimeSpan(7, 0, 0)) return false;
            if (obj.EndTime > new TimeSpan(19, 0, 0)) return false;
            if (obj.StartTime >= obj.EndTime) return false;
            if (string.IsNullOrWhiteSpace(obj.Room)) return false;

            return true;
        }

        public async Task<IActionResult> Edit(int id)
        {
            var schedule = await _scheduleService.GetScheduleById(id);
            if (schedule == null)
            {
                TempData["Error"] = "Đã xảy ra lỗi khi truy cập lịch học này";
                return RedirectToAction("Index");
            }
            return View(schedule);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Schedule obj)
        {
            string resultMessage = "Lỗi không thể sửa lịch học này";
            try
            {
                var schedule = await _scheduleService.UpdateSchedule(obj);
                if (schedule != null)
                {
                    TempData["Success"] = "Chỉnh sửa lịch học thành công";
                    return RedirectToAction("Edit", "Class", new { id = obj.ClassId });
                }
                TempData["Error"] = resultMessage;
                return RedirectToAction("Edit", "Class", new { id = obj.ClassId });
            }
            catch (Exception ex)
            {
                TempData["Error"] = resultMessage;
                return RedirectToAction("Edit", "Class", new { id = obj.ClassId });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var resultMessage = "Xóa lịch học không thành công";
            var schedule = await _scheduleService.DeleteSchedule(id);
            if (schedule.Id != id)
            {
                TempData["Error"] = "Không thể xóa vì dữ liệu đang được sử dụng";
                return View();
            }
            if (schedule != null)
            {
                TempData["Success"] = "Xóa lịch học thành công";
                return RedirectToAction("Edit", "Class", new { id = schedule.ClassId });
            }
            TempData["Error"] = resultMessage;
            return Ok(resultMessage);
        }
    }
}
