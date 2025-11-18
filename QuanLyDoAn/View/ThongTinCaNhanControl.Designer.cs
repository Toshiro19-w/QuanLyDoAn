namespace QuanLyDoAn.View
{
    partial class ThongTinCaNhanControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitle = new Label();
            pnlThongTin = new Panel();
            lblMa = new Label();
            txtMa = new TextBox();
            lblHoTen = new Label();
            txtHoTen = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblSoDienThoai = new Label();
            txtSoDienThoai = new TextBox();
            lblDiaChi = new Label();
            txtDiaChi = new TextBox();
            lblNgaySinh = new Label();
            txtNgaySinh = new TextBox();
            lblTenDangNhap = new Label();
            txtTenDangNhap = new TextBox();
            lblVaiTro = new Label();
            txtVaiTro = new TextBox();
            pnlDoiMatKhau = new Panel();
            lblDoiMatKhau = new Label();
            lblMatKhauMoi = new Label();
            txtMatKhauMoi = new TextBox();
            lblXacNhanMatKhau = new Label();
            txtXacNhanMatKhau = new TextBox();
            btnDoiMatKhau = new Button();
            pnlThongTin.SuspendLayout();
            pnlDoiMatKhau.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(30, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(234, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Thông tin cá nhân";
            // 
            // pnlThongTin
            // 
            pnlThongTin.BackColor = Color.White;
            pnlThongTin.BorderStyle = BorderStyle.FixedSingle;
            pnlThongTin.Controls.Add(txtVaiTro);
            pnlThongTin.Controls.Add(lblVaiTro);
            pnlThongTin.Controls.Add(txtTenDangNhap);
            pnlThongTin.Controls.Add(lblTenDangNhap);
            pnlThongTin.Controls.Add(txtNgaySinh);
            pnlThongTin.Controls.Add(lblNgaySinh);
            pnlThongTin.Controls.Add(txtDiaChi);
            pnlThongTin.Controls.Add(lblDiaChi);
            pnlThongTin.Controls.Add(txtSoDienThoai);
            pnlThongTin.Controls.Add(lblSoDienThoai);
            pnlThongTin.Controls.Add(txtEmail);
            pnlThongTin.Controls.Add(lblEmail);
            pnlThongTin.Controls.Add(txtHoTen);
            pnlThongTin.Controls.Add(lblHoTen);
            pnlThongTin.Controls.Add(txtMa);
            pnlThongTin.Controls.Add(lblMa);
            pnlThongTin.Location = new Point(30, 90);
            pnlThongTin.Name = "pnlThongTin";
            pnlThongTin.Size = new Size(600, 350);
            pnlThongTin.TabIndex = 1;
            // 
            // lblMa
            // 
            lblMa.AutoSize = true;
            lblMa.Location = new Point(20, 20);
            lblMa.Name = "lblMa";
            lblMa.Size = new Size(35, 20);
            lblMa.TabIndex = 0;
            lblMa.Text = "Mã:";
            // 
            // txtMa
            // 
            txtMa.Location = new Point(150, 17);
            txtMa.Name = "txtMa";
            txtMa.ReadOnly = true;
            txtMa.Size = new Size(400, 27);
            txtMa.TabIndex = 1;
            // 
            // lblHoTen
            // 
            lblHoTen.AutoSize = true;
            lblHoTen.Location = new Point(20, 60);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(57, 20);
            lblHoTen.TabIndex = 2;
            lblHoTen.Text = "Họ tên:";
            // 
            // txtHoTen
            // 
            txtHoTen.Location = new Point(150, 57);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.ReadOnly = true;
            txtHoTen.Size = new Size(400, 27);
            txtHoTen.TabIndex = 3;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(20, 100);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(49, 20);
            lblEmail.TabIndex = 4;
            lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(150, 97);
            txtEmail.Name = "txtEmail";
            txtEmail.ReadOnly = true;
            txtEmail.Size = new Size(400, 27);
            txtEmail.TabIndex = 5;
            // 
            // lblSoDienThoai
            // 
            lblSoDienThoai.AutoSize = true;
            lblSoDienThoai.Location = new Point(20, 140);
            lblSoDienThoai.Name = "lblSoDienThoai";
            lblSoDienThoai.Size = new Size(120, 20);
            lblSoDienThoai.TabIndex = 6;
            lblSoDienThoai.Text = "SĐT/Chức vụ:";
            // 
            // txtSoDienThoai
            // 
            txtSoDienThoai.Location = new Point(150, 137);
            txtSoDienThoai.Name = "txtSoDienThoai";
            txtSoDienThoai.ReadOnly = true;
            txtSoDienThoai.Size = new Size(400, 27);
            txtSoDienThoai.TabIndex = 7;
            // 
            // lblDiaChi
            // 
            lblDiaChi.AutoSize = true;
            lblDiaChi.Location = new Point(20, 180);
            lblDiaChi.Name = "lblDiaChi";
            lblDiaChi.Size = new Size(100, 20);
            lblDiaChi.TabIndex = 8;
            lblDiaChi.Text = "Lớp/Bộ môn:";
            // 
            // txtDiaChi
            // 
            txtDiaChi.Location = new Point(150, 177);
            txtDiaChi.Name = "txtDiaChi";
            txtDiaChi.ReadOnly = true;
            txtDiaChi.Size = new Size(400, 27);
            txtDiaChi.TabIndex = 9;
            // 
            // lblNgaySinh
            // 
            lblNgaySinh.AutoSize = true;
            lblNgaySinh.Location = new Point(20, 220);
            lblNgaySinh.Name = "lblNgaySinh";
            lblNgaySinh.Size = new Size(76, 20);
            lblNgaySinh.TabIndex = 10;
            lblNgaySinh.Text = "Ngày sinh:";
            // 
            // txtNgaySinh
            // 
            txtNgaySinh.Location = new Point(150, 217);
            txtNgaySinh.Name = "txtNgaySinh";
            txtNgaySinh.ReadOnly = true;
            txtNgaySinh.Size = new Size(400, 27);
            txtNgaySinh.TabIndex = 11;
            // 
            // lblTenDangNhap
            // 
            lblTenDangNhap.AutoSize = true;
            lblTenDangNhap.Location = new Point(20, 260);
            lblTenDangNhap.Name = "lblTenDangNhap";
            lblTenDangNhap.Size = new Size(109, 20);
            lblTenDangNhap.TabIndex = 12;
            lblTenDangNhap.Text = "Tên đăng nhập:";
            // 
            // txtTenDangNhap
            // 
            txtTenDangNhap.Location = new Point(150, 257);
            txtTenDangNhap.Name = "txtTenDangNhap";
            txtTenDangNhap.ReadOnly = true;
            txtTenDangNhap.Size = new Size(400, 27);
            txtTenDangNhap.TabIndex = 13;
            // 
            // lblVaiTro
            // 
            lblVaiTro.AutoSize = true;
            lblVaiTro.Location = new Point(20, 300);
            lblVaiTro.Name = "lblVaiTro";
            lblVaiTro.Size = new Size(54, 20);
            lblVaiTro.TabIndex = 14;
            lblVaiTro.Text = "Vai trò:";
            // 
            // txtVaiTro
            // 
            txtVaiTro.Location = new Point(150, 297);
            txtVaiTro.Name = "txtVaiTro";
            txtVaiTro.ReadOnly = true;
            txtVaiTro.Size = new Size(400, 27);
            txtVaiTro.TabIndex = 15;
            // 
            // pnlDoiMatKhau
            // 
            pnlDoiMatKhau.BackColor = Color.White;
            pnlDoiMatKhau.BorderStyle = BorderStyle.FixedSingle;
            pnlDoiMatKhau.Controls.Add(btnDoiMatKhau);
            pnlDoiMatKhau.Controls.Add(txtXacNhanMatKhau);
            pnlDoiMatKhau.Controls.Add(lblXacNhanMatKhau);
            pnlDoiMatKhau.Controls.Add(txtMatKhauMoi);
            pnlDoiMatKhau.Controls.Add(lblMatKhauMoi);
            pnlDoiMatKhau.Controls.Add(lblDoiMatKhau);
            pnlDoiMatKhau.Location = new Point(30, 460);
            pnlDoiMatKhau.Name = "pnlDoiMatKhau";
            pnlDoiMatKhau.Size = new Size(600, 180);
            pnlDoiMatKhau.TabIndex = 2;
            // 
            // lblDoiMatKhau
            // 
            lblDoiMatKhau.AutoSize = true;
            lblDoiMatKhau.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDoiMatKhau.Location = new Point(20, 20);
            lblDoiMatKhau.Name = "lblDoiMatKhau";
            lblDoiMatKhau.Size = new Size(138, 28);
            lblDoiMatKhau.TabIndex = 0;
            lblDoiMatKhau.Text = "Đổi mật khẩu";
            // 
            // lblMatKhauMoi
            // 
            lblMatKhauMoi.AutoSize = true;
            lblMatKhauMoi.Location = new Point(20, 70);
            lblMatKhauMoi.Name = "lblMatKhauMoi";
            lblMatKhauMoi.Size = new Size(103, 20);
            lblMatKhauMoi.TabIndex = 1;
            lblMatKhauMoi.Text = "Mật khẩu mới:";
            // 
            // txtMatKhauMoi
            // 
            txtMatKhauMoi.Location = new Point(180, 67);
            txtMatKhauMoi.Name = "txtMatKhauMoi";
            txtMatKhauMoi.Size = new Size(370, 27);
            txtMatKhauMoi.TabIndex = 2;
            txtMatKhauMoi.UseSystemPasswordChar = true;
            // 
            // lblXacNhanMatKhau
            // 
            lblXacNhanMatKhau.AutoSize = true;
            lblXacNhanMatKhau.Location = new Point(20, 110);
            lblXacNhanMatKhau.Name = "lblXacNhanMatKhau";
            lblXacNhanMatKhau.Size = new Size(143, 20);
            lblXacNhanMatKhau.TabIndex = 3;
            lblXacNhanMatKhau.Text = "Xác nhận mật khẩu:";
            // 
            // txtXacNhanMatKhau
            // 
            txtXacNhanMatKhau.Location = new Point(180, 107);
            txtXacNhanMatKhau.Name = "txtXacNhanMatKhau";
            txtXacNhanMatKhau.Size = new Size(370, 27);
            txtXacNhanMatKhau.TabIndex = 4;
            txtXacNhanMatKhau.UseSystemPasswordChar = true;
            // 
            // btnDoiMatKhau
            // 
            btnDoiMatKhau.BackColor = Color.FromArgb(52, 152, 219);
            btnDoiMatKhau.FlatStyle = FlatStyle.Flat;
            btnDoiMatKhau.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDoiMatKhau.ForeColor = Color.White;
            btnDoiMatKhau.Location = new Point(420, 140);
            btnDoiMatKhau.Name = "btnDoiMatKhau";
            btnDoiMatKhau.Size = new Size(130, 35);
            btnDoiMatKhau.TabIndex = 5;
            btnDoiMatKhau.Text = "Đổi mật khẩu";
            btnDoiMatKhau.UseVisualStyleBackColor = false;
            btnDoiMatKhau.Click += btnDoiMatKhau_Click;
            // 
            // ThongTinCaNhanControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(236, 240, 241);
            Controls.Add(pnlDoiMatKhau);
            Controls.Add(pnlThongTin);
            Controls.Add(lblTitle);
            Name = "ThongTinCaNhanControl";
            Size = new Size(700, 680);
            pnlThongTin.ResumeLayout(false);
            pnlThongTin.PerformLayout();
            pnlDoiMatKhau.ResumeLayout(false);
            pnlDoiMatKhau.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Panel pnlThongTin;
        private Label lblMa;
        private TextBox txtMa;
        private Label lblHoTen;
        private TextBox txtHoTen;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblSoDienThoai;
        private TextBox txtSoDienThoai;
        private Label lblDiaChi;
        private TextBox txtDiaChi;
        private Label lblNgaySinh;
        private TextBox txtNgaySinh;
        private Label lblTenDangNhap;
        private TextBox txtTenDangNhap;
        private Label lblVaiTro;
        private TextBox txtVaiTro;
        private Panel pnlDoiMatKhau;
        private Label lblDoiMatKhau;
        private Label lblMatKhauMoi;
        private TextBox txtMatKhauMoi;
        private Label lblXacNhanMatKhau;
        private TextBox txtXacNhanMatKhau;
        private Button btnDoiMatKhau;
    }
}