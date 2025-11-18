-- Tạo bảng YeuCauDangKy
CREATE TABLE YeuCauDangKy (
    MaYeuCau INT PRIMARY KEY IDENTITY(1,1),
    MaDeTai VARCHAR(10) NOT NULL,
    MaSV VARCHAR(10) NOT NULL,
    NgayGui DATE NOT NULL,
    TrangThai NVARCHAR(20) NOT NULL, -- 'Pending', 'Approved', 'Rejected'
    GhiChu NVARCHAR(500),
    FOREIGN KEY (MaDeTai) REFERENCES DoAn(MaDeTai) ON DELETE CASCADE,
    FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV) ON DELETE CASCADE
);

-- Index để tăng tốc truy vấn
CREATE INDEX IX_YeuCauDangKy_MaDeTai ON YeuCauDangKy(MaDeTai);
CREATE INDEX IX_YeuCauDangKy_MaSV ON YeuCauDangKy(MaSV);
CREATE INDEX IX_YeuCauDangKy_TrangThai ON YeuCauDangKy(TrangThai);
