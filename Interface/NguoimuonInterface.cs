using HienTangToc.Models;

namespace HienTangToc.Interface
{
    public interface NguoimuonInterface
    {
        Task<List<NguoimuonModel>> GetAllNguoimuonsAsync(int pageNumber = 1, int pageSize = 10);
        Task<NguoimuonModel> GetNguoimuonByIdAsync(int id);
        Task<(NguoimuonModel nguoiMuon, string message)> CreateNguoimuonAsync(NguoimuonModel nguoiMuon);
        Task<(NguoimuonModel originalNguoiMuon, NguoimuonModel updatedNguoiMuon, string message)> UpdateNguoimuonAsync(int id, NguoimuonModel nguoiMuonUpdate);
        Task<(bool success, string message)> DeleteNguoimuonAsync(int id);
        public int CountNguoiMuon();
    }
}
