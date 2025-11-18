using System;
using System.Windows.Forms;
using QuanLyDoAn.Controller;
using QuanLyDoAn.Model.Entities;
using QuanLyDoAn.Utils;

namespace QuanLyDoAn.View
{
    public partial class CapNhatTienDoControl : UserControl
    {
        private SinhVienController sinhVienController;
        private string maDeTai;

        public CapNhatTienDoControl()
        {
            InitializeComponent();
            sinhVienController = new SinhVienController();
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
                LoadTienDo();
            }
            else
            {
                lblTenDeTai.Text = "Bạn chưa được phân công đồ án";
                btnThemTienDo.Enabled = false;
            }
        }

        private void LoadTienDo()
        {
            if (string.IsNullOrEmpty(maDeTai)) return;

            var tienDos = sinhVienController.LayTienDoDoAn(maDeTai);
            dgvTienDo.DataSource = tienDos;

            // Ẩn cột navigation và mã
            if (dgvTienDo.Columns["MaDeTaiNavigation"] != null)
                dgvTienDo.Columns["MaDeTaiNavigation"].Visible = false;
            if (dgvTienDo.Columns["MaTienDo"] != null)
                dgvTienDo.Columns["MaTienDo"].Visible = false;
            if (dgvTienDo.Columns["MaDeTai"] != null)
                dgvTienDo.Columns["MaDeTai"].Visible = false;

            // Đặt tên cột tiếng Việt
            if (dgvTienDo.Columns["GiaiDoan"] != null)
                dgvTienDo.Columns["GiaiDoan"].HeaderText = "Giai đoạn";
            if (dgvTienDo.Columns["NgayNop"] != null)
                dgvTienDo.Columns["NgayNop"].HeaderText = "Ngày nộp";
            if (dgvTienDo.Columns["NhanXet"] != null)
                dgvTienDo.Columns["NhanXet"].HeaderText = "Nhận xét GV";
            if (dgvTienDo.Columns["TrangThaiNop"] != null)
                dgvTienDo.Columns["TrangThaiNop"].HeaderText = "Trạng thái";
        }

        private void btnThemTienDo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtGiaiDoan.Text))
            {
                MessageBox.Show("Vui lòng nhập nội dung giai đoạn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaiDoan.Focus();
                return;
            }

            if (txtGiaiDoan.Text.Length > 50)
            {
                MessageBox.Show("Nội dung giai đoạn không được vượt quá 50 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaiDoan.Focus();
                return;
            }

            var result = MessageBox.Show("Bạn có chắc chắn muốn nộp tiến độ này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            var tienDo = new TienDo
            {
                MaDeTai = maDeTai,
                GiaiDoan = txtGiaiDoan.Text.Trim(),
                NgayNop = DateOnly.FromDateTime(DateTime.Now),
                TrangThaiNop = "DungHan"
            };

            if (sinhVienController.ThemTienDo(tienDo))
            {
                MessageBox.Show("Nộp tiến độ thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTienDo();
                txtGiaiDoan.Clear();
            }
            else
            {
                MessageBox.Show("Nộp tiến độ thất bại! Vui lòng kiểm tra lại thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}