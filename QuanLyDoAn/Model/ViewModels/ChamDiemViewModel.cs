namespace QuanLyDoAn.Model.ViewModels
{
    public class ChamDiemViewModel
    {
        public string MaDeTai { get; set; } = string.Empty;
        public string TenDeTai { get; set; } = string.Empty;
        public string SinhVien { get; set; } = string.Empty;
        public string MaGv { get; set; } = string.Empty;
        public string MaLoaiDanhGia { get; set; } = string.Empty;
        public string TenLoaiDanhGia { get; set; } = string.Empty;
        public List<ChiTietChamDiem> ChiTietDiem { get; set; } = new List<ChiTietChamDiem>();
    }

    public class ChiTietChamDiem
    {
        public int MaTieuChi { get; set; }
        public string TenTieuChi { get; set; } = string.Empty;
        public string? MoTa { get; set; }
        public decimal TrongSo { get; set; }
        public decimal DiemToiDa { get; set; }
        public decimal Diem { get; set; }
        public string? NhanXet { get; set; }
    }

    public class KetQuaChamDiemBasicViewModel
    {
        public string MaDeTai { get; set; } = string.Empty;
        public string TenDeTai { get; set; } = string.Empty;
        public string SinhVien { get; set; } = string.Empty;
        public decimal? DiemTongKet { get; set; }
        public List<DanhGiaViewModel> DanhSachDanhGia { get; set; } = new List<DanhGiaViewModel>();
    }

    public class DanhGiaViewModel
    {
        public int MaDanhGia { get; set; }
        public string TenGiangVien { get; set; } = string.Empty;
        public string LoaiDanhGia { get; set; } = string.Empty;
        public decimal? DiemThanhPhan { get; set; }
        public DateOnly? NgayDanhGia { get; set; }
        public List<ChiTietDanhGiaViewModel> ChiTietDanhGias { get; set; } = new List<ChiTietDanhGiaViewModel>();
    }

    public class ChiTietDanhGiaViewModel
    {
        public string TenTieuChi { get; set; } = string.Empty;
        public decimal Diem { get; set; }
        public string? NhanXet { get; set; }
    }
}