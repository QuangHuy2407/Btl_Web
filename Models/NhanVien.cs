using System;
using System.Collections.Generic;

namespace FASTFOOD.Models
{
    public partial class NhanVien
    {
		public NhanVien()
		{
			HoaDons = new HashSet<HoaDon>();
		}

		public int MaNhanVien { get; set; }
		public string TenNhanVien { get; set; } = null!;
		public string? SoDienThoai { get; set; }
		public string? DiaChi { get; set; }
		public string? ChucVu { get; set; }
		public string Username { get; set; } = null!;

		public virtual NguoiDung UsernameNavigation { get; set; } = null!;
		public virtual ICollection<HoaDon> HoaDons { get; set; }
	}
}
