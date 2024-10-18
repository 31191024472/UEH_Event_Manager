using System;
using System.Data.SqlClient;
using System.Media;

namespace Event_UEH
{
    public class Admin
    {
        public static void ShowDashboard()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan; // Chọn màu chữ là Cyan
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
            Console.WriteLine("1. Quản lý người dùng");
            Console.WriteLine("2. Quản lý sự kiện");
            Console.WriteLine("3. Thống kê báo cáo");
            Console.WriteLine("4. Đăng xuất");
            Console.WriteLine(new string('=', 60)); // Dòng phân cách
            Console.Write("Nhập lựa chọn: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ManageUsers();
                    break;
                case "2":
                    ManageEvents();
                    break;
                case "3":
                    GenerateReport();
                    break;
                case "4":
                    Console.WriteLine("Đăng xuất thành công!");
                    break;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ. Nhấn phím bất kỳ để quay lại...");
                    Console.ReadKey();
                    ShowDashboard();
                    break;
            }
        }


        private static void ManageUsers()
        {
            Console.Clear();
            Console.WriteLine("=== Quản lý người dùng ===");
            Console.WriteLine("1. Hiển thị danh sách người dùng");
            Console.WriteLine("2. Thêm người dùng mới");
            Console.WriteLine("3. Xóa người dùng");
            Console.WriteLine("4. Chỉnh sửa người dùng");
            Console.WriteLine("5. Quay lại");
            Console.Write("Nhập lựa chọn: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayUsers();
                    break;
                case "2":
                    AddUser();
                    break;
                case "3":
                    DeleteUser();
                    break;
                case "4":
                    EditUser();
                    break;
                case "5":
                    ShowDashboard();
                    break;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ. Nhấn phím bất kỳ để quay lại...");
                    Console.ReadKey();
                    ManageUsers();
                    break;
            }
        }

        private static void DisplayUsers()
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

            Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
            ManageUsers();
        }
        private static void AddUser()
        {
            Console.Clear();
            Console.WriteLine("=== Thêm người dùng mới ===");

            Console.Write("Nhập tên người dùng: ");
            string username = Console.ReadLine();

            Console.Write("Nhập họ và tên: ");
            string fullName = Console.ReadLine();

            Console.Write("Nhập email: ");
            string email = Console.ReadLine();

            Console.Write("Nhập mật khẩu: ");
            string password = Console.ReadLine();  // Trong thực tế nên mã hóa mật khẩu

            Console.Write("Nhập vai trò (1 = Admin, 2 = Tổ chức, 3 = Sinh viên): ");
            int roleId = int.Parse(Console.ReadLine());

            // Câu lệnh SQL thêm giá trị FullName vào bảng
            string query = "INSERT INTO Users (Username, FullName, Email, Password, RoleId) VALUES (@username, @fullName, @email, @password, @roleId)";

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@fullName", fullName);  // Thêm giá trị FullName
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

            Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
            ManageUsers();
        }


        private static void DeleteUser()
        {
            Console.Clear();
            Console.WriteLine("=== Xóa người dùng ===");

            Console.Write("Nhập ID người dùng muốn xóa: ");
            int userId = int.Parse(Console.ReadLine());

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

            Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
            ManageUsers();
        }
        private static void EditUser()
        {
            Console.Clear();
            Console.WriteLine("=== Chỉnh sửa người dùng ===");

            Console.Write("Nhập ID người dùng muốn chỉnh sửa: ");
            int userId = int.Parse(Console.ReadLine());

            Console.Write("Nhập tên người dùng mới (để trống nếu không thay đổi): ");
            string newUsername = Console.ReadLine();

            Console.Write("Nhập mật khẩu mới (để trống nếu không thay đổi): ");
            string newPassword = Console.ReadLine();  // Mã hóa mật khẩu nếu cần

            Console.Write("Nhập vai trò mới (1 = Admin, 2 = Tổ chức, 3 = Sinh viên) (để trống nếu không thay đổi): ");
            string newRoleIdInput = Console.ReadLine();
            int newRoleId = string.IsNullOrEmpty(newRoleIdInput) ? -1 : int.Parse(newRoleIdInput);

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

            if (newRoleId != -1)
            {
                query += (hasUpdate ? ", " : "") + "RoleId = @roleId ";
                hasUpdate = true;
            }

            query += "WHERE Id = @userId";

            if (hasUpdate)
            {
                using (SqlConnection connection = DatabaseConnection.GetConnection())
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@userId", userId);
                    if (!string.IsNullOrEmpty(newUsername)) command.Parameters.AddWithValue("@username", newUsername);
                    if (!string.IsNullOrEmpty(newPassword)) command.Parameters.AddWithValue("@password", newPassword);  // Mã hóa nếu cần
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

            Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
            ManageUsers();
        }


        private static void ManageEvents()
        {
            Console.Clear();
            Console.WriteLine("=== Quản lý sự kiện ===");
            Console.WriteLine("1. Thêm sự kiện");
            Console.WriteLine("2. Sửa sự kiện");
            Console.WriteLine("3. Xóa sự kiện");
            Console.WriteLine("4. Hiển thị danh sách sự kiện");
            Console.WriteLine("5. Quay lại");
            Console.Write("Nhập lựa chọn: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddEvent();
                    break;
                case "2":
                    EditEvent();
                    break;
                case "3":
                    DeleteEvent();
                    break;
                case "4":
                    DisplayEvents();
                    break;
                case "5":
                    ShowDashboard();
                    break;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ. Nhấn phím bất kỳ để quay lại...");
                    Console.ReadKey();
                    ManageEvents();
                    break;
            }
        }
        private static void AddEvent()
        {
            Console.Clear();
            Console.WriteLine("=== Thêm sự kiện mới ===");

            Console.Write("Nhập tiêu đề sự kiện: ");
            string title = Console.ReadLine();

            Console.Write("Nhập mô tả sự kiện: ");
            string description = Console.ReadLine();

            Console.Write("Nhập địa điểm: ");
            string location = Console.ReadLine();

            Console.Write("Nhập ngày bắt đầu (dd/MM/yyyy): ");
            DateTime startDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

            Console.Write("Nhập ngày kết thúc (dd/MM/yyyy): ");
            DateTime endDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

            Console.Write("Nhập ID người tạo sự kiện: ");
            int createdBy = int.Parse(Console.ReadLine());

            Console.Write("Nhập ID tổ chức (người tổ chức): ");
            int organizerId = int.Parse(Console.ReadLine());

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

            Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
            ManageEvents();
        }

        private static void EditEvent()
        {
            Console.Clear();
            Console.WriteLine("=== Sửa sự kiện ===");
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

            Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
            ManageEvents();
        }



        private static void DeleteEvent()
        {
            Console.Clear();
            Console.WriteLine("=== Xóa sự kiện ===");
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

            Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
            ManageEvents();
        }


        private static void DisplayEvents()
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
                    ManageEvents();
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
            Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
            ManageEvents();
        }

        private static void ViewReports()
        {
            Console.WriteLine("Thống kê báo cáo - Chức năng sẽ được thêm sau.");
            Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
            ShowDashboard();
        }
        public static void GenerateReport()
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                if (connection != null)
                {
                    int totalEvents = GetTotalEvents(connection);
                    int ongoingEvents = GetOngoingEvents(connection);
                    int finishedEvents = GetFinishedEvents(connection);
                    int totalRegistrations = GetTotalRegistrations(connection);
                    double averageRating = GetAverageRating(connection);

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("==== BÁO CÁO THỐNG KÊ ====");
                    Console.ResetColor();

                    Console.WriteLine($"Tổng số sự kiện: {FormatNumber(totalEvents)}");
                    Console.WriteLine($"Sự kiện đang diễn ra: {FormatNumber(ongoingEvents)}");
                    Console.WriteLine($"Sự kiện đã kết thúc: {FormatNumber(finishedEvents)}");
                    Console.WriteLine($"Tổng số người đăng ký: {FormatNumber(totalRegistrations)}");
                    Console.WriteLine($"Đánh giá trung bình: {averageRating:F2}");

                    Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
                    Console.ReadKey();
                    ShowDashboard();
                }
            }
        }
    

        private static string FormatNumber(int number)
        {
            return string.Format("{0:N0}", number); // Định dạng số với dấu phân cách hàng nghìn
        }
        public static int GetTotalEvents(SqlConnection connection)
        {
            string query = "SELECT COUNT(*) FROM Events";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                return (int)command.ExecuteScalar();
            }
        }

        // Phương thức để lấy số sự kiện đang diễn ra
        public static int GetOngoingEvents(SqlConnection connection)
        {
            string query = "SELECT COUNT(*) FROM RegisteredEvents WHERE Status = 'Đã đăng ký'";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                return (int)command.ExecuteScalar();
            }
        }

        // Phương thức để lấy số sự kiện đã kết thúc
        public static int GetFinishedEvents(SqlConnection connection)
        {
            // Truy vấn đếm số lượng sự kiện đã bị xóa trong bảng Trash
            string query = "SELECT COUNT(*) FROM Trash";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                object result = command.ExecuteScalar();

                // Kiểm tra nếu kết quả là null hoặc không có giá trị thì trả về 0, ngược lại trả về số lượng
                return result != DBNull.Value && result != null ? Convert.ToInt32(result) : 0;
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

    }
}
