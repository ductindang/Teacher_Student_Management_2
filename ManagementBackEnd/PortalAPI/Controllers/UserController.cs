using DAL.Models;
using DAL.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IPasswordHasher<User> _passHasher;

        public UserController(IUserRepository userRepo, IPasswordHasher<User> passHasher)
        {
            _userRepo = userRepo;
            _passHasher = passHasher;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var user = await _userRepo.GetAll();
            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var user = await _userRepo.GetById(id);
            if(user == null)
            {
                return NotFound("Cannot find this user");
            }
            return Ok(user);
        }

        [HttpGet("Email_Pass")]
        public async Task<ActionResult<User>> GetByEmailPass(string email, string password)
        {
            //var passwordHasher = new PasswordHasher<User>();
            var userExist = await _userRepo.GetByEmail(email);
            if(userExist == null) 
                return NotFound();
            var result = _passHasher.VerifyHashedPassword(
                userExist,
                userExist.PasswordHash,
                password
            );

            if (result == PasswordVerificationResult.Success)
            {
                return Ok(userExist);
            }

            return NotFound("Password do not match");
        }

        [HttpGet("email")]
        public async Task<ActionResult<User>> GetByEmail(string email)
        {
            var user = await _userRepo.GetByEmail(email);
            if (user == null) 
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> Insert(User user)
        {
            user.Id = 0;
            try
            {
                var userExist = await _userRepo.GetByEmail(user.Email);
                if (userExist != null)
                    return Conflict("Email existed!");
                user.CreatedAt = DateTime.Now;
                user.PasswordHash = _passHasher.HashPassword(user, user.PasswordHash);
                var userInsert = await _userRepo.Insert(user);
                if (user == null)
                {
                    return BadRequest();
                }
                return Ok(userInsert);
            }
            catch (Exception ex)
            {
                return BadRequest(error: ex.Message);
            }
            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Update(int id, [FromBody] User user)
        {
            try
            {
                var userExist = await _userRepo.GetById(id);
                if (userExist == null)
                    return NotFound("Cannot find this user id");
                userExist.Username = user.Username;
                userExist.Email = user.Email;
                userExist.Phone = user.Phone;
                userExist.Status = user.Status;
                userExist.RoleId = user.RoleId;
                userExist.Id = id;
                if (!string.IsNullOrEmpty(user.PasswordHash))
                {
                    userExist.PasswordHash = _passHasher.HashPassword(user, user.PasswordHash);
                }

                var result = await _userRepo.Update(userExist);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(error: ex.Message);
            }
        }

        //[HttpDelete("{id}")]
        //public async Task<ActionResult<User>> Delete(int id)
        //{
        //    var user = await _userRepo.GetById(id);
        //    try
        //    {
        //        if (user == null)
        //            return NotFound();

        //        await _userRepo.Delete(user);
        //        return Ok(user);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
