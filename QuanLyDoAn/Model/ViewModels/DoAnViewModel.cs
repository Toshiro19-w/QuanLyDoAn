using QuanLyDoAn.Model.Entities;

namespace QuanLyDoAn.Model.ViewModels
{
    public class DoAnViewModel
    {
        public string MaDeTai { get; set; } = null!;
        public string TenDeTai { get; set; } = null!;
        public string SinhVien { get; set; } = null!;
        public string TenGiangVien { get; set; } = null!;
        public DateOnly? NgayBatDau { get; set; }
        public DateOnly? NgayKetThuc { get; set; }
        public string TrangThai { get; set; } = null!;
        public string DiemText { get; set; } = null!;
        public string LoaiDoAn { get; set; } = null!;
        public string? MaLoaiDoAn { get; set; }
        public bool CoChamDiem => !string.IsNullOrEmpty(DiemText) && DiemText != "Chưa có điểm";
        public List<DanhGiaInfo> DanhSachDanhGia { get; set; } = new List<DanhGiaInfo>();
    }

    public class DanhGiaInfo
    {
        public string LoaiDanhGia { get; set; } = null!;
        public string GiangVien { get; set; } = null!;
        public decimal? Diem { get; set; }
        public DateOnly? NgayDanhGia { get; set; }
        public string TrangThaiDanhGia => Diem.HasValue ? "Đã chấm" : "Chưa chấm";
    }

    public class SinhVienInfo
    {
        public string MaSv { get; set; } = null!;
        public string HoTen { get; set; } = null!;
        public string? TenNhom { get; set; }
    }

    public class ChamDiemDoAnViewModel
    {
        public string MaDeTai { get; set; } = null!;
        public string TenDeTai { get; set; } = null!;
        public string SinhVien { get; set; } = null!;
        public string MaLoaiDoAn { get; set; } = null!;
        public List<TieuChiDanhGia> TieuChis { get; set; } = new List<TieuChiDanhGia>();
        public List<LoaiDanhGia> LoaiDanhGias { get; set; } = new List<LoaiDanhGia>();
    }
}