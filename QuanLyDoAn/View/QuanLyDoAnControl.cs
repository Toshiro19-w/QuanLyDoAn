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
        private bool suppressComboChange = false;

        public QuanLyDoAnControl()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("=== BẮT ĐẦU CONSTRUCTOR ===");
                
                InitializeComponents();
                InitializeController();
                InitializeComboBoxData();
                SetupEventHandlers();
                ConfigureDateTimePickers();
                ConfigureAuthorization();
                ApplyTheme();
                
                System.Diagnostics.Debug.WriteLine("=== KẾT THÚC CONSTRUCTOR THÀNH CÔNG ===");
            }
            catch (Exception ex)
            {
                HandleConstructorError(ex);
                throw;
            }
        }

        private void InitializeComponents()
        {
            System.Diagnostics.Debug.WriteLine("Đang gọi InitializeComponent()...");
            InitializeComponent();
            System.Diagnostics.Debug.WriteLine("✓ InitializeComponent() thành công");
        }

        private void InitializeController()
        {
            System.Diagnostics.Debug.WriteLine("Đang khởi tạo DoAnController...");
            doAnController = new DoAnController();
            System.Diagnostics.Debug.WriteLine("✓ DoAnController khởi tạo thành công");
        }

        private void InitializeComboBoxData()
        {
            System.Diagnostics.Debug.WriteLine("Đang gọi LoadComboBoxData()...");
            LoadComboBoxData();
            System.Diagnostics.Debug.WriteLine("✓ LoadComboBoxData() thành công");
        }

        private void SetupEventHandlers()
        {
            System.Diagnostics.Debug.WriteLine("Đang thêm event handlers...");
            
            SetupDataGridViewEvents();
            SetupComboBoxEvents();
            SetupLoadEvent();
            
            System.Diagnostics.Debug.WriteLine("✓ Event handlers đã được thêm");
        }

        private void SetupDataGridViewEvents()
        {
            if (dgvDoAn != null)
            {
                dgvDoAn.DataBindingComplete += DgvDoAn_DataBindingComplete;
                System.Diagnostics.Debug.WriteLine("✓ DataGridView event handler đã được thêm");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("CẢNH BÁO: dgvDoAn is null");
            }
        }

        private void SetupComboBoxEvents()
        {
            if (cmbMaLoai != null)
            {
                cmbMaLoai.SelectionChangeCommitted += CmbMaLoai_SelectionChangeCommitted;
                System.Diagnostics.Debug.WriteLine("✓ CmbMaLoai event handler đã được thêm");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("CẢNH BÁO: cmbMaLoai is null");
            }
        }

        private void SetupLoadEvent()
        {
            this.Load += (s, e) => LoadData();
        }

        private void ConfigureDateTimePickers()
        {
            System.Diagnostics.Debug.WriteLine("Đang cấu hình DateTimePicker...");
            
            if (dtpNgayBatDau != null && dtpNgayKetThuc != null)
            {
                dtpNgayBatDau.Format = DateTimePickerFormat.Custom;
                dtpNgayBatDau.CustomFormat = "dd/MM/yyyy";
                dtpNgayKetThuc.Format = DateTimePickerFormat.Custom;
                dtpNgayKetThuc.CustomFormat = "dd/MM/yyyy";
                System.Diagnostics.Debug.WriteLine("✓ DateTimePicker đã được cấu hình");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("CẢNH BÁO: DateTimePicker controls are null");
            }
        }

        private void ConfigureAuthorization()
        {
            System.Diagnostics.Debug.WriteLine("Đang cấu hình quyền...");
            AuthorizationHelper.ConfigureControlsByRole(this.Controls);
            System.Diagnostics.Debug.WriteLine("✓ Cấu hình quyền thành công");
        }

        private void HandleConstructorError(Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"=== LỖI TRONG CONSTRUCTOR ===");
            System.Diagnostics.Debug.WriteLine($"Message: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");
            if (ex.InnerException != null)
            {
                System.Diagnostics.Debug.WriteLine($"InnerException: {ex.InnerException.Message}");
            }
            MessageBox.Show($"Lỗi trong constructor: {ex.Message}\n\nStackTrace: {ex.StackTrace}\n\nInnerException: {ex.InnerException?.Message}", "Lỗi Debug", MessageBoxButtons.OK, MessageBoxIcon.Error);
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



        private void LoadData()
        {
            try
            {
                if (doAnController == null || dgvDoAn == null) return;

                var danhSachDoAn = doAnController.LayDanhSachDoAn();
                dgvDoAn.DataSource = danhSachDoAn;
                ConfigureColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void DgvDoAn_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            ConfigureColumns();
        }
        
        private void ConfigureColumns()
        {
            try
            {
                // Kiểm tra điều kiện cơ bản
                if (dgvDoAn?.Columns == null || dgvDoAn.Columns.Count == 0)
                    return;

                // Sử dụng Invoke nếu cần thiết
                if (dgvDoAn.InvokeRequired)
                {
                    dgvDoAn.Invoke(new Action(ConfigureColumns));
                    return;
                }

                // Ẩn cột không cần thiết
                if (dgvDoAn.Columns["MaLoaiDoAn"] != null)
                    dgvDoAn.Columns["MaLoaiDoAn"].Visible = false;
                if (dgvDoAn.Columns["CoChamDiemChiTiet"] != null)
                    dgvDoAn.Columns["CoChamDiemChiTiet"].Visible = false;
                
                // Cấu hình từng column một cách an toàn
                ConfigureColumn("MaDeTai", "Mã đề tài", 0, 100);
                ConfigureColumn("TenDeTai", "Tên đề tài", 1, 300);
                ConfigureColumn("SinhVien", "Sinh viên", 2, 130);
                ConfigureColumn("TenGiangVien", "Giảng viên", 3, 150);
                ConfigureColumn("NgayBatDau", "Ngày bắt đầu", 4, 100, "dd/MM/yyyy");
                ConfigureColumn("NgayKetThuc", "Ngày kết thúc", 5, 100, "dd/MM/yyyy");
                ConfigureColumn("TrangThai", "Trạng thái", 6, 100);
                ConfigureColumn("DiemText", "Điểm", 7, 80, null, DataGridViewContentAlignment.MiddleCenter);
                ConfigureColumn("LoaiDoAn", "Loại đồ án", 8, 120);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi ConfigureColumns: {ex.Message}");
            }
        }

        private void ConfigureColumn(string columnName, string headerText, int displayIndex, int width, 
            string format = null, DataGridViewContentAlignment? alignment = null)
        {
            try
            {
                if (dgvDoAn?.Columns?[columnName] == null) return;

                var column = dgvDoAn.Columns[columnName];
                column.HeaderText = headerText;
                column.DisplayIndex = displayIndex;
                column.Width = width;

                if (!string.IsNullOrEmpty(format))
                    column.DefaultCellStyle.Format = format;

                if (alignment.HasValue)
                    column.DefaultCellStyle.Alignment = alignment.Value;

                if (columnName == "TenDeTai")
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi cấu hình column {columnName}: {ex.Message}");
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



        private void dgvDoAn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvDoAn.Rows[e.RowIndex].DataBoundItem is DoAnViewModel doAn)
            {
                try
                {
                    LoadBasicInfo(doAn);
                    LoadDescription(doAn);
                    LoadGiangVienSelection(doAn);
                    LoadSinhVienSelection(doAn);
                    LoadLoaiDoAnSelection(doAn);
                    LoadDateValues(doAn);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi load dữ liệu: {ex.Message}");
                }
            }
        }

        private void LoadBasicInfo(DoAnViewModel doAn)
        {
            txtMaDeTai.Text = doAn.MaDeTai;
            txtTenDeTai.Text = doAn.TenDeTai;
            txtDiem.Text = (doAn.DiemText == "Chưa có điểm" || string.IsNullOrEmpty(doAn.DiemText)) ? "" : doAn.DiemText;
        }

        private void LoadDescription(DoAnViewModel doAn)
        {
            using var context = new QuanLyDoAnContext();
            var doAnEntity = context.DoAns.Find(doAn.MaDeTai);
            if (txtMoTa != null && doAnEntity != null)
                txtMoTa.Text = doAnEntity.MoTa ?? "";
        }

        private void LoadGiangVienSelection(DoAnViewModel doAn)
        {
            for (int i = 0; i < cmbMaGv.Items.Count; i++)
            {
                var item = cmbMaGv.Items[i];
                if (item.GetType().GetProperty("HoTen")?.GetValue(item)?.ToString() == doAn.TenGiangVien)
                {
                    cmbMaGv.SelectedIndex = i;
                    break;
                }
            }
        }

        private void LoadSinhVienSelection(DoAnViewModel doAn)
        {
            if (string.IsNullOrEmpty(doAn.SinhVien) || doAn.SinhVien == "Chưa phân công")
            {
                cmbMaSv.SelectedIndex = 0;
            }
            else
            {
                for (int i = 0; i < cmbMaSv.Items.Count; i++)
                {
                    var item = cmbMaSv.Items[i];
                    if (item.GetType().GetProperty("HoTen")?.GetValue(item)?.ToString() == doAn.SinhVien)
                    {
                        cmbMaSv.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void LoadLoaiDoAnSelection(DoAnViewModel doAn)
        {
            if (cmbMaLoai != null)
            {
                suppressComboChange = true;
                for (int i = 0; i < cmbMaLoai.Items.Count; i++)
                {
                    var item = cmbMaLoai.Items[i];
                    if (item.GetType().GetProperty("TenLoaiDoAn")?.GetValue(item)?.ToString() == doAn.LoaiDoAn)
                    {
                        cmbMaLoai.SelectedIndex = i;
                        break;
                    }
                }
                suppressComboChange = false;
            }
        }

        private void LoadDateValues(DoAnViewModel doAn)
        {
            if (doAn.NgayBatDau.HasValue)
                dtpNgayBatDau.Value = doAn.NgayBatDau.Value.ToDateTime(TimeOnly.MinValue);
            if (doAn.NgayKetThuc.HasValue)
                dtpNgayKetThuc.Value = doAn.NgayKetThuc.Value.ToDateTime(TimeOnly.MinValue);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            
            var doAn = CreateDoAnFromForm();

            if (doAnController.TaoDoAn(doAn, out string error))
            {
                ShowSuccessMessage("Thêm thành công");
                RefreshDataAndClearForm();
            }
            else
            {
                ShowErrorMessage(error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!ValidateUpdatePermissions()) return;
            if (!ValidateInputForUpdate()) return;
            if (!ValidateGiangVienNotDuplicate()) return;

            var doAn = CreateDoAnFromForm();

            if (doAnController.CapNhatDoAn(doAn, out string error))
            {
                ShowSuccessMessage("✅ Cập nhật đồ án thành công!");
                RefreshDataAndClearForm();
            }
            else
            {
                ShowErrorMessage(error);
            }
         }

         private void btnXoa_Click(object sender, EventArgs e)
         {
             if (!ValidateDeletePermissions()) return;
             if (!ValidateDoAnForDeletion()) return;
             if (!ConfirmDeletion()) return;

             if (doAnController.XoaDoAn(txtMaDeTai.Text))
             {
                 ShowSuccessMessage("✅ Xóa đồ án thành công!");
                 RefreshDataAndClearForm();
             }
             else
             {
                 ShowErrorMessage("Lỗi khi xóa đồ án!");
             }
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

        private DoAn CreateDoAnFromForm()
        {
            return new DoAn
            {
                MaDeTai = txtMaDeTai.Text,
                TenDeTai = txtTenDeTai.Text,
                MoTa = txtMoTa?.Text ?? "",
                MaGv = cmbMaGv.SelectedValue?.ToString() ?? "",
                MaSv = cmbMaSv.SelectedValue?.ToString() == "" ? null : cmbMaSv.SelectedValue?.ToString(),
                MaTrangThai = GetDefaultMaTrangThai(),
                NgayBatDau = DateOnly.FromDateTime(dtpNgayBatDau.Value),
                NgayKetThuc = DateOnly.FromDateTime(dtpNgayKetThuc.Value),
                Diem = string.IsNullOrEmpty(txtDiem.Text) ? null : decimal.Parse(txtDiem.Text),
                MaLoaiDoAn = cmbMaLoai?.SelectedValue?.ToString()
            };
        }

        private bool ValidateUpdatePermissions()
        {
            if (string.IsNullOrEmpty(txtMaDeTai.Text))
            {
                ShowWarningMessage("Vui lòng chọn đồ án cần sửa");
                return false;
            }

            if (!AuthorizationHelper.IsAdmin())
            {
                ShowInfoMessage("Chỉ Admin mới có thể sửa thông tin đề tài!");
                return false;
            }

            return true;
        }

        private bool ValidateDeletePermissions()
        {
            if (string.IsNullOrEmpty(txtMaDeTai.Text))
            {
                ShowWarningMessage("Vui lòng chọn đồ án cần xóa");
                return false;
            }

            if (!AuthorizationHelper.IsAdmin())
            {
                ShowInfoMessage("Chỉ Admin mới có thể xóa đồ án!");
                return false;
            }

            return true;
        }

        private bool ValidateDoAnForDeletion()
        {
            using var context = new QuanLyDoAnContext();
            var doAn = context.DoAns.Find(txtMaDeTai.Text);
            
            if (doAn == null)
            {
                ShowErrorMessage("Đồ án không tồn tại!");
                return false;
            }

            if (!string.IsNullOrEmpty(doAn.MaSv))
            {
                ShowWarningMessage("Không thể xóa đồ án đã được phân công cho sinh viên!");
                return false;
            }

            return true;
        }

        private bool ConfirmDeletion()
        {
            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa đồ án '{txtMaDeTai.Text}'?\n\nTên: {txtTenDeTai.Text}",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            return result == DialogResult.Yes;
        }

        private void RefreshDataAndClearForm()
        {
            LoadData();
            ClearForm();
        }

        private void ShowSuccessMessage(string message)
        {
            MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowWarningMessage(string message)
        {
            MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ShowInfoMessage(string message)
        {
            MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        private bool ValidateGiangVienNotDuplicate()
        {
            var maGvhd = cmbMaGv.SelectedValue?.ToString();
            var maDeTai = txtMaDeTai.Text;
            
            if (string.IsNullOrEmpty(maGvhd) || string.IsNullOrEmpty(maDeTai)) return true;
            
            using var context = new QuanLyDoAnContext();
            var daPhanCong = context.DanhGia.Any(d => 
                d.MaDeTai == maDeTai && 
                d.MaGv == maGvhd);
            
            if (daPhanCong)
            {
                ShowWarningMessage("Giảng viên này đã được phân công vai trò khác (PB/HĐ) cho đồ án này. Vui lòng chọn giảng viên khác!");
                return false;
            }
            
            return true;
        }

        private void ApplyTheme()
        {
            System.Diagnostics.Debug.WriteLine("Đang áp dụng theme...");
            
            ApplyControlTheme();
            ApplyDataGridViewTheme();
            
            System.Diagnostics.Debug.WriteLine("✓ Áp dụng theme thành công");
        }

        private void ApplyControlTheme()
        {
            this.BackColor = Constants.Colors.Background;
            
            foreach (Control control in this.Controls)
            {
                ApplyControlSpecificTheme(control);
            }
        }

        private void ApplyControlSpecificTheme(Control control)
        {
            switch (control)
            {
                case Button btn:
                    ApplyButtonTheme(btn);
                    break;
                case TextBox txt:
                    ApplyTextBoxTheme(txt);
                    break;
                case ComboBox cmb:
                    ApplyComboBoxTheme(cmb);
                    break;
                case Label lbl:
                    ApplyLabelTheme(lbl);
                    break;
            }
        }

        private void ApplyButtonTheme(Button btn)
        {
            btn.BackColor = Constants.Colors.Primary;
            btn.ForeColor = System.Drawing.Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = Cursors.Hand;
        }

        private void ApplyTextBoxTheme(TextBox txt)
        {
            txt.BackColor = System.Drawing.Color.White;
            txt.ForeColor = Constants.Colors.TextPrimary;
            txt.BorderStyle = BorderStyle.FixedSingle;
        }

        private void ApplyComboBoxTheme(ComboBox cmb)
        {
            cmb.BackColor = System.Drawing.Color.White;
            cmb.ForeColor = Constants.Colors.TextPrimary;
        }

        private void ApplyLabelTheme(Label lbl)
        {
            lbl.ForeColor = Constants.Colors.TextDark;
        }

        private void ApplyDataGridViewTheme()
        {
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
        
        private bool ValidateInputForUpdate()
        {
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
                // Thêm tùy chọn "Chưa phân công"
                sinhViens.Insert(0, new { MaSv = "", HoTen = "Chưa phân công" });
                
                cmbMaSv.DataSource = sinhViens;
                cmbMaSv.DisplayMember = "HoTen";
                cmbMaSv.ValueMember = "MaSv";
                cmbMaSv.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbMaSv.SelectedIndex = 0; // Mặc định chọn "Chưa phân công"

                var giangViens = context.GiangViens.Select(gv => new { gv.MaGv, gv.HoTen }).ToList();
                cmbMaGv.DataSource = giangViens;
                cmbMaGv.DisplayMember = "HoTen";
                cmbMaGv.ValueMember = "MaGv";
                cmbMaGv.DropDownStyle = ComboBoxStyle.DropDownList;
                
                var loaiDoAns = context.LoaiDoAns.Select(l => new { l.MaLoaiDoAn, l.TenLoaiDoAn }).ToList();
                cmbMaLoai.DataSource = loaiDoAns;
                cmbMaLoai.DisplayMember = "TenLoaiDoAn";
                cmbMaLoai.ValueMember = "MaLoaiDoAn";
                cmbMaLoai.DropDownStyle = ComboBoxStyle.DropDownList;
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
                System.Diagnostics.Debug.WriteLine("Bắt đầu txtTimKiem_TextChanged");
                
                var keyword = GetSearchKeyword();
                var filteredData = FilterDoAnData(keyword);
                UpdateDataGridView(filteredData);
                
                System.Diagnostics.Debug.WriteLine("Hoàn thành txtTimKiem_TextChanged");
            }
            catch (Exception ex)
            {
                HandleSearchError(ex);
            }
        }

        private string GetSearchKeyword()
        {
            var keyword = txtTimKiem.Text?.ToLower() ?? "";
            System.Diagnostics.Debug.WriteLine($"Keyword: {keyword}");
            return keyword;
        }

        private List<DoAnViewModel> FilterDoAnData(string keyword)
        {
            var danhSach = doAnController.LayDanhSachDoAn();
            System.Diagnostics.Debug.WriteLine($"Số lượng ban đầu: {danhSach?.Count ?? 0}");

            if (!string.IsNullOrEmpty(keyword) && danhSach != null)
            {
                danhSach = danhSach.Where(d =>
                    (d.MaDeTai?.ToLower().Contains(keyword) ?? false) ||
                    (d.TenDeTai?.ToLower().Contains(keyword) ?? false) ||
                    (d.SinhVien?.ToLower().Contains(keyword) ?? false) ||
                    (d.TenGiangVien?.ToLower().Contains(keyword) ?? false) ||
                    (d.LoaiDoAn?.ToLower().Contains(keyword) ?? false)
                ).ToList();
                System.Diagnostics.Debug.WriteLine($"Số lượng sau filter: {danhSach.Count}");
            }

            return danhSach;
        }

        private void UpdateDataGridView(List<DoAnViewModel> data)
        {
            System.Diagnostics.Debug.WriteLine("Gán DataSource");
            dgvDoAn.DataSource = data;
            
            System.Diagnostics.Debug.WriteLine("Gọi ConfigureColumns");
            ConfigureColumns();
        }

        private void HandleSearchError(Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Lỗi trong txtTimKiem_TextChanged: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");
            MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}\n\nStackTrace: {ex.StackTrace}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}