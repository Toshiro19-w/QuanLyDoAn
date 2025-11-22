namespace QuanLyDoAn.Model.ViewModels
{
    // ViewModel cho Admin
    public class AdminChamDiemViewModel
    {
        public string MaDeTai { get; set; } = string.Empty;
        public string TenDeTai { get; set; } = string.Empty;
        public string SinhVien { get; set; } = string.Empty;
        public string TrangThai { get; set; } = string.Empty;
        public decimal? DiemHienTai { get; set; }
        public bool CoChamDiemChiTiet { get; set; }
        public List<DanhGiaInfo> DanhSachDanhGia { get; set; } = new List<DanhGiaInfo>();
    }

    // ViewModel cho Giảng viên
    public class GiangVienChamDiemViewModel
    {
        public string MaDeTai { get; set; } = string.Empty;
        public string TenDeTai { get; set; } = string.Empty;
        public string SinhVien { get; set; } = string.Empty;
        public string TrangThaiChamDiem { get; set; } = string.Empty;
        public List<LoaiDanhGiaChoPhep> LoaiChoPhep { get; set; } = new List<LoaiDanhGiaChoPhep>();
        public List<TienDoInfo> TienDos { get; set; } = new List<TienDoInfo>();
        public List<DanhGiaInfo> DanhGiaDaLam { get; set; } = new List<DanhGiaInfo>();
    }

    public class LoaiDanhGiaChoPhep
    {
        public string MaLoai { get; set; } = string.Empty;
        public string TenLoai { get; set; } = string.Empty;
        public bool ChoPhep { get; set; }
        public string LyDoKhongChoPhep { get; set; } = string.Empty;
    }

    public class TienDoInfo
    {
        public string GiaiDoan { get; set; } = string.Empty;
        public DateOnly? NgayNop { get; set; }
        public string TrangThaiNop { get; set; } = string.Empty;
        public string? NhanXet { get; set; }
    }

    // Form chấm điểm chi tiết
    public class FormChamDiemViewModel
    {
        public string MaDeTai { get; set; } = string.Empty;
        public string TenDeTai { get; set; } = string.Empty;
        public string SinhVien { get; set; } = string.Empty;
        public string MaGv { get; set; } = string.Empty;
        public string MaLoaiDanhGia { get; set; } = string.Empty;
        public string TenLoaiDanhGia { get; set; } = string.Empty;
        public List<TieuChiChamDiem> TieuChis { get; set; } = new List<TieuChiChamDiem>();
        public string? NhanXetChung { get; set; }
    }

    public class TieuChiChamDiem
    {
        public int MaTieuChi { get; set; }
        public string TenTieuChi { get; set; } = string.Empty;
        public string? MoTa { get; set; }
        public decimal TrongSo { get; set; }
        public decimal DiemToiDa { get; set; }
        public decimal Diem { get; set; }
        public string? NhanXet { get; set; }
    }

    // Kết quả chấm điểm
    public class KetQuaChamDiemViewModel
    {
        public string MaDeTai { get; set; } = string.Empty;
        public string TenDeTai { get; set; } = string.Empty;
        public string SinhVien { get; set; } = string.Empty;
        public decimal? DiemTongKet { get; set; }
        public List<ChiTietKetQuaDanhGia> ChiTietDanhGias { get; set; } = new List<ChiTietKetQuaDanhGia>();
        public string TrangThaiHoanThanh { get; set; } = string.Empty;
    }

    public class ChiTietKetQuaDanhGia
    {
        public string LoaiDanhGia { get; set; } = string.Empty;
        public string GiangVien { get; set; } = string.Empty;
        public decimal? DiemThanhPhan { get; set; }
        public decimal TrongSo { get; set; }
        public DateOnly? NgayDanhGia { get; set; }
        public List<ChiTietTieuChi> ChiTietTieuChis { get; set; } = new List<ChiTietTieuChi>();
    }

    public class ChiTietTieuChi
    {
        public string TenTieuChi { get; set; } = string.Empty;
        public decimal Diem { get; set; }
        public string? NhanXet { get; set; }
    }
}