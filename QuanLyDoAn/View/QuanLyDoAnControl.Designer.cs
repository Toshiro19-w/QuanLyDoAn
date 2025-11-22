namespace QuanLyDoAn.View
{
    partial class QuanLyDoAnControl
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dgvDoAn;
        private TextBox txtMaDeTai;
        private TextBox txtTenDeTai;
        private ComboBox cmbMaSv;
        private ComboBox cmbMaGv;
        private ComboBox cmbMaLoai;
        private Label lblSinhVien;
        private Label lblLoaiDoAn;
        private TextBox txtDiem;
        private TextBox txtTimKiem;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;

        private Label lblMaDeTai;
        private Label lblTenDeTai;
        private Label lblMaSv;
        private Label lblMaGv;
        private Label lblNgayBatDau;
        private Label lblNgayKetThuc;
        private Label lblDiem;
        private Label lblTimKiem;


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
            dgvDoAn = new DataGridView();

            txtMaDeTai = new TextBox();
            txtTenDeTai = new TextBox();
            cmbMaSv = new ComboBox();
            cmbMaGv = new ComboBox();
            cmbMaLoai = new ComboBox();
            lblSinhVien = new Label();
            lblLoaiDoAn = new Label();
            txtDiem = new TextBox();
            txtTimKiem = new TextBox();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();

            lblMaDeTai = new Label();
            lblTenDeTai = new Label();
            lblMaSv = new Label();
            lblMaGv = new Label();
            lblNgayBatDau = new Label();
            lblNgayKetThuc = new Label();
            lblDiem = new Label();
            lblTimKiem = new Label();
            dtpNgayBatDau = new DateTimePicker();
            dtpNgayKetThuc = new DateTimePicker();
            label1 = new Label();
            txtMoTa = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvDoAn).BeginInit();
            SuspendLayout();
            // 
            // dgvDoAn
            // 
            dgvDoAn.AllowUserToAddRows = false;
            dgvDoAn.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvDoAn.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDoAn.ColumnHeadersHeight = 29;

            dgvDoAn.Location = new Point(3, 302);
            dgvDoAn.Name = "dgvDoAn";
            dgvDoAn.RowHeadersWidth = 51;
            dgvDoAn.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDoAn.Size = new Size(1368, 405);
            dgvDoAn.TabIndex = 3;
            dgvDoAn.CellClick += dgvDoAn_CellClick;
            // 
            // txtMaDeTai
            // 
            txtMaDeTai.Location = new Point(97, 51);
            txtMaDeTai.Name = "txtMaDeTai";
            txtMaDeTai.Size = new Size(150, 27);
            txtMaDeTai.TabIndex = 5;
            // 
            // lblLoaiDoAn
            // 
            lblLoaiDoAn.Location = new Point(260, 54);
            lblLoaiDoAn.Name = "lblLoaiDoAn";
            lblLoaiDoAn.Size = new Size(80, 23);
            lblLoaiDoAn.TabIndex = 6;
            lblLoaiDoAn.Text = "Loại đồ án:";
            // 
            // cmbMaLoai
            // 
            cmbMaLoai.Location = new Point(350, 51);
            cmbMaLoai.Name = "cmbMaLoai";
            cmbMaLoai.Size = new Size(156, 28);
            cmbMaLoai.TabIndex = 6;
            cmbMaLoai.DropDownStyle = ComboBoxStyle.DropDownList;
            // 
            // txtTenDeTai
            // 
            txtTenDeTai.Location = new Point(97, 84);
            txtTenDeTai.Name = "txtTenDeTai";
            txtTenDeTai.Size = new Size(409, 27);
            txtTenDeTai.TabIndex = 7;
            // 
            // cmbMaSv
            // 
            cmbMaSv.Location = new Point(406, 150);
            cmbMaSv.Name = "cmbMaSv";
            cmbMaSv.Size = new Size(155, 28);
            cmbMaSv.TabIndex = 28;
            // 
            // cmbMaGv
            // 
            cmbMaGv.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMaGv.Location = new Point(97, 117);
            cmbMaGv.Name = "cmbMaGv";
            cmbMaGv.Size = new Size(150, 28);
            cmbMaGv.TabIndex = 13;
            // 
            // lblSinhVien
            // 
            lblSinhVien.Location = new Point(406, 124);
            lblSinhVien.Name = "lblSinhVien";
            lblSinhVien.Size = new Size(72, 23);
            lblSinhVien.TabIndex = 29;
            lblSinhVien.Text = "Sinh viên:";
            // 
            // txtDiem
            // 
            txtDiem.Location = new Point(300, 118);
            txtDiem.Name = "txtDiem";
            txtDiem.Size = new Size(100, 27);
            txtDiem.TabIndex = 19;
            // 
            // txtTimKiem
            // 
            txtTimKiem.Location = new Point(141, 11);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(204, 27);
            txtTimKiem.TabIndex = 1;
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(108, 235);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(75, 36);
            btnThem.TabIndex = 20;
            btnThem.Text = "Thêm";
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(189, 235);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(75, 36);
            btnSua.TabIndex = 21;
            btnSua.Text = "Sửa";
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(270, 235);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(75, 36);
            btnXoa.TabIndex = 22;
            btnXoa.Text = "Xóa";
            btnXoa.Click += btnXoa_Click;
            // 
            // lblMaDeTai
            // 
            lblMaDeTai.Location = new Point(9, 54);
            lblMaDeTai.Name = "lblMaDeTai";
            lblMaDeTai.Size = new Size(80, 23);
            lblMaDeTai.TabIndex = 4;
            lblMaDeTai.Text = "Mã đề tài:";
            // 
            // lblTenDeTai
            // 
            lblTenDeTai.Location = new Point(9, 87);
            lblTenDeTai.Name = "lblTenDeTai";
            lblTenDeTai.Size = new Size(80, 23);
            lblTenDeTai.TabIndex = 6;
            lblTenDeTai.Text = "Tên đề tài:";
            // 
            // lblMaSv
            // 
            lblMaSv.Location = new Point(0, 0);
            lblMaSv.Name = "lblMaSv";
            lblMaSv.Size = new Size(100, 23);
            lblMaSv.TabIndex = 25;
            // 
            // lblMaGv
            // 
            lblMaGv.Location = new Point(9, 120);
            lblMaGv.Name = "lblMaGv";
            lblMaGv.Size = new Size(80, 23);
            lblMaGv.TabIndex = 12;
            lblMaGv.Text = "Giáo viên:";
            // 
            // lblNgayBatDau
            // 
            lblNgayBatDau.Location = new Point(7, 151);
            lblNgayBatDau.Name = "lblNgayBatDau";
            lblNgayBatDau.Size = new Size(106, 23);
            lblNgayBatDau.TabIndex = 14;
            lblNgayBatDau.Text = "Ngày bắt đầu:";
            // 
            // lblNgayKetThuc
            // 
            lblNgayKetThuc.Location = new Point(8, 185);
            lblNgayKetThuc.Name = "lblNgayKetThuc";
            lblNgayKetThuc.Size = new Size(106, 23);
            lblNgayKetThuc.TabIndex = 16;
            lblNgayKetThuc.Text = "Ngày kết thúc:";
            // 
            // lblDiem
            // 
            lblDiem.Location = new Point(253, 120);
            lblDiem.Name = "lblDiem";
            lblDiem.Size = new Size(50, 23);
            lblDiem.TabIndex = 18;
            lblDiem.Text = "Điểm:";
            // 
            // lblTimKiem
            // 
            lblTimKiem.Location = new Point(10, 15);
            lblTimKiem.Name = "lblTimKiem";
            lblTimKiem.Size = new Size(125, 23);
            lblTimKiem.TabIndex = 0;
            lblTimKiem.Text = "Tìm kiếm đồ án:";
            // 
            // dtpNgayBatDau
            // 
            dtpNgayBatDau.Location = new Point(120, 151);
            dtpNgayBatDau.Name = "dtpNgayBatDau";
            dtpNgayBatDau.Size = new Size(280, 27);
            dtpNgayBatDau.TabIndex = 23;
            // 
            // dtpNgayKetThuc
            // 
            dtpNgayKetThuc.Location = new Point(120, 185);
            dtpNgayKetThuc.Name = "dtpNgayKetThuc";
            dtpNgayKetThuc.Size = new Size(280, 27);
            dtpNgayKetThuc.TabIndex = 24;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(588, 11);
            label1.Name = "label1";
            label1.Size = new Size(51, 20);
            label1.TabIndex = 26;
            label1.Text = "Mô tả:";
            // 
            // txtMoTa
            // 
            txtMoTa.Location = new Point(588, 39);
            txtMoTa.Multiline = true;
            txtMoTa.Name = "txtMoTa";
            txtMoTa.Size = new Size(430, 246);
            txtMoTa.TabIndex = 27;
            // 
            // QuanLyDoAnControl
            // 
            Controls.Add(txtMoTa);
            Controls.Add(label1);
            Controls.Add(dtpNgayKetThuc);
            Controls.Add(dtpNgayBatDau);
            Controls.Add(lblTimKiem);
            Controls.Add(txtTimKiem);
            Controls.Add(dgvDoAn);
            Controls.Add(lblMaDeTai);
            Controls.Add(txtMaDeTai);
            Controls.Add(lblTenDeTai);
            Controls.Add(txtTenDeTai);
            Controls.Add(lblLoaiDoAn);
            Controls.Add(cmbMaLoai);
            Controls.Add(lblSinhVien);
            Controls.Add(cmbMaSv);
            Controls.Add(lblMaGv);
            Controls.Add(cmbMaGv);
            Controls.Add(lblNgayBatDau);
            Controls.Add(lblNgayKetThuc);
            Controls.Add(lblDiem);
            Controls.Add(txtDiem);
            Controls.Add(btnThem);
            Controls.Add(btnSua);
            Controls.Add(btnXoa);

            Name = "QuanLyDoAnControl";
            Size = new Size(1385, 816);
            ((System.ComponentModel.ISupportInitialize)dgvDoAn).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private DateTimePicker dtpNgayBatDau;
        private DateTimePicker dtpNgayKetThuc;
        private Label label1;
        private TextBox txtMoTa;
    }
}