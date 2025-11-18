using System;
using System.Collections.Generic;

namespace QuanLyDoAn.Model.Entities;

public partial class TrangThaiDoAn
{
    public string MaTrangThai { get; set; } = null!;

    public string? TenTrangThai { get; set; }

    public virtual ICollection<DoAn> DoAns { get; set; } = new List<DoAn>();
}
