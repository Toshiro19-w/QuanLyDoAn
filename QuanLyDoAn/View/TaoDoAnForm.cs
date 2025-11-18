using System;
using System.Windows.Forms;
using QuanLyDoAn.Controller;
using QuanLyDoAn.Model.Entities;
using QuanLyDoAn.Model.EF;
using QuanLyDoAn.Utils;
using System.Linq;

namespace QuanLyDoAn.View
{
    public partial class TaoDoAnForm : Form
    {
        private DangKyDoAnController controller;
        private DoAnController doAnController;
        private string? editingMaDeTai = null; // null = tạo mới, có giá trị = sửa

        public TaoDoAnForm()
        {
            InitializeComponent();
            doAnController = new DoAnController();
            ThemeHelper.ApplyTheme(this);
            LoadComboBoxData();
        }

        // Constructor overload để mở form với dữ liệu sửa
        public TaoDoAnForm(string maDeTai) : this()
        {
            editingMaDeTai = maDeTai;
            LoadEditingData(maDeTai);
        }

        private void LoadEditingData(string maDeTai)
        {
            using var context = new QuanLyDoAnContext();
            var doAn = context.DoAns.Find(maDeTai);
            
            if (doAn != null)
            {
                this.Text = "Sửa đề tài";
                txtMaDeTai.Text = doAn.MaDeTai;
                txtMaDeTai.ReadOnly = true; // Không cho phép sửa mã đề tài
                txtTenDeTai.Text = doAn.TenDeTai;
                txtMoTa.Text = doAn.MoTa ?? "";
                
                cboLoaiDoAn.SelectedValue = doAn.MaLoaiDoAn ?? "";
                cboKyHoc.SelectedValue = doAn.MaKy ?? "";
                cboChuyenNganh.SelectedValue = doAn.MaChuyenNganh ?? "";
                cboTrangThai.SelectedValue = doAn.MaTrangThai ?? "";
                
                dtpNgayBatDau.Value = doAn.NgayBatDau?.ToDateTime(TimeOnly.MinValue) ?? DateTime.Now;
                dtpNgayKetThuc.Value = doAn.NgayKetThuc?.ToDateTime(TimeOnly.MinValue) ?? DateTime.Now.AddMonths(6);
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

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaDeTai.Text) || string.IsNullOrWhiteSpace(txtTenDeTai.Text))
            {
                MessageBox.Show("Vui lòng nhập mã và tên đề tài!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var doAn = new DoAn
            {
                MaDeTai = txtMaDeTai.Text.Trim(),
                TenDeTai = txtTenDeTai.Text.Trim(),
                MoTa = txtMoTa.Text?.Trim(),
                MaGv = UserSession.CurrentUser?.MaGv ?? "",
                MaLoaiDoAn = cboLoaiDoAn.SelectedValue?.ToString(),
                MaKy = cboKyHoc.SelectedValue?.ToString(),
                MaChuyenNganh = cboChuyenNganh.SelectedValue?.ToString(),
                MaTrangThai = cboTrangThai.SelectedValue?.ToString() ?? "",
                NgayBatDau = DateOnly.FromDateTime(dtpNgayBatDau.Value),
                NgayKetThuc = DateOnly.FromDateTime(dtpNgayKetThuc.Value),
                MaSv = null // Không set MaSv khi tạo đề tài mới - sinh viên sẽ đăng ký sau
            };

            // Nếu editingMaDeTai có giá trị = sửa, ngược lại = tạo mới
            if (!string.IsNullOrEmpty(editingMaDeTai))
            {
                // Sửa đề tài
                if (doAnController.CapNhatDoAn(doAn, out string errUpdate))
                {
                    MessageBox.Show("Cập nhật đề tài thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(errUpdate, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Tạo mới đề tài - Chỉ GiangVien mới có thể tạo đề tài mới (không phải Admin)
                if (!AuthorizationHelper.IsGiangVien())
                {
                    MessageBox.Show("Chỉ giảng viên mới có thể tạo đề tài mới!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (doAnController.TaoDoAn(doAn, out string error))
                {
                    MessageBox.Show("Tạo đồ án thành công! Sinh viên có thể đăng ký.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
