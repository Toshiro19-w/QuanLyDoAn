using System;
using System.Windows.Forms;
using QuanLyDoAn.Controller;
using QuanLyDoAn.Utils;

namespace QuanLyDoAn.View
{
    public partial class ThongBaoSinhVienControl : UserControl
    {
        private SinhVienController sinhVienController;

        public ThongBaoSinhVienControl()
        {
            InitializeComponent();
            sinhVienController = new SinhVienController();
            ThemeHelper.ApplyTheme(this);
            LoadThongBao();
        }

        private void LoadThongBao()
        {
            var maSv = UserSession.CurrentUser?.MaSv;
            if (string.IsNullOrEmpty(maSv)) return;

            var doAn = sinhVienController.LayDoAnCuaSinhVien(maSv);
            if (doAn != null)
            {
                lblTenDeTai.Text = $"Đồ án: {doAn.TenDeTai}";
                var thongBaos = sinhVienController.LayThongBaoDoAn(doAn.MaDeTai);
                dgvThongBao.DataSource = thongBaos;

                if (dgvThongBao.Columns.Count > 0)
                {
                    if (dgvThongBao.Columns.Contains("MaDeTaiNavigation"))
                        dgvThongBao.Columns["MaDeTaiNavigation"].Visible = false;
                    if (dgvThongBao.Columns.Contains("MaThongBao"))
                        dgvThongBao.Columns["MaThongBao"].Visible = false;
                    if (dgvThongBao.Columns.Contains("MaDeTai"))
                        dgvThongBao.Columns["MaDeTai"].Visible = false;

                    if (dgvThongBao.Columns.Contains("NoiDung"))
                    {
                        dgvThongBao.Columns["NoiDung"].HeaderText = "Nội dung";
                        dgvThongBao.Columns["NoiDung"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                    if (dgvThongBao.Columns.Contains("NgayGui"))
                    {
                        dgvThongBao.Columns["NgayGui"].HeaderText = "Ngày gửi";
                    }
                }

                lblSoThongBao.Text = $"Tổng số thông báo: {thongBaos.Count}";
            }
            else
            {
                lblTenDeTai.Text = "Bạn chưa được phân công đồ án";
                lblSoThongBao.Text = "Tổng số thông báo: 0";
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadThongBao();
            MessageBox.Show("Đã làm mới danh sách thông báo!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvThongBao_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var noiDung = dgvThongBao.Rows[e.RowIndex].Cells["NoiDung"].Value?.ToString();
                var ngayGui = dgvThongBao.Rows[e.RowIndex].Cells["NgayGui"].Value?.ToString();
                
                MessageBox.Show($"{noiDung}\n\n{ngayGui}", "Chi tiết thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
