using DAL.Models;
using DAL.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepo;

        public RoleController(IRoleRepository roleRepo)
        {
            _roleRepo = roleRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetAll()
        {
            var roles = await _roleRepo.GetAll();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetById(int id)
        {
            var role = await _roleRepo.GetById(id);
            if (role == null)
                return NotFound();
            return Ok(role);
        }

        [HttpPost]
        public async Task<ActionResult<Role>> Insert([FromBody] Role role)
        {
            role.Id = 0;
            try
            {
                var roleInsert = await _roleRepo.Insert(role);
                return Ok(roleInsert);
            }
            catch (Exception ex)
            {
                return BadRequest(error: ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Role>> Update(int id, [FromBody] Role role)
        {
            var roleUpdate = await _roleRepo.GetById(id);
            try
            {
                if (roleUpdate == null)
                {
                    return NotFound();
                }
                roleUpdate = role;
                roleUpdate.Id = id;
                var result = await _roleRepo.Update(roleUpdate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(error: ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Role>> Delete(int id)
        {
            var roleExist = await _roleRepo.GetById(id);
            try
            {
                if (roleExist == null)
                    return NotFound();

                await _roleRepo.Delete(roleExist);
                return Ok(roleExist);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
