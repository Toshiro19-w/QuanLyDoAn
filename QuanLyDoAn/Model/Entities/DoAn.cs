using System;
using System.Collections.Generic;

namespace QuanLyDoAn.Model.Entities;

public partial class DoAn
{
    public string MaDeTai { get; set; } = null!;

    public string TenDeTai { get; set; } = null!;

    public string? MoTa { get; set; }

    public string? MaSv { get; set; }

    public string MaGv { get; set; } = null!;

    public string? MaKy { get; set; }

    public string? MaChuyenNganh { get; set; }

    public DateOnly? NgayBatDau { get; set; }

    public DateOnly? NgayKetThuc { get; set; }

    public decimal? Diem { get; set; }

    public string? MaLoaiDoAn { get; set; }

    public string MaTrangThai { get; set; } = null!;

    public virtual ICollection<DanhGia> DanhGia { get; set; } = new List<DanhGia>();

    public virtual ChuyenNganh? MaChuyenNganhNavigation { get; set; }

    public virtual GiangVien MaGvNavigation { get; set; } = null!;

    public virtual KyHoc? MaKyNavigation { get; set; }

    public virtual LoaiDoAn? MaLoaiDoAnNavigation { get; set; }

    public virtual SinhVien? MaSvNavigation { get; set; }

    public virtual TrangThaiDoAn MaTrangThaiNavigation { get; set; } = null!;

    public virtual ICollection<TaiLieu> TaiLieus { get; set; } = new List<TaiLieu>();

    public virtual ICollection<ThongBao> ThongBaos { get; set; } = new List<ThongBao>();

    public virtual ICollection<TienDo> TienDos { get; set; } = new List<TienDo>();
}
