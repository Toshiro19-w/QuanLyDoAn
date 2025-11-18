using System;
using System.Collections.Generic;

namespace QuanLyDoAn.Model.Entities;

public partial class GiangVien
{
    public string MaGv { get; set; } = null!;

    public string HoTen { get; set; } = null!;

    public string? BoMon { get; set; }

    public string? Email { get; set; }

    public string? MaChuyenNganh { get; set; }

    public string? ChucVu { get; set; }

    public virtual ICollection<DanhGia> DanhGia { get; set; } = new List<DanhGia>();

    public virtual ICollection<DoAn> DoAns { get; set; } = new List<DoAn>();

    public virtual ChuyenNganh? MaChuyenNganhNavigation { get; set; }

    public virtual ICollection<TaiKhoan> TaiKhoans { get; set; } = new List<TaiKhoan>();
}
