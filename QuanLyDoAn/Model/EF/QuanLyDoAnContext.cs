using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using QuanLyDoAn.Model.Entities;

namespace QuanLyDoAn.Model.EF;

public partial class QuanLyDoAnContext : DbContext
{
    public QuanLyDoAnContext()
    {
    }

    public QuanLyDoAnContext(DbContextOptions<QuanLyDoAnContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChuyenNganh> ChuyenNganhs { get; set; }

    public virtual DbSet<DanhGia> DanhGia { get; set; }

    public virtual DbSet<DoAn> DoAns { get; set; }

    public virtual DbSet<GiangVien> GiangViens { get; set; }

    public virtual DbSet<KyHoc> KyHocs { get; set; }

    public virtual DbSet<LoaiDoAn> LoaiDoAns { get; set; }

    public virtual DbSet<SinhVien> SinhViens { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    public virtual DbSet<TaiLieu> TaiLieus { get; set; }

    public virtual DbSet<ThongBao> ThongBaos { get; set; }

    public virtual DbSet<TienDo> TienDos { get; set; }

    public virtual DbSet<TrangThaiDoAn> TrangThaiDoAns { get; set; }

    public virtual DbSet<YeuCauDangKy> YeuCauDangKies { get; set; }

    public virtual DbSet<TieuChiDanhGia> TieuChiDanhGias { get; set; }

    public virtual DbSet<ChiTietDanhGia> ChiTietDanhGias { get; set; }

    public virtual DbSet<LoaiDanhGia> LoaiDanhGias { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = GetConnectionString();
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    private static string GetConnectionString()
    {
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var devSettingsPath = Path.Combine(basePath, "appsettings.development.json");
        var settingsPath = Path.Combine(basePath, "appsettings.json");
        
        string filePath = File.Exists(devSettingsPath) ? devSettingsPath : settingsPath;
        
        var json = File.ReadAllText(filePath);
        using var doc = JsonDocument.Parse(json);
        return doc.RootElement.GetProperty("ConnectionStrings").GetProperty("DefaultConnection").GetString() ?? "";
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChuyenNganh>(entity =>
        {
            entity.HasKey(e => e.MaChuyenNganh).HasName("PK__ChuyenNg__20FEA98D05E3B5EC");

            entity.ToTable("ChuyenNganh");

            entity.Property(e => e.MaChuyenNganh)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TenChuyenNganh).HasMaxLength(50);
        });

        modelBuilder.Entity<DanhGia>(entity =>
        {
            entity.HasKey(e => e.MaDanhGia).HasName("PK__DanhGia__AA9515BFFFD8B4F6");

            entity.Property(e => e.DiemThanhPhan).HasColumnType("decimal(4, 2)");
            entity.Property(e => e.MaDeTai)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MaGv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaGV");
            entity.Property(e => e.MaLoaiDanhGia)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.MaDeTaiNavigation).WithMany(p => p.DanhGia)
                .HasForeignKey(d => d.MaDeTai)
                .HasConstraintName("FK__DanhGia__MaDeTai__1BC821DD");

            entity.HasOne(d => d.MaGvNavigation).WithMany(p => p.DanhGia)
                .HasForeignKey(d => d.MaGv)
                .HasConstraintName("FK__DanhGia__MaGV__1CBC4616");

            entity.HasOne(d => d.MaLoaiDanhGiaNavigation).WithMany(p => p.DanhGias)
                .HasForeignKey(d => d.MaLoaiDanhGia)
                .HasConstraintName("FK__DanhGia__MaLoaiDanhGia");
        });

        modelBuilder.Entity<DoAn>(entity =>
        {
            entity.HasKey(e => e.MaDeTai).HasName("PK__DoAn__9F967D5B6B0CA34C");

            entity.ToTable("DoAn");

            entity.HasIndex(e => e.MaSv, "UQ__DoAn__2725081B9E65619A").IsUnique();

            entity.Property(e => e.MaDeTai)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Diem).HasColumnType("decimal(4, 2)");
            entity.Property(e => e.MaChuyenNganh)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MaGv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaGV");
            entity.Property(e => e.MaKy)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MaLoaiDoAn)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MaSv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaSV");
            entity.Property(e => e.MaTrangThai)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TenDeTai).HasMaxLength(200);

            entity.HasOne(d => d.MaChuyenNganhNavigation).WithMany(p => p.DoAns)
                .HasForeignKey(d => d.MaChuyenNganh)
                .HasConstraintName("FK__DoAn__MaChuyenNg__0E6E26BF");

            entity.HasOne(d => d.MaGvNavigation).WithMany(p => p.DoAns)
                .HasForeignKey(d => d.MaGv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DoAn__MaGV__0C85DE4D");

            entity.HasOne(d => d.MaKyNavigation).WithMany(p => p.DoAns)
                .HasForeignKey(d => d.MaKy)
                .HasConstraintName("FK__DoAn__MaKy__0D7A0286");

            entity.HasOne(d => d.MaLoaiDoAnNavigation).WithMany(p => p.DoAns)
                .HasForeignKey(d => d.MaLoaiDoAn)
                .HasConstraintName("FK__DoAn__MaLoaiDoAn__0F624AF8");

            entity.HasOne(d => d.MaSvNavigation).WithOne(p => p.DoAn)
                .HasForeignKey<DoAn>(d => d.MaSv)
                .HasConstraintName("FK__DoAn__MaSV__0B91BA14");

            entity.HasOne(d => d.MaTrangThaiNavigation).WithMany(p => p.DoAns)
                .HasForeignKey(d => d.MaTrangThai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DoAn__MaTrangTha__10566F31");
        });

        modelBuilder.Entity<GiangVien>(entity =>
        {
            entity.HasKey(e => e.MaGv).HasName("PK__GiangVie__2725AEF39C60637D");

            entity.ToTable("GiangVien");

            entity.Property(e => e.MaGv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaGV");
            entity.Property(e => e.BoMon).HasMaxLength(50);
            entity.Property(e => e.ChucVu).HasMaxLength(50);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HoTen).HasMaxLength(50);
            entity.Property(e => e.MaChuyenNganh)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.MaChuyenNganhNavigation).WithMany(p => p.GiangViens)
                .HasForeignKey(d => d.MaChuyenNganh)
                .HasConstraintName("FK__GiangVien__MaChu__72C60C4A");
        });

        modelBuilder.Entity<KyHoc>(entity =>
        {
            entity.HasKey(e => e.MaKy).HasName("PK__KyHoc__2725CF496EB77093");

            entity.ToTable("KyHoc");

            entity.Property(e => e.MaKy)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TenKy).HasMaxLength(50);
        });

        modelBuilder.Entity<LoaiDoAn>(entity =>
        {
            entity.HasKey(e => e.MaLoaiDoAn).HasName("PK__LoaiDoAn__73391AD10FAE5D31");

            entity.ToTable("LoaiDoAn");

            entity.Property(e => e.MaLoaiDoAn)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TenLoaiDoAn).HasMaxLength(50);
        });

        modelBuilder.Entity<SinhVien>(entity =>
        {
            entity.HasKey(e => e.MaSv).HasName("PK__SinhVien__2725081A3CDA4C2E");

            entity.ToTable("SinhVien");

            entity.Property(e => e.MaSv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaSV");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HoTen).HasMaxLength(50);
            entity.Property(e => e.Lop)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MaChuyenNganh)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.MaChuyenNganhNavigation).WithMany(p => p.SinhViens)
                .HasForeignKey(d => d.MaChuyenNganh)
                .HasConstraintName("FK__SinhVien__MaChuy__6FE99F9F");
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.TenDangNhap).HasName("PK__TaiKhoan__55F68FC1E1CADB1F");

            entity.ToTable("TaiKhoan");

            entity.Property(e => e.TenDangNhap)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.MaGv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaGV");
            entity.Property(e => e.MaSv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaSV");
            entity.Property(e => e.MatKhau).HasMaxLength(100);
            entity.Property(e => e.VaiTro).HasMaxLength(20);

            entity.HasOne(d => d.MaGvNavigation).WithMany(p => p.TaiKhoans)
                .HasForeignKey(d => d.MaGv)
                .HasConstraintName("FK__TaiKhoan__MaGV__245D67DE");

            entity.HasOne(d => d.MaSvNavigation).WithMany(p => p.TaiKhoans)
                .HasForeignKey(d => d.MaSv)
                .HasConstraintName("FK__TaiKhoan__MaSV__236943A5");
        });

        modelBuilder.Entity<TaiLieu>(entity =>
        {
            entity.HasKey(e => e.MaTaiLieu).HasName("PK__TaiLieu__FD18A657959A3E0E");

            entity.ToTable("TaiLieu");

            entity.Property(e => e.DuongDan)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.MaDeTai)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TenTaiLieu).HasMaxLength(100);

            entity.HasOne(d => d.MaDeTaiNavigation).WithMany(p => p.TaiLieus)
                .HasForeignKey(d => d.MaDeTai)
                .HasConstraintName("FK__TaiLieu__MaDeTai__17F790F9");
        });

        modelBuilder.Entity<ThongBao>(entity =>
        {
            entity.HasKey(e => e.MaThongBao).HasName("PK__ThongBao__04DEB54E4FCDBAC0");

            entity.ToTable("ThongBao");

            entity.Property(e => e.MaDeTai)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.MaDeTaiNavigation).WithMany(p => p.ThongBaos)
                .HasForeignKey(d => d.MaDeTai)
                .HasConstraintName("FK__ThongBao__MaDeTa__1F98B2C1");
        });

        modelBuilder.Entity<TienDo>(entity =>
        {
            entity.HasKey(e => e.MaTienDo).HasName("PK__TienDo__C5D04CAE0DA8CB2E");

            entity.ToTable("TienDo");

            entity.Property(e => e.GiaiDoan).HasMaxLength(50);
            entity.Property(e => e.MaDeTai)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TrangThaiNop).HasMaxLength(20);

            entity.HasOne(d => d.MaDeTaiNavigation).WithMany(p => p.TienDos)
                .HasForeignKey(d => d.MaDeTai)
                .HasConstraintName("FK__TienDo__MaDeTai__151B244E");
        });

        modelBuilder.Entity<TrangThaiDoAn>(entity =>
        {
            entity.HasKey(e => e.MaTrangThai).HasName("PK__TrangTha__AADE413836629BBC");

            entity.ToTable("TrangThaiDoAn");

            entity.Property(e => e.MaTrangThai)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TenTrangThai).HasMaxLength(50);
        });

        modelBuilder.Entity<YeuCauDangKy>(entity =>
        {
            entity.HasKey(e => e.MaYeuCau);

            entity.ToTable("YeuCauDangKy");

            entity.Property(e => e.MaDeTai)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MaSv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaSV");
            entity.Property(e => e.TrangThai).HasMaxLength(20);
            entity.Property(e => e.GhiChu).HasMaxLength(500);

            entity.HasOne(d => d.MaDeTaiNavigation).WithMany()
                .HasForeignKey(d => d.MaDeTai)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.MaSvNavigation).WithMany()
                .HasForeignKey(d => d.MaSv)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TieuChiDanhGia>(entity =>
        {
            entity.HasKey(e => e.MaTieuChi);
            entity.ToTable("TieuChiDanhGia");
            
            entity.Property(e => e.TenTieuChi).HasMaxLength(100).IsRequired();
            entity.Property(e => e.MoTa).HasMaxLength(500);
            entity.Property(e => e.TrongSo).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.DiemToiDa).HasColumnType("decimal(4, 2)");
            entity.Property(e => e.MaLoaiDoAn)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.MaLoaiDoAnNavigation).WithMany(p => p.TieuChiDanhGias)
                .HasForeignKey(d => d.MaLoaiDoAn);
        });

        modelBuilder.Entity<ChiTietDanhGia>(entity =>
        {
            entity.HasKey(e => e.MaChiTiet);
            entity.ToTable("ChiTietDanhGia");
            
            entity.Property(e => e.Diem).HasColumnType("decimal(4, 2)");
            entity.Property(e => e.NhanXet).HasMaxLength(500);

            entity.HasOne(d => d.MaDanhGiaNavigation).WithMany(p => p.ChiTietDanhGias)
                .HasForeignKey(d => d.MaDanhGia)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.MaTieuChiNavigation).WithMany(p => p.ChiTietDanhGias)
                .HasForeignKey(d => d.MaTieuChi)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<LoaiDanhGia>(entity =>
        {
            entity.HasKey(e => e.MaLoaiDanhGia);
            entity.ToTable("LoaiDanhGia");
            
            entity.Property(e => e.MaLoaiDanhGia)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TenLoaiDanhGia).HasMaxLength(50).IsRequired();
            entity.Property(e => e.TrongSoDiem).HasColumnType("decimal(5, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
