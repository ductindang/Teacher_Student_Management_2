using DAL.Data;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ImageController(AppDbContext context)
        {
            _context = context;
        }


        [HttpPut("DBUploadImage")]
        public async Task<IActionResult> DBUploadImage(IFormFile file, int classId)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty");

            using MemoryStream stream = new MemoryStream();
            await file.CopyToAsync(stream);

            var existingImage = await _context.ClassImages
                .FirstOrDefaultAsync(x => x.ClassId == classId);

            if (existingImage != null)
            {
                existingImage.ClassImg = stream.ToArray();
                _context.ClassImages.Update(existingImage);
            }
            else
            {
                var classImage = new ClassImage()
                {
                    ClassId = classId,
                    ClassImg = stream.ToArray()
                };

                _context.ClassImages.Add(classImage);
            }

            await _context.SaveChangesAsync();

            var result = await _context.ClassImages
                .FirstOrDefaultAsync(x => x.ClassId == classId);

            return Ok(result); 
        }

        [HttpGet("GetDBImage")]
        public async Task<IActionResult> GetDBImage(int classId)
        {
            var classImage = await _context.ClassImages
                .FirstOrDefaultAsync(x => x.ClassId == classId);

            if (classImage == null || classImage.ClassImg == null)
                return NotFound();

            return File(classImage.ClassImg, "image/jpeg");
        }
    }
}
