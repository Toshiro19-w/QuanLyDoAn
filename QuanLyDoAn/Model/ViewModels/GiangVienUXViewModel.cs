namespace QuanLyDoAn.Model.ViewModels
{
    // ViewModel thống nhất cho giao diện giảng viên
    public class GiangVienDoAnViewModel
    {
        public string MaDeTai { get; set; } = string.Empty;
        public string TenDeTai { get; set; } = string.Empty;
        public string SinhVien { get; set; } = string.Empty;
        public string LoaiDoAn { get; set; } = string.Empty;
        public string TrangThai { get; set; } = string.Empty;
        public DateOnly? NgayBatDau { get; set; }
        public DateOnly? NgayKetThuc { get; set; }
        public decimal? DiemTongKet { get; set; }
        
        // Thông tin chấm điểm
        public string TrangThaiChamDiem { get; set; } = string.Empty;
        public List<LoaiDanhGiaStatus> DanhSachLoaiDanhGia { get; set; } = new List<LoaiDanhGiaStatus>();
        public List<TienDoSummary> TienDoTomTat { get; set; } = new List<TienDoSummary>();
        
        // Trạng thái UI
        public bool CoTheXem { get; set; } = true;
        public bool CoTheChamDiem { get; set; }
        public string GhiChuTrangThai { get; set; } = string.Empty;
    }

    public class LoaiDanhGiaStatus
    {
        public string MaLoai { get; set; } = string.Empty;
        public string TenLoai { get; set; } = string.Empty;
        public decimal TrongSo { get; set; }
        public bool DaCham { get; set; }
        public bool CoTheChams { get; set; }
        public decimal? DiemDaCham { get; set; }
        public DateOnly? NgayDanhGia { get; set; }
        public string LyDoKhongCham { get; set; } = string.Empty;
        public string TrangThaiHienThi => DaCham ? $"Đã chấm ({DiemDaCham:F1})" : 
                                        CoTheChams ? "Có thể chấm" : $"Chưa thể chấm - {LyDoKhongCham}";
    }

    public class TienDoSummary
    {
        public string GiaiDoan { get; set; } = string.Empty;
        public DateOnly? NgayNop { get; set; }
        public string TrangThaiNop { get; set; } = string.Empty;
        public bool DaNop => TrangThaiNop == "Đã nộp" || TrangThaiNop == "Đạt";
    }

    // ViewModel cho form chấm điểm đơn giản
    public class ChamDiemNhanhViewModel
    {
        public string MaDeTai { get; set; } = string.Empty;
        public string TenDeTai { get; set; } = string.Empty;
        public string SinhVien { get; set; } = string.Empty;
        public string MaGv { get; set; } = string.Empty;
        public string MaLoaiDanhGia { get; set; } = string.Empty;
        public string TenLoaiDanhGia { get; set; } = string.Empty;
        public string? NhanXetChung { get; set; }
        
        public List<TieuChiNhanh> DanhSachTieuChi { get; set; } = new List<TieuChiNhanh>();
        
        // Tính toán tự động
        public decimal DiemTrungBinh => DanhSachTieuChi.Any() ? 
            DanhSachTieuChi.Average(t => t.Diem) : 0;
        public bool DayDuThongTin => DanhSachTieuChi.All(t => t.Diem > 0);
    }

    public class TieuChiNhanh
    {
        public int MaTieuChi { get; set; }
        public string TenTieuChi { get; set; } = string.Empty;
        public string? MoTa { get; set; }
        public decimal TrongSo { get; set; }
        public decimal DiemToiDa { get; set; }
        public decimal Diem { get; set; }
        public string? NhanXet { get; set; }
        
        // UI helpers
        public string HienThiTieuChi => $"{TenTieuChi} ({TrongSo}% - Tối đa: {DiemToiDa})";
        public bool HopLe => Diem >= 0 && Diem <= DiemToiDa;
    }

    // ViewModel cho danh sách đồ án của giảng viên
    public class DanhSachDoAnGiangVienViewModel
    {
        public string MaGiangVien { get; set; } = string.Empty;
        public string TenGiangVien { get; set; } = string.Empty;
        public List<GiangVienDoAnViewModel> DoAnHuongDan { get; set; } = new List<GiangVienDoAnViewModel>();
        public List<GiangVienDoAnViewModel> DoAnCoTheCham { get; set; } = new List<GiangVienDoAnViewModel>();
        
        // Thống kê
        public int TongSoDoAn => DoAnHuongDan.Count + DoAnCoTheCham.Count;
        public int SoDoAnDaChamHet => DoAnHuongDan.Count(d => d.DanhSachLoaiDanhGia.All(l => l.DaCham));
        public int SoDoAnCanCham => DoAnHuongDan.Count(d => d.DanhSachLoaiDanhGia.Any(l => l.CoTheChams && !l.DaCham));
    }
}