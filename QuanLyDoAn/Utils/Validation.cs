using System;
using System.Text.RegularExpressions;

namespace QuanLyDoAn.Utils
{
    public static class Validation
    {
        // Kiểm tra chuỗi rỗng hoặc null
        public static bool IsNullOrEmpty(string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        // Kiểm tra email hợp lệ
        public static bool IsValidEmail(string email)
        {
            if (IsNullOrEmpty(email)) return false;
            
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        // Kiểm tra số điện thoại hợp lệ (10-11 số)
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (IsNullOrEmpty(phoneNumber)) return false;
            
            string pattern = @"^[0-9]{10,11}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }

        // Kiểm tra mật khẩu (ít nhất 6 ký tự)
        public static bool IsValidPassword(string password)
        {
            return !IsNullOrEmpty(password) && password.Length >= 6;
        }

        // Kiểm tra mã sinh viên (8 ký tự số)
        public static bool IsValidStudentCode(string studentCode)
        {
            if (IsNullOrEmpty(studentCode)) return false;
            
            string pattern = @"^SV[0-9]{8}$";
            return Regex.IsMatch(studentCode, pattern);
        }

        // Kiểm tra mã giảng viên (bắt đầu bằng GV và theo sau là số)
        public static bool IsValidTeacherCode(string teacherCode)
        {
            if (IsNullOrEmpty(teacherCode)) return false;
            
            string pattern = @"^GV[0-9]+$";
            return Regex.IsMatch(teacherCode, pattern);
        }

        // Kiểm tra độ dài chuỗi
        public static bool IsValidLength(string value, int minLength, int maxLength)
        {
            if (IsNullOrEmpty(value)) return false;
            
            return value.Length >= minLength && value.Length <= maxLength;
        }

        // Kiểm tra số nguyên dương
        public static bool IsPositiveInteger(string value)
        {
            if (IsNullOrEmpty(value)) return false;
            
            return int.TryParse(value, out int result) && result > 0;
        }

        // Kiểm tra ngày tháng hợp lệ
        public static bool IsValidDate(string dateString)
        {
            return DateTime.TryParse(dateString, out _);
        }

        // Kiểm tra ngày bắt đầu phải trước ngày kết thúc
        public static bool IsValidDateRange(DateTime startDate, DateTime endDate)
        {
            return startDate < endDate;
        }

        // Kiểm tra tên đăng nhập (chỉ chứa chữ cái, số và dấu gạch dưới)
        public static bool IsValidUsername(string username)
        {
            if (IsNullOrEmpty(username)) return false;
            
            string pattern = @"^[a-zA-Z0-9_]{3,20}$";
            return Regex.IsMatch(username, pattern);
        }
    }
}