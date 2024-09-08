using HienTangToc.Data;
using HienTangToc.Interface;
using HienTangToc.Models;
using Microsoft.EntityFrameworkCore;

namespace HienTangToc.Services
{
    public class NguoihienService : NguoihienInterface
    {
        private readonly HientocDbcontext _context;

        public NguoihienService(HientocDbcontext context)
        {
            _context = context;
        }

        public async Task<List<NguoihienModel>> GetAllNguoihienAsync(int pageNumber = 1, int pageSize = 10)
        {
            return await _context.NguoihienModels
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<NguoihienModel> GetNguoihienByIdAsync(int id)
        {
            return await _context.NguoihienModels.FindAsync(id);
        }

        public async Task<(NguoihienModel nguoiHien, string message)> CreateNguoihienAsync(NguoihienModel nguoiHien)
        {
            if (string.IsNullOrWhiteSpace(nguoiHien.Hoten) ||
           string.IsNullOrWhiteSpace(nguoiHien.Sdt) ||
           string.IsNullOrWhiteSpace(nguoiHien.Diachi) ||
           string.IsNullOrWhiteSpace(nguoiHien.Email) ||
           nguoiHien.Gioitinh < 0 ||
           nguoiHien.Ngayhien == DateTime.MinValue)
            {
                return (null, "Các trường thông tin không được để trống hoặc không hợp lệ.");
            }
            _context.NguoihienModels.Add(nguoiHien);
            await _context.SaveChangesAsync();

            return (nguoiHien, "Tạo người hiến thành công.");
        }

        public async Task<(NguoihienModel originalNguoiHien, NguoihienModel updatedNguoiHien, string message)> UpdateNguoihienAsync(int id, NguoihienModel nguoiHienUpdate)
        {
            if (string.IsNullOrWhiteSpace(nguoiHienUpdate.Hoten) ||
                string.IsNullOrWhiteSpace(nguoiHienUpdate.Sdt) ||
                string.IsNullOrWhiteSpace(nguoiHienUpdate.Diachi) ||
                string.IsNullOrWhiteSpace(nguoiHienUpdate.Email) ||
                nguoiHienUpdate.Gioitinh < 0 ||
                nguoiHienUpdate.Ngayhien == DateTime.MinValue)
            {
                return (null, null, "Các trường thông tin không được để trống hoặc không hợp lệ.");
            }

            var existingNguoihien = await _context.NguoihienModels.FindAsync(id);
            if (existingNguoihien == null)
            {
                return (null, null, "Người hiến không tồn tại.");
            }

            var originalNguoiHien = new NguoihienModel
            {
                Idnh = existingNguoihien.Idnh,
                Hoten = existingNguoihien.Hoten,
                Sdt = existingNguoihien.Sdt,
                Diachi = existingNguoihien.Diachi,
                Email = existingNguoihien.Email,
                Gioitinh = existingNguoihien.Gioitinh,
                Ngayhien = existingNguoihien.Ngayhien
            };

            existingNguoihien.Hoten = nguoiHienUpdate.Hoten;
            existingNguoihien.Sdt = nguoiHienUpdate.Sdt;
            existingNguoihien.Diachi = nguoiHienUpdate.Diachi;
            existingNguoihien.Email = nguoiHienUpdate.Email;
            existingNguoihien.Gioitinh = nguoiHienUpdate.Gioitinh;
            existingNguoihien.Ngayhien = nguoiHienUpdate.Ngayhien;

            _context.NguoihienModels.Update(existingNguoihien);
            await _context.SaveChangesAsync();

            return (originalNguoiHien, existingNguoihien, "Cập nhật người hiến thành công.");
        }

        public async Task<(bool success, string message)> DeleteNguoihienAsync(int id)
        {
            var existingNguoihien = await _context.NguoihienModels.FindAsync(id);
            if (existingNguoihien == null)
            {
                return (false, "Không tìm thấy người hiến.");
            }

            _context.NguoihienModels.Remove(existingNguoihien);
            await _context.SaveChangesAsync();

            return (true, "Xóa người hiến thành công.");
        }

        public int CountNguoiHien()
        {
            return _context.NguoihienModels.Count();
        }

    }
}
