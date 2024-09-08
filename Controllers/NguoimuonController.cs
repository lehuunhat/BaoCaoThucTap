using HienTangToc.Interface;
using HienTangToc.Models;
using Microsoft.AspNetCore.Mvc;

namespace HienTangToc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NguoimuonController : ControllerBase
    {
        private readonly NguoimuonInterface _nguoiMuonService;

        public NguoimuonController(NguoimuonInterface nguoiMuonService)
        {
            _nguoiMuonService = nguoiMuonService;
        }

        [HttpGet]
        public async Task<ActionResult<List<NguoimuonModel>>> GetAllNguoimuons(int pageNumber = 1, int pageSize = 10)
        {
            var nguoiimuons = await _nguoiMuonService.GetAllNguoimuonsAsync(pageNumber, pageSize);
            return Ok(nguoiimuons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NguoimuonModel>> GetNguoimuonById(int id)
        {
            var nguoiMuon = await _nguoiMuonService.GetNguoimuonByIdAsync(id);
            if (nguoiMuon == null)
            {
                return NotFound("NguoiMuon not found.");
            }
            return Ok(nguoiMuon);
        }

        [HttpPost]
        public async Task<ActionResult> CreateNguoimuon(NguoimuonModel nguoiMuon)
        {
            var (createdNguoiMuon, message) = await _nguoiMuonService.CreateNguoimuonAsync(nguoiMuon);
            if (createdNguoiMuon == null)
            {
                return BadRequest(message);
            }
            return CreatedAtAction(nameof(GetNguoimuonById), new { id = createdNguoiMuon.Idnm }, createdNguoiMuon);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateNguoimuon(int id, NguoimuonModel nguoiMuonUpdate)
        {
            var (originalNguoiMuon, updatedNguoiMuon, message) = await _nguoiMuonService.UpdateNguoimuonAsync(id, nguoiMuonUpdate);
            if (updatedNguoiMuon == null)
            {
                return BadRequest(message);
            }
            return Ok(updatedNguoiMuon);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNguoimuon(int id)
        {
            var (success, message) = await _nguoiMuonService.DeleteNguoimuonAsync(id);
            if (!success)
            {
                return NotFound(message);
            }
            return NoContent();
        }
    }
}
