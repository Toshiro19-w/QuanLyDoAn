using System;
using System.Windows.Forms;
using System.Linq;
using QuanLyDoAn.Controller;
using QuanLyDoAn.Utils;

namespace QuanLyDoAn.View
{
    public partial class LichSuYeuCauControl : UserControl
    {
        private DangKyDoAnController controller;

        public LichSuYeuCauControl()
        {
            InitializeComponent();
            controller = new DangKyDoAnController();
            ThemeHelper.ApplyTheme(this);
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var maSv = UserSession.CurrentUser?.MaSv ?? "";
                if (string.IsNullOrEmpty(maSv)) return;

                var yeuCaus = controller.LayYeuCauCuaSinhVien(maSv);
                
                if (yeuCaus.Count == 0)
                {
                    lblThongTin.Text = "Chưa có yêu cầu nào";
                    dgvLichSu.DataSource = null;
                    return;
                }

                var displayData = yeuCaus.Select(y => new
                {
                    y.MaYeuCau,
                    y.MaDeTai,
                    TenDeTai = y.MaDeTaiNavigation?.TenDeTai ?? "",
                    GiangVien = y.MaDeTaiNavigation?.MaGvNavigation?.HoTen ?? "N/A",
                    TrangThaiCode = y.TrangThai, // Lưu code gốc
                    TrangThaiDisplay = GetTrangThaiDisplay(y.TrangThai), // Hiển thị với emoji
                    y.NgayGui,
                    y.GhiChu
                }).ToList();

                dgvLichSu.DataSource = displayData;

                // Configure columns
                if (dgvLichSu.Columns["MaYeuCau"] != null)
                    dgvLichSu.Columns["MaYeuCau"].Visible = false;
                if (dgvLichSu.Columns["MaDeTai"] != null)
                    dgvLichSu.Columns["MaDeTai"].Visible = false;
                if (dgvLichSu.Columns["TrangThaiCode"] != null)
                    dgvLichSu.Columns["TrangThaiCode"].Visible = false;
                    
                if (dgvLichSu.Columns["TenDeTai"] != null)
                    dgvLichSu.Columns["TenDeTai"].HeaderText = "Tên đề tài";
                if (dgvLichSu.Columns["GiangVien"] != null)
                    dgvLichSu.Columns["GiangVien"].HeaderText = "Giảng viên";
                if (dgvLichSu.Columns["TrangThaiDisplay"] != null)
                    dgvLichSu.Columns["TrangThaiDisplay"].HeaderText = "Trạng thái";
                if (dgvLichSu.Columns["NgayGui"] != null)
                    dgvLichSu.Columns["NgayGui"].HeaderText = "Ngày gửi";
                if (dgvLichSu.Columns["GhiChu"] != null)
                    dgvLichSu.Columns["GhiChu"].HeaderText = "Ghi chú";

                // Highlight rows by status - use code instead of display text
                foreach (DataGridViewRow row in dgvLichSu.Rows)
                {
                    var trangThaiCode = row.Cells["TrangThaiCode"].Value?.ToString() ?? "";
                    
                    if (trangThaiCode == "Approved")
                    {
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
                    }
                    else if (trangThaiCode == "Rejected")
                    {
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.LightCoral;
                    }
                    else if (trangThaiCode == "Pending")
                    {
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;
                    }
                }

                // Statistics
                var pendingCount = yeuCaus.Count(y => y.TrangThai == "Pending");
                var approvedCount = yeuCaus.Count(y => y.TrangThai == "Approved");
                var rejectedCount = yeuCaus.Count(y => y.TrangThai == "Rejected");

                lblThongTin.Text = $"Tổng cộng: {yeuCaus.Count} yêu cầu ({pendingCount} chờ duyệt, {approvedCount} được duyệt, {rejectedCount} bị từ chối)";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetTrangThaiDisplay(string trangThai)
        {
            // Return display text with emoji based on status code
            return trangThai switch
            {
                "Pending" => "⏳ Chờ duyệt",
                "Approved" => "✅ Đã duyệt",
                "Rejected" => "❌ Bị từ chối",
                _ => trangThai ?? "N/A"
            };
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
            MessageBox.Show("✅ Đã làm mới danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvLichSu.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn yêu cầu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check status using code, not display text
            var trangThaiCode = dgvLichSu.CurrentRow.Cells["TrangThaiCode"].Value?.ToString() ?? "";
            
            if (trangThaiCode == "Approved")
            {
                MessageBox.Show("Không thể xóa yêu cầu đã được duyệt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var tenDeTai = dgvLichSu.CurrentRow.Cells["TenDeTai"].Value?.ToString() ?? "";
            var result = MessageBox.Show($"Bạn chắc chắn muốn hủy yêu cầu:\n\n{tenDeTai}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                MessageBox.Show("✅ Đã hủy yêu cầu! Liên hệ giảng viên nếu cần.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
        }
    }
}
