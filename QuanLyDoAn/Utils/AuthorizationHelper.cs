using QuanLyDoAn.Model.Entities;
using System.Windows.Forms;
using System.Linq;

namespace QuanLyDoAn.Utils
{
    public static class AuthorizationHelper
    {
        public static bool IsAdmin()
        {
            return UserSession.CurrentUser?.VaiTro == Constants.UserRoles.Admin;
        }

        public static bool IsGiangVien()
        {
            return UserSession.CurrentUser?.VaiTro == Constants.UserRoles.GiangVien;
        }

        public static bool IsSinhVien()
        {
            return UserSession.CurrentUser?.VaiTro == Constants.UserRoles.SinhVien;
        }

        public static void ConfigureMenuByRole(MenuStrip menuStrip)
        {
            foreach (ToolStripMenuItem item in menuStrip.Items)
            {
                ConfigureMenuItemByRole(item);
            }
        }

        private static void ConfigureMenuItemByRole(ToolStripMenuItem menuItem)
        {
            switch (menuItem.Name)
            {
                case "mnuQuanLyTaiKhoan":
                case "mnuQuanLyDoAn":
                    menuItem.Visible = IsAdmin();
                    break;
                case "mnuDoAnCuaToi":
                case "mnuTienDo":
                    menuItem.Visible = IsGiangVien() || IsSinhVien();
                    break;
                case "mnuThongBao":
                    menuItem.Visible = true; // Tất cả đều thấy
                    break;
            }

            foreach (ToolStripMenuItem subItem in menuItem.DropDownItems.OfType<ToolStripMenuItem>())
            {
                ConfigureMenuItemByRole(subItem);
            }
        }

        public static void ConfigureControlsByRole(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                ConfigureControlByRole(control);

                // recurse into child controls
                if (control.HasChildren)
                {
                    ConfigureControlsByRole(control.Controls);
                }
            }
        }

        private static void ConfigureControlByRole(Control control)
        {
            // adjust visibility by control name
            switch (control.Name)
            {
                case "btnDuyetYeuCau":
                case "btnDuyetDeTai":
                    control.Visible = IsGiangVien();
                    break;
                // keep existing buttons/menu behavior unchanged by default
            }
        }

        public static bool CheckPermission(string action)
        {
            if (UserSession.CurrentUser == null) return false;

            switch (action)
            {
                case "QuanLyTaiKhoan":
                    return IsAdmin();
                case "QuanLyDoAn":
                    // Admin có thể quản lý tất cả đồ án
                    return IsAdmin();
                case "DoAnGiangVien":
                case "ChamDiem":
                case "NhanXetTienDo":
                case "TaoDeTai":
                case "DuyetDeTai":
                case "DuyetYeuCau":
                    // Chỉ giảng viên có thể tạo và duyệt đề tài
                    return IsGiangVien();
                case "XemDoAn":
                case "CapNhatTienDo":
                    return IsGiangVien() || IsSinhVien();
                case "DangKyDoAn":
                    // Chỉ sinh viên có thể đăng ký đồ án
                    return IsSinhVien();
                default:
                    return false;
            }
        }

        public static void ShowAccessDeniedMessage()
        {
            MessageBox.Show("Bạn không có quyền truy cập chức năng này!", 
                "Truy cập bị từ chối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}