using System;
using System.Collections.Generic;

namespace QuanLyDoAn.Model.Entities;

public partial class ChuyenNganh
{
    public string MaChuyenNganh { get; set; } = null!;

    public string TenChuyenNganh { get; set; } = null!;

    public virtual ICollection<DoAn> DoAns { get; set; } = new List<DoAn>();

    public virtual ICollection<GiangVien> GiangViens { get; set; } = new List<GiangVien>();

    public virtual ICollection<SinhVien> SinhViens { get; set; } = new List<SinhVien>();
}
