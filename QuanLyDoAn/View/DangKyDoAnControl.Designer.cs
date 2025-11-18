namespace QuanLyDoAn.View
{
    partial class DangKyDoAnControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvDoAn;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Button btnDangKy;
        private System.Windows.Forms.Button btnLamMoi;

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
            this.dgvDoAn = new System.Windows.Forms.DataGridView();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.btnDangKy = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoAn)).BeginInit();
            this.SuspendLayout();
            
            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(250, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "ĐĂNG KÝ ĐỀ TÀI";
            
            // dgvDoAn
            this.dgvDoAn.AllowUserToAddRows = false;
            this.dgvDoAn.AllowUserToDeleteRows = false;
            this.dgvDoAn.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDoAn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDoAn.Location = new System.Drawing.Point(20, 70);
            this.dgvDoAn.MultiSelect = false;
            this.dgvDoAn.Name = "dgvDoAn";
            this.dgvDoAn.ReadOnly = true;
            this.dgvDoAn.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDoAn.Size = new System.Drawing.Size(960, 400);
            this.dgvDoAn.TabIndex = 1;
            
            // lblGhiChu
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Location = new System.Drawing.Point(20, 490);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(55, 15);
            this.lblGhiChu.TabIndex = 2;
            this.lblGhiChu.Text = "Ghi chú:";
            
            // txtGhiChu
            this.txtGhiChu.Location = new System.Drawing.Point(90, 487);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(600, 60);
            this.txtGhiChu.TabIndex = 3;
            
            // btnDangKy
            this.btnDangKy.Location = new System.Drawing.Point(710, 487);
            this.btnDangKy.Name = "btnDangKy";
            this.btnDangKy.Size = new System.Drawing.Size(120, 35);
            this.btnDangKy.TabIndex = 4;
            this.btnDangKy.Text = "Đăng ký";
            this.btnDangKy.UseVisualStyleBackColor = true;
            this.btnDangKy.Click += new System.EventHandler(this.BtnDangKy_Click);
            
            // btnLamMoi
            this.btnLamMoi.Location = new System.Drawing.Point(850, 487);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(120, 35);
            this.btnLamMoi.TabIndex = 5;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.BtnLamMoi_Click);
            
            // DangKyDoAnControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.btnDangKy);
            this.Controls.Add(this.txtGhiChu);
            this.Controls.Add(this.lblGhiChu);
            this.Controls.Add(this.dgvDoAn);
            this.Controls.Add(this.lblTitle);
            this.Name = "DangKyDoAnControl";
            this.Size = new System.Drawing.Size(1000, 600);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoAn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
