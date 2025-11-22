using QuanLyDoAn.Model.Entities;
using QuanLyDoAn.Model.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System;
using System.Linq;

namespace QuanLyDoAn.Controller
{
    public class GiangVienController
    {
        public List<DoAn> LayDoAnDuocPhanCong(string maGv)
        {
            using var context = new QuanLyDoAnContext();
            
            // Lấy đồ án hướng dẫn
            var doAnHuongDan = context.DoAns
                .Include(d => d.MaSvNavigation)
                .Include(d => d.MaLoaiDoAnNavigation)
                .Include(d => d.MaTrangThaiNavigation)
                .Where(d => d.MaGv == maGv)
                .ToList();
            
            // Lấy đồ án được phân công chấm (PB, HĐ) từ bảng DanhGia
            var maDeTaiDuocCham = context.DanhGia
                .Where(d => d.MaGv == maGv)
                .Select(d => d.MaDeTai)
                .Distinct()
                .ToList();
            
            var doAnDuocCham = context.DoAns
                .Include(d => d.MaSvNavigation)
                .Include(d => d.MaLoaiDoAnNavigation)
                .Include(d => d.MaTrangThaiNavigation)
                .Where(d => maDeTaiDuocCham.Contains(d.MaDeTai) && d.MaGv != maGv)
                .ToList();
            
            // Gộp lại và loại trùng
            return doAnHuongDan.Union(doAnDuocCham).ToList();
        }

        public List<TienDo> LayTienDoTheoDoAn(string maDeTai)
        {
            using var context = new QuanLyDoAnContext();
            return context.TienDos
                .Where(t => t.MaDeTai == maDeTai)
                .OrderByDescending(t => t.NgayNop)
                .ToList();
        }

        public bool CapNhatNhanXet(int maTienDo, string nhanXet, string trangThaiNop)
        {
            try
            {
                using var context = new QuanLyDoAnContext();
                context.Database.ExecuteSqlRaw(
                    "EXEC sp_SuaTienDo @MaTienDo, @NhanXet, @TrangThaiNop",
                    new SqlParameter("@MaTienDo", maTienDo),
                    new SqlParameter("@NhanXet", nhanXet),
                    new SqlParameter("@TrangThaiNop", trangThaiNop)
                );
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CapNhatNhanXetVaDiem(int maTienDo, string nhanXet, string trangThaiNop, decimal? diemTienDo)
        {
            try
            {
                using var context = new QuanLyDoAnContext();
                var tienDo = context.TienDos.Find(maTienDo);
                if (tienDo == null) return false;
                
                tienDo.NhanXet = nhanXet;
                tienDo.TrangThaiNop = trangThaiNop;
                tienDo.DiemTienDo = diemTienDo;
                
                context.SaveChanges();
                
                // Tự động tính lại điểm tổng kết
                if (!string.IsNullOrEmpty(tienDo.MaDeTai))
                {
                    try
                    {
                        context.Database.ExecuteSqlRaw(
                            "EXEC sp_TinhDiemTongKetMoi @MaDeTai",
                            new Microsoft.Data.SqlClient.SqlParameter("@MaDeTai", tienDo.MaDeTai)
                        );
                    }
                    catch { }
                }
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ChamDiem(string maDeTai, decimal diem, string maGv)
        {
            if (string.IsNullOrWhiteSpace(maDeTai) || string.IsNullOrWhiteSpace(maGv))
            {
                return false;
            }

            if (diem < 0 || diem > 10)
            {
                return false;
            }

            using var context = new QuanLyDoAnContext();
            var doAn = context.DoAns.FirstOrDefault(d => d.MaDeTai == maDeTai && d.MaGv == maGv);
            if (doAn == null || doAn.MaSv == null)
            {
                return false;
            }

            doAn.Diem = Math.Round(diem, 2, MidpointRounding.AwayFromZero);
            context.SaveChanges();
            return true;
        }

        public bool XoaDeTaiCuaGiangVien(string maDeTai, string maGv, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (string.IsNullOrWhiteSpace(maDeTai) || string.IsNullOrWhiteSpace(maGv))
            {
                errorMessage = "Thiếu thông tin đề tài hoặc giảng viên.";
                return false;
            }

            using var context = new QuanLyDoAnContext();
            var doAn = context.DoAns
                .FirstOrDefault(d => d.MaDeTai == maDeTai && d.MaGv == maGv);

            if (doAn == null)
            {
                errorMessage = "Bạn không có quyền xóa đề tài này.";
                return false;
            }

            if (doAn.MaSv != null)
            {
                errorMessage = "Không thể xóa đề tài đã có sinh viên được phân công.";
                return false;
            }

            context.Database.ExecuteSqlRaw(
                "EXEC sp_XoaDoAn @MaDeTai",
                new SqlParameter("@MaDeTai", maDeTai));

            return true;
        }
    }
}