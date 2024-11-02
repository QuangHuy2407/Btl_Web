using FASTFOOD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace FASTFOOD.Areas.Admin.Controllers
{
	[Area("admin")]
	[Route("admin")]
	[Route("admin/homeadmin")]
	public class HomeAdminController : Controller
	{	QLBanDoAnContext db = new QLBanDoAnContext();
		[Route("")]
		[Route("Index")]
		public IActionResult Index()
		{
			return View();
		}
		[Route("danhmucmonan")]
		public IActionResult DanhMucMonAn(int? page)
        {
            int pageSize = 6;
            int pagenumber = page == null || page < 0 ? 1 : page.Value;
            var listMonAn = db.MonAns.AsNoTracking().OrderBy(x => x.TenMonAn);
            PagedList<MonAn> list = new PagedList<MonAn>(listMonAn, pagenumber, pageSize);
            return View(list);
        }
		[Route("Themmonan")]
		[HttpGet]
        public IActionResult ThemMonAn()
        {
			ViewBag.MaLoaiMonAn = new SelectList(db.LoaiMonAns.ToList(), "MaLoaiMonAn", "TenLoaiMonAn");
            return View();
        }
		[Route("Themmonan")]
		[HttpPost]
		[ValidateAntiForgeryToken]
        public IActionResult ThemMonAn(MonAn MonAn)
        {	
			if (!ModelState.IsValid)
			{
				db.MonAns.Add(MonAn);
				db.SaveChanges();
				return RedirectToAction("DanhMucMonAn");
			}

            return View(MonAn);
        }
        [Route("Suamonan")]
        [HttpGet]
        public IActionResult Suamonan(int MaMonAn)
        {
            ViewBag.MaLoaiMonAn = new SelectList(db.LoaiMonAns.ToList(), "MaLoaiMonAn", "TenLoaiMonAn");
            var MonAn=db.MonAns.Find(MaMonAn);
            return View(MonAn);
        }
        [Route("Suamonan")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Suamonan(MonAn MonAn)
        {
            if (!ModelState.IsValid)
            {
                db.Entry(MonAn).State= EntityState.Modified; 
                db.SaveChanges();
                return RedirectToAction("DanhMucMonAn","HomeAdmin");
            }

            return View(MonAn);
        }
        [Route("Xoamonan")]
        [HttpGet]
        public IActionResult Xoamonan(int MaMonAn)
        {
            TempData["Message"] = "";
            var anhmonan=db.MonAns.Where(x=>x.MaMonAn==MaMonAn).ToList();
            if(anhmonan.Any()) db.RemoveRange(anhmonan);
            db.Remove(db.MonAns.Find(MaMonAn));
            db.SaveChanges();
            TempData["Message"] = "Món ăn đã bị xóa";
            return RedirectToAction("DanhMucMonAn","HomeAdmin");
        }

    }
}
