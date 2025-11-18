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
            var thongBaos = sinhVienController.LayThongBaoDoAn(maDeTai);
            dgvThongBao.DataSource = thongBaos;

            // Ẩn cột navigation và mã
            if (dgvThongBao.Columns["MaDeTaiNavigation"] != null)
                dgvThongBao.Columns["MaDeTaiNavigation"].Visible = false;
            if (dgvThongBao.Columns["MaThongBao"] != null)
                dgvThongBao.Columns["MaThongBao"].Visible = false;
            if (dgvThongBao.Columns["MaDeTai"] != null)
                dgvThongBao.Columns["MaDeTai"].Visible = false;

            // Đặt tên cột tiếng Việt
            if (dgvThongBao.Columns["NoiDung"] != null)
                dgvThongBao.Columns["NoiDung"].HeaderText = "Nội dung";
            if (dgvThongBao.Columns["NgayGui"] != null)
                dgvThongBao.Columns["NgayGui"].HeaderText = "Ngày gửi";
        }
    }
}