using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;

namespace Event_UEH
{
    public class Trash
    {
        private const int RetentionTimeInSeconds = 30; // Thời gian lưu trữ trong thùng rác (giây)

        // Thêm sự kiện vào thùng rác
        public void AddToTrash(Event eventData)
        {
            if (eventData == null) // Kiểm tra xem eventData có phải là null không
            {
                Console.WriteLine("Sự kiện không hợp lệ.");
                return;
            }

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string insertQuery = "INSERT INTO Trash (EventId, Name, Club, Date, Location, Description) VALUES (@EventId, @Name, @Club, @Date, @Location, @Description)";
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@EventId", eventData.Id);
                    command.Parameters.AddWithValue("@Name", eventData.Name);
                    command.Parameters.AddWithValue("@Club", eventData.Club);
                    command.Parameters.AddWithValue("@Date", eventData.Date);
                    command.Parameters.AddWithValue("@Location", eventData.Location);
                    command.Parameters.AddWithValue("@Description", eventData.Description);
                    command.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Sự kiện đã được chuyển vào thùng rác.");
            Console.WriteLine($"Dữ liệu sẽ tự động xóa vĩnh viễn sau {RetentionTimeInSeconds} giây.");

            // Bắt đầu quy trình tự động xóa
            Thread deleteThread = new Thread(new ThreadStart(AutoDeleteAfterTime));
            deleteThread.Start();
        }

        // Hiển thị các sự kiện trong thùng rác
        public void DisplayTrash()
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = "SELECT * FROM Trash";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("=== Các sự kiện trong thùng rác ===");
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"ID: {reader["Id"]} - Tên: {reader["Name"]} - Câu lạc bộ: {reader["Club"]} - Ngày: {((DateTime)reader["Date"]).ToShortDateString()}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Thùng rác rỗng.");
                        }
                    }
                }
            }
            Console.WriteLine("Nhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
        }

        // Xóa vĩnh viễn các sự kiện trong thùng rác
        public void PermanentlyDelete()
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string deleteQuery = "DELETE FROM Trash";
                using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                {
                    int rowsAffected = deleteCommand.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} sự kiện trong thùng rác đã bị xóa vĩnh viễn.");
                }
            }
        }

        // Tự động xóa dữ liệu sau khoảng thời gian quy định
        public void AutoDeleteAfterTime()
        {
            Thread.Sleep(RetentionTimeInSeconds * 1000); // Chờ thời gian retention
            PermanentlyDelete();
        }
    }
}
