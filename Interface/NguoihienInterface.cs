using HienTangToc.Models;

namespace HienTangToc.Interface
{
    public interface NguoihienInterface
    {
        Task<List<NguoihienModel>> GetAllNguoihienAsync(int pageNumber = 1, int pageSize = 10);
        Task<NguoihienModel> GetNguoihienByIdAsync(int id);
        Task<(NguoihienModel nguoiHien, string message)> CreateNguoihienAsync(NguoihienModel nguoiHien);
        Task<(NguoihienModel originalNguoiHien, NguoihienModel updatedNguoiHien, string message)> UpdateNguoihienAsync(int id, NguoihienModel nguoiHienUpdate);
        Task<(bool success, string message)> DeleteNguoihienAsync(int id);
        public int CountNguoiHien();
    }
}
