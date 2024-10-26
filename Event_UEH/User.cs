using System;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;

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
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("=============================================");
            Console.WriteLine("                ĐĂNG KÝ TÀI KHOẢN            ");
            Console.WriteLine("=============================================");
            Console.ResetColor();

            int roleId = SelectRole();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=============================================");
            Console.WriteLine("           NHẬP THÔNG TIN NGƯỜI DÙNG        ");
            Console.WriteLine("=============================================");
            Console.ResetColor();

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
                Console.Write("Nhập họ tên: ");
                fullName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(fullName))
                {
                    Console.WriteLine("Họ tên không được để trống. Vui lòng nhập lại.");
                    continue;
                }
                break;
            }

            SaveUserToDatabase(username, password, email, fullName, roleId);
            Console.WriteLine("Đăng ký thành công! Nhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
        }

        // Phương thức kiểm xác định loại tài khoản
        private static int SelectRole()
        {
            // Chỉ hiển thị hai vai trò: "Sinh viên" và "Tổ chức"
            string[] roles = { "Sinh viên", "Tổ chức" };
            int currentSelection = 0;

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;

                Console.WriteLine("=============================================");
                Console.WriteLine("              CHỌN LOẠI TÀI KHOẢN             ");
                Console.WriteLine("=============================================");
                Console.ResetColor();

                for (int i = 0; i < roles.Length; i++)
                {
                    if (i == currentSelection)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"> {roles[i]} <");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(roles[i]);
                    }
                }
                Console.ResetColor();

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
                    // Trả về RoleId tương ứng (2 cho Sinh viên, 3 cho Tổ chức)
                    return currentSelection + 2;
                }
            }
        }

        // Phương Thức kiểm tra EMail đã tồn tại chưa 
        public static bool UserExists(string email)
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

        // Phương thức kiểm tra username đã tồn tại chưa
        public static bool UsernameExists(string username)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        // Phương thức lưu người dùng vào database 
        public static void SaveUserToDatabase(string username, string password, string email, string fullName, int roleId)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = "INSERT INTO [Users] (Username, Password, Email, FullName, RoleId) VALUES" +
                    " (@Username, @Password, @Email, @FullName, @RoleId)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password); 
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@FullName", fullName);
                    command.Parameters.AddWithValue("@RoleId", roleId);
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Console.WriteLine("Thêm người dùng mới thành công. Nhấn phím bất kỳ để tiếp tục...");
                    }
                    else
                    {
                        Console.WriteLine("Thêm người dùng thất bại.");
                    }
                }
            }
        }
        // Phương thức xác thực người dùng 
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
        
        // Chức năng đăng ký
        public static void Login()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("=============================================");
            Console.WriteLine("                ĐĂNG NHẬP                    ");
            Console.WriteLine("=============================================");
            Console.ResetColor();

            Console.Write("Nhập tên đăng nhập: ");
            string username = Console.ReadLine();

            Console.Write("Nhập mật khẩu: ");
            string password = ReadPassword();

            User user = AuthenticateUser(username, password);
            if (user != null)
            {
                Console.WriteLine($"Đăng nhập thành công! Chào {user.FullName}!");

                Session.CurrentUserId = user.Id;

                ShowDashboard(GetRoleName(user.RoleId));
            }
            else
            {
                Console.WriteLine("Tên đăng nhập hoặc mật khẩu không đúng. Nhấn phím bất kỳ để tiếp tục...");
                Console.ReadKey();
            }
        }

        // phương thức xác định quyền của người dùng bằng ID đã đăng nhập 
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
                    return "Khách";
            }
        }

        // Phương thức hiển thị giao diện của người dùng theo cái ROLE của tài khoản đó 
        public static void ShowDashboard(string role)
        {
            switch (role)
            {
                case "Admin":
                    Console.WriteLine("Giao diện Admin - Quản lý sự kiện và người dùng.");
                    Admin.ShowDashboard();
                    break;
                case "Tổ chức":
                    Console.WriteLine("Giao diện Tổ chức - Thêm, sửa, xóa sự kiện.");
                    Organizer.ShowDashboard();
                    break;
                case "Sinh viên":
                    Console.WriteLine("Giao diện Sinh viên - Đăng ký sự kiện, đánh giá sự kiện.");
                    Student.ShowDashboard();
                    break;
                default:
                    Console.WriteLine("Vai trò không hợp lệ.");
                    break;
            }
        }

        public static void ShowMainMenu()
        {
            string[] menuOptions = { "Đăng nhập", "Đăng ký tài khoản mới", "Thoát chương trình" };
            int currentSelection = 0;

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;

                Console.WriteLine("=============================================");
                Console.WriteLine("                 CHỌN CHỨC NĂNG              ");
                Console.WriteLine("=============================================");
                Console.ResetColor();

                for (int i = 0; i < menuOptions.Length; i++)
                {
                    if (i == currentSelection)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"> {menuOptions[i]} <");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(menuOptions[i]);
                    }
                }
                Console.ResetColor();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    currentSelection = (currentSelection == 0) ? menuOptions.Length - 1 : currentSelection - 1;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    currentSelection = (currentSelection == menuOptions.Length - 1) ? 0 : currentSelection + 1;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    switch (currentSelection)
                    {
                        case 0:
                            Login();
                            break;
                        case 1:
                            Register();
                            break;
                        case 2:
                            Console.WriteLine("Tạm biệt! Nhấn phím bất kỳ để thoát...");
                            Console.ReadKey();
                            return;
                    }
                }
            }
        }

        // Phương thức đọc pass word bỏ qua các nút đặc biệt 
        private static string ReadPassword()
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

        // Phương thức kiểm tra password có đúng yêu cầu chưa 
        private static bool IsValidPassword(string password)
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

        // Phương thức kiểm tra Email có đúng yêu cầu chưa 
        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            // Sử dụng biểu thức chính quy để kiểm tra tính hợp lệ của email
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}
