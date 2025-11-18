using QuanLyDoAn.Model.Entities;
using QuanLyDoAn.Model.EF;
using Microsoft.EntityFrameworkCore;

namespace QuanLyDoAn.Controller
{
    public class DangKyDoAnController
    {
        public bool TaoDoAnMoi(DoAn doAn)
        {
            try
            {
                using var context = new QuanLyDoAnContext();
                doAn.MaSv = null; // Chưa có sinh viên
                context.DoAns.Add(doAn);
                context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public List<DoAn> LayDoAnChuaCoSinhVien()
        {
            using var context = new QuanLyDoAnContext();
            return context.DoAns
                .Include(d => d.MaGvNavigation)
                .Include(d => d.MaLoaiDoAnNavigation)
                .Include(d => d.MaTrangThaiNavigation)
                .Where(d => d.MaSv == null)
                .OrderByDescending(d => d.NgayBatDau)
                .ToList();
        }

        public bool GuiYeuCauDangKy(string maDeTai, string maSv, string? ghiChu)
        {
            try
            {
                using var context = new QuanLyDoAnContext();
                
                // Kiểm tra đã đăng ký chưa
                var existing = context.YeuCauDangKies
                    .Any(y => y.MaDeTai == maDeTai && y.MaSv == maSv && y.TrangThai == "Pending");
                if (existing) return false;

                var yeuCau = new YeuCauDangKy
                {
                    MaDeTai = maDeTai,
                    MaSv = maSv,
                    NgayGui = DateOnly.FromDateTime(DateTime.Now),
                    TrangThai = "Pending",
                    GhiChu = ghiChu
                };
                context.YeuCauDangKies.Add(yeuCau);
                context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public List<YeuCauDangKy> LayYeuCauTheoGiangVien(string maGv)
        {
            using var context = new QuanLyDoAnContext();
            return context.YeuCauDangKies
                .Include(y => y.MaDeTaiNavigation)
                .Include(y => y.MaSvNavigation)
                .Where(y => y.MaDeTaiNavigation.MaGv == maGv && y.TrangThai == "Pending")
                .OrderByDescending(y => y.NgayGui)
                .ToList();
        }

        public List<YeuCauDangKy> LayYeuCauCuaSinhVien(string maSv)
        {
            using var context = new QuanLyDoAnContext();
            return context.YeuCauDangKies
                .Include(y => y.MaDeTaiNavigation)
                .Where(y => y.MaSv == maSv)
                .OrderByDescending(y => y.NgayGui)
                .ToList();
        }

        public bool DuyetYeuCau(int maYeuCau, bool chapNhan)
        {
            try
            {
                using var context = new QuanLyDoAnContext();
                var yeuCau = context.YeuCauDangKies
                    .Include(y => y.MaDeTaiNavigation)
                    .FirstOrDefault(y => y.MaYeuCau == maYeuCau);
                
                if (yeuCau == null) return false;

                if (chapNhan)
                {
                    // Gán sinh viên vào đồ án
                    var doAn = context.DoAns.Find(yeuCau.MaDeTai);
                    if (doAn != null && doAn.MaSv == null)
                    {
                        doAn.MaSv = yeuCau.MaSv;
                        yeuCau.TrangThai = "Approved";
                        
                        // Từ chối tất cả các yêu cầu khác cho đề tài này
                        var otherRequests = context.YeuCauDangKies
                            .Where(y => y.MaDeTai == yeuCau.MaDeTai 
                                     && y.MaYeuCau != maYeuCau 
                                     && y.TrangThai == "Pending")
                            .ToList();
                        
                        foreach (var req in otherRequests)
                        {
                            req.TrangThai = "Rejected";
                        }
                    }
                    else return false;
                }
                else
                {
                    yeuCau.TrangThai = "Rejected";
                }

                context.SaveChanges();
                return true;
            }
            catch 
            { 
                return false; 
            }
        }
    }
}
