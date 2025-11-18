using System;
using System.Windows.Forms;
using QuanLyDoAn.Controller;
using QuanLyDoAn.Model.Entities;
using QuanLyDoAn.Utils;
using System.Linq;

namespace QuanLyDoAn.View
{
    public partial class DoAnGiangVienControl : UserControl
    {
        private GiangVienController giangVienController;

        public DoAnGiangVienControl()
        {
            InitializeComponent();
            giangVienController = new GiangVienController();
            ThemeHelper.ApplyTheme(this);
            LoadData();
        }

        private void LoadData()
        {
            var maGv = UserSession.CurrentUser?.MaGv;
            if (string.IsNullOrEmpty(maGv)) return;

            var doAns = giangVienController.LayDoAnDuocPhanCong(maGv);
            
            // Tạo dữ liệu hiển thị với thông tin bổ sung
            var displayData = doAns.Select(d => new
            {
                d.MaDeTai,
                d.TenDeTai,
                d.MoTa,
                LoaiDoAn = d.MaLoaiDoAnNavigation?.TenLoaiDoAn ?? "Chưa xác định",
                SinhVien = d.MaSvNavigation?.HoTen ?? "Chưa phân công",
                TrangThai = d.MaTrangThaiNavigation?.TenTrangThai ?? "Chưa xác định",
                d.NgayBatDau,
                d.NgayKetThuc,
                d.Diem
            }).ToList();
            
            dgvDoAn.DataSource = displayData;

            // Đặt tên cột tiếng Việt
            if (dgvDoAn.Columns["MaDeTai"] != null)
                dgvDoAn.Columns["MaDeTai"].Visible = false;
            if (dgvDoAn.Columns["TenDeTai"] != null)
                dgvDoAn.Columns["TenDeTai"].HeaderText = "Tên đề tài";
            if (dgvDoAn.Columns["MoTa"] != null)
                dgvDoAn.Columns["MoTa"].HeaderText = "Mô tả";
            if (dgvDoAn.Columns["LoaiDoAn"] != null)
                dgvDoAn.Columns["LoaiDoAn"].HeaderText = "Loại đồ án";
            if (dgvDoAn.Columns["SinhVien"] != null)
                dgvDoAn.Columns["SinhVien"].HeaderText = "Sinh viên";
            if (dgvDoAn.Columns["TrangThai"] != null)
                dgvDoAn.Columns["TrangThai"].HeaderText = "Trạng thái";
            if (dgvDoAn.Columns["NgayBatDau"] != null)
                dgvDoAn.Columns["NgayBatDau"].HeaderText = "Ngày bắt đầu";
            if (dgvDoAn.Columns["NgayKetThuc"] != null)
                dgvDoAn.Columns["NgayKetThuc"].HeaderText = "Ngày kết thúc";
            if (dgvDoAn.Columns["Diem"] != null)
                dgvDoAn.Columns["Diem"].HeaderText = "Điểm";
        }


        private void DgvDoAn_SelectionChanged(object? sender, EventArgs? e)
        {
            if (dgvDoAn.CurrentRow?.DataBoundItem != null)
            {
                var maDeTai = dgvDoAn.CurrentRow.Cells["MaDeTai"].Value?.ToString();
                if (string.IsNullOrEmpty(maDeTai)) return;
                
                var tienDos = giangVienController.LayTienDoTheoDoAn(maDeTai);
                dgvTienDo.DataSource = tienDos;

                // Ẩn cột navigation và mã
                if (dgvTienDo.Columns["MaDeTaiNavigation"] != null)
                    dgvTienDo.Columns["MaDeTaiNavigation"].Visible = false;
                if (dgvTienDo.Columns["MaTienDo"] != null)
                    dgvTienDo.Columns["MaTienDo"].Visible = false;
                if (dgvTienDo.Columns["MaDeTai"] != null)
                    dgvTienDo.Columns["MaDeTai"].Visible = false;

                // Đặt tên cột tiếng Việt cho tiến độ
                if (dgvTienDo.Columns["GiaiDoan"] != null)
                    dgvTienDo.Columns["GiaiDoan"].HeaderText = "Giai đoạn";
                if (dgvTienDo.Columns["NgayNop"] != null)
                    dgvTienDo.Columns["NgayNop"].HeaderText = "Ngày nộp";
                if (dgvTienDo.Columns["NhanXet"] != null)
                    dgvTienDo.Columns["NhanXet"].HeaderText = "Nhận xét";
                if (dgvTienDo.Columns["TrangThaiNop"] != null)
                {
                    dgvTienDo.Columns["TrangThaiNop"].HeaderText = "Trạng thái nộp";
                    // Chuyển đổi dữ liệu sang tiếng Việt
                    foreach (DataGridViewRow row in dgvTienDo.Rows)
                    {
                        if (row.Cells["TrangThaiNop"].Value != null)
                        {
                            string status = row.Cells["TrangThaiNop"].Value.ToString();
                            row.Cells["TrangThaiNop"].Value = status switch
                            {
                                "Submitted" => "Đã nộp",
                                "Late" => "Nộp muộn",
                                "Pending" => "Chờ nộp",
                                "Not Submitted" => "Chưa nộp",
                                _ => status
                            };
                        }
                    }
                }
            }
        }
        
        private void DgvTienDo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvTienDo.Rows[e.RowIndex].DataBoundItem is TienDo tienDo)
            {
                txtNhanXet.Text = tienDo.NhanXet ?? "";
            }
        }

        private void BtnCapNhatNhanXet_Click(object sender, EventArgs e)
        {
            if (dgvTienDo.CurrentRow?.DataBoundItem is TienDo tienDo)
            {
                string trangThaiNop = "DungHan";
                if (giangVienController.CapNhatNhanXet(tienDo.MaTienDo, txtNhanXet.Text, trangThaiNop))
                {
                    MessageBox.Show("Cập nhật nhận xét thành công!");
                    DgvDoAn_SelectionChanged(null, null);
                    txtNhanXet.Clear();
                }
            }
        }

        private void BtnChamDiem_Click(object sender, EventArgs e)
        {
            if (dgvDoAn.CurrentRow?.DataBoundItem != null)
            {
                var maDeTai = dgvDoAn.CurrentRow.Cells["MaDeTai"].Value?.ToString();
                if (string.IsNullOrEmpty(maDeTai)) return;
                
                if (decimal.TryParse(txtDiem.Text, out decimal diem) && diem >= 0 && diem <= 10)
                {
                    if (giangVienController.ChamDiem(maDeTai, diem))
                    {
                        MessageBox.Show("Chấm điểm thành công!");
                        LoadData();
                        txtDiem.Clear();
                        txtNhanXetCuoi.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Điểm phải từ 0 đến 10!");
                }
            }
        }
    }
}