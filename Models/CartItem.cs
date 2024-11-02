using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace FASTFOOD.Models
{
    public class CartItem
    {
        public int MaMonAn { get; set; }
        public string TenMonAn { get; set; }
        public string AnhDaiDien { get; set; }
        public double Gia { get; set; }
        public int SoLuong { get; set; }
        public double ThanhTien => SoLuong * Gia;
    }
}
