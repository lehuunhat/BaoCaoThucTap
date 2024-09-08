using System.Collections.Generic;
using HienTangToc.Models;

namespace HienTangToc.Models
{
    public class HomeViewModel
    {
        public List<UserModel> Users { get; set; }
        public List<SalonModel> Salons { get; set; }
        public List<NguoihienModel> NguoiHiens { get; set; }
        public List<NguoimuonModel> NguoiMuons { get; set; }
    }
}
