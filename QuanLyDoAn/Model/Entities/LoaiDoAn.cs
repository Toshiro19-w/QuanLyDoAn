using System;
using System.Collections.Generic;

namespace QuanLyDoAn.Model.Entities;

public partial class LoaiDoAn
{
    public string MaLoaiDoAn { get; set; } = null!;

    public string? TenLoaiDoAn { get; set; }

    public virtual ICollection<DoAn> DoAns { get; set; } = new List<DoAn>();
}
