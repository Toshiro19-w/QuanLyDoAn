using System;
using System.Collections.Generic;

namespace QuanLyDoAn.Model.Entities;

public partial class ChiTietDanhGia
{
    public int MaChiTiet { get; set; }
    
    public int MaDanhGia { get; set; }
    
    public int MaTieuChi { get; set; }
    
    public decimal Diem { get; set; }
    
    public string? NhanXet { get; set; }
    
    public virtual DanhGia MaDanhGiaNavigation { get; set; } = null!;
    
    public virtual TieuChiDanhGia MaTieuChiNavigation { get; set; } = null!;
}