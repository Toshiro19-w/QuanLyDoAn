using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuanLyDoAn.Controller;
using QuanLyDoAn.Model.Entities;
using QuanLyDoAn.Utils;

namespace QuanLyDoAn.View
{
    public class QuanLyGiangVienControl : UserControl
    {
        private readonly QuanLyGiangVienAdminController controller;
        private DataGridView dgvGiangVien = null!;
        private TextBox txtMaGv = null!;
        private TextBox txtHoTen = null!;
        private TextBox txtEmail = null!;

        private TextBox txtBoMon = null!;
        private TextBox txtChucVu = null!;
        private ComboBox cboChuyenNganh = null!;
        private Button btnThem = null!;
        private Button btnSua = null!;
        private Button btnXoa = null!;
        private Button btnClear = null!;

        public QuanLyGiangVienControl()
        {
            controller = new QuanLyGiangVienAdminController();
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
                RowCount = 4,
                Dock = DockStyle.Top,
                AutoSize = true
            };

            formLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
            formLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            formLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
            formLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));

            txtMaGv = CreateTextBox();
            txtHoTen = CreateTextBox();
            txtEmail = CreateTextBox();

            txtBoMon = CreateTextBox();
            txtChucVu = CreateTextBox();
            cboChuyenNganh = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 200 };

            AddFormRow(formLayout, 0, "Mã giảng viên", txtMaGv, "Họ tên", txtHoTen);
            AddFormRow(formLayout, 1, "Email", txtEmail, "Chức vụ", txtChucVu);
            AddFormRow(formLayout, 2, "Bộ môn", txtBoMon, string.Empty, null);
            AddFormRow(formLayout, 3, "Chuyên ngành", cboChuyenNganh, string.Empty, null);

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

            dgvGiangVien = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgvGiangVien.SelectionChanged += DgvGiangVien_SelectionChanged;

            mainLayout.Controls.Add(formLayout, 0, 0);
            mainLayout.Controls.Add(buttonPanel, 0, 1);
            mainLayout.Controls.Add(dgvGiangVien, 0, 2);
            Controls.Add(mainLayout);
        }

        private void AddFormRow(TableLayoutPanel layout, int rowIndex, string label1, Control control1, string label2, Control? control2)
        {
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.Controls.Add(new Label { Text = label1, AutoSize = true, Anchor = AnchorStyles.Left }, 0, rowIndex);
            layout.Controls.Add(control1, 1, rowIndex);

            if (!string.IsNullOrEmpty(label2) && control2 != null)
            {
                layout.Controls.Add(new Label { Text = label2, AutoSize = true, Anchor = AnchorStyles.Left }, 2, rowIndex);
                layout.Controls.Add(control2, 3, rowIndex);
            }
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
            var data = controller.LayDanhSachGiangVien();
            
            // Tạo danh sách với tên chuyên ngành
            var displayList = data.Select(gv => new
            {
                gv.MaGv,
                gv.HoTen,
                gv.Email,
                gv.BoMon,
                gv.ChucVu,
                TenChuyenNganh = gv.MaChuyenNganhNavigation?.TenChuyenNganh ?? "",
                gv.MaChuyenNganh
            }).ToList();
            
            dgvGiangVien.DataSource = displayList;

            // Ẩn cột mã chuyên ngành
            if (dgvGiangVien.Columns["MaChuyenNganh"] != null)
                dgvGiangVien.Columns["MaChuyenNganh"].Visible = false;

            if (dgvGiangVien.Columns["MaGv"] != null)
                dgvGiangVien.Columns["MaGv"].HeaderText = "Mã GV";
            if (dgvGiangVien.Columns["HoTen"] != null)
                dgvGiangVien.Columns["HoTen"].HeaderText = "Họ tên";
            if (dgvGiangVien.Columns["Email"] != null)
                dgvGiangVien.Columns["Email"].HeaderText = "Email";
            if (dgvGiangVien.Columns["BoMon"] != null)
                dgvGiangVien.Columns["BoMon"].HeaderText = "Bộ môn";
            if (dgvGiangVien.Columns["ChucVu"] != null)
                dgvGiangVien.Columns["ChucVu"].HeaderText = "Chức vụ";
            if (dgvGiangVien.Columns["TenChuyenNganh"] != null)
                dgvGiangVien.Columns["TenChuyenNganh"].HeaderText = "Chuyên ngành";
        }

        private void DgvGiangVien_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvGiangVien.CurrentRow?.DataBoundItem == null) return;
            
            var item = dgvGiangVien.CurrentRow.DataBoundItem;
            var maGv = item.GetType().GetProperty("MaGv")?.GetValue(item)?.ToString() ?? "";
            var hoTen = item.GetType().GetProperty("HoTen")?.GetValue(item)?.ToString() ?? "";
            var email = item.GetType().GetProperty("Email")?.GetValue(item)?.ToString() ?? "";
            var boMon = item.GetType().GetProperty("BoMon")?.GetValue(item)?.ToString() ?? "";
            var chucVu = item.GetType().GetProperty("ChucVu")?.GetValue(item)?.ToString() ?? "";
            var maChuyenNganh = item.GetType().GetProperty("MaChuyenNganh")?.GetValue(item)?.ToString() ?? "";
            
            txtMaGv.Text = maGv;
            txtHoTen.Text = hoTen;
            txtEmail.Text = email;
            txtBoMon.Text = boMon;
            txtChucVu.Text = chucVu;
            cboChuyenNganh.SelectedValue = maChuyenNganh;
            txtMaGv.Enabled = false;
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            var gv = GetGiangVienFromForm();
            gv.MaGv = txtMaGv.Text.Trim();

            if (controller.ThemGiangVien(gv, out string error))
            {
                MessageBox.Show("Đã thêm giảng viên.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            var gv = GetGiangVienFromForm();
            gv.MaGv = txtMaGv.Text.Trim();

            if (controller.CapNhatGiangVien(gv, out string error))
            {
                MessageBox.Show("Đã cập nhật giảng viên.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            else
            {
                MessageBox.Show(error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            var maGv = txtMaGv.Text.Trim();
            if (string.IsNullOrEmpty(maGv)) return;

            if (MessageBox.Show($"Bạn có chắc muốn xóa giảng viên {maGv}?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            if (controller.XoaGiangVien(maGv, out string error))
            {
                MessageBox.Show("Đã xóa giảng viên.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                ClearForm();
            }
            else
            {
                MessageBox.Show(error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private GiangVien GetGiangVienFromForm()
        {
            return new GiangVien
            {
                HoTen = txtHoTen.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                BoMon = txtBoMon.Text.Trim(),
                ChucVu = txtChucVu.Text.Trim(),
                MaChuyenNganh = cboChuyenNganh.SelectedValue?.ToString()
            };
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtMaGv.Text) || string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập mã và họ tên giảng viên.");
                return false;
            }
            return true;
        }

        private void ClearForm()
        {
            txtMaGv.Clear();
            txtHoTen.Clear();
            txtEmail.Clear();
            txtBoMon.Clear();
            txtChucVu.Clear();
            cboChuyenNganh.SelectedIndex = -1;
            txtMaGv.Enabled = true;
            dgvGiangVien.ClearSelection();
        }
    }
}

