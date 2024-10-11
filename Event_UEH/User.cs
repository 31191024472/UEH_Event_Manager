using System;
using System.Data.SqlClient;

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

        // Đăng ký người dùng mới
        public static void RegisterUser()
        {
            Console.Clear();
            Console.WriteLine("=== Đăng ký tài khoản ===");

            Console.Write("Nhập email: ");
            string email = Console.ReadLine();

            // Kiểm tra xem email đã tồn tại hay chưa
            string checkQuery = "SELECT COUNT(*) FROM Users WHERE Email = @Email"; // Giả sử bảng Users có cột Email
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@Email", email);

                try
                {
                    connection.Open();
                    int count = (int)checkCommand.ExecuteScalar();
                    if (count > 0)
                    {
                        Console.WriteLine("Email này đã có người sở hữu. Vui lòng nhập email khác.");
                        Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
                        Console.ReadKey();
                        return; // Trở về mà không thực hiện đăng ký
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi: {ex.Message}");
                    return; // Trở về trong trường hợp lỗi
                }
            }

            // Nếu email chưa tồn tại, tiến hành nhập thêm thông tin khác
            Console.Write("Nhập mật khẩu: ");
            string password = Console.ReadLine();

            Console.Write("Nhập họ và tên: ");
            string fullName = Console.ReadLine();

            // Câu lệnh SQL để chèn tài khoản mới
            string insertQuery = "INSERT INTO Users (Email, Password, FullName) VALUES (@Email, @Password, @FullName)";
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@Email", email);
                insertCommand.Parameters.AddWithValue("@Password", password); // Mật khẩu nên được mã hóa
                insertCommand.Parameters.AddWithValue("@FullName", fullName);

                try
                {
                    connection.Open();
                    int result = insertCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Console.WriteLine("Đăng ký tài khoản thành công!");
                    }
                    else
                    {
                        Console.WriteLine("Đăng ký tài khoản thất bại.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi: {ex.Message}");
                }
            }

            Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
        }


        // Kiểm tra người dùng có tồn tại trong cơ sở dữ liệu không
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

        // Lưu người dùng vào cơ sở dữ liệu
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
                    command.Parameters.AddWithValue("@RoleId", roleId); // Lưu RoleId tùy theo loại tài khoản
                    command.ExecuteNonQuery();
                }
            }
        }

        // Xác thực người dùng
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

        // Đăng nhập người dùng
        public static void Login()
        {
            Console.Clear();
            Console.WriteLine("=== Đăng nhập ===");

            Console.Write("Nhập tên đăng nhập: ");
            string username = Console.ReadLine();

            Console.Write("Nhập mật khẩu: ");
            string password = Console.ReadLine();

            User user = AuthenticateUser(username, password);
            if (user != null)
            {
                Console.WriteLine($"Đăng nhập thành công! Chào {user.FullName}!");

                // Gọi hàm để hiển thị giao diện tương ứng với vai trò
                ShowDashboard(GetRoleName(user.RoleId));
            }
            else
            {
                Console.WriteLine("Tên đăng nhập hoặc mật khẩu không đúng. Nhấn phím bất kỳ để tiếp tục...");
                Console.ReadKey();
            }
        }

        // Phương thức để chuyển RoleId thành tên vai trò
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
                    // Thêm chức năng dành cho Admin
                    Admin.ShowDashboard(); // Gọi đến giao diện Admin
                    break;
                case "Tổ chức":
                    Console.WriteLine("Giao diện Tổ chức - Thêm, sửa, xóa sự kiện.");
                    // Thêm chức năng dành cho Tổ chức
                    Organizer.ShowDashboard(); // Gọi đến giao diện Tổ chức
                    break;
                case "Sinh viên":
                    Console.WriteLine("Giao diện Sinh viên - Đăng ký sự kiện, đánh giá sự kiện.");
                    // Thêm chức năng dành cho Sinh viên
                    Student.ShowDashboard(); // Gọi đến giao diện Sinh viên
                    break;
                default:
                    Console.WriteLine("Vai trò không hợp lệ.");
                    break;
            }
        }



        // Lưu người dùng vào cơ sở dữ liệu

    }
}
