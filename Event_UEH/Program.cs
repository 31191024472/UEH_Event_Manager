using System;

namespace Event_UEH
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; // Hỗ trợ ký tự tiếng Việt
            Console.InputEncoding = System.Text.Encoding.UTF8;

            DatabaseSetup.SetupDatabase(); // Thiết lập cơ sở dữ liệu nếu cần

            MainMenu();
        }

        static void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Phần mềm quản lý sự kiện ===");
                Console.WriteLine("1. Đăng nhập");
                Console.WriteLine("2. Đăng ký tài khoản mới");
                Console.WriteLine("0. Thoát chương trình");
                Console.Write("Chọn một tùy chọn: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        User.Login(); // Gọi phương thức đăng nhập
                        break;
                    case "2":
                        User.Register(); // Gọi phương thức đăng ký
                        break;
                    case "0":
                        Console.WriteLine("Tạm biệt!");
                        return; // Thoát chương trình
                    default:
                        Console.WriteLine("Tùy chọn không hợp lệ! Nhấn phím bất kỳ để tiếp tục...");
                        Console.ReadKey();
                        break;
                }
            }
        }

    }
}
