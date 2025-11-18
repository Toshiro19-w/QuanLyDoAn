using System;
using System.Windows.Forms;
using QuanLyDoAn.Controller;
using QuanLyDoAn.Model.Entities;
using QuanLyDoAn.Utils;

namespace QuanLyDoAn.View
{
    public partial class ThongBaoGiangVienControl : UserControl
    {
        private ThongBaoController thongBaoController;

        public ThongBaoGiangVienControl()
        {
            InitializeComponent();
            thongBaoController = new ThongBaoController();
            ThemeHelper.ApplyTheme(this);
            LoadDoAn();
        }

        private void LoadDoAn()
        {
            var maGv = UserSession.CurrentUser?.MaGv;
            if (string.IsNullOrEmpty(maGv)) return;

            using var context = new QuanLyDoAn.Model.EF.QuanLyDoAnContext();
            var doAns = context.DoAns
                .Where(d => d.MaGv == maGv)
                .Select(d => new { d.MaDeTai, d.TenDeTai })
                .ToList();

            cmbDoAn.DataSource = doAns;
            cmbDoAn.DisplayMember = "TenDeTai";
            cmbDoAn.ValueMember = "MaDeTai";
        }

        private void btnGuiThongBao_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTieuDe.Text) || string.IsNullOrEmpty(txtNoiDung.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tiêu đề và nội dung!");
                return;
            }

            string noiDungDayDu = $"Tiêu đề: {txtTieuDe.Text}\n\nNội dung: {txtNoiDung.Text}\n\nNgười gửi: {UserSession.CurrentUser?.TenDangNhap}";
            string maDeTai = cmbDoAn.SelectedValue?.ToString() ?? "";

            if (thongBaoController.GuiThongBao(maDeTai, noiDungDayDu))
            {
                MessageBox.Show("Gửi thông báo thành công!");
                txtTieuDe.Clear();
                txtNoiDung.Clear();
            }
            else
            {
                MessageBox.Show("Gửi thông báo thất bại!");
            }
        }
    }
}