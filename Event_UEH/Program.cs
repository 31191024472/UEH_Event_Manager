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

        // Chức năng hiển thị thanh Header UEH 
        static void DisplayHeader()
        {
            // Thiết lập màu sắc cho tiêu đề
            Console.ForegroundColor = ConsoleColor.Cyan; // Màu chữ
            int windowWidth = Console.WindowWidth;

            // Tiêu đề với khung viền
            string titleBorder = new string('=', windowWidth);
            string titleText = @"
  _    _ ______ _    _   ______               _     __  __                                   
 | |  | |  ____| |  | | |  ____|             | |   |  \/  |                                  
 | |  | | |__  | |__| | | |____   _____ _ __ | |_  | \  / | __ _ _ __   __ _  __ _  ___ _ __  
 | |  | |  __| |  __  | |  __\ \ / / _ \ '_ \| __| | |\/| |/ _` | '_ \ / _` |/ _` |/ _ \ '__| 
 | |__| | |____| |  | | | |___\ V /  __/ | | | |_  | |  | | (_| | | | | (_| | (_| |  __/ |    
  \____/|______|_|  |_| |______\_/ \___|_| |_|\__| |_|  |_|\__,_|_| |_|\__,_|\__, |\___|_|    
                                                                            __/ |           
                                                                           |___/            
";

            // Căn giữa các phần tử
            Console.WriteLine(titleBorder);
            CenterText(titleText);
            Console.WriteLine(titleBorder);

            Console.ResetColor(); // Khôi phục lại màu sắc mặc định
        }

        // Phương thức căn giữa chiều dọc và chiều ngang 
        public static void CenterText(string text)
        {
            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;
            string[] lines = text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            // Tính số dòng trống cần thiết để căn giữa theo chiều dọc
            int emptyLines = (windowHeight - lines.Length) / 2;

            // Thêm các dòng trống để căn giữa theo chiều dọc
            for (int i = 0; i < emptyLines; i++)
            {
                Console.WriteLine();
            }

            // Căn giữa theo chiều ngang từng dòng
            foreach (string line in lines)
            {
                int padding = (windowWidth - line.Length) / 2;
                if (padding > 0)
                    Console.WriteLine(new string(' ', padding) + line);
                else
                    Console.WriteLine(line); // Nếu dòng quá dài, in ra mà không căn giữa
            }
        }

        // C
        public static void MainMenu()
        {
            string[] menuOptions = { "1. Đăng nhập", "2. Đăng ký tài khoản mới", "0. Thoát chương trình" };
            int currentSelection = 0; // Chỉ số tùy chọn hiện tại

            while (true)
            {
                Console.Clear();
                DisplayHeader(); // Gọi phương thức hiển thị tiêu đề

                // Hiển thị các tùy chọn menu
                for (int i = 0; i < menuOptions.Length; i++)
                {
                    int padding = (Console.WindowWidth - menuOptions[i].Length) / 2;
                    if (i == currentSelection) // Đánh dấu tùy chọn hiện tại
                    {
                        Console.ForegroundColor = ConsoleColor.Green; // Màu cho tùy chọn được chọn
                        Console.WriteLine(new string(' ', padding) + $"> {menuOptions[i]} <"); // Hiển thị tùy chọn được chọn
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White; // Màu chữ bình thường
                        Console.WriteLine(new string(' ', padding) + menuOptions[i]); // Hiển thị tùy chọn bình thường
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
                            Console.Clear();
                            Console.WriteLine("Tạm biệt! Nhấn phím bất kỳ để thoát...");
                            Console.ReadKey(); // Đợi người dùng nhấn phím
                            Environment.Exit(0); // Thoát hoàn toàn khỏi chương trình
                            break;
                    }
                }
                
            }
        }

    }
}
