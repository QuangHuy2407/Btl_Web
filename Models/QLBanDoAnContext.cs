using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace FASTFOOD.Models
{
	public partial class QLBanDoAnContext : DbContext
	{
		public QLBanDoAnContext()
		{
		}

		public QLBanDoAnContext(DbContextOptions<QLBanDoAnContext> options)
			: base(options)
		{
		}

		public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; } = null!;
		public virtual DbSet<DatBan> DatBans { get; set; } = null!;
		public virtual DbSet<HoaDon> HoaDons { get; set; } = null!;
		public virtual DbSet<KhachHang> KhachHangs { get; set; } = null!;
		public virtual DbSet<LoaiMonAn> LoaiMonAns { get; set; } = null!;
		public virtual DbSet<MonAn> MonAns { get; set; } = null!;
		public virtual DbSet<NguoiDung> NguoiDungs { get; set; } = null!;
		public virtual DbSet<NhanVien> NhanViens { get; set; } = null!;

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
				optionsBuilder.UseSqlServer("Data Source=PHONG\\PHONG;Initial Catalog=QLBanDoAn;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ChiTietHoaDon>(entity =>
			{
				entity.HasKey(e => e.MaChiTietHoaDon)
					.HasName("PK__ChiTietH__CFF2C426C839AE71");

				entity.ToTable("ChiTietHoaDon");

				entity.Property(e => e.DonGia).HasColumnType("decimal(10, 2)");

				entity.HasOne(d => d.MaHoaDonNavigation)
					.WithMany(p => p.ChiTietHoaDons)
					.HasForeignKey(d => d.MaHoaDon)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__ChiTietHo__MaHoa__5AEE82B9");

				entity.HasOne(d => d.MaMonAnNavigation)
					.WithMany(p => p.ChiTietHoaDons)
					.HasForeignKey(d => d.MaMonAn)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__ChiTietHo__MaMon__5BE2A6F2");
			});

			modelBuilder.Entity<DatBan>(entity =>
			{
				entity.HasKey(e => e.MaDatBan)
					.HasName("PK__DatBan__703DFB75CFF2C994");

				entity.ToTable("DatBan");

				entity.Property(e => e.GhiChu).HasMaxLength(255);

				entity.Property(e => e.NgayDat).HasColumnType("date");

				entity.Property(e => e.TrangThai)
					.HasMaxLength(20)
					.HasDefaultValueSql("('pending')");

				entity.HasOne(d => d.MaKhachHangNavigation)
					.WithMany(p => p.DatBans)
					.HasForeignKey(d => d.MaKhachHang)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__DatBan__MaKhachH__60A75C0F");
			});

			modelBuilder.Entity<HoaDon>(entity =>
			{
				entity.HasKey(e => e.MaHoaDon)
					.HasName("PK__HoaDon__835ED13B268BB953");

				entity.ToTable("HoaDon");

				entity.Property(e => e.GhiChu).HasMaxLength(255);

				entity.Property(e => e.NgayHoaDon).HasColumnType("date");

				entity.Property(e => e.TongTien).HasColumnType("decimal(10, 2)");

				entity.HasOne(d => d.MaKhachHangNavigation)
					.WithMany(p => p.HoaDons)
					.HasForeignKey(d => d.MaKhachHang)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__HoaDon__MaKhachH__571DF1D5");

				entity.HasOne(d => d.MaNhanVienNavigation)
					.WithMany(p => p.HoaDons)
					.HasForeignKey(d => d.MaNhanVien)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__HoaDon__MaNhanVi__5812160E");
			});

			modelBuilder.Entity<KhachHang>(entity =>
			{
				entity.HasKey(e => e.MaKhachHang)
					.HasName("PK__KhachHan__88D2F0E530F187F5");

				entity.ToTable("KhachHang");

				entity.Property(e => e.DiaChi).HasMaxLength(255);

				entity.Property(e => e.Email).HasMaxLength(100);

				entity.Property(e => e.GioiTinh).HasMaxLength(10);

				entity.Property(e => e.NgaySinh).HasColumnType("date");

				entity.Property(e => e.SoDienThoai).HasMaxLength(15);

				entity.Property(e => e.TenKhachHang).HasMaxLength(100);

				entity.Property(e => e.Username)
					.HasMaxLength(50)
					.HasColumnName("username");

				entity.HasOne(d => d.UsernameNavigation)
					.WithMany(p => p.KhachHangs)
					.HasForeignKey(d => d.Username)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_NguoiDung_KhachHang");
			});

			modelBuilder.Entity<LoaiMonAn>(entity =>
			{
				entity.HasKey(e => e.MaLoaiMonAn)
					.HasName("PK__LoaiMonA__AF2559D366CC2DE6");

				entity.ToTable("LoaiMonAn");

				entity.Property(e => e.TenLoaiMonAn).HasMaxLength(100);
			});

			modelBuilder.Entity<MonAn>(entity =>
			{
				entity.HasKey(e => e.MaMonAn)
					.HasName("PK__MonAn__B117162525D36E71");

				entity.ToTable("MonAn");

				entity.Property(e => e.AnhDaiDien).HasMaxLength(255);

				entity.Property(e => e.Gia).HasColumnType("decimal(10, 2)");

				entity.Property(e => e.MoTa).HasMaxLength(255);

				entity.Property(e => e.TenMonAn).HasMaxLength(100);

				entity.HasOne(d => d.MaLoaiMonAnNavigation)
					.WithMany(p => p.MonAns)
					.HasForeignKey(d => d.MaLoaiMonAn)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__MonAn__MaLoaiMon__4BAC3F29");
			});

			modelBuilder.Entity<NguoiDung>(entity =>
			{
				entity.HasKey(e => e.Username)
					.HasName("PK__NguoiDun__F3DBC573508D8969");

				entity.ToTable("NguoiDung");

				entity.Property(e => e.Username)
					.HasMaxLength(50)
					.HasColumnName("username");

				entity.Property(e => e.LoaiUser).HasMaxLength(20);

				entity.Property(e => e.NgayDangKy).HasColumnType("date");

				entity.Property(e => e.Password)
					.HasMaxLength(255)
					.HasColumnName("password");

				entity.Property(e => e.TrangThai)
					.HasMaxLength(20)
					.HasDefaultValueSql("('active')");
			});

			modelBuilder.Entity<NhanVien>(entity =>
			{
				entity.HasKey(e => e.MaNhanVien)
					.HasName("PK__NhanVien__77B2CA4756A7678D");

				entity.ToTable("NhanVien");

				entity.Property(e => e.ChucVu).HasMaxLength(50);

				entity.Property(e => e.DiaChi).HasMaxLength(255);

				entity.Property(e => e.SoDienThoai).HasMaxLength(15);

				entity.Property(e => e.TenNhanVien).HasMaxLength(100);

				entity.Property(e => e.Username)
					.HasMaxLength(50)
					.HasColumnName("username");

				entity.HasOne(d => d.UsernameNavigation)
					.WithMany(p => p.NhanViens)
					.HasForeignKey(d => d.Username)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_NguoiDung_NhanVien");
			});

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
