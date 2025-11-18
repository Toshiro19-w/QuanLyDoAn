namespace QuanLyDoAn.View
{
    partial class DuyetYeuCauControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvYeuCau;
        private System.Windows.Forms.Button btnChapNhan;
        private System.Windows.Forms.Button btnTuChoi;
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
            this.dgvYeuCau = new System.Windows.Forms.DataGridView();
            this.btnChapNhan = new System.Windows.Forms.Button();
            this.btnTuChoi = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvYeuCau)).BeginInit();
            this.SuspendLayout();
            
            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(300, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "DUYỆT YÊU CẦU ĐĂNG KÝ";
            
            // dgvYeuCau
            this.dgvYeuCau.AllowUserToAddRows = false;
            this.dgvYeuCau.AllowUserToDeleteRows = false;
            this.dgvYeuCau.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvYeuCau.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvYeuCau.Location = new System.Drawing.Point(20, 70);
            this.dgvYeuCau.MultiSelect = false;
            this.dgvYeuCau.Name = "dgvYeuCau";
            this.dgvYeuCau.ReadOnly = true;
            this.dgvYeuCau.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvYeuCau.Size = new System.Drawing.Size(960, 450);
            this.dgvYeuCau.TabIndex = 1;
            
            // btnChapNhan
            this.btnChapNhan.Location = new System.Drawing.Point(650, 540);
            this.btnChapNhan.Name = "btnChapNhan";
            this.btnChapNhan.Size = new System.Drawing.Size(120, 35);
            this.btnChapNhan.TabIndex = 2;
            this.btnChapNhan.Text = "Chấp nhận";
            this.btnChapNhan.UseVisualStyleBackColor = true;
            this.btnChapNhan.Click += new System.EventHandler(this.BtnChapNhan_Click);
            
            // btnTuChoi
            this.btnTuChoi.Location = new System.Drawing.Point(790, 540);
            this.btnTuChoi.Name = "btnTuChoi";
            this.btnTuChoi.Size = new System.Drawing.Size(120, 35);
            this.btnTuChoi.TabIndex = 3;
            this.btnTuChoi.Text = "Từ chối";
            this.btnTuChoi.UseVisualStyleBackColor = true;
            this.btnTuChoi.Click += new System.EventHandler(this.BtnTuChoi_Click);
            
            // btnLamMoi
            this.btnLamMoi.Location = new System.Drawing.Point(20, 540);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(120, 35);
            this.btnLamMoi.TabIndex = 4;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.BtnLamMoi_Click);
            
            // DuyetYeuCauControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.btnTuChoi);
            this.Controls.Add(this.btnChapNhan);
            this.Controls.Add(this.dgvYeuCau);
            this.Controls.Add(this.lblTitle);
            this.Name = "DuyetYeuCauControl";
            this.Size = new System.Drawing.Size(1000, 600);
            ((System.ComponentModel.ISupportInitialize)(this.dgvYeuCau)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
