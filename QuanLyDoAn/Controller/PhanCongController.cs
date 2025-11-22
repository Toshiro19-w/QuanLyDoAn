using QuanLyDoAn.Model.EF;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace QuanLyDoAn.Controller
{
    public class PhanCongController
    {
        public bool PhanCongGiangVien(string maDeTai, string maGv, string maLoaiDanhGia, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using var context = new QuanLyDoAnContext();
                
                // Kiểm tra giảng viên đã được phân công vai trò khác chưa
                var daPhanCongKhac = context.DanhGia.Any(d => 
                    d.MaDeTai == maDeTai && 
                    d.MaGv == maGv);
                
                if (daPhanCongKhac)
                {
                    errorMessage = "Giảng viên này đã được phân công vai trò khác cho đồ án này!";
                    return false;
                }
                
                // Kiểm tra đồ án tồn tại
                var doAn = context.DoAns.FirstOrDefault(d => d.MaDeTai == maDeTai);
                if (doAn == null)
                {
                    errorMessage = "Đồ án không tồn tại";
                    return false;
                }
                
                // Không cho phép phân công HD (GVHD được gán trong DoAn.MaGvhd)
                if (maLoaiDanhGia == "HD")
                {
                    errorMessage = "Không thể phân công GVHD qua chức năng này. GVHD được gán trực tiếp trong quản lý đồ án.";
                    return false;
                }
                
                // Kiểm tra GVHD không được làm PB hoặc HĐ
                if (doAn.MaGv == maGv)
                {
                    errorMessage = "Giảng viên này đang là GVHD của đồ án, không thể phân công thêm vai trò khác!";
                    return false;
                }
                
                // Kiểm tra loại đánh giá đã có người chấm chưa
                var daCoNguoiCham = context.DanhGia.Any(d => 
                    d.MaDeTai == maDeTai && 
                    d.MaLoaiDanhGia == maLoaiDanhGia);
                
                if (daCoNguoiCham)
                {
                    errorMessage = "Loại đánh giá này đã có giảng viên phân công rồi!";
                    return false;
                }
                
                context.Database.ExecuteSqlRaw(
                    "EXEC sp_PhanCongGiangVien @MaDeTai, @MaGV, @MaLoaiDanhGia",
                    new SqlParameter("@MaDeTai", maDeTai),
                    new SqlParameter("@MaGV", maGv),
                    new SqlParameter("@MaLoaiDanhGia", maLoaiDanhGia)
                );
                
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi: " + ex.Message;
                return false;
            }
        }
        
        public bool HuyPhanCong(string maDeTai, string maGv, string maLoaiDanhGia, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using var context = new QuanLyDoAnContext();
                context.Database.ExecuteSqlRaw(
                    "EXEC sp_HuyPhanCongGiangVien @MaDeTai, @MaGV, @MaLoaiDanhGia",
                    new SqlParameter("@MaDeTai", maDeTai),
                    new SqlParameter("@MaGV", maGv),
                    new SqlParameter("@MaLoaiDanhGia", maLoaiDanhGia)
                );
                
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi: " + ex.Message;
                return false;
            }
        }
        
        public List<PhanCongInfo> LayDanhSachPhanCong(string maDeTai)
        {
            try
            {
                using var context = new QuanLyDoAnContext();
                return context.DanhGia
                    .Include(d => d.MaGvNavigation)
                    .Include(d => d.MaLoaiDanhGiaNavigation)
                    .Where(d => d.MaDeTai == maDeTai)
                    .Select(d => new PhanCongInfo
                    {
                        MaGv = d.MaGv,
                        TenGiangVien = d.MaGvNavigation!.HoTen,
                        MaLoaiDanhGia = d.MaLoaiDanhGia,
                        TenLoaiDanhGia = d.MaLoaiDanhGiaNavigation!.TenLoaiDanhGia,
                        DaCham = d.DiemThanhPhan.HasValue,
                        DiemThanhPhan = d.DiemThanhPhan,
                        NgayDanhGia = d.NgayDanhGia
                    })
                    .ToList();
            }
            catch
            {
                return new List<PhanCongInfo>();
            }
        }
    }
    
    public class PhanCongInfo
    {
        public string MaGv { get; set; } = "";
        public string TenGiangVien { get; set; } = "";
        public string? MaLoaiDanhGia { get; set; }
        public string TenLoaiDanhGia { get; set; } = "";
        public bool DaCham { get; set; }
        public decimal? DiemThanhPhan { get; set; }
        public DateOnly? NgayDanhGia { get; set; }
    }
}
