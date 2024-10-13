using System;

namespace Event_UEH
{
    class Program
    {
        static void Main(string[] args)
        {
            // Thiết lập mã hóa hỗ trợ ký tự tiếng Việt
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            DatabaseSetup.SetupDatabase(); // Thiết lập cơ sở dữ liệu nếu cần

            MainMenu(); // Gọi phương thức menu chính
        }

        static void MainMenu()
        {
            string[] menuOptions = { "1. Đăng nhập", "2. Đăng ký tài khoản mới", "0. Thoát chương trình" };
            int currentSelection = 0; // Chỉ số tùy chọn hiện tại

            while (true)
            {
                Console.Clear(); // Xóa màn hình trước khi hiển thị menu
                DisplayHeader(); // Gọi phương thức hiển thị tiêu đề

                // Hiển thị các tùy chọn menu
                for (int i = 0; i < menuOptions.Length; i++)
                {
                    if (i == currentSelection) // Đánh dấu tùy chọn hiện tại
                    {
                        Console.ForegroundColor = ConsoleColor.Green; // Màu cho tùy chọn được chọn
                        Console.WriteLine($"> {menuOptions[i]} <"); // Hiển thị tùy chọn được chọn
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White; // Màu chữ bình thường
                        Console.WriteLine(menuOptions[i]); // Hiển thị tùy chọn bình thường
                    }
                }
                Console.ResetColor(); // Khôi phục lại màu sắc mặc định

                // Nhận phím nhập từ người dùng
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.UpArrow) // Nếu nhấn phím lên
                {
                    currentSelection = (currentSelection == 0) ? menuOptions.Length - 1 : currentSelection - 1; // Quay về tùy chọn cuối nếu đang ở đầu
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow) // Nếu nhấn phím xuống
                {
                    currentSelection = (currentSelection == menuOptions.Length - 1) ? 0 : currentSelection + 1; // Quay về đầu nếu đang ở cuối
                }
                else if (keyInfo.Key == ConsoleKey.Enter) // Nếu nhấn phím Enter
                {
                    switch (currentSelection)
                    {
                        case 0:
                            User.Login(); // Gọi phương thức đăng nhập
                            break;
                        case 1:
                            User.Register(); // Gọi phương thức đăng ký
                            break;
                        case 2:
                            Console.WriteLine("Tạm biệt! Nhấn phím bất kỳ để thoát...");
                            Console.ReadKey(); // Đợi người dùng nhấn phím
                            return; // Thoát chương trình
                    }
                }
            }
        }

        static void DisplayHeader()
        {
            // Thiết lập màu sắc cho tiêu đề
            Console.ForegroundColor = ConsoleColor.Cyan; // Màu chữ

            // Hiển thị tiêu đề với khung viền
            Console.WriteLine("=============================================");
            Console.WriteLine("           Phần mềm quản lý sự kiện       ");
            Console.WriteLine("=============================================");

            Console.ResetColor(); // Khôi phục lại màu sắc mặc định
        }
    }
}
