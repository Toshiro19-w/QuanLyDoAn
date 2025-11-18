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
            try
            {
                var maSv = UserSession.CurrentUser?.MaSv ?? "";

                bool daCoDoAn = controller.SinhVienDaCoDoAn(maSv);
                btnDangKy.Enabled = !daCoDoAn;

                if (daCoDoAn)
                {
                    dgvDoAn.DataSource = null;
                    return;
                }

                // Load danh sách đề tài chưa có sinh viên (ẩn đề tài SV đã yêu cầu)
                var doAns = controller.LayDoAnChuaCoSinhVien(maSv);
                var yeuCauCuaSv = controller.LayYeuCauCuaSinhVien(maSv);
                
                var displayData = doAns.Select(d => 
                {
                    // Kiểm tra xem SV đã đăng ký đề tài này chưa
                    var yeuCau = yeuCauCuaSv.FirstOrDefault(y => y.MaDeTai == d.MaDeTai);
                    var trangThaiYeuCau = yeuCau?.TrangThai ?? "Chưa đăng ký";
                    
                    // Display text với emoji
                    var trangThaiDisplay = GetTrangThaiYeuCauDisplay(trangThaiYeuCau);
                    
                    return new
                    {
                        d.MaDeTai,
                        d.TenDeTai,
                        d.MoTa,
                        GiangVien = d.MaGvNavigation?.HoTen ?? "N/A",
                        LoaiDoAn = d.MaLoaiDoAnNavigation?.TenLoaiDoAn ?? "",
                        TrangThai = d.MaTrangThaiNavigation?.TenTrangThai ?? "",
                        d.NgayBatDau,
                        d.NgayKetThuc,
                        TrangThaiYeuCauCode = trangThaiYeuCau, // Code để compare
                        TrangThaiYeuCauDisplay = trangThaiDisplay, // Display text
                        DaYeuCau = yeuCau != null // Field để kiểm tra
                    };
                }).ToList();

                dgvDoAn.DataSource = displayData;

                // Configure columns
                if (dgvDoAn.Columns["MaDeTai"] != null)
                    dgvDoAn.Columns["MaDeTai"].Visible = false;
                if (dgvDoAn.Columns["DaYeuCau"] != null)
                    dgvDoAn.Columns["DaYeuCau"].Visible = false;
                if (dgvDoAn.Columns["TrangThaiYeuCauCode"] != null)
                    dgvDoAn.Columns["TrangThaiYeuCauCode"].Visible = false;
                    
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
                if (dgvDoAn.Columns["TrangThaiYeuCauDisplay"] != null)
                {
                    dgvDoAn.Columns["TrangThaiYeuCauDisplay"].HeaderText = "Yêu cầu của bạn";
                    dgvDoAn.Columns["TrangThaiYeuCauDisplay"].DisplayIndex = dgvDoAn.Columns.Count - 1;
                }
                if (dgvDoAn.Columns["NgayBatDau"] != null)
                    dgvDoAn.Columns["NgayBatDau"].HeaderText = "Ngày bắt đầu";
                if (dgvDoAn.Columns["NgayKetThuc"] != null)
                    dgvDoAn.Columns["NgayKetThuc"].HeaderText = "Ngày kết thúc";
                    
                // Highlight rows đã đăng ký
                foreach (DataGridViewRow row in dgvDoAn.Rows)
                {
                    if (row.DataBoundItem != null)
                    {
                        var daDangKy = (bool?)row.DataBoundItem.GetType()
                            .GetProperty("DaYeuCau")?.GetValue(row.DataBoundItem) ?? false;
                        
                        if (daDangKy)
                        {
                            row.DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;
                            row.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Gold;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetTrangThaiYeuCauDisplay(string trangThaiCode)
        {
            // Convert status code to display text with emoji
            return trangThaiCode switch
            {
                "Pending" => "⏳ Chờ duyệt",
                "Approved" => "✅ Đã duyệt",
                "Rejected" => "❌ Bị từ chối",
                "Chưa đăng ký" => "Chưa đăng ký",
                _ => trangThaiCode ?? "N/A"
            };
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
            var daDangKy = (bool?)dgvDoAn.CurrentRow.DataBoundItem?.GetType()
                .GetProperty("DaYeuCau")?.GetValue(dgvDoAn.CurrentRow.DataBoundItem) ?? false;

            if (string.IsNullOrEmpty(maDeTai) || string.IsNullOrEmpty(maSv))
                return;

            // Kiểm tra đã gửi yêu cầu chưa
            if (daDangKy)
            {
                MessageBox.Show("Bạn đã gửi yêu cầu cho đề tài này rồi! Vui lòng chờ giảng viên duyệt.", 
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show(
                $"Bạn muốn đăng ký đề tài này?\n\nTên: {dgvDoAn.CurrentRow.Cells["TenDeTai"].Value}\n\nGhi chú (tùy chọn):",
                "Xác nhận đăng ký", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                if (controller.GuiYeuCauDangKy(maDeTai, maSv, txtGhiChu.Text))
                {
                    MessageBox.Show("✅ Gửi yêu cầu đăng ký thành công!\n\nChờ giảng viên duyệt...", 
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtGhiChu.Clear();
                    LoadData(); // Làm mới để thấy trạng thái mới
                }
                else
                {
                    MessageBox.Show("❌ Gửi yêu cầu thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
            MessageBox.Show("✅ Đã làm mới danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
