using System;
using System.Data.SqlClient;

namespace Event_UEH
{
    public class Student
    {
        private static int currentUserId; // Giả sử bạn đã lưu ID người dùng hiện tại sau khi đăng nhập.

        public static void ShowDashboard()
        {
            Console.Clear();
            Console.WriteLine("=== Giao diện Sinh viên ===");
            Console.WriteLine("Chức năng:");
            Console.WriteLine("1. Đăng ký sự kiện");
            Console.WriteLine("2. Đánh giá sự kiện");
            Console.WriteLine("3. Xem danh sách sự kiện đã đăng ký");
            Console.WriteLine("4. Đăng xuất");
            Console.Write("Nhập lựa chọn: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    RegisterEvent();
                    break;
                case "2":
                    EvaluateEvent();
                    break;
                case "3":
                    ViewRegisteredEvents();
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

        private static void RegisterEvent()
        {
            Console.Clear();
            Console.WriteLine("=== Đăng ký sự kiện ===");
            Console.Write("Nhập ID sự kiện: ");
            int eventId;
            if (!int.TryParse(Console.ReadLine(), out eventId))
            {
                Console.WriteLine("ID sự kiện không hợp lệ. Nhấn phím bất kỳ để quay lại...");
                Console.ReadKey();
                ShowDashboard();
                return;
            }

            // Câu lệnh SQL để đăng ký sự kiện
            string insertQuery = "INSERT INTO RegisteredEvents (UserId, EventId) VALUES (@userId, @eventId)";
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@userId", currentUserId);
                insertCommand.Parameters.AddWithValue("@eventId", eventId);

                try
                {
                    connection.Open();
                    int result = insertCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Console.WriteLine("Đăng ký sự kiện thành công.");
                    }
                    else
                    {
                        Console.WriteLine("Đăng ký sự kiện thất bại.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi: {ex.Message}");
                }
            }

            Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
            ShowDashboard();
        }

        private static void EvaluateEvent()
        {
            Console.Clear();
            Console.WriteLine("=== Đánh giá sự kiện ===");
            Console.Write("Nhập ID sự kiện để đánh giá: ");
            int eventId;
            if (!int.TryParse(Console.ReadLine(), out eventId))
            {
                Console.WriteLine("ID sự kiện không hợp lệ. Nhấn phím bất kỳ để quay lại...");
                Console.ReadKey();
                ShowDashboard();
                return;
            }

            Console.Write("Nhập điểm đánh giá (1-5): ");
            int rating;
            if (!int.TryParse(Console.ReadLine(), out rating) || rating < 1 || rating > 5)
            {
                Console.WriteLine("Điểm đánh giá không hợp lệ. Nhấn phím bất kỳ để quay lại...");
                Console.ReadKey();
                ShowDashboard();
                return;
            }

            // Câu lệnh SQL để lưu đánh giá
            string insertQuery = "INSERT INTO Rate (UserId, EventId, Rating) VALUES (@userId, @eventId, @rating)";
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@userId", currentUserId);
                insertCommand.Parameters.AddWithValue("@eventId", eventId);
                insertCommand.Parameters.AddWithValue("@rating", rating);

                try
                {
                    connection.Open();
                    int result = insertCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Console.WriteLine("Đánh giá sự kiện thành công.");
                    }
                    else
                    {
                        Console.WriteLine("Đánh giá sự kiện thất bại.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi: {ex.Message}");
                }
            }

            Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
            ShowDashboard();
        }

        private static void ViewRegisteredEvents()
        {
            Console.Clear();
            Console.WriteLine("=== Danh sách sự kiện đã đăng ký ===");

            // Câu lệnh SQL để lấy danh sách sự kiện đã đăng ký
            string selectQuery = "SELECT E.Title, E.StartDate FROM Events E " +
                                 "JOIN RegisteredEvents R ON E.Id = R.EventId " +
                                 "WHERE R.UserId = @userId";

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@userId", currentUserId);

                try
                {
                    connection.Open();
                    SqlDataReader reader = selectCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"Tiêu đề: {reader["Title"]}, Ngày bắt đầu: {reader["StartDate"]}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi: {ex.Message}");
                }
            }

            Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
            ShowDashboard();
        }
    }
}
