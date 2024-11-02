using FASTFOOD.Models;
namespace FASTFOOD.Responsitory
{
    public interface ILoaiMonResponsitory
    {
        LoaiMonAn Add(LoaiMonAn TenLoaiMonAn);
        LoaiMonAn Update(LoaiMonAn TenLoaiMonAn);
        LoaiMonAn Delete(String  MaLoaiMonAn);
        LoaiMonAn Get(String MaLoaiMonAn);
        IEnumerable<LoaiMonAn> GetAllTenLoaiMonAn();
    }
}
