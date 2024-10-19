using System;
using System.Data.SqlClient;
using System.Text;

namespace Event_UEH
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int RoleId { get; set; }

        public static class Session
        {
            public static int CurrentUserId { get; set; }
        }

        // Đăng ký người dùng mới
        public static void Register()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan; // Màu chữ

            // Hiển thị tiêu đề với khung viền
            Console.WriteLine("=============================================");
            Console.WriteLine("                ĐĂNG KÝ TÀI KHOẢN            ");
            Console.WriteLine("=============================================");

            Console.ResetColor(); // Khôi phục lại màu sắc mặc định

            // Bước chọn loại tài khoản (Admin, Sinh viên, Tổ chức)
            int roleId = SelectRole();

            // Xóa màn hình sau khi chọn vai trò
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan; // Màu chữ

            // Hiển thị tiêu đề cho phần nhập thông tin người dùng
            Console.WriteLine("=============================================");
            Console.WriteLine("           NHẬP THÔNG TIN NGƯỜI DÙNG        ");
            Console.WriteLine("=============================================");
            Console.ResetColor(); // Khôi phục lại màu sắc mặc định

            // Bước nhập thông tin người dùng
            string email;
            while (true)
            {
                Console.Write("Nhập địa chỉ Email: ");
                email = Console.ReadLine();
                if (!UserExists(email)) // Kiểm tra xem email đã tồn tại chưa
                {
                    break;
                }
                Console.WriteLine("Email này đã được đăng ký. Vui lòng sử dụng email khác.");
            }

            Console.Write("Nhập tên đăng nhập: ");
            string username = Console.ReadLine();

            string password;
            string confirmPassword;
            while (true)
            {
                Console.Write("Nhập mật khẩu: ");
                password = ReadPassword();

                if (!IsValidPassword(password))
                {
                    Console.WriteLine("Mật khẩu phải có ít nhất 8 ký tự và chứa ít nhất một số. Vui lòng thử lại.");
                    continue; // Continue to prompt for password again
                }

                Console.Write("Nhập lại mật khẩu: ");
                confirmPassword = ReadPassword();

                if (password == confirmPassword)
                {
                    break; // Nếu hai mật khẩu khớp, thoát khỏi vòng lặp
                }
                else
                {
                    Console.WriteLine("Mật khẩu không khớp. Vui lòng thử lại.");
                }
            }

            Console.Write("Nhập họ tên: ");
            string fullName = Console.ReadLine();

            // Lưu người dùng vào cơ sở dữ liệu với RoleId tương ứng
            SaveUserToDatabase(username, password, email, fullName, roleId);
            Console.WriteLine("Đăng ký thành công! Nhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
        }

        private static int SelectRole()
        {
            string[] roles = { "Admin", "Sinh viên", "Tổ chức" };
            int currentSelection = 0;

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan; // Màu chữ

                // Hiển thị tiêu đề với khung viền
                Console.WriteLine("=============================================");
                Console.WriteLine("              CHỌN TÀI KHOẢN                 ");
                Console.WriteLine("=============================================");

                Console.ResetColor(); // Khôi phục lại màu sắc mặc định

                for (int i = 0; i < roles.Length; i++)
                {
                    if (i == currentSelection)
                    {
                        Console.ForegroundColor = ConsoleColor.Green; // Màu cho tùy chọn được chọn
                        Console.WriteLine($"> {roles[i]} <");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White; // Màu chữ bình thường
                        Console.WriteLine(roles[i]);
                    }
                }
                Console.ResetColor();

                // Nhận phím nhập từ người dùng
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    currentSelection = (currentSelection == 0) ? roles.Length - 1 : currentSelection - 1;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    currentSelection = (currentSelection == roles.Length - 1) ? 0 : currentSelection + 1;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    return currentSelection + 1; // Trả về RoleId tương ứng (1, 2, 3)
                }
            }
        }

        private static bool UserExists(string email)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private static void SaveUserToDatabase(string username, string password, string email, string fullName, int roleId)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = "INSERT INTO [Users] (Username, Password, Email, FullName, RoleId) VALUES (@Username, @Password, @Email, @FullName, @RoleId)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password); // Nên mã hóa mật khẩu trước khi lưu
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@FullName", fullName);
                    command.Parameters.AddWithValue("@RoleId", roleId);
                    command.ExecuteNonQuery();
                }
            }
        }

        private static User AuthenticateUser(string username, string password)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = "SELECT * FROM [Users] WHERE Username = @Username AND Password = @Password"; // Đảm bảo tên bảng chính xác
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password); // Nên mã hóa mật khẩu trước khi kiểm tra

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                Id = (int)reader["Id"],
                                Username = reader["Username"].ToString(),
                                Email = reader["Email"].ToString(),
                                FullName = reader["FullName"].ToString(),
                                RoleId = (int)reader["RoleId"]
                            };
                        }
                    }
                }
            }
            return null; // Trả về null nếu không tìm thấy người dùng
        }

        public static void Login()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan; // Màu chữ

            // Hiển thị tiêu đề với khung viền
            Console.WriteLine("=============================================");
            Console.WriteLine("                ĐĂNG NHẬP                    ");
            Console.WriteLine("=============================================");

            Console.ResetColor(); // Khôi phục lại màu sắc mặc định

            Console.Write("Nhập tên đăng nhập: ");
            string username = Console.ReadLine();

            Console.Write("Nhập mật khẩu: ");
            string password = ReadPassword(); // Sử dụng hàm đọc mật khẩu đã cập nhật

            User user = AuthenticateUser(username, password);
            if (user != null)
            {
                Console.WriteLine($"Đăng nhập thành công! Chào {user.FullName}!");

                // Lưu UserId của người dùng vào biến toàn cục
                Session.CurrentUserId = user.Id;

                // Gọi hàm để hiển thị giao diện tương ứng với vai trò
                ShowDashboard(GetRoleName(user.RoleId));
            }
            else
            {
                Console.WriteLine("Tên đăng nhập hoặc mật khẩu không đúng. Nhấn phím bất kỳ để tiếp tục...");
                Console.ReadKey();
            }
        }

        private static string GetRoleName(int roleId)
        {
            switch (roleId)
            {
                case 1:
                    return "Admin";
                case 2:
                    return "Sinh viên";
                case 3:
                    return "Tổ chức";
                default:
                    return "Khách"; // Trả về "Khách" nếu không có vai trò phù hợp
            }
        }

        public static void ShowDashboard(string role)
        {
            switch (role)
            {
                case "Admin":
                    Console.WriteLine("Giao diện Admin - Quản lý sự kiện và người dùng.");
                    Admin.ShowDashboard(); // Gọi đến giao diện Admin
                    break;
                case "Tổ chức":
                    Console.WriteLine("Giao diện Tổ chức - Thêm, sửa, xóa sự kiện.");
                    Organizer.ShowDashboard(); // Gọi đến giao diện Tổ chức
                    break;
                case "Sinh viên":
                    Console.WriteLine("Giao diện Sinh viên - Đăng ký sự kiện, đánh giá sự kiện.");
                    Student.ShowDashboard(); // Gọi đến giao diện Sinh viên
                    break;
                default:
                    Console.WriteLine("Vai trò không hợp lệ.");
                    break;
            }
        }

        // Hiển thị menu chính với lựa chọn điều hướng
        public static void ShowMainMenu()
        {
            string[] menuOptions = { "Đăng nhập", "Đăng ký tài khoản mới", "Thoát chương trình" };
            int currentSelection = 0; // Chỉ số tùy chọn hiện tại

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan; // Màu chữ

                // Hiển thị tiêu đề với khung viền
                Console.WriteLine("=============================================");
                Console.WriteLine("                 CHỌN CHỨC NĂNG              ");
                Console.WriteLine("=============================================");

                Console.ResetColor(); // Khôi phục lại màu sắc mặc định

                // Hiển thị các tùy chọn menu
                for (int i = 0; i < menuOptions.Length; i++)
                {
                    if (i == currentSelection) // Đánh dấu tùy chọn hiện tại
                    {
                        Console.ForegroundColor = ConsoleColor.Green; // Màu cho tùy chọn được chọn
                        Console.WriteLine($"> {menuOptions[i]} <");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White; // Màu chữ bình thường
                        Console.WriteLine(menuOptions[i]);
                    }
                }
                Console.ResetColor(); // Khôi phục lại màu sắc mặc định

                // Nhận phím nhập từ người dùng
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    currentSelection = (currentSelection == 0) ? menuOptions.Length - 1 : currentSelection - 1; // Quay về tùy chọn cuối nếu đang ở đầu
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    currentSelection = (currentSelection == menuOptions.Length - 1) ? 0 : currentSelection + 1; // Quay về đầu nếu đang ở cuối
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    switch (currentSelection)
                    {
                        case 0:
                            Login(); // Gọi phương thức đăng nhập
                            break;
                        case 1:
                            Register(); // Gọi phương thức đăng ký
                            break;
                        case 2:
                            Console.WriteLine("Tạm biệt! Nhấn phím bất kỳ để thoát...");
                            Console.ReadKey(); // Đợi người dùng nhấn phím
                            return; // Thoát chương trình
                    }
                }
            }
        }

        // Phương thức để đọc mật khẩu và hiển thị dấu *
        private static string ReadPassword()
        {
            StringBuilder password = new StringBuilder();
            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey(intercept: true);

                // Check if the key pressed is not one of the excluded keys
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
                    // Add the character to the password
                    password.Append(keyInfo.KeyChar);
                    Console.Write("*"); // Display asterisk
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    // Handle backspace
                    password.Remove(password.Length - 1, 1);
                    Console.Write("\b \b"); // Move cursor back, overwrite with space, move back again
                }
            } while (keyInfo.Key != ConsoleKey.Enter);

            Console.WriteLine(); // Move to the next line after pressing Enter
            return password.ToString();
        }

        // Kiểm tra tính hợp lệ của mật khẩu
        private static bool IsValidPassword(string password)
        {
            // Kiểm tra độ dài của mật khẩu
            if (password.Length < 8)
                return false;

            // Kiểm tra xem mật khẩu có chứa ít nhất một chữ số
            bool hasDigit = false;
            foreach (char c in password)
            {
                if (char.IsDigit(c))
                {
                    hasDigit = true;
                    break;
                }
            }

            return hasDigit; // Trả về true nếu có ít nhất một chữ số
        }
    }
}
