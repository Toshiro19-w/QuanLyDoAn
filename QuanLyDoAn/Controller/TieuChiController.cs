using QuanLyDoAn.Model.Entities;
using QuanLyDoAn.Model.EF;

namespace QuanLyDoAn.Controller
{
    public class TieuChiController
    {
        public bool TaoTieuChi(TieuChiDanhGia tieuChi, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using var context = new QuanLyDoAnContext();
                context.TieuChiDanhGias.Add(tieuChi);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi khi tạo tiêu chí: " + ex.Message;
                return false;
            }
        }

        public bool CapNhatTieuChi(TieuChiDanhGia tieuChi, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using var context = new QuanLyDoAnContext();
                var entity = context.TieuChiDanhGias.Find(tieuChi.MaTieuChi);
                if (entity == null)
                {
                    errorMessage = "Tiêu chí không tồn tại";
                    return false;
                }

                entity.TenTieuChi = tieuChi.TenTieuChi;
                entity.MoTa = tieuChi.MoTa;
                entity.TrongSo = tieuChi.TrongSo;
                entity.DiemToiDa = tieuChi.DiemToiDa;
                entity.MaLoaiDoAn = tieuChi.MaLoaiDoAn;

                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi khi cập nhật tiêu chí: " + ex.Message;
                return false;
            }
        }

        public bool XoaTieuChi(int maTieuChi, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using var context = new QuanLyDoAnContext();
                var entity = context.TieuChiDanhGias.Find(maTieuChi);
                if (entity == null)
                {
                    errorMessage = "Tiêu chí không tồn tại";
                    return false;
                }

                context.TieuChiDanhGias.Remove(entity);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi khi xóa tiêu chí: " + ex.Message;
                return false;
            }
        }

        public List<TieuChiDanhGia> LayDanhSachTieuChi(string? maLoaiDoAn = null)
        {
            try
            {
                using var context = new QuanLyDoAnContext();
                var query = context.TieuChiDanhGias.AsQueryable();
                
                if (!string.IsNullOrEmpty(maLoaiDoAn))
                    query = query.Where(t => t.MaLoaiDoAn == maLoaiDoAn);

                return query.OrderBy(t => t.TenTieuChi).ToList();
            }
            catch
            {
                return new List<TieuChiDanhGia>();
            }
        }

        public bool TaoLoaiDanhGia(LoaiDanhGia loaiDanhGia, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using var context = new QuanLyDoAnContext();
                context.LoaiDanhGias.Add(loaiDanhGia);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi khi tạo loại đánh giá: " + ex.Message;
                return false;
            }
        }

        public List<LoaiDanhGia> LayDanhSachLoaiDanhGia()
        {
            try
            {
                using var context = new QuanLyDoAnContext();
                return context.LoaiDanhGias.OrderBy(l => l.TenLoaiDanhGia).ToList();
            }
            catch
            {
                return new List<LoaiDanhGia>();
            }
        }
    }
}