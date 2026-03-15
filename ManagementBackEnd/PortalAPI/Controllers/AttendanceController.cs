using DAL.Data;
using DAL.Models;
using DAL.Models.Enum;
using DAL.Repositories.IRepository;
using DAL.Request;
using DAL.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceRepository _attendanceRepo;
        private readonly AppDbContext _context;

        public AttendanceController(IAttendanceRepository attendanceRepo, AppDbContext context)
        {
            _attendanceRepo = attendanceRepo;
            _context = context;
        }
        // GET: api/<AttendanceController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAllAttendances()
        {
            var attendances = await _attendanceRepo.GetAll();
            return Ok(attendances);
        }

        [HttpGet("student/{studentId}/class/{classId}")]
        public async Task<ActionResult<IEnumerable<AttendanceScheduleClassResponse>>> GetAllAttendanceDetails(int studentId, int classId)
        {
            var attendances = await _attendanceRepo.GetAttendanceDetails(studentId, classId);
            return Ok(attendances);
        }

        // GET api/<AttendanceController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Attendance>> GetAttendanceById(int id)
        {
            var att = await _attendanceRepo.GetById(id);
            if (att == null)
                return NotFound();
            return Ok(att);
        }

        // POST api/<AttendanceController>
        [HttpPost]
        public async Task<ActionResult<Attendance>> InsertAttendance([FromBody] Attendance attendance)
        {
            attendance.Id = 0;
            try
            {
                var att = await _attendanceRepo.Insert(attendance);
                return Ok(att);
            } catch (Exception ex) {
                return BadRequest(error: ex.Message);
            }
        }

        // PUT api/<AttendanceController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Attendance>> Update(int id, [FromBody] Attendance attendance)
        {
            var att = await _attendanceRepo.GetById(id);
            try
            {
                if (att == null)
                {
                    return NotFound();
                }
                att = attendance;
                att.Id = id;
                var result = await _attendanceRepo.Update(att);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(error: ex.Message);
            }
            
        }

        // DELETE api/<AttendanceController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Attendance>> Delete(int id)
        {
            var att = await _attendanceRepo.GetById(id);
            try
            {
                if (att == null)
                    return NotFound();

                await _attendanceRepo.Delete(att);
                return Ok(att);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("schedule/{scheduleId}")]
        public async Task<IActionResult> GetStudentsBySchedule(int scheduleId)
        {
            var schedule = await _context.Schedules
                .FirstOrDefaultAsync(s => s.Id == scheduleId);

            if (schedule == null)
                return NotFound();

            var students = await (
                from e in _context.Enrollments
                join s in _context.Students on e.StudentId equals s.Id
                join a in _context.Attendances.Where(x => x.ScheduleId == scheduleId)
                    on s.Id equals a.StudentId into attendanceGroup
                from a in attendanceGroup.DefaultIfEmpty()
                where e.ClassId == schedule.ClassId
                select new StudentAttendanceResponse
                {
                    StudentId = s.Id,
                    UserId = s.UserId,
                    FullName = s.FullName,
                    DateOfBirth = s.DateOfBirth,
                    Gender = s.Gender,
                    Address = s.Address,
                    Status = a != null ? a.Status : null
                }
            ).ToListAsync();

            return Ok(students);
        }

        [HttpPost("UpdateAttendance")]
        public async Task<IActionResult> TakeAttendance([FromBody] AttendanceRequest request)
        {
            foreach (var item in request.Students)
            {
                var existing = await _context.Attendances
                    .FirstOrDefaultAsync(a =>
                        a.ScheduleId == request.ScheduleId &&
                        a.ClassId == request .ClassId &&
                        a.StudentId == item.StudentId);

                if (existing == null)
                {
                    var attendance = new Attendance
                    {
                        ScheduleId = request.ScheduleId,
                        StudentId = item.StudentId,
                        ClassId = request.ClassId,
                        AttendanceDate = DateTime.Now,
                        Status = item.Status
                    };

                    _context.Attendances.Add(attendance);
                }
                else
                {
                    existing.Status = item.Status;
                }
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = "Attendance saved successfully" });
        }
    }
}
