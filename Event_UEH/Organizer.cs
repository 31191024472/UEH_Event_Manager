using System;

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
            Console.WriteLine("4. Đăng xuất");
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
                    Console.WriteLine("Đăng xuất thành công!");
                    break;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ. Nhấn phím bất kỳ để quay lại...");
                    Console.ReadKey();
                    ShowDashboard();
                    break;
            }
        }

        private static void AddEvent()
        {
            Console.WriteLine("Thêm sự kiện - Chức năng sẽ được thêm sau.");
            Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
            ShowDashboard();
        }

        private static void EditEvent()
        {
            Console.WriteLine("Sửa sự kiện - Chức năng sẽ được thêm sau.");
            Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
            ShowDashboard();
        }

        private static void DeleteEvent()
        {
            Console.WriteLine("Xóa sự kiện - Chức năng sẽ được thêm sau.");
            Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
            ShowDashboard();
        }
    }
}
