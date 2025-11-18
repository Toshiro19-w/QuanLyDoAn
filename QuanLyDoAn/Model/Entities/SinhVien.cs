using System;
using System.Collections.Generic;

namespace QuanLyDoAn.Model.Entities;

public partial class SinhVien
{
    public string MaSv { get; set; } = null!;

    public string HoTen { get; set; } = null!;

    public string? Lop { get; set; }

    public string? Email { get; set; }

    public string? SoDienThoai { get; set; }

    public string? MaChuyenNganh { get; set; }

    public DateOnly? NgaySinh { get; set; }

    public virtual DoAn? DoAn { get; set; }

    public virtual ChuyenNganh? MaChuyenNganhNavigation { get; set; }

    public virtual ICollection<TaiKhoan> TaiKhoans { get; set; } = new List<TaiKhoan>();
}
