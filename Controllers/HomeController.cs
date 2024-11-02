using FASTFOOD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;


using FASTFOOD.Models.Authentication;
using FASTFOOD.Models.ViewModels;

namespace FASTFOOD.Controllers
{
    public class HomeController : Controller
    { QLBanDoAnContext db = new QLBanDoAnContext();
        private readonly ILogger<HomeController> _logger;
        private readonly QLBanDoAnContext _context;
        public HomeController(ILogger<HomeController> logger, QLBanDoAnContext context)
        {
            _logger = logger;
            _context = context;
        }
        [Authentication]

        public IActionResult Index(int ? page)
        {   int  pageSize = 8;
            int  pagenumber = page==null||page<0?1:page.Value;
            var listMonAn = db.MonAns.AsNoTracking().OrderBy(x => x.TenMonAn);
            PagedList<MonAn> list=new PagedList<MonAn>(listMonAn,pagenumber,pageSize);
            return View(list);
        }
        [Authentication]
        public IActionResult MonAnTheoLoai(int MaLoaiMonAn, int ? page)
        {
            int pageSize = 8;
            int pagenumber = page == null || page < 0?1:page.Value;
            var listMonAn = db.MonAns.AsNoTracking().Where(x=>x.MaLoaiMonAn==MaLoaiMonAn).OrderBy(x => x.TenMonAn);
            PagedList<MonAn> list = new PagedList<MonAn>(listMonAn, pagenumber, pageSize);
            ViewBag.MaLoaiMonAn = MaLoaiMonAn;
            return View(list);
        }
        public IActionResult Chitietmonan(int MaMonAn)
        {   
            var monan=db.MonAns.SingleOrDefault(x=>x.MaMonAn==MaMonAn);
            if (monan == null)
            {
                return NotFound(); // hoặc return RedirectToAction("Index");
            }
            var anhmonan=db.MonAns.Where(x=>x.MaMonAn==MaMonAn).ToList();
            ViewBag.anhmonan = anhmonan;
            return View(monan);
 
        }
        [HttpGet]
        public async Task<IActionResult> TimKiemMonAn(string keyword, int page = 1)
        {
            const int pageSize = 9; // Số món ăn trên mỗi trang

            var query = _context.MonAns.AsQueryable();

            // Tìm kiếm không phân biệt chữ hoa/thường
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Trim().ToLower();
                query = query.Where(x => x.TenMonAn.ToLower().Contains(keyword));
            }

            // Đếm tổng số kết quả
            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            // Đảm bảo page trong phạm vi hợp lệ
            page = Math.Max(1, Math.Min(page, totalPages));

            // Lấy dữ liệu cho trang hiện tại
            var result = await query
                .OrderBy(x => x.TenMonAn)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Tạo view model
            var viewModel = new TimKiemMonAnViewModel
            {
                Keyword = keyword,
                MonAn = result,
                CurrentPage = page,
                TotalPages = totalPages,
                TotalItems = totalItems
            };

            return View(viewModel);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
