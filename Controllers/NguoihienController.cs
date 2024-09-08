using HienTangToc.Interface;
using HienTangToc.Models;
using HienTangToc.Services;
using Microsoft.AspNetCore.Mvc;

namespace HienTangToc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NguoihienController : ControllerBase
    {
        private readonly NguoihienInterface _nguoiHienService;

        public NguoihienController(NguoihienInterface nguoiHienService)
        {
            _nguoiHienService = nguoiHienService;
        }

        [HttpGet]
        public async Task<ActionResult<List<NguoihienModel>>> GetAllNguoihien(int pageNumber = 1, int pageSize = 10)
        {
            var nguoiHien = await _nguoiHienService.GetAllNguoihienAsync(pageNumber, pageSize);
            return Ok(nguoiHien);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NguoihienModel>> GetNguoihienById(int id)
        {
            var nguoiHien = await _nguoiHienService.GetNguoihienByIdAsync(id);
            if (nguoiHien == null)
            {
                return NotFound("NguoiHien not found.");
            }
            return Ok(nguoiHien);
        }

        [HttpPost]
        public async Task<ActionResult> CreateNguoihien(NguoihienModel nguoiHien)
        {
            var (createdNguoiHien, message) = await _nguoiHienService.CreateNguoihienAsync(nguoiHien);
            if (createdNguoiHien == null)
            {
                return BadRequest(message);
            }
            return CreatedAtAction(nameof(GetNguoihienById), new { id = createdNguoiHien.Idnh }, createdNguoiHien);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateNguoihien(int id, NguoihienModel nguoiHienUpdate)
        {
            var (originalNguoiHien, updatedNguoiHien, message) = await _nguoiHienService.UpdateNguoihienAsync(id, nguoiHienUpdate);
            if (updatedNguoiHien == null)
            {
                return BadRequest(message);
            }
            return Ok(updatedNguoiHien);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNguoihien(int id)
        {
            var (success, message) = await _nguoiHienService.DeleteNguoihienAsync(id);
            if (!success)
            {
                return NotFound(message);
            }
            return NoContent();
        }
    }
}
