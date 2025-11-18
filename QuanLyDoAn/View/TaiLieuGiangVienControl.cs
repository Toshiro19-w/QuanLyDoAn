using System;
using System.Windows.Forms;
using QuanLyDoAn.Controller;
using QuanLyDoAn.Model.Entities;
using QuanLyDoAn.Utils;
using System.Diagnostics;
using System.IO;

namespace QuanLyDoAn.View
{
    public partial class TaiLieuGiangVienControl : UserControl
    {
        private TaiLieuController taiLieuController;
        private GiangVienController giangVienController;

        public TaiLieuGiangVienControl()
        {
            InitializeComponent();
            taiLieuController = new TaiLieuController();
            giangVienController = new GiangVienController();
            ThemeHelper.ApplyTheme(this);
            LoadDoAn();
        }

        private void LoadDoAn()
        {
            var maGv = UserSession.CurrentUser?.MaGv;
            if (string.IsNullOrEmpty(maGv)) return;

            var doAns = giangVienController.LayDoAnDuocPhanCong(maGv);
            cmbDoAn.DataSource = doAns;
            cmbDoAn.DisplayMember = "TenDeTai";
            cmbDoAn.ValueMember = "MaDeTai";
        }

        private void cmbDoAn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDoAn.SelectedValue != null)
            {
                string maDeTai = cmbDoAn.SelectedValue.ToString();
                var taiLieus = taiLieuController.LayTaiLieuTheoDoAn(maDeTai);
                dgvTaiLieu.DataSource = taiLieus;

                // Ẩn cột navigation và mã
                if (dgvTaiLieu.Columns["MaDeTaiNavigation"] != null)
                    dgvTaiLieu.Columns["MaDeTaiNavigation"].Visible = false;
                if (dgvTaiLieu.Columns["MaTaiLieu"] != null)
                    dgvTaiLieu.Columns["MaTaiLieu"].Visible = false;
                if (dgvTaiLieu.Columns["MaDeTai"] != null)
                    dgvTaiLieu.Columns["MaDeTai"].Visible = false;

                // Đặt tên cột tiếng Việt
                if (dgvTaiLieu.Columns["TenTaiLieu"] != null)
                    dgvTaiLieu.Columns["TenTaiLieu"].HeaderText = "Tên tài liệu";
                if (dgvTaiLieu.Columns["DuongDan"] != null)
                    dgvTaiLieu.Columns["DuongDan"].HeaderText = "Đường dẫn";
                if (dgvTaiLieu.Columns["NgayUpload"] != null)
                    dgvTaiLieu.Columns["NgayUpload"].HeaderText = "Ngày upload";
            }
        }

        private void btnTaiVe_Click(object sender, EventArgs e)
        {
            if (dgvTaiLieu.CurrentRow?.DataBoundItem is TaiLieu taiLieu)
            {
                try
                {
                    if (File.Exists(taiLieu.DuongDan))
                    {
                        using (SaveFileDialog saveDialog = new SaveFileDialog())
                        {
                            saveDialog.FileName = taiLieu.TenTaiLieu;
                            saveDialog.Filter = "All files (*.*)|*.*";
                            
                            if (saveDialog.ShowDialog() == DialogResult.OK)
                            {
                                File.Copy(taiLieu.DuongDan, saveDialog.FileName, true);
                                MessageBox.Show("Tải tài liệu thành công!");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy file tại đường dẫn đã lưu!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi tải tài liệu: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn tài liệu cần tải!");
            }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            if (dgvTaiLieu.CurrentRow?.DataBoundItem is TaiLieu taiLieu)
            {
                try
                {
                    if (File.Exists(taiLieu.DuongDan))
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = taiLieu.DuongDan,
                            UseShellExecute = true
                        });
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy file tại đường dẫn đã lưu!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi mở tài liệu: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn tài liệu cần xem!");
            }
        }
    }
}