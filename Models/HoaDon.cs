using System;
using System.Collections.Generic;

namespace FASTFOOD.Models
{
	public partial class HoaDon
	{
		public HoaDon()
		{
			ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
		}

		public int MaHoaDon { get; set; }
		public DateTime NgayHoaDon { get; set; }
		public int MaKhachHang { get; set; }
		public int MaNhanVien { get; set; }
		public decimal TongTien { get; set; }
		public string? GhiChu { get; set; }

		public virtual KhachHang MaKhachHangNavigation { get; set; } = null!;
		public virtual NhanVien MaNhanVienNavigation { get; set; } = null!;
		public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
	}
}
