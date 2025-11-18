using System;

namespace QuanLyDoAn.Model.Entities
{
    public partial class YeuCauDangKy
    {
        public int MaYeuCau { get; set; }
        public string MaDeTai { get; set; } = null!;
        public string MaSv { get; set; } = null!;
        public DateOnly NgayGui { get; set; }
        public string TrangThai { get; set; } = null!; // "Pending", "Approved", "Rejected"
        public string? GhiChu { get; set; }

        public virtual DoAn MaDeTaiNavigation { get; set; } = null!;
        public virtual SinhVien MaSvNavigation { get; set; } = null!;
    }
}
