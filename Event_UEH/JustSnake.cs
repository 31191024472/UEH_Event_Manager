using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace QuanLySuKien_UEH
{
    public class TroChoiRan
    {
        // Cách dễ dàng để truy cập tọa độ của các phần tử
        struct ViTri
        {
            public int dong;
            public int cot;

            public ViTri(int dong, int cot)
            {
                this.dong = dong;
                this.cot = cot;
            }
        }

        public static void BatDauTroChoiRan()
        {
        ketThuc:
            Console.Title = "SNAKE - DO MARTIN NIKOLOV LÀM";

            // Cài đặt kích thước cửa sổ Console
            Console.WindowHeight = 25;
            Console.WindowWidth = 50;

            // Cài đặt kích thước bộ đệm của Console
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;

            // Bộ tạo số ngẫu nhiên
            Random randomNumberGenerator = new Random();

            // Biến tăng tốc độ
            double tangToc = 100;

            // Các phần tử đầu tiên của con rắn
            Queue<ViTri> cacPhanTuRan = new Queue<ViTri>();
            for (int i = 0; i <= 4; i++)
            {
                cacPhanTuRan.Enqueue(new ViTri(0, i));
            }

            // Tạo tọa độ ngẫu nhiên cho thức ăn
            ViTri thucAn;
            int thoiGianDatThucAn;
            do
            {
                thucAn = new ViTri(
                    randomNumberGenerator.Next(1, Console.WindowHeight - 1),
                    randomNumberGenerator.Next(1, Console.WindowWidth - 1));
                thoiGianDatThucAn = Environment.TickCount;
            }
            while (cacPhanTuRan.Contains(thucAn));

            // Hiển thị thức ăn trên Console
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(thucAn.cot, thucAn.dong);
            Console.Write("@");
            Console.ResetColor();

            // Đá
            List<ViTri> cacVienDa = new List<ViTri>();
            for (int i = 0; i < randomNumberGenerator.Next(1, 11); i++)
            {
                do
                {
                    cacVienDa.Add(new ViTri(randomNumberGenerator.Next(1, Console.WindowHeight - 1),
                        randomNumberGenerator.Next(1, Console.WindowWidth - 1)));
                }
                while (cacPhanTuRan.Contains(cacVienDa[i]) ||
                       (thucAn.dong == cacVienDa[i].dong && thucAn.cot == cacVienDa[i].cot));
            }

            // Hiển thị đá
            foreach (ViTri da in cacVienDa)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(da.cot, da.dong);
                Console.Write("=");
                Console.ResetColor();
            }

            // Các hướng di chuyển
            byte phai = 0;
            byte trai = 1;
            byte xuong = 2;
            byte len = 3;

            // Các hướng khả dụng
            ViTri[] huongDiChuyen =
            {
                new ViTri(0,1), // Phải
                new ViTri(0,-1), // Trái
                new ViTri(1,0), // Xuống
                new ViTri(-1,0) // Lên
            };

            // Cài đặt hướng bắt đầu - dễ nhất là đi phải
            byte huongHienTai = phai;

            // Hiển thị rắn với các tọa độ mặc định
            foreach (ViTri item in cacPhanTuRan)
            {
                Console.SetCursorPosition(item.cot, item.dong);
                Console.Write("*");
            }

            while (true)
            {
                // Kiểm tra nếu có nhấn phím nào đó
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo nhapTuBanPhim = Console.ReadKey();

                    if (nhapTuBanPhim.Key == ConsoleKey.RightArrow && huongHienTai != trai)
                    {
                        huongHienTai = phai;
                    }
                    else if (nhapTuBanPhim.Key == ConsoleKey.LeftArrow && huongHienTai != phai)
                    {
                        huongHienTai = trai;
                    }
                    else if (nhapTuBanPhim.Key == ConsoleKey.DownArrow && huongHienTai != len)
                    {
                        huongHienTai = xuong;
                    }
                    else if (nhapTuBanPhim.Key == ConsoleKey.UpArrow && huongHienTai != xuong)
                    {
                        huongHienTai = len;
                    }
                }

                ViTri dauRanHienTai = cacPhanTuRan.Last();
                ViTri huongTiepTheo = huongDiChuyen[huongHienTai];
                ViTri dauRanMoi = new ViTri(
                    dauRanHienTai.dong + huongTiepTheo.dong,
                    dauRanHienTai.cot + huongTiepTheo.cot);

                if (cacPhanTuRan.Contains(dauRanMoi) || cacVienDa.Contains(dauRanMoi))
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("GAME OVER!" + "\n" + "Điểm số của bạn là {0}!", (cacPhanTuRan.Count - 5) * 100);

                    Console.SetCursorPosition(10, 11);
                    Console.WriteLine("Nhấn [SPACE] để chơi lại...");

                    Console.SetCursorPosition(0, Console.WindowHeight - 1);
                    ConsoleKeyInfo nhapTuBanPhim = Console.ReadKey();

                    if (nhapTuBanPhim.Key == ConsoleKey.Spacebar)
                    {
                        Console.ResetColor();
                        Console.Clear();
                        goto ketThuc;
                    }
                    else
                    {
                        Console.ResetColor();
                        Console.SetCursorPosition(0, Console.WindowHeight - 1);
                        return;
                    }
                }

                if (dauRanMoi.dong == thucAn.dong && dauRanMoi.cot == thucAn.cot)
                {
                    Console.SetCursorPosition(thucAn.cot, thucAn.dong);
                    Console.Write(" ");

                    Console.Beep(80, 50);

                    do
                    {
                        thucAn = new ViTri(
                            randomNumberGenerator.Next(1, Console.WindowHeight - 1),
                            randomNumberGenerator.Next(1, Console.WindowWidth - 1));
                        thoiGianDatThucAn = Environment.TickCount;
                    }
                    while (cacPhanTuRan.Contains(thucAn) || cacVienDa.Contains(thucAn));

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.SetCursorPosition(thucAn.cot, thucAn.dong);
                    Console.Write("@");
                    Console.ResetColor();

                    int randomDa = randomNumberGenerator.Next(1, 3);

                    if (randomDa == 1)
                    {
                        ViTri daMoi;
                        do
                        {
                            daMoi = new ViTri(randomNumberGenerator.Next(1, Console.WindowHeight - 1),
                                randomNumberGenerator.Next(1, Console.WindowWidth - 1));
                        }
                        while (cacPhanTuRan.Contains(daMoi) || (thucAn.dong == daMoi.dong && thucAn.cot == daMoi.cot));

                        cacVienDa.Add(new ViTri(daMoi.dong, daMoi.cot));

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(daMoi.cot, daMoi.dong);
                        Console.Write("=");
                        Console.ResetColor();
                    }
                }
                else
                {
                    ViTri phanTuCuoi = cacPhanTuRan.Dequeue();
                    Console.SetCursorPosition(phanTuCuoi.cot, phanTuCuoi.dong);
                    Console.Write(" ");
                }

                if (dauRanMoi.dong < 0)
                    dauRanMoi.dong = Console.WindowHeight - 2;
                else if (dauRanMoi.dong >= Console.WindowHeight - 1)
                    dauRanMoi.dong = 0;
                else if (dauRanMoi.cot < 0)
                    dauRanMoi.cot = Console.WindowWidth - 2;
                else if (dauRanMoi.cot >= Console.WindowWidth - 1)
                    dauRanMoi.cot = 0;

                Console.SetCursorPosition(dauRanHienTai.cot, dauRanHienTai.dong);
                Console.WriteLine("*");

                cacPhanTuRan.Enqueue(dauRanMoi);
                Console.SetCursorPosition(dauRanMoi.cot, dauRanMoi.dong);
                if (huongHienTai == phai)
                    Console.Write(">");
                else if (huongHienTai == trai)
                    Console.Write("<");
                else if (huongHienTai == xuong)
                    Console.Write("V");
                else if (huongHienTai == len)
                    Console.Write("^");

                if (Environment.TickCount - thoiGianDatThucAn >= 6000)
                {
                    Console.SetCursorPosition(thucAn.cot, thucAn.dong);
                    Console.Write(" ");

                    do
                    {
                        thucAn = new ViTri(
                            randomNumberGenerator.Next(1, Console.WindowHeight - 1),
                            randomNumberGenerator.Next(1, Console.WindowWidth - 1));
                        thoiGianDatThucAn = Environment.TickCount;
                    }
                    while (cacPhanTuRan.Contains(thucAn) || cacVienDa.Contains(thucAn));

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.SetCursorPosition(thucAn.cot, thucAn.dong);
                    Console.Write("@");
                    Console.ResetColor();
                }

                tangToc -= 0.01;
                Thread.Sleep((int)tangToc);
            }
        }
    }
}
