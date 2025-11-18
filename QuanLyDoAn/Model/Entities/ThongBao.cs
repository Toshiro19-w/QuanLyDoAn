using System;
using System.Collections.Generic;

namespace QuanLyDoAn.Model.Entities;

public partial class ThongBao
{
    public int MaThongBao { get; set; }

    public string? MaDeTai { get; set; }

    public string? NoiDung { get; set; }

    public DateOnly? NgayGui { get; set; }

    public virtual DoAn? MaDeTaiNavigation { get; set; }
}
