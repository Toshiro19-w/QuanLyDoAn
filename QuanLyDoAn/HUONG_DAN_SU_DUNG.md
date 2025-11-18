# HƯỚNG DẪN SỬ DỤNG QUY TRÌNH TẠO VÀ ĐĂNG KÝ ĐỒ ÁN

## 1. CÀI ĐẶT DATABASE

Chạy script SQL sau để tạo bảng YeuCauDangKy:

```sql
-- File: YeuCauDangKy.sql
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
```

## 2. QUY TRÌNH SỬ DỤNG

### BƯỚC 1: Giảng viên tạo đề tài

1. Đăng nhập với tài khoản giảng viên
2. Vào màn hình "Đồ án của tôi"
3. Click nút **"Tạo đề tài mới"**
4. Nhập thông tin:
   - Mã đề tài (bắt buộc)
   - Tên đề tài (bắt buộc)
   - Mô tả
   - Loại đồ án
   - Kỳ học
   - Chuyên ngành
   - Trạng thái
   - Ngày bắt đầu/kết thúc
5. Click **"Lưu"**
6. Đề tài được tạo với MaSV = NULL (chưa có sinh viên)

### BƯỚC 2: Sinh viên đăng ký đề tài

1. Đăng nhập với tài khoản sinh viên
2. Thêm menu/control **"Đăng ký đồ án"** (DangKyDoAnControl)
3. Xem danh sách đề tài chưa có sinh viên
4. Chọn đề tài muốn đăng ký
5. Nhập ghi chú (tùy chọn)
6. Click **"Đăng ký"**
7. Yêu cầu được gửi với trạng thái "Pending"

### BƯỚC 3: Giảng viên duyệt yêu cầu

1. Đăng nhập với tài khoản giảng viên
2. Vào màn hình "Đồ án của tôi"
3. Click nút **"Duyệt yêu cầu"**
4. Xem danh sách yêu cầu đăng ký:
   - Thông tin đề tài
   - Thông tin sinh viên (Họ tên, Mã SV, Lớp, Email)
   - Ngày gửi
   - Ghi chú
5. Chọn yêu cầu và click:
   - **"Chấp nhận"**: Gán sinh viên vào đồ án, từ chối các yêu cầu khác cho đề tài này
   - **"Từ chối"**: Từ chối yêu cầu, đề tài vẫn mở cho sinh viên khác

## 3. CÁC FILE ĐÃ TẠO

### Entities
- `Model/Entities/YeuCauDangKy.cs` - Entity yêu cầu đăng ký

### Controllers
- `Controller/DangKyDoAnController.cs` - Xử lý logic tạo đồ án, đăng ký, duyệt

### Views
- `View/TaoDoAnForm.cs` - Form tạo đề tài (Giảng viên)
- `View/TaoDoAnForm.Designer.cs`
- `View/DangKyDoAnControl.cs` - Control đăng ký đề tài (Sinh viên)
- `View/DangKyDoAnControl.Designer.cs`
- `View/DuyetYeuCauControl.cs` - Control duyệt yêu cầu (Giảng viên)
- `View/DuyetYeuCauControl.Designer.cs`

### Database
- `YeuCauDangKy.sql` - Script tạo bảng

## 4. TÍCH HỢP VÀO MAINFORM

### Cho Sinh viên:
Thêm menu item trong MainForm:

```csharp
var menuDangKy = new ToolStripMenuItem("Đăng ký đồ án");
menuDangKy.Click += (s, e) => LoadControl(new DangKyDoAnControl());
// Thêm vào menu sinh viên
```

### Cho Giảng viên:
Đã tích hợp sẵn 2 nút trong DoAnGiangVienControl:
- "Tạo đề tài mới" - Mở form tạo đề tài
- "Duyệt yêu cầu" - Chuyển sang màn hình duyệt yêu cầu

## 5. LƯU Ý

- Sinh viên chỉ thấy đề tài chưa có sinh viên (MaSV = NULL)
- Mỗi sinh viên có thể gửi nhiều yêu cầu cho các đề tài khác nhau
- Không thể gửi yêu cầu trùng lặp cho cùng 1 đề tài
- Khi chấp nhận yêu cầu, tất cả yêu cầu khác cho đề tài đó sẽ tự động bị từ chối
- Giảng viên chỉ thấy yêu cầu cho đề tài của mình
