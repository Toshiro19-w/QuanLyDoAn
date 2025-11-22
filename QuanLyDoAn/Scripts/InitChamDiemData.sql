-- Tạo bảng LoaiDanhGia
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='LoaiDanhGia' AND xtype='U')
CREATE TABLE LoaiDanhGia (
    MaLoaiDanhGia VARCHAR(10) PRIMARY KEY,
    TenLoaiDanhGia NVARCHAR(50) NOT NULL,
    TrongSoDiem DECIMAL(5,2) NOT NULL
);

-- Tạo bảng TieuChiDanhGia
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TieuChiDanhGia' AND xtype='U')
CREATE TABLE TieuChiDanhGia (
    MaTieuChi INT IDENTITY(1,1) PRIMARY KEY,
    TenTieuChi NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(500),
    TrongSo DECIMAL(5,2) NOT NULL,
    DiemToiDa DECIMAL(4,2) DEFAULT 10,
    MaLoaiDoAn VARCHAR(10),
    FOREIGN KEY (MaLoaiDoAn) REFERENCES LoaiDoAn(MaLoaiDoAn)
);

-- Tạo bảng ChiTietDanhGia
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ChiTietDanhGia' AND xtype='U')
CREATE TABLE ChiTietDanhGia (
    MaChiTiet INT IDENTITY(1,1) PRIMARY KEY,
    MaDanhGia INT NOT NULL,
    MaTieuChi INT NOT NULL,
    Diem DECIMAL(4,2) NOT NULL,
    NhanXet NVARCHAR(500),
    FOREIGN KEY (MaDanhGia) REFERENCES DanhGia(MaDanhGia) ON DELETE CASCADE,
    FOREIGN KEY (MaTieuChi) REFERENCES TieuChiDanhGia(MaTieuChi) ON DELETE CASCADE
);

-- Thêm cột MaLoaiDanhGia vào bảng DanhGia nếu chưa có
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'DanhGia' AND COLUMN_NAME = 'MaLoaiDanhGia')
ALTER TABLE DanhGia ADD MaLoaiDanhGia VARCHAR(10);

-- Thêm foreign key constraint
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_DanhGia_LoaiDanhGia')
ALTER TABLE DanhGia ADD CONSTRAINT FK_DanhGia_LoaiDanhGia 
FOREIGN KEY (MaLoaiDanhGia) REFERENCES LoaiDanhGia(MaLoaiDanhGia);

-- Dữ liệu mẫu cho LoaiDanhGia
INSERT INTO LoaiDanhGia (MaLoaiDanhGia, TenLoaiDanhGia, TrongSoDiem) VALUES
('HD', N'Hướng dẫn', 40.00),
('PB', N'Phản biện', 30.00),
('HĐ', N'Hội đồng', 30.00);

-- Dữ liệu mẫu cho TieuChiDanhGia (cho đồ án tốt nghiệp)
INSERT INTO TieuChiDanhGia (TenTieuChi, MoTa, TrongSo, DiemToiDa, MaLoaiDoAn) VALUES
(N'Tính đúng đắn của nội dung', N'Đánh giá tính chính xác, khoa học của nội dung đồ án', 25.00, 10, 'DA01'),
(N'Tính sáng tạo và ứng dụng', N'Đánh giá khả năng sáng tạo và tính ứng dụng thực tế', 20.00, 10, 'DA01'),
(N'Khả năng trình bày', N'Đánh giá kỹ năng thuyết trình và trả lời câu hỏi', 15.00, 10, 'DA01'),
(N'Chất lượng báo cáo', N'Đánh giá chất lượng văn bản, hình ảnh, biểu đồ', 20.00, 10, 'DA01'),
(N'Tiến độ thực hiện', N'Đánh giá việc tuân thủ tiến độ và kế hoạch', 20.00, 10, 'DA01');

-- Dữ liệu mẫu cho TieuChiDanhGia (cho đồ án chuyên ngành)
INSERT INTO TieuChiDanhGia (TenTieuChi, MoTa, TrongSo, DiemToiDa, MaLoaiDoAn) VALUES
(N'Nắm vững kiến thức chuyên môn', N'Đánh giá mức độ hiểu biết kiến thức chuyên ngành', 30.00, 10, 'DA02'),
(N'Kỹ năng thực hành', N'Đánh giá khả năng áp dụng kiến thức vào thực tế', 25.00, 10, 'DA02'),
(N'Tính logic và mạch lạc', N'Đánh giá tính logic trong trình bày và giải quyết vấn đề', 20.00, 10, 'DA02'),
(N'Khả năng phân tích', N'Đánh giá khả năng phân tích và đưa ra giải pháp', 25.00, 10, 'DA02');

PRINT N'Đã khởi tạo thành công dữ liệu cho hệ thống chấm điểm!';
GO

-- Bước 1: Tạo bản ghi DanhGia và trả về MaDanhGia
CREATE OR ALTER PROCEDURE sp_ThemDanhGia
    @MaDeTai VARCHAR(10),
    @MaGV VARCHAR(10),
    @MaLoaiDanhGia VARCHAR(10),
    @NhanXet NVARCHAR(MAX) = NULL,
    @MaDanhGia INT OUTPUT 
AS
BEGIN
    INSERT INTO DanhGia (MaDeTai, MaGV, DiemThanhPhan, NhanXet, NgayDanhGia, MaLoaiDanhGia)
    VALUES (@MaDeTai, @MaGV, NULL, @NhanXet, GETDATE(), @MaLoaiDanhGia);
    
    SET @MaDanhGia = SCOPE_IDENTITY(); 
END;
GO

-- Bước 2: Tính toán DiemThanhPhan sau khi đã có ChiTietDanhGia
CREATE OR ALTER PROCEDURE sp_TinhDiemThanhPhan
    @MaDanhGia INT
AS
BEGIN
    DECLARE @DiemTinhToan DECIMAL(4, 2);

    -- Tính điểm dựa trên công thức trọng số của từng tiêu chí
    SELECT @DiemTinhToan = SUM(
        -- (Điểm thực tế / Điểm tối đa) * (Trọng số tiêu chí / 100) * 10 
        CTDG.Diem * (TCDG.TrongSo / 100.0) / TCDG.DiemToiDa * 10 
    )
    FROM ChiTietDanhGia CTDG
    JOIN TieuChiDanhGia TCDG ON CTDG.MaTieuChi = TCDG.MaTieuChi
    WHERE CTDG.MaDanhGia = @MaDanhGia;

    -- Cập nhật điểm thành phần đã tính vào bảng DanhGia
    UPDATE DanhGia
    SET DiemThanhPhan = ROUND(@DiemTinhToan, 2) -- Làm tròn 2 chữ số thập phân
    WHERE MaDanhGia = @MaDanhGia;
END;
GO

-- Bước 2.5: Thêm chi tiết đánh giá
CREATE OR ALTER PROCEDURE sp_ThemChiTietDanhGia
    @MaDanhGia INT,
    @MaTieuChi INT,
    @Diem DECIMAL(4,2),
    @NhanXet NVARCHAR(500) = NULL
AS
BEGIN
    INSERT INTO ChiTietDanhGia (MaDanhGia, MaTieuChi, Diem, NhanXet)
    VALUES (@MaDanhGia, @MaTieuChi, @Diem, @NhanXet);
END;
GO

-- Bước 3: Tính toán Diem Tong Ket cho Do An
CREATE OR ALTER PROCEDURE sp_TinhDiemTongKet
    @MaDeTai VARCHAR(10)
AS
BEGIN
    DECLARE @DiemTongKet DECIMAL(4, 2);

    -- Tính điểm tổng kết dựa trên trọng số của từng Loại Đánh Giá
    SELECT @DiemTongKet = SUM(
        -- DiemThanhPhan * (TrongSoLoaiDanhGia / 100)
        DG.DiemThanhPhan * (LDG.TrongSoDiem / 100.0)
    )
    FROM DanhGia DG
    JOIN LoaiDanhGia LDG ON DG.MaLoaiDanhGia = LDG.MaLoaiDanhGia
    WHERE DG.MaDeTai = @MaDeTai 
      AND DG.DiemThanhPhan IS NOT NULL; 
      
    -- Cập nhật điểm tổng kết vào bảng DoAn
    UPDATE DoAn
    SET Diem = ROUND(@DiemTongKet, 2)
    WHERE MaDeTai = @MaDeTai;
END;
GO