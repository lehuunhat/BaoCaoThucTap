using HienTangToc.Data;
using HienTangToc.Interface;
using HienTangToc.Models;
using Microsoft.EntityFrameworkCore;

namespace HienTangToc.Services
{
    public class SalonService : SalonInterface
    {
        private readonly HientocDbcontext _context;

        public SalonService(HientocDbcontext context)
        {
            _context = context;
        }


        public async Task<List<SalonModel>> GetAllSalonsAsync(int pageNumber = 1, int pageSize = 10)
        {
            return await _context.SalonModels
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<SalonModel> GetSalonByIdAsync(int id)
        {
            return await _context.SalonModels.FindAsync(id);
        }

        public async Task<(SalonModel salon, string message)> CreateSalonAsync(SalonModel salon, IFormFile imageFile)
        {
            // Kiểm tra các trường thông tin không được để trống
            if (string.IsNullOrWhiteSpace(salon.Ten) ||
                string.IsNullOrWhiteSpace(salon.Diachi) ||
                string.IsNullOrWhiteSpace(salon.Facebook) ||
                string.IsNullOrWhiteSpace(salon.Thoigianhd) ||
                string.IsNullOrWhiteSpace(salon.Uudai))
            {
                return (null, "Các trường thông tin không được để trống.");
            }

            // Kiểm tra xem tệp hình ảnh có được cung cấp không và xử lý nếu có
            if (imageFile != null)
            {
                if (imageFile.Length > 0)
                {
                    var fileExtension = Path.GetExtension(imageFile.FileName).ToLower();
                    if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".png")
                    {
                        return (null, "File ảnh phải có định dạng JPG hoặc PNG.");
                    }

                    var fileName = $"{Guid.NewGuid()}{fileExtension}";
                    var filePath = Path.Combine("Data/images", fileName);

                    // Lưu tệp hình ảnh vào hệ thống file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    salon.Image = $"/images/{fileName}";
                }
                else
                {
                    return (null, "File ảnh không hợp lệ.");
                }
            }
            else
            {
                // Nếu không có hình ảnh, giữ giá trị hình ảnh mặc định hoặc yêu cầu phải có hình ảnh
                if (string.IsNullOrWhiteSpace(salon.Image))
                {
                    return (null, "Bạn phải chọn một tệp ảnh cho salon.");
                }
            }

            // Thêm salon vào cơ sở dữ liệu
            _context.SalonModels.Add(salon);
            await _context.SaveChangesAsync();

            return (salon, "Tạo salon thành công.");
        }


        public async Task<(SalonModel originalSalon, SalonModel updatedSalon, string message)> UpdateSalonAsync(int id, SalonModel salonUpdate)
        {
            if (string.IsNullOrWhiteSpace(salonUpdate.Ten) ||
                string.IsNullOrWhiteSpace(salonUpdate.Image) ||
                string.IsNullOrWhiteSpace(salonUpdate.Diachi) ||
                string.IsNullOrWhiteSpace(salonUpdate.Facebook) ||
                string.IsNullOrWhiteSpace(salonUpdate.Thoigianhd) ||
                string.IsNullOrWhiteSpace(salonUpdate.Uudai))
            {
                return (null, null, "Các trường thông tin không được để trống.");
            }

            var existingSalon = await _context.SalonModels.FindAsync(id);
            if (existingSalon == null)
            {
                return (null, null, "Salon không tồn tại.");
            }

            var originalSalon = new SalonModel
            {
                Idsl = existingSalon.Idsl,
                Ten = existingSalon.Ten,
                Image = existingSalon.Image,
                Diachi = existingSalon.Diachi,
                Facebook = existingSalon.Facebook,
                Thoigianhd = existingSalon.Thoigianhd,
                Uudai = existingSalon.Uudai
            };

            existingSalon.Ten = salonUpdate.Ten;
            existingSalon.Image = salonUpdate.Image;
            existingSalon.Diachi = salonUpdate.Diachi;
            existingSalon.Facebook = salonUpdate.Facebook;
            existingSalon.Thoigianhd = salonUpdate.Thoigianhd;
            existingSalon.Uudai = salonUpdate.Uudai;

            _context.SalonModels.Update(existingSalon);
            await _context.SaveChangesAsync();

            return (originalSalon, existingSalon, "Cập nhật salon thành công.");
        }

        public async Task<(bool success, string message)> DeleteSalonAsync(int id)
        {
            var existingSalon = await _context.SalonModels.FindAsync(id);
            if (existingSalon == null)
            {
                return (false, "Không tìm thấy salon.");
            }

            _context.SalonModels.Remove(existingSalon);
            await _context.SaveChangesAsync();

            return (true, "Xóa salon thành công.");
        }

        public int CountSalon()
        {
            return _context.SalonModels.Count();
        }
    }
}
