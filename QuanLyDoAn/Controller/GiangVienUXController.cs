using QuanLyDoAn.Model.Entities;
using QuanLyDoAn.Model.EF;
using QuanLyDoAn.Model.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace QuanLyDoAn.Controller
{
    public class GiangVienUXController
    {
        private readonly DoAnController _doAnController = new DoAnController();
        private readonly ChamDiemUXController _chamDiemController = new ChamDiemUXController();

        // Lấy danh sách đồ án thống nhất cho giảng viên
        public DanhSachDoAnGiangVienViewModel LayDanhSachDoAnChoGiangVien(string maGv)
        {
            try
            {
                using var context = new QuanLyDoAnContext();
                
                var giangVien = context.GiangViens.Find(maGv);
                if (giangVien == null) return new DanhSachDoAnGiangVienViewModel();

                // Đồ án hướng dẫn (kiểm tra cột MaGvhd)
                var doAnHuongDan = context.DoAns
                    .Include(d => d.MaSvNavigation)
                    .Include(d => d.MaLoaiDoAnNavigation)
                    .Include(d => d.MaTrangThaiNavigation)
                    .Include(d => d.TienDos)
                    .Where(d => d.MaGv == maGv)
                    .Select(d => TaoGiangVienDoAnViewModel(d, maGv))
                    .ToList();

                // Đồ án có thể chấm (phản biện, hội đồng) - đã được phân công
                var doAnDuocPhanCong = context.DanhGia
                    .Where(d => d.MaGv == maGv)
                    .Select(d => d.MaDeTai)
                    .Distinct()
                    .ToList();
                    
                var doAnCoTheCham = context.DoAns
                    .Include(d => d.MaSvNavigation)
                    .Include(d => d.MaLoaiDoAnNavigation)
                    .Include(d => d.MaTrangThaiNavigation)
                    .Include(d => d.TienDos)
                    .Where(d => doAnDuocPhanCong.Contains(d.MaDeTai) && d.MaGv != maGv)
                    .Select(d => TaoGiangVienDoAnViewModel(d, maGv))
                    .ToList();

                return new DanhSachDoAnGiangVienViewModel
                {
                    MaGiangVien = maGv,
                    TenGiangVien = giangVien.HoTen,
                    DoAnHuongDan = doAnHuongDan,
                    DoAnCoTheCham = doAnCoTheCham
                };
            }
            catch
            {
                return new DanhSachDoAnGiangVienViewModel();
            }
        }

        private GiangVienDoAnViewModel TaoGiangVienDoAnViewModel(DoAn doAn, string maGv)
        {
            var tienDoSummary = doAn.TienDos.Select(t => new TienDoSummary
            {
                GiaiDoan = t.GiaiDoan ?? "",
                NgayNop = t.NgayNop,
                TrangThaiNop = t.TrangThaiNop ?? ""
            }).ToList();

            var loaiDanhGiaStatus = LayTrangThaiLoaiDanhGia(doAn.MaDeTai, maGv);
            var trangThaiChamDiem = _doAnController.LayTrangThaiChamDiem(doAn.MaDeTai);

            return new GiangVienDoAnViewModel
            {
                MaDeTai = doAn.MaDeTai,
                TenDeTai = doAn.TenDeTai,
                SinhVien = doAn.MaSvNavigation?.HoTen ?? "Chưa phân công",
                LoaiDoAn = doAn.MaLoaiDoAnNavigation?.TenLoaiDoAn ?? "Chưa xác định",
                TrangThai = doAn.MaTrangThaiNavigation?.TenTrangThai ?? "Chưa xác định",
                NgayBatDau = doAn.NgayBatDau,
                NgayKetThuc = doAn.NgayKetThuc,
                DiemTongKet = doAn.Diem,
                TrangThaiChamDiem = trangThaiChamDiem,
                DanhSachLoaiDanhGia = loaiDanhGiaStatus,
                TienDoTomTat = tienDoSummary,
                CoTheXem = true,
                CoTheChamDiem = loaiDanhGiaStatus.Any(l => l.CoTheChams && !l.DaCham),
                GhiChuTrangThai = TaoGhiChuTrangThai(doAn, loaiDanhGiaStatus)
            };
        }

        private List<LoaiDanhGiaStatus> LayTrangThaiLoaiDanhGia(string maDeTai, string maGv)
        {
            try
            {
                using var context = new QuanLyDoAnContext();
                
                var doAn = context.DoAns.FirstOrDefault(d => d.MaDeTai == maDeTai);
                if (doAn == null) return new List<LoaiDanhGiaStatus>();
                
                var loaiDanhGias = context.LoaiDanhGias.ToList();
                var danhGiasDaLam = context.DanhGia
                    .Where(d => d.MaDeTai == maDeTai)
                    .ToList();
                
                // Lấy danh sách phân công cho đồ án này
                var phanCongChoDoAn = context.DanhGia
                    .Where(d => d.MaDeTai == maDeTai)
                    .Select(d => new { d.MaGv, d.MaLoaiDanhGia })
                    .ToList();

                return loaiDanhGias.Select(l => 
                {
                    bool duocPhanCong = false;
                    bool daCham = false;
                    decimal? diemDaCham = null;
                    DateOnly? ngayDanhGia = null;
                    string lyDoKhongCham = "";
                    
                    // Kiểm tra từng loại đánh giá
                    if (l.MaLoaiDanhGia == "HD")
                    {
                        // GVHD: kiểm tra từ cột MaGv trong bảng DoAn
                        duocPhanCong = doAn.MaGv == maGv;
                        if (!duocPhanCong)
                            lyDoKhongCham = "Bạn không phải GVHD của đồ án này";
                    }
                    else
                    {
                        // GVPB và HĐ: kiểm tra từ bảng DanhGia (phân công)
                        duocPhanCong = phanCongChoDoAn.Any(p => p.MaGv == maGv && p.MaLoaiDanhGia == l.MaLoaiDanhGia);
                        if (!duocPhanCong)
                            lyDoKhongCham = "Bạn chưa được phân công vai trò này";
                    }
                    
                    // Kiểm tra đã chấm chưa - CHỈ khi có DiemThanhPhan
                    var danhGiaCuaGV = danhGiasDaLam.FirstOrDefault(d => d.MaLoaiDanhGia == l.MaLoaiDanhGia && d.MaGv == maGv);
                    if (danhGiaCuaGV != null && danhGiaCuaGV.DiemThanhPhan.HasValue)
                    {
                        daCham = true;
                        diemDaCham = danhGiaCuaGV.DiemThanhPhan;
                        ngayDanhGia = danhGiaCuaGV.NgayDanhGia;
                        lyDoKhongCham = "Đã chấm điểm rồi";
                    }
                    
                    // Kiểm tra điều kiện chấm điểm
                    bool coTheChams = duocPhanCong && !daCham;
                    if (coTheChams)
                    {
                        coTheChams = _doAnController.KiemTraQuyenChamDiem(maDeTai, maGv, l.MaLoaiDanhGia, out string loi);
                        if (!coTheChams)
                            lyDoKhongCham = loi;
                    }
                    
                    return new LoaiDanhGiaStatus
                    {
                        MaLoai = l.MaLoaiDanhGia,
                        TenLoai = l.TenLoaiDanhGia,
                        TrongSo = l.TrongSoDiem,
                        DaCham = daCham,
                        CoTheChams = coTheChams,
                        DiemDaCham = diemDaCham,
                        NgayDanhGia = ngayDanhGia,
                        LyDoKhongCham = lyDoKhongCham
                    };
                }).ToList();
            }
            catch
            {
                return new List<LoaiDanhGiaStatus>();
            }
        }

        private string TaoGhiChuTrangThai(DoAn doAn, List<LoaiDanhGiaStatus> loaiDanhGiaStatus)
        {
            // Chỉ tính các loại đã được phân công
            var duocPhanCong = loaiDanhGiaStatus.Where(l => l.CoTheChams || l.DaCham).ToList();
            var daCham = duocPhanCong.Count(l => l.DaCham);
            var chuaCham = duocPhanCong.Count(l => !l.DaCham);
            
            if (duocPhanCong.Count == 0)
                return "Chưa thể chấm điểm";
            else if (chuaCham > 0)
                return $"Còn {chuaCham}/{duocPhanCong.Count} loại chưa chấm";
            else
                return "Đã hoàn thành chấm điểm";
        }

        // Lấy form chấm điểm nhanh
        public ChamDiemNhanhViewModel? LayFormChamDiemNhanh(string maDeTai, string maGv, string maLoaiDanhGia)
        {
            var form = _chamDiemController.LayFormChamDiem(maDeTai, maGv, maLoaiDanhGia);
            if (form == null) return null;

            return new ChamDiemNhanhViewModel
            {
                MaDeTai = form.MaDeTai,
                TenDeTai = form.TenDeTai,
                SinhVien = form.SinhVien,
                MaGv = form.MaGv,
                MaLoaiDanhGia = form.MaLoaiDanhGia,
                TenLoaiDanhGia = form.TenLoaiDanhGia,
                DanhSachTieuChi = form.TieuChis.Select(t => new TieuChiNhanh
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

        // Lưu kết quả chấm điểm nhanh
        public bool LuuChamDiemNhanh(ChamDiemNhanhViewModel model, out string errorMessage)
        {
            var form = new FormChamDiemViewModel
            {
                MaDeTai = model.MaDeTai,
                TenDeTai = model.TenDeTai,
                SinhVien = model.SinhVien,
                MaGv = model.MaGv,
                MaLoaiDanhGia = model.MaLoaiDanhGia,
                TenLoaiDanhGia = model.TenLoaiDanhGia,
                NhanXetChung = model.NhanXetChung,
                TieuChis = model.DanhSachTieuChi.Select(t => new TieuChiChamDiem
                {
                    MaTieuChi = t.MaTieuChi,
                    TenTieuChi = t.TenTieuChi,
                    MoTa = t.MoTa,
                    TrongSo = t.TrongSo,
                    DiemToiDa = t.DiemToiDa,
                    Diem = t.Diem,
                    NhanXet = t.NhanXet
                }).ToList()
            };

            return _chamDiemController.LuuKetQuaChamDiem(form, out errorMessage);
        }

        // Lấy chi tiết đồ án cho giảng viên
        public GiangVienDoAnViewModel? LayChiTietDoAn(string maDeTai, string maGv)
        {
            try
            {
                using var context = new QuanLyDoAnContext();
                var doAn = context.DoAns
                    .Include(d => d.MaSvNavigation)
                    .Include(d => d.MaLoaiDoAnNavigation)
                    .Include(d => d.MaTrangThaiNavigation)
                    .Include(d => d.TienDos)
                    .FirstOrDefault(d => d.MaDeTai == maDeTai);

                if (doAn == null) return null;

                return TaoGiangVienDoAnViewModel(doAn, maGv);
            }
            catch
            {
                return null;
            }
        }
    }
}