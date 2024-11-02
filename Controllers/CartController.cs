using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using FASTFOOD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace FASTFOOD.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {

        private readonly QLBanDoAnContext _context;

        public CartController(QLBanDoAnContext context)
        {
            _context = context;
        }

        // Lấy giỏ hàng từ session
        public List<CartItem> Cart
        {
            get
            {
                byte[] data;
                var cart = new List<CartItem>();
                if (HttpContext.Session.TryGetValue("Cart", out data))
                {
                    string jsonData = System.Text.Encoding.UTF8.GetString(data);
                    cart = System.Text.Json.JsonSerializer.Deserialize<List<CartItem>>(jsonData);
                }
                return cart;
            }
        }

        // Lưu giỏ hàng vào session
        private void SaveCart(List<CartItem> cart)
        {
            string jsonData = System.Text.Json.JsonSerializer.Serialize(cart);
            HttpContext.Session.Set("Cart", System.Text.Encoding.UTF8.GetBytes(jsonData));
        }

        // Thêm món ăn vào giỏ hàng
        [HttpGet("AddToCart")]
        [HttpPost("AddToCart")]
        public IActionResult AddToCart(int MaMonAn, int soLuong = 1)
        {
            try
            {
                var cart = Cart;
                var item = cart.FirstOrDefault(x => x.MaMonAn == MaMonAn);

                if (soLuong == 0) // Xử lý xóa item
                {
                    if (item != null)
                    {
                        cart.Remove(item);
                    }
                }
                else
                {
                    if (item == null) // Thêm mới
                    {
                        var monAn = _context.MonAns.Find(MaMonAn);
                        if (monAn != null)
                        {
                            item = new CartItem
                            {
                                MaMonAn = MaMonAn,
                                TenMonAn = monAn.TenMonAn,
                                Gia = (double)monAn.Gia,
                                SoLuong = soLuong
                            };
                            cart.Add(item);
                        }
                    }
                    else // Cập nhật số lượng
                    {
                        item.SoLuong += soLuong;
                        if (item.SoLuong <= 0)
                        {
                            cart.Remove(item);
                        }
                    }
                }

                SaveCart(cart);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Carts", Cart);
        }
        public double GetCartTotal()
        {
            return Cart.Sum(item => item.Gia * item.SoLuong);
        }
    }
}
