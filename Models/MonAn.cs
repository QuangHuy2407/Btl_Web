using System;
using System.Collections.Generic;
using FASTFOOD.Models;

namespace FASTFOOD.Models
{
	public partial class MonAn
	{
		public MonAn()
		{
			ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
		}

		public int MaMonAn { get; set; }
		public string TenMonAn { get; set; } = null!;
		public string? MoTa { get; set; }
		public decimal Gia { get; set; }
		public int MaLoaiMonAn { get; set; }
		public string? AnhDaiDien { get; set; }
		public virtual LoaiMonAn MaLoaiMonAnNavigation { get; set; } = null!;
		public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
	}
}
