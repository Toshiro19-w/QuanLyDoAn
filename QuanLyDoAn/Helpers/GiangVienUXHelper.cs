using QuanLyDoAn.Controller;
using QuanLyDoAn.Model.ViewModels;

namespace QuanLyDoAn.Helpers
{
    public static class GiangVienUXHelper
    {
        private static readonly GiangVienUXController _controller = new GiangVienUXController();

        // Lấy dashboard chính cho giảng viên
        public static DanhSachDoAnGiangVienViewModel LayDashboard(string maGv)
        {
            return _controller.LayDanhSachDoAnChoGiangVien(maGv);
        }

        // Lấy chi tiết một đồ án
        public static GiangVienDoAnViewModel? LayChiTietDoAn(string maDeTai, string maGv)
        {
            return _controller.LayChiTietDoAn(maDeTai, maGv);
        }

        // Chấm điểm nhanh
        public static class ChamDiem
        {
            public static ChamDiemNhanhViewModel? LayFormChamDiem(string maDeTai, string maGv, string maLoaiDanhGia)
            {
                return _controller.LayFormChamDiemNhanh(maDeTai, maGv, maLoaiDanhGia);
            }

            public static bool LuuKetQua(ChamDiemNhanhViewModel model, out string errorMessage)
            {
                return _controller.LuuChamDiemNhanh(model, out errorMessage);
            }

            public static bool ChamDiemNhanh(string maDeTai, string maGv, string maLoaiDanhGia, 
                Dictionary<int, (decimal diem, string? nhanXet)> diemTheoTieuChi, out string errorMessage)
            {
                var form = LayFormChamDiem(maDeTai, maGv, maLoaiDanhGia);
                if (form == null)
                {
                    errorMessage = "Không thể tạo form chấm điểm";
                    return false;
                }

                foreach (var tieuChi in form.DanhSachTieuChi)
                {
                    if (diemTheoTieuChi.ContainsKey(tieuChi.MaTieuChi))
                    {
                        var (diem, nhanXet) = diemTheoTieuChi[tieuChi.MaTieuChi];
                        tieuChi.Diem = diem;
                        tieuChi.NhanXet = nhanXet;
                    }
                }

                return LuuKetQua(form, out errorMessage);
            }
        }

        // Thống kê và báo cáo
        public static class ThongKe
        {
            public static Dictionary<string, object> LayThongKeGiangVien(string maGv)
            {
                var dashboard = LayDashboard(maGv);
                
                var doAnHuongDan = dashboard.DoAnHuongDan;
                var doAnCoTheCham = dashboard.DoAnCoTheCham;

                return new Dictionary<string, object>
                {
                    ["TongDoAnHuongDan"] = doAnHuongDan.Count,
                    ["DoAnDaHoanThanh"] = doAnHuongDan.Count(d => d.DiemTongKet.HasValue),
                    ["DoAnCanChamDiem"] = doAnHuongDan.Count(d => d.CoTheChamDiem),
                    ["DoAnCoTheCham"] = doAnCoTheCham.Count,
                    ["TongCongViec"] = doAnHuongDan.Count + doAnCoTheCham.Count(d => d.CoTheChamDiem),
                    ["TienDoHoanThanh"] = doAnHuongDan.Count > 0 ? 
                        (double)doAnHuongDan.Count(d => d.DiemTongKet.HasValue) / doAnHuongDan.Count * 100 : 0
                };
            }

            public static List<GiangVienDoAnViewModel> LayDoAnCanChamDiem(string maGv)
            {
                var dashboard = LayDashboard(maGv);
                return dashboard.DoAnHuongDan
                    .Where(d => d.CoTheChamDiem)
                    .OrderBy(d => d.NgayKetThuc)
                    .ToList();
            }

            public static List<GiangVienDoAnViewModel> LayDoAnSapHetHan(string maGv, int soNgay = 7)
            {
                var dashboard = LayDashboard(maGv);
                var ngayHienTai = DateOnly.FromDateTime(DateTime.Now);
                
                return dashboard.DoAnHuongDan
                    .Where(d => d.NgayKetThuc.HasValue && 
                               d.NgayKetThuc.Value.AddDays(-soNgay) <= ngayHienTai &&
                               d.NgayKetThuc.Value >= ngayHienTai)
                    .OrderBy(d => d.NgayKetThuc)
                    .ToList();
            }
        }

        // Utilities
        public static class Utils
        {
            public static string LayMauTrangThai(string trangThai)
            {
                return trangThai switch
                {
                    "Đang thực hiện" => "info",
                    "Sẵn sàng bảo vệ" => "success",
                    "Đã bảo vệ" => "primary",
                    "Tạm dừng" => "warning",
                    "Đã hủy" => "danger",
                    _ => "secondary"
                };
            }

            public static string LayIconTrangThai(string trangThai)
            {
                return trangThai switch
                {
                    "Đang thực hiện" => "⏳",
                    "Sẵn sàng bảo vệ" => "✅",
                    "Đã bảo vệ" => "🎓",
                    "Tạm dừng" => "⏸️",
                    "Đã hủy" => "❌",
                    _ => "📋"
                };
            }

            public static string LayMauDiem(decimal? diem)
            {
                if (!diem.HasValue) return "secondary";
                
                return diem.Value switch
                {
                    >= 8.5m => "success",
                    >= 7.0m => "info", 
                    >= 5.5m => "warning",
                    _ => "danger"
                };
            }

            public static bool KiemTraHopLeDiem(decimal diem, decimal diemToiDa)
            {
                return diem >= 0 && diem <= diemToiDa;
            }

            public static string FormatDiem(decimal? diem)
            {
                return diem?.ToString("F1") ?? "N/A";
            }
        }
    }
}