using DAL.Models;
using DAL.Repositories;
using DAL.Repositories.IRepository;
using DAL.Response;
using Microsoft.AspNetCore.Mvc;

namespace PortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleRepository _scheduleRepo;
        private readonly IAttendanceRepository _attendanceRepository;

        public ScheduleController(IScheduleRepository scheduleRepo, IAttendanceRepository attendanceRepository)
        {
            _scheduleRepo = scheduleRepo;
            _attendanceRepository = attendanceRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetAll()
        {
            var schedules = await _scheduleRepo.GetAll();
            return Ok(schedules);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule>> GetById(int id)
        {
            var scd = await _scheduleRepo.GetById(id);
            if (scd == null)
                return NotFound();
            return Ok(scd);
        }

        [HttpGet("by-class/{classId}")]
        public async Task<IActionResult> GetByClass(int classId)
        {
            var schedules = await _scheduleRepo.GetByClassId(classId);

            if (!schedules.Any())
                return NotFound("Lớp này chưa có lịch học");

            return Ok(schedules);
        }

        [HttpGet("by-teacher/{teacherId}")]
        public async Task<ActionResult<IEnumerable<ClassDetailResponse>>> GetScheduleByTeacherId(int teacherId)
        {
            var cls = await _scheduleRepo.GetAllScheduleByTeacher(teacherId);
            if (cls == null)
                return NotFound();
            return Ok(cls);
        }

        [HttpGet("by-student/{studentId}")]
        public async Task<ActionResult<IEnumerable<ClassDetailResponse>>> GetScheduleByStudentId(int studentId)
        {
            var cls = await _scheduleRepo.GetAllScheduleByStudent(studentId);
            if (cls == null)
                return NotFound();
            return Ok(cls);
        }

        [HttpPost]
        public async Task<ActionResult<Schedule>> Insert([FromBody] Schedule schedule)
        {
            schedule.Id = 0;
            try
            {
                var scd = await _scheduleRepo.Insert(schedule);
                return Ok(scd);
            }
            catch (Exception ex)
            {
                return BadRequest(error: ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Schedule>> Update(int id, [FromBody] Schedule schedule)
        {
            var scd = await _scheduleRepo.GetById(id);
            try
            {
                if (scd == null)
                {
                    return NotFound();
                }
                scd = schedule;
                scd.Id = id;
                var result = await _scheduleRepo.Update(scd);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(error: ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Schedule>> Delete(int id)
        {
            var att = await _attendanceRepository.GetAttendanceBySchedule(id);

            if (att != null)
            {
                return Conflict("Không thể xóa vì dữ liệu đang được sử dụng");
            }
            var scd = await _scheduleRepo.GetById(id);
            try
            {
                if (scd == null)
                    return NotFound();

                await _scheduleRepo.Delete(scd);
                return Ok(scd);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
