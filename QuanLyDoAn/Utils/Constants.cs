using System.Drawing;

namespace QuanLyDoAn.Utils
{
    public static class Constants
    {
        // Màu sắc cho giao diện
        public static class Colors
        {
            public static readonly Color Background = ColorTranslator.FromHtml("#F2FDFF");
            public static readonly Color FormBackground = ColorTranslator.FromHtml("#BCF6FF");
            public static readonly Color HeaderBackground = ColorTranslator.FromHtml("#85F4FB");
            public static readonly Color Hover = ColorTranslator.FromHtml("#4EF2F3");
            public static readonly Color Primary = ColorTranslator.FromHtml("#19E3D9");
            public static readonly Color PrimaryHover = ColorTranslator.FromHtml("#08B4A1");
            public static readonly Color Border = ColorTranslator.FromHtml("#02856F");
            public static readonly Color TextDark = ColorTranslator.FromHtml("#005542");
            public static readonly Color TextPrimary = ColorTranslator.FromHtml("#00261B");
        }

        // Vai trò người dùng
        public static class UserRoles
        {
            public const string Admin = "Admin";
            public const string GiangVien = "GiangVien";
            public const string SinhVien = "SinhVien";
        }

        // Trạng thái đồ án
        public static class DoAnStatus
        {
            public const string DangThucHien = "Đang thực hiện";
            public const string HoanThanh = "Hoàn thành";
            public const string TamDung = "Tạm dừng";
            public const string Huy = "Hủy";
        }

        // Thông báo
        public static class Messages
        {
            public const string ThemThanhCong = "Thêm thành công!";
            public const string CapNhatThanhCong = "Cập nhật thành công!";
            public const string XoaThanhCong = "Xóa thành công!";
            public const string XacNhanXoa = "Bạn có chắc muốn xóa?";
            public const string DuLieuKhongHopLe = "Dữ liệu không hợp lệ!";
            public const string VuiLongNhapDayDu = "Vui lòng nhập đầy đủ thông tin!";
        }
    }
}