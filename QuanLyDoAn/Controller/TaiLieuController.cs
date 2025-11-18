using QuanLyDoAn.Model.Entities;
using QuanLyDoAn.Model.EF;

namespace QuanLyDoAn.Controller
{
    public class TaiLieuController
    {
        public List<TaiLieu> LayTaiLieuTheoDoAn(string maDeTai)
        {
            using var context = new QuanLyDoAnContext();
            return context.TaiLieus
                .Where(tl => tl.MaDeTai == maDeTai)
                .OrderByDescending(tl => tl.NgayUpload)
                .ToList();
        }

        public bool ThemTaiLieu(TaiLieu taiLieu)
        {
            try
            {
                using var context = new QuanLyDoAnContext();
                context.TaiLieus.Add(taiLieu);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool XoaTaiLieu(int maTaiLieu)
        {
            try
            {
                using var context = new QuanLyDoAnContext();
                var taiLieu = context.TaiLieus.Find(maTaiLieu);
                if (taiLieu != null)
                {
                    context.TaiLieus.Remove(taiLieu);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}