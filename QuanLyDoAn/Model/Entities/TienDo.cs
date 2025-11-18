using System;
using System.Collections.Generic;

namespace QuanLyDoAn.Model.Entities;

public partial class TienDo
{
    public int MaTienDo { get; set; }

    public string? MaDeTai { get; set; }

    public string? GiaiDoan { get; set; }

    public DateOnly? NgayNop { get; set; }

    public string? NhanXet { get; set; }

    public string? TrangThaiNop { get; set; }

    public virtual DoAn? MaDeTaiNavigation { get; set; }
}
