using DAL.Models;
using DAL.Repositories;
using DAL.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepo;
        private readonly IClassRepository _classRepository;

        public CourseController(ICourseRepository courseRepo, IClassRepository classRepository)
        {
            _courseRepo = courseRepo;
            _classRepository = classRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetAll()
        {
            var courses = await _courseRepo.GetAll();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetById(int id)
        {
            var crs = await _courseRepo.GetById(id);
            if (crs == null)
                return NotFound();
            return Ok(crs);
        }

        [HttpPost]
        public async Task<ActionResult<Course>> Insert([FromBody] Course course)
        {
            course.Id = 0;
            try
            {
                var crs = await _courseRepo.Insert(course);
                return Ok(crs);
            }
            catch (Exception ex)
            {
                return BadRequest(error: ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Course>> Update(int id, [FromBody] Course course)
        {
            var crs = await _courseRepo.GetById(id);
            try
            {
                if (crs == null)
                {
                    return NotFound();
                }
                crs = course;
                crs.Id = id;
                var result = await _courseRepo.Update(crs);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(error: ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Course>> Delete(int id)
        {
            var cls = await _classRepository.GetClassByCourse(id);
            if (cls != null)
            {
                return Conflict("Không thể xóa vì dữ liệu đang được sử dụng");
            }

            var crs = await _courseRepo.GetById(id);
            try
            {
                if (crs == null)
                    return NotFound();

                await _courseRepo.Delete(crs);
                return Ok(crs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
