﻿using System;
using System.Data.SqlClient;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using static Event_UEH.User;

namespace Event_UEH
{
    public class Admin
    {

        // Giao diện của tài khoản Admin
        private static string[] adminOptions = new[]
        {
    "👤 Quản lý người dùng",
    "📅 Quản lý sự kiện",
    "📊 Thống kê báo cáo",
    "🚪 Đăng xuất"
};

        private static int currentAdminSelection = 0; // Chỉ số lựa chọn hiện tại cho admin

        public static void ShowDashboard()
        {
            while (true) // Vòng lặp để giữ cho giao diện hiển thị cho đến khi người dùng chọn đăng xuất
            {
                Console.Clear();

                // Hiển thị chữ "UEH" bằng ký tự ASCII
                string ueh = @"
 _    _ ______ _    _ 
| |  | |  ____| |  | |
| |  | | |__  | |__| |
| |  | |  __| |  __  |
| |__| | |____| |  | |
 \____/|______|_|  |_|               
       ";
                Console.WriteLine(ueh);
                Console.WriteLine(new string('=', 60)); // Dòng phân cách

                Console.WriteLine("=== Giao diện Admin ===");
                Console.WriteLine("Chức năng:");
                for (int i = 0; i < adminOptions.Length; i++)
                {
                    if (i == currentAdminSelection)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan; // Đổi màu lựa chọn hiện tại
                        Console.WriteLine($"> {adminOptions[i]} <"); // Hiển thị lựa chọn hiện tại với dấu mũi tên
                        Console.ResetColor(); // Khôi phục màu sắc
                    }
                    else
                    {
                        Console.WriteLine($"  {adminOptions[i]}");
                    }
                }
                Console.WriteLine(new string('=', 60)); // Dòng phân cách

                // Kiểm tra phím được nhấn
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    currentAdminSelection = (currentAdminSelection > 0) ? currentAdminSelection - 1 : adminOptions.Length - 1; // Di chuyển lên
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    currentAdminSelection = (currentAdminSelection < adminOptions.Length - 1) ? currentAdminSelection + 1 : 0; // Di chuyển xuống
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    ExecuteAdminSelection(currentAdminSelection); // Thực hiện lựa chọn hiện tại
                }
            }
        }

        private static void ExecuteAdminSelection(int selection)
        {
            switch (selection)
            {
                case 0:
                    ManageUsers();
                    break;
                case 1:
                    QuanLySuKien();
                    break;
                case 2:
                    GenerateReport();
                    Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
                    Console.ReadKey();
                    break; // Quay lại vòng lặp chính
                case 3:
                    Console.WriteLine("Đăng xuất thành công!");
                    Program.MainMenu(); // Gọi hàm quay lại menu chính
                    return;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ. Nhấn phím bất kỳ để quay lại...");
                    Console.ReadKey();
                    ShowDashboard();
                    break;
            }
        }

        // Chức năng quản lý người dùng
        private static string[] userManagementOptions = new[]
        {
    "📋 Hiển thị danh sách người dùng",
    "➕ Thêm người dùng mới",
    "🗑️ Xóa người dùng",
    "✏️ Chỉnh sửa người dùng",
    "🔙 Quay lại"
};

        private static int currentUserSelection = 0; // Chỉ số lựa chọn hiện tại cho quản lý người dùng

        private static void ManageUsers()
        {
            while (true) // Vòng lặp để giữ cho giao diện hiển thị cho đến khi người dùng chọn quay lại
            {
                Console.Clear();
                Console.WriteLine("=== Quản lý người dùng ===");

                for (int i = 0; i < userManagementOptions.Length; i++)
                {
                    if (i == currentUserSelection)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan; // Đổi màu lựa chọn hiện tại
                        Console.WriteLine($"> {userManagementOptions[i]} <"); // Hiển thị lựa chọn hiện tại với dấu mũi tên
                        Console.ResetColor(); // Khôi phục màu sắc
                    }
                    else
                    {
                        Console.WriteLine($"  {userManagementOptions[i]}");
                    }
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    currentUserSelection = (currentUserSelection > 0) ? currentUserSelection - 1 : userManagementOptions.Length - 1; // Di chuyển lên
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    currentUserSelection = (currentUserSelection < userManagementOptions.Length - 1) ? currentUserSelection + 1 : 0; // Di chuyển xuống
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    ExecuteUserSelection(currentUserSelection); // Thực hiện lựa chọn hiện tại
                }
            }
        }

        private static void ExecuteUserSelection(int selection)
        {
            switch (selection)
            {
                case 0:
                    HienThiNguoiDUng();
                    Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
                    Console.ReadKey();
                    ManageUsers();
                    break;
                case 1:
                    ThemNguoiDung();
                    Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
                    Console.ReadKey();
                    ManageUsers();
                    break;
                case 2:
                    XoaNguoiDung();
                    Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
                    Console.ReadKey();
                    ManageUsers();
                    break;
                case 3:
                    ChinhSuaNguoiDung();
                    Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
                    Console.ReadKey();
                    ManageUsers();
                    break;
                case 4:
                    ShowDashboard(); // Quay lại dashboard
                    return;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ. Nhấn phím bất kỳ để quay lại...");
                    Console.ReadKey();
                    ManageUsers();
                    break;
            }
        }

        // Chức năng quản lý sự kiện
        private static string[] eventManagementOptions = new[]
        {
    "➕ Thêm sự kiện",
    "✏️ Sửa sự kiện",
    "🗑️ Xóa sự kiện",
    "📋 Hiển thị danh sách sự kiện",
    "🔙 Quay lại"
};

        private static int currentEventSelection = 0; // Chỉ số lựa chọn hiện tại cho quản lý sự kiện

        // Chức năng quản lý sự kiện
        private static void QuanLySuKien()
        {
            while (true) // Vòng lặp để giữ cho giao diện hiển thị cho đến khi người dùng chọn quay lại
            {
                Console.Clear();
                Console.WriteLine("=== Quản lý sự kiện ===");

                for (int i = 0; i < eventManagementOptions.Length; i++)
                {
                    if (i == currentEventSelection)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan; // Đổi màu lựa chọn hiện tại
                        Console.WriteLine($"> {eventManagementOptions[i]} <"); // Hiển thị lựa chọn hiện tại với dấu mũi tên
                        Console.ResetColor(); // Khôi phục màu sắc
                    }
                    else
                    {
                        Console.WriteLine($"  {eventManagementOptions[i]}");
                    }
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    currentEventSelection = (currentEventSelection > 0) ? currentEventSelection - 1 : eventManagementOptions.Length - 1; // Di chuyển lên
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    currentEventSelection = (currentEventSelection < eventManagementOptions.Length - 1) ? currentEventSelection + 1 : 0; // Di chuyển xuống
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    ExecuteEventSelection(currentEventSelection); // Thực hiện lựa chọn hiện tại
                }
            }
        }

        // Danh sách các sự lựa chọn của chức năng quản lý sự kiện
        private static void ExecuteEventSelection(int selection)
        {
            switch (selection)
            {
                case 0:
                    ThemSuKien();
                    Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
                    Console.ReadKey();
                    QuanLySuKien();
                    break;
                case 1:
                    ChinhSuaSuKien();
                    break;
                case 2:
                    XoaSuKien();
                    Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
                    Console.ReadKey();
                    QuanLySuKien();
                    break;
                case 3:
                    HienThiAllSuKien();
                    Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
                    Console.ReadKey();
                    QuanLySuKien();
                    break;
                case 4:
                    ShowDashboard(); // Quay lại dashboard
                    return;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ. Nhấn phím bất kỳ để quay lại...");
                    Console.ReadKey();
                    QuanLySuKien();
                    break;
            }
        }

        // Chức năng hiển thị người dùng
        private static void HienThiNguoiDUng()
        {
            Console.Clear();
            Console.WriteLine("=== Danh sách người dùng ===");

            string query = "SELECT Id, Username, RoleId FROM Users";

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["Id"]}, Tên: {reader["Username"]}, Vai trò: {reader["RoleId"]}");
                }
                reader.Close();
            }
        }
        // Chức năng thêm người dùng mới
        private static void ThemNguoiDung()
        {
            Console.Clear();
            Console.WriteLine("=== Thêm người dùng mới ===");
            string email;
            while (true)
            {
                Console.Write("Nhập địa chỉ Email: ");
                email = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(email))
                {
                    Console.WriteLine("Email không được để trống. Vui lòng nhập lại.");
                    continue;
                }

                if (IsValidEmail(email))
                {
                    if (!UserExists(email))
                    {
                        break;
                    }
                    Console.WriteLine("Email này đã được đăng ký. Vui lòng sử dụng email khác.");
                }
                else
                {
                    Console.WriteLine("Email không hợp lệ. Vui lòng nhập lại.");
                }
            }

            string username;
            while (true)
            {
                Console.Write("Nhập tên đăng nhập: ");
                username = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(username))
                {
                    Console.WriteLine("Tên đăng nhập không được để trống. Vui lòng nhập lại.");
                    continue;
                }

                if (!UsernameExists(username)) // Kiểm tra xem username đã tồn tại chưa
                {
                    break;
                }
                Console.WriteLine("Tên đăng nhập này đã được sử dụng. Vui lòng chọn tên khác.");
            }

            string password;
            string confirmPassword;
            while (true)
            {
                Console.Write("Nhập mật khẩu: ");
                password = ReadPassword();

                if (!IsValidPassword(password))
                {
                    Console.WriteLine("Mật khẩu phải có ít nhất 8 ký tự và chứa ít nhất một số. Vui lòng thử lại.");
                    continue;
                }

                Console.Write("Nhập lại mật khẩu: ");
                confirmPassword = ReadPassword();

                if (password == confirmPassword)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Mật khẩu không khớp. Vui lòng thử lại.");
                }
            }

            string fullName;
            while (true)
            {
                Console.Write("Nhập họ và tên: ");
                fullName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(fullName))
                {
                    break;
                }
                Console.WriteLine("Họ và tên không được để trống. Vui lòng nhập lại.");
            }

            Console.Write("Nhập vai trò (1 = Admin, 2 = Tổ chức, 3 = Sinh viên): ");
            int roleId;
            while (!int.TryParse(Console.ReadLine(), out roleId) || roleId < 1 || roleId > 3)
            {
                Console.WriteLine("Vai trò không hợp lệ. Vui lòng nhập lại (1 = Admin, 2 = Tổ chức, 3 = Sinh viên): ");
            }

            // Câu lệnh SQL thêm giá trị FullName vào bảng
            string query = "INSERT INTO Users (Username, FullName, Email, Password, RoleId) VALUES (@username, @fullName, @email, @password, @roleId)";

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@fullName", fullName);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@password", password);  // Mã hóa nếu cần
                command.Parameters.AddWithValue("@roleId", roleId);

                int result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    Console.WriteLine("Thêm người dùng mới thành công.");
                }
                else
                {
                    Console.WriteLine("Thêm người dùng thất bại.");
                }
            }
        }

        // Hàm kiểm tra định dạng email
        private static bool IsValidEmail(string email)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }
        public static bool IsValidPassword(string password)
        {
            if (password.Length < 8)
                return false;

            bool hasDigit = false;
            foreach (char c in password)
            {
                if (char.IsDigit(c))
                {
                    hasDigit = true;
                    break;
                }
            }

            return hasDigit;
        }

        public static string ReadPassword()
        {
            StringBuilder password = new StringBuilder();
            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey(intercept: true);

                if (keyInfo.Key != ConsoleKey.Enter &&
                    keyInfo.Key != ConsoleKey.Spacebar &&
                    keyInfo.Key != ConsoleKey.Backspace &&
                    keyInfo.Key != ConsoleKey.Delete &&
                    (keyInfo.Key < ConsoleKey.F1 || keyInfo.Key > ConsoleKey.F12) &&
                    keyInfo.Key != ConsoleKey.Escape &&
                    keyInfo.Key != ConsoleKey.UpArrow &&
                    keyInfo.Key != ConsoleKey.DownArrow &&
                    keyInfo.Key != ConsoleKey.LeftArrow &&
                    keyInfo.Key != ConsoleKey.RightArrow)
                {
                    password.Append(keyInfo.KeyChar);
                    Console.Write("*");
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password.Remove(password.Length - 1, 1);
                    Console.Write("\b \b");
                }
            } while (keyInfo.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password.ToString();
        }

        // Chức năng xóa người dùng
        private static void XoaNguoiDung()
        {
            Console.Clear();
            Console.WriteLine("=== Xóa người dùng ===");
            HienThiNguoiDUng();
            // Kiểm tra nhập ID người dùng là số
            int userId;
            while (true)
            {
                Console.Write("Nhập ID người dùng muốn xóa: ");
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out userId)) // Kiểm tra nếu người dùng nhập đúng số
                {
                    break; // Nếu đúng, thoát vòng lặp
                }
                else
                {
                    Console.WriteLine("ID người dùng không hợp lệ. Vui lòng nhập lại (chỉ nhập số).");
                }
            }

            string query = "DELETE FROM Users WHERE Id = @userId";

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);

                int result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    Console.WriteLine("Xóa người dùng thành công.");
                }
                else
                {
                    Console.WriteLine("Không tìm thấy người dùng với ID này.");
                }
            }
        }


        // Chức năng chỉnh sửa thông tin người dùng
        private static void ChinhSuaNguoiDung()
        {
            Console.Clear();
            Console.WriteLine("=== Chỉnh sửa người dùng ===");
            HienThiNguoiDUng();

            // Kiểm tra nhập ID người dùng là số và kiểm tra sự tồn tại trong cơ sở dữ liệu
            int userId;
            while (true)
            {
                Console.Write("Nhập ID người dùng muốn chỉnh sửa: ");
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out userId)) // Kiểm tra nếu người dùng nhập đúng số
                {
                    // Kiểm tra xem người dùng có tồn tại trong cơ sở dữ liệu không
                    using (SqlConnection connection = DatabaseConnection.GetConnection())
                    {
                        string checkQuery = "SELECT COUNT(*) FROM Users WHERE Id = @userId";
                        SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                        checkCommand.Parameters.AddWithValue("@userId", userId);

                        int userExists = (int)checkCommand.ExecuteScalar();
                        if (userExists > 0)
                        {
                            break; // Nếu người dùng tồn tại, thoát vòng lặp
                        }
                        else
                        {
                            Console.WriteLine("Không tìm thấy người dùng với ID này. Vui lòng nhập lại.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("ID người dùng không hợp lệ. Vui lòng nhập lại (chỉ nhập số).");
                }
            }

            Console.Write("Nhập tên người dùng mới (để trống nếu không thay đổi): ");
            string newUsername = Console.ReadLine();

            Console.Write("Nhập mật khẩu mới (để trống nếu không thay đổi): ");
            string newPassword = Console.ReadLine();  // Mã hóa mật khẩu nếu cần

            Console.Write("Nhập Email mới (để trống nếu không thay đổi): ");
            string newEmail = Console.ReadLine();

            Console.Write("Nhập họ và tên mới (để trống nếu không thay đổi): ");
            string newFullName = Console.ReadLine();

            // Kiểm tra nhập vai trò là số
            int newRoleId = -1;  // Giá trị mặc định nếu không thay đổi vai trò
            while (true)
            {
                Console.Write("Nhập vai trò mới (1 = Admin, 2 = Tổ chức, 3 = Sinh viên) (để trống nếu không thay đổi): ");
                string newRoleIdInput = Console.ReadLine();

                if (string.IsNullOrEmpty(newRoleIdInput)) // Nếu không nhập thì không thay đổi
                {
                    break;
                }
                else if (int.TryParse(newRoleIdInput, out newRoleId) && (newRoleId >= 1 && newRoleId <= 3)) // Kiểm tra vai trò hợp lệ
                {
                    break; // Nếu hợp lệ, thoát vòng lặp
                }
                else
                {
                    Console.WriteLine("Vai trò không hợp lệ. Vui lòng nhập 1, 2 hoặc 3.");
                }
            }

            // Xây dựng câu truy vấn cập nhật
            string query = "UPDATE Users SET ";
            bool hasUpdate = false;

            if (!string.IsNullOrEmpty(newUsername))
            {
                query += "Username = @username ";
                hasUpdate = true;
            }

            if (!string.IsNullOrEmpty(newPassword))
            {
                query += (hasUpdate ? ", " : "") + "Password = @password ";
                hasUpdate = true;
            }

            if (!string.IsNullOrEmpty(newEmail))
            {
                query += (hasUpdate ? ", " : "") + "Email = @Email ";
                hasUpdate = true;
            }

            if (!string.IsNullOrEmpty(newFullName))
            {
                query += (hasUpdate ? ", " : "") + "FullName = @FullName ";
                hasUpdate = true;
            }

            if (newRoleId != -1)
            {
                query += (hasUpdate ? ", " : "") + "RoleId = @roleId ";
                hasUpdate = true;
            }

            query += "WHERE Id = @userId";

            // Thực hiện câu lệnh cập nhật nếu có thay đổi
            if (hasUpdate)
            {
                using (SqlConnection connection = DatabaseConnection.GetConnection())
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@userId", userId);
                    if (!string.IsNullOrEmpty(newUsername)) command.Parameters.AddWithValue("@username", newUsername);
                    if (!string.IsNullOrEmpty(newPassword)) command.Parameters.AddWithValue("@password", newPassword);  // Mã hóa nếu cần
                    if (!string.IsNullOrEmpty(newEmail)) command.Parameters.AddWithValue("@Email", newEmail);
                    if (!string.IsNullOrEmpty(newFullName)) command.Parameters.AddWithValue("@FullName", newFullName);
                    if (newRoleId != -1) command.Parameters.AddWithValue("@roleId", newRoleId);

                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Console.WriteLine("Chỉnh sửa thông tin người dùng thành công.");
                    }
                    else
                    {
                        Console.WriteLine("Chỉnh sửa thất bại hoặc không có thay đổi.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Không có thay đổi nào được thực hiện.");
            }
        }

        //Chứ năng thêm sự kiện
        private static void ThemSuKien()
        {
            Console.Clear();
            Console.WriteLine("=== Thêm sự kiện mới ===");

            // Nhập tiêu đề sự kiện
            string title;
            while (true)
            {
                Console.Write("Nhập tiêu đề sự kiện: ");
                title = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(title))
                    break;
                Console.WriteLine("Tiêu đề sự kiện không được để trống. Vui lòng nhập lại.");
            }

            // Nhập mô tả sự kiện
            string description;
            while (true)
            {
                Console.Write("Nhập mô tả sự kiện: ");
                description = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(description))
                    break;
                Console.WriteLine("Mô tả sự kiện không được để trống. Vui lòng nhập lại.");
            }

            // Nhập địa điểm
            string location;
            while (true)
            {
                Console.Write("Nhập địa điểm: ");
                location = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(location))
                    break;
                Console.WriteLine("Địa điểm không được để trống. Vui lòng nhập lại.");
            }

            // Nhập ngày bắt đầu
            DateTime startDate;
            while (true)
            {
                Console.Write("Nhập ngày bắt đầu (dd/MM/yyyy): ");
                string startInput = Console.ReadLine();
                if (DateTime.TryParseExact(startInput, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out startDate))
                    break;
                Console.WriteLine("Ngày bắt đầu không hợp lệ. Vui lòng nhập lại.");
            }

            // Nhập ngày kết thúc
            DateTime endDate;
            while (true)
            {
                Console.Write("Nhập ngày kết thúc (dd/MM/yyyy): ");
                string endInput = Console.ReadLine();
                if (DateTime.TryParseExact(endInput, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out endDate))
                {
                    if (endDate >= startDate)
                        break;
                    else
                        Console.WriteLine("Ngày kết thúc phải sau ngày bắt đầu. Vui lòng nhập lại.");
                }
                else
                {
                    Console.WriteLine("Ngày kết thúc không hợp lệ. Vui lòng nhập lại.");
                }
            }

            // Lấy ID người tạo từ session
            int createdBy = Session.CurrentUserId;
            int organizerId = Session.CurrentUserId;

            // Câu lệnh SQL thêm sự kiện
            string query = "INSERT INTO Events (Title, Description, Location, StartDate, EndDate, CreatedBy, OrganizerId) VALUES (@title, @description, @location, @startDate, @endDate, @createdBy, @organizerId)";

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@description", description);
                command.Parameters.AddWithValue("@location", location);
                command.Parameters.AddWithValue("@startDate", startDate);
                command.Parameters.AddWithValue("@endDate", endDate);
                command.Parameters.AddWithValue("@createdBy", createdBy);
                command.Parameters.AddWithValue("@organizerId", organizerId);

                int result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    Console.WriteLine("Thêm sự kiện thành công.");
                }
                else
                {
                    Console.WriteLine("Thêm sự kiện thất bại.");
                }
            }
        }

        // Chức năng chính sửa sự kiện
        private static void ChinhSuaSuKien()
        {
            Console.Clear();
            Console.WriteLine("=== Sửa sự kiện ===");
            HienThiAllSuKien();
            Console.Write("Nhập ID hoặc tên sự kiện cần sửa: ");
            string input = Console.ReadLine();

            // Câu truy vấn SQL để lấy thông tin sự kiện
            string selectQuery = "SELECT * FROM Events WHERE Id = @eventId OR Title = @title";

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                int eventId;
                string currentTitle = string.Empty, currentDescription = string.Empty, currentLocation = string.Empty;
                DateTime currentStartDate = DateTime.MinValue, currentEndDate = DateTime.MinValue;

                // Kiểm tra xem input có phải là số (ID) hay không
                if (int.TryParse(input, out eventId))
                {
                    selectCommand.Parameters.AddWithValue("@eventId", eventId);
                    selectCommand.Parameters.AddWithValue("@title", DBNull.Value);
                }
                else
                {
                    selectCommand.Parameters.AddWithValue("@title", input);
                    selectCommand.Parameters.AddWithValue("@eventId", DBNull.Value);
                }

                using (SqlDataReader reader = selectCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Lưu thông tin sự kiện hiện tại vào biến
                        currentTitle = reader["Title"].ToString();
                        currentDescription = reader["Description"].ToString();
                        currentLocation = reader["Location"].ToString();
                        currentStartDate = (DateTime)reader["StartDate"];
                        currentEndDate = (DateTime)reader["EndDate"];

                        // Hiển thị thông tin sự kiện hiện tại
                        Console.WriteLine($"Tiêu đề: {currentTitle}");
                        Console.WriteLine($"Mô tả: {currentDescription}");
                        Console.WriteLine($"Địa điểm: {currentLocation}");
                        Console.WriteLine($"Ngày bắt đầu: {currentStartDate}");
                        Console.WriteLine($"Ngày kết thúc: {currentEndDate}");

                        // Yêu cầu nhập thông tin mới
                        Console.Write("Nhập tiêu đề mới (để trống nếu không thay đổi): ");
                        string newTitle = Console.ReadLine();
                        Console.Write("Nhập mô tả mới (để trống nếu không thay đổi): ");
                        string newDescription = Console.ReadLine();
                        Console.Write("Nhập địa điểm mới (để trống nếu không thay đổi): ");
                        string newLocation = Console.ReadLine();

                        Console.Write("Nhập ngày bắt đầu mới (dd/MM/yyyy) (để trống nếu không thay đổi): ");
                        string newStartDateInput = Console.ReadLine();
                        DateTime newStartDate;
                        if (string.IsNullOrEmpty(newStartDateInput))
                        {
                            newStartDate = currentStartDate;
                        }
                        else
                        {
                            if (!DateTime.TryParseExact(newStartDateInput, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out newStartDate))
                            {
                                Console.WriteLine("Ngày bắt đầu không hợp lệ. Giữ nguyên giá trị cũ.");
                                newStartDate = currentStartDate;
                            }
                        }

                        Console.Write("Nhập ngày kết thúc mới (dd/MM/yyyy) (để trống nếu không thay đổi): ");
                        string newEndDateInput = Console.ReadLine();
                        DateTime newEndDate;
                        if (string.IsNullOrEmpty(newEndDateInput))
                        {
                            newEndDate = currentEndDate;
                        }
                        else
                        {
                            if (!DateTime.TryParseExact(newEndDateInput, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out newEndDate))
                            {
                                Console.WriteLine("Ngày kết thúc không hợp lệ. Giữ nguyên giá trị cũ.");
                                newEndDate = currentEndDate;
                            }
                        }

                        // Câu lệnh SQL sửa sự kiện
                        string updateQuery = "UPDATE Events SET Title = @title, Description = @description, Location = @location, StartDate = @startDate, EndDate = @endDate WHERE Id = @eventId";

                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@title", string.IsNullOrEmpty(newTitle) ? currentTitle : newTitle);
                            updateCommand.Parameters.AddWithValue("@description", string.IsNullOrEmpty(newDescription) ? currentDescription : newDescription);
                            updateCommand.Parameters.AddWithValue("@location", string.IsNullOrEmpty(newLocation) ? currentLocation : newLocation);
                            updateCommand.Parameters.AddWithValue("@startDate", newStartDate);
                            updateCommand.Parameters.AddWithValue("@endDate", newEndDate);
                            updateCommand.Parameters.AddWithValue("@eventId", eventId);

                            int result = updateCommand.ExecuteNonQuery();
                            if (result > 0)
                            {
                                Console.WriteLine("Sửa sự kiện thành công.");
                            }
                            else
                            {
                                Console.WriteLine("Sửa sự kiện thất bại.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Không tìm thấy sự kiện với ID hoặc tên đã nhập.");
                    }
                }
            }
        }

        // Chức năng xóa sự kiện
        private static void XoaSuKien()
        {
            Console.Clear();
            Console.WriteLine("=== Xóa sự kiện ===");
            HienThiAllSuKien();
            Console.Write("Nhập ID sự kiện cần xóa: ");
            int eventId;
            if (int.TryParse(Console.ReadLine(), out eventId))
            {
                using (SqlConnection connection = DatabaseConnection.GetConnection())
                {
                    // Xóa các bản ghi liên quan trong bảng RegisteredEvents
                    string deleteRegisteredEventsQuery = "DELETE FROM RegisteredEvents WHERE EventId = @eventId";
                    using (SqlCommand deleteRegisteredEventsCommand = new SqlCommand(deleteRegisteredEventsQuery, connection))
                    {
                        deleteRegisteredEventsCommand.Parameters.AddWithValue("@eventId", eventId);
                        deleteRegisteredEventsCommand.ExecuteNonQuery();
                    }

                    // Xóa sự kiện
                    string deleteEventQuery = "DELETE FROM Events WHERE Id = @eventId";
                    using (SqlCommand deleteEventCommand = new SqlCommand(deleteEventQuery, connection))
                    {
                        deleteEventCommand.Parameters.AddWithValue("@eventId", eventId);
                        int result = deleteEventCommand.ExecuteNonQuery();
                        if (result > 0)
                        {
                            Console.WriteLine("Xóa sự kiện thành công.");
                        }
                        else
                        {
                            Console.WriteLine("Xóa sự kiện thất bại. Có thể không tìm thấy sự kiện với ID đã nhập.");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("ID không hợp lệ. Vui lòng nhập lại.");
            }
        }

        // Chức năng hiển thị sự kiện
        private static void HienThiAllSuKien()
        {
            Console.Clear();
            Console.WriteLine("=== Danh sách sự kiện ===");
            Console.WriteLine();

            string query = "SELECT * FROM Events";

            // Mở kết nối tới cơ sở dữ liệu
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("Không thể kết nối tới cơ sở dữ liệu.");
                    Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
                    Console.ReadKey();
                    QuanLySuKien();
                    return;
                }

                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Kiểm tra xem có sự kiện nào không
                        if (!reader.HasRows)
                        {
                            Console.WriteLine("Không có sự kiện nào được tìm thấy.");
                        }
                        else
                        {
                            // Hiển thị tiêu đề bảng
                            Console.WriteLine("{0,-5} {1,-30} {2,-20} {3,-20} {4,-30}", "ID", "Tiêu đề", "Ngày bắt đầu", "Ngày kết thúc", "Địa điểm");
                            Console.WriteLine(new string('-', 125)); // Đường kẻ phân cách

                            while (reader.Read())
                            {
                                // Hiển thị thông tin từng sự kiện
                                Console.WriteLine("{0,-5} {1,-30} {2,-20} {3,-20} {4,-30}",
                                    reader["Id"],
                                    reader["Title"],
                                    DateTime.Parse(reader["StartDate"].ToString()).ToString("dd/MM/yyyy HH:mm"),
                                    DateTime.Parse(reader["EndDate"].ToString()).ToString("dd/MM/yyyy HH:mm"),
                                    reader["Location"]);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Có lỗi xảy ra khi truy xuất sự kiện: {ex.Message}");
                }
            }

            Console.WriteLine(new string('-', 125)); // Đường kẻ phân cách
        }

        // Chức năng thống kê và báo cáo
        public static void GenerateReport()
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                if (connection != null)
                {
                    int totalEvents = GetTotalEvents(connection);
                    int upcomingEvents = GetUpcomingEvents(connection);
                    int totalRegistrations = GetTotalRegistrations(connection);
                    double averageRating = GetAverageRating(connection);
                    int totalRatings = GetTotalRatings(connection);
                    double participationRate = GetParticipationRate(totalEvents, totalRegistrations);

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("==== BÁO CÁO THỐNG KÊ ====");
                    Console.ResetColor();

                    Console.WriteLine($"Tổng số sự kiện: {FormatNumber(totalEvents)}");
                    Console.WriteLine($"Sự kiện sắp diễn ra: {FormatNumber(upcomingEvents)}");
                    Console.WriteLine($"Tổng số người đăng ký: {FormatNumber(totalRegistrations)}");
                    Console.WriteLine($"Số lượng đánh giá: {FormatNumber(totalRatings)}");
                    Console.WriteLine($"Đánh giá trung bình: {averageRating:F2}");
                    Console.WriteLine($"Tỷ lệ tham gia: {participationRate:F2}%");

                    Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
                    Console.ReadKey();
                    ShowDashboard();
                }
            }
        }

        // Phương thức để lấy tổng số sự kiện
        public static int GetTotalEvents(SqlConnection connection)
        {
            string query = "SELECT COUNT(*) FROM Events";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                return (int)command.ExecuteScalar();
            }
        }


        // Phương thức để lấy số sự kiện sắp diễn ra
        public static int GetUpcomingEvents(SqlConnection connection)
        {
            string query = "SELECT COUNT(*) FROM Events WHERE StartDate > GETDATE()";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                return (int)command.ExecuteScalar();
            }
        }

        // Phương thức để lấy tổng số người đăng ký
        public static int GetTotalRegistrations(SqlConnection connection)
        {
            string query = "SELECT COUNT(*) FROM RegisteredEvents";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                return (int)command.ExecuteScalar();
            }
        }

        // Phương thức để lấy đánh giá trung bình
        public static double GetAverageRating(SqlConnection connection)
        {
            string query = "SELECT AVG(CAST(Rating AS FLOAT)) FROM Rate WHERE Rating IS NOT NULL";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                object result = command.ExecuteScalar();
                return result != DBNull.Value && result != null ? Convert.ToDouble(result) : 0.0;
            }
        }

        // Phương thức để lấy tổng số đánh giá
        public static int GetTotalRatings(SqlConnection connection)
        {
            string query = "SELECT COUNT(*) FROM Rate";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                return (int)command.ExecuteScalar();
            }
        }

        // Phương thức để tính tỷ lệ tham gia
        public static double GetParticipationRate(int totalEvents, int totalRegistrations)
        {
            return totalEvents > 0 ? ((double)totalRegistrations / totalEvents) * 100 : 0;
        }

        // Phương thức định dạng số
        private static string FormatNumber(int number)
        {
            return string.Format("{0:N0}", number); // Định dạng số với dấu phân cách hàng nghìn
        }


    }
}
