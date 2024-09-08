using HienTangToc.Interface;
using HienTangToc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HienTangToc.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserInterface _userService;

        public RegisterController(UserInterface userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }



        public async Task<IActionResult> Register([FromForm] UserModel user)
        {
            var existingUserByEmail = await _userService.GetUserByEmailOrPhoneAsync(user.Email);
            if (existingUserByEmail != null)
            {
                ModelState.AddModelError(string.Empty, "Email đã tồn tại.");
                return View(user);  // Trả về trang đăng ký cùng với lỗi
            }

            var result = await _userService.CreateUserAsync(user);

            if (result.user == null)
            {
                ModelState.AddModelError(string.Empty, result.message);
                return View(user);  // Trả về trang đăng ký cùng với lỗi
            }

            HttpContext.Session.SetString("UserName", user.Name);

            return RedirectToAction("Index", "Home");
        }


    }
}
