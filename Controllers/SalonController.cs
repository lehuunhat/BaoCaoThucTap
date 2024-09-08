using HienTangToc.Interface;
using HienTangToc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HienTangToc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalonController : ControllerBase
    {
        private readonly SalonInterface _salonService;

        public SalonController(SalonInterface salonService)
        {
            _salonService = salonService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SalonModel>>> GetAllSalons(int pageNumber = 1, int pageSize = 10)
        {
            var salons = await _salonService.GetAllSalonsAsync(pageNumber, pageSize);
            return Ok(salons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SalonModel>> GetSalonById(int id)
        {
            var salon = await _salonService.GetSalonByIdAsync(id);
            if (salon == null)
            {
                return NotFound("Salon not found.");
            }
            return Ok(salon);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSalon(int id, SalonModel salonUpdate)
        {
            var (originalSalon, updatedSalon, message) = await _salonService.UpdateSalonAsync(id, salonUpdate);
            if (updatedSalon == null)
            {
                return BadRequest(message);
            }
            return Ok(updatedSalon);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSalon(int id)
        {
            var (success, message) = await _salonService.DeleteSalonAsync(id);
            if (!success)
            {
                return NotFound(message);
            }
            return NoContent();
        }
    }
}
