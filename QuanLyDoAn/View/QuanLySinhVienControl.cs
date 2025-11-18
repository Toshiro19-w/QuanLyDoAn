using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyDoAn.Controller;
using QuanLyDoAn.Model.Entities;
using QuanLyDoAn.Utils;

namespace QuanLyDoAn.View
{
    public class QuanLySinhVienControl : UserControl
    {
        private readonly QuanLySinhVienAdminController controller;
        private DataGridView dgvSinhVien = null!;
        private TextBox txtMaSv = null!;
        private TextBox txtHoTen = null!;
        private TextBox txtEmail = null!;
        private TextBox txtSoDienThoai = null!;
        private TextBox txtLop = null!;
        private ComboBox cboChuyenNganh = null!;
        private Button btnThem = null!;
        private Button btnSua = null!;
        private Button btnXoa = null!;
        private Button btnClear = null!;

        public QuanLySinhVienControl()
        {
            controller = new QuanLySinhVienAdminController();
            InitializeComponent();
            ThemeHelper.ApplyTheme(this);
            LoadChuyenNganh();
            LoadData();
        }

        private void InitializeComponent()
        {
            Dock = DockStyle.Fill;
            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(15),
                RowStyles =
                {
                    new RowStyle(SizeType.AutoSize),
                    new RowStyle(SizeType.AutoSize),
                    new RowStyle(SizeType.Percent, 100)
                }
            };

            var formLayout = new TableLayoutPanel
            {
                ColumnCount = 4,
                RowCount = 3,
                Dock = DockStyle.Top,
                AutoSize = true
            };
            formLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
            formLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            formLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
            formLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));

            txtMaSv = CreateTextBox();
            txtHoTen = CreateTextBox();
            txtEmail = CreateTextBox();
            txtSoDienThoai = CreateTextBox();
            txtLop = CreateTextBox();
            cboChuyenNganh = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 200 };

            AddFormRow(formLayout, 0, "Mã sinh viên", txtMaSv, "Họ tên", txtHoTen);
            AddFormRow(formLayout, 1, "Email", txtEmail, "Số điện thoại", txtSoDienThoai);
            AddFormRow(formLayout, 2, "Lớp", txtLop, "Chuyên ngành", cboChuyenNganh);

            var buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                FlowDirection = FlowDirection.LeftToRight,
                Height = 45,
                Padding = new Padding(0, 10, 0, 10)
            };
            btnThem = CreateButton("Thêm", BtnThem_Click);
            btnSua = CreateButton("Sửa", BtnSua_Click);
            btnXoa = CreateButton("Xóa", BtnXoa_Click);
            btnClear = CreateButton("Làm mới", (s, e) => ClearForm());
            buttonPanel.Controls.AddRange(new Control[] { btnThem, btnSua, btnXoa, btnClear });

            dgvSinhVien = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgvSinhVien.SelectionChanged += DgvSinhVien_SelectionChanged;

            mainLayout.Controls.Add(formLayout, 0, 0);
            mainLayout.Controls.Add(buttonPanel, 0, 1);
            mainLayout.Controls.Add(dgvSinhVien, 0, 2);
            Controls.Add(mainLayout);
        }

        private void AddFormRow(TableLayoutPanel layout, int rowIndex, string label1, Control control1, string label2, Control control2)
        {
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.Controls.Add(new Label { Text = label1, AutoSize = true, Anchor = AnchorStyles.Left }, 0, rowIndex);
            layout.Controls.Add(control1, 1, rowIndex);

            layout.Controls.Add(new Label { Text = label2, AutoSize = true, Anchor = AnchorStyles.Left }, 2, rowIndex);
            layout.Controls.Add(control2, 3, rowIndex);
        }

        private TextBox CreateTextBox() => new TextBox { Width = 200 };

        private Button CreateButton(string text, EventHandler handler)
        {
            var button = new Button
            {
                Text = text,
                Width = 110,
                Height = 30,
                Margin = new Padding(0, 0, 10, 0)
            };
            button.Click += handler;
            return button;
        }

        private void LoadChuyenNganh()
        {
            var list = controller.LayDanhSachChuyenNganh();
            cboChuyenNganh.DataSource = list;
            cboChuyenNganh.DisplayMember = nameof(ChuyenNganh.TenChuyenNganh);
            cboChuyenNganh.ValueMember = nameof(ChuyenNganh.MaChuyenNganh);
        }

        private void LoadData()
        {
            var data = controller.LayDanhSachSinhVien();
            dgvSinhVien.DataSource = data;

            if (dgvSinhVien.Columns["MaChuyenNganhNavigation"] != null)
                dgvSinhVien.Columns["MaChuyenNganhNavigation"].Visible = false;
            if (dgvSinhVien.Columns["TaiKhoans"] != null)
                dgvSinhVien.Columns["TaiKhoans"].Visible = false;
            if (dgvSinhVien.Columns["DoAn"] != null)
                dgvSinhVien.Columns["DoAn"].Visible = false;

            if (dgvSinhVien.Columns["MaSv"] != null)
                dgvSinhVien.Columns["MaSv"].HeaderText = "Mã SV";
            if (dgvSinhVien.Columns["HoTen"] != null)
                dgvSinhVien.Columns["HoTen"].HeaderText = "Họ tên";
            if (dgvSinhVien.Columns["Email"] != null)
                dgvSinhVien.Columns["Email"].HeaderText = "Email";
            if (dgvSinhVien.Columns["SoDienThoai"] != null)
                dgvSinhVien.Columns["SoDienThoai"].HeaderText = "Số điện thoại";
            if (dgvSinhVien.Columns["Lop"] != null)
                dgvSinhVien.Columns["Lop"].HeaderText = "Lớp";
            if (dgvSinhVien.Columns["MaChuyenNganh"] != null)
                dgvSinhVien.Columns["MaChuyenNganh"].HeaderText = "Mã chuyên ngành";
        }

        private void DgvSinhVien_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvSinhVien.CurrentRow?.DataBoundItem is not SinhVien sv) return;
            txtMaSv.Text = sv.MaSv;
            txtHoTen.Text = sv.HoTen ?? "";
            txtEmail.Text = sv.Email ?? "";
            txtSoDienThoai.Text = sv.SoDienThoai ?? "";
            txtLop.Text = sv.Lop ?? "";
            cboChuyenNganh.SelectedValue = sv.MaChuyenNganh ?? "";
            txtMaSv.Enabled = false;
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            var sv = GetSinhVienFromForm();
            sv.MaSv = txtMaSv.Text.Trim();

            if (controller.ThemSinhVien(sv, out string error))
            {
                MessageBox.Show("Đã thêm sinh viên.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                ClearForm();
            }
            else
            {
                MessageBox.Show(error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnSua_Click(object? sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            var sv = GetSinhVienFromForm();
            sv.MaSv = txtMaSv.Text.Trim();

            if (controller.CapNhatSinhVien(sv, out string error))
            {
                MessageBox.Show("Đã cập nhật sinh viên.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            else
            {
                MessageBox.Show(error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            var maSv = txtMaSv.Text.Trim();
            if (string.IsNullOrEmpty(maSv)) return;

            if (MessageBox.Show($"Bạn có chắc muốn xóa sinh viên {maSv}?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            if (controller.XoaSinhVien(maSv, out string error))
            {
                MessageBox.Show("Đã xóa sinh viên.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                ClearForm();
            }
            else
            {
                MessageBox.Show(error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private SinhVien GetSinhVienFromForm()
        {
            return new SinhVien
            {
                HoTen = txtHoTen.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                SoDienThoai = txtSoDienThoai.Text.Trim(),
                Lop = txtLop.Text.Trim(),
                MaChuyenNganh = cboChuyenNganh.SelectedValue?.ToString()
            };
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtMaSv.Text) || string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập mã và họ tên sinh viên.");
                return false;
            }
            return true;
        }

        private void ClearForm()
        {
            txtMaSv.Clear();
            txtHoTen.Clear();
            txtEmail.Clear();
            txtSoDienThoai.Clear();
            txtLop.Clear();
            cboChuyenNganh.SelectedIndex = -1;
            txtMaSv.Enabled = true;
            dgvSinhVien.ClearSelection();
        }
    }
}

