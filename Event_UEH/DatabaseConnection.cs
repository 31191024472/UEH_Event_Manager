using System;
using System.Data.SqlClient;

namespace Event_UEH
{
    public class DatabaseConnection
    {
        // Chuỗi kết nối tới SQL Server
        private static string connectionString = "Data Source=LAPTOP-S3RORAI8;" +
            "Initial Catalog=EventManagementDB;Integrated Security=True";

        // Phương thức để lấy SqlConnection
        public static SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Kết nối tới SQL Server thành công.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Có lỗi xảy ra khi kết nối: " + ex.Message);
            }
            return connection;
        }

        // Đóng kết nối
        public static void CloseConnection(SqlConnection connection)
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
                Console.WriteLine("Kết nối đã được đóng.");
            }
        }
    }
}
