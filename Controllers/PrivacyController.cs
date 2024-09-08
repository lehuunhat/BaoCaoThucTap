using HienTangToc.Interface;
using HienTangToc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HienTangToc.Controllers
{
    public class PrivacyController : Controller
    {
        private readonly UserInterface _userService;
        private readonly SalonInterface _salonService;
        private readonly NguoihienInterface _nguoiHienService;
        private readonly NguoimuonInterface _nguoiMuonService;

        public PrivacyController(
            UserInterface userService,
            SalonInterface salonService,
            NguoihienInterface nguoiHienService,
            NguoimuonInterface nguoiMuonService)
        {
            _userService = userService;
            _salonService = salonService;
            _nguoiHienService = nguoiHienService;
            _nguoiMuonService = nguoiMuonService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            var salons = await _salonService.GetAllSalonsAsync();
            var nguoiHien = await _nguoiHienService.GetAllNguoihienAsync();
            var nguoiMuon = await _nguoiMuonService.GetAllNguoimuonsAsync();

            ViewData["Users"] = users;
            ViewData["Salons"] = salons;
            ViewData["NguoiHien"] = nguoiHien;
            ViewData["NguoiMuon"] = nguoiMuon;

            return View();
        }
    }
}
