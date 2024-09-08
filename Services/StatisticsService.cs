using HienTangToc.Data;

namespace HienTangToc.Services
{
    public class StatisticsService
    {
        private readonly HientocDbcontext _context;

        public StatisticsService(HientocDbcontext context)
        {
            _context = context;
        }

        public int CountNguoiHien()
        {
            return _context.NguoihienModels.Count();
        }

        public int CountNguoiMuon()
        {
            return _context.NguoimuonModels.Count();
        }

        public int CountSalon()
        {
            return _context.SalonModels.Count();
        }

        public int CountToc()
        {
            return _context.TocModels.Sum(t => t.Soluong);
        }
    }
}
