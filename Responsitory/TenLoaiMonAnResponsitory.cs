using FASTFOOD.Models;

namespace FASTFOOD.Responsitory
{
    public class TenLoaiMonAnResponsitory : ILoaiMonResponsitory
    {
        private readonly QLBanDoAnContext _context;
        public TenLoaiMonAnResponsitory(QLBanDoAnContext context)
        {
            _context = context;
        }
        public LoaiMonAn Add(LoaiMonAn TenLoaiMonAn)
        {
            _context.Add(TenLoaiMonAn);
            _context.SaveChanges();
            return TenLoaiMonAn;
        }

        public LoaiMonAn Delete(string MaLoaiMonAn)
        {
            throw new NotImplementedException();
        }

        public LoaiMonAn Get(string MaLoaiMonAn)
        {
            return _context.LoaiMonAns.Find(MaLoaiMonAn);
        }

        public IEnumerable<LoaiMonAn> GetAllTenLoaiMonAn()
        {
            return _context.LoaiMonAns;
        }

        public LoaiMonAn Update(LoaiMonAn TenLoaiMonAn)
        {
            _context.Update(TenLoaiMonAn);
            _context.SaveChanges();
            return TenLoaiMonAn;
        }
    }
}
