namespace QuanLyDoAn.View
{
    partial class ThongBaoGiangVienControl
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
            lblTieuDe = new Label();
            txtTieuDe = new TextBox();
            lblNoiDung = new Label();
            txtNoiDung = new TextBox();
            btnGuiThongBao = new Button();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(30, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(259, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Gửi thông báo mới";
            // 
            // lblDoAn
            // 
            lblDoAn.AutoSize = true;
            lblDoAn.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDoAn.Location = new Point(30, 100);
            lblDoAn.Name = "lblDoAn";
            lblDoAn.Size = new Size(104, 23);
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
            cmbDoAn.Size = new Size(431, 31);
            cmbDoAn.TabIndex = 2;
            // 
            // lblTieuDe
            // 
            lblTieuDe.AutoSize = true;
            lblTieuDe.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTieuDe.Location = new Point(30, 160);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(70, 23);
            lblTieuDe.TabIndex = 3;
            lblTieuDe.Text = "Tiêu đề:";
            // 
            // txtTieuDe
            // 
            txtTieuDe.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTieuDe.Location = new Point(150, 157);
            txtTieuDe.Name = "txtTieuDe";
            txtTieuDe.Size = new Size(431, 30);
            txtTieuDe.TabIndex = 4;
            // 
            // lblNoiDung
            // 
            lblNoiDung.AutoSize = true;
            lblNoiDung.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNoiDung.Location = new Point(30, 220);
            lblNoiDung.Name = "lblNoiDung";
            lblNoiDung.Size = new Size(86, 23);
            lblNoiDung.TabIndex = 5;
            lblNoiDung.Text = "Nội dung:";
            // 
            // txtNoiDung
            // 
            txtNoiDung.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNoiDung.Location = new Point(150, 217);
            txtNoiDung.Multiline = true;
            txtNoiDung.Name = "txtNoiDung";
            txtNoiDung.ScrollBars = ScrollBars.Vertical;
            txtNoiDung.Size = new Size(431, 200);
            txtNoiDung.TabIndex = 6;
            // 
            // btnGuiThongBao
            // 
            btnGuiThongBao.BackColor = Color.FromArgb(46, 204, 113);
            btnGuiThongBao.FlatStyle = FlatStyle.Flat;
            btnGuiThongBao.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuiThongBao.ForeColor = Color.White;
            btnGuiThongBao.Location = new Point(420, 450);
            btnGuiThongBao.Name = "btnGuiThongBao";
            btnGuiThongBao.Size = new Size(161, 47);
            btnGuiThongBao.TabIndex = 7;
            btnGuiThongBao.Text = "Gửi thông báo";
            btnGuiThongBao.UseVisualStyleBackColor = false;
            btnGuiThongBao.Click += btnGuiThongBao_Click;
            // 
            // ThongBaoGiangVienControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(236, 240, 241);
            Controls.Add(btnGuiThongBao);
            Controls.Add(txtNoiDung);
            Controls.Add(lblNoiDung);
            Controls.Add(txtTieuDe);
            Controls.Add(lblTieuDe);
            Controls.Add(cmbDoAn);
            Controls.Add(lblDoAn);
            Controls.Add(lblTitle);
            Name = "ThongBaoGiangVienControl";
            Size = new Size(1016, 801);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblDoAn;
        private ComboBox cmbDoAn;
        private Label lblTieuDe;
        private TextBox txtTieuDe;
        private Label lblNoiDung;
        private TextBox txtNoiDung;
        private Button btnGuiThongBao;
    }
}