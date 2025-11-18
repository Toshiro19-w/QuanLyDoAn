using System;
using System.Windows.Forms;
using QuanLyDoAn.Controller;
using QuanLyDoAn.Model.Entities;
using QuanLyDoAn.Utils;

namespace QuanLyDoAn.View
{
    public partial class BaoCaoTienDoControl : UserControl
    {
        private GiangVienController giangVienController;

        public BaoCaoTienDoControl()
        {
            InitializeComponent();
            giangVienController = new GiangVienController();
            ThemeHelper.ApplyTheme(this);
            LoadBaoCao();
        }

        private void LoadBaoCao()
        {
            var maGv = UserSession.CurrentUser?.MaGv;
            if (string.IsNullOrEmpty(maGv)) return;

            var doAns = giangVienController.LayDoAnDuocPhanCong(maGv);
            var baoCaoData = doAns.Select(d => new
            {
                d.TenDeTai,
                SinhVien = d.MaSvNavigation?.HoTen ?? "Chưa phân công",
                TrangThai = d.MaTrangThaiNavigation?.TenTrangThai ?? d.MaTrangThai,
                Diem = d.Diem?.ToString("F1") ?? "Chưa chấm",
                SoTienDo = giangVienController.LayTienDoTheoDoAn(d.MaDeTai).Count,
                TienDoGanNhat = giangVienController.LayTienDoTheoDoAn(d.MaDeTai)
                    .OrderByDescending(t => t.NgayNop)
                    .FirstOrDefault()?.GiaiDoan ?? "Chưa có tiến độ"
            }).ToList();

            dgvBaoCao.DataSource = baoCaoData;

            // Đặt tên cột tiếng Việt
            if (dgvBaoCao.Columns["TenDeTai"] != null)
                dgvBaoCao.Columns["TenDeTai"].HeaderText = "Tên đề tài";
            if (dgvBaoCao.Columns["SinhVien"] != null)
                dgvBaoCao.Columns["SinhVien"].HeaderText = "Sinh viên";
            if (dgvBaoCao.Columns["TrangThai"] != null)
                dgvBaoCao.Columns["TrangThai"].HeaderText = "Trạng thái";
            if (dgvBaoCao.Columns["Diem"] != null)
                dgvBaoCao.Columns["Diem"].HeaderText = "Điểm";
            if (dgvBaoCao.Columns["SoTienDo"] != null)
                dgvBaoCao.Columns["SoTienDo"].HeaderText = "Số tiến độ";
            if (dgvBaoCao.Columns["TienDoGanNhat"] != null)
                dgvBaoCao.Columns["TienDoGanNhat"].HeaderText = "Tiến độ gần nhất";

            // Tính toán thống kê
            lblTongDoAn.Text = $"Tổng số đồ án: {doAns.Count}";
            lblDaHoanThanh.Text = $"Đã hoàn thành: {doAns.Count(d => d.Diem.HasValue)}";
            lblDangThucHien.Text = $"Đang thực hiện: {doAns.Count(d => !d.Diem.HasValue)}";
            lblDiemTrungBinh.Text = $"Điểm trung bình: {(doAns.Where(d => d.Diem.HasValue).Any() ? doAns.Where(d => d.Diem.HasValue).Average(d => d.Diem.Value).ToString("F2") : "N/A")}";
        }

        private void btnXuatBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "CSV files (*.csv)|*.csv";
                    saveDialog.FileName = $"BaoCaoTienDo_{DateTime.Now:yyyyMMdd}.csv";
                    
                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        XuatCSV(saveDialog.FileName);
                        MessageBox.Show("Xuất báo cáo thành công!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất báo cáo: {ex.Message}");
            }
        }

        private void XuatCSV(string filePath)
        {
            using (var writer = new System.IO.StreamWriter(filePath, false, System.Text.Encoding.UTF8))
            {
                // Header
                writer.WriteLine("Tên đề tài,Sinh viên,Trạng thái,Điểm,Số tiến độ,Tiến độ gần nhất");
                
                // Data
                foreach (DataGridViewRow row in dgvBaoCao.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        var values = new string[6];
                        for (int i = 0; i < 6; i++)
                        {
                            values[i] = row.Cells[i].Value?.ToString() ?? "";
                        }
                        writer.WriteLine(string.Join(",", values));
                    }
                }
            }
        }
    }
}