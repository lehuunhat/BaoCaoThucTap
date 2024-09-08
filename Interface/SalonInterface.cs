using HienTangToc.Models;

namespace HienTangToc.Interface
{
    public interface SalonInterface
    {
        Task<List<SalonModel>> GetAllSalonsAsync(int pageNumber = 1, int pageSize = 10);
        Task<SalonModel> GetSalonByIdAsync(int id);
        Task<(SalonModel salon, string message)> CreateSalonAsync(SalonModel salon, IFormFile imageFile);
        Task<(SalonModel originalSalon, SalonModel updatedSalon, string message)> UpdateSalonAsync(int id, SalonModel salonUpdate);
        Task<(bool success, string message)> DeleteSalonAsync(int id);
        public int CountSalon();
    }
}
