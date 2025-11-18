using QuanLyDoAn.Model.Entities;
using QuanLyDoAn.Model.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace QuanLyDoAn.Controller
{
    public class ThongBaoController
    {
        public bool GuiThongBao(string maDeTai, string noiDung)
        {
            try
            {
                using var context = new QuanLyDoAnContext();
                context.Database.ExecuteSqlRaw(
                    "EXEC sp_ThemThongBao @MaDeTai, @NoiDung",
                    new SqlParameter("@MaDeTai", maDeTai),
                    new SqlParameter("@NoiDung", noiDung)
                );
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<ThongBao> LayThongBaoTheoDoAn(string maDeTai)
        {
            using var context = new QuanLyDoAnContext();
            return context.ThongBaos
                .Where(tb => tb.MaDeTai == maDeTai)
                .OrderByDescending(tb => tb.NgayGui)
                .ToList();
        }
    }
}