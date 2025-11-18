using System;
using System.Collections.Generic;

namespace QuanLyDoAn.Model.Entities;

public partial class TaiLieu
{
    public int MaTaiLieu { get; set; }

    public string? MaDeTai { get; set; }

    public string? TenTaiLieu { get; set; }

    public string? DuongDan { get; set; }

    public DateOnly? NgayUpload { get; set; }

    public virtual DoAn? MaDeTaiNavigation { get; set; }
}
