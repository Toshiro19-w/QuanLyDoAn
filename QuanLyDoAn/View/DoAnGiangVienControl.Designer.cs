namespace QuanLyDoAn.View
{
    partial class DoAnGiangVienControl
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
            lblDoAnTitle = new Label();
            btnSuaDeTai = new Button();
            btnXoaDeTai = new Button();
            dgvDoAn = new DataGridView();
            lblTienDoTitle = new Label();
            dgvTienDo = new DataGridView();
            lblNhanXet = new Label();
            txtNhanXet = new TextBox();
            btnCapNhatNhanXet = new Button();
            btnChamDiem = new Button();
            btnChamDiemTienDo = new Button();
            lblDiemTienDo = new Label();
            txtDiemTienDo = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvDoAn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvTienDo).BeginInit();
            SuspendLayout();
            // 
            // lblDoAnTitle
            // 
            lblDoAnTitle.AutoSize = true;
            lblDoAnTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDoAnTitle.Location = new Point(20, 20);
            lblDoAnTitle.Name = "lblDoAnTitle";
            lblDoAnTitle.Size = new Size(335, 28);
            lblDoAnTitle.TabIndex = 0;
            lblDoAnTitle.Text = "Danh sách đồ án được phân công:";
            // 
            // btnSuaDeTai
            // 
            btnSuaDeTai.BackColor = Color.FromArgb(241, 196, 15);
            btnSuaDeTai.FlatStyle = FlatStyle.Flat;
            btnSuaDeTai.ForeColor = Color.White;
            btnSuaDeTai.Location = new Point(1460, 15);
            btnSuaDeTai.Name = "btnSuaDeTai";
            btnSuaDeTai.Size = new Size(60, 35);
            btnSuaDeTai.TabIndex = 13;
            btnSuaDeTai.Text = "Sửa";
            btnSuaDeTai.UseVisualStyleBackColor = false;
            btnSuaDeTai.Click += BtnSuaDeTai_Click;
            // 
            // btnXoaDeTai
            // 
            btnXoaDeTai.BackColor = Color.FromArgb(231, 76, 60);
            btnXoaDeTai.FlatStyle = FlatStyle.Flat;
            btnXoaDeTai.ForeColor = Color.White;
            btnXoaDeTai.Location = new Point(1526, 15);
            btnXoaDeTai.Name = "btnXoaDeTai";
            btnXoaDeTai.Size = new Size(69, 35);
            btnXoaDeTai.TabIndex = 14;
            btnXoaDeTai.Text = "Xóa";
            btnXoaDeTai.UseVisualStyleBackColor = false;
            btnXoaDeTai.Click += BtnXoaDeTai_Click;
            // 
            // dgvDoAn
            // 
            dgvDoAn.AllowUserToAddRows = false;
            dgvDoAn.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDoAn.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDoAn.Location = new Point(20, 50);
            dgvDoAn.Name = "dgvDoAn";
            dgvDoAn.ReadOnly = true;
            dgvDoAn.RowHeadersWidth = 51;
            dgvDoAn.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDoAn.Size = new Size(1641, 180);
            dgvDoAn.TabIndex = 1;
            dgvDoAn.SelectionChanged += DgvDoAn_SelectionChanged;
            // 
            // lblTienDoTitle
            // 
            lblTienDoTitle.AutoSize = true;
            lblTienDoTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTienDoTitle.Location = new Point(20, 250);
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
            dgvTienDo.Location = new Point(20, 280);
            dgvTienDo.Name = "dgvTienDo";
            dgvTienDo.ReadOnly = true;
            dgvTienDo.RowHeadersWidth = 51;
            dgvTienDo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTienDo.Size = new Size(1641, 150);
            dgvTienDo.TabIndex = 3;
            dgvTienDo.CellClick += DgvTienDo_CellClick;
            // 
            // lblNhanXet
            // 
            lblNhanXet.AutoSize = true;
            lblNhanXet.Location = new Point(20, 450);
            lblNhanXet.Name = "lblNhanXet";
            lblNhanXet.Size = new Size(122, 20);
            lblNhanXet.TabIndex = 4;
            lblNhanXet.Text = "Nhận xét tiến độ:";
            // 
            // txtNhanXet
            // 
            txtNhanXet.Location = new Point(150, 450);
            txtNhanXet.Multiline = true;
            txtNhanXet.Name = "txtNhanXet";
            txtNhanXet.ScrollBars = ScrollBars.Vertical;
            txtNhanXet.Size = new Size(784, 60);
            txtNhanXet.TabIndex = 5;
            // 
            // btnCapNhatNhanXet
            // 
            btnCapNhatNhanXet.BackColor = Color.FromArgb(52, 152, 219);
            btnCapNhatNhanXet.FlatStyle = FlatStyle.Flat;
            btnCapNhatNhanXet.ForeColor = Color.White;
            btnCapNhatNhanXet.Location = new Point(1531, 446);
            btnCapNhatNhanXet.Name = "btnCapNhatNhanXet";
            btnCapNhatNhanXet.Size = new Size(130, 35);
            btnCapNhatNhanXet.TabIndex = 6;
            btnCapNhatNhanXet.Text = "Cập nhật nhận xét";
            btnCapNhatNhanXet.UseVisualStyleBackColor = false;
            btnCapNhatNhanXet.Click += BtnCapNhatNhanXet_Click;
            // 
            // btnChamDiem
            // 
            btnChamDiem.BackColor = Color.FromArgb(46, 204, 113);
            btnChamDiem.FlatStyle = FlatStyle.Flat;
            btnChamDiem.ForeColor = Color.White;
            btnChamDiem.Location = new Point(1531, 446);
            btnChamDiem.Name = "btnChamDiem";
            btnChamDiem.Size = new Size(130, 35);
            btnChamDiem.TabIndex = 7;
            btnChamDiem.Text = "Chấm theo tiêu chí";
            btnChamDiem.UseVisualStyleBackColor = false;
            btnChamDiem.Click += BtnChamDiem_Click;
            // 
            // lblDiemTienDo
            // 
            lblDiemTienDo.AutoSize = true;
            lblDiemTienDo.Location = new Point(940, 453);
            lblDiemTienDo.Name = "lblDiemTienDo";
            lblDiemTienDo.Size = new Size(95, 20);
            lblDiemTienDo.TabIndex = 8;
            lblDiemTienDo.Text = "Điểm tiến độ:";
            // 
            // txtDiemTienDo
            // 
            txtDiemTienDo.Location = new Point(1045, 450);
            txtDiemTienDo.Name = "txtDiemTienDo";
            txtDiemTienDo.Size = new Size(80, 27);
            txtDiemTienDo.TabIndex = 9;
            // 
            // btnChamDiemTienDo
            // 
            btnChamDiemTienDo.BackColor = Color.FromArgb(52, 152, 219);
            btnChamDiemTienDo.FlatStyle = FlatStyle.Flat;
            btnChamDiemTienDo.ForeColor = Color.White;
            btnChamDiemTienDo.Location = new Point(1135, 446);
            btnChamDiemTienDo.Name = "btnChamDiemTienDo";
            btnChamDiemTienDo.Size = new Size(130, 35);
            btnChamDiemTienDo.TabIndex = 10;
            btnChamDiemTienDo.Text = "Lưu điểm tiến độ";
            btnChamDiemTienDo.UseVisualStyleBackColor = false;
            btnChamDiemTienDo.Click += BtnChamDiemTienDo_Click;
            // 
            // DoAnGiangVienControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(236, 240, 241);
            Controls.Add(btnXoaDeTai);
            Controls.Add(btnSuaDeTai);
            Controls.Add(btnChamDiem);
            Controls.Add(btnChamDiemTienDo);
            Controls.Add(txtDiemTienDo);
            Controls.Add(lblDiemTienDo);
            Controls.Add(btnCapNhatNhanXet);
            Controls.Add(txtNhanXet);
            Controls.Add(lblNhanXet);
            Controls.Add(dgvTienDo);
            Controls.Add(lblTienDoTitle);
            Controls.Add(dgvDoAn);
            Controls.Add(lblDoAnTitle);
            Name = "DoAnGiangVienControl";
            Size = new Size(1920, 1080);
            ((System.ComponentModel.ISupportInitialize)dgvDoAn).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvTienDo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblDoAnTitle;
        private DataGridView dgvDoAn;
        private Label lblTienDoTitle;
        private DataGridView dgvTienDo;
        private Label lblNhanXet;
        private TextBox txtNhanXet;
        private Button btnCapNhatNhanXet;
        private Button btnChamDiem;
        private Button btnChamDiemTienDo;
        private Label lblDiemTienDo;
        private TextBox txtDiemTienDo;
        private Button btnSuaDeTai;
        private Button btnXoaDeTai;
    }
}