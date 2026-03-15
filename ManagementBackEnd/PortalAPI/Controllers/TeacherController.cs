using DAL.Models;
using DAL.Repositories;
using DAL.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherRepository _teacherRepo;
        private readonly IClassRepository _classRepository;
        private readonly ITeacherReviewRepository _teacherReviewRepo;

        public TeacherController(ITeacherRepository teacherRepo, IClassRepository classRepository, ITeacherReviewRepository teacherReviewRepo)
        {
            _teacherRepo = teacherRepo;
            _classRepository = classRepository;
            _teacherReviewRepo = teacherReviewRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetAll()
        {
            var teachers = await _teacherRepo.GetAll();
            return Ok(teachers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetById(int id)
        {
            var tec = await _teacherRepo.GetById(id);
            if (tec == null)
                return NotFound();
            return Ok(tec);
        }

        [HttpGet("userId/{userId}")]
        public async Task<ActionResult<Teacher>> GetTeacherByUserId(int userId)
        {
            var tea = await _teacherRepo.GetTeacherByUserId(userId);
            if (tea == null)
            {
                return NotFound();
            }
            return Ok(tea);
        }

        [HttpPost]
        public async Task<ActionResult<Teacher>> Insert([FromBody] Teacher teacher)
        {
            teacher.Id = 0;
            try
            {
                var tec = await _teacherRepo.Insert(teacher);
                return Ok(tec);
            }
            catch (Exception ex)
            {
                return BadRequest(error: ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Teacher>> Update(int id, [FromBody] Teacher teacher)
        {
            try
            {
                var tec = await _teacherRepo.GetById(id);
                if (tec == null)
                {
                    return NotFound();
                }
                teacher.UserId = tec.UserId;
                tec = teacher;
                tec.Id = id;
                var result = await _teacherRepo.Update(tec);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(error: ex.Message);
            }

        }

        [HttpPut("UpdateAccount")]
        public async Task<ActionResult<Teacher>> UpdateUserForTeacher([FromQuery] int teacherId, [FromQuery] int userId)
        {
            try
            {
                var tec = await _teacherRepo.GetById(teacherId);
                if (tec == null)
                {
                    return NotFound();
                }
                tec.UserId = userId;
                var result = await _teacherRepo.Update(tec);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(error: ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Teacher>> Delete(int id)
        {
            var cls = await _classRepository.GetClassByTeacher(id);
            var tear = await _teacherReviewRepo.GetByTeacherId(id);

            if (cls != null || (tear != null && tear.Any()))
            {
                return Conflict("Không thể xóa vì dữ liệu đang được sử dụng");
            }

            var tec = await _teacherRepo.GetById(id);

            try
            {
                if (tec == null)
                    return NotFound();

                await _teacherRepo.Delete(tec);
                return Ok(tec);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
