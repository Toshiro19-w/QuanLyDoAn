using System;
using System.Windows.Forms;
using System.Linq;
using QuanLyDoAn.Controller;
using QuanLyDoAn.Utils;

namespace QuanLyDoAn.View
{
    public partial class DuyetYeuCauControl : UserControl
    {
        private DangKyDoAnController controller;

        public DuyetYeuCauControl()
        {
            InitializeComponent();
            controller = new DangKyDoAnController();
            ThemeHelper.ApplyTheme(this);
            LoadData();
        }

        private void LoadData()
        {
            var maGv = UserSession.CurrentUser?.MaGv;
            if (string.IsNullOrEmpty(maGv)) return;

            var yeuCaus = controller.LayYeuCauTheoGiangVien(maGv);
            var displayData = yeuCaus.Select(y => new
            {
                y.MaYeuCau,
                TenDeTai = y.MaDeTaiNavigation?.TenDeTai ?? "",
                SinhVien = y.MaSvNavigation?.HoTen ?? "",
                MaSv = y.MaSvNavigation?.MaSv ?? "",
                Lop = y.MaSvNavigation?.Lop ?? "",
                Email = y.MaSvNavigation?.Email ?? "",
                y.NgayGui,
                y.GhiChu
            }).ToList();

            dgvYeuCau.DataSource = displayData;

            if (dgvYeuCau.Columns["MaYeuCau"] != null)
                dgvYeuCau.Columns["MaYeuCau"].Visible = false;
            if (dgvYeuCau.Columns["TenDeTai"] != null)
                dgvYeuCau.Columns["TenDeTai"].HeaderText = "Đề tài";
            if (dgvYeuCau.Columns["SinhVien"] != null)
                dgvYeuCau.Columns["SinhVien"].HeaderText = "Sinh viên";
            if (dgvYeuCau.Columns["MaSv"] != null)
                dgvYeuCau.Columns["MaSv"].HeaderText = "Mã SV";
            if (dgvYeuCau.Columns["Lop"] != null)
                dgvYeuCau.Columns["Lop"].HeaderText = "Lớp";
            if (dgvYeuCau.Columns["Email"] != null)
                dgvYeuCau.Columns["Email"].HeaderText = "Email";
            if (dgvYeuCau.Columns["NgayGui"] != null)
                dgvYeuCau.Columns["NgayGui"].HeaderText = "Ngày gửi";
            if (dgvYeuCau.Columns["GhiChu"] != null)
                dgvYeuCau.Columns["GhiChu"].HeaderText = "Ghi chú";
        }

        private void BtnChapNhan_Click(object sender, EventArgs e)
        {
            if (dgvYeuCau.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn yêu cầu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var maYeuCau = Convert.ToInt32(dgvYeuCau.CurrentRow.Cells["MaYeuCau"].Value);
            var tenDeTai = dgvYeuCau.CurrentRow.Cells["TenDeTai"].Value?.ToString() ?? "";
            var tenSv = dgvYeuCau.CurrentRow.Cells["SinhVien"].Value?.ToString() ?? "";
            
            var result = MessageBox.Show(
                $"Bạn có chắc muốn chấp nhận yêu cầu này?\n\n" +
                $"📌 Đề tài: {tenDeTai}\n" +
                $"👤 Sinh viên: {tenSv}\n\n" +
                $"Các yêu cầu khác cho đề tài này sẽ bị từ chối tự động!", 
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (controller.DuyetYeuCau(maYeuCau, true))
                {
                    MessageBox.Show(
                        $"✅ Đã chấp nhận yêu cầu!\n\n" +
                        $"✓ Sinh viên: {tenSv} được gán vào đề tài\n" +
                        $"✓ Các yêu cầu khác bị từ chối\n" +
                        $"✓ Đề tài sẽ biến mất khỏi danh sách", 
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData(); // Làm mới - yêu cầu sẽ biến mất
                }
                else
                {
                    MessageBox.Show("❌ Duyệt yêu cầu thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnTuChoi_Click(object sender, EventArgs e)
        {
            if (dgvYeuCau.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn yêu cầu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var maYeuCau = Convert.ToInt32(dgvYeuCau.CurrentRow.Cells["MaYeuCau"].Value);
            var tenDeTai = dgvYeuCau.CurrentRow.Cells["TenDeTai"].Value?.ToString() ?? "";
            var tenSv = dgvYeuCau.CurrentRow.Cells["SinhVien"].Value?.ToString() ?? "";
            
            var result = MessageBox.Show(
                $"Bạn có chắc muốn từ chối yêu cầu này?\n\n" +
                $"📌 Đề tài: {tenDeTai}\n" +
                $"👤 Sinh viên: {tenSv}\n\n" +
                $"⚠️ Đề tài sẽ vẫn mở cho sinh viên khác đăng ký!", 
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (controller.DuyetYeuCau(maYeuCau, false))
                {
                    MessageBox.Show(
                        $"✅ Đã từ chối yêu cầu!\n\n" +
                        $"✓ Sinh viên: {tenSv} bị từ chối\n" +
                        $"✓ Đề tài vẫn mở cho sinh viên khác", 
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData(); // Làm mới
                }
                else
                {
                    MessageBox.Show("❌ Từ chối yêu cầu thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
