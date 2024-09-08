using HienTangToc.Data;
using HienTangToc.Interface;
using Microsoft.EntityFrameworkCore;

namespace HienTangToc.Services
{
    public class TocService : TocInterface
    {
        private readonly HientocDbcontext _context;

        public TocService(HientocDbcontext context)
        {
            _context = context;
        }
        public int CountToc()
        {
            return _context.TocModels.Sum(t => t.Soluong);
        }
    }
}
