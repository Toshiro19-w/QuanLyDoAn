namespace QuanLyDoAn.View
{
    partial class BaoCaoTienDoControl
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
            dgvBaoCao = new DataGridView();
            pnlThongKe = new Panel();
            lblTongDoAn = new Label();
            lblDaHoanThanh = new Label();
            lblDangThucHien = new Label();
            lblDiemTrungBinh = new Label();
            btnXuatBaoCao = new Button();
            lblThongKe = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvBaoCao).BeginInit();
            pnlThongKe.SuspendLayout();
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
            lblTitle.Text = "Báo cáo tiến độ";
            // 
            // dgvBaoCao
            // 
            dgvBaoCao.AllowUserToAddRows = false;
            dgvBaoCao.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBaoCao.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBaoCao.Location = new Point(30, 90);
            dgvBaoCao.Name = "dgvBaoCao";
            dgvBaoCao.ReadOnly = true;
            dgvBaoCao.RowHeadersWidth = 51;
            dgvBaoCao.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBaoCao.Size = new Size(900, 350);
            dgvBaoCao.TabIndex = 1;
            // 
            // pnlThongKe
            // 
            pnlThongKe.BackColor = Color.White;
            pnlThongKe.BorderStyle = BorderStyle.FixedSingle;
            pnlThongKe.Controls.Add(lblDiemTrungBinh);
            pnlThongKe.Controls.Add(lblDangThucHien);
            pnlThongKe.Controls.Add(lblDaHoanThanh);
            pnlThongKe.Controls.Add(lblTongDoAn);
            pnlThongKe.Location = new Point(30, 480);
            pnlThongKe.Name = "pnlThongKe";
            pnlThongKe.Size = new Size(600, 120);
            pnlThongKe.TabIndex = 2;
            // 
            // lblTongDoAn
            // 
            lblTongDoAn.AutoSize = true;
            lblTongDoAn.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTongDoAn.Location = new Point(20, 20);
            lblTongDoAn.Name = "lblTongDoAn";
            lblTongDoAn.Size = new Size(118, 23);
            lblTongDoAn.TabIndex = 0;
            lblTongDoAn.Text = "Tổng số đồ án:";
            // 
            // lblDaHoanThanh
            // 
            lblDaHoanThanh.AutoSize = true;
            lblDaHoanThanh.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDaHoanThanh.Location = new Point(300, 20);
            lblDaHoanThanh.Name = "lblDaHoanThanh";
            lblDaHoanThanh.Size = new Size(130, 23);
            lblDaHoanThanh.TabIndex = 1;
            lblDaHoanThanh.Text = "Đã hoàn thành:";
            // 
            // lblDangThucHien
            // 
            lblDangThucHien.AutoSize = true;
            lblDangThucHien.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDangThucHien.Location = new Point(20, 60);
            lblDangThucHien.Name = "lblDangThucHien";
            lblDangThucHien.Size = new Size(130, 23);
            lblDangThucHien.TabIndex = 2;
            lblDangThucHien.Text = "Đang thực hiện:";
            // 
            // lblDiemTrungBinh
            // 
            lblDiemTrungBinh.AutoSize = true;
            lblDiemTrungBinh.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDiemTrungBinh.Location = new Point(300, 60);
            lblDiemTrungBinh.Name = "lblDiemTrungBinh";
            lblDiemTrungBinh.Size = new Size(143, 23);
            lblDiemTrungBinh.TabIndex = 3;
            lblDiemTrungBinh.Text = "Điểm trung bình:";
            // 
            // btnXuatBaoCao
            // 
            btnXuatBaoCao.BackColor = Color.FromArgb(46, 204, 113);
            btnXuatBaoCao.FlatStyle = FlatStyle.Flat;
            btnXuatBaoCao.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnXuatBaoCao.ForeColor = Color.White;
            btnXuatBaoCao.Location = new Point(780, 520);
            btnXuatBaoCao.Name = "btnXuatBaoCao";
            btnXuatBaoCao.Size = new Size(150, 45);
            btnXuatBaoCao.TabIndex = 3;
            btnXuatBaoCao.Text = "Xuất báo cáo";
            btnXuatBaoCao.UseVisualStyleBackColor = false;
            btnXuatBaoCao.Click += btnXuatBaoCao_Click;
            // 
            // lblThongKe
            // 
            lblThongKe.AutoSize = true;
            lblThongKe.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblThongKe.Location = new Point(30, 450);
            lblThongKe.Name = "lblThongKe";
            lblThongKe.Size = new Size(95, 28);
            lblThongKe.TabIndex = 4;
            lblThongKe.Text = "Thống kê";
            // 
            // BaoCaoTienDoControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(236, 240, 241);
            Controls.Add(lblThongKe);
            Controls.Add(btnXuatBaoCao);
            Controls.Add(pnlThongKe);
            Controls.Add(dgvBaoCao);
            Controls.Add(lblTitle);
            Name = "BaoCaoTienDoControl";
            Size = new Size(1000, 650);
            ((System.ComponentModel.ISupportInitialize)dgvBaoCao).EndInit();
            pnlThongKe.ResumeLayout(false);
            pnlThongKe.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private DataGridView dgvBaoCao;
        private Panel pnlThongKe;
        private Label lblTongDoAn;
        private Label lblDaHoanThanh;
        private Label lblDangThucHien;
        private Label lblDiemTrungBinh;
        private Button btnXuatBaoCao;
        private Label lblThongKe;
    }
}