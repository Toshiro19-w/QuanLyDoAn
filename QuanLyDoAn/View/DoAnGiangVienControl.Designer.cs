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
            dgvDoAn = new DataGridView();
            lblTienDoTitle = new Label();
            dgvTienDo = new DataGridView();
            lblNhanXet = new Label();
            txtNhanXet = new TextBox();
            btnCapNhatNhanXet = new Button();
            lblDiem = new Label();
            txtDiem = new TextBox();
            lblNhanXetCuoi = new Label();
            txtNhanXetCuoi = new TextBox();
            btnChamDiem = new Button();
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
            // lblDiem
            // 
            lblDiem.AutoSize = true;
            lblDiem.Location = new Point(94, 530);
            lblDiem.Name = "lblDiem";
            lblDiem.Size = new Size(48, 20);
            lblDiem.TabIndex = 7;
            lblDiem.Text = "Điểm:";
            // 
            // txtDiem
            // 
            txtDiem.Location = new Point(150, 530);
            txtDiem.Name = "txtDiem";
            txtDiem.Size = new Size(80, 27);
            txtDiem.TabIndex = 8;
            // 
            // lblNhanXetCuoi
            // 
            lblNhanXetCuoi.AutoSize = true;
            lblNhanXetCuoi.Location = new Point(565, 537);
            lblNhanXetCuoi.Name = "lblNhanXetCuoi";
            lblNhanXetCuoi.Size = new Size(103, 20);
            lblNhanXetCuoi.TabIndex = 9;
            lblNhanXetCuoi.Text = "Nhận xét cuối:";
            // 
            // txtNhanXetCuoi
            // 
            txtNhanXetCuoi.Location = new Point(674, 530);
            txtNhanXetCuoi.Multiline = true;
            txtNhanXetCuoi.Name = "txtNhanXetCuoi";
            txtNhanXetCuoi.ScrollBars = ScrollBars.Vertical;
            txtNhanXetCuoi.Size = new Size(260, 60);
            txtNhanXetCuoi.TabIndex = 10;
            // 
            // btnChamDiem
            // 
            btnChamDiem.BackColor = Color.FromArgb(46, 204, 113);
            btnChamDiem.FlatStyle = FlatStyle.Flat;
            btnChamDiem.ForeColor = Color.White;
            btnChamDiem.Location = new Point(1531, 503);
            btnChamDiem.Name = "btnChamDiem";
            btnChamDiem.Size = new Size(130, 35);
            btnChamDiem.TabIndex = 11;
            btnChamDiem.Text = "Chấm điểm";
            btnChamDiem.UseVisualStyleBackColor = false;
            btnChamDiem.Click += BtnChamDiem_Click;
            // 
            // DoAnGiangVienControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(236, 240, 241);
            Controls.Add(btnChamDiem);
            Controls.Add(txtNhanXetCuoi);
            Controls.Add(lblNhanXetCuoi);
            Controls.Add(txtDiem);
            Controls.Add(lblDiem);
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
        private Label lblDiem;
        private TextBox txtDiem;
        private Label lblNhanXetCuoi;
        private TextBox txtNhanXetCuoi;
        private Button btnChamDiem;
    }
}