using QuanLyDoAn.Model.Entities;
using QuanLyDoAn.Model.EF;
using QuanLyDoAn.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace QuanLyDoAn.Controller
{
    public class QuanLyTaiKhoanController
    {

        public List<TaiKhoan> LayDanhSachTaiKhoan()
        {
            using var context = new QuanLyDoAnContext();
            return context.TaiKhoans
                .Include(t => t.MaSvNavigation)
                .Include(t => t.MaGvNavigation)
                .ToList();
        }

        public bool TaoTaiKhoan(TaiKhoan taiKhoan)
        {
            try
            {
                if (!Validation.IsValidPassword(taiKhoan.MatKhau))
                {
                    return false;
                }

                taiKhoan.MatKhau = HashHelper.HashPassword(taiKhoan.MatKhau);

                using var context = new QuanLyDoAnContext();
                context.TaiKhoans.Add(taiKhoan);
                context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool CapNhatTaiKhoan(TaiKhoan taiKhoan)
        {
            try
            {
                using var context = new QuanLyDoAnContext();
                var existing = context.TaiKhoans.FirstOrDefault(t => t.TenDangNhap == taiKhoan.TenDangNhap);
                if (existing == null) return false;

                existing.VaiTro = taiKhoan.VaiTro;
                existing.MaGv = taiKhoan.MaGv;
                existing.MaSv = taiKhoan.MaSv;

                if (!string.IsNullOrWhiteSpace(taiKhoan.MatKhau))
                {
                    if (!Validation.IsValidPassword(taiKhoan.MatKhau))
                    {
                        return false;
                    }

                    existing.MatKhau = HashHelper.HashPassword(taiKhoan.MatKhau);
                }

                context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool XoaTaiKhoan(string tenDangNhap)
        {
            try
            {
                using var context = new QuanLyDoAnContext();
                var taiKhoan = context.TaiKhoans.Find(tenDangNhap);
                if (taiKhoan != null)
                {
                    context.TaiKhoans.Remove(taiKhoan);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        public List<SinhVien> LayDanhSachSinhVien()
        {
            using var context = new QuanLyDoAnContext();
            return context.SinhViens.ToList();
        }

        public List<GiangVien> LayDanhSachGiangVien()
        {
            using var context = new QuanLyDoAnContext();
            return context.GiangViens.ToList();
        }
    }
}