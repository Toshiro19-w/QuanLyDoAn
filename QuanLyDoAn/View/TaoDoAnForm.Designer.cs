namespace QuanLyDoAn.View
{
    partial class TaoDoAnForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMaDeTai;
        private System.Windows.Forms.TextBox txtMaDeTai;
        private System.Windows.Forms.Label lblTenDeTai;
        private System.Windows.Forms.TextBox txtTenDeTai;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.Label lblLoaiDoAn;
        private System.Windows.Forms.ComboBox cboLoaiDoAn;
        private System.Windows.Forms.Label lblKyHoc;
        private System.Windows.Forms.ComboBox cboKyHoc;
        private System.Windows.Forms.Label lblChuyenNganh;
        private System.Windows.Forms.ComboBox cboChuyenNganh;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Label lblNgayBatDau;
        private System.Windows.Forms.DateTimePicker dtpNgayBatDau;
        private System.Windows.Forms.Label lblNgayKetThuc;
        private System.Windows.Forms.DateTimePicker dtpNgayKetThuc;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnHuy;

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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMaDeTai = new System.Windows.Forms.Label();
            this.txtMaDeTai = new System.Windows.Forms.TextBox();
            this.lblTenDeTai = new System.Windows.Forms.Label();
            this.txtTenDeTai = new System.Windows.Forms.TextBox();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.lblLoaiDoAn = new System.Windows.Forms.Label();
            this.cboLoaiDoAn = new System.Windows.Forms.ComboBox();
            this.lblKyHoc = new System.Windows.Forms.Label();
            this.cboKyHoc = new System.Windows.Forms.ComboBox();
            this.lblChuyenNganh = new System.Windows.Forms.Label();
            this.cboChuyenNganh = new System.Windows.Forms.ComboBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.lblNgayBatDau = new System.Windows.Forms.Label();
            this.dtpNgayBatDau = new System.Windows.Forms.DateTimePicker();
            this.lblNgayKetThuc = new System.Windows.Forms.Label();
            this.dtpNgayKetThuc = new System.Windows.Forms.DateTimePicker();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            
            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(200, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(200, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "TẠO ĐỀ TÀI MỚI";
            
            // lblMaDeTai
            this.lblMaDeTai.AutoSize = true;
            this.lblMaDeTai.Location = new System.Drawing.Point(30, 70);
            this.lblMaDeTai.Name = "lblMaDeTai";
            this.lblMaDeTai.Size = new System.Drawing.Size(70, 15);
            this.lblMaDeTai.TabIndex = 1;
            this.lblMaDeTai.Text = "Mã đề tài:";
            
            // txtMaDeTai
            this.txtMaDeTai.Location = new System.Drawing.Point(150, 67);
            this.txtMaDeTai.Name = "txtMaDeTai";
            this.txtMaDeTai.Size = new System.Drawing.Size(400, 23);
            this.txtMaDeTai.TabIndex = 2;
            
            // lblTenDeTai
            this.lblTenDeTai.AutoSize = true;
            this.lblTenDeTai.Location = new System.Drawing.Point(30, 105);
            this.lblTenDeTai.Name = "lblTenDeTai";
            this.lblTenDeTai.Size = new System.Drawing.Size(72, 15);
            this.lblTenDeTai.TabIndex = 3;
            this.lblTenDeTai.Text = "Tên đề tài:";
            
            // txtTenDeTai
            this.txtTenDeTai.Location = new System.Drawing.Point(150, 102);
            this.txtTenDeTai.Name = "txtTenDeTai";
            this.txtTenDeTai.Size = new System.Drawing.Size(400, 23);
            this.txtTenDeTai.TabIndex = 4;
            
            // lblMoTa
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Location = new System.Drawing.Point(30, 140);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(45, 15);
            this.lblMoTa.TabIndex = 5;
            this.lblMoTa.Text = "Mô tả:";
            
            // txtMoTa
            this.txtMoTa.Location = new System.Drawing.Point(150, 137);
            this.txtMoTa.Multiline = true;
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.Size = new System.Drawing.Size(400, 60);
            this.txtMoTa.TabIndex = 6;
            
            // lblLoaiDoAn
            this.lblLoaiDoAn.AutoSize = true;
            this.lblLoaiDoAn.Location = new System.Drawing.Point(30, 215);
            this.lblLoaiDoAn.Name = "lblLoaiDoAn";
            this.lblLoaiDoAn.Size = new System.Drawing.Size(72, 15);
            this.lblLoaiDoAn.TabIndex = 7;
            this.lblLoaiDoAn.Text = "Loại đồ án:";
            
            // cboLoaiDoAn
            this.cboLoaiDoAn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiDoAn.Location = new System.Drawing.Point(150, 212);
            this.cboLoaiDoAn.Name = "cboLoaiDoAn";
            this.cboLoaiDoAn.Size = new System.Drawing.Size(400, 23);
            this.cboLoaiDoAn.TabIndex = 8;
            
            // lblKyHoc
            this.lblKyHoc.AutoSize = true;
            this.lblKyHoc.Location = new System.Drawing.Point(30, 250);
            this.lblKyHoc.Name = "lblKyHoc";
            this.lblKyHoc.Size = new System.Drawing.Size(50, 15);
            this.lblKyHoc.TabIndex = 9;
            this.lblKyHoc.Text = "Kỳ học:";
            
            // cboKyHoc
            this.cboKyHoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKyHoc.Location = new System.Drawing.Point(150, 247);
            this.cboKyHoc.Name = "cboKyHoc";
            this.cboKyHoc.Size = new System.Drawing.Size(400, 23);
            this.cboKyHoc.TabIndex = 10;
            
            // lblChuyenNganh
            this.lblChuyenNganh.AutoSize = true;
            this.lblChuyenNganh.Location = new System.Drawing.Point(30, 285);
            this.lblChuyenNganh.Name = "lblChuyenNganh";
            this.lblChuyenNganh.Size = new System.Drawing.Size(95, 15);
            this.lblChuyenNganh.TabIndex = 11;
            this.lblChuyenNganh.Text = "Chuyên ngành:";
            
            // cboChuyenNganh
            this.cboChuyenNganh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChuyenNganh.Location = new System.Drawing.Point(150, 282);
            this.cboChuyenNganh.Name = "cboChuyenNganh";
            this.cboChuyenNganh.Size = new System.Drawing.Size(400, 23);
            this.cboChuyenNganh.TabIndex = 12;
            
            // lblTrangThai
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Location = new System.Drawing.Point(30, 320);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(70, 15);
            this.lblTrangThai.TabIndex = 13;
            this.lblTrangThai.Text = "Trạng thái:";
            
            // cboTrangThai
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.Location = new System.Drawing.Point(150, 317);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(400, 23);
            this.cboTrangThai.TabIndex = 14;
            
            // lblNgayBatDau
            this.lblNgayBatDau.AutoSize = true;
            this.lblNgayBatDau.Location = new System.Drawing.Point(30, 355);
            this.lblNgayBatDau.Name = "lblNgayBatDau";
            this.lblNgayBatDau.Size = new System.Drawing.Size(90, 15);
            this.lblNgayBatDau.TabIndex = 15;
            this.lblNgayBatDau.Text = "Ngày bắt đầu:";
            
            // dtpNgayBatDau
            this.dtpNgayBatDau.Location = new System.Drawing.Point(150, 352);
            this.dtpNgayBatDau.Name = "dtpNgayBatDau";
            this.dtpNgayBatDau.Size = new System.Drawing.Size(400, 23);
            this.dtpNgayBatDau.TabIndex = 16;
            
            // lblNgayKetThuc
            this.lblNgayKetThuc.AutoSize = true;
            this.lblNgayKetThuc.Location = new System.Drawing.Point(30, 390);
            this.lblNgayKetThuc.Name = "lblNgayKetThuc";
            this.lblNgayKetThuc.Size = new System.Drawing.Size(95, 15);
            this.lblNgayKetThuc.TabIndex = 17;
            this.lblNgayKetThuc.Text = "Ngày kết thúc:";
            
            // dtpNgayKetThuc
            this.dtpNgayKetThuc.Location = new System.Drawing.Point(150, 387);
            this.dtpNgayKetThuc.Name = "dtpNgayKetThuc";
            this.dtpNgayKetThuc.Size = new System.Drawing.Size(400, 23);
            this.dtpNgayKetThuc.TabIndex = 18;
            
            // btnLuu
            this.btnLuu.Location = new System.Drawing.Point(350, 440);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(90, 35);
            this.btnLuu.TabIndex = 19;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.BtnLuu_Click);
            
            // btnHuy
            this.btnHuy.Location = new System.Drawing.Point(460, 440);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(90, 35);
            this.btnHuy.TabIndex = 20;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.BtnHuy_Click);
            
            // TaoDoAnForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 500);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.dtpNgayKetThuc);
            this.Controls.Add(this.lblNgayKetThuc);
            this.Controls.Add(this.dtpNgayBatDau);
            this.Controls.Add(this.lblNgayBatDau);
            this.Controls.Add(this.cboTrangThai);
            this.Controls.Add(this.lblTrangThai);
            this.Controls.Add(this.cboChuyenNganh);
            this.Controls.Add(this.lblChuyenNganh);
            this.Controls.Add(this.cboKyHoc);
            this.Controls.Add(this.lblKyHoc);
            this.Controls.Add(this.cboLoaiDoAn);
            this.Controls.Add(this.lblLoaiDoAn);
            this.Controls.Add(this.txtMoTa);
            this.Controls.Add(this.lblMoTa);
            this.Controls.Add(this.txtTenDeTai);
            this.Controls.Add(this.lblTenDeTai);
            this.Controls.Add(this.txtMaDeTai);
            this.Controls.Add(this.lblMaDeTai);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TaoDoAnForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tạo đề tài mới";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
