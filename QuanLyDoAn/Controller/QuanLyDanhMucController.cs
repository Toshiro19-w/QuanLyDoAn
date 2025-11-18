using System;
using System.Collections.Generic;
using System.Linq;
using QuanLyDoAn.Model.EF;
using QuanLyDoAn.Model.Entities;

namespace QuanLyDoAn.Controller
{
    public class QuanLyDanhMucController
    {
        public List<LoaiDoAn> LayLoaiDoAn()
        {
            using var context = new QuanLyDoAnContext();
            return context.LoaiDoAns.OrderBy(l => l.TenLoaiDoAn).ToList();
        }

        public bool ThemLoaiDoAn(LoaiDoAn loaiDoAn, out string error)
        {
            error = string.Empty;
            try
            {
                using var context = new QuanLyDoAnContext();
                context.LoaiDoAns.Add(loaiDoAn);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool CapNhatLoaiDoAn(LoaiDoAn loaiDoAn, out string error)
        {
            error = string.Empty;
            try
            {
                using var context = new QuanLyDoAnContext();
                var entity = context.LoaiDoAns.Find(loaiDoAn.MaLoaiDoAn);
                if (entity == null)
                {
                    error = "Loại đồ án không tồn tại.";
                    return false;
                }

                entity.TenLoaiDoAn = loaiDoAn.TenLoaiDoAn;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool XoaLoaiDoAn(string maLoai, out string error)
        {
            error = string.Empty;
            try
            {
                using var context = new QuanLyDoAnContext();
                if (context.DoAns.Any(d => d.MaLoaiDoAn == maLoai))
                {
                    error = "Không thể xóa loại đồ án đang được sử dụng.";
                    return false;
                }

                var entity = context.LoaiDoAns.Find(maLoai);
                if (entity == null)
                {
                    error = "Loại đồ án không tồn tại.";
                    return false;
                }

                context.LoaiDoAns.Remove(entity);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public List<KyHoc> LayKyHoc()
        {
            using var context = new QuanLyDoAnContext();
            return context.KyHocs.OrderBy(k => k.TenKy).ToList();
        }

        public bool LuuKyHoc(KyHoc kyHoc, bool isNew, out string error)
        {
            error = string.Empty;
            try
            {
                using var context = new QuanLyDoAnContext();
                if (isNew)
                {
                    context.KyHocs.Add(kyHoc);
                }
                else
                {
                    var entity = context.KyHocs.Find(kyHoc.MaKy);
                    if (entity == null)
                    {
                        error = "Kỳ học không tồn tại.";
                        return false;
                    }
                    entity.TenKy = kyHoc.TenKy;
                }
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool XoaKyHoc(string maKy, out string error)
        {
            error = string.Empty;
            try
            {
                using var context = new QuanLyDoAnContext();
                if (context.DoAns.Any(d => d.MaKy == maKy))
                {
                    error = "Không thể xóa kỳ học đang được sử dụng.";
                    return false;
                }

                var entity = context.KyHocs.Find(maKy);
                if (entity == null)
                {
                    error = "Kỳ học không tồn tại.";
                    return false;
                }

                context.KyHocs.Remove(entity);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public List<ChuyenNganh> LayChuyenNganh()
        {
            using var context = new QuanLyDoAnContext();
            return context.ChuyenNganhs.OrderBy(c => c.TenChuyenNganh).ToList();
        }

        public bool LuuChuyenNganh(ChuyenNganh chuyenNganh, bool isNew, out string error)
        {
            error = string.Empty;
            try
            {
                using var context = new QuanLyDoAnContext();
                if (isNew)
                {
                    context.ChuyenNganhs.Add(chuyenNganh);
                }
                else
                {
                    var entity = context.ChuyenNganhs.Find(chuyenNganh.MaChuyenNganh);
                    if (entity == null)
                    {
                        error = "Chuyên ngành không tồn tại.";
                        return false;
                    }
                    entity.TenChuyenNganh = chuyenNganh.TenChuyenNganh;
                }
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool XoaChuyenNganh(string maChuyenNganh, out string error)
        {
            error = string.Empty;
            try
            {
                using var context = new QuanLyDoAnContext();
                if (context.GiangViens.Any(g => g.MaChuyenNganh == maChuyenNganh) ||
                    context.SinhViens.Any(s => s.MaChuyenNganh == maChuyenNganh))
                {
                    error = "Không thể xóa chuyên ngành đang có giảng viên hoặc sinh viên.";
                    return false;
                }

                var entity = context.ChuyenNganhs.Find(maChuyenNganh);
                if (entity == null)
                {
                    error = "Chuyên ngành không tồn tại.";
                    return false;
                }

                context.ChuyenNganhs.Remove(entity);
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

