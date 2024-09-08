using HienTangToc.Services;
using Microsoft.AspNetCore.Mvc;

namespace HienTangToc.Controllers
{
    public class AdminController : Controller
    {
        private readonly StatisticsService _statisticsService;

        public AdminController(StatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        public IActionResult Admin()
        {
            var userRole = HttpContext.Session.GetString("UserRole");

            int soNguoiHien = _statisticsService.CountNguoiHien();
            int soNguoiMuon = _statisticsService.CountNguoiMuon();
            int soSalon = _statisticsService.CountSalon();
            int soToc = _statisticsService.CountToc();
            Console.WriteLine($"Số người hiến tóc: {soNguoiHien}");
            Console.WriteLine($"Số người mượn tóc: {soNguoiMuon}");
            Console.WriteLine($"Số người salon tóc: {soSalon}");
            Console.WriteLine($"Số người salon tóc: {soToc}");

            ViewData["SoNguoiHien"] = soNguoiHien;
            ViewData["SoNguoiMuon"] = soNguoiMuon;
            ViewData["SoSalon"] = soSalon;
            ViewData["SoToc"] = soToc;

            if (userRole != "admin")
            {
                return RedirectToAction("Index", "Home");
            }
            var userName = HttpContext.Session.GetString("UserName");

            // Truyền tên người dùng vào ViewData
            ViewData["UserName"] = userName;

            return View();
        }
    }
}
