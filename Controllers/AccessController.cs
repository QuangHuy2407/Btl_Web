using Microsoft.AspNetCore.Mvc;
using FASTFOOD.Models;

namespace FASTFOOD.Controllers
{
    public class AccessController : Controller
    {   QLBanDoAnContext db = new QLBanDoAnContext();
        [HttpGet]
        public IActionResult Login()
        {   if(HttpContext.Session.GetString("Username")==null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }
        [HttpPost]
        public IActionResult Login(NguoiDung user)
        {
            if (HttpContext.Session.GetString("Username") == null)
            { 
                var u = db.NguoiDungs.Where(x=>x.Username.Equals(user.Username) && x.Password.Equals(user.Password)).FirstOrDefault();
                if (u != null) { 
                    HttpContext.Session.SetString("Username", u.Username.ToString());
                    return RedirectToAction("Index", "Home");
                }
				ModelState.AddModelError(string.Empty, "Sai tài khoản hoặc mật khẩu");
			}
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("username");
            return RedirectToAction("Login","Access");
        }
        // GET: Register
        public IActionResult Register()
        {
            // Kiểm tra xem đã có session hay chưa
            if (HttpContext.Session.GetString("Username") != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(); // Trả về view đăng ký
        }

        // POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(NguoiDung nguoiDung)
        {
            // Kiểm tra xem đã có session hay chưa
            if (HttpContext.Session.GetString("Username") == null)
            {
                if (ModelState.IsValid)
                {
                    // Kiểm tra xem tên người dùng đã tồn tại chưa
                    if (db.NguoiDungs.Any(u => u.Username == nguoiDung.Username))
                    {
                        ModelState.AddModelError("Username", "Tên người dùng đã tồn tại. Vui lòng chọn tên khác.");
                        return View(nguoiDung); // Trả về model để hiển thị lại thông tin đã nhập
                    }

                    try
                    {
                        nguoiDung.TrangThai = "active"; // Kích hoạt mặc định
                        nguoiDung.NgayDangKy = DateTime.Now; // Ngày đăng ký hiện tại

                        // Lưu thông tin vào database
                        db.NguoiDungs.Add(nguoiDung);
                        db.SaveChanges();

                        // Chuyển hướng đến trang đăng nhập
                        return RedirectToAction("Login", "Access");
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(string.Empty, "Đã xảy ra lỗi khi đăng ký. Vui lòng thử lại.");
                    }
                }
            }

            return View(nguoiDung); // Nếu không thành công, trả về view đăng ký
        }
    }

}

