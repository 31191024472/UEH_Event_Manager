using Newtonsoft.Json.Linq;
using System;
using System.Data.SqlClient;
using static Event_UEH.User;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;


namespace Event_UEH
{
    public class Student
    {
        private static string[] options = new[]
        {
           "📅 Đăng ký sự kiện",
           "⭐ Đánh giá sự kiện",
           "📜 Xem danh sách sự kiện đã đăng ký",
           "❌ Hủy đăng ký sự kiện",
           "🔍 Tìm kiếm sự kiện",
           "🎉 Hiển thị toàn bộ sự kiện",
           "🛠️ Cập nhật thông tin tài khoản",
           "🎮 Chơi game giải trí",
           "🌤️ Xem thời tiết",
           "🚪 Đăng xuất"
        };
        private static int currentSelection = 0; // Chỉ số lựa chọn hiện tại

        // Sử dụng Session.CurrentUserId để lấy ID người dùng hiện tại
        public static void ShowDashboard()
        {
            while (true) // Vòng lặp để giữ cho giao diện hiển thị cho đến khi người dùng chọn đăng xuất
            {
                Console.Clear();
                Console.WriteLine("=== Giao diện Sinh viên ===");
                Console.WriteLine("Chức năng:");
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == currentSelection)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan; // Đổi màu lựa chọn hiện tại
                        Console.WriteLine($"> {options[i]} <"); // Hiển thị lựa chọn hiện tại với dấu mũi tên
                        Console.ResetColor(); // Khôi phục màu sắc
                    }
                    else
                    {
                        Console.WriteLine($"  {options[i]}");
                    }
                }

                // Kiểm tra phím được nhấn
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    currentSelection = (currentSelection > 0) ? currentSelection - 1 : options.Length - 1; // Di chuyển lên
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    currentSelection = (currentSelection < options.Length - 1) ? currentSelection + 1 : 0; // Di chuyển xuống
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    ExecuteSelection(currentSelection); // Thực hiện lựa chọn hiện tại
                }
            }
        }

        private static int gameSelection = 0; // Chỉ số lựa chọn trò chơi hiện tại
        private static string[] gameOptions = new[]
        {
    "🎮 Đường Lên Đỉnh UEH",
    "🎮 Trò Chơi Con Rắn"
};
        private static void ExecuteSelection(int selection)
        {
            switch (selection)
            {
                case 0:
                    RegisterEvent();
                    Console.ReadKey();
                    break;
                case 1:
                    EvaluateEvent();
                    Console.ReadKey();
                    break; // Quay lại vòng lặp chính
                case 2:
                    ViewRegisteredEvents();
                    Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
                    Console.ReadKey();
                    break; // Quay lại vòng lặp chính
                case 3:
                    CancelRegistration();
                    Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
                    Console.ReadKey();
                    ShowDashboard();
                    break; // Quay lại vòng lặp chính
                case 4:
                    SearchEvents();
                    Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
                    Console.ReadKey();
                    break; // Quay lại vòng lặp chính
                case 5:
                    DisplayAllEvents();
                    Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
                    Console.ReadKey();
                    break; // Quay lại vòng lặp chính
                case 6:
                    UpdateAccount(); // Gọi phương thức cập nhật thông tin tài khoản
                    break; // Quay lại vòng lặp chính
                case 7:
                    SelectGame();
                    break; // Quay lại vòng lặp chính
                case 8:
                    Weather();
                    break;
                case 9:
                    Console.WriteLine("Đăng xuất thành công!");
                    Program.MainMenu();
                    return;
                    break;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ. Nhấn phím bất kỳ để quay lại...");
                    Console.ReadKey();
                    ShowDashboard();
                    break;
            }
        }
        private static void SelectGame()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Chọn trò chơi ===");
                for (int i = 0; i < gameOptions.Length; i++)
                {
                    if (i == gameSelection)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan; // Đổi màu lựa chọn hiện tại
                        Console.WriteLine($"> {gameOptions[i]} <"); // Hiển thị lựa chọn hiện tại với dấu mũi tên
                        Console.ResetColor(); // Khôi phục màu sắc
                    }
                    else
                    {
                        Console.WriteLine($"  {gameOptions[i]}");
                    }
                }

                // Kiểm tra phím được nhấn
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    gameSelection = (gameSelection > 0) ? gameSelection - 1 : gameOptions.Length - 1; // Di chuyển lên
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    gameSelection = (gameSelection < gameOptions.Length - 1) ? gameSelection + 1 : 0; // Di chuyển xuống
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    ExecuteGameSelection(gameSelection); // Thực hiện lựa chọn trò chơi
                    return; // Trở về màn hình chính sau khi chơi
                }
            }
        }

        private static void ExecuteGameSelection(int gameSelection)
        {
            switch (gameSelection)
            {
                case 0:
                    DuongLenDinhUEH.ChoiTroChoi();
                    break;
                case 1:
                    Console.Clear();
                    QuanLySuKien_UEH.TroChoiRan.BatDauTroChoiRan();
                    break;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ. Nhấn phím bất kỳ để quay lại...");
                    Console.ReadKey();
                    break;
            }

            Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
        }
        // Chức năng đánh giá sự kiện
        private static void EvaluateEvent()
        {
            Console.Clear();
            Console.WriteLine("=== Đánh giá sự kiện ===");

            int eventId;
            while (true)
            {
                Console.Write("Nhập ID sự kiện để đánh giá: ");
                if (!int.TryParse(Console.ReadLine(), out eventId))
                {
                    Console.WriteLine("ID sự kiện không hợp lệ. Vui lòng thử lại.");
                    return; // Quay lại vòng lặp để nhập lại ID
                }

                // Kiểm tra sự tồn tại của sự kiện
                string checkQuery = "SELECT COUNT(1) FROM Events WHERE Id = @eventId";
                using (SqlConnection connection = DatabaseConnection.GetConnection())
                {
                    using (SqlCommand command = new SqlCommand(checkQuery, connection))
                    {
                        command.Parameters.AddWithValue("@eventId", eventId);
                        if ((int)command.ExecuteScalar() == 0)
                        {
                            Console.WriteLine("ID sự kiện không tồn tại. Vui lòng thử lại.");
                            continue; // Quay lại vòng lặp nếu sự kiện không tồn tại
                        }
                    }
                }
                break; // Thoát vòng lặp nếu sự kiện tồn tại
            }

            // Nhập điểm đánh giá
            int rating;
            while (true)
            {
                Console.Write("Nhập điểm đánh giá (1-5): ");
                if (!int.TryParse(Console.ReadLine(), out rating) || rating < 1 || rating > 5)
                {
                    Console.WriteLine("Điểm đánh giá không hợp lệ. Vui lòng thử lại.");
                }
                else
                {
                    break; // Thoát vòng lặp nếu điểm hợp lệ
                }
            }

            // Nhập ghi chú
            Console.Write("Nhập ghi chú (bỏ qua nếu không có): ");
            string content = Console.ReadLine();

            string insertQuery = @"
    INSERT INTO Rate (UserId, EventId, Rating, Content) 
    VALUES (@userId, @eventId, @rating, @content)";

            using (SqlConnection insertConnection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand insertCommand = new SqlCommand(insertQuery, insertConnection))
                {
                    insertCommand.Parameters.AddWithValue("@userId", Session.CurrentUserId);
                    insertCommand.Parameters.AddWithValue("@eventId", eventId);
                    insertCommand.Parameters.AddWithValue("@rating", rating);
                    insertCommand.Parameters.AddWithValue("@content", (object)content ?? DBNull.Value); // Nếu không có ghi chú, sử dụng NULL

                    try
                    {
                        int result = insertCommand.ExecuteNonQuery();
                        Console.WriteLine(result > 0 ? "Đánh giá sự kiện thành công." : "Đánh giá sự kiện thất bại.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi: {ex.Message}");
                    }
                }
            }
            Console.WriteLine("Cảm ơn bạn đã đánh giá! Nhấn phím bất kỳ để quay lại...");
            Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
            ShowDashboard();
        }


        // Chức năng hiển thị toàn bộ sự kiện
        private static void DisplayAllEvents()
        {
            Console.Clear();
            Console.WriteLine("=== Tất cả sự kiện ===");
            Console.WriteLine(new string('=', 30)); // Dòng phân cách

            // Cập nhật truy vấn SQL để lấy thêm thông tin sự kiện
            string selectQuery = "SELECT Id, Title, Description, StartDate, EndDate, Location, CreatedBy FROM Events";
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                {
                    try
                    {
                        SqlDataReader reader = selectCommand.ExecuteReader();
                        while (reader.Read())
                        {
                            // Hiển thị thông tin của sự kiện
                            Console.WriteLine($"ID: {reader["Id"]}");
                            Console.WriteLine($"Tiêu đề: {reader["Title"]}");
                            Console.WriteLine($"Mô tả: {reader["Description"]}");
                            Console.WriteLine($"Ngày bắt đầu: {reader["StartDate"]}");
                            Console.WriteLine($"Ngày kết thúc: {reader["EndDate"]}");
                            Console.WriteLine($"Địa điểm: {reader["Location"]}");

                            // Lấy tên người tổ chức
                            int createdBy = (int)reader["CreatedBy"]; // lấy ID của người tạo sự kiện
                            string organizerName = GetOrganizerName(createdBy);
                            Console.WriteLine($"Người tổ chức: {organizerName}");

                            Console.WriteLine(new string('-', 40)); // Thêm dòng phân cách
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi: {ex.Message}");
                    }
                }
            }

            Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
            ShowDashboard();
        }

        // Phương thức kiểm tra người dùng đã đăng ký sự kiện hay chưa
        private static bool IsEventRegistered(int eventId, int userId, SqlConnection connection)
        {
            string checkQuery = "SELECT COUNT(*) FROM RegisteredEvents WHERE UserId = @UserId AND EventId = @EventId";
            using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
            {
                checkCommand.Parameters.AddWithValue("@UserId", userId);
                checkCommand.Parameters.AddWithValue("@EventId", eventId);
                int count = (int)checkCommand.ExecuteScalar();
                return count > 0; // Trả về true nếu đã đăng ký, false nếu chưa
            }
        }

        // Phương thức để lấy tên người tổ chức
        private static string GetOrganizerName(int organizerId)
        {
            string organizerName = "Không có thông tin";

            // Mở kết nối riêng cho việc lấy tên người tổ chức
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = "SELECT FullName FROM Users WHERE Id = @OrganizerId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrganizerId", organizerId);
                    organizerName = command.ExecuteScalar()?.ToString() ?? organizerName;
                }
            }
            return organizerName;
        }

        // Phương thức để thêm bản ghi vào bảng RegisteredEvents
        private static void RegisterToEvent(int eventId, int userId, SqlConnection connection)
        {
            string insertQuery = "INSERT INTO RegisteredEvents (UserId, EventId, RegistrationDate, Status, Notes) " +
                                 "VALUES (@UserId, @EventId, @RegistrationDate, @Status, @Notes)";
            using (SqlCommand command = new SqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@EventId", eventId);
                command.Parameters.AddWithValue("@RegistrationDate", DateTime.Now); // Ghi nhận ngày giờ đăng ký
                command.Parameters.AddWithValue("@Status", "Đã đăng ký"); // Trạng thái có thể tùy chỉnh
                command.Parameters.AddWithValue("@Notes", ""); // Ghi chú có thể để trống
                command.ExecuteNonQuery();
            }

        }

        // Phương thức đăng ký sự kiện
        public static void RegisterEvent()
        {
            Console.WriteLine("=== Đăng ký sự kiện ===");
            int eventId;

            while (true)
            {
                Console.Write("Nhập ID sự kiện: ");
                string input = Console.ReadLine();

                // Kiểm tra nếu input không trống và có thể chuyển đổi sang số
                if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out eventId))
                {
                    break; // Thoát vòng lặp nếu ID hợp lệ
                }
                else
                {
                    Console.WriteLine("ID không hợp lệ. Vui lòng nhập lại.");
                }
            }

            // Kết nối tới cơ sở dữ liệu
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                if (connection == null)
                {
                    Console.WriteLine("Không thể kết nối tới cơ sở dữ liệu.");
                    return;
                }

                // Kiểm tra xem người dùng đã đăng ký sự kiện chưa
                string checkQuery = "SELECT COUNT(*) FROM RegisteredEvents WHERE UserId = @UserId AND EventId = @EventId";
                using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@UserId", Session.CurrentUserId);
                    checkCommand.Parameters.AddWithValue("@EventId", eventId);

                    int registrationCount = (int)checkCommand.ExecuteScalar();
                    if (registrationCount > 0)
                    {
                        Console.WriteLine("Bạn đã đăng ký sự kiện này.");
                        Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
                        Console.ReadKey();
                        ShowDashboard();
                        return;
                    }
                }

                // Lấy thông tin sự kiện từ cơ sở dữ liệu
                string query = "SELECT Title, Description, StartDate, EndDate, Location, CreatedBy FROM Events WHERE Id = @EventId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EventId", eventId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Hiển thị thông tin sự kiện
                            Console.WriteLine("Tiêu đề: " + reader["Title"]);
                            Console.WriteLine("Mô tả: " + reader["Description"]);
                            Console.WriteLine("Ngày bắt đầu: " + reader["StartDate"]);
                            Console.WriteLine("Ngày kết thúc: " + reader["EndDate"]);
                            Console.WriteLine("Địa điểm: " + reader["Location"]);

                            // Lấy tên người tổ chức
                            int createdBy = (int)reader["CreatedBy"]; // lấy ID của người tạo sự kiện
                            string organizerName = GetOrganizerName(createdBy);
                            Console.WriteLine("Người tổ chức: " + organizerName);

                            // Hỏi người dùng có muốn đăng ký không
                            Console.Write("Bạn có muốn đăng ký sự kiện này không? (y/n): ");
                            char response = Console.ReadKey().KeyChar;
                            Console.WriteLine();

                            if (response == 'y' || response == 'Y')
                            {
                                // Đóng DataReader trước khi thực hiện thao tác ghi
                                reader.Close();

                                // Thêm bản ghi vào bảng RegisteredEvents
                                RegisterToEvent(eventId, Session.CurrentUserId, connection);

                                // Thông báo đăng ký thành công
                                Console.WriteLine("Bạn đã đăng ký sự kiện thành công.");
                            }
                            else
                            {
                                Console.WriteLine("Bạn đã chọn không đăng ký sự kiện.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Không tìm thấy sự kiện với ID này.");
                        }
                    }
                   
                }
                Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
                Console.ReadKey();
                ShowDashboard();
            }
        }


        // Chức năng hiển thị các sự kiện đã đăng ký
        private static void ViewRegisteredEvents()
        {
            Console.Clear();
            Console.WriteLine("=== Danh sách sự kiện đã đăng ký ===");

            // Cập nhật truy vấn SQL để lấy thêm thông tin sự kiện
            string selectQuery = "SELECT E.Id, E.Title, E.Description, E.StartDate, E.EndDate, E.Location, E.CreatedBy " +
                                 "FROM Events E " +
                                 "JOIN RegisteredEvents R ON E.Id = R.EventId " +
                                 "WHERE R.UserId = @userId";

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@userId", Session.CurrentUserId);
                    try
                    {
                        SqlDataReader reader = selectCommand.ExecuteReader();
                        while (reader.Read())
                        {
                            // Hiển thị toàn bộ thông tin của sự kiện
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"ID: {reader["Id"]}");
                            Console.ResetColor();
                            Console.WriteLine($"Tiêu đề: {reader["Title"]}");
                            Console.WriteLine($"Mô tả: {reader["Description"]}");
                            Console.WriteLine($"Ngày bắt đầu: {reader["StartDate"]}");
                            Console.WriteLine($"Ngày kết thúc: {reader["EndDate"]}");
                            Console.WriteLine($"Địa điểm: {reader["Location"]}");

                            // Lấy tên người tổ chức
                            int createdBy = (int)reader["CreatedBy"]; // lấy ID của người tạo sự kiện
                            string organizerName = GetOrganizerName(createdBy);
                            Console.WriteLine($"Người tổ chức: {organizerName}");

                            Console.WriteLine(new string('-', 40)); // Thêm dòng phân cách
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi: {ex.Message}");
                    }
                }
            }
        }

        // Chức năng hủy sự kiện đã đăng ký
        private static void CancelRegistration()
        {
            Console.Clear();
            ViewRegisteredEvents();
            Console.WriteLine("=== Hủy đăng ký sự kiện ===");
            Console.Write("Nhập ID sự kiện muốn hủy: ");
            if (!int.TryParse(Console.ReadLine(), out int eventId))
            {
                Console.WriteLine("ID sự kiện không hợp lệ. Nhấn phím bất kỳ để quay lại...");
                Console.ReadKey();
                ShowDashboard();
                return;
            }

            // Xác nhận hủy đăng ký
            Console.Write("Bạn có chắc chắn muốn hủy đăng ký sự kiện này? (Y/N): ");
            string confirmation = Console.ReadLine();
            if (confirmation?.ToUpper() != "Y")
            {
                Console.WriteLine("Hủy thao tác. Nhấn phím bất kỳ để quay lại...");
                Console.ReadKey();
                ShowDashboard();
                return;
            }

            string deleteQuery = "DELETE FROM RegisteredEvents WHERE UserId = @userId AND EventId = @eventId";
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                {
                    deleteCommand.Parameters.AddWithValue("@userId", Session.CurrentUserId);
                    deleteCommand.Parameters.AddWithValue("@eventId", eventId);

                    try
                    {
                        int result = deleteCommand.ExecuteNonQuery();
                        Console.WriteLine(result > 0 ? "Hủy đăng ký sự kiện thành công." : "Hủy đăng ký sự kiện thất bại hoặc sự kiện không tồn tại.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi: {ex.Message}");
                    }
                }
            }

            Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
            ShowDashboard();
        }


        private static void SearchEvents()
        {
            Console.Clear();
            Console.WriteLine("=== Tìm kiếm sự kiện ===");
            Console.WriteLine("Bạn có thể tìm kiếm theo:");
            Console.WriteLine("1. ID sự kiện");
            Console.WriteLine("2. Tên sự kiện");
            Console.WriteLine("3. Tên câu lạc bộ tổ chức");
            Console.WriteLine("4. Địa điểm tổ chức");
            Console.Write("Chọn phương thức tìm kiếm (1-4): ");
            string searchChoice = Console.ReadLine();

            string searchQuery = "";
            SqlParameter searchParameter = null;

            switch (searchChoice)
            {
                case "1": // Tìm kiếm theo ID sự kiện
                    Console.Write("Nhập ID sự kiện: ");
                    if (int.TryParse(Console.ReadLine(), out int eventId))
                    {
                        searchQuery = "SELECT Id, Title, Description, StartDate, EndDate, Location, CreatedBy FROM Events WHERE Id = @searchValue";
                        searchParameter = new SqlParameter("@searchValue", eventId);
                    }
                    else
                    {
                        Console.WriteLine("ID không hợp lệ.");
                        Console.ReadKey();
                        ShowDashboard();
                        return;
                    }
                    break;

                case "2": // Tìm kiếm theo tên sự kiện
                    Console.Write("Nhập tên sự kiện: ");
                    string eventName = Console.ReadLine();
                    searchQuery = "SELECT Id, Title, Description, StartDate, EndDate, Location, " +
                        "CreatedBy FROM Events WHERE Title LIKE @searchValue";
                    searchParameter = new SqlParameter("@searchValue", "%" + eventName + "%");
                    break;

                case "3": // Tìm kiếm theo tên câu lạc bộ tổ chức
                    Console.Write("Nhập tên câu lạc bộ tổ chức: ");
                    string clubName = Console.ReadLine();
                    searchQuery = @"
        SELECT E.Id, E.Title, E.Description, E.StartDate, E.EndDate, E.Location, E.CreatedBy 
        FROM Events E 
        JOIN Users U ON E.CreatedBy = U.Id
        WHERE U.FullName LIKE @searchValue";
                    searchParameter = new SqlParameter("@searchValue", "%" + clubName + "%");
                    break;

                case "4": // Tìm kiếm theo địa điểm
                    Console.Write("Nhập địa điểm: ");
                    string location = Console.ReadLine();
                    searchQuery = "SELECT Id, Title, Description, StartDate, EndDate, Location, " +
                        "CreatedBy FROM Events WHERE Location LIKE @searchValue";
                    searchParameter = new SqlParameter("@searchValue", "%" + location + "%");
                    break;

                default:
                    Console.WriteLine("Lựa chọn không hợp lệ.");
                    Console.ReadKey();
                    ShowDashboard();
                    return;
            }

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand searchCommand = new SqlCommand(searchQuery, connection))
                {
                    searchCommand.Parameters.Add(searchParameter);
                    SqlDataReader reader = searchCommand.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        Console.WriteLine("Không tìm thấy sự kiện.");
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            // Hiển thị thông tin sự kiện
                            Console.WriteLine($"ID: {reader["Id"]}");
                            Console.WriteLine($"Tiêu đề: {reader["Title"]}");
                            Console.WriteLine($"Mô tả: {reader["Description"]}");
                            Console.WriteLine($"Ngày bắt đầu: {reader["StartDate"]}");
                            Console.WriteLine($"Ngày kết thúc: {reader["EndDate"]}");
                            Console.WriteLine($"Địa điểm: {reader["Location"]}");

                            int createdBy = (int)reader["CreatedBy"];
                            string organizerName = GetOrganizerName(createdBy);
                            Console.WriteLine($"Người tổ chức: {organizerName}");
                            Console.WriteLine(new string('-', 40)); // Dòng phân cách
                        }
                    }
                }
            }

            Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
            ShowDashboard();
        }


        // Chức năng xem thời tiết bằng cách gọi API bên ngoài của Open Weather Map
        public static void Weather()
        {
            string apiKey = "be3294eb40ddf30921e33ae77653c6f8"; // Thay bằng API Key của bạn

            var client = new HttpClient();

            // Danh sách các thành phố và tọa độ
            var cities = new Dictionary<string, (string, double, double)>()
            {
                { "Ho Chi Minh City", ("Ho Chi Minh City", 10.8231, 106.6297) },
                { "Ha Noi", ("Ha Noi", 21.0285, 105.8542) },
                { "Lam Dong", ("Lam Dong", 11.5753, 108.1429) },
                { "Da Nang", ("Da Nang", 16.0471, 108.2068) }
            };

            Console.WriteLine("Bạn muốn xem thời tiết của thành phố nào (Ho Chi Minh City, Ha Noi, Lam Dong, Da Nang)?");
            var city_name = Console.ReadLine();

            // Chuyển tên thành phố nhập vào thành dạng chữ thường và tìm khớp
            city_name = city_name.Trim().ToLower();

            foreach (var city in cities.Keys)
            {
                if (city.ToLower() == city_name)
                {
                    var (cityDisplayName, lat, lon) = cities[city];

                    // URL API kèm tọa độ thành phố và API Key
                    var weatherURL = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}&units=metric";

                    var weatherResponse = client.GetAsync(weatherURL).Result;

                    if (weatherResponse.IsSuccessStatusCode)
                    {
                        var formatResponseMain = JObject.Parse(weatherResponse.Content.ReadAsStringAsync().Result);

                        // Nhiệt độ
                        var temp = formatResponseMain["main"]["temp"];
                        // Độ ẩm
                        var humidity = formatResponseMain["main"]["humidity"];
                        // Áp suất
                        var pressure = formatResponseMain["main"]["pressure"];
                        // Tốc độ gió
                        var windSpeed = formatResponseMain["wind"]["speed"];
                        // Mô tả thời tiết
                        var description = formatResponseMain["weather"][0]["description"];

                        // Hiển thị các thông tin thời tiết
                        Console.WriteLine($"Thời tiết hiện tại ở {cityDisplayName}:");
                        Console.WriteLine($"- Nhiệt độ: {temp}°C");
                        Console.WriteLine($"- Độ ẩm: {humidity}%");
                        Console.WriteLine($"- Áp suất: {pressure} hPa");
                        Console.WriteLine($"- Tốc độ gió: {windSpeed} m/s");
                        Console.WriteLine($"- Mô tả: {description}");
                    }
                    else
                    {
                        var errorResponse = weatherResponse.Content.ReadAsStringAsync().Result;
                        Console.WriteLine("Không thể lấy thông tin thời tiết. Lỗi phản hồi API: " + errorResponse);
                    }

                    break;
                }
            }

            Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
            ShowDashboard(); // Giả sử đây là hàm quay lại giao diện chính
        }

        //chức năng thay đổi thông tin tài khoản
        public static void UpdateAccount()
        {
            Console.Clear();
            Console.WriteLine("=== Cập nhật tài khoản ===");

            // Lấy thông tin hiện tại của người dùng từ cơ sở dữ liệu
            string selectQuery = "SELECT FullName, Email FROM Users WHERE Id = @userId";
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@userId", Session.CurrentUserId);
                    SqlDataReader reader = selectCommand.ExecuteReader();
                    if (reader.Read())
                    {
                        Console.WriteLine($"Họ và tên hiện tại: {reader["FullName"]}");
                        Console.WriteLine($"Email hiện tại: {reader["Email"]}");
                    }
                    reader.Close();
                }

                // Nhập họ và tên mới (người dùng có thể bỏ qua)
                Console.Write("Nhập họ và tên mới (bỏ qua để giữ nguyên): ");
                string newFullName = Console.ReadLine();

                // Nhập email mới (người dùng có thể bỏ qua)
                string newEmail = null;
                while (true)
                {
                    Console.Write("Nhập email mới (bỏ qua để giữ nguyên): ");
                    newEmail = Console.ReadLine();

                    if (string.IsNullOrEmpty(newEmail))
                    {
                        break; // Người dùng không nhập email
                    }
                    else if (!IsValidEmail(newEmail))
                    {
                        Console.WriteLine("Email không hợp lệ. Vui lòng nhập lại.");
                    }
                    else if (User.UserExists(newEmail)) // Kiểm tra sự tồn tại của email
                    {
                        Console.WriteLine("Email đã tồn tại. Vui lòng nhập email khác.");
                    }
                    else
                    {
                        break; // Email hợp lệ
                    }
                }

                // Nhập mật khẩu mới (người dùng có thể bỏ qua)
                string newPassword = null;
                while (true)
                {
                    Console.Write("Nhập mật khẩu mới (bỏ qua để giữ nguyên): ");
                    newPassword = ReadPassword();

                    if (string.IsNullOrEmpty(newPassword))
                    {
                        break; // Không đổi mật khẩu
                    }
                    if (!IsValidPassword(newPassword))
                    {
                        Console.WriteLine("Mật khẩu phải có ít nhất 8 ký tự và bao gồm cả chữ và số. Vui lòng thử lại.");
                    }
                    else
                    {
                        break; // Mật khẩu hợp lệ
                    }
                }

                // Nhập tên người dùng mới (người dùng có thể bỏ qua)
                string newUsername = null;
                while (true)
                {
                    Console.Write("Nhập tên người dùng mới (bỏ qua để giữ nguyên): ");
                    newUsername = Console.ReadLine();

                    if (string.IsNullOrEmpty(newUsername))
                    {
                        break; // Người dùng không nhập tên người dùng
                    }
                    else if (User.UsernameExists(newUsername)) // Kiểm tra sự tồn tại của tên người dùng
                    {
                        Console.WriteLine("Tên người dùng đã tồn tại. Vui lòng nhập tên khác.");
                    }
                    else
                    {
                        break; // Tên người dùng hợp lệ
                    }
                }

                // Cập nhật thông tin người dùng
                string updateQuery = "UPDATE Users SET FullName = COALESCE(NULLIF(@newFullName, ''), FullName), " +
                                     "Email = COALESCE(NULLIF(@newEmail, ''), Email), " +
                                     "Password = COALESCE(NULLIF(@newPassword, ''), Password), " +
                                     "Username = COALESCE(NULLIF(@newUsername, ''), Username) " + // Cập nhật username
                                     "WHERE Id = @userId";
                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@newFullName", newFullName);
                    updateCommand.Parameters.AddWithValue("@newEmail", string.IsNullOrEmpty(newEmail) ? DBNull.Value : newEmail);
                    updateCommand.Parameters.AddWithValue("@newPassword", string.IsNullOrEmpty(newPassword) ? DBNull.Value : newPassword);
                    updateCommand.Parameters.AddWithValue("@newUsername", string.IsNullOrEmpty(newUsername) ? DBNull.Value : newUsername);
                    updateCommand.Parameters.AddWithValue("@userId", Session.CurrentUserId);

                    try
                    {
                        int result = updateCommand.ExecuteNonQuery();
                        Console.WriteLine(result > 0 ? "Cập nhật tài khoản thành công." : "Không có thông tin nào được cập nhật.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi: {ex.Message}");
                    }
                }
            }

            Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
            ShowDashboard(); // Quay lại giao diện chính sau khi cập nhật
        }


        // Kiểm tra định dạng email hợp lệ
        private static bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        // Phương thức đọc mật khẩu và hiển thị dấu *
        private static string ReadPassword()
        {
            StringBuilder password = new StringBuilder();
            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey(intercept: true);
                if (keyInfo.Key != ConsoleKey.Enter && keyInfo.Key != ConsoleKey.Backspace)
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

        // Kiểm tra tính hợp lệ của mật khẩu
        private static bool IsValidPassword(string password)
        {
            if (password.Length < 8)
                return false;

            bool hasLetter = false, hasDigit = false;
            foreach (char c in password)
            {
                if (char.IsLetter(c))
                    hasLetter = true;
                if (char.IsDigit(c))
                    hasDigit = true;

                if (hasLetter && hasDigit)
                    return true; // Mật khẩu hợp lệ
            }

            return false; // Mật khẩu không hợp lệ
        }
    }
}
