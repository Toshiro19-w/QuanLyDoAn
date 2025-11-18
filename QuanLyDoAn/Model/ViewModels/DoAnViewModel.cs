using QuanLyDoAn.Model.Entities;

namespace QuanLyDoAn.Model.ViewModels
{
    public class DoAnViewModel
    {
        public string MaDeTai { get; set; } = null!;
        public string TenDeTai { get; set; } = null!;
        public string DanhSachSinhVien { get; set; } = null!;
        public string TenGiangVien { get; set; } = null!;
        public DateOnly? NgayBatDau { get; set; }
        public DateOnly? NgayKetThuc { get; set; }
        public string TrangThai { get; set; } = null!;
        public string DiemText { get; set; } = null!;
    }

    public class SinhVienInfo
    {
        public string MaSv { get; set; } = null!;
        public string HoTen { get; set; } = null!;
        public string? TenNhom { get; set; }
    }
}