namespace QuanLyDoAn.View
{
    partial class NopTaiLieuControl
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
            lblTenDeTai = new Label();
            lblTenTaiLieu = new Label();
            txtTenTaiLieu = new TextBox();
            lblDuongDan = new Label();
            txtDuongDan = new TextBox();
            btnChonFile = new Button();
            btnNopTaiLieu = new Button();
            lblDanhSach = new Label();
            dgvTaiLieu = new DataGridView();
            btnMoFile = new Button();
            lblSoTaiLieu = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvTaiLieu).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(30, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(167, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Nộp tài liệu";
            // 
            // lblTenDeTai
            // 
            lblTenDeTai.AutoSize = true;
            lblTenDeTai.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTenDeTai.Location = new Point(30, 80);
            lblTenDeTai.Name = "lblTenDeTai";
            lblTenDeTai.Size = new Size(68, 28);
            lblTenDeTai.TabIndex = 1;
            lblTenDeTai.Text = "Đồ án:";
            // 
            // lblTenTaiLieu
            // 
            lblTenTaiLieu.AutoSize = true;
            lblTenTaiLieu.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTenTaiLieu.Location = new Point(30, 130);
            lblTenTaiLieu.Name = "lblTenTaiLieu";
            lblTenTaiLieu.Size = new Size(96, 23);
            lblTenTaiLieu.TabIndex = 2;
            lblTenTaiLieu.Text = "Tên tài liệu:";
            // 
            // txtTenTaiLieu
            // 
            txtTenTaiLieu.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTenTaiLieu.Location = new Point(150, 127);
            txtTenTaiLieu.Name = "txtTenTaiLieu";
            txtTenTaiLieu.Size = new Size(520, 30);
            txtTenTaiLieu.TabIndex = 3;
            // 
            // lblDuongDan
            // 
            lblDuongDan.AutoSize = true;
            lblDuongDan.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDuongDan.Location = new Point(30, 180);
            lblDuongDan.Name = "lblDuongDan";
            lblDuongDan.Size = new Size(100, 23);
            lblDuongDan.TabIndex = 4;
            lblDuongDan.Text = "Đường dẫn:";
            // 
            // txtDuongDan
            // 
            txtDuongDan.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDuongDan.Location = new Point(150, 177);
            txtDuongDan.Name = "txtDuongDan";
            txtDuongDan.ReadOnly = true;
            txtDuongDan.Size = new Size(400, 30);
            txtDuongDan.TabIndex = 5;
            // 
            // btnChonFile
            // 
            btnChonFile.BackColor = Color.FromArgb(52, 152, 219);
            btnChonFile.FlatStyle = FlatStyle.Flat;
            btnChonFile.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnChonFile.ForeColor = Color.White;
            btnChonFile.Location = new Point(560, 175);
            btnChonFile.Name = "btnChonFile";
            btnChonFile.Size = new Size(110, 35);
            btnChonFile.TabIndex = 6;
            btnChonFile.Text = "Chọn file";
            btnChonFile.UseVisualStyleBackColor = false;
            btnChonFile.Click += btnChonFile_Click;
            // 
            // btnNopTaiLieu
            // 
            btnNopTaiLieu.BackColor = Color.FromArgb(46, 204, 113);
            btnNopTaiLieu.FlatStyle = FlatStyle.Flat;
            btnNopTaiLieu.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnNopTaiLieu.ForeColor = Color.White;
            btnNopTaiLieu.Location = new Point(520, 230);
            btnNopTaiLieu.Name = "btnNopTaiLieu";
            btnNopTaiLieu.Size = new Size(150, 45);
            btnNopTaiLieu.TabIndex = 7;
            btnNopTaiLieu.Text = "Nộp tài liệu";
            btnNopTaiLieu.UseVisualStyleBackColor = false;
            btnNopTaiLieu.Click += btnNopTaiLieu_Click;
            // 
            // lblDanhSach
            // 
            lblDanhSach.AutoSize = true;
            lblDanhSach.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDanhSach.Location = new Point(30, 300);
            lblDanhSach.Name = "lblDanhSach";
            lblDanhSach.Size = new Size(157, 28);
            lblDanhSach.TabIndex = 8;
            lblDanhSach.Text = "Tài liệu đã nộp:";
            // 
            // dgvTaiLieu
            // 
            dgvTaiLieu.AllowUserToAddRows = false;
            dgvTaiLieu.AllowUserToDeleteRows = false;
            dgvTaiLieu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTaiLieu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTaiLieu.Location = new Point(30, 370);
            dgvTaiLieu.Name = "dgvTaiLieu";
            dgvTaiLieu.ReadOnly = true;
            dgvTaiLieu.RowHeadersWidth = 51;
            dgvTaiLieu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTaiLieu.Size = new Size(840, 280);
            dgvTaiLieu.TabIndex = 9;
            // 
            // btnMoFile
            // 
            btnMoFile.BackColor = Color.FromArgb(52, 152, 219);
            btnMoFile.FlatStyle = FlatStyle.Flat;
            btnMoFile.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnMoFile.ForeColor = Color.White;
            btnMoFile.Location = new Point(750, 325);
            btnMoFile.Name = "btnMoFile";
            btnMoFile.Size = new Size(120, 40);
            btnMoFile.TabIndex = 10;
            btnMoFile.Text = "Mở file";
            btnMoFile.UseVisualStyleBackColor = false;
            btnMoFile.Click += btnMoFile_Click;
            // 
            // lblSoTaiLieu
            // 
            lblSoTaiLieu.AutoSize = true;
            lblSoTaiLieu.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSoTaiLieu.Location = new Point(30, 335);
            lblSoTaiLieu.Name = "lblSoTaiLieu";
            lblSoTaiLieu.Size = new Size(145, 23);
            lblSoTaiLieu.TabIndex = 11;
            lblSoTaiLieu.Text = "Tổng số tài liệu: 0";
            // 
            // NopTaiLieuControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(236, 240, 241);
            Controls.Add(lblSoTaiLieu);
            Controls.Add(btnMoFile);
            Controls.Add(dgvTaiLieu);
            Controls.Add(lblDanhSach);
            Controls.Add(btnNopTaiLieu);
            Controls.Add(btnChonFile);
            Controls.Add(txtDuongDan);
            Controls.Add(lblDuongDan);
            Controls.Add(txtTenTaiLieu);
            Controls.Add(lblTenTaiLieu);
            Controls.Add(lblTenDeTai);
            Controls.Add(lblTitle);
            Name = "NopTaiLieuControl";
            Size = new Size(900, 700);
            ((System.ComponentModel.ISupportInitialize)dgvTaiLieu).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle;
        private Label lblTenDeTai;
        private Label lblTenTaiLieu;
        private TextBox txtTenTaiLieu;
        private Label lblDuongDan;
        private TextBox txtDuongDan;
        private Button btnChonFile;
        private Button btnNopTaiLieu;
        private Label lblDanhSach;
        private DataGridView dgvTaiLieu;
        private Button btnMoFile;
        private Label lblSoTaiLieu;
    }
}
