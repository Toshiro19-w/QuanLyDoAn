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
                    MessageBox.Show("B?n ch?a g?i yêu c?u nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblThongTin.Text = "Ch?a có yêu c?u nào";
                    dgvLichSu.DataSource = null;
                    return;
                }

                var displayData = yeuCaus.Select(y => new
                {
                    y.MaYeuCau,
                    y.MaDeTai,
                    TenDeTai = y.MaDeTaiNavigation?.TenDeTai ?? "",
                    GiangVien = y.MaDeTaiNavigation?.MaGvNavigation?.HoTen ?? "N/A",
                    TrangThaiCode = y.TrangThai, // L?u code g?c
                    TrangThaiDisplay = GetTrangThaiDisplay(y.TrangThai), // Hi?n th? v?i emoji
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
                    dgvLichSu.Columns["TenDeTai"].HeaderText = "Tên ?? tài";
                if (dgvLichSu.Columns["GiangVien"] != null)
                    dgvLichSu.Columns["GiangVien"].HeaderText = "Gi?ng viên";
                if (dgvLichSu.Columns["TrangThaiDisplay"] != null)
                    dgvLichSu.Columns["TrangThaiDisplay"].HeaderText = "Tr?ng thái";
                if (dgvLichSu.Columns["NgayGui"] != null)
                    dgvLichSu.Columns["NgayGui"].HeaderText = "Ngày g?i";
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

                lblThongTin.Text = $"T?ng c?ng: {yeuCaus.Count} yêu c?u ({pendingCount} ch? duy?t, {approvedCount} ???c duy?t, {rejectedCount} b? t? ch?i)";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"L?i khi t?i d? li?u: {ex.Message}", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetTrangThaiDisplay(string trangThai)
        {
            // Return display text with emoji based on status code
            return trangThai switch
            {
                "Pending" => "? Ch? duy?t",
                "Approved" => "? ?ã duy?t",
                "Rejected" => "? B? t? ch?i",
                _ => trangThai ?? "N/A"
            };
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
            MessageBox.Show("? ?ã làm m?i danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvLichSu.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng ch?n yêu c?u!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check status using code, not display text
            var trangThaiCode = dgvLichSu.CurrentRow.Cells["TrangThaiCode"].Value?.ToString() ?? "";
            
            if (trangThaiCode == "Approved")
            {
                MessageBox.Show("Không th? xóa yêu c?u ?ã ???c duy?t!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var tenDeTai = dgvLichSu.CurrentRow.Cells["TenDeTai"].Value?.ToString() ?? "";
            var result = MessageBox.Show($"B?n ch?c ch?n mu?n h?y yêu c?u:\n\n{tenDeTai}?", "Xác nh?n", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                MessageBox.Show("? ?ã h?y yêu c?u! Liên h? gi?ng viên n?u c?n.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
        }
    }
}
