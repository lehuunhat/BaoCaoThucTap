using Microsoft.AspNetCore.Mvc;
using HienTangToc.Interface;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class LoginController : Controller
{
    private readonly UserInterface _userService;

    public LoginController(UserInterface userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string emailOrPhone, string password)
    {
        if (string.IsNullOrEmpty(emailOrPhone) || string.IsNullOrEmpty(password))
        {
            ModelState.AddModelError("", "Email/Số điện thoại và mật khẩu không được để trống.");
            return View();
        }

        var user = await _userService.AuthenticateUser(emailOrPhone, password);

        if (user == null)
        {
            ModelState.AddModelError("", "Email/Số điện thoại hoặc mật khẩu không đúng.");
            return View();
        }

        HttpContext.Session.SetString("UserName", user.Name);
        HttpContext.Session.SetString("UserRole", user.Role);

        if (user.Role == "admin")
        {
            return RedirectToAction("Admin", "Admin");
        }
        else
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
