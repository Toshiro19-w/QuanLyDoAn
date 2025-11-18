namespace QuanLyDoAn.View
{
    partial class ThongBaoSinhVienControl
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
            dgvThongBao = new DataGridView();
            btnLamMoi = new Button();
            lblSoThongBao = new Label();
            lblHuongDan = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvThongBao).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(30, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(138, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Thông báo";
            // 
            // lblTenDeTai
            // 
            lblTenDeTai.AutoSize = true;
            lblTenDeTai.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTenDeTai.Location = new Point(30, 80);
            lblTenDeTai.Name = "lblTenDeTai";
            lblTenDeTai.Size = new Size(73, 28);
            lblTenDeTai.TabIndex = 1;
            lblTenDeTai.Text = "Đồ án:";
            // 
            // dgvThongBao
            // 
            dgvThongBao.AllowUserToAddRows = false;
            dgvThongBao.AllowUserToDeleteRows = false;
            dgvThongBao.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvThongBao.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvThongBao.Location = new Point(30, 170);
            dgvThongBao.Name = "dgvThongBao";
            dgvThongBao.ReadOnly = true;
            dgvThongBao.RowHeadersWidth = 51;
            dgvThongBao.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvThongBao.Size = new Size(840, 450);
            dgvThongBao.TabIndex = 2;
            dgvThongBao.CellDoubleClick += dgvThongBao_CellDoubleClick;
            // 
            // btnLamMoi
            // 
            btnLamMoi.BackColor = Color.FromArgb(52, 152, 219);
            btnLamMoi.FlatStyle = FlatStyle.Flat;
            btnLamMoi.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLamMoi.ForeColor = Color.White;
            btnLamMoi.Location = new Point(750, 120);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(120, 40);
            btnLamMoi.TabIndex = 3;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = false;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // lblSoThongBao
            // 
            lblSoThongBao.AutoSize = true;
            lblSoThongBao.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSoThongBao.Location = new Point(30, 130);
            lblSoThongBao.Name = "lblSoThongBao";
            lblSoThongBao.Size = new Size(168, 23);
            lblSoThongBao.TabIndex = 4;
            lblSoThongBao.Text = "Tổng số thông báo: 0";
            // 
            // lblHuongDan
            // 
            lblHuongDan.AutoSize = true;
            lblHuongDan.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            lblHuongDan.ForeColor = Color.Gray;
            lblHuongDan.Location = new Point(30, 630);
            lblHuongDan.Name = "lblHuongDan";
            lblHuongDan.Size = new Size(334, 20);
            lblHuongDan.TabIndex = 5;
            lblHuongDan.Text = "* Nhấp đúp vào thông báo để xem chi tiết đầy đủ";
            // 
            // ThongBaoSinhVienControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(236, 240, 241);
            Controls.Add(lblHuongDan);
            Controls.Add(lblSoThongBao);
            Controls.Add(btnLamMoi);
            Controls.Add(dgvThongBao);
            Controls.Add(lblTenDeTai);
            Controls.Add(lblTitle);
            Name = "ThongBaoSinhVienControl";
            Size = new Size(900, 700);
            ((System.ComponentModel.ISupportInitialize)dgvThongBao).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle;
        private Label lblTenDeTai;
        private DataGridView dgvThongBao;
        private Button btnLamMoi;
        private Label lblSoThongBao;
        private Label lblHuongDan;
    }
}
