using DAL.Models;
using DAL.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentRepository _enrollRepo;

        public EnrollmentController(IEnrollmentRepository enrollRepo)
        {
            _enrollRepo = enrollRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetAll()
        {
            var enrollments = await _enrollRepo.GetAllEnrollmentDetail();
            return Ok(enrollments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Enrollment>> GetById(int id)
        {
            var erl = await _enrollRepo.GetById(id);
            if (erl == null)
                return NotFound();
            return Ok(erl);
        }

        [HttpGet("by-class_student/{classId}/{studentId}")]
        public async Task<ActionResult<Enrollment>> GetByClassStudent(int classId, int studentId)
        {
            var erl = await _enrollRepo.GetErollmentByClassAndStudent(classId, studentId);
            if (erl == null)
                return NotFound();
            return Ok(erl);
        }

        [HttpPost]
        public async Task<ActionResult<Enrollment>> Insert([FromBody] Enrollment enrollment)
        {
            enrollment.Id = 0;
            try
            {
                var erl = await _enrollRepo.Insert(enrollment);
                return Ok(erl);
            }
            catch (Exception ex)
            {
                return BadRequest(error: ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Enrollment>> Update(int id, [FromBody] Enrollment enrollment)
        {
            var erl = await _enrollRepo.GetById(id);
            try
            {
                if (erl == null)
                {
                    return NotFound();
                }
                erl = enrollment;
                erl.Id = id;
                var result = await _enrollRepo.Update(erl);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(error: ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Enrollment>> Delete(int id)
        {
            var erl = await _enrollRepo.GetById(id);
            try
            {
                if (erl == null)
                    return NotFound();

                await _enrollRepo.Delete(erl);
                return Ok(erl);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
