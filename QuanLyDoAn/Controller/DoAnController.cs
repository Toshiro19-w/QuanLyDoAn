using QuanLyDoAn.Model.Entities;
using QuanLyDoAn.Model.EF;
using QuanLyDoAn.Model.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;

namespace QuanLyDoAn.Controller
{
    public class DoAnController
    {
        public List<DoAnViewModel> LayDanhSachDoAn()
        {
            using var context = new QuanLyDoAnContext();
            var result = new List<DoAnViewModel>();
            
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = "sp_LietKeDoAn";
            command.CommandType = CommandType.StoredProcedure;
            context.Database.OpenConnection();
            
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new DoAnViewModel
                {
                    MaDeTai = reader["MaDeTai"].ToString() ?? "",
                    TenDeTai = reader["TenDeTai"].ToString() ?? "",
                    DanhSachSinhVien = reader["SinhVien"].ToString() ?? "",
                    TenGiangVien = reader["GiangVien"].ToString() ?? "",
                    TrangThai = reader["TenTrangThai"].ToString() ?? "",
                    NgayBatDau = reader["NgayBatDau"] != DBNull.Value ? DateOnly.FromDateTime((DateTime)reader["NgayBatDau"]) : null,
                    NgayKetThuc = reader["NgayKetThuc"] != DBNull.Value ? DateOnly.FromDateTime((DateTime)reader["NgayKetThuc"]) : null,
                    DiemText = reader["Diem"] != DBNull.Value ? Convert.ToDecimal(reader["Diem"]).ToString("F2") : "Chưa có điểm"
                });
            }
            
            return result;
        }

        public bool TaoDoAn(DoAn doAn, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using var context = new QuanLyDoAnContext();
                // Đảm bảo MaSv là NULL để tránh UNIQUE constraint violation
                doAn.MaSv = null;
                
                context.DoAns.Add(doAn);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException?.Message;
                errorMessage = "Lỗi khi tạo đồ án: " + ex.Message + (inner != null ? $" Chi tiết: {inner}" : string.Empty);
                return false;
            }
        }

        public bool CapNhatDoAn(DoAn doAn, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using var context = new QuanLyDoAnContext();
                context.Database.ExecuteSqlRaw(
                    "EXEC sp_SuaDoAn @MaDeTai, @TenDeTai, @MoTa, @MaSV, @MaGV, @MaKy, @NgayKetThuc, @MaTrangThai",
                    new SqlParameter("@MaDeTai", doAn.MaDeTai),
                    new SqlParameter("@TenDeTai", doAn.TenDeTai),
                    new SqlParameter("@MoTa", (object?)doAn.MoTa ?? DBNull.Value),
                    new SqlParameter("@MaSV", (object?)doAn.MaSv ?? DBNull.Value),
                    new SqlParameter("@MaGV", doAn.MaGv),
                    new SqlParameter("@MaKy", (object?)doAn.MaKy ?? DBNull.Value),
                    new SqlParameter("@NgayKetThuc", (object?)doAn.NgayKetThuc ?? DBNull.Value),
                    new SqlParameter("@MaTrangThai", doAn.MaTrangThai)
                );
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi khi cập nhật đồ án: " + ex.Message;
                return false;
            }
        }

        // Update only the LoaiDoAn for a given MaDeTai (use EF to avoid raw table name issues)
        public bool CapNhatLoaiDoAn(string maDeTai, string? maLoaiDoAn, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using var context = new QuanLyDoAnContext();
                var entity = context.DoAns.Find(maDeTai);
                if (entity == null)
                {
                    errorMessage = "Đề tài không tồn tại";
                    return false;
                }

                entity.MaLoaiDoAn = maLoaiDoAn;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi khi cập nhật loại đồ án: " + ex.Message;
                return false;
            }
        }

        public bool XoaDoAn(string maDeTai)
        {
            try
            {
                using var context = new QuanLyDoAnContext();
                context.Database.ExecuteSqlRaw(
                    "EXEC sp_XoaDoAn @MaDeTai",
                    new SqlParameter("@MaDeTai", maDeTai)
                );
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}