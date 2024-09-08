using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace HienTangToc.Controllers
{
    public class LogoutController : Controller
    {
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Xóa toàn bộ dữ liệu trong session
            return RedirectToAction("Index", "Home");
        }
    }
}
