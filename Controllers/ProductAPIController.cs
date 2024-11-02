using FASTFOOD.Models;
using FASTFOOD.Models.MonAnModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FASTFOOD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        QLBanDoAnContext db =new QLBanDoAnContext();
        [HttpGet]
        public IEnumerable<MonAn> GetAllMonAn()
        {
            var MonAn = (from p in db.MonAns
                         select new MonAn
                         {
                             MaMonAn = p.MaMonAn,
                             TenMonAn= p.TenMonAn,
                             AnhDaiDien=p.AnhDaiDien,
                             Gia=p.Gia,
                             MoTa=p.MoTa
                         }).ToList();
            return MonAn;
        }
        [HttpGet("{maloaimonan}")]
        public IEnumerable<MonAn> GetMonAnByCategory(int maloaimonan)
        {
            var MonAn = (from p in db.MonAns
                         where p.MaLoaiMonAn == maloaimonan
                         select new MonAn
                         {
                             MaMonAn = p.MaMonAn,
                             TenMonAn = p.TenMonAn,
                             AnhDaiDien = p.AnhDaiDien,
                             Gia = p.Gia,
                             MoTa = p.MoTa
                         }).ToList();
            return MonAn;
        }
    }
}
