using DAL.Models;
using DAL.Repositories;
using DAL.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ITeacherReviewRepository _reviewRepo;

        public StudentController(IStudentRepository studentRepo, IAttendanceRepository attendanceRepository, IEnrollmentRepository enrollmentRepository, ITeacherReviewRepository reviewRepo)
        {
            _studentRepo = studentRepo;
            _attendanceRepository = attendanceRepository;
            _enrollmentRepository = enrollmentRepository;
            _reviewRepo = reviewRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetAll()
        {
            var students = await _studentRepo.GetAll();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetById(int id)
        {
            var std = await _studentRepo.GetById(id);
            if (std == null)
                return NotFound();
            return Ok(std);
        }

        [HttpGet("userId/{userId}")]
        public async Task<ActionResult<Student>> GetStudentByUserId(int userId)
        {
            var std = await _studentRepo.GetStudentByUserId(userId);
            if(std == null)
            {
                return NotFound();
            }
            return Ok(std);
        }

        [HttpGet("class")]
        public async Task<ActionResult<IEnumerable<Student>>> GetAllStudentByClass(int classId)
        {
            var students = await _studentRepo.GetAllStudentByClass(classId);
            return Ok(students);
        }

        [HttpPost]
        public async Task<ActionResult<Student>> Insert([FromBody] Student student)
        {
            student.Id = 0;
            try
            {
                var std = await _studentRepo.Insert(student);
                return Ok(std);
            }
            catch (Exception ex)
            {
                return BadRequest(error: ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> Update(int id, [FromBody] Student student)
        {
            try
            {
                var std = await _studentRepo.GetById(id);
                if (std == null)
                {
                    return NotFound();
                }
                student.UserId = std.UserId;
                std = student;
                std.Id = id;
                var result = await _studentRepo.Update(std);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(error: ex.Message);
            }
        }

        [HttpPut("UpdateAccount")]
        public async Task<ActionResult<Student>> UpdateUserForStudent([FromQuery] int studentId, [FromQuery] int userId)
        {
            try
            {
                var stu = await _studentRepo.GetById(studentId);
                if (stu == null)
                {
                    return NotFound();
                }
                stu.UserId = userId;
                var result = await _studentRepo.Update(stu);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(error: ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> Delete(int id)
        {
            var att = await _attendanceRepository.GetAttendanceByStudent(id);
            var err = await _enrollmentRepository.GetEnrollmentByStudent(id);
            var rev = await _reviewRepo.GetByStudent(id);

            if (att != null || err != null || rev != null)
            {
                return Conflict("Không thể xóa vì dữ liệu đang được sử dụng");
            }
            var std = await _studentRepo.GetById(id);
            try
            {
                if (std == null)
                    return NotFound();

                await _studentRepo.Delete(std);
                return Ok(std);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
