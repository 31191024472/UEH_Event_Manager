using Newtonsoft.Json.Linq;
using System;
using System.Data.SqlClient;
using static Event_UEH.User;
using System.Net.Http;
using Newtonsoft.Json.Linq;


namespace Event_UEH 
{
    public class Student
    {
        // Sử dụng Session.CurrentUserId để lấy ID người dùng hiện tại
        public static void ShowDashboard()
        {
            Console.Clear();
            Console.WriteLine("=== Giao diện Sinh viên ===");
            Console.WriteLine("Chức năng:");
            Console.WriteLine("1. Đăng ký sự kiện");
            Console.WriteLine("2. Đánh giá sự kiện");
            Console.WriteLine("3. Xem danh sách sự kiện đã đăng ký");
            Console.WriteLine("4. Hủy đăng ký sự kiện");
            Console.WriteLine("5. Tìm kiếm sự kiện");
            Console.WriteLine("6. Hiển thị toàn bộ sự kiện");
            Console.WriteLine("7. Chơi game giải trí");
            Console.WriteLine("8. Xem thời tiết");
            Console.WriteLine("9. Đăng xuất");
            Console.Write("Nhập lựa chọn: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    RegisterEvent();
                    break;
                case "2":
                    EvaluateEvent();
                    Console.WriteLine("Cảm ơn bạn đã đánh giá,Nhấn phím bất kỳ để quay lại... ");
                    Console.ReadKey();
                    ShowDashboard();
                    break;
                case "3":
                    ViewRegisteredEvents();
                    break;
                case "4":
                    CancelRegistration();
                    break;
                case "5":
                    SearchEvents();
                    break;
                case "6":
                    DisplayAllEvents();
                    break;
                case "7":
                    Console.Clear();
                    AiLaTrieuPhu.ChoiTroChoi();
                    Console.WriteLine(" Nhấn phím bất kỳ để quay lại...");
                    Console.ReadKey();
                    ShowDashboard();
                    break;
                case "8":
                    Weather();
                    break;
                case "9":
                    Console.WriteLine("Đăng xuất thành công!");
                    break;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ. Nhấn phím bất kỳ để quay lại...");
                    Console.ReadKey();
                    ShowDashboard();
                    break;
            }
        }
        // Chức năng đánh giá sự kiện
        private static void EvaluateEvent()
        {
            Console.Clear();
            Console.WriteLine("=== Đánh giá sự kiện ===");
            Console.Write("Nhập ID sự kiện để đánh giá: ");

            if (!int.TryParse(Console.ReadLine(), out int eventId))
            {
                Console.WriteLine("ID sự kiện không hợp lệ. Nhấn phím bất kỳ để quay lại...");
                Console.ReadKey();
                ShowDashboard();
                return;
            }

            Console.Write("Nhập điểm đánh giá (1-5): ");
            if (!int.TryParse(Console.ReadLine(), out int rating) || rating < 1 || rating > 5)
            {
                Console.WriteLine("Điểm đánh giá không hợp lệ. Nhấn phím bất kỳ để quay lại...");
                Console.ReadKey();
                ShowDashboard();
                return;
            }

            // Nhập ghi chú
            Console.Write("Nhập ghi chú (bỏ qua nếu không có): ");
            string content = Console.ReadLine();

            string insertQuery = @"
        INSERT INTO Rate (UserId, EventId, Rating, Content) 
        VALUES (@userId, @eventId, @rating, @content)";

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
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
            Console.Write("Nhập ID sự kiện: ");
            int eventId = int.Parse(Console.ReadLine());

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
            }
            // Giữ người dùng ở lại giao diện
            Console.WriteLine("Bạn đã đăng ký sự kiện thành công");
            Console.ReadKey();
            ShowDashboard(); // Quay lại giao diện dashboard sau khi người dùng đã thực hiện xong
        }
        
        // Chức năng hiển thị các sự kiện đã đăng ký
        private static void ViewRegisteredEvents()
        {
            Console.Clear();
            Console.WriteLine("=== Danh sách sự kiện đã đăng ký ===");

            // Cập nhật truy vấn SQL để lấy thêm thông tin sự kiện
            string selectQuery = "SELECT E.Title, E.Description, E.StartDate, E.EndDate, E.Location, E.CreatedBy " +
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

        // Chức năng hủy sự kiện đã đăng ký
        private static void CancelRegistration()
        {
            Console.Clear();
            Console.WriteLine("=== Hủy đăng ký sự kiện ===");
            Console.Write("Nhập ID sự kiện muốn hủy: ");
            if (!int.TryParse(Console.ReadLine(), out int eventId))
            {
                Console.WriteLine("ID sự kiện không hợp lệ. Nhấn phím bất kỳ để quay lại...");
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

        // Chức năng tìm kiếm sự kiện
        private static void SearchEvents()
        {
            Console.Clear();
            Console.WriteLine("=== Tìm kiếm sự kiện ===");
            Console.Write("Nhập từ khóa tìm kiếm: ");
            string keyword = Console.ReadLine();

            // Cập nhật truy vấn SQL để lấy thêm thông tin sự kiện
            string searchQuery = "SELECT Id, Title, Description, StartDate, EndDate, Location, CreatedBy FROM Events WHERE Title LIKE @keyword";

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (SqlCommand searchCommand = new SqlCommand(searchQuery, connection))
                {
                    searchCommand.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                    try
                    {
                        SqlDataReader reader = searchCommand.ExecuteReader();
                        if (reader.HasRows)
                        {
                            Console.WriteLine(new string('=', 30)); // Dòng phân cách
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
                                int createdBy = (int)reader["CreatedBy"];
                                string organizerName = GetOrganizerName(createdBy);
                                Console.WriteLine($"Người tổ chức: {organizerName}");

                                Console.WriteLine(new string('-', 40)); // Dòng phân cách giữa các sự kiện
                            }
                            Console.WriteLine(new string('=', 30)); // Kết thúc danh sách
                        }
                        else
                        {
                            Console.WriteLine("Không tìm thấy sự kiện nào phù hợp với từ khóa.");
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

    }
}
