using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HienTangToc.Data;
using HienTangToc.Models;
using System.Threading.Tasks;
using System.Linq;

namespace HienTangToc.Controllers
{
    public class HomeController : Controller
    {
        private readonly HientocDbcontext _context;

        public HomeController(HientocDbcontext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var salons = await _context.SalonModels.ToListAsync();
            ViewBag.Salons = salons;
            ViewData["UserName"] = HttpContext.Session.GetString("UserName");
            ViewData["UserRole"] = HttpContext.Session.GetString("UserRole");
            return View();
        }

        public async Task<IActionResult> Salon(int id)
        {
            var salon = await _context.SalonModels.FindAsync(id);
            if (salon == null)
            {
                return NotFound();
            }

            // Đảm bảo luôn có danh sách salon để hiển thị trong menu
            ViewBag.Salons = await _context.SalonModels.ToListAsync();
            return View(salon);
        }

       
    }
}
