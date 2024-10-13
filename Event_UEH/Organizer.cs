using System;
using System.Data.SqlClient;
using static Event_UEH.User;

namespace Event_UEH
{
    public class Organizer
    {
        public static void ShowDashboard()
        {
            Console.Clear();
            Console.WriteLine("=== Giao diện Tổ chức ===");
            Console.WriteLine("Chức năng:");
            Console.WriteLine("1. Thêm sự kiện");
            Console.WriteLine("2. Sửa sự kiện");
            Console.WriteLine("3. Xóa sự kiện");
            Console.WriteLine("4. Xem các sự kiện đã tổ chức");
            Console.WriteLine("5. Xem các sự kiện đã xóa");
            Console.WriteLine("6. Đăng xuất");
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
                    ViewOrganizedEvents();
                    break;
                case "5":
                    ViewDeletedEvents();
                    break;
                case "6":
                    Console.WriteLine("Đăng xuất thành công!");
                    break;
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

            Console.Write("Nhập mô tả sự kiện: ");
            string description = Console.ReadLine();

            Console.Write("Nhập địa điểm tổ chức: ");
            string location = Console.ReadLine();

            Console.Write("Nhập ngày bắt đầu (dd/MM/yyyy): ");
            DateTime startDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

            Console.Write("Nhập ngày kết thúc (dd/MM/yyyy): ");
            DateTime endDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

            // Lấy ID người dùng từ Session
            int organizerId = Session.CurrentUserId;  // Đây là ID của tổ chức đang đăng nhập
            int createdBy = Session.CurrentUserId;  // Sử dụng ID của người tổ chức làm giá trị cho createdBy

            // Gọi hàm để lưu thông tin sự kiện vào cơ sở dữ liệu
            SaveEventToDatabase(title, description, location, startDate, endDate, organizerId, createdBy);

            Console.WriteLine("Thêm sự kiện thành công!");
            Console.ReadKey();
            ShowDashboard();  // Quay lại giao diện chính sau khi thêm sự kiện
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
            int eventId = int.Parse(Console.ReadLine());

            Console.Write("Nhập tên mới (bỏ qua nếu không muốn thay đổi): ");
            string newTitle = Console.ReadLine();

            Console.Write("Nhập mô tả mới (bỏ qua nếu không muốn thay đổi): ");
            string newDescription = Console.ReadLine();

            Console.Write("Nhập địa điểm mới (bỏ qua nếu không muốn thay đổi): ");
            string newLocation = Console.ReadLine();

            Console.Write("Nhập ngày bắt đầu mới (bỏ qua nếu không muốn thay đổi): ");
            string startDateInput = Console.ReadLine();
            DateTime? newStartDate = !string.IsNullOrEmpty(startDateInput) ? DateTime.ParseExact(startDateInput, "dd/MM/yyyy", null) : (DateTime?)null;

            Console.Write("Nhập ngày kết thúc mới (bỏ qua nếu không muốn thay đổi): ");
            string endDateInput = Console.ReadLine();
            DateTime? newEndDate = !string.IsNullOrEmpty(endDateInput) ? DateTime.ParseExact(endDateInput, "dd/MM/yyyy", null) : (DateTime?)null;

            UpdateEventInDatabase(eventId, newTitle, newDescription, newLocation, newStartDate, newEndDate);

            Console.WriteLine("Sửa sự kiện thành công!");
            Console.ReadKey();
            ShowDashboard();
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
            int eventId = int.Parse(Console.ReadLine());

            // Xóa sự kiện và lưu thông tin vào bảng Trash
            MoveEventToTrash(eventId);

            Console.WriteLine("Xóa sự kiện thành công!");
            Console.ReadKey();
            ShowDashboard();
        }

        private static void MoveEventToTrash(int eventId)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
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
            }
        }

        // Xem các sự kiện đã tổ chức
        private static void ViewOrganizedEvents()
        {
            Console.Clear();
            Console.WriteLine("=== Các sự kiện đã tổ chức ===");

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = "SELECT * FROM Events WHERE OrganizerId = @OrganizerId AND IsActive = 1";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@OrganizerId", Session.CurrentUserId); // Sử dụng ID người dùng hiện tại

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["Id"]}, Tên: {reader["Title"]}, Mô tả: {reader["Description"]}, Địa điểm: {reader["Location"]}, Ngày bắt đầu: {reader["StartDate"]}, Ngày kết thúc: {reader["EndDate"]}");
                }
            }

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
    }
}
