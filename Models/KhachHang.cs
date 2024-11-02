using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FASTFOOD.Models
{
    public partial class KhachHang
    {
		public KhachHang()
		{
			DatBans = new HashSet<DatBan>();
			HoaDons = new HashSet<HoaDon>();
		}

		public int MaKhachHang { get; set; }
		public string TenKhachHang { get; set; } = null!;
		public string SoDienThoai { get; set; } = null!;
		public string? DiaChi { get; set; }
		public string? Email { get; set; }
		public DateTime? NgaySinh { get; set; }
		public string? GioiTinh { get; set; }
		public string Username { get; set; } = null!;

		public virtual NguoiDung UsernameNavigation { get; set; } = null!;
		public virtual ICollection<DatBan> DatBans { get; set; }
		public virtual ICollection<HoaDon> HoaDons { get; set; }
	}
}
