namespace QuanLyDoAn.View
{
    partial class ChamDiemChiTietForm
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
            lblThongTinDoAn = new Label();
            lblLoaiDanhGia = new Label();
            cboLoaiDanhGia = new ComboBox();
            dgvTieuChi = new DataGridView();
            lblDiemTB = new Label();
            btnLuu = new Button();
            btnHuy = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvTieuChi).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(234, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Chấm điểm chi tiết";
            // 
            // lblThongTinDoAn
            // 
            lblThongTinDoAn.AutoSize = true;
            lblThongTinDoAn.Font = new Font("Segoe UI", 10F);
            lblThongTinDoAn.Location = new Point(20, 60);
            lblThongTinDoAn.Name = "lblThongTinDoAn";
            lblThongTinDoAn.Size = new Size(150, 23);
            lblThongTinDoAn.TabIndex = 1;
            lblThongTinDoAn.Text = "Thông tin đồ án...";
            // 
            // lblLoaiDanhGia
            // 
            lblLoaiDanhGia.AutoSize = true;
            lblLoaiDanhGia.Location = new Point(20, 100);
            lblLoaiDanhGia.Name = "lblLoaiDanhGia";
            lblLoaiDanhGia.Size = new Size(107, 20);
            lblLoaiDanhGia.TabIndex = 2;
            lblLoaiDanhGia.Text = "Loại đánh giá:";
            // 
            // cboLoaiDanhGia
            // 
            cboLoaiDanhGia.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoaiDanhGia.FormattingEnabled = true;
            cboLoaiDanhGia.Location = new Point(140, 97);
            cboLoaiDanhGia.Name = "cboLoaiDanhGia";
            cboLoaiDanhGia.Size = new Size(250, 28);
            cboLoaiDanhGia.TabIndex = 3;
            cboLoaiDanhGia.SelectedIndexChanged += CboLoaiDanhGia_SelectedIndexChanged;
            // 
            // dgvTieuChi
            // 
            dgvTieuChi.AllowUserToAddRows = false;
            dgvTieuChi.AllowUserToDeleteRows = false;
            dgvTieuChi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvTieuChi.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvTieuChi.Location = new Point(20, 140);
            dgvTieuChi.Name = "dgvTieuChi";
            dgvTieuChi.RowHeadersWidth = 51;
            dgvTieuChi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTieuChi.Size = new Size(1160, 400);
            dgvTieuChi.TabIndex = 4;
            dgvTieuChi.CellValueChanged += DgvTieuChi_CellValueChanged;
            // 
            // lblDiemTB
            // 
            lblDiemTB.AutoSize = true;
            lblDiemTB.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblDiemTB.Location = new Point(20, 560);
            lblDiemTB.Name = "lblDiemTB";
            lblDiemTB.Size = new Size(213, 28);
            lblDiemTB.TabIndex = 5;
            lblDiemTB.Text = "Điểm trung bình: 0.0";
            // 
            // btnLuu
            // 
            btnLuu.BackColor = Color.FromArgb(46, 204, 113);
            btnLuu.FlatStyle = FlatStyle.Flat;
            btnLuu.ForeColor = Color.White;
            btnLuu.Location = new Point(970, 555);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(100, 40);
            btnLuu.TabIndex = 6;
            btnLuu.Text = "Lưu";
            btnLuu.UseVisualStyleBackColor = false;
            btnLuu.Click += BtnLuu_Click;
            // 
            // btnHuy
            // 
            btnHuy.BackColor = Color.FromArgb(231, 76, 60);
            btnHuy.FlatStyle = FlatStyle.Flat;
            btnHuy.ForeColor = Color.White;
            btnHuy.Location = new Point(1080, 555);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(100, 40);
            btnHuy.TabIndex = 7;
            btnHuy.Text = "Hủy";
            btnHuy.UseVisualStyleBackColor = false;
            btnHuy.Click += BtnHuy_Click;
            // 
            // ChamDiemChiTietForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(236, 240, 241);
            ClientSize = new Size(1200, 620);
            Controls.Add(btnHuy);
            Controls.Add(btnLuu);
            Controls.Add(lblDiemTB);
            Controls.Add(dgvTieuChi);
            Controls.Add(cboLoaiDanhGia);
            Controls.Add(lblLoaiDanhGia);
            Controls.Add(lblThongTinDoAn);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ChamDiemChiTietForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Chấm điểm chi tiết";
            ((System.ComponentModel.ISupportInitialize)dgvTieuChi).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle;
        private Label lblThongTinDoAn;
        private Label lblLoaiDanhGia;
        private ComboBox cboLoaiDanhGia;
        private DataGridView dgvTieuChi;
        private Label lblDiemTB;
        private Button btnLuu;
        private Button btnHuy;
    }
}
