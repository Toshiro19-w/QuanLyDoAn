using QuanLyDoAn.Model.Entities;
using QuanLyDoAn.Model.EF;
using QuanLyDoAn.Utils;
using Microsoft.EntityFrameworkCore;

namespace QuanLyDoAn.Controller
{
    public class TaiKhoanController
    {
        private readonly QuanLyDoAnContext _context;

        public TaiKhoanController()
        {
            _context = new QuanLyDoAnContext();
        }

        public TaiKhoan? DangNhap(string tenDangNhap, string matKhau)
        {
            string hashedPassword = HashHelper.HashPassword(matKhau);
            return _context.TaiKhoans
                .Include(t => t.MaSvNavigation)
                .Include(t => t.MaGvNavigation)
                .FirstOrDefault(t => t.TenDangNhap == tenDangNhap && t.MatKhau == hashedPassword);
        }

        public bool DoiMatKhau(string tenDangNhap, string matKhauMoi)
        {
            if (!Validation.IsValidPassword(matKhauMoi)) return false;
            
            var taiKhoan = _context.TaiKhoans.Find(tenDangNhap);
            if (taiKhoan != null)
            {
                taiKhoan.MatKhau = HashHelper.HashPassword(matKhauMoi);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}