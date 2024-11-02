using System;
using System.Collections.Generic;

namespace FASTFOOD.Models
{
    public partial class LoaiMonAn
    {
		public LoaiMonAn()
		{
			MonAns = new HashSet<MonAn>();
		}

		public int MaLoaiMonAn { get; set; }
		public string TenLoaiMonAn { get; set; } = null!;

		public virtual ICollection<MonAn> MonAns { get; set; }
	}
}
