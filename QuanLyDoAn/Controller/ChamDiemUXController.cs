using QuanLyDoAn.Model.Entities;
using QuanLyDoAn.Model.EF;
using QuanLyDoAn.Model.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace QuanLyDoAn.Controller
{
    public class ChamDiemUXController
    {
        private readonly DoAnController _doAnController = new DoAnController();

        // Admin UX
        public List<AdminChamDiemViewModel> LayDanhSachDoAnChoAdmin()
        {
            try
            {
                using var context = new QuanLyDoAnContext();
                return context.DoAns
                    .Include(d => d.MaSvNavigation)
                    .Include(d => d.MaTrangThaiNavigation)
                    .Include(d => d.DanhGia)
                        .ThenInclude(dg => dg.MaLoaiDanhGiaNavigation)
                    .Select(d => new AdminChamDiemViewModel
                    {
                        MaDeTai = d.MaDeTai,
                        TenDeTai = d.TenDeTai,
                        SinhVien = d.MaSvNavigation != null ? d.MaSvNavigation.HoTen : "Chưa phân công",
                        TrangThai = d.MaTrangThaiNavigation != null ? d.MaTrangThaiNavigation.TenTrangThai : "Chưa xác định",
                        DiemHienTai = d.Diem,
                        CoChamDiemChiTiet = d.DanhGia.Any(),
                        DanhSachDanhGia = d.DanhGia.Select(dg => new DanhGiaInfo
                        {
                            LoaiDanhGia = dg.MaLoaiDanhGiaNavigation != null ? dg.MaLoaiDanhGiaNavigation.TenLoaiDanhGia : "",
                            GiangVien = dg.MaGvNavigation != null ? dg.MaGvNavigation.HoTen : "",
                            Diem = dg.DiemThanhPhan,
                            NgayDanhGia = dg.NgayDanhGia
                        }).ToList()
                    })
                    .ToList();
            }
            catch
            {
                return new List<AdminChamDiemViewModel>();
            }
        }

        public AdminChamDiemViewModel? LayChiTietDoAnChoAdmin(string maDeTai)
        {
            try
            {
                using var context = new QuanLyDoAnContext();
                var doAn = context.DoAns
                    .Include(d => d.MaSvNavigation)
                    .Include(d => d.MaTrangThaiNavigation)
                    .Include(d => d.DanhGia)
                        .ThenInclude(dg => dg.MaLoaiDanhGiaNavigation)
                    .Include(d => d.DanhGia)
                        .ThenInclude(dg => dg.MaGvNavigation)
                    .FirstOrDefault(d => d.MaDeTai == maDeTai);

                if (doAn == null) return null;

                return new AdminChamDiemViewModel
                {
                    MaDeTai = doAn.MaDeTai,
                    TenDeTai = doAn.TenDeTai,
                    SinhVien = doAn.MaSvNavigation?.HoTen ?? "Chưa phân công",
                    TrangThai = doAn.MaTrangThaiNavigation?.TenTrangThai ?? "Chưa xác định",
                    DiemHienTai = doAn.Diem,
                    CoChamDiemChiTiet = doAn.DanhGia.Any(),
                    DanhSachDanhGia = doAn.DanhGia.Select(dg => new DanhGiaInfo
                    {
                        LoaiDanhGia = dg.MaLoaiDanhGiaNavigation?.TenLoaiDanhGia ?? "",
                        GiangVien = dg.MaGvNavigation?.HoTen ?? "",
                        Diem = dg.DiemThanhPhan,
                        NgayDanhGia = dg.NgayDanhGia
                    }).ToList()
                };
            }
            catch
            {
                return null;
            }
        }

        // Giảng viên UX
        public List<GiangVienChamDiemViewModel> LayDanhSachDoAnChoGiangVien(string maGv)
        {
            try
            {
                using var context = new QuanLyDoAnContext();
                var doAns = context.DoAns
                    .Include(d => d.MaSvNavigation)
                    .Include(d => d.MaTrangThaiNavigation)
                    .Include(d => d.TienDos)
                    .Include(d => d.DanhGia)
                        .ThenInclude(dg => dg.MaLoaiDanhGiaNavigation)
                    .Where(d => d.MaGv == maGv || context.DanhGia.Any(dg => dg.MaDeTai == d.MaDeTai))
                    .ToList();

                return doAns.Select(d => new GiangVienChamDiemViewModel
                {
                    MaDeTai = d.MaDeTai,
                    TenDeTai = d.TenDeTai,
                    SinhVien = d.MaSvNavigation?.HoTen ?? "Chưa phân công",
                    TrangThaiChamDiem = _doAnController.LayTrangThaiChamDiem(d.MaDeTai),
                    LoaiChoPhep = LayLoaiDanhGiaChoPhep(d.MaDeTai, maGv),
                    TienDos = d.TienDos.Select(t => new TienDoInfo
                    {
                        GiaiDoan = t.GiaiDoan ?? "",
                        NgayNop = t.NgayNop,
                        TrangThaiNop = t.TrangThaiNop ?? "",
                        NhanXet = t.NhanXet
                    }).ToList(),
                    DanhGiaDaLam = d.DanhGia.Where(dg => dg.MaGv == maGv).Select(dg => new DanhGiaInfo
                    {
                        LoaiDanhGia = dg.MaLoaiDanhGiaNavigation?.TenLoaiDanhGia ?? "",
                        GiangVien = maGv,
                        Diem = dg.DiemThanhPhan,
                        NgayDanhGia = dg.NgayDanhGia
                    }).ToList()
                }).ToList();
            }
            catch
            {
                return new List<GiangVienChamDiemViewModel>();
            }
        }

        private List<LoaiDanhGiaChoPhep> LayLoaiDanhGiaChoPhep(string maDeTai, string maGv)
        {
            var loaiDanhGias = _doAnController.LayDanhSachLoaiDanhGia();
            return loaiDanhGias.Select(l => new LoaiDanhGiaChoPhep
            {
                MaLoai = l.MaLoaiDanhGia,
                TenLoai = l.TenLoaiDanhGia,
                ChoPhep = _doAnController.KiemTraQuyenChamDiem(maDeTai, maGv, l.MaLoaiDanhGia, out string loi),
                LyDoKhongChoPhep = loi
            }).ToList();
        }

        // Form chấm điểm
        public FormChamDiemViewModel? LayFormChamDiem(string maDeTai, string maGv, string maLoaiDanhGia)
        {
            if (!_doAnController.KiemTraQuyenChamDiem(maDeTai, maGv, maLoaiDanhGia, out _))
                return null;

            try
            {
                using var context = new QuanLyDoAnContext();
                var doAn = context.DoAns
                    .Include(d => d.MaSvNavigation)
                    .Include(d => d.MaLoaiDoAnNavigation)
                    .FirstOrDefault(d => d.MaDeTai == maDeTai);

                if (doAn == null) return null;

                var loaiDanhGia = context.LoaiDanhGias.Find(maLoaiDanhGia);
                var tieuChis = _doAnController.LayTieuChiTheoLoaiDoAn(doAn.MaLoaiDoAn);

                return new FormChamDiemViewModel
                {
                    MaDeTai = doAn.MaDeTai,
                    TenDeTai = doAn.TenDeTai,
                    SinhVien = doAn.MaSvNavigation?.HoTen ?? "Chưa phân công",
                    MaGv = maGv,
                    MaLoaiDanhGia = maLoaiDanhGia,
                    TenLoaiDanhGia = loaiDanhGia?.TenLoaiDanhGia ?? "",
                    TieuChis = tieuChis.Select(t => new TieuChiChamDiem
                    {
                        MaTieuChi = t.MaTieuChi,
                        TenTieuChi = t.TenTieuChi,
                        MoTa = t.MoTa,
                        TrongSo = t.TrongSo,
                        DiemToiDa = t.DiemToiDa,
                        Diem = 0
                    }).ToList()
                };
            }
            catch
            {
                return null;
            }
        }

        // Lưu kết quả chấm điểm (sử dụng stored procedures)
        public bool LuuKetQuaChamDiem(FormChamDiemViewModel form, out string errorMessage)
        {
            errorMessage = string.Empty;
            
            // Kiểm tra quyền trước khi chấm
            if (!_doAnController.KiemTraQuyenChamDiem(form.MaDeTai, form.MaGv, form.MaLoaiDanhGia, out errorMessage))
                return false;

            try
            {
                using var context = new QuanLyDoAnContext();
                using var transaction = context.Database.BeginTransaction();

                // Bước 1: Lấy hoặc tạo DanhGia
                var danhGia = context.DanhGia.FirstOrDefault(d => 
                    d.MaDeTai == form.MaDeTai && 
                    d.MaGv == form.MaGv && 
                    d.MaLoaiDanhGia == form.MaLoaiDanhGia);
                
                int maDanhGia;
                if (danhGia != null)
                {
                    // Đã có bản ghi từ phân công, cập nhật
                    maDanhGia = danhGia.MaDanhGia;
                    danhGia.NhanXet = form.NhanXetChung;
                    danhGia.NgayDanhGia = DateOnly.FromDateTime(DateTime.Now);
                    context.SaveChanges();
                }
                else
                {
                    // Tạo mới (trường hợp GVHD)
                    var maDanhGiaParam = new Microsoft.Data.SqlClient.SqlParameter("@MaDanhGia", System.Data.SqlDbType.Int) 
                    { 
                        Direction = System.Data.ParameterDirection.Output 
                    };
                    
                    context.Database.ExecuteSqlRaw(
                        "EXEC sp_ThemDanhGia @MaDeTai, @MaGV, @MaLoaiDanhGia, @NhanXet, @MaDanhGia OUTPUT",
                        new Microsoft.Data.SqlClient.SqlParameter("@MaDeTai", form.MaDeTai),
                        new Microsoft.Data.SqlClient.SqlParameter("@MaGV", form.MaGv),
                        new Microsoft.Data.SqlClient.SqlParameter("@MaLoaiDanhGia", form.MaLoaiDanhGia),
                        new Microsoft.Data.SqlClient.SqlParameter("@NhanXet", (object?)form.NhanXetChung ?? DBNull.Value),
                        maDanhGiaParam
                    );
                    maDanhGia = (int)maDanhGiaParam.Value;
                }

                // Bước 2: Thêm chi tiết đánh giá
                foreach (var tieuChi in form.TieuChis)
                {
                    context.Database.ExecuteSqlRaw(
                        "EXEC sp_ThemChiTietDanhGia @MaDanhGia, @MaTieuChi, @Diem, @NhanXet",
                        new Microsoft.Data.SqlClient.SqlParameter("@MaDanhGia", maDanhGia),
                        new Microsoft.Data.SqlClient.SqlParameter("@MaTieuChi", tieuChi.MaTieuChi),
                        new Microsoft.Data.SqlClient.SqlParameter("@Diem", tieuChi.Diem),
                        new Microsoft.Data.SqlClient.SqlParameter("@NhanXet", (object?)tieuChi.NhanXet ?? DBNull.Value)
                    );
                }

                // Bước 3: Tính điểm thành phần
                context.Database.ExecuteSqlRaw(
                    "EXEC sp_TinhDiemThanhPhan @MaDanhGia",
                    new Microsoft.Data.SqlClient.SqlParameter("@MaDanhGia", maDanhGia)
                );

                // Bước 4: Tính điểm tổng kết
                context.Database.ExecuteSqlRaw(
                    "EXEC sp_TinhDiemTongKet @MaDeTai",
                    new Microsoft.Data.SqlClient.SqlParameter("@MaDeTai", form.MaDeTai)
                );
                
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi khi lưu kết quả chấm điểm: " + ex.Message;
                return false;
            }
        }

        // Xem kết quả chi tiết
        public KetQuaChamDiemViewModel? LayKetQuaChiTiet(string maDeTai)
        {
            try
            {
                using var context = new QuanLyDoAnContext();
                var doAn = context.DoAns
                    .Include(d => d.MaSvNavigation)
                    .Include(d => d.DanhGia)
                        .ThenInclude(dg => dg.MaLoaiDanhGiaNavigation)
                    .Include(d => d.DanhGia)
                        .ThenInclude(dg => dg.MaGvNavigation)
                    .Include(d => d.DanhGia)
                        .ThenInclude(dg => dg.ChiTietDanhGias)
                            .ThenInclude(ct => ct.MaTieuChiNavigation)
                    .FirstOrDefault(d => d.MaDeTai == maDeTai);

                if (doAn == null) return null;

                var danhGias = doAn.DanhGia.ToList();
                var tongTrongSo = danhGias.Sum(d => d.MaLoaiDanhGiaNavigation?.TrongSoDiem ?? 0);

                return new KetQuaChamDiemViewModel
                {
                    MaDeTai = doAn.MaDeTai,
                    TenDeTai = doAn.TenDeTai,
                    SinhVien = doAn.MaSvNavigation?.HoTen ?? "Chưa phân công",
                    DiemTongKet = doAn.Diem,
                    TrangThaiHoanThanh = tongTrongSo >= 100 ? "Hoàn thành" : $"Còn thiếu {100 - tongTrongSo}%",
                    ChiTietDanhGias = danhGias.Select(dg => new ChiTietKetQuaDanhGia
                    {
                        LoaiDanhGia = dg.MaLoaiDanhGiaNavigation?.TenLoaiDanhGia ?? "",
                        GiangVien = dg.MaGvNavigation?.HoTen ?? "",
                        DiemThanhPhan = dg.DiemThanhPhan,
                        TrongSo = dg.MaLoaiDanhGiaNavigation?.TrongSoDiem ?? 0,
                        NgayDanhGia = dg.NgayDanhGia,
                        ChiTietTieuChis = dg.ChiTietDanhGias.Select(ct => new ChiTietTieuChi
                        {
                            TenTieuChi = ct.MaTieuChiNavigation?.TenTieuChi ?? "",
                            Diem = ct.Diem,
                            NhanXet = ct.NhanXet
                        }).ToList()
                    }).ToList()
                };
            }
            catch
            {
                return null;
            }
        }
    }
}