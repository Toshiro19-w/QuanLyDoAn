-- Thêm cột điểm vào bảng TienDo

USE QLDA_SIMPLE;
GO

-- Thêm cột DiemTienDo vào bảng TienDo
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'TienDo' AND COLUMN_NAME = 'DiemTienDo')
BEGIN
    ALTER TABLE TienDo ADD DiemTienDo DECIMAL(4,2) NULL;
    PRINT N'Đã thêm cột DiemTienDo vào bảng TienDo';
END
ELSE
    PRINT N'Cột DiemTienDo đã tồn tại';
GO

-- Stored procedure chấm điểm tiến độ
CREATE OR ALTER PROCEDURE sp_ChamDiemTienDo
    @MaTienDo INT,
    @DiemTienDo DECIMAL(4,2),
    @NhanXet NVARCHAR(MAX) = NULL
AS
BEGIN
    UPDATE TienDo
    SET DiemTienDo = @DiemTienDo,
        NhanXet = @NhanXet
    WHERE MaTienDo = @MaTienDo;
END;
GO

-- Stored procedure tính điểm trung bình tiến độ (có hệ số phạt)
CREATE OR ALTER PROCEDURE sp_TinhDiemTrungBinhTienDo
    @MaDeTai VARCHAR(10),
    @DiemTrungBinh DECIMAL(4,2) OUTPUT
AS
BEGIN
    -- Tính điểm với hệ số phạt:
    -- - Nộp đúng hạn (DungHan): 100% điểm
    -- - Nộp trễ (TreHan): 80% điểm (phạt 20%)
    -- - Không nộp: 0 điểm
    SELECT @DiemTrungBinh = AVG(
        CASE 
            WHEN TrangThaiNop = 'DungHan' THEN DiemTienDo
            WHEN TrangThaiNop = 'TreHan' THEN DiemTienDo * 0.8
            ELSE 0
        END
    )
    FROM TienDo
    WHERE MaDeTai = @MaDeTai 
      AND DiemTienDo IS NOT NULL;
END;
GO

-- Stored procedure tính điểm tổng kết mới (bao gồm điểm tiến độ)
CREATE OR ALTER PROCEDURE sp_TinhDiemTongKetMoi
    @MaDeTai VARCHAR(10)
AS
BEGIN
    DECLARE @DiemTieuChi DECIMAL(4, 2);
    DECLARE @DiemTienDo DECIMAL(4, 2);
    DECLARE @DiemTongKet DECIMAL(4, 2);

    -- Tính điểm theo tiêu chí (HD: 40%, PB: 30%, HĐ: 30%)
    SELECT @DiemTieuChi = SUM(
        DG.DiemThanhPhan * (LDG.TrongSoDiem / 100.0)
    )
    FROM DanhGia DG
    JOIN LoaiDanhGia LDG ON DG.MaLoaiDanhGia = LDG.MaLoaiDanhGia
    WHERE DG.MaDeTai = @MaDeTai 
      AND DG.DiemThanhPhan IS NOT NULL;

    -- Tính điểm tiến độ (có hệ số phạt)
    EXEC sp_TinhDiemTrungBinhTienDo @MaDeTai, @DiemTienDo OUTPUT;

    -- Tính điểm tổng kết:
    -- - Điểm tiêu chí (HD/PB/HĐ): 70%
    -- - Điểm tiến độ (GVHD): 30%
    IF @DiemTieuChi IS NOT NULL AND @DiemTienDo IS NOT NULL
        SET @DiemTongKet = (@DiemTieuChi * 0.7) + (@DiemTienDo * 0.3);
    ELSE IF @DiemTieuChi IS NOT NULL
        SET @DiemTongKet = @DiemTieuChi;
    ELSE IF @DiemTienDo IS NOT NULL
        SET @DiemTongKet = @DiemTienDo;
      
    -- Cập nhật điểm tổng kết vào bảng DoAn
    UPDATE DoAn
    SET Diem = ROUND(@DiemTongKet, 2)
    WHERE MaDeTai = @MaDeTai;
END;
GO

PRINT N'✅ Đã thêm tính năng chấm điểm tiến độ với hệ số phạt!';
PRINT N'Công thức: DiemTongKet = DiemTieuChi × 70% + DiemTienDo × 30%';
PRINT N'Hệ số phạt: Nộp trễ -20%, Không nộp = 0 điểm';
