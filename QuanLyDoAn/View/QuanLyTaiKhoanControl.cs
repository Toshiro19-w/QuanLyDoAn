using System;
using System.Windows.Forms;
using QuanLyDoAn.Controller;
using QuanLyDoAn.Model.Entities;
using QuanLyDoAn.Utils;

namespace QuanLyDoAn.View
{
    public partial class QuanLyTaiKhoanControl : UserControl
    {
        private QuanLyTaiKhoanController taiKhoanController;

        public QuanLyTaiKhoanControl()
        {
            InitializeComponent();
            taiKhoanController = new QuanLyTaiKhoanController();
            LoadData();
            LoadComboBoxes();
            ThemeHelper.ApplyTheme(this);
            
            // Ẩn textbox mật khẩu
            txtMatKhau.UseSystemPasswordChar = true;
            
            // Thêm event handler cho DataGridView
            dgvTaiKhoan.CellClick += dgvTaiKhoan_CellClick;
        }

        private void LoadData()
        {
            var danhSachTaiKhoan = taiKhoanController.LayDanhSachTaiKhoan();
            
            // Tạo danh sách với tên hiển thị
            var displayList = danhSachTaiKhoan.Select(tk => new
            {
                tk.TenDangNhap,
                tk.MatKhau,
                tk.VaiTro,
                TenGiangVien = tk.MaGvNavigation?.HoTen ?? "",
                TenSinhVien = tk.MaSvNavigation?.HoTen ?? "",
                tk.MaSv,
                tk.MaGv
            }).ToList();
            
            dgvTaiKhoan.DataSource = displayList;
            
            // Ẩn các cột không cần thiết
            if (dgvTaiKhoan.Columns["MaSv"] != null)
                dgvTaiKhoan.Columns["MaSv"].Visible = false;
            if (dgvTaiKhoan.Columns["MaGv"] != null)
                dgvTaiKhoan.Columns["MaGv"].Visible = false;
            
            // Đặt tên cột tiếng Việt
            if (dgvTaiKhoan.Columns["TenDangNhap"] != null)
                dgvTaiKhoan.Columns["TenDangNhap"].HeaderText = "Tên đăng nhập";
            if (dgvTaiKhoan.Columns["MatKhau"] != null)
                dgvTaiKhoan.Columns["MatKhau"].HeaderText = "Mật khẩu";
            if (dgvTaiKhoan.Columns["VaiTro"] != null)
                dgvTaiKhoan.Columns["VaiTro"].HeaderText = "Vai trò";
            if (dgvTaiKhoan.Columns["TenGiangVien"] != null)
                dgvTaiKhoan.Columns["TenGiangVien"].HeaderText = "Giảng viên";
            if (dgvTaiKhoan.Columns["TenSinhVien"] != null)
                dgvTaiKhoan.Columns["TenSinhVien"].HeaderText = "Sinh viên";
        }

        private void LoadComboBoxes()
        {
            cmbVaiTro.Items.AddRange(new[] { Constants.UserRoles.Admin, Constants.UserRoles.GiangVien, Constants.UserRoles.SinhVien });
            
            var sinhViens = taiKhoanController.LayDanhSachSinhVien();
            cmbSinhVien.DataSource = sinhViens;
            cmbSinhVien.DisplayMember = "HoTen";
            cmbSinhVien.ValueMember = "MaSv";

            var giangViens = taiKhoanController.LayDanhSachGiangVien();
            cmbGiangVien.DataSource = giangViens;
            cmbGiangVien.DisplayMember = "HoTen";
            cmbGiangVien.ValueMember = "MaGv";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            var taiKhoan = new TaiKhoan
            {
                TenDangNhap = txtTenDangNhap.Text,
                MatKhau = HashHelper.HashPassword(txtMatKhau.Text),
                VaiTro = cmbVaiTro.Text,
                MaSv = cmbVaiTro.Text == Constants.UserRoles.SinhVien ? cmbSinhVien.SelectedValue?.ToString() : null,
                MaGv = cmbVaiTro.Text == Constants.UserRoles.GiangVien ? cmbGiangVien.SelectedValue?.ToString() : null
            };

            if (taiKhoanController.TaoTaiKhoan(taiKhoan))
            {
                MessageBox.Show(Constants.Messages.ThemThanhCong);
                LoadData();
                ClearForm();
            }
            else
            {
                MessageBox.Show(Constants.Messages.DuLieuKhongHopLe);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!ValidateUpdateSelection()) return;
            if (!ValidateInput()) return;

            try
            {
                var selectedRow = dgvTaiKhoan.CurrentRow.DataBoundItem;
                var tenDangNhap = selectedRow.GetType().GetProperty("TenDangNhap")?.GetValue(selectedRow)?.ToString();
                
                var taiKhoan = new TaiKhoan
                {
                    TenDangNhap = tenDangNhap,
                    MatKhau = HashHelper.HashPassword(txtMatKhau.Text),
                    VaiTro = cmbVaiTro.Text,
                    MaSv = cmbVaiTro.Text == Constants.UserRoles.SinhVien ? cmbSinhVien.SelectedValue?.ToString() : null,
                    MaGv = cmbVaiTro.Text == Constants.UserRoles.GiangVien ? cmbGiangVien.SelectedValue?.ToString() : null
                };

                if (taiKhoanController.CapNhatTaiKhoan(taiKhoan))
                {
                    MessageBox.Show(Constants.Messages.CapNhatThanhCong);
                    LoadData();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show(Constants.Messages.DuLieuKhongHopLe);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật tài khoản: {ex.Message}");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!ValidateDeleteSelection()) return;
            if (!ConfirmDeletion()) return;

            try
            {
                var selectedRow = dgvTaiKhoan.CurrentRow.DataBoundItem;
                var tenDangNhap = selectedRow.GetType().GetProperty("TenDangNhap")?.GetValue(selectedRow)?.ToString();
                
                if (taiKhoanController.XoaTaiKhoan(tenDangNhap))
                {
                    MessageBox.Show(Constants.Messages.XoaThanhCong);
                    LoadData();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Không thể xóa tài khoản này!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa tài khoản: {ex.Message}");
            }
        }

        private void ClearForm()
        {
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
            cmbVaiTro.SelectedIndex = -1;
            cmbSinhVien.SelectedIndex = -1;
            cmbGiangVien.SelectedIndex = -1;
        }

        private void cmbVaiTro_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSinhVien.Visible = cmbVaiTro.Text == Constants.UserRoles.SinhVien;
            cmbGiangVien.Visible = cmbVaiTro.Text == Constants.UserRoles.GiangVien;
        }

        private bool ValidateInput()
        {
            if (Validation.IsNullOrEmpty(txtTenDangNhap.Text) || !Validation.IsValidUsername(txtTenDangNhap.Text))
            {
                MessageBox.Show("Tên đăng nhập không hợp lệ (3-20 ký tự, chỉ chữ cái, số và _)");
                return false;
            }

            if (!Validation.IsValidPassword(txtMatKhau.Text))
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự");
                return false;
            }

            if (Validation.IsNullOrEmpty(cmbVaiTro.Text))
            {
                MessageBox.Show(Constants.Messages.VuiLongNhapDayDu);
                return false;
            }

            return true;
        }

        private bool ValidateUpdateSelection()
        {
            if (dgvTaiKhoan.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần sửa!");
                return false;
            }

            var selectedRow = dgvTaiKhoan.CurrentRow.DataBoundItem;
            var tenDangNhap = selectedRow?.GetType().GetProperty("TenDangNhap")?.GetValue(selectedRow)?.ToString();
            
            if (string.IsNullOrEmpty(tenDangNhap))
            {
                MessageBox.Show("Không thể xác định tài khoản được chọn!");
                return false;
            }

            return true;
        }

        private bool ValidateDeleteSelection()
        {
            if (dgvTaiKhoan.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần xóa!");
                return false;
            }

            var selectedRow = dgvTaiKhoan.CurrentRow.DataBoundItem;
            var tenDangNhap = selectedRow?.GetType().GetProperty("TenDangNhap")?.GetValue(selectedRow)?.ToString();
            
            if (string.IsNullOrEmpty(tenDangNhap))
            {
                MessageBox.Show("Không thể xác định tài khoản được chọn!");
                return false;
            }

            if (tenDangNhap == UserSession.CurrentUser?.TenDangNhap)
            {
                MessageBox.Show("Không thể xóa tài khoản đang sử dụng!");
                return false;
            }

            return true;
        }

        private bool ConfirmDeletion()
        {
            var selectedRow = dgvTaiKhoan.CurrentRow.DataBoundItem;
            var tenDangNhap = selectedRow?.GetType().GetProperty("TenDangNhap")?.GetValue(selectedRow)?.ToString();
            
            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa tài khoản '{tenDangNhap}'?\n\nHành động này không thể hoàn tác!",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            return result == DialogResult.Yes;
        }

        private void dgvTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    var selectedRow = dgvTaiKhoan.Rows[e.RowIndex].DataBoundItem;
                    
                    txtTenDangNhap.Text = selectedRow.GetType().GetProperty("TenDangNhap")?.GetValue(selectedRow)?.ToString() ?? "";
                    txtMatKhau.Text = ""; // Không hiển thị mật khẩu cũ
                    
                    var vaiTro = selectedRow.GetType().GetProperty("VaiTro")?.GetValue(selectedRow)?.ToString();
                    cmbVaiTro.Text = vaiTro ?? "";
                    
                    // Load combo boxes tương ứng
                    if (vaiTro == Constants.UserRoles.SinhVien)
                    {
                        var maSv = selectedRow.GetType().GetProperty("MaSv")?.GetValue(selectedRow)?.ToString();
                        cmbSinhVien.SelectedValue = maSv;
                    }
                    else if (vaiTro == Constants.UserRoles.GiangVien)
                    {
                        var maGv = selectedRow.GetType().GetProperty("MaGv")?.GetValue(selectedRow)?.ToString();
                        cmbGiangVien.SelectedValue = maGv;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi load dữ liệu: {ex.Message}");
                }
            }
        }


    }
}