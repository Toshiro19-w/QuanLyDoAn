using System;
using System.Windows.Forms;
using System.Linq;
using QuanLyDoAn.Controller;
using QuanLyDoAn.Utils;

namespace QuanLyDoAn.View
{
    public partial class DangKyDoAnControl : UserControl
    {
        private DangKyDoAnController controller;

        public DangKyDoAnControl()
        {
            InitializeComponent();
            controller = new DangKyDoAnController();
            ThemeHelper.ApplyTheme(this);
            LoadData();
        }

        private void LoadData()
        {
            // Load danh sách đề tài chưa có sinh viên
            var doAns = controller.LayDoAnChuaCoSinhVien();
            var displayData = doAns.Select(d => new
            {
                d.MaDeTai,
                d.TenDeTai,
                d.MoTa,
                GiangVien = d.MaGvNavigation?.HoTen ?? "",
                LoaiDoAn = d.MaLoaiDoAnNavigation?.TenLoaiDoAn ?? "",
                TrangThai = d.MaTrangThaiNavigation?.TenTrangThai ?? "",
                d.NgayBatDau,
                d.NgayKetThuc
            }).ToList();

            dgvDoAn.DataSource = displayData;

            if (dgvDoAn.Columns["MaDeTai"] != null)
                dgvDoAn.Columns["MaDeTai"].Visible = false;
            if (dgvDoAn.Columns["TenDeTai"] != null)
                dgvDoAn.Columns["TenDeTai"].HeaderText = "Tên đề tài";
            if (dgvDoAn.Columns["MoTa"] != null)
                dgvDoAn.Columns["MoTa"].HeaderText = "Mô tả";
            if (dgvDoAn.Columns["GiangVien"] != null)
                dgvDoAn.Columns["GiangVien"].HeaderText = "Giảng viên";
            if (dgvDoAn.Columns["LoaiDoAn"] != null)
                dgvDoAn.Columns["LoaiDoAn"].HeaderText = "Loại đồ án";
            if (dgvDoAn.Columns["TrangThai"] != null)
                dgvDoAn.Columns["TrangThai"].HeaderText = "Trạng thái";
            if (dgvDoAn.Columns["NgayBatDau"] != null)
                dgvDoAn.Columns["NgayBatDau"].HeaderText = "Ngày bắt đầu";
            if (dgvDoAn.Columns["NgayKetThuc"] != null)
                dgvDoAn.Columns["NgayKetThuc"].HeaderText = "Ngày kết thúc";
        }

        private void BtnDangKy_Click(object sender, EventArgs e)
        {
            if (dgvDoAn.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn đề tài!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var maDeTai = dgvDoAn.CurrentRow.Cells["MaDeTai"].Value?.ToString();
            var maSv = UserSession.CurrentUser?.MaSv;

            if (string.IsNullOrEmpty(maDeTai) || string.IsNullOrEmpty(maSv))
                return;

            // Kiểm tra đã gửi yêu cầu chưa
            var yeuCaus = controller.LayYeuCauCuaSinhVien(maSv);
            var existing = yeuCaus.FirstOrDefault(y => y.MaDeTai == maDeTai && y.TrangThai == "Pending");
            
            if (existing != null)
            {
                MessageBox.Show("Bạn đã gửi yêu cầu cho đề tài này rồi. Vui lòng chờ giảng viên duyệt.", 
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (controller.GuiYeuCauDangKy(maDeTai, maSv, txtGhiChu.Text))
            {
                MessageBox.Show("Gửi yêu cầu đăng ký thành công!\n\nChờ giảng viên duyệt...", 
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGhiChu.Clear();
                LoadData();
            }
            else
            {
                MessageBox.Show("Gửi yêu cầu thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
