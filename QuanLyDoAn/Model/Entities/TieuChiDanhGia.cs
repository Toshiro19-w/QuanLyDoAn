using System;
using System.Collections.Generic;

namespace QuanLyDoAn.Model.Entities;

public partial class TieuChiDanhGia
{
    public int MaTieuChi { get; set; }
    
    public string TenTieuChi { get; set; } = null!;
    
    public string? MoTa { get; set; }
    
    public decimal TrongSo { get; set; } // Trọng số %
    
    public decimal DiemToiDa { get; set; } = 10;
    
    public string? MaLoaiDoAn { get; set; }
    
    public virtual LoaiDoAn? MaLoaiDoAnNavigation { get; set; }
    
    public virtual ICollection<ChiTietDanhGia> ChiTietDanhGias { get; set; } = new List<ChiTietDanhGia>();
}