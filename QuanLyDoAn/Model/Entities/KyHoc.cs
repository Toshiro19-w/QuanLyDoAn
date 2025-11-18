using System;
using System.Collections.Generic;

namespace QuanLyDoAn.Model.Entities;

public partial class KyHoc
{
    public string MaKy { get; set; } = null!;

    public string? TenKy { get; set; }

    public int? NamHoc { get; set; }

    public virtual ICollection<DoAn> DoAns { get; set; } = new List<DoAn>();
}
