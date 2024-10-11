using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Event_UEH
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Club { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int? Rating { get; set; }
        public string Feedback { get; set; }

        private static string connectionString = "Data Source=LAPTOP-S3RORAI8;Initial Catalog=EventManagementDB;Integrated Security=True";

        // Tạo sự kiện mới
        public static void CreateEvent()
        {
            Console.Write("Nhập tên sự kiện: ");
            string name = Console.ReadLine();

            Console.Write("Nhập tên câu lạc bộ: ");
            string club = Console.ReadLine();

            Console.Write("Nhập ngày tổ chức (yyyy-MM-dd): ");
            DateTime date;
            while (!DateTime.TryParse(Console.ReadLine(), out date))
            {
                Console.Write("Ngày không hợp lệ, vui lòng nhập lại (yyyy-MM-dd): ");
            }

            Console.Write("Nhập địa điểm tổ chức: ");
            string location = Console.ReadLine();

            Console.Write("Nhập mô tả sự kiện: ");
            string description = Console.ReadLine();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = "INSERT INTO Events (Name, Club, Date, Location, Description) VALUES (@Name, @Club, @Date, @Location, @Description)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Club", club);
                    command.Parameters.AddWithValue("@Date", date);
                    command.Parameters.AddWithValue("@Location", location);
                    command.Parameters.AddWithValue("@Description", description);
                    command.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Sự kiện đã được tạo thành công! Nhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
        }

        // Cập nhật sự kiện
        public static void UpdateEvent()
        {
            Console.WriteLine("=== Cập nhật sự kiện ===");
            ViewEvents();

            Console.Write("Nhập ID sự kiện cần cập nhật: ");
            if (int.TryParse(Console.ReadLine(), out int eventId))
            {
                using (SqlConnection connection = DatabaseConnection.GetConnection())
                {
                    // Truy vấn lấy thông tin sự kiện cần cập nhật
                    string query = "SELECT * FROM Events WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", eventId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Event eventToUpdate = new Event
                                {
                                    Id = (int)reader["Id"],
                                    Name = (string)reader["Name"],
                                    Club = (string)reader["Club"],
                                    Date = (DateTime)reader["Date"],
                                    Location = (string)reader["Location"],
                                    Description = (string)reader["Description"]
                                };

                                Console.Write("Nhập tên sự kiện mới (để trống nếu không thay đổi): ");
                                string newName = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(newName))
                                {
                                    eventToUpdate.Name = newName;
                                }

                                Console.Write("Nhập câu lạc bộ mới (để trống nếu không thay đổi): ");
                                string newClub = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(newClub))
                                {
                                    eventToUpdate.Club = newClub;
                                }

                                Console.Write("Nhập địa điểm mới (để trống nếu không thay đổi): ");
                                string newLocation = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(newLocation))
                                {
                                    eventToUpdate.Location = newLocation;
                                }

                                Console.Write("Nhập ngày mới (để trống nếu không thay đổi): ");
                                string newDateInput = Console.ReadLine();
                                if (DateTime.TryParse(newDateInput, out DateTime newDate))
                                {
                                    eventToUpdate.Date = newDate;
                                }

                                Console.Write("Nhập mô tả mới (để trống nếu không thay đổi): ");
                                string newDescription = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(newDescription))
                                {
                                    eventToUpdate.Description = newDescription;
                                }

                                // Cập nhật lại sự kiện trong cơ sở dữ liệu
                                string updateQuery = "UPDATE Events SET Name = @Name, Club = @Club, Date = @Date, Location = @Location, Description = @Description WHERE Id = @Id";
                                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                                {
                                    updateCommand.Parameters.AddWithValue("@Name", eventToUpdate.Name);
                                    updateCommand.Parameters.AddWithValue("@Club", eventToUpdate.Club);
                                    updateCommand.Parameters.AddWithValue("@Date", eventToUpdate.Date);
                                    updateCommand.Parameters.AddWithValue("@Location", eventToUpdate.Location);
                                    updateCommand.Parameters.AddWithValue("@Description", eventToUpdate.Description);
                                    updateCommand.Parameters.AddWithValue("@Id", eventToUpdate.Id);
                                    updateCommand.ExecuteNonQuery();
                                }

                                Console.WriteLine("Cập nhật sự kiện thành công! Nhấn phím bất kỳ để tiếp tục...");
                            }
                            else
                            {
                                Console.WriteLine("Không tìm thấy sự kiện với ID đã cho. Nhấn phím bất kỳ để tiếp tục...");
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("ID không hợp lệ. Nhấn phím bất kỳ để tiếp tục...");
            }
        }

        // Xóa sự kiện
        public static void DeleteEvent()
        {
            Console.WriteLine("=== Xóa sự kiện ===");
            ViewEvents();

            Console.Write("Nhập ID sự kiện cần xóa: ");
            if (int.TryParse(Console.ReadLine(), out int eventId))
            {
                using (SqlConnection connection = DatabaseConnection.GetConnection())
                {
                    // Xóa sự kiện trong cơ sở dữ liệu
                    string deleteQuery = "DELETE FROM Events WHERE Id = @Id";
                    using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@Id", eventId);
                        int rowsAffected = deleteCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Xóa sự kiện thành công! Nhấn phím bất kỳ để tiếp tục...");
                        }
                        else
                        {
                            Console.WriteLine("Không tìm thấy sự kiện với ID đã cho. Nhấn phím bất kỳ để tiếp tục...");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("ID không hợp lệ. Nhấn phím bất kỳ để tiếp tục...");
            }
        }

        // Hiển thị danh sách sự kiện
        public static void ViewEvents()
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = "SELECT * FROM Events";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("=== Danh sách sự kiện ===");
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"ID: {reader["Id"]} - Tên: {reader["Name"]} - Câu lạc bộ: {reader["Club"]} - Ngày: {((DateTime)reader["Date"]).ToShortDateString()} - Địa điểm: {reader["Location"]} - Mô tả: {reader["Description"]}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Không có sự kiện nào được tạo.");
                        }
                    }
                }
            }
            Console.WriteLine("Nhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
        }

        // Tìm kiếm sự kiện theo ID
        public static Event GetEventById(int id)
        {
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = "SELECT * FROM Events WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Event
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                Club = (string)reader["Club"],
                                Date = (DateTime)reader["Date"],
                                Location = (string)reader["Location"],
                                Description = (string)reader["Description"]
                            };
                        }
                    }
                }
            }
            return null; // Nếu không tìm thấy sự kiện
        }

        // Tìm kiếm sự kiện
        public static void SearchEvents()
        {
            Console.Write("Nhập tên sự kiện hoặc câu lạc bộ để tìm kiếm: ");
            string keyword = Console.ReadLine();

            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = "SELECT * FROM Events WHERE Name LIKE @Keyword OR Club LIKE @Keyword";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.Clear();
                        Console.WriteLine("=== Kết quả tìm kiếm ===");
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader["Id"]}: {reader["Name"]} - {reader["Club"]} - {((DateTime)reader["Date"]).ToShortDateString()}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Không tìm thấy sự kiện nào.");
                        }
                    }
                }
            }
            Console.WriteLine("Nhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
        }

        // Đánh giá sự kiện
        public static void RateEvent()
        {
            Console.Write("Nhập ID sự kiện để đánh giá: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                using (SqlConnection connection = DatabaseConnection.GetConnection())
                {
                    string query = "SELECT * FROM Events WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int rating = GetRating();
                                string updateQuery = "UPDATE Events SET Rating = @Rating WHERE Id = @Id";
                                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                                {
                                    updateCommand.Parameters.AddWithValue("@Rating", rating);
                                    updateCommand.Parameters.AddWithValue("@Id", id);
                                    updateCommand.ExecuteNonQuery();
                                }
                                Console.WriteLine($"Đánh giá sự kiện {reader["Name"]} thành công với điểm: {rating}!");
                            }
                            else
                            {
                                Console.WriteLine("Không tìm thấy sự kiện với ID đã cho.");
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("ID không hợp lệ.");
            }
            Console.ReadKey();
        }

        // Phương thức lấy đánh giá từ người dùng
        private static int GetRating()
        {
            Console.Write("Nhập đánh giá của bạn (1-5): ");
            int rating;
            while (!int.TryParse(Console.ReadLine(), out rating) || rating < 1 || rating > 5)
            {
                Console.Write("Đánh giá không hợp lệ. Vui lòng nhập lại (1-5): ");
            }
            return rating;
        }
    }
}
