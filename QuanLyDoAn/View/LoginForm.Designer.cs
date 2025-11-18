namespace QuanLyDoAn
{
    partial class LoginForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            linkLabel2 = new LinkLabel();
            linkLabel1 = new LinkLabel();
            label2 = new Label();
            label1 = new Label();
            lblNew = new Label();
            button1 = new Button();
            chkSignedIn = new CheckBox();
            txtMatKhau = new TextBox();
            txtTenDangNhap = new TextBox();
            lblAccess = new Label();
            lblSignIn = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(linkLabel2);
            groupBox1.Controls.Add(linkLabel1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(lblNew);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(chkSignedIn);
            groupBox1.Controls.Add(txtMatKhau);
            groupBox1.Controls.Add(txtTenDangNhap);
            groupBox1.Controls.Add(lblAccess);
            groupBox1.Controls.Add(lblSignIn);
            groupBox1.Location = new Point(313, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(455, 721);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.Location = new Point(250, 603);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.RightToLeft = RightToLeft.No;
            linkLabel2.Size = new Size(60, 20);
            linkLabel2.TabIndex = 12;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "Đăng kí";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(289, 435);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(116, 20);
            linkLabel1.TabIndex = 11;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Quên mật khẩu?";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(45, 320);
            label2.Name = "label2";
            label2.Size = new Size(70, 20);
            label2.TabIndex = 10;
            label2.Text = "Mật khẩu";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(45, 220);
            label1.Name = "label1";
            label1.Size = new Size(107, 20);
            label1.TabIndex = 9;
            label1.Text = "Tên đăng nhập";
            // 
            // lblNew
            // 
            lblNew.AutoSize = true;
            lblNew.Location = new Point(109, 603);
            lblNew.Name = "lblNew";
            lblNew.Size = new Size(135, 20);
            lblNew.TabIndex = 7;
            lblNew.Text = "Chưa có tài khoản?";
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(45, 482);
            button1.Name = "button1";
            button1.Size = new Size(360, 79);
            button1.TabIndex = 6;
            button1.Text = "Đăng nhập";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnDangNhap_Click;
            // 
            // chkSignedIn
            // 
            chkSignedIn.AutoSize = true;
            chkSignedIn.Location = new Point(45, 434);
            chkSignedIn.Name = "chkSignedIn";
            chkSignedIn.Size = new Size(130, 24);
            chkSignedIn.TabIndex = 4;
            chkSignedIn.Text = "Lưu đăng nhập";
            chkSignedIn.UseVisualStyleBackColor = true;
            // 
            // txtMatKhau
            // 
            txtMatKhau.Location = new Point(43, 343);
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.Size = new Size(362, 27);
            txtMatKhau.TabIndex = 3;
            // 
            // txtTenDangNhap
            // 
            txtTenDangNhap.Location = new Point(43, 243);
            txtTenDangNhap.Name = "txtTenDangNhap";
            txtTenDangNhap.Size = new Size(362, 27);
            txtTenDangNhap.TabIndex = 2;
            // 
            // lblAccess
            // 
            lblAccess.AutoSize = true;
            lblAccess.Location = new Point(158, 140);
            lblAccess.Name = "lblAccess";
            lblAccess.Size = new Size(142, 20);
            lblAccess.TabIndex = 1;
            lblAccess.Text = "Access your account";
            // 
            // lblSignIn
            // 
            lblSignIn.AutoSize = true;
            lblSignIn.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSignIn.Location = new Point(158, 62);
            lblSignIn.Name = "lblSignIn";
            lblSignIn.Size = new Size(153, 38);
            lblSignIn.TabIndex = 0;
            lblSignIn.Text = "Đăng nhập";
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1074, 766);
            Controls.Add(groupBox1);
            Name = "LoginForm";
            Text = "LoginForm";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label lblAccess;
        private Label lblSignIn;
        private TextBox txtTenDangNhap;
        private CheckBox chkSignedIn;
        private TextBox txtMatKhau;
        private Label lblNew;
        private Button button1;
        private Label label2;
        private Label label1;
        private LinkLabel linkLabel1;
        private LinkLabel linkLabel2;
    }
}