using System;
using System.Collections.Generic;

namespace QuanLyDoAn.Model.Entities;

public partial class DanhGia
{
    public int MaDanhGia { get; set; }

    public string? MaDeTai { get; set; }

    public string? MaGv { get; set; }

    public decimal? DiemThanhPhan { get; set; }

    public string? NhanXet { get; set; }

    public DateOnly? NgayDanhGia { get; set; }

    public virtual DoAn? MaDeTaiNavigation { get; set; }

    public virtual GiangVien? MaGvNavigation { get; set; }
}
