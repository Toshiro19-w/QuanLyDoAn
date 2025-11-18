using QuanLyDoAn.View;
using QuanLyDoAn.Model.Entities;
using QuanLyDoAn.Utils;

namespace QuanLyDoAn
{
    public partial class MainForm : Form
    {
        private Panel panelSidebar;
        private Panel panelContent;
        private Panel panelHeader;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public MainForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            SetupLayout();
            SetupMenu();
        }
        
        private void SetupLayout()
        {
            // Header panel
            panelHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Constants.Colors.Primary
            };
            
            var lblTitle = new Label
            {
                Text = $"Hệ thống Quản lý Đồ án - {UserSession.CurrentUser?.VaiTro}: {GetUserDisplayName()}",
                ForeColor = System.Drawing.Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(20, 15),
                AutoSize = true
            };
            
            var btnDangXuat = new Button
            {
                Text = "Đăng xuất",
                Size = new Size(100, 35),
                Location = new Point(this.Width - 120, 12),
                BackColor = Constants.Colors.PrimaryHover,
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btnDangXuat.Click += BtnDangXuat_Click;
            
            panelHeader.Controls.AddRange(new Control[] { lblTitle, btnDangXuat });
            
            // Sidebar panel
            panelSidebar = new Panel
            {
                Dock = DockStyle.Left,
                Width = 250,
                BackColor = Constants.Colors.Border
            };
            
            // Content panel
            panelContent = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Constants.Colors.Background
            };
            
            this.Controls.AddRange(new Control[] { panelContent, panelSidebar, panelHeader });
        }

        private void SetupMenu()
        {
            int yPosition = 20;
            
            if (AuthorizationHelper.IsAdmin())
            {
                AddMenuButton("Quản lý Đồ án", yPosition, BtnQuanLyDoAn_Click);
                yPosition += 60;
                AddMenuButton("Quản lý Tài khoản", yPosition, BtnQuanLyTaiKhoan_Click);
                yPosition += 60;
                AddMenuButton("Quản lý Giảng viên", yPosition, BtnQuanLyGiangVien_Click);
                yPosition += 60;
                AddMenuButton("Quản lý Sinh viên", yPosition, BtnQuanLySinhVien_Click);
                yPosition += 60;
                AddMenuButton("Quản lý Danh mục", yPosition, BtnQuanLyDanhMuc_Click);
                yPosition += 60;
                AddMenuButton("Thông tin cá nhân", yPosition, BtnThongTinCaNhan_Click);
                yPosition += 60;
            }
            else if (AuthorizationHelper.IsGiangVien())
            {
                AddMenuButton("Đồ án của tôi", yPosition, BtnDoAnGiangVien_Click);
                yPosition += 60;
                AddMenuButton("Tạo đề tài mới", yPosition, BtnTaoDoAn_Click);
                yPosition += 60;
                AddMenuButton("Duyệt yêu cầu", yPosition, BtnDuyetYeuCau_Click);
                yPosition += 60;
                AddMenuButton("Thông báo", yPosition, BtnThongBao_Click);
                yPosition += 60;
                AddMenuButton("Tài liệu", yPosition, BtnTaiLieu_Click);
                yPosition += 60;
                AddMenuButton("Báo cáo tiến độ", yPosition, BtnBaoCaoTienDo_Click);
                yPosition += 60;
                AddMenuButton("Thông tin cá nhân", yPosition, BtnThongTinCaNhan_Click);
                yPosition += 60;
            }
            else if (AuthorizationHelper.IsSinhVien())
            {
                AddMenuButton("Đồ án của tôi", yPosition, BtnDoAnSinhVien_Click);
                yPosition += 60;
                AddMenuButton("Đăng ký đồ án", yPosition, BtnDangKyDoAn_Click);
                yPosition += 60;
                AddMenuButton("Lịch sử yêu cầu", yPosition, BtnLichSuYeuCau_Click);
                yPosition += 60;
                AddMenuButton("Cập nhật tiến độ", yPosition, BtnCapNhatTienDo_Click);
                yPosition += 60;
                AddMenuButton("Nộp tài liệu", yPosition, BtnNopTaiLieu_Click);
                yPosition += 60;
                AddMenuButton("Thông báo", yPosition, BtnThongBao_Click);
                yPosition += 60;
                AddMenuButton("Thông tin cá nhân", yPosition, BtnThongTinCaNhan_Click);
                yPosition += 60;
            }
            
            // Thêm nút đăng xuất ở cuối
            yPosition += 40;
            AddLogoutButton(yPosition);
        }
        
        private void AddMenuButton(string text, int yPosition, EventHandler clickHandler)
        {
            var button = new Button
            {
                Text = text,
                Size = new Size(220, 50),
                Location = new Point(15, yPosition),
                BackColor = Constants.Colors.TextDark,
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0)
            };
            button.FlatAppearance.BorderSize = 0;
            button.Click += clickHandler;
            
            // Hover effect
            button.MouseEnter += (s, e) => button.BackColor = Constants.Colors.Primary;
            button.MouseLeave += (s, e) => button.BackColor = Constants.Colors.TextDark;
            
            panelSidebar.Controls.Add(button);
        }
        
        private void AddLogoutButton(int yPosition)
        {
            var button = new Button
            {
                Text = "Đăng xuất",
                Size = new Size(220, 50),
                Location = new Point(15, yPosition),
                BackColor = Constants.Colors.PrimaryHover,
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };
            button.FlatAppearance.BorderSize = 0;
            button.Click += BtnDangXuat_Click;
            
            panelSidebar.Controls.Add(button);
        }
        
        private string GetUserDisplayName()
        {
            var user = UserSession.CurrentUser;
            if (user?.MaSvNavigation != null)
                return user.MaSvNavigation.HoTen;
            if (user?.MaGvNavigation != null)
                return user.MaGvNavigation.HoTen;
            return user?.TenDangNhap ?? "";
        }

        // Event handlers cho các menu button
        private void BtnQuanLyDoAn_Click(object sender, EventArgs e)
        {
            if (!AuthorizationHelper.CheckPermission("QuanLyDoAn"))
            {
                AuthorizationHelper.ShowAccessDeniedMessage();
                return;
            }
            LoadUserControl(new QuanLyDoAnControl());
        }

        private void BtnQuanLyTaiKhoan_Click(object sender, EventArgs e)
        {
            if (!AuthorizationHelper.CheckPermission("QuanLyTaiKhoan"))
            {
                AuthorizationHelper.ShowAccessDeniedMessage();
                return;
            }
            LoadUserControl(new QuanLyTaiKhoanControl());
        }

        private void BtnQuanLyGiangVien_Click(object sender, EventArgs e)
        {
            if (!AuthorizationHelper.IsAdmin())
            {
                AuthorizationHelper.ShowAccessDeniedMessage();
                return;
            }
            LoadUserControl(new QuanLyGiangVienControl());
        }

        private void BtnQuanLySinhVien_Click(object sender, EventArgs e)
        {
            if (!AuthorizationHelper.IsAdmin())
            {
                AuthorizationHelper.ShowAccessDeniedMessage();
                return;
            }
            LoadUserControl(new QuanLySinhVienControl());
        }

        private void BtnQuanLyDanhMuc_Click(object sender, EventArgs e)
        {
            if (!AuthorizationHelper.IsAdmin())
            {
                AuthorizationHelper.ShowAccessDeniedMessage();
                return;
            }
            LoadUserControl(new QuanLyDanhMucControl());
        }
        
        private void BtnDoAnGiangVien_Click(object sender, EventArgs e)
        {
            if (!AuthorizationHelper.CheckPermission("DoAnGiangVien"))
            {
                AuthorizationHelper.ShowAccessDeniedMessage();
                return;
            }
            LoadUserControl(new DoAnGiangVienControl());
        }
        
        private void BtnDoAnSinhVien_Click(object sender, EventArgs e)
        {
            LoadUserControl(new DoAnSinhVienControl());
        }
        
        private void BtnCapNhatTienDo_Click(object sender, EventArgs e)
        {
            LoadUserControl(new CapNhatTienDoControl());
        }
        
        private void BtnNopTaiLieu_Click(object sender, EventArgs e)
        {
            LoadUserControl(new NopTaiLieuControl());
        }
        
        private void BtnThongTinCaNhan_Click(object sender, EventArgs e)
        {
            LoadUserControl(new ThongTinCaNhanControl());
        }
        
        private void BtnThongBao_Click(object sender, EventArgs e)
        {
            if (AuthorizationHelper.IsGiangVien())
            {
                LoadUserControl(new ThongBaoGiangVienControl());
            }
            else if (AuthorizationHelper.IsSinhVien())
            {
                LoadUserControl(new ThongBaoSinhVienControl());
            }
        }
        
        private void BtnTaiLieu_Click(object sender, EventArgs e)
        {
            if (AuthorizationHelper.IsGiangVien())
            {
                LoadUserControl(new TaiLieuGiangVienControl());
            }
            else
            {
                MessageBox.Show("Chức năng đang phát triển!");
            }
        }
        
        private void BtnBaoCaoTienDo_Click(object sender, EventArgs e)
        {
            if (AuthorizationHelper.IsGiangVien())
            {
                LoadUserControl(new BaoCaoTienDoControl());
            }
            else
            {
                MessageBox.Show("Chức năng đang phát triển!");
            }
        }

        private void BtnTaoDoAn_Click(object sender, EventArgs e)
        {
            var form = new TaoDoAnForm();
            form.ShowDialog();
        }

        private void BtnDuyetYeuCau_Click(object sender, EventArgs e)
        {
            LoadUserControl(new DuyetYeuCauControl());
        }

        private void BtnDangKyDoAn_Click(object sender, EventArgs e)
        {
            LoadUserControl(new DangKyDoAnControl());
        }
        
        private void BtnLichSuYeuCau_Click(object sender, EventArgs e)
        {
            LoadUserControl(new LichSuYeuCauControl());
        }
        
        private void LoadUserControl(UserControl userControl)
        {
            panelContent.Controls.Clear();
            userControl.Dock = DockStyle.Fill;
            panelContent.Controls.Add(userControl);
        }

        private void BtnDangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                UserSession.CurrentUser = null;
                this.Hide();
                new LoginForm().ShowDialog();
                this.Close();
            }
        }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    }
}
