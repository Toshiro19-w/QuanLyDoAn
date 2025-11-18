using System;
using System.IO;
using System.Windows.Forms;
using QuanLyDoAn.Controller;
using QuanLyDoAn.Model.Entities;
using QuanLyDoAn.Utils;

namespace QuanLyDoAn.View
{
    public partial class NopTaiLieuControl : UserControl
    {
        private SinhVienController sinhVienController;
        private TaiLieuController taiLieuController;
        private string maDeTai;

        public NopTaiLieuControl()
        {
            InitializeComponent();
            sinhVienController = new SinhVienController();
            taiLieuController = new TaiLieuController();
            ThemeHelper.ApplyTheme(this);
            LoadDoAn();
        }

        private void LoadDoAn()
        {
            var maSv = UserSession.CurrentUser?.MaSv;
            if (string.IsNullOrEmpty(maSv)) return;

            var doAn = sinhVienController.LayDoAnCuaSinhVien(maSv);
            if (doAn != null)
            {
                maDeTai = doAn.MaDeTai;
                lblTenDeTai.Text = $"Đồ án: {doAn.TenDeTai}";
                LoadTaiLieu();
            }
            else
            {
                lblTenDeTai.Text = "Bạn chưa được phân công đồ án";
                btnChonFile.Enabled = false;
                btnNopTaiLieu.Enabled = false;
            }
        }

        private void LoadTaiLieu()
        {
            if (string.IsNullOrEmpty(maDeTai)) return;

            var taiLieus = taiLieuController.LayTaiLieuTheoDoAn(maDeTai);
            dgvTaiLieu.DataSource = taiLieus;

            if (dgvTaiLieu.Columns.Count > 0)
            {
                if (dgvTaiLieu.Columns.Contains("MaDeTaiNavigation"))
                    dgvTaiLieu.Columns["MaDeTaiNavigation"].Visible = false;
                if (dgvTaiLieu.Columns.Contains("MaTaiLieu"))
                    dgvTaiLieu.Columns["MaTaiLieu"].Visible = false;
                if (dgvTaiLieu.Columns.Contains("MaDeTai"))
                    dgvTaiLieu.Columns["MaDeTai"].Visible = false;

                if (dgvTaiLieu.Columns.Contains("TenTaiLieu"))
                    dgvTaiLieu.Columns["TenTaiLieu"].HeaderText = "Tên tài liệu";
                if (dgvTaiLieu.Columns.Contains("DuongDan"))
                    dgvTaiLieu.Columns["DuongDan"].HeaderText = "Đường dẫn";
                if (dgvTaiLieu.Columns.Contains("NgayUpload"))
                    dgvTaiLieu.Columns["NgayUpload"].HeaderText = "Ngày nộp";
            }

            lblSoTaiLieu.Text = $"Tổng số tài liệu: {taiLieus.Count}";
        }

        private void btnChonFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "All files (*.*)|*.*|PDF files (*.pdf)|*.pdf|Word files (*.docx)|*.docx";
                openFileDialog.Title = "Chọn tài liệu";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtDuongDan.Text = openFileDialog.FileName;
                    if (string.IsNullOrEmpty(txtTenTaiLieu.Text))
                    {
                        txtTenTaiLieu.Text = Path.GetFileName(openFileDialog.FileName);
                    }
                }
            }
        }

        private void btnNopTaiLieu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenTaiLieu.Text))
            {
                MessageBox.Show("Vui lòng nhập tên tài liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenTaiLieu.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDuongDan.Text))
            {
                MessageBox.Show("Vui lòng chọn file tài liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!File.Exists(txtDuongDan.Text))
            {
                MessageBox.Show("File không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show("Bạn có chắc chắn muốn nộp tài liệu này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            var taiLieu = new TaiLieu
            {
                MaDeTai = maDeTai,
                TenTaiLieu = txtTenTaiLieu.Text.Trim(),
                DuongDan = txtDuongDan.Text.Trim(),
                NgayUpload = DateOnly.FromDateTime(DateTime.Now)
            };

            if (taiLieuController.ThemTaiLieu(taiLieu))
            {
                MessageBox.Show("Nộp tài liệu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTaiLieu();
                txtTenTaiLieu.Clear();
                txtDuongDan.Clear();
            }
            else
            {
                MessageBox.Show("Nộp tài liệu thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMoFile_Click(object sender, EventArgs e)
        {
            if (dgvTaiLieu.SelectedRows.Count > 0)
            {
                var duongDan = dgvTaiLieu.SelectedRows[0].Cells["DuongDan"].Value?.ToString();
                if (!string.IsNullOrEmpty(duongDan) && File.Exists(duongDan))
                {
                    try
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = duongDan,
                            UseShellExecute = true
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Không thể mở file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("File không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn tài liệu cần mở!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
