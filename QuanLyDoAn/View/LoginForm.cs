using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyDoAn.Controller;
using QuanLyDoAn.Model.Entities;

namespace QuanLyDoAn
{
    public partial class LoginForm : Form
    {
        private TaiKhoanController taiKhoanController;

        public LoginForm()
        {
            InitializeComponent();
            taiKhoanController = new TaiKhoanController();
            Utils.ThemeHelper.ApplyTheme(this);
            
            // Ẩn textbox mật khẩu
            txtMatKhau.UseSystemPasswordChar = true;
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTenDangNhap.Text;
            string matKhau = txtMatKhau.Text;
            
            // Debug: Hiển thị hash
            string hash = Utils.HashHelper.HashPassword(matKhau);
            System.Diagnostics.Debug.WriteLine($"Password: {matKhau}");
            System.Diagnostics.Debug.WriteLine($"Hash: {hash}");

            var taiKhoan = taiKhoanController.DangNhap(tenDangNhap, matKhau);
            if (taiKhoan != null)
            {
                UserSession.CurrentUser = taiKhoan;
                
                MainForm mainForm = new MainForm();
                this.Hide();
                mainForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác.\nVui lòng kiểm tra lại thông tin đăng nhập.", 
                    "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Clear();
                txtMatKhau.Focus();
            }
        }
    }

    public static class UserSession
    {
        public static TaiKhoan? CurrentUser { get; set; }
    }
}
