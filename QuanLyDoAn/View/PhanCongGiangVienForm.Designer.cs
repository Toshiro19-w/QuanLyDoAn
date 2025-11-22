namespace QuanLyDoAn.View
{
    partial class PhanCongGiangVienForm
    {
        private System.ComponentModel.IContainer components = null;

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
            lblTitle = new Label();
            lblThongTin = new Label();
            lblGiangVien = new Label();
            cboGiangVien = new ComboBox();
            lblLoaiDanhGia = new Label();
            cboLoaiDanhGia = new ComboBox();
            btnPhanCong = new Button();
            lblDanhSach = new Label();
            dgvPhanCong = new DataGridView();
            btnHuyPhanCong = new Button();
            btnDong = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvPhanCong).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(268, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Phân công giảng viên";
            // 
            // lblThongTin
            // 
            lblThongTin.AutoSize = true;
            lblThongTin.Font = new Font("Segoe UI", 10F);
            lblThongTin.Location = new Point(20, 60);
            lblThongTin.Name = "lblThongTin";
            lblThongTin.Size = new Size(100, 23);
            lblThongTin.TabIndex = 1;
            lblThongTin.Text = "Đồ án: ...";
            // 
            // lblGiangVien
            // 
            lblGiangVien.AutoSize = true;
            lblGiangVien.Location = new Point(20, 100);
            lblGiangVien.Name = "lblGiangVien";
            lblGiangVien.Size = new Size(86, 20);
            lblGiangVien.TabIndex = 2;
            lblGiangVien.Text = "Giảng viên:";
            // 
            // cboGiangVien
            // 
            cboGiangVien.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGiangVien.FormattingEnabled = true;
            cboGiangVien.Location = new Point(140, 97);
            cboGiangVien.Name = "cboGiangVien";
            cboGiangVien.Size = new Size(300, 28);
            cboGiangVien.TabIndex = 3;
            // 
            // lblLoaiDanhGia
            // 
            lblLoaiDanhGia.AutoSize = true;
            lblLoaiDanhGia.Location = new Point(20, 140);
            lblLoaiDanhGia.Name = "lblLoaiDanhGia";
            lblLoaiDanhGia.Size = new Size(107, 20);
            lblLoaiDanhGia.TabIndex = 4;
            lblLoaiDanhGia.Text = "Loại đánh giá:";
            // 
            // cboLoaiDanhGia
            // 
            cboLoaiDanhGia.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoaiDanhGia.FormattingEnabled = true;
            cboLoaiDanhGia.Location = new Point(140, 137);
            cboLoaiDanhGia.Name = "cboLoaiDanhGia";
            cboLoaiDanhGia.Size = new Size(200, 28);
            cboLoaiDanhGia.TabIndex = 5;
            // 
            // btnPhanCong
            // 
            btnPhanCong.BackColor = Color.FromArgb(46, 204, 113);
            btnPhanCong.FlatStyle = FlatStyle.Flat;
            btnPhanCong.ForeColor = Color.White;
            btnPhanCong.Location = new Point(360, 133);
            btnPhanCong.Name = "btnPhanCong";
            btnPhanCong.Size = new Size(120, 35);
            btnPhanCong.TabIndex = 6;
            btnPhanCong.Text = "Phân công";
            btnPhanCong.UseVisualStyleBackColor = false;
            btnPhanCong.Click += BtnPhanCong_Click;
            // 
            // lblDanhSach
            // 
            lblDanhSach.AutoSize = true;
            lblDanhSach.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDanhSach.Location = new Point(20, 190);
            lblDanhSach.Name = "lblDanhSach";
            lblDanhSach.Size = new Size(213, 23);
            lblDanhSach.TabIndex = 7;
            lblDanhSach.Text = "Danh sách đã phân công:";
            // 
            // dgvPhanCong
            // 
            dgvPhanCong.AllowUserToAddRows = false;
            dgvPhanCong.AllowUserToDeleteRows = false;
            dgvPhanCong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPhanCong.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPhanCong.Location = new Point(20, 220);
            dgvPhanCong.Name = "dgvPhanCong";
            dgvPhanCong.ReadOnly = true;
            dgvPhanCong.RowHeadersWidth = 51;
            dgvPhanCong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPhanCong.Size = new Size(760, 250);
            dgvPhanCong.TabIndex = 8;
            // 
            // btnHuyPhanCong
            // 
            btnHuyPhanCong.BackColor = Color.FromArgb(231, 76, 60);
            btnHuyPhanCong.FlatStyle = FlatStyle.Flat;
            btnHuyPhanCong.ForeColor = Color.White;
            btnHuyPhanCong.Location = new Point(540, 485);
            btnHuyPhanCong.Name = "btnHuyPhanCong";
            btnHuyPhanCong.Size = new Size(120, 35);
            btnHuyPhanCong.TabIndex = 9;
            btnHuyPhanCong.Text = "Hủy phân công";
            btnHuyPhanCong.UseVisualStyleBackColor = false;
            btnHuyPhanCong.Click += BtnHuyPhanCong_Click;
            // 
            // btnDong
            // 
            btnDong.BackColor = Color.FromArgb(149, 165, 166);
            btnDong.FlatStyle = FlatStyle.Flat;
            btnDong.ForeColor = Color.White;
            btnDong.Location = new Point(670, 485);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(110, 35);
            btnDong.TabIndex = 10;
            btnDong.Text = "Đóng";
            btnDong.UseVisualStyleBackColor = false;
            btnDong.Click += BtnDong_Click;
            // 
            // PhanCongGiangVienForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(236, 240, 241);
            ClientSize = new Size(800, 540);
            Controls.Add(btnDong);
            Controls.Add(btnHuyPhanCong);
            Controls.Add(dgvPhanCong);
            Controls.Add(lblDanhSach);
            Controls.Add(btnPhanCong);
            Controls.Add(cboLoaiDanhGia);
            Controls.Add(lblLoaiDanhGia);
            Controls.Add(cboGiangVien);
            Controls.Add(lblGiangVien);
            Controls.Add(lblThongTin);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PhanCongGiangVienForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Phân công giảng viên";
            ((System.ComponentModel.ISupportInitialize)dgvPhanCong).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle;
        private Label lblThongTin;
        private Label lblGiangVien;
        private ComboBox cboGiangVien;
        private Label lblLoaiDanhGia;
        private ComboBox cboLoaiDanhGia;
        private Button btnPhanCong;
        private Label lblDanhSach;
        private DataGridView dgvPhanCong;
        private Button btnHuyPhanCong;
        private Button btnDong;
    }
}
