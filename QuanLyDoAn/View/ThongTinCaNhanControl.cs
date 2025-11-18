using System;
using System.Windows.Forms;
using QuanLyDoAn.Controller;
using QuanLyDoAn.Model.Entities;
using QuanLyDoAn.Utils;

namespace QuanLyDoAn.View
{
    public partial class ThongTinCaNhanControl : UserControl
    {
        private TaiKhoanController taiKhoanController;

        public ThongTinCaNhanControl()
        {
            InitializeComponent();
            taiKhoanController = new TaiKhoanController();
            ThemeHelper.ApplyTheme(this);
            LoadThongTin();
        }

        private void LoadThongTin()
        {
            var user = UserSession.CurrentUser;
            if (user == null) return;

            // Thông tin tài khoản
            txtTenDangNhap.Text = user.TenDangNhap;
            txtVaiTro.Text = user.VaiTro;

            // Thông tin cá nhân theo vai trò
            if (user.VaiTro == Constants.UserRoles.SinhVien && user.MaSvNavigation != null)
            {
                var sv = user.MaSvNavigation;
                txtMa.Text = sv.MaSv;
                txtHoTen.Text = sv.HoTen;
                txtEmail.Text = sv.Email ?? "";
                txtSoDienThoai.Text = sv.SoDienThoai ?? "";
                txtDiaChi.Text = sv.Lop ?? "";
                txtNgaySinh.Text = sv.NgaySinh?.ToString("dd/MM/yyyy") ?? "";
            }
            else if (user.VaiTro == Constants.UserRoles.GiangVien && user.MaGvNavigation != null)
            {
                var gv = user.MaGvNavigation;
                txtMa.Text = gv.MaGv;
                txtHoTen.Text = gv.HoTen;
                txtEmail.Text = gv.Email ?? "";
                txtSoDienThoai.Text = gv.ChucVu ?? "";
                txtDiaChi.Text = gv.BoMon ?? "";
                txtNgaySinh.Text = "";
            }
            else if (user.VaiTro == Constants.UserRoles.Admin)
            {
                txtMa.Text = "ADMIN";
                txtHoTen.Text = user.TenDangNhap;
                txtEmail.Text = "";
                txtSoDienThoai.Text = "";
                txtDiaChi.Text = "";
                txtNgaySinh.Text = "";
            }
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMatKhauMoi.Text) || string.IsNullOrEmpty(txtXacNhanMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ mật khẩu mới!");
                return;
            }

            if (txtMatKhauMoi.Text != txtXacNhanMatKhau.Text)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!");
                return;
            }

            if (!Validation.IsValidPassword(txtMatKhauMoi.Text))
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự!");
                return;
            }

            if (taiKhoanController.DoiMatKhau(UserSession.CurrentUser.TenDangNhap, txtMatKhauMoi.Text))
            {
                MessageBox.Show("Đổi mật khẩu thành công!");
                txtMatKhauMoi.Clear();
                txtXacNhanMatKhau.Clear();
            }
            else
            {
                MessageBox.Show("Đổi mật khẩu thất bại!");
            }
        }
    }
}