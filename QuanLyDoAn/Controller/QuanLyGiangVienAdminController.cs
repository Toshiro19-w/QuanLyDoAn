using System;
using System.Collections.Generic;
using System.Linq;
using QuanLyDoAn.Model.EF;
using QuanLyDoAn.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace QuanLyDoAn.Controller
{
    public class QuanLyGiangVienAdminController
    {
        public List<GiangVien> LayDanhSachGiangVien()
        {
            using var context = new QuanLyDoAnContext();
            return context.GiangViens
                .Include(g => g.MaChuyenNganhNavigation)
                .OrderBy(g => g.MaGv)
                .ToList();
        }

        public List<ChuyenNganh> LayDanhSachChuyenNganh()
        {
            using var context = new QuanLyDoAnContext();
            return context.ChuyenNganhs
                .OrderBy(c => c.TenChuyenNganh)
                .ToList();
        }

        public bool ThemGiangVien(GiangVien giangVien, out string error)
        {
            error = string.Empty;
            try
            {
                using var context = new QuanLyDoAnContext();
                context.GiangViens.Add(giangVien);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool CapNhatGiangVien(GiangVien giangVien, out string error)
        {
            error = string.Empty;
            try
            {
                using var context = new QuanLyDoAnContext();
                var entity = context.GiangViens.Find(giangVien.MaGv);
                if (entity == null)
                {
                    error = "Giảng viên không tồn tại.";
                    return false;
                }

                entity.HoTen = giangVien.HoTen;
                entity.Email = giangVien.Email;
                entity.SoDienThoai = giangVien.SoDienThoai;
                entity.BoMon = giangVien.BoMon;
                entity.ChucVu = giangVien.ChucVu;
                entity.MaChuyenNganh = giangVien.MaChuyenNganh;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool XoaGiangVien(string maGv, out string error)
        {
            error = string.Empty;
            try
            {
                using var context = new QuanLyDoAnContext();

                if (context.DoAns.Any(d => d.MaGv == maGv))
                {
                    error = "Không thể xóa giảng viên đang hướng dẫn đồ án.";
                    return false;
                }

                var entity = context.GiangViens.Find(maGv);
                if (entity == null)
                {
                    error = "Giảng viên không tồn tại.";
                    return false;
                }

                context.GiangViens.Remove(entity);
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

