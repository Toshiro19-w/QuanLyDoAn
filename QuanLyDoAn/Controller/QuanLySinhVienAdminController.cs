using System;
using System.Collections.Generic;
using System.Linq;
using QuanLyDoAn.Model.EF;
using QuanLyDoAn.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace QuanLyDoAn.Controller
{
    public class QuanLySinhVienAdminController
    {
        public List<SinhVien> LayDanhSachSinhVien()
        {
            using var context = new QuanLyDoAnContext();
            return context.SinhViens
                .Include(sv => sv.MaChuyenNganhNavigation)
                .OrderBy(sv => sv.MaSv)
                .ToList();
        }

        public List<ChuyenNganh> LayDanhSachChuyenNganh()
        {
            using var context = new QuanLyDoAnContext();
            return context.ChuyenNganhs
                .OrderBy(cn => cn.TenChuyenNganh)
                .ToList();
        }

        public bool ThemSinhVien(SinhVien sinhVien, out string error)
        {
            error = string.Empty;
            try
            {
                using var context = new QuanLyDoAnContext();
                context.SinhViens.Add(sinhVien);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool CapNhatSinhVien(SinhVien sinhVien, out string error)
        {
            error = string.Empty;
            try
            {
                using var context = new QuanLyDoAnContext();
                var entity = context.SinhViens.Find(sinhVien.MaSv);
                if (entity == null)
                {
                    error = "Sinh viên không tồn tại.";
                    return false;
                }

                entity.HoTen = sinhVien.HoTen;
                entity.Email = sinhVien.Email;
                entity.SoDienThoai = sinhVien.SoDienThoai;
                entity.Lop = sinhVien.Lop;
                entity.MaChuyenNganh = sinhVien.MaChuyenNganh;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool XoaSinhVien(string maSv, out string error)
        {
            error = string.Empty;
            try
            {
                using var context = new QuanLyDoAnContext();

                if (context.DoAns.Any(d => d.MaSv == maSv))
                {
                    error = "Không thể xóa sinh viên đã được phân công đồ án.";
                    return false;
                }

                var entity = context.SinhViens.Find(maSv);
                if (entity == null)
                {
                    error = "Sinh viên không tồn tại.";
                    return false;
                }

                context.SinhViens.Remove(entity);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}

