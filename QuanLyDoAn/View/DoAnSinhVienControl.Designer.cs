namespace QuanLyDoAn.View
{
    partial class DoAnSinhVienControl
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
            lblTenDeTai = new Label();
            lblMoTa = new Label();
            lblGiangVien = new Label();
            lblLoaiDoAn = new Label();
            lblTrangThai = new Label();
            lblNgayBatDau = new Label();
            lblNgayKetThuc = new Label();
            lblDiem = new Label();
            lblTienDoTitle = new Label();
            dgvTienDo = new DataGridView();
            lblThongBaoTitle = new Label();
            dgvThongBao = new DataGridView();
            pnlThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTienDo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvThongBao).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(30, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(188, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Đồ án của tôi";
            // 
            // pnlThongTin
            // 
            pnlThongTin.BackColor = Color.White;
            pnlThongTin.BorderStyle = BorderStyle.FixedSingle;
            pnlThongTin.Controls.Add(lblDiem);
            pnlThongTin.Controls.Add(lblNgayKetThuc);
            pnlThongTin.Controls.Add(lblNgayBatDau);
            pnlThongTin.Controls.Add(lblTrangThai);
            pnlThongTin.Controls.Add(lblLoaiDoAn);
            pnlThongTin.Controls.Add(lblGiangVien);
            pnlThongTin.Controls.Add(lblMoTa);
            pnlThongTin.Controls.Add(lblTenDeTai);
            pnlThongTin.Location = new Point(30, 90);
            pnlThongTin.Name = "pnlThongTin";
            pnlThongTin.Size = new Size(800, 200);
            pnlThongTin.TabIndex = 1;
            // 
            // lblTenDeTai
            // 
            lblTenDeTai.AutoSize = true;
            lblTenDeTai.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTenDeTai.Location = new Point(20, 20);
            lblTenDeTai.Name = "lblTenDeTai";
            lblTenDeTai.Size = new Size(130, 32);
            lblTenDeTai.TabIndex = 0;
            lblTenDeTai.Text = "Tên đề tài";
            // 
            // lblMoTa
            // 
            lblMoTa.Location = new Point(20, 60);
            lblMoTa.Name = "lblMoTa";
            lblMoTa.Size = new Size(760, 40);
            lblMoTa.TabIndex = 1;
            lblMoTa.Text = "Mô tả";
            // 
            // lblGiangVien
            // 
            lblGiangVien.AutoSize = true;
            lblGiangVien.Location = new Point(20, 110);
            lblGiangVien.Name = "lblGiangVien";
            lblGiangVien.Size = new Size(85, 20);
            lblGiangVien.TabIndex = 2;
            lblGiangVien.Text = "Giảng viên:";
            // 
            // lblLoaiDoAn
            // 
            lblLoaiDoAn.AutoSize = true;
            lblLoaiDoAn.Location = new Point(250, 110);
            lblLoaiDoAn.Name = "lblLoaiDoAn";
            lblLoaiDoAn.Size = new Size(82, 20);
            lblLoaiDoAn.TabIndex = 3;
            lblLoaiDoAn.Text = "Loại đồ án:";
            // 
            // lblTrangThai
            // 
            lblTrangThai.AutoSize = true;
            lblTrangThai.Location = new Point(450, 110);
            lblTrangThai.Name = "lblTrangThai";
            lblTrangThai.Size = new Size(78, 20);
            lblTrangThai.TabIndex = 4;
            lblTrangThai.Text = "Trạng thái:";
            // 
            // lblNgayBatDau
            // 
            lblNgayBatDau.AutoSize = true;
            lblNgayBatDau.Location = new Point(20, 140);
            lblNgayBatDau.Name = "lblNgayBatDau";
            lblNgayBatDau.Size = new Size(102, 20);
            lblNgayBatDau.TabIndex = 5;
            lblNgayBatDau.Text = "Ngày bắt đầu:";
            // 
            // lblNgayKetThuc
            // 
            lblNgayKetThuc.AutoSize = true;
            lblNgayKetThuc.Location = new Point(250, 140);
            lblNgayKetThuc.Name = "lblNgayKetThuc";
            lblNgayKetThuc.Size = new Size(105, 20);
            lblNgayKetThuc.TabIndex = 6;
            lblNgayKetThuc.Text = "Ngày kết thúc:";
            // 
            // lblDiem
            // 
            lblDiem.AutoSize = true;
            lblDiem.Location = new Point(450, 140);
            lblDiem.Name = "lblDiem";
            lblDiem.Size = new Size(48, 20);
            lblDiem.TabIndex = 7;
            lblDiem.Text = "Điểm:";
            // 
            // lblTienDoTitle
            // 
            lblTienDoTitle.AutoSize = true;
            lblTienDoTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTienDoTitle.Location = new Point(30, 310);
            lblTienDoTitle.Name = "lblTienDoTitle";
            lblTienDoTitle.Size = new Size(185, 28);
            lblTienDoTitle.TabIndex = 2;
            lblTienDoTitle.Text = "Tiến độ thực hiện:";
            // 
            // dgvTienDo
            // 
            dgvTienDo.AllowUserToAddRows = false;
            dgvTienDo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTienDo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTienDo.Location = new Point(30, 350);
            dgvTienDo.Name = "dgvTienDo";
            dgvTienDo.ReadOnly = true;
            dgvTienDo.RowHeadersWidth = 51;
            dgvTienDo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTienDo.Size = new Size(800, 150);
            dgvTienDo.TabIndex = 3;
            // 
            // lblThongBaoTitle
            // 
            lblThongBaoTitle.AutoSize = true;
            lblThongBaoTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblThongBaoTitle.Location = new Point(30, 520);
            lblThongBaoTitle.Name = "lblThongBaoTitle";
            lblThongBaoTitle.Size = new Size(110, 28);
            lblThongBaoTitle.TabIndex = 4;
            lblThongBaoTitle.Text = "Thông báo:";
            // 
            // dgvThongBao
            // 
            dgvThongBao.AllowUserToAddRows = false;
            dgvThongBao.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvThongBao.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvThongBao.Location = new Point(30, 560);
            dgvThongBao.Name = "dgvThongBao";
            dgvThongBao.ReadOnly = true;
            dgvThongBao.RowHeadersWidth = 51;
            dgvThongBao.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvThongBao.Size = new Size(800, 150);
            dgvThongBao.TabIndex = 5;
            // 
            // DoAnSinhVienControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(236, 240, 241);
            Controls.Add(dgvThongBao);
            Controls.Add(lblThongBaoTitle);
            Controls.Add(dgvTienDo);
            Controls.Add(lblTienDoTitle);
            Controls.Add(pnlThongTin);
            Controls.Add(lblTitle);
            Name = "DoAnSinhVienControl";
            Size = new Size(900, 750);
            pnlThongTin.ResumeLayout(false);
            pnlThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTienDo).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvThongBao).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Panel pnlThongTin;
        private Label lblTenDeTai;
        private Label lblMoTa;
        private Label lblGiangVien;
        private Label lblLoaiDoAn;
        private Label lblTrangThai;
        private Label lblNgayBatDau;
        private Label lblNgayKetThuc;
        private Label lblDiem;
        private Label lblTienDoTitle;
        private DataGridView dgvTienDo;
        private Label lblThongBaoTitle;
        private DataGridView dgvThongBao;
    }
}