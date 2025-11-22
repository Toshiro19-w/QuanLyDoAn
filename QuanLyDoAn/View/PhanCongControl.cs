using System;
using System.Linq;
using System.Windows.Forms;
using QuanLyDoAn.Model.EF;
using QuanLyDoAn.Utils;

namespace QuanLyDoAn.View
{
    public partial class PhanCongControl : UserControl
    {
        private DataGridView dgvDoAn;
        private Button btnPhanCong;

        public PhanCongControl()
        {
            InitializeComponent();
            ThemeHelper.ApplyTheme(this);
            LoadData();
        }

        private void InitializeComponent()
        {
            this.dgvDoAn = new DataGridView();
            this.btnPhanCong = new Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoAn)).BeginInit();
            this.SuspendLayout();

            // dgvDoAn
            this.dgvDoAn.AllowUserToAddRows = false;
            this.dgvDoAn.AllowUserToDeleteRows = false;
            this.dgvDoAn.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDoAn.Location = new System.Drawing.Point(20, 70);
            this.dgvDoAn.Name = "dgvDoAn";
            this.dgvDoAn.ReadOnly = true;
            this.dgvDoAn.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvDoAn.Size = new System.Drawing.Size(960, 450);
            this.dgvDoAn.TabIndex = 0;

            // btnPhanCong
            this.btnPhanCong.BackColor = Constants.Colors.Primary;
            this.btnPhanCong.ForeColor = System.Drawing.Color.White;
            this.btnPhanCong.Location = new System.Drawing.Point(20, 20);
            this.btnPhanCong.Name = "btnPhanCong";
            this.btnPhanCong.Size = new System.Drawing.Size(150, 35);
            this.btnPhanCong.TabIndex = 1;
            this.btnPhanCong.Text = "Phân công";
            this.btnPhanCong.UseVisualStyleBackColor = false;
            this.btnPhanCong.Click += new EventHandler(this.BtnPhanCong_Click);

            // PhanCongControl
            this.Controls.Add(this.btnPhanCong);
            this.Controls.Add(this.dgvDoAn);
            this.Name = "PhanCongControl";
            this.Size = new System.Drawing.Size(1000, 600);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoAn)).EndInit();
            this.ResumeLayout(false);
        }

        private void LoadData()
        {
            using var context = new QuanLyDoAnContext();
            var doAns = context.DoAns
                .Select(d => new
                {
                    d.MaDeTai,
                    d.TenDeTai,
                    d.MaSv,
                    TenSinhVien = d.MaSvNavigation != null ? d.MaSvNavigation.HoTen : "",
                    d.MaGv,
                    TenGVHD = d.MaGvNavigation != null ? d.MaGvNavigation.HoTen : ""
                })
                .ToList();

            dgvDoAn.DataSource = doAns;

            if (dgvDoAn.Columns["MaDeTai"] != null)
                dgvDoAn.Columns["MaDeTai"].HeaderText = "Mã đề tài";
            if (dgvDoAn.Columns["TenDeTai"] != null)
                dgvDoAn.Columns["TenDeTai"].HeaderText = "Tên đề tài";
            if (dgvDoAn.Columns["MaSv"] != null)
                dgvDoAn.Columns["MaSv"].Visible = false;
            if (dgvDoAn.Columns["TenSinhVien"] != null)
                dgvDoAn.Columns["TenSinhVien"].HeaderText = "Sinh viên";
            if (dgvDoAn.Columns["MaGvhd"] != null)
                dgvDoAn.Columns["MaGvhd"].Visible = false;
            if (dgvDoAn.Columns["TenGVHD"] != null)
                dgvDoAn.Columns["TenGVHD"].HeaderText = "GVHD";
        }

        private void BtnPhanCong_Click(object sender, EventArgs e)
        {
            if (dgvDoAn.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn đồ án cần phân công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maDeTai = dgvDoAn.CurrentRow.Cells["MaDeTai"].Value?.ToString() ?? "";
            string tenDeTai = dgvDoAn.CurrentRow.Cells["TenDeTai"].Value?.ToString() ?? "";

            var form = new PhanCongGiangVienForm(maDeTai, tenDeTai);
            form.ShowDialog();
            LoadData();
        }
    }
}
