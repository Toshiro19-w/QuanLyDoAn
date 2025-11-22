using QuanLyDoAn.Model.Entities;
using QuanLyDoAn.Model.EF;
using Microsoft.EntityFrameworkCore;

namespace QuanLyDoAn.Controller
{
    public class ChamDiemController
    {
        public bool TaoDanhGia(string maDeTai, string maGv, string maLoaiDanhGia, List<(int maTieuChi, decimal diem, string? nhanXet)> chiTietDiem, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using var context = new QuanLyDoAnContext();
                using var transaction = context.Database.BeginTransaction();

                // Debug: Kiểm tra giá trị trước khi insert
                System.Diagnostics.Debug.WriteLine($"MaLoaiDanhGia: '{maLoaiDanhGia}'");
                
                var danhGia = new DanhGia
                {
                    MaDeTai = maDeTai,
                    MaGv = maGv,
                    MaLoaiDanhGia = maLoaiDanhGia,
                    NgayDanhGia = DateOnly.FromDateTime(DateTime.Now)
                };

                context.DanhGia.Add(danhGia);
                context.SaveChanges();

                decimal tongDiem = 0;
                foreach (var (maTieuChi, diem, nhanXet) in chiTietDiem)
                {
                    var chiTiet = new ChiTietDanhGia
                    {
                        MaDanhGia = danhGia.MaDanhGia,
                        MaTieuChi = maTieuChi,
                        Diem = diem,
                        NhanXet = nhanXet
                    };
                    context.ChiTietDanhGias.Add(chiTiet);
                    tongDiem += diem;
                }

                danhGia.DiemThanhPhan = tongDiem / chiTietDiem.Count;
                context.SaveChanges();

                CapNhatDiemTongKet(maDeTai, context);
                
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi khi tạo đánh giá: " + ex.Message;
                return false;
            }
        }

        public bool CapNhatDanhGia(int maDanhGia, List<(int maTieuChi, decimal diem, string? nhanXet)> chiTietDiem, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using var context = new QuanLyDoAnContext();
                using var transaction = context.Database.BeginTransaction();

                var danhGia = context.DanhGia.Include(d => d.ChiTietDanhGias).FirstOrDefault(d => d.MaDanhGia == maDanhGia);
                if (danhGia == null)
                {
                    errorMessage = "Đánh giá không tồn tại";
                    return false;
                }

                context.ChiTietDanhGias.RemoveRange(danhGia.ChiTietDanhGias);

                decimal tongDiem = 0;
                foreach (var (maTieuChi, diem, nhanXet) in chiTietDiem)
                {
                    var chiTiet = new ChiTietDanhGia
                    {
                        MaDanhGia = maDanhGia,
                        MaTieuChi = maTieuChi,
                        Diem = diem,
                        NhanXet = nhanXet
                    };
                    context.ChiTietDanhGias.Add(chiTiet);
                    tongDiem += diem;
                }

                danhGia.DiemThanhPhan = tongDiem / chiTietDiem.Count;
                danhGia.NgayDanhGia = DateOnly.FromDateTime(DateTime.Now);
                
                context.SaveChanges();
                CapNhatDiemTongKet(danhGia.MaDeTai!, context);
                
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi khi cập nhật đánh giá: " + ex.Message;
                return false;
            }
        }

        private void CapNhatDiemTongKet(string maDeTai, QuanLyDoAnContext context)
        {
            try
            {
                // Sử dụng stored procedure mới tính điểm (bao gồm tiến độ)
                context.Database.ExecuteSqlRaw(
                    "EXEC sp_TinhDiemTongKetMoi @MaDeTai",
                    new Microsoft.Data.SqlClient.SqlParameter("@MaDeTai", maDeTai)
                );
            }
            catch
            {
                // Nếu stored procedure mới chưa có, dùng cách cũ
                var doAn = context.DoAns.Find(maDeTai);
                if (doAn == null) return;

                var danhGias = context.DanhGia
                    .Include(d => d.MaLoaiDanhGiaNavigation)
                    .Where(d => d.MaDeTai == maDeTai && d.DiemThanhPhan.HasValue)
                    .ToList();

                if (!danhGias.Any()) return;

                decimal tongDiemCuoiKy = 0;
                decimal tongTrongSo = 0;

                foreach (var danhGia in danhGias)
                {
                    var trongSo = danhGia.MaLoaiDanhGiaNavigation?.TrongSoDiem ?? 0;
                    tongDiemCuoiKy += danhGia.DiemThanhPhan.Value * trongSo / 100;
                    tongTrongSo += trongSo;
                }

                doAn.Diem = tongTrongSo > 0 ? Math.Round(tongDiemCuoiKy, 2) : null;
                context.SaveChanges();
            }
        }

        public List<TieuChiDanhGia> LayTieuChiTheoLoaiDoAn(string? maLoaiDoAn)
        {
            try
            {
                using var context = new QuanLyDoAnContext();
                return context.TieuChiDanhGias
                    .Where(t => t.MaLoaiDoAn == maLoaiDoAn)
                    .OrderBy(t => t.TenTieuChi)
                    .ToList();
            }
            catch
            {
                return new List<TieuChiDanhGia>();
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

        public List<DanhGia> LayDanhGiaTheoDoAn(string maDeTai)
        {
            try
            {
                using var context = new QuanLyDoAnContext();
                return context.DanhGia
                    .Include(d => d.MaGvNavigation)
                    .Include(d => d.MaLoaiDanhGiaNavigation)
                    .Include(d => d.ChiTietDanhGias)
                        .ThenInclude(ct => ct.MaTieuChiNavigation)
                    .Where(d => d.MaDeTai == maDeTai)
                    .OrderBy(d => d.NgayDanhGia)
                    .ToList();
            }
            catch
            {
                return new List<DanhGia>();
            }
        }
    }
}