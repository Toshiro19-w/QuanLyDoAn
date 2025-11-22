using System;
using System.Windows.Forms;
using QuanLyDoAn.Controller;
using QuanLyDoAn.Model.EF;
using QuanLyDoAn.Utils;
using Microsoft.EntityFrameworkCore;

namespace QuanLyDoAn.View
{
    public partial class PhanCongGiangVienForm : Form
    {
        private string _maDeTai;
        private PhanCongController _controller;
        
        public PhanCongGiangVienForm(string maDeTai, string tenDeTai)
        {
            InitializeComponent();
            _maDeTai = maDeTai;
            _controller = new PhanCongController();
            lblThongTin.Text = $"Đồ án: {tenDeTai}";
            ThemeHelper.ApplyTheme(this);
            LoadData();
        }
        
        private void LoadData()
        {
            // Load danh sách giảng viên
            using var context = new QuanLyDoAnContext();
            var giangViens = context.GiangViens
                .Select(g => new { g.MaGv, g.HoTen })
                .ToList();
            
            cboGiangVien.DataSource = giangViens;
            cboGiangVien.DisplayMember = "HoTen";
            cboGiangVien.ValueMember = "MaGv";
            
            // Load loại đánh giá (chỉ PB và HĐ, không có HD)
            var loaiDanhGia = context.LoaiDanhGias
                .Where(l => l.MaLoaiDanhGia != "HD")
                .Select(l => new { l.MaLoaiDanhGia, l.TenLoaiDanhGia })
                .ToList();
            
            cboLoaiDanhGia.DataSource = loaiDanhGia;
            cboLoaiDanhGia.DisplayMember = "TenLoaiDanhGia";
            cboLoaiDanhGia.ValueMember = "MaLoaiDanhGia";
            
            // Load danh sách đã phân công
            LoadDanhSachPhanCong();
        }
        
        private void LoadDanhSachPhanCong()
        {
            var danhSach = _controller.LayDanhSachPhanCong(_maDeTai);
            
            // Thêm GVHD vào danh sách hiển thị
            using var ctx = new QuanLyDoAnContext();
            var doAn = ctx.DoAns
                .Include(d => d.MaGvNavigation)
                .FirstOrDefault(d => d.MaDeTai == _maDeTai);
            
            var danhSachFull = new List<dynamic>();
            
            // Thêm GVHD nếu có
            if (doAn?.MaGvNavigation != null)
            {
                danhSachFull.Add(new
                {
                    MaGv = doAn.MaGv,
                    TenGiangVien = doAn.MaGvNavigation.HoTen,
                    MaLoaiDanhGia = "HD",
                    TenLoaiDanhGia = "Hướng dẫn",
                    DaCham = false,
                    DiemThanhPhan = (decimal?)null,
                    NgayDanhGia = (DateOnly?)null
                });
            }
            
            // Thêm các phân công khác
            foreach (var item in danhSach)
            {
                danhSachFull.Add(new
                {
                    item.MaGv,
                    item.TenGiangVien,
                    item.MaLoaiDanhGia,
                    item.TenLoaiDanhGia,
                    item.DaCham,
                    item.DiemThanhPhan,
                    item.NgayDanhGia
                });
            }
            
            dgvPhanCong.DataSource = danhSachFull;
            
            if (dgvPhanCong.Columns["MaGv"] != null)
                dgvPhanCong.Columns["MaGv"].Visible = false;
            if (dgvPhanCong.Columns["MaLoaiDanhGia"] != null)
                dgvPhanCong.Columns["MaLoaiDanhGia"].Visible = false;
            if (dgvPhanCong.Columns["TenGiangVien"] != null)
                dgvPhanCong.Columns["TenGiangVien"].HeaderText = "Giảng viên";
            if (dgvPhanCong.Columns["TenLoaiDanhGia"] != null)
                dgvPhanCong.Columns["TenLoaiDanhGia"].HeaderText = "Loại đánh giá";
            if (dgvPhanCong.Columns["DaCham"] != null)
                dgvPhanCong.Columns["DaCham"].HeaderText = "Đã chấm";
            if (dgvPhanCong.Columns["DiemThanhPhan"] != null)
                dgvPhanCong.Columns["DiemThanhPhan"].HeaderText = "Điểm";
            if (dgvPhanCong.Columns["NgayDanhGia"] != null)
                dgvPhanCong.Columns["NgayDanhGia"].HeaderText = "Ngày chấm";
        }
        
        private void BtnPhanCong_Click(object sender, EventArgs e)
        {
            if (cboGiangVien.SelectedValue == null || cboLoaiDanhGia.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn giảng viên và loại đánh giá!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            string maGv = cboGiangVien.SelectedValue.ToString()!;
            string maLoaiDanhGia = cboLoaiDanhGia.SelectedValue.ToString()!;
            
            if (_controller.PhanCongGiangVien(_maDeTai, maGv, maLoaiDanhGia, out string error))
            {
                MessageBox.Show("Phân công thành công!", "Thành công", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachPhanCong();
            }
            else
            {
                MessageBox.Show(error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void BtnHuyPhanCong_Click(object sender, EventArgs e)
        {
            if (dgvPhanCong.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn phân công cần hủy!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var row = dgvPhanCong.CurrentRow;
            string maGv = row.Cells["MaGv"].Value?.ToString() ?? "";
            string maLoaiDanhGia = row.Cells["MaLoaiDanhGia"].Value?.ToString() ?? "";
            bool daCham = (bool)(row.Cells["DaCham"].Value ?? false);
            
            if (daCham)
            {
                MessageBox.Show("Không thể hủy phân công đã chấm điểm!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var confirm = MessageBox.Show("Bạn có chắc muốn hủy phân công này?", "Xác nhận", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (confirm == DialogResult.Yes)
            {
                if (_controller.HuyPhanCong(_maDeTai, maGv, maLoaiDanhGia, out string error))
                {
                    MessageBox.Show("Hủy phân công thành công!", "Thành công", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachPhanCong();
                }
                else
                {
                    MessageBox.Show(error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        private void BtnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
