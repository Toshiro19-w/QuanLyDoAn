using System;
using System.Windows.Forms;
using QuanLyDoAn.Helpers;
using QuanLyDoAn.Model.ViewModels;
using QuanLyDoAn.Utils;

namespace QuanLyDoAn.View
{
    public partial class ChamDiemChiTietForm : Form
    {
        private string _maDeTai;
        private string _maGv;
        private ChamDiemNhanhViewModel? _formData;

        public ChamDiemChiTietForm(string maDeTai, string maGv)
        {
            InitializeComponent();
            _maDeTai = maDeTai;
            _maGv = maGv;
            ThemeHelper.ApplyTheme(this);
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Lấy thông tin đồ án
                var chiTiet = GiangVienUXHelper.LayChiTietDoAn(_maDeTai, _maGv);
                if (chiTiet == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin đồ án!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.Cancel;
                    return;
                }

                lblThongTinDoAn.Text = $"{chiTiet.TenDeTai} - {chiTiet.SinhVien}";

                // Load các loại đánh giá có thể chấm
                var loaiChoPhep = chiTiet.DanhSachLoaiDanhGia
                    .Where(l => l.CoTheChams && !l.DaCham)
                    .ToList();

                if (!loaiChoPhep.Any())
                {
                    MessageBox.Show("Không có loại đánh giá nào có thể chấm!\n\nLý do có thể:\n- Đã chấm tất cả loại đánh giá\n- Chưa đủ điều kiện chấm\n- Không phải giảng viên được phân công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.Cancel;
                    return;
                }

                cboLoaiDanhGia.DataSource = loaiChoPhep;
                cboLoaiDanhGia.DisplayMember = "TenLoai";
                cboLoaiDanhGia.ValueMember = "MaLoai";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void CboLoaiDanhGia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLoaiDanhGia.SelectedItem == null) return;
            
            var selected = cboLoaiDanhGia.SelectedItem as dynamic;
            if (selected == null) return;
            
            string maLoaiDanhGia = selected.MaLoai;
            _formData = GiangVienUXHelper.ChamDiem.LayFormChamDiem(_maDeTai, _maGv, maLoaiDanhGia);

            if (_formData == null)
            {
                MessageBox.Show("Không thể tạo form chấm điểm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tạm dừng layout để tránh lỗi resize
            dgvTieuChi.SuspendLayout();
            try
            {
                dgvTieuChi.DataSource = null;
                dgvTieuChi.DataSource = _formData.DanhSachTieuChi;

                // Cấu hình cột
                if (dgvTieuChi.Columns["MaTieuChi"] != null)
                    dgvTieuChi.Columns["MaTieuChi"].Visible = false;
                if (dgvTieuChi.Columns["TenTieuChi"] != null)
                {
                    dgvTieuChi.Columns["TenTieuChi"].HeaderText = "Tiêu chí";
                    dgvTieuChi.Columns["TenTieuChi"].ReadOnly = true;
                    dgvTieuChi.Columns["TenTieuChi"].Width = 250;
                }
                if (dgvTieuChi.Columns["MoTa"] != null)
                {
                    dgvTieuChi.Columns["MoTa"].HeaderText = "Mô tả";
                    dgvTieuChi.Columns["MoTa"].ReadOnly = true;
                    dgvTieuChi.Columns["MoTa"].Width = 200;
                }
                if (dgvTieuChi.Columns["TrongSo"] != null)
                {
                    dgvTieuChi.Columns["TrongSo"].HeaderText = "Trọng số (%)";
                    dgvTieuChi.Columns["TrongSo"].ReadOnly = true;
                    dgvTieuChi.Columns["TrongSo"].Width = 100;
                }
                if (dgvTieuChi.Columns["DiemToiDa"] != null)
                {
                    dgvTieuChi.Columns["DiemToiDa"].HeaderText = "Điểm tối đa";
                    dgvTieuChi.Columns["DiemToiDa"].ReadOnly = true;
                    dgvTieuChi.Columns["DiemToiDa"].Width = 100;
                }
                if (dgvTieuChi.Columns["Diem"] != null)
                {
                    dgvTieuChi.Columns["Diem"].HeaderText = "Điểm";
                    dgvTieuChi.Columns["Diem"].Width = 80;
                }
                if (dgvTieuChi.Columns["NhanXet"] != null)
                {
                    dgvTieuChi.Columns["NhanXet"].HeaderText = "Nhận xét";
                    dgvTieuChi.Columns["NhanXet"].Width = 200;
                }
                if (dgvTieuChi.Columns["HopLe"] != null)
                    dgvTieuChi.Columns["HopLe"].Visible = false;
                if (dgvTieuChi.Columns["HienThiTieuChi"] != null)
                    dgvTieuChi.Columns["HienThiTieuChi"].Visible = false;
            }
            finally
            {
                dgvTieuChi.ResumeLayout();
            }

            CapNhatDiemTrungBinh();
        }

        private void DgvTieuChi_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            CapNhatDiemTrungBinh();
        }

        private void CapNhatDiemTrungBinh()
        {
            if (_formData == null) return;
            lblDiemTB.Text = $"Điểm trung bình: {_formData.DiemTrungBinh:F2}/10";
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (_formData == null) return;

            // Kiểm tra hợp lệ
            if (!_formData.DayDuThongTin)
            {
                MessageBox.Show("Vui lòng nhập đủ điểm cho tất cả tiêu chí!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (var tieuChi in _formData.DanhSachTieuChi)
            {
                if (!tieuChi.HopLe)
                {
                    MessageBox.Show($"Điểm '{tieuChi.TenTieuChi}' không hợp lệ (0-{tieuChi.DiemToiDa})!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Lưu kết quả
            if (GiangVienUXHelper.ChamDiem.LuuKetQua(_formData, out string errorMessage))
            {
                MessageBox.Show("Chấm điểm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show($"Lỗi: {errorMessage}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
