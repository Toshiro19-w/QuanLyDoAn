namespace QuanLyDoAn.View
{
    partial class QuanLyTaiKhoanControl
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dgvTaiKhoan;
        private TextBox txtTenDangNhap;
        private TextBox txtMatKhau;
        private ComboBox cmbVaiTro;
        private ComboBox cmbSinhVien;
        private ComboBox cmbGiangVien;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Label lblTenDangNhap;
        private Label lblMatKhau;
        private Label lblVaiTro;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dgvTaiKhoan = new DataGridView();
            txtTenDangNhap = new TextBox();
            txtMatKhau = new TextBox();
            cmbVaiTro = new ComboBox();
            cmbSinhVien = new ComboBox();
            cmbGiangVien = new ComboBox();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            lblTenDangNhap = new Label();
            lblMatKhau = new Label();
            lblVaiTro = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvTaiKhoan).BeginInit();
            SuspendLayout();
            // 
            // dgvTaiKhoan
            // 
            dgvTaiKhoan.AllowUserToAddRows = false;
            dgvTaiKhoan.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvTaiKhoan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTaiKhoan.ColumnHeadersHeight = 29;
            dgvTaiKhoan.Location = new Point(3, 12);
            dgvTaiKhoan.Name = "dgvTaiKhoan";
            dgvTaiKhoan.RowHeadersWidth = 51;
            dgvTaiKhoan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTaiKhoan.Size = new Size(1116, 397);
            dgvTaiKhoan.TabIndex = 0;
            // 
            // txtTenDangNhap
            // 
            txtTenDangNhap.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtTenDangNhap.Location = new Point(167, 423);
            txtTenDangNhap.Name = "txtTenDangNhap";
            txtTenDangNhap.Size = new Size(150, 27);
            txtTenDangNhap.TabIndex = 2;
            // 
            // txtMatKhau
            // 
            txtMatKhau.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtMatKhau.Location = new Point(167, 453);
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.Size = new Size(150, 27);
            txtMatKhau.TabIndex = 4;
            // 
            // cmbVaiTro
            // 
            cmbVaiTro.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cmbVaiTro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbVaiTro.Location = new Point(167, 483);
            cmbVaiTro.Name = "cmbVaiTro";
            cmbVaiTro.Size = new Size(150, 28);
            cmbVaiTro.TabIndex = 6;
            cmbVaiTro.SelectedIndexChanged += cmbVaiTro_SelectedIndexChanged;
            // 
            // cmbSinhVien
            // 
            cmbSinhVien.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cmbSinhVien.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSinhVien.Location = new Point(327, 483);
            cmbSinhVien.Name = "cmbSinhVien";
            cmbSinhVien.Size = new Size(150, 28);
            cmbSinhVien.TabIndex = 7;
            cmbSinhVien.Visible = false;
            // 
            // cmbGiangVien
            // 
            cmbGiangVien.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cmbGiangVien.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGiangVien.Location = new Point(327, 483);
            cmbGiangVien.Name = "cmbGiangVien";
            cmbGiangVien.Size = new Size(150, 28);
            cmbGiangVien.TabIndex = 8;
            cmbGiangVien.Visible = false;
            // 
            // btnThem
            // 
            btnThem.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnThem.Location = new Point(497, 423);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(75, 27);
            btnThem.TabIndex = 9;
            btnThem.Text = "Thêm";
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSua.Location = new Point(580, 423);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(75, 27);
            btnSua.TabIndex = 10;
            btnSua.Text = "Sửa";
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnXoa.Location = new Point(661, 423);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(75, 27);
            btnXoa.TabIndex = 11;
            btnXoa.Text = "Xóa";
            btnXoa.Click += btnXoa_Click;
            // 
            // lblTenDangNhap
            // 
            lblTenDangNhap.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblTenDangNhap.Location = new Point(10, 426);
            lblTenDangNhap.Name = "lblTenDangNhap";
            lblTenDangNhap.Size = new Size(133, 23);
            lblTenDangNhap.TabIndex = 1;
            lblTenDangNhap.Text = "Tên đăng nhập:";
            // 
            // lblMatKhau
            // 
            lblMatKhau.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblMatKhau.Location = new Point(10, 456);
            lblMatKhau.Name = "lblMatKhau";
            lblMatKhau.Size = new Size(100, 23);
            lblMatKhau.TabIndex = 3;
            lblMatKhau.Text = "Mật khẩu:";
            // 
            // lblVaiTro
            // 
            lblVaiTro.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblVaiTro.Location = new Point(10, 486);
            lblVaiTro.Name = "lblVaiTro";
            lblVaiTro.Size = new Size(100, 23);
            lblVaiTro.TabIndex = 5;
            lblVaiTro.Text = "Vai trò:";
            // 
            // QuanLyTaiKhoanControl
            // 
            Controls.Add(dgvTaiKhoan);
            Controls.Add(lblTenDangNhap);
            Controls.Add(txtTenDangNhap);
            Controls.Add(lblMatKhau);
            Controls.Add(txtMatKhau);
            Controls.Add(lblVaiTro);
            Controls.Add(cmbVaiTro);
            Controls.Add(cmbSinhVien);
            Controls.Add(cmbGiangVien);
            Controls.Add(btnThem);
            Controls.Add(btnSua);
            Controls.Add(btnXoa);
            Name = "QuanLyTaiKhoanControl";
            Size = new Size(1131, 763);
            ((System.ComponentModel.ISupportInitialize)dgvTaiKhoan).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}