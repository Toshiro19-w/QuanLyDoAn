using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyDoAn.Controller;
using QuanLyDoAn.Model.Entities;
using QuanLyDoAn.Utils;

namespace QuanLyDoAn.View
{
    public class QuanLyDanhMucControl : UserControl
    {
        private readonly QuanLyDanhMucController controller;

        private DataGridView dgvLoaiDoAn = null!;
        private DataGridView dgvKyHoc = null!;
        private DataGridView dgvChuyenNganh = null!;

        private TextBox txtMaLoai = null!;
        private TextBox txtTenLoai = null!;

        private TextBox txtMaKy = null!;
        private TextBox txtTenKy = null!;

        private TextBox txtMaChuyenNganh = null!;
        private TextBox txtTenChuyenNganh = null!;

        public QuanLyDanhMucControl()
        {
            controller = new QuanLyDanhMucController();
            InitializeComponent();
            ThemeHelper.ApplyTheme(this);
            LoadAllData();
        }

        private void InitializeComponent()
        {
            Dock = DockStyle.Fill;
            var tabControl = new TabControl { Dock = DockStyle.Fill };

            tabControl.TabPages.Add(CreateLoaiDoAnTab());
            tabControl.TabPages.Add(CreateKyHocTab());
            tabControl.TabPages.Add(CreateChuyenNganhTab());

            Controls.Add(tabControl);
        }

        private TabPage CreateLoaiDoAnTab()
        {
            var tab = new TabPage("Loại đồ án");
            var layout = CreateTabLayout();

            txtMaLoai = CreateTextBox();
            txtTenLoai = CreateTextBox();
            var form = CreateFormPanel();
            AddLabelControl(form, "Mã loại", txtMaLoai, 0);
            AddLabelControl(form, "Tên loại", txtTenLoai, 1);

            var buttons = CreateButtonPanel();
            buttons.Controls.Add(CreateButton("Thêm", (s, e) =>
            {
                var entity = new LoaiDoAn
                {
                    MaLoaiDoAn = txtMaLoai.Text.Trim(),
                    TenLoaiDoAn = txtTenLoai.Text.Trim()
                };
                if (controller.ThemLoaiDoAn(entity, out string err))
                {
                    LoadLoaiDoAn();
                    ClearLoaiDoAnForm();
                    MessageBox.Show("Đã thêm loại đồ án.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(err, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }));
            buttons.Controls.Add(CreateButton("Cập nhật", (s, e) =>
            {
                var entity = new LoaiDoAn
                {
                    MaLoaiDoAn = txtMaLoai.Text.Trim(),
                    TenLoaiDoAn = txtTenLoai.Text.Trim()
                };
                if (controller.CapNhatLoaiDoAn(entity, out string err))
                {
                    LoadLoaiDoAn();
                    MessageBox.Show("Đã cập nhật loại đồ án.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(err, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }));
            buttons.Controls.Add(CreateButton("Xóa", (s, e) =>
            {
                var ma = txtMaLoai.Text.Trim();
                if (string.IsNullOrEmpty(ma)) return;
                if (controller.XoaLoaiDoAn(ma, out string err))
                {
                    LoadLoaiDoAn();
                    ClearLoaiDoAnForm();
                    MessageBox.Show("Đã xóa loại đồ án.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(err, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }));

            dgvLoaiDoAn = CreateGrid();
            dgvLoaiDoAn.SelectionChanged += (s, e) =>
            {
                if (dgvLoaiDoAn.CurrentRow?.DataBoundItem is LoaiDoAn loai)
                {
                    txtMaLoai.Text = loai.MaLoaiDoAn;
                    txtTenLoai.Text = loai.TenLoaiDoAn ?? "";
                    txtMaLoai.Enabled = false;
                }
            };

            layout.Controls.Add(form);
            layout.Controls.Add(buttons);
            layout.Controls.Add(dgvLoaiDoAn);

            tab.Controls.Add(layout);
            return tab;
        }

        private TabPage CreateKyHocTab()
        {
            var tab = new TabPage("Kỳ học");
            var layout = CreateTabLayout();

            txtMaKy = CreateTextBox();
            txtTenKy = CreateTextBox();
            var form = CreateFormPanel();
            AddLabelControl(form, "Mã kỳ", txtMaKy, 0);
            AddLabelControl(form, "Tên kỳ", txtTenKy, 1);

            var buttons = CreateButtonPanel();
            buttons.Controls.Add(CreateButton("Thêm", (s, e) => SaveKyHoc(true)));
            buttons.Controls.Add(CreateButton("Cập nhật", (s, e) => SaveKyHoc(false)));
            buttons.Controls.Add(CreateButton("Xóa", (s, e) =>
            {
                var ma = txtMaKy.Text.Trim();
                if (string.IsNullOrEmpty(ma)) return;
                if (controller.XoaKyHoc(ma, out string err))
                {
                    LoadKyHoc();
                    ClearKyHocForm();
                    MessageBox.Show("Đã xóa kỳ học.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(err, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }));

            dgvKyHoc = CreateGrid();
            dgvKyHoc.SelectionChanged += (s, e) =>
            {
                if (dgvKyHoc.CurrentRow?.DataBoundItem is KyHoc ky)
                {
                    txtMaKy.Text = ky.MaKy;
                    txtTenKy.Text = ky.TenKy ?? "";
                    txtMaKy.Enabled = false;
                }
            };

            layout.Controls.Add(form);
            layout.Controls.Add(buttons);
            layout.Controls.Add(dgvKyHoc);

            tab.Controls.Add(layout);
            return tab;
        }

        private TabPage CreateChuyenNganhTab()
        {
            var tab = new TabPage("Chuyên ngành");
            var layout = CreateTabLayout();

            txtMaChuyenNganh = CreateTextBox();
            txtTenChuyenNganh = CreateTextBox();
            var form = CreateFormPanel();
            AddLabelControl(form, "Mã chuyên ngành", txtMaChuyenNganh, 0);
            AddLabelControl(form, "Tên chuyên ngành", txtTenChuyenNganh, 1);

            var buttons = CreateButtonPanel();
            buttons.Controls.Add(CreateButton("Thêm", (s, e) => SaveChuyenNganh(true)));
            buttons.Controls.Add(CreateButton("Cập nhật", (s, e) => SaveChuyenNganh(false)));
            buttons.Controls.Add(CreateButton("Xóa", (s, e) =>
            {
                var ma = txtMaChuyenNganh.Text.Trim();
                if (string.IsNullOrEmpty(ma)) return;
                if (controller.XoaChuyenNganh(ma, out string err))
                {
                    LoadChuyenNganh();
                    ClearChuyenNganhForm();
                    MessageBox.Show("Đã xóa chuyên ngành.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(err, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }));

            dgvChuyenNganh = CreateGrid();
            dgvChuyenNganh.SelectionChanged += (s, e) =>
            {
                if (dgvChuyenNganh.CurrentRow?.DataBoundItem is ChuyenNganh cn)
                {
                    txtMaChuyenNganh.Text = cn.MaChuyenNganh;
                    txtTenChuyenNganh.Text = cn.TenChuyenNganh ?? "";
                    txtMaChuyenNganh.Enabled = false;
                }
            };

            layout.Controls.Add(form);
            layout.Controls.Add(buttons);
            layout.Controls.Add(dgvChuyenNganh);

            tab.Controls.Add(layout);
            return tab;
        }

        private TableLayoutPanel CreateTabLayout()
        {
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1,
                Padding = new Padding(10)
            };
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            return layout;
        }

        private TableLayoutPanel CreateFormPanel()
        {
            return new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 2,
                Dock = DockStyle.Top,
                AutoSize = true
            };
        }

        private FlowLayoutPanel CreateButtonPanel()
        {
            return new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                Padding = new Padding(0, 10, 0, 10)
            };
        }

        private TextBox CreateTextBox() => new TextBox { Width = 220 };

        private DataGridView CreateGrid()
        {
            return new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
        }

        private Button CreateButton(string text, EventHandler handler)
        {
            var button = new Button
            {
                Text = text,
                Width = 100,
                Height = 30,
                Margin = new Padding(0, 0, 10, 0)
            };
            button.Click += handler;
            return button;
        }

        private void AddLabelControl(TableLayoutPanel layout, string label, Control control, int row)
        {
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.Controls.Add(new Label { Text = label, AutoSize = true, Anchor = AnchorStyles.Left }, 0, row);
            layout.Controls.Add(control, 1, row);
        }

        private void LoadAllData()
        {
            LoadLoaiDoAn();
            LoadKyHoc();
            LoadChuyenNganh();
        }

        private void LoadLoaiDoAn()
        {
            dgvLoaiDoAn.DataSource = controller.LayLoaiDoAn();
            if (dgvLoaiDoAn.Columns["MaLoaiDoAn"] != null)
                dgvLoaiDoAn.Columns["MaLoaiDoAn"].HeaderText = "Mã loại";
            if (dgvLoaiDoAn.Columns["TenLoaiDoAn"] != null)
                dgvLoaiDoAn.Columns["TenLoaiDoAn"].HeaderText = "Tên loại";
        }

        private void LoadKyHoc()
        {
            dgvKyHoc.DataSource = controller.LayKyHoc();
            if (dgvKyHoc.Columns["MaKy"] != null)
                dgvKyHoc.Columns["MaKy"].HeaderText = "Mã kỳ";
            if (dgvKyHoc.Columns["TenKy"] != null)
                dgvKyHoc.Columns["TenKy"].HeaderText = "Tên kỳ";
        }

        private void LoadChuyenNganh()
        {
            dgvChuyenNganh.DataSource = controller.LayChuyenNganh();
            if (dgvChuyenNganh.Columns["MaChuyenNganh"] != null)
                dgvChuyenNganh.Columns["MaChuyenNganh"].HeaderText = "Mã chuyên ngành";
            if (dgvChuyenNganh.Columns["TenChuyenNganh"] != null)
                dgvChuyenNganh.Columns["TenChuyenNganh"].HeaderText = "Tên chuyên ngành";
        }

        private void ClearLoaiDoAnForm()
        {
            txtMaLoai.Clear();
            txtTenLoai.Clear();
            txtMaLoai.Enabled = true;
            dgvLoaiDoAn.ClearSelection();
        }

        private void SaveKyHoc(bool isNew)
        {
            if (!ValidateRequired(txtMaKy.Text, txtTenKy.Text)) return;
            var entity = new KyHoc { MaKy = txtMaKy.Text.Trim(), TenKy = txtTenKy.Text.Trim() };
            if (controller.LuuKyHoc(entity, isNew, out string err))
            {
                LoadKyHoc();
                MessageBox.Show(isNew ? "Đã thêm kỳ học." : "Đã cập nhật kỳ học.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (isNew) ClearKyHocForm();
            }
            else
            {
                MessageBox.Show(err, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ClearKyHocForm()
        {
            txtMaKy.Clear();
            txtTenKy.Clear();
            txtMaKy.Enabled = true;
            dgvKyHoc.ClearSelection();
        }

        private void SaveChuyenNganh(bool isNew)
        {
            if (!ValidateRequired(txtMaChuyenNganh.Text, txtTenChuyenNganh.Text)) return;
            var entity = new ChuyenNganh
            {
                MaChuyenNganh = txtMaChuyenNganh.Text.Trim(),
                TenChuyenNganh = txtTenChuyenNganh.Text.Trim()
            };

            if (controller.LuuChuyenNganh(entity, isNew, out string err))
            {
                LoadChuyenNganh();
                MessageBox.Show(isNew ? "Đã thêm chuyên ngành." : "Đã cập nhật chuyên ngành.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (isNew) ClearChuyenNganhForm();
            }
            else
            {
                MessageBox.Show(err, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ClearChuyenNganhForm()
        {
            txtMaChuyenNganh.Clear();
            txtTenChuyenNganh.Clear();
            txtMaChuyenNganh.Enabled = true;
            dgvChuyenNganh.ClearSelection();
        }

        private bool ValidateRequired(params string[] values)
        {
            if (values == null) return false;
            foreach (var value in values)
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                    return false;
                }
            }
            return true;
        }
    }
}

