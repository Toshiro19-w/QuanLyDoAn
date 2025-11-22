using QuanLyDoAn.Model.Entities;
using QuanLyDoAn.Model.EF;
using QuanLyDoAn.Model.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;

namespace QuanLyDoAn.Controller
{
    public class DoAnController
    {
        public List<DoAnViewModel> LayDanhSachDoAn()
        {
            try
            {
                using var context = new QuanLyDoAnContext();
                var result = new List<DoAnViewModel>();
                
                using var command = context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "sp_LietKeDoAn";
                command.CommandType = CommandType.StoredProcedure;
                context.Database.OpenConnection();
                
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new DoAnViewModel
                    {
                        MaDeTai = GetSafeString(reader, "MaDeTai"),
                        TenDeTai = GetSafeString(reader, "TenDeTai"),
                        LoaiDoAn = GetSafeString(reader, "TenLoaiDoAn", "Chưa xác định"),
                        SinhVien = GetSafeString(reader, "SinhVien", "Chưa phân công"),
                        TenGiangVien = GetSafeString(reader, "GiangVien", "Chưa phân công"),
                        TrangThai = GetSafeString(reader, "TenTrangThai", "Chưa xác định"),
                        NgayBatDau = reader["NgayBatDau"] != DBNull.Value ? DateOnly.FromDateTime((DateTime)reader["NgayBatDau"]) : null,
                        NgayKetThuc = reader["NgayKetThuc"] != DBNull.Value ? DateOnly.FromDateTime((DateTime)reader["NgayKetThuc"]) : null,
                        DiemText = reader["Diem"] != DBNull.Value ? Convert.ToDecimal(reader["Diem"]).ToString("F2") : "Chưa có điểm"
                    });
                }
                
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi LayDanhSachDoAn: {ex.Message}");
                return new List<DoAnViewModel>();
            }
        }
        
        private string GetSafeString(System.Data.IDataReader reader, string columnName, string defaultValue = "")
        {
            try
            {
                var value = reader[columnName];
                return value == null || value == DBNull.Value ? defaultValue : value.ToString() ?? defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        public bool TaoDoAn(DoAn doAn, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using var context = new QuanLyDoAnContext();
                // Đảm bảo MaSv là NULL để tránh UNIQUE constraint violation
                doAn.MaSv = null;
                
                context.DoAns.Add(doAn);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException?.Message;
                errorMessage = "Lỗi khi tạo đồ án: " + ex.Message + (inner != null ? $" Chi tiết: {inner}" : string.Empty);
                return false;
            }
        }

        public bool CapNhatDoAn(DoAn doAn, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using var context = new QuanLyDoAnContext();
                var entity = context.DoAns.Find(doAn.MaDeTai);
                if (entity == null)
                {
                    errorMessage = "Đề tài không tồn tại";
                    return false;
                }

                entity.TenDeTai = doAn.TenDeTai;
                entity.MoTa = doAn.MoTa;
                entity.MaSv = doAn.MaSv;
                entity.MaGv = doAn.MaGv;
                entity.MaKy = doAn.MaKy;
                entity.NgayBatDau = doAn.NgayBatDau;
                entity.NgayKetThuc = doAn.NgayKetThuc;
                entity.MaTrangThai = doAn.MaTrangThai;
                entity.MaLoaiDoAn = doAn.MaLoaiDoAn;
                entity.Diem = doAn.Diem;

                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi khi cập nhật đồ án: " + ex.Message;
                return false;
            }
        }

        // Update only the LoaiDoAn for a given MaDeTai (use EF to avoid raw table name issues)
        public bool CapNhatLoaiDoAn(string maDeTai, string? maLoaiDoAn, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using var context = new QuanLyDoAnContext();
                var entity = context.DoAns.Find(maDeTai);
                if (entity == null)
                {
                    errorMessage = "Đề tài không tồn tại";
                    return false;
                }

                entity.MaLoaiDoAn = maLoaiDoAn;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi khi cập nhật loại đồ án: " + ex.Message;
                return false;
            }
        }

        public bool XoaDoAn(string maDeTai)
        {
            try
            {
                using var context = new QuanLyDoAnContext();
                context.Database.ExecuteSqlRaw(
                    "EXEC sp_XoaDoAn @MaDeTai",
                    new SqlParameter("@MaDeTai", maDeTai)
                );
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Kiểm tra quyền chấm điểm
        public bool KiemTraQuyenChamDiem(string maDeTai, string maGv, string maLoaiDanhGia, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using var context = new QuanLyDoAnContext();
                var doAn = context.DoAns
                    .Include(d => d.MaTrangThaiNavigation)
                    .Include(d => d.TienDos)
                    .FirstOrDefault(d => d.MaDeTai == maDeTai);

                if (doAn == null)
                {
                    errorMessage = "Đề tài không tồn tại";
                    return false;
                }

                // Kiểm tra trạng thái đồ án
                var trangThai = doAn.MaTrangThaiNavigation?.TenTrangThai;
                if (trangThai == "Đã hủy" || trangThai == "Tạm dừng")
                {
                    errorMessage = "Không thể chấm điểm đồ án đã hủy hoặc tạm dừng";
                    return false;
                }

                // Kiểm tra quyền giảng viên theo vai trò
                if (maLoaiDanhGia == "HD")
                {
                    // GVHD: kiểm tra từ cột MaGvhd
                    if (doAn.MaGv != maGv)
                    {
                        errorMessage = "Chỉ giảng viên hướng dẫn mới được chấm điểm hướng dẫn";
                        return false;
                    }
                }
                else
                {
                    // GVPB và HĐ: kiểm tra đã được phân công chưa (có bản ghi trong DanhGia)
                    var duocPhanCong = context.DanhGia.Any(d => 
                        d.MaDeTai == maDeTai && 
                        d.MaGv == maGv && 
                        d.MaLoaiDanhGia == maLoaiDanhGia);
                    
                    if (!duocPhanCong)
                    {
                        errorMessage = "Bạn chưa được phân công vai trò này";
                        return false;
                    }
                }

                // Kiểm tra tiến độ theo loại đánh giá
                if (!KiemTraTienDoChoPhepChamDiem(doAn, maLoaiDanhGia, out string loiTienDo))
                {
                    errorMessage = loiTienDo;
                    return false;
                }

                // Kiểm tra đã chấm điểm chưa (có điểm)
                var daChamDiem = context.DanhGia.Any(d => 
                    d.MaDeTai == maDeTai && 
                    d.MaGv == maGv && 
                    d.MaLoaiDanhGia == maLoaiDanhGia && 
                    d.DiemThanhPhan.HasValue);
                    
                if (daChamDiem)
                {
                    errorMessage = "Đã chấm điểm loại này rồi";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi kiểm tra quyền: " + ex.Message;
                return false;
            }
        }

        private bool KiemTraTienDoChoPhepChamDiem(DoAn doAn, string maLoaiDanhGia, out string errorMessage)
        {
            errorMessage = string.Empty;
            var tienDos = doAn.TienDos.OrderBy(t => t.NgayNop).ToList();
            var trangThai = doAn.MaTrangThaiNavigation?.TenTrangThai;

            switch (maLoaiDanhGia)
            {
                case "HD": // Hướng dẫn - có thể chấm khi có ít nhất 1 tiến độ
                    if (!tienDos.Any())
                    {
                        errorMessage = "Sinh viên chưa nộp tiến độ nào";
                        return false;
                    }
                    break;

                case "PB": // Phản biện - cần hoàn thành ít nhất 50% tiến độ
                    var tienDoHoanThanh = tienDos.Count(t => t.TrangThaiNop == "Đã nộp" || t.TrangThaiNop == "Đạt" || t.TrangThaiNop == "DungHan");
                    if (tienDos.Count == 0 || (tienDoHoanThanh * 100.0 / tienDos.Count) < 50)
                    {
                        errorMessage = "Cần hoàn thành ít nhất 50% tiến độ mới được phản biện";
                        return false;
                    }
                    break;

                case "HĐ": // Hội đồng - cần hoàn thành tất cả và trạng thái "Sẵn sàng bảo vệ"
                    if (trangThai != "Sẵn sàng bảo vệ" && trangThai != "Đã bảo vệ")
                    {
                        errorMessage = "Đồ án chưa sẵn sàng bảo vệ";
                        return false;
                    }
                    break;
            }

            return true;
        }

        // Admin: Nhập điểm trực tiếp (bỏ qua kiểm tra tiến độ)
        public bool NhapDiemTrucTiep(string maDeTai, decimal diem, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using var context = new QuanLyDoAnContext();
                var entity = context.DoAns.Find(maDeTai);
                if (entity == null)
                {
                    errorMessage = "Đề tài không tồn tại";
                    return false;
                }

                entity.Diem = diem;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi khi nhập điểm: " + ex.Message;
                return false;
            }
        }

        // Giảng viên: Chấm điểm theo tiêu chí (sử dụng stored procedures)
        public bool ChamDiemTheoTieuChi(string maDeTai, string maGv, string maLoaiDanhGia, List<(int maTieuChi, decimal diem, string? nhanXet)> chiTietDiem, out string errorMessage)
        {
            errorMessage = string.Empty;
            
            // Kiểm tra quyền và tiến độ trước khi chấm
            if (!KiemTraQuyenChamDiem(maDeTai, maGv, maLoaiDanhGia, out errorMessage))
                return false;

            try
            {
                using var context = new QuanLyDoAnContext();
                using var transaction = context.Database.BeginTransaction();

                // Bước 1: Tạo bản ghi DanhGia bằng stored procedure
                var maDanhGiaParam = new SqlParameter("@MaDanhGia", SqlDbType.Int) { Direction = ParameterDirection.Output };
                
                context.Database.ExecuteSqlRaw(
                    "EXEC sp_ThemDanhGia @MaDeTai, @MaGV, @MaLoaiDanhGia, @NhanXet, @MaDanhGia OUTPUT",
                    new SqlParameter("@MaDeTai", maDeTai),
                    new SqlParameter("@MaGV", maGv),
                    new SqlParameter("@MaLoaiDanhGia", maLoaiDanhGia),
                    new SqlParameter("@NhanXet", (object?)null ?? DBNull.Value),
                    maDanhGiaParam
                );

                int maDanhGia = (int)maDanhGiaParam.Value;

                // Bước 2: Thêm chi tiết đánh giá
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
                }
                context.SaveChanges();

                // Bước 3: Tính điểm thành phần bằng stored procedure
                context.Database.ExecuteSqlRaw(
                    "EXEC sp_TinhDiemThanhPhan @MaDanhGia",
                    new SqlParameter("@MaDanhGia", maDanhGia)
                );

                // Bước 4: Tính điểm tổng kết bằng stored procedure
                context.Database.ExecuteSqlRaw(
                    "EXEC sp_TinhDiemTongKet @MaDeTai",
                    new SqlParameter("@MaDeTai", maDeTai)
                );
                
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi khi chấm điểm: " + ex.Message;
                return false;
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

        public ChamDiemDoAnViewModel? LayThongTinChamDiem(string maDeTai)
        {
            try
            {
                using var context = new QuanLyDoAnContext();
                var doAn = context.DoAns
                    .Include(d => d.MaSvNavigation)
                    .Include(d => d.MaLoaiDoAnNavigation)
                    .FirstOrDefault(d => d.MaDeTai == maDeTai);

                if (doAn == null) return null;

                return new ChamDiemDoAnViewModel
                {
                    MaDeTai = doAn.MaDeTai,
                    TenDeTai = doAn.TenDeTai,
                    SinhVien = doAn.MaSvNavigation?.HoTen ?? "Chưa phân công",
                    MaLoaiDoAn = doAn.MaLoaiDoAn ?? "",
                    TieuChis = LayTieuChiTheoLoaiDoAn(doAn.MaLoaiDoAn),
                    LoaiDanhGias = LayDanhSachLoaiDanhGia()
                };
            }
            catch
            {
                return null;
            }
        }

        public DoAnViewModel? LayChiTietDoAn(string maDeTai)
        {
            try
            {
                using var context = new QuanLyDoAnContext();
                var doAn = context.DoAns
                    .Include(d => d.MaSvNavigation)
                    .Include(d => d.MaGvNavigation)
                    .Include(d => d.MaLoaiDoAnNavigation)
                    .Include(d => d.MaTrangThaiNavigation)
                    .FirstOrDefault(d => d.MaDeTai == maDeTai);

                if (doAn == null) return null;

                var danhGias = LayDanhGiaTheoDoAn(maDeTai);
                var danhGiaInfos = danhGias.Select(d => new DanhGiaInfo
                {
                    LoaiDanhGia = d.MaLoaiDanhGiaNavigation?.TenLoaiDanhGia ?? "",
                    GiangVien = d.MaGvNavigation?.HoTen ?? "",
                    Diem = d.DiemThanhPhan,
                    NgayDanhGia = d.NgayDanhGia
                }).ToList();

                return new DoAnViewModel
                {
                    MaDeTai = doAn.MaDeTai,
                    TenDeTai = doAn.TenDeTai,
                    SinhVien = doAn.MaSvNavigation?.HoTen ?? "Chưa phân công",
                    TenGiangVien = doAn.MaGvNavigation?.HoTen ?? "Chưa phân công",
                    LoaiDoAn = doAn.MaLoaiDoAnNavigation?.TenLoaiDoAn ?? "Chưa xác định",
                    MaLoaiDoAn = doAn.MaLoaiDoAn,
                    TrangThai = doAn.MaTrangThaiNavigation?.TenTrangThai ?? "Chưa xác định",
                    NgayBatDau = doAn.NgayBatDau,
                    NgayKetThuc = doAn.NgayKetThuc,
                    DiemText = doAn.Diem?.ToString("F2") ?? "Chưa có điểm",
                    DanhSachDanhGia = danhGiaInfos
                };
            }
            catch
            {
                return null;
            }
        }

        // Tính điểm tổng kết bằng stored procedure
        public bool TinhDiemTongKet(string maDeTai, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using var context = new QuanLyDoAnContext();
                context.Database.ExecuteSqlRaw(
                    "EXEC sp_TinhDiemTongKet @MaDeTai",
                    new SqlParameter("@MaDeTai", maDeTai)
                );
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi khi tính điểm tổng kết: " + ex.Message;
                return false;
            }
        }

        // Quản lý tiến độ
        public List<TienDo> LayTienDoDoAn(string maDeTai)
        {
            try
            {
                using var context = new QuanLyDoAnContext();
                return context.TienDos
                    .Where(t => t.MaDeTai == maDeTai)
                    .OrderBy(t => t.NgayNop)
                    .ToList();
            }
            catch
            {
                return new List<TienDo>();
            }
        }

        public string LayTrangThaiChamDiem(string maDeTai)
        {
            try
            {
                using var context = new QuanLyDoAnContext();
                var doAn = context.DoAns
                    .Include(d => d.MaTrangThaiNavigation)
                    .Include(d => d.TienDos)
                    .FirstOrDefault(d => d.MaDeTai == maDeTai);

                if (doAn == null) return "Không tìm thấy";

                var tienDos = doAn.TienDos.ToList();
                var trangThai = doAn.MaTrangThaiNavigation?.TenTrangThai ?? "";
                var tienDoHoanThanh = tienDos.Count(t => t.TrangThaiNop == "Đã nộp" || t.TrangThaiNop == "Đạt");
                var phanTramHoanThanh = tienDos.Count > 0 ? (tienDoHoanThanh * 100.0 / tienDos.Count) : 0;

                if (trangThai == "Sẵn sàng bảo vệ" || trangThai == "Đã bảo vệ")
                    return "Có thể chấm tất cả loại";
                else if (phanTramHoanThanh >= 80)
                    return "Có thể chấm hướng dẫn và phản biện";
                else if (tienDos.Any())
                    return "Chỉ có thể chấm hướng dẫn";
                else
                    return "Chưa thể chấm điểm";
            }
            catch
            {
                return "Lỗi kiểm tra";
            }
        }

        public List<string> LayDanhSachLoaiDanhGiaChoPhep(string maDeTai, string maGv)
        {
            var result = new List<string>();
            
            if (KiemTraQuyenChamDiem(maDeTai, maGv, "HD", out _))
                result.Add("HD");
            
            if (KiemTraQuyenChamDiem(maDeTai, maGv, "PB", out _))
                result.Add("PB");
            
            if (KiemTraQuyenChamDiem(maDeTai, maGv, "HĐ", out _))
                result.Add("HĐ");
            
            return result;
        }
    }
}