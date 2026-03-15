using DAL.Data;
using DAL.Models;
using DAL.Repositories.IRepository;
using DAL.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassRepository _classRepo;
        private readonly IClassImageRepository _classImgRepo;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly ITeacherReviewRepository _reviewRepo;
        private readonly AppDbContext _context;

        public ClassController(IClassRepository classRepo, IClassImageRepository classImgRepo, AppDbContext context, IAttendanceRepository attendanceRepository, IEnrollmentRepository enrollmentRepository, IScheduleRepository scheduleRepository, ITeacherReviewRepository reviewRepo)
        {
            _classRepo = classRepo;
            _classImgRepo = classImgRepo;
            _context = context;
            _attendanceRepository = attendanceRepository;
            _enrollmentRepository = enrollmentRepository;
            _scheduleRepository = scheduleRepository;
            _reviewRepo = reviewRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassResponse>>> GetAll()
        {
            var classes = await _classRepo.GetAllClassDetail();
            return Ok(classes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClassResponse>> GetById(int id)
        {
            var cls = await _classRepo.GetClassById(id);
            if (cls == null)
                return NotFound();
            return Ok(cls);
        }

        [HttpGet("today-by-teacher/{teacherId}")]
        public async Task<ActionResult<IEnumerable<ClassDetailResponse>>> GetClassTodayByTeacherId(int teacherId)
        {
            var cls = await _classRepo.GetClassDetailTodayByTeacherId(teacherId);
            if (cls == null)
                return NotFound();
            return Ok(cls);
        }

        [HttpGet("in-progress/by-teacher/{teacherId}")]
        public async Task<ActionResult<IEnumerable<ClassDetailResponse>>> GetAllClassByTeacherIdInProgress(int teacherId)
        {
            var cls = await _classRepo.GetAllClassDetailByTeacherIdInProgress(teacherId);
            if (cls == null)
                return NotFound();
            return Ok(cls);
        }


        [HttpGet("upcomming/by-teacher/{teacherId}")]
        public async Task<ActionResult<IEnumerable<ClassDetailResponse>>> GetAllClassByTeacherIdUpcomming(int teacherId)
        {
            var cls = await _classRepo.GetAllClassDetailByTeacherIdUpcomming(teacherId);
            if (cls == null)
                return NotFound();
            return Ok(cls);
        }
        [HttpGet("finished/by-teacher/{teacherId}")]
        public async Task<ActionResult<IEnumerable<ClassDetailResponse>>> GetAllClassDetailByTeacherIdFinished(int teacherId)
        {
            var cls = await _classRepo.GetAllClassDetailByTeacherIdFinished(teacherId);
            if (cls == null)
                return NotFound();
            return Ok(cls);
        }

        [HttpGet("today-by-student/{studentId}")]
        public async Task<ActionResult<IEnumerable<ClassDetailResponse>>> GetClassTodayByStudentId(int studentId)
        {
            var cls = await _classRepo.GetClassDetailTodayByStudentId(studentId);
            if (cls == null)
                return NotFound();
            return Ok(cls);
        }

        [HttpGet("in-progress/by-student/{studentId}")]
        public async Task<ActionResult<IEnumerable<ClassDetailResponse>>> GetAllClassByStudentIdInProgress(int studentId)
        {
            var cls = await _classRepo.GetAllClassDetailByStudentIdInProgress(studentId);
            if (cls == null)
                return NotFound();
            return Ok(cls);
        }


        [HttpGet("upcomming/by-student/{studentId}")]
        public async Task<ActionResult<IEnumerable<ClassDetailResponse>>> GetAllClassByStudentIdUpcomming(int studentId)
        {
            var cls = await _classRepo.GetAllClassDetailByStudentIdUpcomming(studentId);
            if (cls == null)
                return NotFound();
            return Ok(cls);
        }

        [HttpGet("finished/by-student/{studentId}")]
        public async Task<ActionResult<IEnumerable<ClassDetailResponse>>> GetAllClassDetailByStudentIdFinished(int studentId)
        {
            var cls = await _classRepo.GetAllClassDetailByStudentIdFinished(studentId);
            if (cls == null)
                return NotFound();
            return Ok(cls);
        }

        [HttpGet("review/by-student/{studentId}")]
        public async Task<ActionResult<IEnumerable<TeacherClassReviewResponse>>> GetAllClassAndReviewDetailByStudentIdReview(int studentId)
        {
            var cls = await _classRepo.GetAllClassAndReviewDetailByStudentId(studentId);
            if (cls == null)
                return NotFound();
            return Ok(cls);
        }

        [HttpGet("upcoming")]
        public async Task<ActionResult<IEnumerable<ClassDetailResponse>>> GetAllClassUpcoming()
        {
            var cls = await _classRepo.GetAllClassDetailUpcomming();
            if (cls == null)
                return NotFound();
            return Ok(cls);
        }

        [HttpPost]
        public async Task<ActionResult<Class>> Insert([FromBody] Class classObj)
        {
            classObj.Id = 0;
            try
            {
                var cls = await _classRepo.Insert(classObj);
                return Ok(cls);
            }
            catch (Exception ex)
            {
                return BadRequest(error: ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Class>> Update(int id, [FromBody] Class classObj)
        {
            var cls = await _classRepo.GetById(id);
            try
            {
                if (cls == null)
                {
                    return NotFound();
                }
                cls = classObj;
                cls.Id = id;
                var result = await _classRepo.Update(cls);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(error: ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Class>> Delete(int id)
        {
            var att = await _attendanceRepository.GetAttendanceByClass(id);
            var cli = await _classImgRepo.GetClassImageByClass(id);
            var err = await _enrollmentRepository.GetEnrollmentByClass(id);
            var sch = await _scheduleRepository.GetByClassId(id);
            var rev = await _reviewRepo.GetByClass(id);

            if(att != null || cli != null || err != null || (sch != null && sch.Any()) || rev != null)
            {
                return Conflict("Không thể xóa vì dữ liệu đang được sử dụng");
            }
            var cls = await _classRepo.GetById(id);
            try
            {
                if (cls == null)
                    return NotFound();

                await _classRepo.Delete(cls);
                return Ok(cls);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("ClassImage/{classId}")]
        public async Task<ActionResult<ClassImage>> UpdateClassImage(int classId, [FromBody] ClassImage classImgObj)
        {
            var existingImage = await _context.ClassImages
                .FirstOrDefaultAsync(x => x.ClassId == classId);

            if (existingImage != null)
            {
                // Update đúng entity đang track
                existingImage.ClassImg = classImgObj.ClassImg;
            }
            else
            {
                classImgObj.ClassId = classId;
                await _context.ClassImages.AddAsync(classImgObj);
            }

            await _context.SaveChangesAsync();

            var result = await _context.ClassImages
                .FirstOrDefaultAsync(x => x.ClassId == classId);

            return Ok(result);
        }
    }
}
