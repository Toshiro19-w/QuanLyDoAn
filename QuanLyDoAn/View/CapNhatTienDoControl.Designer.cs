namespace QuanLyDoAn.View
{
    partial class CapNhatTienDoControl
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
            lblTenDeTai = new Label();
            lblGiaiDoan = new Label();
            txtGiaiDoan = new TextBox();
            btnThemTienDo = new Button();
            lblDanhSach = new Label();
            dgvTienDo = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvTienDo).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(30, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(229, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Cập nhật tiến độ";
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
            // lblGiaiDoan
            // 
            lblGiaiDoan.AutoSize = true;
            lblGiaiDoan.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblGiaiDoan.Location = new Point(30, 130);
            lblGiaiDoan.Name = "lblGiaiDoan";
            lblGiaiDoan.Size = new Size(87, 23);
            lblGiaiDoan.TabIndex = 2;
            lblGiaiDoan.Text = "Giai đoạn:";
            // 
            // txtGiaiDoan
            // 
            txtGiaiDoan.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtGiaiDoan.Location = new Point(150, 127);
            txtGiaiDoan.Multiline = true;
            txtGiaiDoan.Name = "txtGiaiDoan";
            txtGiaiDoan.ScrollBars = ScrollBars.Vertical;
            txtGiaiDoan.Size = new Size(523, 100);
            txtGiaiDoan.TabIndex = 3;
            // 
            // btnThemTienDo
            // 
            btnThemTienDo.BackColor = Color.FromArgb(46, 204, 113);
            btnThemTienDo.FlatStyle = FlatStyle.Flat;
            btnThemTienDo.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnThemTienDo.ForeColor = Color.White;
            btnThemTienDo.Location = new Point(520, 251);
            btnThemTienDo.Name = "btnThemTienDo";
            btnThemTienDo.Size = new Size(153, 45);
            btnThemTienDo.TabIndex = 4;
            btnThemTienDo.Text = "Nộp tiến độ";
            btnThemTienDo.UseVisualStyleBackColor = false;
            btnThemTienDo.Click += btnThemTienDo_Click;
            // 
            // lblDanhSach
            // 
            lblDanhSach.AutoSize = true;
            lblDanhSach.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDanhSach.Location = new Point(30, 320);
            lblDanhSach.Name = "lblDanhSach";
            lblDanhSach.Size = new Size(199, 28);
            lblDanhSach.TabIndex = 5;
            lblDanhSach.Text = "Lịch sử nộp tiến độ:";
            // 
            // dgvTienDo
            // 
            dgvTienDo.AllowUserToAddRows = false;
            dgvTienDo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTienDo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTienDo.Location = new Point(30, 360);
            dgvTienDo.Name = "dgvTienDo";
            dgvTienDo.ReadOnly = true;
            dgvTienDo.RowHeadersWidth = 51;
            dgvTienDo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTienDo.Size = new Size(800, 300);
            dgvTienDo.TabIndex = 6;
            // 
            // CapNhatTienDoControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(236, 240, 241);
            Controls.Add(dgvTienDo);
            Controls.Add(lblDanhSach);
            Controls.Add(btnThemTienDo);
            Controls.Add(txtGiaiDoan);
            Controls.Add(lblGiaiDoan);
            Controls.Add(lblTenDeTai);
            Controls.Add(lblTitle);
            Name = "CapNhatTienDoControl";
            Size = new Size(900, 700);
            ((System.ComponentModel.ISupportInitialize)dgvTienDo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblTenDeTai;
        private Label lblGiaiDoan;
        private TextBox txtGiaiDoan;
        private Button btnThemTienDo;
        private Label lblDanhSach;
        private DataGridView dgvTienDo;
    }
}