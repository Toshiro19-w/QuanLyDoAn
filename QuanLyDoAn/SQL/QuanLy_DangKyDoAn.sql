-- SQL Script ?? ki?m tra và qu?n lý quy trình ??ng ký ?? án

-- 1. Ki?m tra b?ng YeuCauDangKy t?n t?i
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'YeuCauDangKy')
BEGIN
    CREATE TABLE YeuCauDangKy (
        MaYeuCau INT PRIMARY KEY IDENTITY(1,1),
        MaDeTai VARCHAR(10) NOT NULL,
        MaSV VARCHAR(10) NOT NULL,
        NgayGui DATE NOT NULL,
        TrangThai NVARCHAR(20) NOT NULL,
        GhiChu NVARCHAR(500),
        FOREIGN KEY (MaDeTai) REFERENCES DoAn(MaDeTai) ON DELETE CASCADE,
        FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV) ON DELETE CASCADE
    );
    PRINT 'T?o b?ng YeuCauDangKy thành công!';
END
ELSE
    PRINT 'B?ng YeuCauDangKy ?ã t?n t?i!';

-- 2. Xem t?t c? yêu c?u ?ang ch? duy?t
SELECT 'DANH SÁCH YÊU C?U CH?A DUY?T' AS Category;
SELECT 
    ycdk.MaYeuCau,
    ycdk.MaDeTai,
    da.TenDeTai,
    da.MaGV,
    gv.HoTen AS TenGiangVien,
    ycdk.MaSV,
    sv.HoTen AS TenSinhVien,
    sv.Lop,
    ycdk.NgayGui,
    ycdk.TrangThai
FROM YeuCauDangKy ycdk
LEFT JOIN DoAn da ON ycdk.MaDeTai = da.MaDeTai
LEFT JOIN GiangVien gv ON da.MaGV = gv.MaGV
LEFT JOIN SinhVien sv ON ycdk.MaSV = sv.MaSV
WHERE ycdk.TrangThai = 'Pending'
ORDER BY ycdk.NgayGui DESC;

-- 3. Xem t?t c? ?? tài ch?a có sinh viên
SELECT 'DANH SÁCH ?? TÀI CH?A CÓ SINH VIÊN' AS Category;
SELECT 
    da.MaDeTai,
    da.TenDeTai,
    da.MaGV,
    gv.HoTen AS TenGiangVien,
    da.MaSV,
    da.MaTrangThai,
    da.NgayBatDau,
    da.NgayKetThuc
FROM DoAn da
LEFT JOIN GiangVien gv ON da.MaGV = gv.MaGV
WHERE da.MaSV IS NULL
ORDER BY da.NgayBatDau DESC;

-- 4. Xem t?t c? ?? tài ?ã có sinh viên
SELECT 'DANH SÁCH ?? TÀI ?Ã CÓ SINH VIÊN' AS Category;
SELECT 
    da.MaDeTai,
    da.TenDeTai,
    da.MaGV,
    gv.HoTen AS TenGiangVien,
    da.MaSV,
    sv.HoTen AS TenSinhVien,
    sv.Lop,
    da.MaTrangThai
FROM DoAn da
LEFT JOIN GiangVien gv ON da.MaGV = gv.MaGV
LEFT JOIN SinhVien sv ON da.MaSV = sv.MaSV
WHERE da.MaSV IS NOT NULL
ORDER BY da.NgayBatDau DESC;

-- 5. Xem t?t c? các yêu c?u ?ã duy?t/t? ch?i
SELECT 'DANH SÁCH YÊU C?U ?Ã DUY?T' AS Category;
SELECT 
    ycdk.MaYeuCau,
    ycdk.MaDeTai,
    da.TenDeTai,
    ycdk.MaSV,
    sv.HoTen AS TenSinhVien,
    ycdk.NgayGui,
    ycdk.TrangThai
FROM YeuCauDangKy ycdk
LEFT JOIN DoAn da ON ycdk.MaDeTai = da.MaDeTai
LEFT JOIN SinhVien sv ON ycdk.MaSV = sv.MaSV
WHERE ycdk.TrangThai IN ('Approved', 'Rejected')
ORDER BY ycdk.NgayGui DESC;

-- 6. Xem yêu c?u c?a 1 gi?ng viên c? th?
-- Thay 'GV001' b?ng mã gi?ng viên c?n xem
SELECT 'YÊU C?U C?A GI?NG VIÊN' AS Category;
SELECT 
    ycdk.MaYeuCau,
    ycdk.MaDeTai,
    da.TenDeTai,
    gv.HoTen AS TenGiangVien,
    ycdk.MaSV,
    sv.HoTen AS TenSinhVien,
    sv.Lop,
    sv.Email,
    ycdk.NgayGui,
    ycdk.TrangThai,
    ycdk.GhiChu
FROM YeuCauDangKy ycdk
LEFT JOIN DoAn da ON ycdk.MaDeTai = da.MaDeTai
LEFT JOIN GiangVien gv ON da.MaGV = gv.MaGV
LEFT JOIN SinhVien sv ON ycdk.MaSV = sv.MaSV
WHERE da.MaGV = 'GV001' AND ycdk.TrangThai = 'Pending'
ORDER BY ycdk.NgayGui DESC;

-- 7. RESET: Xóa t?t c? yêu c?u (ch? dùng trong quá trình test)
-- UNCOMMENT dòng d??i n?u mu?n xóa t?t c?
-- DELETE FROM YeuCauDangKy;
-- PRINT '?ã xóa t?t c? yêu c?u!';

-- 8. RESET: Xóa MaSV t? t?t c? DoAn (reset v? tr?ng thái ch?a có sinh viên)
-- UNCOMMENT dòng d??i n?u mu?n reset
-- UPDATE DoAn SET MaSV = NULL;
-- PRINT '?ã reset t?t c? DoAn v? tr?ng thái ch?a có sinh viên!';

-- 9. Ki?m tra tính toàn v?n d? li?u
SELECT 'TH?NG KÊ TÍNH TOÀN V?N' AS Category;
SELECT 
    (SELECT COUNT(*) FROM DoAn WHERE MaSV IS NULL) AS '?? tài ch?a có SV',
    (SELECT COUNT(*) FROM DoAn WHERE MaSV IS NOT NULL) AS '?? tài có SV',
    (SELECT COUNT(*) FROM YeuCauDangKy WHERE TrangThai = 'Pending') AS 'Yêu c?u ch? duy?t',
    (SELECT COUNT(*) FROM YeuCauDangKy WHERE TrangThai = 'Approved') AS 'Yêu c?u ?ã duy?t',
    (SELECT COUNT(*) FROM YeuCauDangKy WHERE TrangThai = 'Rejected') AS 'Yêu c?u b? t? ch?i';
