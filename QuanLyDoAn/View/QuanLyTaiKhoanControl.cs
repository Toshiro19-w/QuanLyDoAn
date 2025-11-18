using System;
using System.Windows.Forms;
using QuanLyDoAn.Controller;
using QuanLyDoAn.Model.Entities;
using QuanLyDoAn.Utils;

namespace QuanLyDoAn.View
{
    public partial class QuanLyTaiKhoanControl : UserControl
    {
        private QuanLyTaiKhoanController taiKhoanController;

        public QuanLyTaiKhoanControl()
        {
            InitializeComponent();
            taiKhoanController = new QuanLyTaiKhoanController();
            LoadData();
            LoadComboBoxes();
            ThemeHelper.ApplyTheme(this);
            
            // Ẩn textbox mật khẩu
            txtMatKhau.UseSystemPasswordChar = true;
        }

        private void LoadData()
        {
            var danhSachTaiKhoan = taiKhoanController.LayDanhSachTaiKhoan();
            dgvTaiKhoan.DataSource = danhSachTaiKhoan;
            
            // Ẩn các cột navigation
            if (dgvTaiKhoan.Columns["MaSvNavigation"] != null)
                dgvTaiKhoan.Columns["MaSvNavigation"].Visible = false;
            if (dgvTaiKhoan.Columns["MaGvNavigation"] != null)
                dgvTaiKhoan.Columns["MaGvNavigation"].Visible = false;
            
            // Đặt tên cột tiếng Việt
            if (dgvTaiKhoan.Columns["TenDangNhap"] != null)
                dgvTaiKhoan.Columns["TenDangNhap"].HeaderText = "Tên đăng nhập";
            if (dgvTaiKhoan.Columns["MatKhau"] != null)
                dgvTaiKhoan.Columns["MatKhau"].HeaderText = "Mật khẩu";
            if (dgvTaiKhoan.Columns["VaiTro"] != null)
                dgvTaiKhoan.Columns["VaiTro"].HeaderText = "Vai trò";
            if (dgvTaiKhoan.Columns["MaSv"] != null)
                dgvTaiKhoan.Columns["MaSv"].HeaderText = "Mã sinh viên";
            if (dgvTaiKhoan.Columns["MaGv"] != null)
                dgvTaiKhoan.Columns["MaGv"].HeaderText = "Mã giảng viên";
        }

        private void LoadComboBoxes()
        {
            cmbVaiTro.Items.AddRange(new[] { Constants.UserRoles.Admin, Constants.UserRoles.GiangVien, Constants.UserRoles.SinhVien });
            
            var sinhViens = taiKhoanController.LayDanhSachSinhVien();
            cmbSinhVien.DataSource = sinhViens;
            cmbSinhVien.DisplayMember = "HoTen";
            cmbSinhVien.ValueMember = "MaSv";

            var giangViens = taiKhoanController.LayDanhSachGiangVien();
            cmbGiangVien.DataSource = giangViens;
            cmbGiangVien.DisplayMember = "HoTen";
            cmbGiangVien.ValueMember = "MaGv";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            var taiKhoan = new TaiKhoan
            {
                TenDangNhap = txtTenDangNhap.Text,
                MatKhau = HashHelper.HashPassword(txtMatKhau.Text),
                VaiTro = cmbVaiTro.Text,
                MaSv = cmbVaiTro.Text == Constants.UserRoles.SinhVien ? cmbSinhVien.SelectedValue?.ToString() : null,
                MaGv = cmbVaiTro.Text == Constants.UserRoles.GiangVien ? cmbGiangVien.SelectedValue?.ToString() : null
            };

            if (taiKhoanController.TaoTaiKhoan(taiKhoan))
            {
                MessageBox.Show(Constants.Messages.ThemThanhCong);
                LoadData();
                ClearForm();
            }
            else
            {
                MessageBox.Show(Constants.Messages.DuLieuKhongHopLe);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvTaiKhoan.CurrentRow != null && ValidateInput())
            {
                var taiKhoan = (TaiKhoan)dgvTaiKhoan.CurrentRow.DataBoundItem;
                taiKhoan.MatKhau = HashHelper.HashPassword(txtMatKhau.Text);
                taiKhoan.VaiTro = cmbVaiTro.Text;

                if (taiKhoanController.CapNhatTaiKhoan(taiKhoan))
                {
                    MessageBox.Show(Constants.Messages.CapNhatThanhCong);
                    LoadData();
                }
                else
                {
                    MessageBox.Show(Constants.Messages.DuLieuKhongHopLe);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvTaiKhoan.CurrentRow != null)
            {
                var taiKhoan = (TaiKhoan)dgvTaiKhoan.CurrentRow.DataBoundItem;
                if (MessageBox.Show(Constants.Messages.XacNhanXoa, "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (taiKhoanController.XoaTaiKhoan(taiKhoan.TenDangNhap))
                    {
                        MessageBox.Show(Constants.Messages.XoaThanhCong);
                        LoadData();
                    }
                }
            }
        }

        private void ClearForm()
        {
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
            cmbVaiTro.SelectedIndex = -1;
            cmbSinhVien.SelectedIndex = -1;
            cmbGiangVien.SelectedIndex = -1;
        }

        private void cmbVaiTro_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSinhVien.Visible = cmbVaiTro.Text == Constants.UserRoles.SinhVien;
            cmbGiangVien.Visible = cmbVaiTro.Text == Constants.UserRoles.GiangVien;
        }

        private bool ValidateInput()
        {
            if (Validation.IsNullOrEmpty(txtTenDangNhap.Text) || !Validation.IsValidUsername(txtTenDangNhap.Text))
            {
                MessageBox.Show("Tên đăng nhập không hợp lệ (3-20 ký tự, chỉ chữ cái, số và _)");
                return false;
            }

            if (!Validation.IsValidPassword(txtMatKhau.Text))
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự");
                return false;
            }

            if (Validation.IsNullOrEmpty(cmbVaiTro.Text))
            {
                MessageBox.Show(Constants.Messages.VuiLongNhapDayDu);
                return false;
            }

            return true;
        }
    }
}