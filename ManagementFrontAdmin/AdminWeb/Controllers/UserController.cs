using BLL.DTOs;
using BLL.DTOs.Enum;
using BLL.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AdminWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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
                    if(userCheck.RoleId != (int)ERoleName.Admin)
                    {
                        return StatusCode(500, "Bạn không có quyền truy cập vào đây");
                    }
                    var userCheckJson = JsonConvert.SerializeObject(userCheck);
                    HttpContext.Session.SetString("UserLogin", userCheckJson);
                    return Ok("Đăng nhập thành công!");
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
                user.RoleId = (int)ERoleName.Admin;
                user.Status = EAccountStatus.Active;
                var newUser = await _userService.InsertUser(user);
                if (newUser == null)
                {
                    return BadRequest("Tạo mới tài khoản thất bại");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Tạo mới tài khoản thành công");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsers();

            ViewData["page"] = "user";
            ViewData["users"] = users;

            return View(users);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userService.GetUserById(id);
            if(user == null)
            {
                TempData["Error"] = "Lỗi khi truy cập người dùng này";
                return View("Error");
            }

            return View(user);
        }

        //[HttpPost]
        //public async Task<IActionResult> Edit(User user)
        //{
        //    user.PasswordHash = user.PasswordHash ?? "";
        //    string resultMessage = "Cannot update this user";
        //    var userUpdate = await _userService.UpdateUser(user);
        //    if(userUpdate != null)
        //    {
        //        TempData["Success"] = "Update this user success";
        //        return RedirectToAction("Index");
        //    }

        //    TempData["Error"] = resultMessage;
        //    return View(user);
        //}

        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            user.PasswordHash ??= "";

            var userUpdate = await _userService.UpdateUser(user);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                if (userUpdate != null)
                {
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
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Không thể chỉnh sửa người dùng này";
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(User request)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    message = "Dữ liệu không hợp lệ"
                });
            }

            var result = await _userService.InsertUser(request);

            if (result == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Không thể tạo tài khoản"
                });
            }

            return Json(new
            {
                success = true,
                message = "Tạo tài khoản thành công",
                userId = result.Id
            });
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
