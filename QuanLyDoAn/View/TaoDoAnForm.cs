using System;
using System.Linq;
using System.Windows.Forms;
using QuanLyDoAn.Controller;
using QuanLyDoAn.Model.EF;
using QuanLyDoAn.Model.Entities;
using QuanLyDoAn.Utils;
using Microsoft.EntityFrameworkCore;

namespace QuanLyDoAn.View
{
    public partial class TaoDoAnForm : Form
    {
        private readonly DoAnController doAnController;
        private readonly string? editingMaDeTai;
        private string? originalMaGv;
        private string? originalMaSv;

        public TaoDoAnForm(string? maDeTai = null)
        {
            InitializeComponent();
            editingMaDeTai = maDeTai;
            doAnController = new DoAnController();
            ThemeHelper.ApplyTheme(this);
            
            // Set DateTimePicker format
            dtpNgayBatDau.Format = DateTimePickerFormat.Custom;
            dtpNgayBatDau.CustomFormat = "dd/MM/yyyy";
            dtpNgayKetThuc.Format = DateTimePickerFormat.Custom;
            dtpNgayKetThuc.CustomFormat = "dd/MM/yyyy";
            
            LoadComboBoxData();
            ConfigureTrangThaiControl();

            if (!string.IsNullOrEmpty(editingMaDeTai))
            {
                LoadEditingData(editingMaDeTai);
            }
            else
            {
                originalMaGv = UserSession.CurrentUser?.MaGv;
            }
        }

        private void LoadEditingData(string maDeTai)
        {
            using var context = new QuanLyDoAnContext();
            var doAn = context.DoAns.FirstOrDefault(d => d.MaDeTai == maDeTai);

            if (doAn != null)
            {
                Text = "Sửa đề tài";
                txtMaDeTai.Text = doAn.MaDeTai;
                txtMaDeTai.ReadOnly = true;
                txtTenDeTai.Text = doAn.TenDeTai;
                txtMoTa.Text = doAn.MoTa ?? "";

                cboLoaiDoAn.SelectedValue = doAn.MaLoaiDoAn ?? "";
                cboKyHoc.SelectedValue = doAn.MaKy ?? "";
                cboChuyenNganh.SelectedValue = doAn.MaChuyenNganh ?? "";
                cboTrangThai.SelectedValue = doAn.MaTrangThai ?? "";

                dtpNgayBatDau.Value = doAn.NgayBatDau?.ToDateTime(TimeOnly.MinValue) ?? DateTime.Now;
                dtpNgayKetThuc.Value = doAn.NgayKetThuc?.ToDateTime(TimeOnly.MinValue) ?? DateTime.Now.AddMonths(6);
                originalMaGv = doAn.MaGv;
                originalMaSv = doAn.MaSv;
            }
        }

        private void LoadComboBoxData()
        {
            using var context = new QuanLyDoAnContext();

            cboLoaiDoAn.DataSource = context.LoaiDoAns.ToList();
            cboLoaiDoAn.DisplayMember = "TenLoaiDoAn";
            cboLoaiDoAn.ValueMember = "MaLoaiDoAn";

            cboKyHoc.DataSource = context.KyHocs.ToList();
            cboKyHoc.DisplayMember = "TenKy";
            cboKyHoc.ValueMember = "MaKy";

            cboChuyenNganh.DataSource = context.ChuyenNganhs.ToList();
            cboChuyenNganh.DisplayMember = "TenChuyenNganh";
            cboChuyenNganh.ValueMember = "MaChuyenNganh";

            cboTrangThai.DataSource = context.TrangThaiDoAns.ToList();
            cboTrangThai.DisplayMember = "TenTrangThai";
            cboTrangThai.ValueMember = "MaTrangThai";
        }

        private void ConfigureTrangThaiControl()
        {
            bool isCreate = string.IsNullOrEmpty(editingMaDeTai);
            if (isCreate)
            {
                if (cboTrangThai.Items.Count > 0)
                {
                    cboTrangThai.SelectedIndex = 0;
                }

                if (!AuthorizationHelper.IsAdmin())
                {
                    cboTrangThai.Enabled = false;
                }
            }
        }

        private bool ValidateInput(out string errorMessage)
        {
            errorMessage = string.Empty;
            bool isCreate = string.IsNullOrEmpty(editingMaDeTai);
            var currentMaGv = UserSession.CurrentUser?.MaGv;

            if (string.IsNullOrWhiteSpace(txtMaDeTai.Text) || string.IsNullOrWhiteSpace(txtTenDeTai.Text))
            {
                errorMessage = "Vui lòng nhập đầy đủ mã và tên đề tài.";
                return false;
            }

            if (dtpNgayKetThuc.Value <= dtpNgayBatDau.Value)
            {
                errorMessage = "Ngày kết thúc phải sau ngày bắt đầu.";
                return false;
            }

            if (cboLoaiDoAn.SelectedValue == null)
            {
                errorMessage = "Vui lòng chọn loại đồ án.";
                return false;
            }

            if (cboKyHoc.SelectedValue == null)
            {
                errorMessage = "Vui lòng chọn kỳ học.";
                return false;
            }

            if (cboChuyenNganh.SelectedValue == null)
            {
                errorMessage = "Vui lòng chọn chuyên ngành.";
                return false;
            }

            if (cboTrangThai.SelectedValue == null)
            {
                errorMessage = "Vui lòng chọn trạng thái.";
                return false;
            }

            if (isCreate && !AuthorizationHelper.IsGiangVien())
            {
                errorMessage = "Chỉ giảng viên mới có thể tạo đề tài mới.";
                return false;
            }

            using var context = new QuanLyDoAnContext();

            if (isCreate)
            {
                bool existed = context.DoAns.Any(d => d.MaDeTai == txtMaDeTai.Text.Trim());
                if (existed)
                {
                    errorMessage = "Mã đề tài đã tồn tại. Vui lòng chọn mã khác.";
                    return false;
                }

                if (string.IsNullOrWhiteSpace(currentMaGv))
                {
                    errorMessage = "Không xác định được giảng viên tạo đề tài.";
                    return false;
                }
            }
            else
            {
                var doAn = context.DoAns.AsNoTracking().FirstOrDefault(d => d.MaDeTai == editingMaDeTai);
                if (doAn == null)
                {
                    errorMessage = "Không tìm thấy đề tài cần chỉnh sửa.";
                    return false;
                }

                bool isOwner = AuthorizationHelper.IsAdmin() || doAn.MaGv == currentMaGv;
                if (!isOwner)
                {
                    errorMessage = "Bạn không có quyền chỉnh sửa đề tài này.";
                    return false;
                }
            }

            return true;
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(out string validationError))
            {
                MessageBox.Show(validationError, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool isCreate = string.IsNullOrEmpty(editingMaDeTai);
            var assignedMaGv = isCreate
                ? (UserSession.CurrentUser?.MaGv ?? "")
                : (originalMaGv ?? UserSession.CurrentUser?.MaGv ?? "");

            var doAn = new DoAn
            {
                MaDeTai = txtMaDeTai.Text.Trim(),
                TenDeTai = txtTenDeTai.Text.Trim(),
                MoTa = txtMoTa.Text?.Trim(),
                MaGv = assignedMaGv,
                MaLoaiDoAn = cboLoaiDoAn.SelectedValue?.ToString(),
                MaKy = cboKyHoc.SelectedValue?.ToString(),
                MaChuyenNganh = cboChuyenNganh.SelectedValue?.ToString(),
                MaTrangThai = cboTrangThai.SelectedValue?.ToString() ?? "",
                NgayBatDau = DateOnly.FromDateTime(dtpNgayBatDau.Value),
                NgayKetThuc = DateOnly.FromDateTime(dtpNgayKetThuc.Value),
                MaSv = isCreate ? null : originalMaSv
            };

            if (!isCreate)
            {
                if (doAnController.CapNhatDoAn(doAn, out string errUpdate))
                {
                    MessageBox.Show("Cập nhật đề tài thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show(errUpdate, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (doAnController.TaoDoAn(doAn, out string error))
                {
                    MessageBox.Show("Tạo đồ án thành công! Sinh viên có thể đăng ký.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show(error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
