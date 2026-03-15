using BLL.DTOs;
using BLL.DTOs.Enum;
using BLL.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;

        public UserController(IUserService userService, ITeacherService teacherService, IStudentService studentService)
        {
            _userService = userService;
            _teacherService = teacherService;
            _studentService = studentService;
        }

        public IActionResult SignIn()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CheckSignIn([FromForm] string email, [FromForm] string password)
        {
            try
            {
                var userCheck = await _userService.GetUserByEmailPass(email, password);
                
                if (userCheck != null)
                {
                    if (userCheck.RoleId == 0 || userCheck.RoleId == 1)
                    {

                        if (userCheck.Status != EAccountStatus.Active)
                        {
                            return StatusCode(500, "Tài khoản của bạn đã bị khóa, vui lòng liên hệ admin để biết thêm chi tiết");
                        }
                        var userCheckJson = JsonConvert.SerializeObject(userCheck);
                        HttpContext.Session.SetString("UserLogin", userCheckJson);
                        if(userCheck.RoleId == (int)ERoleName.Teacher)
                        {
                            var teacher = await _teacherService.GetTeacherByUserId(userCheck.Id);
                            var teacherJson = JsonConvert.SerializeObject(teacher);
                            HttpContext.Session.SetString("Teacher", teacherJson);
                        }else if (userCheck.RoleId == (int)ERoleName.Student)
                        {
                            var student = await _studentService.GetStudentByUserId(userCheck.Id);
                            var studentJson = JsonConvert.SerializeObject(student);
                            HttpContext.Session.SetString("Student", studentJson);
                        }
                        return Ok("Đăng nhập thành công!");
                    }
                }
                return NotFound("Sai email hoặc mật khẩu");
            }
            catch
            {
                return StatusCode(500, "Lỗi hệ thống. Vui lòng thử lại sau");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CheckEmailExist([FromForm] string email)
        {
            var user = await _userService.GetUserByEmail(email);
            return Ok(user != null);
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromForm] User user)
        {
            try
            {
                var newUser = await _userService.InsertUser(user);
                if (newUser == null)
                {
                    return BadRequest("Create user failed");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Create user failed");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            user.PasswordHash ??= "";

            var userUpdate = await _userService.UpdateUser(user);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                if (userUpdate != null)
                {
                    var userCheckJson = JsonConvert.SerializeObject(userUpdate);
                    HttpContext.Session.SetString("UserLogin", userCheckJson);
                    return Json(new
                    {
                        success = true,
                        message = "Chỉnh sửa người dùng thành công"
                    });
                }

                return Json(new
                {
                    success = false,
                    message = "Không thể chỉnh sửa hồ sơ này"
                });
            }

            if (userUpdate != null)
            {
                TempData["Success"] = "Chỉnh sửa hồ sơ thành công";
                var userCheckJson = JsonConvert.SerializeObject(userUpdate);
                HttpContext.Session.SetString("UserLogin", userCheckJson);
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Không thể chỉnh sửa người dùng này";
            return View(user);
        }

        public async Task<IActionResult> Profile(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                TempData["Error"] = "Lỗi khi truy cập hồ sơ";
                return View("Error");
            }

            return View(user);
        }

    }
}
