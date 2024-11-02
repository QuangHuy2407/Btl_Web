using System;
using System.Collections.Generic;

namespace FASTFOOD.Models
{
    public partial class NguoiDung
    {
		public NguoiDung()
		{
			KhachHangs = new HashSet<KhachHang>();
			NhanViens = new HashSet<NhanVien>();
		}

		public string Username { get; set; } = null!;
		public string Password { get; set; } = null!;
		public string? LoaiUser { get; set; }
		public DateTime NgayDangKy { get; set; }
		public string? TrangThai { get; set; }

		public virtual ICollection<KhachHang> KhachHangs { get; set; }
		public virtual ICollection<NhanVien> NhanViens { get; set; }
	}
}
