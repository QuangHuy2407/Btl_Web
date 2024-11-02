using FASTFOOD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FASTFOOD.Models.ViewModels;


public class KhachHangController : Controller
{
    private readonly QLBanDoAnContext _db;  // Đổi tên biến db thành _db

    public KhachHangController(QLBanDoAnContext context)
    {
        _db = context;  // Khởi tạo _db
    }

    [Route("Profile")]
    public IActionResult Profile()
    {
        var username = HttpContext.Session.GetString("Username");
        if (username == null)
        {
            return RedirectToAction("Login", "Access");
        }

        // Debug: In ra username
        System.Diagnostics.Debug.WriteLine($"Checking profile for username: {username}");

        var khachHang = _db.KhachHangs.FirstOrDefault(k => k.Username == username);

        // Debug: Kiểm tra kết quả query
        System.Diagnostics.Debug.WriteLine($"Query result: {(khachHang == null ? "null" : "found")}");

        if (khachHang == null)
        {
            return RedirectToAction("Create");
        }
        return View(khachHang);
    }
    [HttpGet]
    public IActionResult Create()
    {
        var username = HttpContext.Session.GetString("Username");
        if (string.IsNullOrEmpty(username))
        {
            return RedirectToAction("Login", "Access");
        }
        return View(new KhachHangViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(KhachHangViewModel model)
    {
        var username = HttpContext.Session.GetString("Username");
        if (string.IsNullOrEmpty(username))
        {
            return RedirectToAction("Login", "Access");
        }

        if (ModelState.IsValid)
        {
            try
            {
                // Kiểm tra xem đã tồn tại khách hàng với username này chưa
                var existingCustomer = await _db.KhachHangs
                    .FirstOrDefaultAsync(k => k.Username == username);

                if (existingCustomer != null)
                {
                    ModelState.AddModelError("", "Thông tin khách hàng đã tồn tại");
                    return View(model);
                }

                // Mapping từ ViewModel sang Model
                var khachHang = new KhachHang
                {
                    TenKhachHang = model.TenKhachHang,
                    SoDienThoai = model.SoDienThoai,
                    DiaChi = model.DiaChi,
                    Email = model.Email,
                    NgaySinh = model.NgaySinh,
                    GioiTinh = model.GioiTinh,
                    Username = username
                };

                _db.KhachHangs.Add(khachHang);
                await _db.SaveChangesAsync();

                TempData["SuccessMessage"] = "Tạo thông tin khách hàng thành công!";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi khi lưu dữ liệu: " + ex.Message);
            }
        }

        return View(model);
    }

    // GET: Edit
    public IActionResult Edit(int? id)
    {
        var username = HttpContext.Session.GetString("Username");
        if (username == null)
        {
            return RedirectToAction("Login", "Access");
        }

        if (id == null)
        {
            return NotFound();
        }

        var khachHang = _db.KhachHangs.FirstOrDefault(k => k.MaKhachHang == id && k.Username == username);
        if (khachHang == null)
        {
            return NotFound();
        }

        return View(khachHang);
    }

    // POST: Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, KhachHang khachHang)
    {
        var username = HttpContext.Session.GetString("Username");
        if (username == null)
        {
            return RedirectToAction("Login", "Access");
        }

        if (id != khachHang.MaKhachHang)
        {
            return NotFound();
        }

        // Kiểm tra xem khách hàng có tồn tại không
        var existingCustomer = _db.KhachHangs.AsNoTracking()
            .FirstOrDefault(k => k.MaKhachHang == id && k.Username == username);

        if (existingCustomer == null)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                // Gán lại username để đảm bảo không bị thay đổi
                khachHang.Username = username;

                _db.Update(khachHang);
                _db.SaveChanges();

                TempData["Message"] = "Cập nhật thông tin thành công!";
                TempData["MessageType"] = "success";

                return RedirectToAction("Profile");
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Có lỗi xảy ra: " + ex.Message;
                TempData["MessageType"] = "error";
            }
        }

        return View(khachHang);
    }
}