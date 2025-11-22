using System;
using System.Windows.Forms;
using QuanLyDoAn.Controller;
using QuanLyDoAn.Model.Entities;
using QuanLyDoAn.Utils;

namespace QuanLyDoAn.View
{
    public partial class DoAnSinhVienControl : UserControl
    {
        private SinhVienController sinhVienController;

        public DoAnSinhVienControl()
        {
            InitializeComponent();
            sinhVienController = new SinhVienController();
            ThemeHelper.ApplyTheme(this);
            LoadData();
        }

        private void LoadData()
        {
            var maSv = UserSession.CurrentUser?.MaSv;
            if (string.IsNullOrEmpty(maSv)) return;

            var doAn = sinhVienController.LayDoAnCuaSinhVien(maSv);
            if (doAn != null)
            {
                // Hiển thị thông tin đồ án
                lblTenDeTai.Text = doAn.TenDeTai;
                lblMoTa.Text = doAn.MoTa ?? "Chưa có mô tả";
                lblGiangVien.Text = doAn.MaGvNavigation?.HoTen ?? "Chưa phân công";
                lblLoaiDoAn.Text = doAn.MaLoaiDoAnNavigation?.TenLoaiDoAn ?? "Chưa xác định";
                lblTrangThai.Text = doAn.MaTrangThaiNavigation?.TenTrangThai ?? "Chưa xác định";
                lblNgayBatDau.Text = doAn.NgayBatDau?.ToString("dd/MM/yyyy") ?? "Chưa xác định";
                lblNgayKetThuc.Text = doAn.NgayKetThuc?.ToString("dd/MM/yyyy") ?? "Chưa xác định";
                lblDiem.Text = doAn.Diem?.ToString("F1") ?? "Chưa chấm";

                // Load tiến độ
                LoadTienDo(doAn.MaDeTai);
                
                // Load thông báo
                LoadThongBao(doAn.MaDeTai);
            }
            else
            {
                lblTenDeTai.Text = "Chưa được phân công đồ án";
                lblMoTa.Text = "";
                lblGiangVien.Text = "";
                lblLoaiDoAn.Text = "";
                lblTrangThai.Text = "";
                lblNgayBatDau.Text = "";
                lblNgayKetThuc.Text = "";
                lblDiem.Text = "";
                dgvThongBao.DataSource = null;
            }
        }

        private void LoadTienDo(string maDeTai)
        {
            var tienDos = sinhVienController.LayTienDoDoAn(maDeTai);
            dgvTienDo.DataSource = tienDos;

            // Ẩn cột navigation và mã
            if (dgvTienDo.Columns["MaDeTaiNavigation"] != null)
                dgvTienDo.Columns["MaDeTaiNavigation"].Visible = false;
            if (dgvTienDo.Columns["MaTienDo"] != null)
                dgvTienDo.Columns["MaTienDo"].Visible = false;
            if (dgvTienDo.Columns["MaDeTai"] != null)
                dgvTienDo.Columns["MaDeTai"].Visible = false;

            // Đặt tên cột tiếng Việt
            if (dgvTienDo.Columns["GiaiDoan"] != null)
                dgvTienDo.Columns["GiaiDoan"].HeaderText = "Giai đoạn";
            if (dgvTienDo.Columns["NgayNop"] != null)
                dgvTienDo.Columns["NgayNop"].HeaderText = "Ngày nộp";
            if (dgvTienDo.Columns["NhanXet"] != null)
                dgvTienDo.Columns["NhanXet"].HeaderText = "Nhận xét GV";
            if (dgvTienDo.Columns["TrangThaiNop"] != null)
                dgvTienDo.Columns["TrangThaiNop"].HeaderText = "Trạng thái";
        }

        private void LoadThongBao(string maDeTai)
        {
            try
            {
                var thongBaos = sinhVienController.LayThongBaoDoAn(maDeTai);
            
            // Tạo danh sách hiển thị với các cột tách riêng
            var displayData = thongBaos.Select(tb => 
            {
                var noiDung = tb.NoiDung ?? "";
                var lines = noiDung.Split('\n');
                
                string tieuDe = "Không có tiêu đề";
                string noiDungChinh = "Không có nội dung";
                string nguoiGui = "Không xác định";
                
                // Tách thông tin từ nội dung
                foreach (var line in lines)
                {
                    if (line.StartsWith("Tiêu đề:"))
                        tieuDe = line.Replace("Tiêu đề:", "").Trim();
                    else if (line.StartsWith("Nội dung:"))
                        noiDungChinh = line.Replace("Nội dung:", "").Trim();
                    else if (line.StartsWith("Người gửi:"))
                        nguoiGui = line.Replace("Người gửi:", "").Trim();
                }
                
                return new
                {
                    TieuDe = string.IsNullOrEmpty(tieuDe) || tieuDe == "Không có tiêu đề" ? 
                        (noiDung.Length > 30 ? noiDung.Substring(0, 30) + "..." : noiDung) : tieuDe,
                    NoiDung = string.IsNullOrEmpty(noiDungChinh) || noiDungChinh == "Không có nội dung" ? 
                        "Xem chi tiết" : (noiDungChinh.Length > 50 ? noiDungChinh.Substring(0, 50) + "..." : noiDungChinh),
                    NguoiGui = nguoiGui,
                    NgayGui = tb.NgayGui?.ToString("dd/MM/yyyy") ?? "Chưa xác định",
                    NoiDungDayDu = noiDung
                };
            }).ToList();
            
            dgvThongBao.DataSource = displayData;

            // Cấu hình cột với kiểm tra an toàn
            if (dgvThongBao.Columns?.Count > 0)
            {
                try
                {
                    if (dgvThongBao.Columns["TieuDe"] != null)
                    {
                        dgvThongBao.Columns["TieuDe"].HeaderText = "Tiêu đề";
                    }
                    if (dgvThongBao.Columns["NoiDung"] != null)
                    {
                        dgvThongBao.Columns["NoiDung"].HeaderText = "Nội dung";
                        dgvThongBao.Columns["NoiDung"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                    if (dgvThongBao.Columns["NguoiGui"] != null)
                    {
                        dgvThongBao.Columns["NguoiGui"].HeaderText = "Người gửi";
                        dgvThongBao.Columns["NguoiGui"].Width = 120;
                    }
                    if (dgvThongBao.Columns["NgayGui"] != null)
                    {
                        dgvThongBao.Columns["NgayGui"].HeaderText = "Ngày gửi";
                        dgvThongBao.Columns["NgayGui"].Width = 100;
                    }
                    if (dgvThongBao.Columns["NoiDungDayDu"] != null)
                    {
                        dgvThongBao.Columns["NoiDungDayDu"].Visible = false;
                    }
                }
                catch { }
            }

                // Thêm event handler cho double click
                dgvThongBao.CellDoubleClick -= dgvThongBao_CellDoubleClick;
                dgvThongBao.CellDoubleClick += dgvThongBao_CellDoubleClick;
            }
            catch (Exception ex)
            {
                // Nếu có lỗi, hiển thị thông báo trống
                dgvThongBao.DataSource = new List<object>();
                MessageBox.Show($"Lỗi khi tải thông báo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvThongBao_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var tieuDe = dgvThongBao.Rows[e.RowIndex].Cells["TieuDe"].Value?.ToString();
                var noiDungDayDu = dgvThongBao.Rows[e.RowIndex].Cells["NoiDungDayDu"].Value?.ToString();
                var nguoiGui = dgvThongBao.Rows[e.RowIndex].Cells["NguoiGui"].Value?.ToString();
                var ngayGui = dgvThongBao.Rows[e.RowIndex].Cells["NgayGui"].Value?.ToString();
                
                // Tách và hiển thị thông tin một cách có cấu trúc
                var lines = noiDungDayDu?.Split('\n') ?? new string[0];
                string noiDungChinh = "";
                
                foreach (var line in lines)
                {
                    if (line.StartsWith("Nội dung:"))
                    {
                        noiDungChinh = line.Replace("Nội dung:", "").Trim();
                        break;
                    }
                }
                
                if (string.IsNullOrEmpty(noiDungChinh))
                    noiDungChinh = noiDungDayDu;
                
                var chiTiet = $"Tiêu đề: {tieuDe}\n\n" +
                             $"Nội dung:\n{noiDungChinh}\n\n" +
                             $"Người gửi: {nguoiGui}\n" +
                             $"Ngày gửi: {ngayGui}";
                
                MessageBox.Show(chiTiet, "Chi tiết thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}