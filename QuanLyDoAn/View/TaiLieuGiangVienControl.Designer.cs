namespace QuanLyDoAn.View
{
    partial class TaiLieuGiangVienControl
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
            lblDoAn = new Label();
            cmbDoAn = new ComboBox();
            dgvTaiLieu = new DataGridView();
            btnXem = new Button();
            btnTaiVe = new Button();
            lblTaiLieu = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvTaiLieu).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(30, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(318, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Quản lý tài liệu sinh viên";
            // 
            // lblDoAn
            // 
            lblDoAn.AutoSize = true;
            lblDoAn.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDoAn.Location = new Point(30, 100);
            lblDoAn.Name = "lblDoAn";
            lblDoAn.Size = new Size(95, 23);
            lblDoAn.TabIndex = 1;
            lblDoAn.Text = "Chọn đồ án:";
            // 
            // cmbDoAn
            // 
            cmbDoAn.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDoAn.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbDoAn.FormattingEnabled = true;
            cmbDoAn.Location = new Point(150, 97);
            cmbDoAn.Name = "cmbDoAn";
            cmbDoAn.Size = new Size(400, 31);
            cmbDoAn.TabIndex = 2;
            cmbDoAn.SelectedIndexChanged += cmbDoAn_SelectedIndexChanged;
            // 
            // dgvTaiLieu
            // 
            dgvTaiLieu.AllowUserToAddRows = false;
            dgvTaiLieu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTaiLieu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTaiLieu.Location = new Point(30, 180);
            dgvTaiLieu.Name = "dgvTaiLieu";
            dgvTaiLieu.ReadOnly = true;
            dgvTaiLieu.RowHeadersWidth = 51;
            dgvTaiLieu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTaiLieu.Size = new Size(700, 300);
            dgvTaiLieu.TabIndex = 3;
            // 
            // btnXem
            // 
            btnXem.BackColor = Color.FromArgb(52, 152, 219);
            btnXem.FlatStyle = FlatStyle.Flat;
            btnXem.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnXem.ForeColor = Color.White;
            btnXem.Location = new Point(500, 500);
            btnXem.Name = "btnXem";
            btnXem.Size = new Size(100, 40);
            btnXem.TabIndex = 4;
            btnXem.Text = "Xem";
            btnXem.UseVisualStyleBackColor = false;
            btnXem.Click += btnXem_Click;
            // 
            // btnTaiVe
            // 
            btnTaiVe.BackColor = Color.FromArgb(46, 204, 113);
            btnTaiVe.FlatStyle = FlatStyle.Flat;
            btnTaiVe.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnTaiVe.ForeColor = Color.White;
            btnTaiVe.Location = new Point(630, 500);
            btnTaiVe.Name = "btnTaiVe";
            btnTaiVe.Size = new Size(100, 40);
            btnTaiVe.TabIndex = 5;
            btnTaiVe.Text = "Tải về";
            btnTaiVe.UseVisualStyleBackColor = false;
            btnTaiVe.Click += btnTaiVe_Click;
            // 
            // lblTaiLieu
            // 
            lblTaiLieu.AutoSize = true;
            lblTaiLieu.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTaiLieu.Location = new Point(30, 150);
            lblTaiLieu.Name = "lblTaiLieu";
            lblTaiLieu.Size = new Size(200, 28);
            lblTaiLieu.TabIndex = 6;
            lblTaiLieu.Text = "Danh sách tài liệu:";
            // 
            // TaiLieuGiangVienControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(236, 240, 241);
            Controls.Add(lblTaiLieu);
            Controls.Add(btnTaiVe);
            Controls.Add(btnXem);
            Controls.Add(dgvTaiLieu);
            Controls.Add(cmbDoAn);
            Controls.Add(lblDoAn);
            Controls.Add(lblTitle);
            Name = "TaiLieuGiangVienControl";
            Size = new Size(800, 600);
            ((System.ComponentModel.ISupportInitialize)dgvTaiLieu).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblDoAn;
        private ComboBox cmbDoAn;
        private DataGridView dgvTaiLieu;
        private Button btnXem;
        private Button btnTaiVe;
        private Label lblTaiLieu;
    }
}