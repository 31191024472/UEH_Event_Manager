using Newtonsoft.Json.Linq;
using System;
using System.Data.SqlClient;
using static Event_UEH.User;

namespace Event_UEH
{
    public class Organizer
    {
        private static string[] options = new[]
        {
    "📅 Thêm sự kiện mới",
    "📝 Chỉnh sửa sự kiện",
    "🗑️ Xóa sự kiện",
    "📋 Xem danh sách sự kiện đã tổ chức",
    "🗂️ Xem danh sách sự kiện đã xóa",
    "👥 Xem danh sách người đăng ký",
    "🌤️ Xem thời tiết",
    "🛠️ Cập nhật thông tin tài khoản",
    "🚪 Đăng xuất"
};


        private static int currentSelection = 0; // Chỉ số lựa chọn hiện tại

        // Sử dụng Session.CurrentUserId để lấy ID người dùng hiện tại
        public static void ShowDashboard()
        {
            while (true) // Vòng lặp để giữ cho giao diện hiển thị cho đến khi người dùng chọn đăng xuất
            {
                Console.Clear();
                Console.WriteLine("=== Giao diện Tổ chức ===");
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

        private static void ExecuteSelection(int selection)
        {
            switch (selection)
            {
                case 0:
                    AddEvent();
                    break;
                case 1:
                    EditEvent();
                    Console.WriteLine("Sự kiện đã được chỉnh sửa! Nhấn phím bất kỳ để quay lại...");
                    Console.ReadKey();
                    break; // Quay lại vòng lặp chính
                case 2:
                    DeleteEvent();
                    Console.WriteLine("Sự kiện đã bị xóa! Nhấn phím bất kỳ để quay lại...");
                    Console.ReadKey();
                    break; // Quay lại vòng lặp chính
                case 3:
                    ViewOrganizedEvents();
                    Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
                    Console.ReadKey();
                    break; // Quay lại vòng lặp chính
                case 4:
                    ViewDeletedEvents();
                    Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
                    Console.ReadKey();
                    break; // Quay lại vòng lặp chính
                case 5:
                    ShowRegisteredStudents(Session.CurrentUserId);
                    Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
                    Console.ReadKey();
                    break; // Quay lại vòng lặp chính
                case 6:
                    Weather();
                    Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
                    Console.ReadKey();
                    break; // Quay lại vòng lặp chính
                case 7:
                    UpdateAccount();
                    break; // Quay lại vòng lặp chính
                case 8:
                    Console.WriteLine("Đăng xuất thành công!");
                    Program.MainMenu();
                    return;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ. Nhấn phím bất kỳ để quay lại...");
                    Console.ReadKey();
                    ShowDashboard();
                    break;
            }
        }


        // Chức năng thêm sự kiện
        private static void AddEvent()
        {
            Console.Clear();
            Console.WriteLine("=== Thêm sự kiện ===");

            Console.Write("Nhập tên sự kiện: ");
            string title = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Tên sự kiện không được để trống. Nhấn phím bất kỳ để quay lại.");
                Console.ReadKey();
                return;
            }

            Console.Write("Nhập mô tả sự kiện: ");
            string description = Console.ReadLine();

            Console.Write("Nhập địa điểm tổ chức: ");
            string location = Console.ReadLine();

            DateTime startDate;
            while (true)
            {
                Console.Write("Nhập ngày bắt đầu (dd/MM/yyyy): ");
                if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out startDate) && startDate > DateTime.Now)
                {
                    break;
                }
                Console.WriteLine("Ngày bắt đầu phải sau ngày hiện tại. Vui lòng nhập lại.");
            }

            DateTime endDate;
            while (true)
            {
                Console.Write("Nhập ngày kết thúc (dd/MM/yyyy): ");
                if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out endDate) && endDate > startDate)
                {
                    break;
                }
                Console.WriteLine("Ngày kết thúc phải sau ngày bắt đầu. Vui lòng nhập lại.");
            }

            Console.WriteLine("\n=== Xác nhận thông tin sự kiện ===");
            Console.WriteLine($"Tên: {title}");
            Console.WriteLine($"Mô tả: {description}");
            Console.WriteLine($"Địa điểm: {location}");
            Console.WriteLine($"Ngày bắt đầu: {startDate:dd/MM/yyyy}");
            Console.WriteLine($"Ngày kết thúc: {endDate:dd/MM/yyyy}");
            Console.Write("Bạn có chắc chắn muốn lưu sự kiện này? (y/n): ");
            char confirmation = Console.ReadKey().KeyChar;

            if (char.ToLower(confirmation) != 'y')
            {
                Console.WriteLine("\nĐã hủy thao tác thêm sự kiện. Nhấn phím bất kỳ để quay lại.");
                Console.ReadKey();
                return;
            }

            int organizerId = Session.CurrentUserId;
            SaveEventToDatabase(title, description, location, startDate, endDate, organizerId, organizerId);

            Console.Clear();
            Console.WriteLine("\nThêm sự kiện thành công! Nhấn phím bất kỳ để quay lại.");
            Console.ReadKey();
        }

        // Lưu sự kiện vào database
        private static void SaveEventToDatabase(string title, string description, string location, DateTime startDate, DateTime endDate, int organizerId, int createdBy)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = "INSERT INTO Events (Title, Description, Location, StartDate, EndDate, OrganizerId, CreatedBy, CreatedDate, IsActive) " +
                               "VALUES (@Title, @Description, @Location, @StartDate, @EndDate, @OrganizerId, @CreatedBy, @CreatedDate, @IsActive)";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Title", title);
                command.Parameters.AddWithValue("@Description", description);
                command.Parameters.AddWithValue("@Location", location);
                command.Parameters.AddWithValue("@StartDate", startDate);
                command.Parameters.AddWithValue("@EndDate", endDate);
                command.Parameters.AddWithValue("@OrganizerId", organizerId);
                command.Parameters.AddWithValue("@CreatedBy", createdBy);
                command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                command.Parameters.AddWithValue("@IsActive", true);

                command.ExecuteNonQuery();
            }
        }

        // Chức năng sửa sự kiện
        private static void EditEvent()
        {
            Console.Clear();
            Console.WriteLine("=== Sửa sự kiện ===");

            Console.Write("Nhập ID của sự kiện bạn muốn sửa: ");
            if (!int.TryParse(Console.ReadLine(), out int eventId))
            {
                Console.WriteLine("ID không hợp lệ. Nhấn phím bất kỳ để quay lại.");
                Console.ReadKey();
                return;
            }

            Console.Write("Nhập tên mới (bỏ qua nếu không muốn thay đổi): ");
            string newTitle = Console.ReadLine();

            Console.Write("Nhập mô tả mới (bỏ qua nếu không muốn thay đổi): ");
            string newDescription = Console.ReadLine();

            Console.Write("Nhập địa điểm mới (bỏ qua nếu không muốn thay đổi): ");
            string newLocation = Console.ReadLine();

            DateTime? newStartDate = null;
            while (true)
            {
                Console.Write("Nhập ngày bắt đầu mới (bỏ qua nếu không muốn thay đổi): ");
                string startDateInput = Console.ReadLine();
                if (string.IsNullOrEmpty(startDateInput) || (DateTime.TryParseExact(startDateInput, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out var startDate) && startDate > DateTime.Now))
                {
                    //   newStartDate = startDate;
                    break;
                }
                Console.WriteLine("Ngày bắt đầu phải sau ngày hiện tại. Vui lòng nhập lại.");
            }

            DateTime? newEndDate = null;
            while (true)
            {
                Console.Write("Nhập ngày kết thúc mới (bỏ qua nếu không muốn thay đổi): ");
                string endDateInput = Console.ReadLine();
                if (string.IsNullOrEmpty(endDateInput) || (DateTime.TryParseExact(endDateInput, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out var endDate) && endDate > newStartDate))
                {
                    //  newEndDate = endDate;
                    break;
                }
                Console.WriteLine("Ngày kết thúc phải sau ngày bắt đầu. Vui lòng nhập lại.");
            }

            UpdateEventInDatabase(eventId, newTitle, newDescription, newLocation, newStartDate, newEndDate);

            Console.Clear();
            Console.WriteLine("Sửa sự kiện thành công! Nhấn phím bất kỳ để quay lại.");
            Console.ReadKey();
        }

        // Cập nhật sự kiện trong database
        private static void UpdateEventInDatabase(int eventId, string newTitle, string newDescription, string newLocation, DateTime? newStartDate, DateTime? newEndDate)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = "UPDATE Events SET Title = ISNULL(@NewTitle, Title), Description = ISNULL(@NewDescription, Description), Location = ISNULL(@NewLocation, Location), StartDate = ISNULL(@NewStartDate, StartDate), EndDate = ISNULL(@NewEndDate, EndDate) WHERE Id = @EventId";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@NewTitle", (object)newTitle ?? DBNull.Value);
                command.Parameters.AddWithValue("@NewDescription", (object)newDescription ?? DBNull.Value);
                command.Parameters.AddWithValue("@NewLocation", (object)newLocation ?? DBNull.Value);
                command.Parameters.AddWithValue("@NewStartDate", (object)newStartDate ?? DBNull.Value);
                command.Parameters.AddWithValue("@NewEndDate", (object)newEndDate ?? DBNull.Value);
                command.Parameters.AddWithValue("@EventId", eventId);

                command.ExecuteNonQuery();
            }
        }

        // Chức năng xóa sự kiện
        private static void DeleteEvent()
        {
            Console.Clear();
            Console.WriteLine("=== Xóa sự kiện ===");

            Console.Write("Nhập ID của sự kiện bạn muốn xóa: ");
            if (!int.TryParse(Console.ReadLine(), out int eventId))
            {
                Console.WriteLine("ID không hợp lệ. Nhấn phím bất kỳ để quay lại.");
                Console.ReadKey();
                return;
            }

            Console.Write("Bạn có chắc chắn muốn xóa sự kiện này không? (y/n): ");
            char confirmation = Console.ReadKey().KeyChar;

            if (char.ToLower(confirmation) == 'y')
            {
                MoveEventToTrash(eventId);
                Console.Clear();
                Console.WriteLine("\nXóa sự kiện thành công! Nhấn phím bất kỳ để quay lại.");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\nĐã hủy thao tác xóa sự kiện. Nhấn phím bất kỳ để quay lại.");
            }

            Console.ReadKey();
        }

        private static void MoveEventToTrash(int eventId)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                // Kiểm tra xem sự kiện có người đăng ký không
                string checkRegistrationQuery = "SELECT COUNT(*) FROM RegisteredEvents WHERE EventId = @EventId";
                SqlCommand checkCommand = new SqlCommand(checkRegistrationQuery, connection);
                checkCommand.Parameters.AddWithValue("@EventId", eventId);
                int registrationCount = (int)checkCommand.ExecuteScalar();

                // Nếu sự kiện đã có người đăng ký, không xóa mà thông báo
                if (registrationCount > 0)
                {
                    Console.WriteLine("Không thể xóa sự kiện vì đã có người đăng ký.");
                    return; // Ngừng quá trình nếu sự kiện có người đăng ký
                }

                // Thêm sự kiện vào bảng Trash
                string insertQuery = "INSERT INTO Trash (EventId, UserId) VALUES (@EventId, @UserId)";
                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@EventId", eventId);
                insertCommand.Parameters.AddWithValue("@UserId", Session.CurrentUserId); // Sử dụng ID người dùng hiện tại

                insertCommand.ExecuteNonQuery();

                // Xóa sự kiện khỏi bảng Events
                string deleteQuery = "DELETE FROM Events WHERE Id = @EventId";
                SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("@EventId", eventId);

                deleteCommand.ExecuteNonQuery();

                Console.WriteLine("Sự kiện đã được di chuyển vào thùng rác.");
            }
        }


        // Xem các sự kiện đã tổ chức
        private static void ViewOrganizedEvents()
        {
            Console.Clear();
            Console.WriteLine("=== Các sự kiện đã tổ chức ===");
            Console.WriteLine();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = "SELECT * FROM Events WHERE OrganizerId = @OrganizerId AND IsActive = 1";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@OrganizerId", Session.CurrentUserId);

                SqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    Console.WriteLine("Không có sự kiện nào đã tổ chức.");
                }
                else
                {
                    Console.WriteLine($"{"ID",-5} | {"Tên sự kiện",-30} | {"Mô tả",-50} | {"Địa điểm",-20} | {"Ngày bắt đầu",-15} | {"Ngày kết thúc",-15}");
                    Console.WriteLine(new string('-', 140)); // Dòng phân cách

                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Id"],-5} | {reader["Title"],-30} | {reader["Description"],-50} | {reader["Location"],-20} | {Convert.ToDateTime(reader["StartDate"]):dd/MM/yyyy,-15} | {Convert.ToDateTime(reader["EndDate"]):dd/MM/yyyy,-15}");
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
            ShowDashboard();
        }


        // Xem các sự kiện đã xóa
        private static void ViewDeletedEvents()
        {
            Console.Clear();
            Console.WriteLine("=== Các sự kiện đã xóa ===");

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = "SELECT * FROM Trash WHERE UserId = @UserId"; // Lấy sự kiện đã xóa của người dùng hiện tại
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", Session.CurrentUserId); // Sử dụng ID người dùng hiện tại

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["EventId"]} - Đã xóa bởi UserID: {reader["UserId"]}");
                }
            }

            Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
            ShowDashboard();
        }

        // Hiển thị danh sách sinh viên đã đăng ký sự kiện
        private static void ShowRegisteredStudents(int organizerId)
        {
            Console.Clear();
            Console.WriteLine("=== Danh sách sinh viên đã đăng ký các sự kiện của bạn ===\n");

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = @"
            SELECT E.Id AS EventId, E.Title, E.Description,
                   COUNT(RE.UserId) AS RegisteredCount
            FROM Events E
            LEFT JOIN RegisteredEvents RE ON E.Id = RE.EventId
            WHERE E.OrganizerId = @OrganizerId
            GROUP BY E.Id, E.Title, E.Description
            ORDER BY E.Id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@OrganizerId", organizerId);

                SqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    Console.WriteLine("Không có sự kiện nào được tổ chức hoặc không có sinh viên đã đăng ký.");
                }
                else
                {
                    while (reader.Read())
                    {
                        int eventId = (int)reader["EventId"];
                        int registeredCount = (int)reader["RegisteredCount"];

                        Console.WriteLine("\n-------------------------------------");
                        Console.ForegroundColor = ConsoleColor.Green; // Đổi màu tiêu đề sự kiện
                        Console.WriteLine($"Sự kiện: {reader["Title"]}");
                        Console.ResetColor(); // Khôi phục màu sắc
                        Console.WriteLine($"Mô tả: {reader["Description"]}");
                        Console.WriteLine($"Số sinh viên đã đăng ký: {registeredCount}\n");

                        // Lấy thông tin sinh viên đã đăng ký cho sự kiện này
                        ShowStudentDetails(eventId, organizerId);
                    }
                }
            }

            Console.WriteLine("\nNhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
            ShowDashboard();
        }

        private static void ShowStudentDetails(int eventId, int organizerId)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = @"
            SELECT RE.UserId, RE.RegistrationDate, RE.Status, RE.Notes
            FROM RegisteredEvents RE
            WHERE RE.EventId = @EventId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EventId", eventId);

                SqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    Console.WriteLine("Không có sinh viên nào đã đăng ký cho sự kiện này.");
                }
                else
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Sinh viên ID: {reader["UserId"]}, Ngày đăng ký: {reader["RegistrationDate"]}, Trạng thái: {reader["Status"]}, Ghi chú: {reader["Notes"]}");
                    }
                }
            }
        }


        // Tìm kiếm sự kiện
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

            // Tạo truy vấn SQL và tham số tìm kiếm tùy vào lựa chọn của người dùng
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
                    searchQuery = "SELECT Id, Title, Description, StartDate, EndDate, Location, CreatedBy FROM Events WHERE Title LIKE @searchValue";
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
                    searchQuery = "SELECT Id, Title, Description, StartDate, EndDate, Location, CreatedBy FROM Events WHERE Location LIKE @searchValue";
                    searchParameter = new SqlParameter("@searchValue", "%" + location + "%");
                    break;

                default:
                    Console.WriteLine("Lựa chọn không hợp lệ.");
                    Console.ReadKey();
                    ShowDashboard();
                    return;
            }
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

            // Lấy thông tin hiện tại của sinh viên từ cơ sở dữ liệu
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
                // Nhập thông tin mới
                Console.Write("Nhập họ và tên mới (bỏ qua để giữ nguyên): ");
                string newFullName = Console.ReadLine();
                Console.Write("Nhập email mới (bỏ qua để giữ nguyên): ");
                string newEmail = Console.ReadLine();
                Console.Write("Nhập mật khẩu mới (bỏ qua để giữ nguyên): ");
                string newPassword = Console.ReadLine();

                // Chỉ cập nhật các trường mà người dùng đã nhập
                string updateQuery = "UPDATE Users SET FullName = COALESCE(NULLIF(@newFullName, ''), FullName), " +
                                     "Email = COALESCE(NULLIF(@newEmail, ''), Email), " +
                                     "Password = COALESCE(NULLIF(@newPassword, ''), Password) " +
                                     "WHERE Id = @userId";
                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@newFullName", newFullName);
                    updateCommand.Parameters.AddWithValue("@newEmail", newEmail);
                    updateCommand.Parameters.AddWithValue("@newPassword", string.IsNullOrEmpty(newPassword) ? DBNull.Value : newPassword);
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

    }
}
