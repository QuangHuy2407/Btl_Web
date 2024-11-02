using System;
using System.Collections.Generic;

namespace FASTFOOD.Models
{
    public partial class ChiTietHoaDon
    {
        public int MaChiTietHoaDon { get; set; }
        public int MaHoaDon { get; set; }
        public int MaMonAn { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }

        public virtual HoaDon MaHoaDonNavigation { get; set; } = null!;
        public virtual MonAn MaMonAnNavigation { get; set; } = null!;
        public MonAn MonAn { get; set; }
    }
}
