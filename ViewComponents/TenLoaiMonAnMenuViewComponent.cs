using FASTFOOD.Models;
using FASTFOOD.Responsitory;
using Microsoft.AspNetCore.Mvc;
namespace FASTFOOD.ViewComponents
{
    public class TenLoaiMonAnMenuViewComponent : ViewComponent
    {
        private readonly ILoaiMonResponsitory _TenLoaiMonAn;
        public TenLoaiMonAnMenuViewComponent(ILoaiMonResponsitory TenLoaiMonAnResoinsitory)
        {
            _TenLoaiMonAn = TenLoaiMonAnResoinsitory;
        }
        public IViewComponentResult Invoke()
        {
            var TenLoaiMonAn = _TenLoaiMonAn.GetAllTenLoaiMonAn().OrderBy(x=>x.TenLoaiMonAn);
            return View(TenLoaiMonAn);

        }
    }
}
