using QuanLyDoAn.Model.Entities;
using QuanLyDoAn.Model.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace QuanLyDoAn.Controller
{
    public class TienDoController
    {
        public List<TienDo> LayTienDoTheoDoAn(string maDeTai)
        {
            using var context = new QuanLyDoAnContext();
            return context.TienDos
                .Where(t => t.MaDeTai == maDeTai)
                .OrderBy(t => t.NgayNop)
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
            catch { return false; }
        }

        public bool CapNhatTienDo(int maTienDo, string nhanXet, string trangThaiNop)
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
            catch { return false; }
        }

        public bool XoaTienDo(int maTienDo)
        {
            try
            {
                using var context = new QuanLyDoAnContext();
                context.Database.ExecuteSqlRaw(
                    "EXEC sp_XoaTienDo @MaTienDo",
                    new SqlParameter("@MaTienDo", maTienDo)
                );
                return true;
            }
            catch { return false; }
        }
    }
}