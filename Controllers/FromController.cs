using HienTangToc.Models;
using HienTangToc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace HienTangToc.Controllers
{
    public class FromController : Controller
    {
        private readonly NguoihienService _nguoiHienService;
        private readonly NguoimuonService _nguoiMuonService;
        private readonly SalonService _salonService;

        public FromController(NguoihienService nguoiHienService, NguoimuonService nguoiMuonService, SalonService salonService)
        {
            _nguoiHienService = nguoiHienService;
            _nguoiMuonService = nguoiMuonService;
            _salonService = salonService;
        }

        // GET: /From/Hientoc
        [HttpGet]
        public IActionResult Hientoc()
        {
            return View();
        }

        // POST: /From/Hientoc
        [HttpPost]
        public async Task<IActionResult> CreateHientoc(NguoihienModel model)
        {
            if (ModelState.IsValid)
            {
                var (nguoiHien, message) = await _nguoiHienService.CreateNguoihienAsync(model);

                if (nguoiHien != null)
                {
                    TempData["SuccessMessage"] = message; // Thông báo thành công
                    return RedirectToAction("Hientoc");
                }
                else
                {
                    TempData["ErrorMessage"] = message; // Thông báo lỗi
                }
            }
            return View("Hientoc", model);
        }

        [HttpGet]
        public IActionResult Muontoc()
        {
            return View();
        }

        // POST: /From/Muontoc
        [HttpPost]
        public async Task<IActionResult> CreateMuontoc([FromForm] NguoimuonModel model)
        {
            if (ModelState.IsValid)
            {
                var (nguoiMuon, message) = await _nguoiMuonService.CreateNguoimuonAsync(model);

                if (nguoiMuon != null)
                {
                    TempData["SuccessMessage"] = message; // Thông báo thành công
                    return RedirectToAction("Muontoc");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, message); // Thông báo lỗi
                }
            }
            return View("Muontoc", model);
        }

        [HttpGet]
        public IActionResult Salontoc()
        {
            return View();
        }

        // POST: /From/Salontoc
        [HttpPost]
        public async Task<IActionResult> CreateSalontoc([FromForm] SalonModel salon, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                var (resultSalon, message) = await _salonService.CreateSalonAsync(salon, imageFile);

                if (resultSalon != null)
                {
                    TempData["SuccessMessage"] = message; // Thông báo thành công
                    return RedirectToAction("Salontoc");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, message); // Thông báo lỗi
                }
            }
            return View("Salontoc", salon);
        }

        // GET: /From/Success
        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }
    }
}
