using System;
using System.Collections.Generic;

namespace FASTFOOD.Models
{
	public partial class DatBan
		{
			public int MaDatBan { get; set; }
			public int MaKhachHang { get; set; }
			public DateTime NgayDat { get; set; }
			public TimeSpan GioDat { get; set; }
			public int SoLuongKhach { get; set; }
			public string? TrangThai { get; set; }
			public string? GhiChu { get; set; }

			public virtual KhachHang MaKhachHangNavigation { get; set; } = null!;
		}
}