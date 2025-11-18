using QuanLyDoAn.Model.Entities;
using QuanLyDoAn.Model.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace QuanLyDoAn.Controller
{
    public class GiangVienController
    {
        public List<DoAn> LayDoAnDuocPhanCong(string maGv)
        {
            using var context = new QuanLyDoAnContext();
            return context.DoAns
                .Include(d => d.MaSvNavigation)
                .Include(d => d.MaTrangThaiNavigation)
                .Where(d => d.MaGv == maGv)
                .ToList();
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

        public bool ChamDiem(string maDeTai, decimal diem)
        {
            using var context = new QuanLyDoAnContext();
            var doAn = context.DoAns.Find(maDeTai);
            if (doAn != null)
            {
                doAn.Diem = diem;
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}