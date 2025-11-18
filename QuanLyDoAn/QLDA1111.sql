/* ======================================================
   CƠ SỞ DỮ LIỆU QUẢN LÝ ĐỒ ÁN TỐT NGHIỆP
   ====================================================== */

-- Xóa CSDL nếu đã tồn tại
IF DB_ID('QLDA_SIMPLE') IS NOT NULL
    DROP DATABASE QLDA_SIMPLE;
GO

-- Tạo mới
CREATE DATABASE QLDA_SIMPLE;
GO
USE QLDA_SIMPLE;
GO

/* ======================================================
   1. TẠO CÁC BẢNG
   ====================================================== */
CREATE TABLE ChuyenNganh (
    MaChuyenNganh VARCHAR(10) PRIMARY KEY,
    TenChuyenNganh NVARCHAR(50) NOT NULL
);

CREATE TABLE SinhVien (
    MaSV VARCHAR(10) PRIMARY KEY,
    HoTen NVARCHAR(50) NOT NULL,
    Lop VARCHAR(10),
    Email VARCHAR(50),
    SoDienThoai VARCHAR(15),
    MaChuyenNganh VARCHAR(10),
    NgaySinh DATE,
    FOREIGN KEY (MaChuyenNganh) REFERENCES ChuyenNganh(MaChuyenNganh)
);

CREATE TABLE GiangVien (
    MaGV VARCHAR(10) PRIMARY KEY,
    HoTen NVARCHAR(50) NOT NULL,
    BoMon NVARCHAR(50),
    Email VARCHAR(50),
    MaChuyenNganh VARCHAR(10),
    ChucVu NVARCHAR(50),
    FOREIGN KEY (MaChuyenNganh) REFERENCES ChuyenNganh(MaChuyenNganh)
);

CREATE TABLE KyHoc (
    MaKy VARCHAR(10) PRIMARY KEY,
    TenKy NVARCHAR(50),
    NamHoc INT CHECK (NamHoc > 0)
);

CREATE TABLE LoaiDoAn (
    MaLoaiDoAn VARCHAR(10) PRIMARY KEY,
    TenLoaiDoAn NVARCHAR(50)
);

CREATE TABLE TrangThaiDoAn (
    MaTrangThai VARCHAR(10) PRIMARY KEY,
    TenTrangThai NVARCHAR(50)
);

CREATE TABLE DoAn (
    MaDeTai VARCHAR(10) PRIMARY KEY,
    TenDeTai NVARCHAR(200) NOT NULL,
    MoTa NVARCHAR(MAX),
    MaSV VARCHAR(10) UNIQUE,
    MaGV VARCHAR(10) NOT NULL,
    MaKy VARCHAR(10),
    MaChuyenNganh VARCHAR(10),
    NgayBatDau DATE,
    NgayKetThuc DATE,
    Diem DECIMAL(4,2) CHECK (Diem >= 0 AND Diem <= 10),
    MaLoaiDoAn VARCHAR(10),
    MaTrangThai VARCHAR(10) NOT NULL,
    FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV),
    FOREIGN KEY (MaGV) REFERENCES GiangVien(MaGV),
    FOREIGN KEY (MaKy) REFERENCES KyHoc(MaKy),
    FOREIGN KEY (MaChuyenNganh) REFERENCES ChuyenNganh(MaChuyenNganh),
    FOREIGN KEY (MaLoaiDoAn) REFERENCES LoaiDoAn(MaLoaiDoAn),
    FOREIGN KEY (MaTrangThai) REFERENCES TrangThaiDoAn(MaTrangThai),
	CONSTRAINT CK_DoAn_Ngay CHECK (NgayKetThuc >= NgayBatDau)
);

CREATE TABLE TienDo (
    MaTienDo INT IDENTITY(1,1) PRIMARY KEY,
    MaDeTai VARCHAR(10),
    GiaiDoan NVARCHAR(50),
    NgayNop DATE,
    NhanXet NVARCHAR(MAX),
    TrangThaiNop NVARCHAR(20) CHECK (TrangThaiNop IN (N'DungHan', N'TreHan')),
    FOREIGN KEY (MaDeTai) REFERENCES DoAn(MaDeTai)
);

CREATE TABLE TaiLieu (
    MaTaiLieu INT IDENTITY(1,1) PRIMARY KEY,
    MaDeTai VARCHAR(10),
    TenTaiLieu NVARCHAR(100),
    DuongDan VARCHAR(255),
    NgayUpload DATE,
    FOREIGN KEY (MaDeTai) REFERENCES DoAn(MaDeTai)
);

CREATE TABLE DanhGia (
    MaDanhGia INT IDENTITY(1,1) PRIMARY KEY,
    MaDeTai VARCHAR(10),
    MaGV VARCHAR(10),
    DiemThanhPhan DECIMAL(4,2) CHECK (DiemThanhPhan >= 0 AND DiemThanhPhan <= 10),
    NhanXet NVARCHAR(MAX),
    NgayDanhGia DATE,
    FOREIGN KEY (MaDeTai) REFERENCES DoAn(MaDeTai),
    FOREIGN KEY (MaGV) REFERENCES GiangVien(MaGV)
);

CREATE TABLE ThongBao (
    MaThongBao INT IDENTITY(1,1) PRIMARY KEY,
    MaDeTai VARCHAR(10),
    NoiDung NVARCHAR(MAX),
    NgayGui DATE,
    FOREIGN KEY (MaDeTai) REFERENCES DoAn(MaDeTai)
);

CREATE TABLE TaiKhoan (
    TenDangNhap VARCHAR(30) PRIMARY KEY,
    MatKhau NVARCHAR(100) NOT NULL,
    VaiTro NVARCHAR(20) CHECK (VaiTro IN (N'Admin', N'GiangVien', N'SinhVien')),
    MaSV VARCHAR(10),
    MaGV VARCHAR(10),
    FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV),
    FOREIGN KEY (MaGV) REFERENCES GiangVien(MaGV)
);
GO

/* ======================================================
   2. DỮ LIỆU MẪU
   ====================================================== */
INSERT INTO ChuyenNganh VALUES
('CN001', N'Trí tuệ nhân tạo'),
('CN002', N'Phát triển phần mềm');

INSERT INTO KyHoc VALUES
('HK1-2024', N'Học kỳ 1', 2024),
('HK2-2024', N'Học kỳ 2', 2024);

INSERT INTO LoaiDoAn VALUES
('DA01', N'Đồ án cơ sở'),
('DA02', N'Đồ án tốt nghiệp');

INSERT INTO TrangThaiDoAn VALUES
('TT01', N'Đang thực hiện'),
('TT02', N'Hoàn thành'),
('TT03', N'Chưa nộp');

INSERT INTO GiangVien VALUES
('GV001', N'GS. Nguyễn Văn X', N'Công nghệ thông tin', 'x@gmail.com', 'CN001', N'Giáo sư'),
('GV002', N'TS. Trần Thị Y', N'Công nghệ thông tin', 'y@gmail.com', 'CN002', N'Tiến sĩ');

INSERT INTO SinhVien VALUES
('SV001', N'Nguyễn Văn A', 'CNTT1', 'a@gmail.com', '0901234567', 'CN001', '2002-01-01'),
('SV002', N'Trần Thị B', 'CNTT1', 'b@gmail.com', '0902345678', 'CN002', '2002-02-02'),
('SV003', N'Lê Văn C', 'CNTT2', 'c@gmail.com', '0903456789', 'CN001', '2002-03-03');

INSERT INTO DoAn VALUES
('DA001', N'Hệ thống gợi ý sản phẩm', N'Sử dụng AI để gợi ý sản phẩm', 'SV001', 'GV001', 'HK1-2024', 'CN001', '2024-09-01', '2024-12-01', 8.5, 'DA02', 'TT02'),
('DA002', N'Ứng dụng quản lý lớp học', N'Ứng dụng web quản lý điểm danh', 'SV002', 'GV002', 'HK1-2024', 'CN002', '2024-09-01', '2024-12-01', NULL, 'DA01', 'TT01');

INSERT INTO TaiKhoan VALUES
('admin', N'123456', N'Admin', NULL, NULL),
('sv001', N'sv001', N'SinhVien', 'SV001', NULL),
('sv002', N'sv002', N'SinhVien', 'SV002', NULL),
('gv001', N'gv001', N'GiangVien', NULL, 'GV001'),
('gv002', N'gv002', N'GiangVien', NULL, 'GV002');
GO

/* ======================================================
   3. STORED PROCEDURES
   ====================================================== */

-- ========== SINH VIÊN ==========
CREATE PROCEDURE sp_ThemSinhVien
    @MaSV VARCHAR(10),
    @HoTen NVARCHAR(50),
    @Lop VARCHAR(10),
    @Email VARCHAR(50),
    @SoDienThoai VARCHAR(15),
    @MaChuyenNganh VARCHAR(10),
    @NgaySinh DATE
AS
BEGIN
    INSERT INTO SinhVien VALUES (@MaSV, @HoTen, @Lop, @Email, @SoDienThoai, @MaChuyenNganh, @NgaySinh);
END;
GO

CREATE PROCEDURE sp_SuaSinhVien
    @MaSV VARCHAR(10),
    @HoTen NVARCHAR(50),
    @Lop VARCHAR(10),
    @Email VARCHAR(50),
    @SoDienThoai VARCHAR(15),
    @MaChuyenNganh VARCHAR(10),
    @NgaySinh DATE
AS
BEGIN
    UPDATE SinhVien
    SET HoTen=@HoTen, Lop=@Lop, Email=@Email, SoDienThoai=@SoDienThoai,
        MaChuyenNganh=@MaChuyenNganh, NgaySinh=@NgaySinh
    WHERE MaSV=@MaSV;
END;
GO

CREATE PROCEDURE sp_XoaSinhVien
    @MaSV VARCHAR(10)
AS
BEGIN
    DELETE FROM SinhVien WHERE MaSV=@MaSV;
END;
GO

CREATE PROCEDURE sp_TimSinhVien
    @TuKhoa NVARCHAR(50)
AS
BEGIN
    SELECT * FROM SinhVien
    WHERE MaSV LIKE '%' + @TuKhoa + '%'
       OR HoTen LIKE N'%' + @TuKhoa + N'%'
       OR Lop LIKE N'%' + @TuKhoa + N'%';
END;
GO

-- ========== GIẢNG VIÊN ==========
CREATE PROCEDURE sp_ThemGiangVien
    @MaGV VARCHAR(10),
    @HoTen NVARCHAR(50),
    @BoMon NVARCHAR(50),
    @Email VARCHAR(50),
    @MaChuyenNganh VARCHAR(10),
    @ChucVu NVARCHAR(50)
AS
BEGIN
    INSERT INTO GiangVien VALUES (@MaGV, @HoTen, @BoMon, @Email, @MaChuyenNganh, @ChucVu);
END;
GO

CREATE PROCEDURE sp_SuaGiangVien
    @MaGV VARCHAR(10),
    @HoTen NVARCHAR(50),
    @BoMon NVARCHAR(50),
    @Email VARCHAR(50),
    @MaChuyenNganh VARCHAR(10),
    @ChucVu NVARCHAR(50)
AS
BEGIN
    UPDATE GiangVien
    SET HoTen=@HoTen, BoMon=@BoMon, Email=@Email, MaChuyenNganh=@MaChuyenNganh, ChucVu=@ChucVu
    WHERE MaGV=@MaGV;
END;
GO

CREATE PROCEDURE sp_XoaGiangVien
    @MaGV VARCHAR(10)
AS
BEGIN
    DELETE FROM GiangVien WHERE MaGV=@MaGV;
END;
GO

-- ========== ĐỒ ÁN ==========
CREATE PROCEDURE sp_ThemDoAn
    @MaDeTai VARCHAR(10),
    @TenDeTai NVARCHAR(200),
    @MoTa NVARCHAR(MAX),
    @MaSV VARCHAR(10),
    @MaGV VARCHAR(10),
    @MaKy VARCHAR(10),
    @MaChuyenNganh VARCHAR(10),
    @NgayBatDau DATE,
    @NgayKetThuc DATE,
    @MaLoaiDoAn VARCHAR(10),
    @MaTrangThai VARCHAR(10)
AS
BEGIN
    INSERT INTO DoAn (MaDeTai, TenDeTai, MoTa, MaSV, MaGV, MaKy, MaChuyenNganh,
                      NgayBatDau, NgayKetThuc, MaLoaiDoAn, MaTrangThai)
    VALUES (@MaDeTai, @TenDeTai, @MoTa, @MaSV, @MaGV, @MaKy, @MaChuyenNganh,
            @NgayBatDau, @NgayKetThuc, @MaLoaiDoAn, @MaTrangThai);
END;
GO

CREATE PROCEDURE sp_SuaDoAn
    @MaDeTai VARCHAR(10),
    @TenDeTai NVARCHAR(200),
    @MoTa NVARCHAR(MAX),
    @MaGV VARCHAR(10),
    @MaKy VARCHAR(10),
    @NgayKetThuc DATE,
    @MaTrangThai VARCHAR(10)
AS
BEGIN
    UPDATE DoAn
    SET TenDeTai=@TenDeTai, MoTa=@MoTa, MaGV=@MaGV, MaKy=@MaKy,
        NgayKetThuc=@NgayKetThuc, MaTrangThai=@MaTrangThai
    WHERE MaDeTai=@MaDeTai;
END;
GO

CREATE PROCEDURE sp_XoaDoAn
    @MaDeTai VARCHAR(10)
AS
BEGIN
    DELETE FROM DoAn WHERE MaDeTai=@MaDeTai;
END;
GO

CREATE PROCEDURE sp_LietKeDoAn
AS
BEGIN
    SELECT DA.MaDeTai, DA.TenDeTai, SV.HoTen AS SinhVien, GV.HoTen AS GiangVien,
           TT.TenTrangThai, DA.NgayBatDau, DA.NgayKetThuc, DA.Diem
    FROM DoAn DA
    JOIN SinhVien SV ON DA.MaSV = SV.MaSV
    JOIN GiangVien GV ON DA.MaGV = GV.MaGV
    JOIN TrangThaiDoAn TT ON DA.MaTrangThai = TT.MaTrangThai;
END;
GO

-- ========== TIẾN ĐỘ ==========
CREATE PROCEDURE sp_ThemTienDo
    @MaDeTai VARCHAR(10),
    @GiaiDoan NVARCHAR(50),
    @NgayNop DATE,
    @NhanXet NVARCHAR(MAX),
    @TrangThaiNop NVARCHAR(20)
AS
BEGIN
    INSERT INTO TienDo (MaDeTai, GiaiDoan, NgayNop, NhanXet, TrangThaiNop)
    VALUES (@MaDeTai, @GiaiDoan, @NgayNop, @NhanXet, @TrangThaiNop);
END;
GO

CREATE PROCEDURE sp_SuaTienDo
    @MaTienDo INT,
    @NhanXet NVARCHAR(MAX),
    @TrangThaiNop NVARCHAR(20)
AS
BEGIN
    UPDATE TienDo SET NhanXet=@NhanXet, TrangThaiNop=@TrangThaiNop WHERE MaTienDo=@MaTienDo;
END;
GO

CREATE PROCEDURE sp_XoaTienDo
    @MaTienDo INT
AS
BEGIN
    DELETE FROM TienDo WHERE MaTienDo=@MaTienDo;
END;
GO

CREATE PROCEDURE sp_XemTienDoTheoDoAn
    @MaDeTai VARCHAR(10)
AS
BEGIN
    SELECT * FROM TienDo WHERE MaDeTai=@MaDeTai;
END;
GO

-- ========== ĐÁNH GIÁ ==========
CREATE PROCEDURE sp_ThemDanhGia
    @MaDeTai VARCHAR(10),
    @MaGV VARCHAR(10),
    @DiemThanhPhan DECIMAL(4,2),
    @NhanXet NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO DanhGia (MaDeTai, MaGV, DiemThanhPhan, NhanXet, NgayDanhGia)
    VALUES (@MaDeTai, @MaGV, @DiemThanhPhan, @NhanXet, GETDATE());
END;
GO

CREATE PROCEDURE sp_XemDanhGiaTheoDoAn
    @MaDeTai VARCHAR(10)
AS
BEGIN
    SELECT DG.MaDanhGia, GV.HoTen AS GiangVien, DG.DiemThanhPhan, DG.NhanXet, DG.NgayDanhGia
    FROM DanhGia DG
    JOIN GiangVien GV ON DG.MaGV = GV.MaGV
    WHERE DG.MaDeTai = @MaDeTai;
END;
GO

-- ========== THÔNG BÁO ==========
CREATE PROCEDURE sp_ThemThongBao
    @MaDeTai VARCHAR(10),
    @NoiDung NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO ThongBao (MaDeTai, NoiDung, NgayGui)
    VALUES (@MaDeTai, @NoiDung, GETDATE());
END;
GO

CREATE PROCEDURE sp_XemThongBaoTheoDoAn
    @MaDeTai VARCHAR(10)
AS
BEGIN
    SELECT MaThongBao, NoiDung, NgayGui
    FROM ThongBao
    WHERE MaDeTai = @MaDeTai
    ORDER BY NgayGui DESC;
END;
GO
