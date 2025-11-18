using QuanLyDoAn.Model.Entities;
using QuanLyDoAn.Model.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;

namespace QuanLyDoAn.Controller
{
    public class SinhVienController
    {
        public DoAn? LayDoAnCuaSinhVien(string maSv)
        {
            using var context = new QuanLyDoAnContext();
            return context.DoAns
                .Include(d => d.MaGvNavigation)
                .Include(d => d.MaTrangThaiNavigation)
                .Include(d => d.MaLoaiDoAnNavigation)
                .FirstOrDefault(d => d.MaSv == maSv);
        }

        public List<TienDo> LayTienDoDoAn(string maDeTai)
        {
            using var context = new QuanLyDoAnContext();
            return context.TienDos
                .Where(t => t.MaDeTai == maDeTai)
                .OrderByDescending(t => t.NgayNop)
                .ToList();
        }

        public bool ThemTienDo(TienDo tienDo)
        {
            try
            {
                using var context = new QuanLyDoAnContext();
                context.Database.ExecuteSqlRaw(
                    "EXEC sp_ThemTienDo @MaDeTai, @GiaiDoan, @NgayNop, @NhanXet, @TrangThaiNop",
                    new SqlParameter("@MaDeTai", tienDo.MaDeTai ?? ""),
                    new SqlParameter("@GiaiDoan", tienDo.GiaiDoan ?? ""),
                    new SqlParameter("@NgayNop", (object?)tienDo.NgayNop ?? DBNull.Value),
                    new SqlParameter("@NhanXet", (object?)tienDo.NhanXet ?? DBNull.Value),
                    new SqlParameter("@TrangThaiNop", (object?)tienDo.TrangThaiNop ?? DBNull.Value)
                );
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<ThongBao> LayThongBaoDoAn(string maDeTai)
        {
            using var context = new QuanLyDoAnContext();
            return context.ThongBaos
                .Where(tb => tb.MaDeTai == maDeTai)
                .OrderByDescending(tb => tb.NgayGui)
                .ToList();
        }
    }
}