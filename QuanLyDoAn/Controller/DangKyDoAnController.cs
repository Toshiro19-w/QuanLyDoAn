using QuanLyDoAn.Model.Entities;
using QuanLyDoAn.Model.EF;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public List<DoAn> LayDoAnChuaCoSinhVien(string? maSv = null)
        {
            using var context = new QuanLyDoAnContext();
            var query = context.DoAns
                .Include(d => d.MaGvNavigation)
                .Include(d => d.MaLoaiDoAnNavigation)
                .Include(d => d.MaTrangThaiNavigation)
                .Where(d => d.MaSv == null);

            if (!string.IsNullOrWhiteSpace(maSv))
            {
                string[] trangThaiChan = { "Pending", "Approved" };
                query = query.Where(d => !context.YeuCauDangKies
                    .Any(y => y.MaDeTai == d.MaDeTai
                           && y.MaSv == maSv
                           && trangThaiChan.Contains(y.TrangThai)));
            }

            return query
                .OrderByDescending(d => d.NgayBatDau)
                .ToList();
        }

        public bool GuiYeuCauDangKy(string maDeTai, string maSv, string? ghiChu)
        {
            try
            {
                using var context = new QuanLyDoAnContext();

                var doAn = context.DoAns.FirstOrDefault(d => d.MaDeTai == maDeTai);
                if (doAn == null || doAn.MaSv != null)
                {
                    return false;
                }

                if (context.DoAns.Any(d => d.MaSv == maSv))
                {
                    // Sinh viên đã có đồ án khác
                    return false;
                }

                int soLuongDangKy = context.YeuCauDangKies
                    .Count(y => y.MaDeTai == maDeTai && y.TrangThai == "Pending");
                if (soLuongDangKy >= 10)
                {
                    return false;
                }

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
                using var transaction = context.Database.BeginTransaction();

                var yeuCau = context.YeuCauDangKies
                    .Include(y => y.MaDeTaiNavigation)
                    .FirstOrDefault(y => y.MaYeuCau == maYeuCau);

                if (yeuCau == null) return false;

                if (chapNhan)
                {
                    if (context.DoAns.Any(d => d.MaSv == yeuCau.MaSv))
                    {
                        return false;
                    }

                    var doAn = context.DoAns.FirstOrDefault(d => d.MaDeTai == yeuCau.MaDeTai);
                    if (doAn == null || doAn.MaSv != null)
                    {
                        return false;
                    }

                    doAn.MaSv = yeuCau.MaSv;
                    yeuCau.TrangThai = "Approved";

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
                else
                {
                    yeuCau.TrangThai = "Rejected";
                }

                context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch 
            { 
                return false; 
            }
        }

        public bool SinhVienDaCoDoAn(string maSv)
        {
            if (string.IsNullOrWhiteSpace(maSv)) return false;
            using var context = new QuanLyDoAnContext();
            return context.DoAns.Any(d => d.MaSv == maSv);
        }
    }
}
