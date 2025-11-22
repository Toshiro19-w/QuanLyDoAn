using System;
using System.Collections.Generic;

namespace QuanLyDoAn.Model.Entities;

public partial class LoaiDanhGia
{
    public string MaLoaiDanhGia { get; set; } = null!;
    
    public string TenLoaiDanhGia { get; set; } = null!; // "Hướng dẫn", "Phản biện", "Hội đồng"
    
    public decimal TrongSoDiem { get; set; } // Trọng số trong tổng điểm cuối
    
    public virtual ICollection<DanhGia> DanhGias { get; set; } = new List<DanhGia>();
}