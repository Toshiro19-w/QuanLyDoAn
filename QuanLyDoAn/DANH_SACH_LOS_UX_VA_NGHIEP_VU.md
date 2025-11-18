# DANH SÁCH CÁC L?I UX VÀ NGHI?P V? C?N S?A

## ?? L?I QUAN TR?NG

### 1. ? L?I QUY?N H?N: Admin không xem ???c "Qu?n lý ?? án"
**File:** `MainForm.cs`, `AuthorizationHelper.cs`

**V?n ??:**
- Admin t?o menu "Qu?n lý ?? án" nh?ng không th? vào (b? khóa b?i `CheckPermission`)
- Admin mu?n qu?n lý t?t c? ?? án, nh?ng ch? có quy?n xem

**Code hi?n t?i:**
```csharp
// MainForm.cs - Admin menu
if (AuthorizationHelper.IsAdmin())
{
    AddMenuButton("Qu?n lý ?? án", yPosition, BtnQuanLyDoAn_Click);  // ? Nút xu?t hi?n
    ...
}

// BtnQuanLyDoAn_Click
if (!AuthorizationHelper.CheckPermission("QuanLyDoAn"))  // ? B? t? ch?i!
{
    AuthorizationHelper.ShowAccessDeniedMessage();
    return;
}
```

**Nguyên nhân:**
- Nút menu ???c hi?n th? nh?ng permission ki?m tra không cho phép vào

**S?a:**
- Cho Admin quy?n vào "Qu?n lý ?? án"

---

### 2. ? L?I NGHI?P V?: Sinh viên v?n th?y ?? tài ?ã ??ng ký
**File:** `DangKyDoAnControl.cs`

**V?n ??:**
```
1. SV ??ng ký ?? tài DA001 ? Tr?ng thái Pending
2. SV làm m?i danh sách ? V?N TH?Y DA001
```

**Nguyên nhân:**
- `LayDoAnChuaCoSinhVien()` ch? l?c `MaSv = NULL`
- Nh?ng SV v?n có th? th?y ?? tài này vì nó ch?a ???c duy?t
- SV không bi?t ?ã g?i yêu c?u hay ch?a

**S?a:**
- Thêm column "Tr?ng thái yêu c?u" ?? SV xem ???c yêu c?u c?a mình
- Tô màu khác ho?c ?ánh d?u nh?ng ?? tài ?ã ??ng ký

---

### 3. ? L?I NGHI?P V?: Admin "Qu?n lý ?? án" không có nút "S?a"
**File:** `QuanLyDoAnControl.cs`

**V?n ??:**
- Admin mu?n s?a info c?a ?? tài (tên, mô t?, v.v.)
- Nh?ng không có nút "S?a" trong admin view

**S?a:**
- Thêm nút "S?a" ?? admin có th? edit ?? tài (thông qua TaoDoAnForm)

---

### 4. ? L?I UX: Thông báo "Duy?t thành công" không clear form
**File:** `DuyetYeuCauControl.cs`

**V?n ??:**
- Sau khi duy?t, form v?n gi? data c?
- Ng??i dùng không bi?t ?ã duy?t xong hay ch?a

**S?a:**
- Clear form sau khi duy?t
- Hi?n th? message rõ ràng

---

### 5. ? L?I NGHI?P V?: Không ki?m tra "?ã t? ch?i hay ch?p nh?n"
**File:** `DuyetYeuCauControl.cs`

**V?n ??:**
```
Gi? s?:
- SV A ??ng ký DA001 ? Pending
- SV B ??ng ký DA001 ? Pending
- GV ch?p nh?n A
- B b? reject t? ??ng
- B không bi?t ???c t? ch?i hay ch?a
```

**S?a:**
- Thêm page ho?c tab "Yêu c?u ?ã x? lý" ?? GV xem l?ch s?

---

### 6. ? L?I UX: Sinh viên không th?y k?t qu? ??ng ký
**File:** `DangKyDoAnControl.cs`

**V?n ??:**
- SV ??ng ký ?? tài nh?ng không th?y tr?ng thái
- Không bi?t "Ch? duy?t", "?ã duy?t", hay "B? t? ch?i"

**S?a:**
- Thêm tab/danh sách "Yêu c?u c?a tôi" ?? SV theo dõi

---

### 7. ? L?I NGHI?P V?: DoAnGiangVienControl không cho phép xem
**File:** `DoAnGiangVienControl.cs`

**V?n ??:**
- Gi?ng viên vào "?? án c?a tôi"
- Nó hi?n th? gì? ?? tài c?a GV hay sinh viên ?ã nh?n?

**S?a:**
- Làm rõ: Hi?n th? danh sách t?t c? ?? tài c?a GV (bao g?m ?ã ???c duy?t)

---

### 8. ? L?I UX: Không có "Loading..." khi fetch data
**File:** T?t c? control

**V?n ??:**
- Khi load d? li?u, form b? "freeze"
- Ng??i dùng không bi?t ?ang load hay b? l?i

**S?a:**
- Thêm loading indicator

---

### 9. ? L?I UX: Không có "Không có d? li?u" message
**File:** T?t c? danh sách (DataGridView tr?ng)

**V?n ??:**
- Khi không có d? li?u, danh sách tr?ng r?ng
- Ng??i dùng không bi?t là th?c s? không có hay l?i load

**S?a:**
- Hi?n th? label: "Không có d? li?u"

---

### 10. ? L?I NGHI?P V?: Admin không th? t?o/s?a tài kho?n
**File:** `QuanLyTaiKhoanControl.cs`

**V?n ??:**
- Admin vào "Qu?n lý Tài kho?n"
- Nh?ng không có nút "Thêm", "S?a", "Xóa"

**S?a:**
- Implement CRUD ??y ??

---

## ?? L?I TRUNG BÌNH

### 11. L?I UX: Form TaoDoAn không có validation rõ ràng
**File:** `TaoDoAnForm.cs`

**V?n ??:**
- Khi nh?p sai, không có l?i validation rõ
- VD: Ngày b?t ??u > Ngày k?t thúc ? Error l?t v?t

**S?a:**
- Thêm highlight field l?i
- Message l?i chi ti?t

---

### 12. L?I UX: Không có "Xoá" button trên danh sách
**File:** `QuanLyDoAnControl.cs`

**V?n ??:**
- Admin mu?n xóa ?? tài (admin khóa)
- Nh?ng không có nút xóa

**S?a:**
- Nên có, nh?ng c?n xác nh?n + cascade delete

---

### 13. L?I UX: Dropdown "Lo?i ?? án" không load l?n ??u
**File:** `QuanLyDoAnControl.cs` - v?a s?a

**V?n ?? (?ã s?a):** ? Fixed

---

### 14. L?I UX: Không có "Refresh" button trên t?t c? screens
**File:** T?t c?

**S?a:**
- Thêm F5 ho?c btn "Làm m?i"

---

### 15. L?I UX: Sidebar buttons không highlight khi active
**File:** `MainForm.cs`

**V?n ??:**
- Khi click button, không bi?t button nào ?ang active
- Ng??i dùng confused

**S?a:**
- Highlight button hi?n t?i

---

## ?? L?I NH?

### 16. L?I UX: Font & layout không consistent
**S?a:**
- Unify font, color, size

---

### 17. L?I UX: Không có "Quay l?i" button
**S?a:**
- Breadcrumb ho?c back button

---

### 18. L?I UX: DataGridView columns không adjust width t? ??ng
**S?a:**
- AutoSizeMode = Fill

---

---

## ?? B?NG ?U TIÊN

| # | L?i | M?c ?? | ?nh h??ng | Fix |
|---|-----|-------|----------|-----|
| 1 | Admin không vào Qu?n lý ?? án | ?? | Cao | CheckPermission |
| 2 | SV th?y ?? tài ?ã ??ng ký | ?? | Cao | Add status column |
| 3 | Admin không th? S?a | ?? | Cao | Add Edit button |
| 4 | Duy?t không clear form | ?? | Trung | LoadData() after approve |
| 5 | SV không xem k?t qu? yêu c?u | ?? | Cao | Add "Requests" tab |
| 6 | DoAnGiangVien không rõ logic | ?? | Trung | Add documentation |
| 7 | Không có loading indicator | ?? | Trung | Add ProgressBar |
| 8 | Không có "empty data" message | ?? | Trung | Add label |
| 9 | Admin Qu?n lý Tài kho?n CRUD | ?? | Cao | Implement full CRUD |
| 10 | Form validation không rõ | ?? | Trung | Add field highlight |

---

## ? QUY TRÌNH FIX

1. **Fix l?i #1**: Admin quy?n truy c?p
2. **Fix l?i #2**: SV xem tr?ng thái yêu c?u
3. **Fix l?i #3**: Admin s?a ?? tài
4. **Fix l?i #5**: SV xem k?t qu? yêu c?u
5. **Fix l?i #7**: Loading indicator
6. **Fix l?i #8**: Empty data message
7. **Fix l?i #9**: Admin CRUD Tài kho?n
8. (Các l?i còn l?i tùy m?c ?? ?u tiên)

