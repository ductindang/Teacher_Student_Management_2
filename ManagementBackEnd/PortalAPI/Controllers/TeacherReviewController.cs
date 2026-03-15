using DAL.Models;
using DAL.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace PortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherReviewController : ControllerBase
    {
        private readonly ITeacherReviewRepository _reviewRepo;

        public TeacherReviewController(ITeacherReviewRepository reviewRepo)
        {
            _reviewRepo = reviewRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherReview>>> GetAll()
        {
            var rev = await _reviewRepo.GetAll();
            return Ok(rev);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherReview>> GetById(int id)
        {
            var rev = await _reviewRepo.GetById(id);
            if (rev == null)
                return NotFound();
            return Ok(rev);
        }

        [HttpGet("by-teacher/{teacherId}")]
        public async Task<IActionResult> GetByTeacher(int teacherId)
        {
            var reviews = await _reviewRepo.GetByTeacherId(teacherId);

            if (!reviews.Any())
                return NotFound("Giáo viên này chưa có đánh giá nào!");

            return Ok(reviews);
        }

        [HttpGet("student/{studentId}/class/{classId}/")]
        public async Task<IActionResult> GetByTeacherStudentClass(int studentId, int classId)
        {
            var review = await _reviewRepo.GetByTeacherStudentClass(studentId, classId);

            if (review == null)
                return NotFound("Không thấy đánh giá này");

            return Ok(review);
        }


        [HttpPost]
        public async Task<ActionResult<TeacherReview>> Insert([FromBody] TeacherReview review)
        {
            review.Id = 0;
            try
            {
                review.CreatedAt = DateTime.Now;
                var rev = await _reviewRepo.Insert(review);
                return Ok(rev);
            }
            catch (Exception ex)
            {
                return BadRequest(error: ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TeacherReview>> Update(int id, [FromBody] TeacherReview review)
        {
            var rev = await _reviewRepo.GetById(id);
            try
            {
                if (rev == null)
                {
                    return NotFound();
                }
                rev = review;
                rev.Id = id;
                var result = await _reviewRepo.Update(rev);
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
            var rev = await _reviewRepo.GetById(id);
            try
            {
                if (rev == null)
                    return NotFound();

                await _reviewRepo.Delete(rev);
                return Ok(rev);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
