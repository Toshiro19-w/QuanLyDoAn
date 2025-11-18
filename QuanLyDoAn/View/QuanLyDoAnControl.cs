using System;
using System.Linq;
using System.Windows.Forms;
using QuanLyDoAn.Controller;
using QuanLyDoAn.Model.EF;
using QuanLyDoAn.Model.Entities;
using QuanLyDoAn.Model.ViewModels;
using QuanLyDoAn.Utils;

namespace QuanLyDoAn.View
{
    public partial class QuanLyDoAnControl : UserControl
    {
        private DoAnController doAnController;
        private System.Collections.IList loaiDoAnList;
        private Dictionary<string, string> loaiDoAnMap; // Map MaLoaiDoAn -> TenLoaiDoAn
        private bool suppressCellValueChanged = false;
        private string? previousMaLoaiValue = null;
        private bool suppressComboChange = false;

        public QuanLyDoAnControl()
        {
            InitializeComponent();
            doAnController = new DoAnController();
            loaiDoAnMap = new Dictionary<string, string>();
            LoadComboBoxData();
            LoadData();
            // subscribe to events for editable combobox column
            dgvDoAn.CurrentCellDirtyStateChanged += DgvDoAn_CurrentCellDirtyStateChanged;
            dgvDoAn.CellValueChanged += DgvDoAn_CellValueChanged;
            dgvDoAn.CellBeginEdit += DgvDoAn_CellBeginEdit;

            // handle top-level combo change to update MaLoai for selected row
            if (cmbMaLoai != null)
            {
                cmbMaLoai.SelectionChangeCommitted += CmbMaLoai_SelectionChangeCommitted;
            }
            
            // configure controls by role (shows/hides create/approve buttons)
            AuthorizationHelper.ConfigureControlsByRole(this.Controls);

            ApplyTheme();
        }

        private void CmbMaLoai_SelectionChangeCommitted(object? sender, EventArgs e)
        {
            if (suppressComboChange) return;

            var maDeTai = txtMaDeTai.Text;
            if (string.IsNullOrEmpty(maDeTai))
            {
                MessageBox.Show("Vui lòng chọn đề tài trước khi thay đổi loại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var maLoai = cmbMaLoai.SelectedValue?.ToString();
            if (string.IsNullOrEmpty(maLoai)) return;

            if (!doAnController.CapNhatLoaiDoAn(maDeTai, maLoai, out string err))
            {
                MessageBox.Show(err, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Cập nhật loại đồ án thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadData();
        }

        private void DgvDoAn_CellBeginEdit(object? sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvDoAn.Columns[e.ColumnIndex].Name != "MaLoaiDoAn") return;
            previousMaLoaiValue = dgvDoAn.Rows[e.RowIndex].Cells["MaLoaiDoAn"].Value?.ToString();
        }

        private void LoadData()
        {
            try
            {
                // Load loai doan data FIRST before loading grid data
                EnsureLoaiDoAnColumn();

                var danhSachDoAn = doAnController.LayDanhSachDoAn();
                dgvDoAn.DataSource = danhSachDoAn;

                // Set the MaLoaiDoAn cell values for each row from DB (so combobox shows current value)
                using (var context = new QuanLyDoAnContext())
                {
                    suppressCellValueChanged = true;
                    foreach (DataGridViewRow row in dgvDoAn.Rows)
                    {
                        if (row.DataBoundItem is DoAnViewModel vm)
                        {
                            var doAnEntity = context.DoAns.Find(vm.MaDeTai);
                            if (doAnEntity != null && dgvDoAn.Columns["MaLoaiDoAn"] != null)
                            {
                                // Display TenLoaiDoAn instead of MaLoaiDoAn
                                var maLoai = doAnEntity.MaLoaiDoAn;
                                var tenLoai = loaiDoAnMap.ContainsKey(maLoai ?? "") 
                                    ? loaiDoAnMap[maLoai] 
                                    : maLoai ?? "";
                                row.Cells["MaLoaiDoAn"].Value = tenLoai;
                            }
                        }
                    }
                    suppressCellValueChanged = false;
                }

                // Configure display headers and order including the new "Loại đồ án" column
                if (dgvDoAn.Columns["MaDeTai"] != null)
                {
                    dgvDoAn.Columns["MaDeTai"].HeaderText = "Mã đề tài";
                    dgvDoAn.Columns["MaDeTai"].DisplayIndex = 0;
                }
                if (dgvDoAn.Columns["TenDeTai"] != null)
                {
                    dgvDoAn.Columns["TenDeTai"].HeaderText = "Tên đề tài";
                    dgvDoAn.Columns["TenDeTai"].DisplayIndex = 1;
                }
                // new editable column
                if (dgvDoAn.Columns["MaLoaiDoAn"] != null)
                {
                    dgvDoAn.Columns["MaLoaiDoAn"].HeaderText = "Loại đồ án";
                    dgvDoAn.Columns["MaLoaiDoAn"].DisplayIndex = 2;
                }
                if (dgvDoAn.Columns["DanhSachSinhVien"] != null)
                {
                    dgvDoAn.Columns["DanhSachSinhVien"].HeaderText = "Sinh viên";
                    dgvDoAn.Columns["DanhSachSinhVien"].DisplayIndex = 3;
                }
                if (dgvDoAn.Columns["TenGiangVien"] != null)
                {
                    dgvDoAn.Columns["TenGiangVien"].HeaderText = "Giảng viên hướng dẫn";
                    dgvDoAn.Columns["TenGiangVien"].DisplayIndex = 4;
                }
                if (dgvDoAn.Columns["NgayBatDau"] != null)
                {
                    dgvDoAn.Columns["NgayBatDau"].HeaderText = "Ngày bắt đầu";
                    dgvDoAn.Columns["NgayBatDau"].DisplayIndex = 5;
                }
                if (dgvDoAn.Columns["NgayKetThuc"] != null)
                {
                    dgvDoAn.Columns["NgayKetThuc"].HeaderText = "Ngày kết thúc";
                    dgvDoAn.Columns["NgayKetThuc"].DisplayIndex = 6;
                }
                if (dgvDoAn.Columns["TrangThai"] != null)
                {
                    dgvDoAn.Columns["TrangThai"].HeaderText = "Trạng thái";
                    dgvDoAn.Columns["TrangThai"].DisplayIndex = 7;
                }
                if (dgvDoAn.Columns["DiemText"] != null)
                {
                    dgvDoAn.Columns["DiemText"].HeaderText = "Điểm";
                    dgvDoAn.Columns["DiemText"].DisplayIndex = 8;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Populate the design-time DataGridViewComboBoxColumn named "MaLoaiDoAn".
        /// NOTE: The DataGridViewComboBoxColumn should be added in the designer with Name = "MaLoaiDoAn".
        /// This method will set its DataSource/DisplayMember/ValueMember at runtime.
        /// </summary>
        private void EnsureLoaiDoAnColumn()
        {
            // If the column was not added in designer, do nothing and show a debug message.
            if (dgvDoAn.Columns["MaLoaiDoAn"] == null)
            {
                // It's intentional: require developer to add a DataGridViewComboBoxColumn in design with Name = "MaLoaiDoAn".
                return;
            }

            try
            {
                using var context = new QuanLyDoAnContext();
                // load list of types: assume entity has MaLoaiDoAn and TenLoaiDoAn
                loaiDoAnList = context.LoaiDoAns
                    .Select(l => new { l.MaLoaiDoAn, l.TenLoaiDoAn })
                    .ToList();

                // Build map for quick lookup
                loaiDoAnMap.Clear();
                foreach (dynamic item in loaiDoAnList)
                {
                    loaiDoAnMap[item.MaLoaiDoAn] = item.TenLoaiDoAn;
                }

                // populate the top-level cmbMaLoai (outside grid) so user can edit via that combobox
                if (cmbMaLoai != null)
                {
                    cmbMaLoai.DataSource = loaiDoAnList;
                    cmbMaLoai.DisplayMember = "TenLoaiDoAn";
                    cmbMaLoai.ValueMember = "MaLoaiDoAn";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách loại đồ án: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvDoAn_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
        {
            // Commit edit immediately when combobox value changes so CellValueChanged fires
            if (dgvDoAn.IsCurrentCellDirty && dgvDoAn.CurrentCell is DataGridViewComboBoxCell)
            {
                dgvDoAn.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void DgvDoAn_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            // Ignore direct edits on the grid for MaLoaiDoAn (grid is display-only)
            if (e.RowIndex < 0 || dgvDoAn.Columns[e.ColumnIndex].Name == "MaLoaiDoAn") return;

            // Keep existing behavior for other columns if needed
        }

        private void dgvDoAn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvDoAn.Rows[e.RowIndex].DataBoundItem is DoAnViewModel doAn)
            {
                try
                {
                    // Load thông tin cơ bản
                    txtMaDeTai.Text = doAn.MaDeTai;
                    txtTenDeTai.Text = doAn.TenDeTai;
                    txtDiem.Text = doAn.DiemText == "Chưa có điểm" ? "" : doAn.DiemText;
                    
                    // Load mô tả từ database
                    using var context = new QuanLyDoAnContext();
                    var doAnEntity = context.DoAns.Find(doAn.MaDeTai);
                    if (txtMoTa != null && doAnEntity != null)
                        txtMoTa.Text = doAnEntity.MoTa ?? "";

                    // Chọn giảng viên
                    for (int i = 0; i < cmbMaGv.Items.Count; i++)
                    {
                        var item = cmbMaGv.Items[i];
                        if (item.GetType().GetProperty("HoTen")?.GetValue(item)?.ToString() == doAn.TenGiangVien)
                        {
                            cmbMaGv.SelectedIndex = i;
                            break;
                        }
                    }
                    
                    // Chọn sinh viên
                    for (int i = 0; i < cmbMaSv.Items.Count; i++)
                    {
                        var item = cmbMaSv.Items[i];
                        if (item.GetType().GetProperty("HoTen")?.GetValue(item)?.ToString() == doAn.DanhSachSinhVien)
                        {
                            cmbMaSv.SelectedIndex = i;
                            break;
                        }
                    }

                    // Set LoaiDoAn combobox (outside grid) to value from DB
                    if (cmbMaLoai != null && doAnEntity != null)
                    {
                        suppressComboChange = true;
                        cmbMaLoai.SelectedValue = doAnEntity.MaLoaiDoAn ?? "";
                        suppressComboChange = false;
                    }
                    
                    // Load ngày tháng
                    if (doAn.NgayBatDau.HasValue)
                        dtpNgayBatDau.Value = doAn.NgayBatDau.Value.ToDateTime(TimeOnly.MinValue);
                    if (doAn.NgayKetThuc.HasValue)
                        dtpNgayKetThuc.Value = doAn.NgayKetThuc.Value.ToDateTime(TimeOnly.MinValue);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi load dữ liệu: {ex.Message}");
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            
            if (cmbMaSv.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn sinh viên");
                return;
            }

            var doAn = new DoAn
            {
                MaDeTai = txtMaDeTai.Text,
                TenDeTai = txtTenDeTai.Text,
                MoTa = txtMoTa?.Text ?? "",
                MaGv = cmbMaGv.SelectedValue?.ToString() ?? "",
                MaSv = cmbMaSv.SelectedValue?.ToString(),
                MaTrangThai = GetDefaultMaTrangThai(),
                NgayBatDau = DateOnly.FromDateTime(dtpNgayBatDau.Value),
                NgayKetThuc = DateOnly.FromDateTime(dtpNgayKetThuc.Value),
                Diem = string.IsNullOrEmpty(txtDiem.Text) ? null : decimal.Parse(txtDiem.Text),
                MaLoaiDoAn = cmbMaLoai?.SelectedValue?.ToString()
            };

            if (doAnController.TaoDoAn(doAn, out string error))
            {
                MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                ClearForm();
            }
            else
            {
                MessageBox.Show(error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaDeTai.Text))
            {
                MessageBox.Show("Vui lòng chọn đồ án cần sửa");
                return;
            }

            // Open TaoDoAnForm in edit mode with the selected MaDeTai
            var editForm = new TaoDoAnForm(txtMaDeTai.Text);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                ClearForm();
            }
         }

         private void btnXoa_Click(object sender, EventArgs e)
         {
             if (string.IsNullOrEmpty(txtMaDeTai.Text))
             {
                 MessageBox.Show("Vui lòng chọn đồ án cần xóa");
                 return;
             }

             MessageBox.Show("Không thể xóa đề tài. Chỉ có thể tạo đề tài mới.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
         }

         private string GetDefaultMaTrangThai()
         {
             try
             {
                 using var context = new QuanLyDoAnContext();
                 var trangThai = context.TrangThaiDoAns.FirstOrDefault();
                 return trangThai?.MaTrangThai ?? "";
             }
             catch
             {
                 return "";
             }
         }

        private void ApplyTheme()
        {
            this.BackColor = Constants.Colors.Background;
            
            foreach (Control control in this.Controls)
            {
                if (control is Button btn)
                {
                    btn.BackColor = Constants.Colors.Primary;
                    btn.ForeColor = System.Drawing.Color.White;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Cursor = Cursors.Hand;
                }
                else if (control is TextBox txt)
                {
                    txt.BackColor = System.Drawing.Color.White;
                    txt.ForeColor = Constants.Colors.TextPrimary;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (control is ComboBox cmb)
                {
                    cmb.BackColor = System.Drawing.Color.White;
                    cmb.ForeColor = Constants.Colors.TextPrimary;
                }
                else if (control is Label lbl)
                {
                    lbl.ForeColor = Constants.Colors.TextDark;
                }
            }
            
            dgvDoAn.BackgroundColor = Constants.Colors.Background;
            dgvDoAn.GridColor = Constants.Colors.Border;
            dgvDoAn.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            dgvDoAn.DefaultCellStyle.ForeColor = Constants.Colors.TextPrimary;
            dgvDoAn.DefaultCellStyle.SelectionBackColor = Constants.Colors.Hover;
            dgvDoAn.DefaultCellStyle.SelectionForeColor = Constants.Colors.TextPrimary;
            dgvDoAn.ColumnHeadersDefaultCellStyle.BackColor = Constants.Colors.HeaderBackground;
            dgvDoAn.ColumnHeadersDefaultCellStyle.ForeColor = Constants.Colors.TextDark;
            dgvDoAn.EnableHeadersVisualStyles = false;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(txtMaDeTai.Text))
            {
                MessageBox.Show("Vui lòng nhập mã đề tài");
                return false;
            }

            if (Validation.IsNullOrEmpty(txtTenDeTai.Text) || !Validation.IsValidLength(txtTenDeTai.Text, 5, 200))
            {
                MessageBox.Show("Tên đề tài phải từ 5-200 ký tự");
                return false;
            }

            if (cmbMaGv.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn giảng viên hướng dẫn");
                return false;
            }

            if (!string.IsNullOrEmpty(txtDiem.Text))
            {
                if (!decimal.TryParse(txtDiem.Text, out decimal diem) || diem < 0 || diem > 10)
                {
                    MessageBox.Show("Điểm phải là số từ 0 đến 10");
                    return false;
                }
            }

            if (!Validation.IsValidDateRange(dtpNgayBatDau.Value, dtpNgayKetThuc.Value))
            {
                MessageBox.Show("Ngày bắt đầu phải trước ngày kết thúc");
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            txtMaDeTai.Clear();
            txtTenDeTai.Clear();
            txtDiem.Clear();
            if (txtMoTa != null) txtMoTa.Clear();
            if (cmbMaGv != null) cmbMaGv.SelectedIndex = -1;
            if (cmbMaSv != null) cmbMaSv.SelectedIndex = -1;
            if (cmbMaLoai != null) cmbMaLoai.SelectedIndex = -1;
            dtpNgayBatDau.Value = DateTime.Now;
            dtpNgayKetThuc.Value = DateTime.Now.AddMonths(6);
        }

        private void LoadComboBoxData()
        {
            try
            {
                using var context = new QuanLyDoAnContext();

                var sinhViens = context.SinhViens.Select(sv => new { sv.MaSv, sv.HoTen }).ToList();
                cmbMaSv.DataSource = sinhViens;
                cmbMaSv.DisplayMember = "HoTen";
                cmbMaSv.ValueMember = "MaSv";
                cmbMaSv.DropDownStyle = ComboBoxStyle.DropDown;
                cmbMaSv.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbMaSv.AutoCompleteSource = AutoCompleteSource.CustomSource;
                cmbMaSv.AutoCompleteCustomSource = new AutoCompleteStringCollection();
                cmbMaSv.AutoCompleteCustomSource.AddRange(sinhViens.Select(s => s.HoTen).ToArray());

                var giangViens = context.GiangViens.Select(gv => new { gv.MaGv, gv.HoTen }).ToList();
                cmbMaGv.DataSource = giangViens;
                cmbMaGv.DisplayMember = "HoTen";
                cmbMaGv.ValueMember = "MaGv";
                cmbMaGv.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu ComboBox: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var keyword = txtTimKiem.Text.ToLower();
                var danhSach = doAnController.LayDanhSachDoAn();

                if (!string.IsNullOrEmpty(keyword))
                {
                    danhSach = danhSach.Where(d =>
                        d.MaDeTai.ToLower().Contains(keyword) ||
                        d.TenDeTai.ToLower().Contains(keyword) ||
                        d.DanhSachSinhVien.ToLower().Contains(keyword) ||
                        d.TenGiangVien.ToLower().Contains(keyword)
                    ).ToList();
                }

                dgvDoAn.DataSource = danhSach;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}