using HienTangToc.Data;
using HienTangToc.Interface;
using HienTangToc.Models;
using Microsoft.EntityFrameworkCore;

namespace HienTangToc.Services
{
    public class NguoimuonService : NguoimuonInterface
    {
        private readonly HientocDbcontext _context;

        public NguoimuonService(HientocDbcontext context)
        {
            _context = context;
        }

        public async Task<List<NguoimuonModel>> GetAllNguoimuonsAsync(int pageNumber = 1, int pageSize = 10)
        {
            return await _context.NguoimuonModels
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<NguoimuonModel> GetNguoimuonByIdAsync(int id)
        {
            return await _context.NguoimuonModels.FindAsync(id);
        }

        public async Task<(NguoimuonModel nguoiMuon, string message)> CreateNguoimuonAsync(NguoimuonModel nguoiMuon)
        {
            if (string.IsNullOrWhiteSpace(nguoiMuon.Hoten) ||
            string.IsNullOrWhiteSpace(nguoiMuon.Sdt) ||
            string.IsNullOrWhiteSpace(nguoiMuon.Diachi) ||
            string.IsNullOrWhiteSpace(nguoiMuon.Email) ||
            nguoiMuon.Gioitinh < 0 ||
            string.IsNullOrWhiteSpace(nguoiMuon.Nguyennhan) ||
            nguoiMuon.Ngaydangky == DateTime.MinValue ||
            nguoiMuon.Ngaytra == DateTime.MinValue)
            {
                return (null, "Các trường thông tin không được để trống hoặc không hợp lệ.");
            }

            _context.NguoimuonModels.Add(nguoiMuon);
            await _context.SaveChangesAsync();

            return (nguoiMuon, "Tạo người mượn thành công.");
        }

        public async Task<(NguoimuonModel originalNguoiMuon, NguoimuonModel updatedNguoiMuon, string message)> UpdateNguoimuonAsync(int id, NguoimuonModel nguoiMuonUpdate)
        {
            if (string.IsNullOrWhiteSpace(nguoiMuonUpdate.Hoten) ||
                string.IsNullOrWhiteSpace(nguoiMuonUpdate.Sdt) ||
                string.IsNullOrWhiteSpace(nguoiMuonUpdate.Diachi) ||
                string.IsNullOrWhiteSpace(nguoiMuonUpdate.Email) ||
                nguoiMuonUpdate.Gioitinh < 0 ||
                string.IsNullOrWhiteSpace(nguoiMuonUpdate.Nguyennhan) ||
                nguoiMuonUpdate.Ngaydangky == DateTime.MinValue ||
                nguoiMuonUpdate.Ngaytra == DateTime.MinValue)
            {
                return (null, null, "Các trường thông tin không được để trống hoặc không hợp lệ.");
            }

            var existingNguoimuon = await _context.NguoimuonModels.FindAsync(id);
            if (existingNguoimuon == null)
            {
                return (null, null, "Người mượn không tồn tại.");
            }

            var originalNguoiMuon = new NguoimuonModel
            {
                Idnm = existingNguoimuon.Idnm,
                Hoten = existingNguoimuon.Hoten,
                Sdt = existingNguoimuon.Sdt,
                Diachi = existingNguoimuon.Diachi,
                Email = existingNguoimuon.Email,
                Gioitinh = existingNguoimuon.Gioitinh,
                Ngaysinh = existingNguoimuon.Ngaysinh,
                Nguyennhan = existingNguoimuon.Nguyennhan,
                Ngaydangky = existingNguoimuon.Ngaydangky,
                Ngaytra = existingNguoimuon.Ngaytra
            };

            existingNguoimuon.Hoten = nguoiMuonUpdate.Hoten;
            existingNguoimuon.Sdt = nguoiMuonUpdate.Sdt;
            existingNguoimuon.Diachi = nguoiMuonUpdate.Diachi;
            existingNguoimuon.Email = nguoiMuonUpdate.Email;
            existingNguoimuon.Gioitinh = nguoiMuonUpdate.Gioitinh;
            existingNguoimuon.Ngaysinh = nguoiMuonUpdate.Ngaysinh;
            existingNguoimuon.Nguyennhan = nguoiMuonUpdate.Nguyennhan;
            existingNguoimuon.Ngaydangky = nguoiMuonUpdate.Ngaydangky;
            existingNguoimuon.Ngaytra = nguoiMuonUpdate.Ngaytra;

            _context.NguoimuonModels.Update(existingNguoimuon);
            await _context.SaveChangesAsync();

            return (originalNguoiMuon, existingNguoimuon, "Cập nhật người mượn thành công.");
        }

        public async Task<(bool success, string message)> DeleteNguoimuonAsync(int id)
        {
            var existingNguoimuon = await _context.NguoimuonModels.FindAsync(id);
            if (existingNguoimuon == null)
            {
                return (false, "Không tìm thấy người mượn.");
            }

            _context.NguoimuonModels.Remove(existingNguoimuon);
            await _context.SaveChangesAsync();

            return (true, "Xóa người mượn thành công.");
        }

        public int CountNguoiMuon()
        {
            return _context.NguoimuonModels.Count();
        }
    }
}
