using System;

namespace Event_UEH
{
    public class AiLaTrieuPhu
    {
        static int[] giaThuong = {
    500000, 1000000, 2000000, 3000000,
    5000000, 10000000, 14000000, 22000000,
    30000000, 50000000, 100000000, 200000000,
    400000000, 800000000, 1000000000
}; static int[] mocQuanTrong = { 5, 10, 15, 20 };
        static string[] cauHoi = {
            "Cổng thông tin điện tử của UEH là gì?",
            "Địa chỉ của DSA là?",
            "Chủ thể nào thực hiện chương trình tư vấn của DSA?",
            "Có bao nhiêu cơ sở của UEH?",
            "Sinh viên có thể liên hệ với thư viện viên bằng cách nào?",
            "Có bao nhiêu loại học bổng hỗ trợ học tập tại UEH?",
            "Chương trình giao lưu sinh viên quốc tế diễn ra vào thời gian nào?",
            "Sinh viên có thể tìm thông tin học bổng tại đâu?",
            "Địa chỉ các cơ sở của UEH nằm ở đâu?",
            "Các kênh thông tin để bạn có thể liên hệ với DSA là gì?",
            "Có bao nhiêu kênh tư vấn tại DSA?",
            "Thời gian tổ chức các hoạt động văn nghệ thường diễn ra vào lúc nào?",
            "Thời gian đăng ký học bổng là khi nào?",
            "Các hoạt động nào thường được tổ chức bởi Đoàn Thanh niên?",
            "Sinh viên có thể tham gia các khóa học kỹ năng mềm ở đâu?",
            "Nội dung nào là chính của chương trình tư vấn của DSA?",
            "Yêu cầu đối với sinh viên khi tham gia các hoạt động tình nguyện là gì?",
            "Căn cứ xét học bổng khuyến khích học tập là gì?",
            "Có bao nhiêu loại chứng chỉ tiếng Anh quốc tế được áp dụng miễn học phần tiếng Anh tổng quát?",
            "Quy trình xin học bổng hỗ trợ học tập gồm những bước nào?",
            "Các hoạt động nào nằm trong kế hoạch rèn luyện của sinh viên?"
        };
        static char[] dapAnDung = {
            'A', 'C', 'B', 'D', 'A', 'B', 'C', 'D', 'A', 'B', 'C', 'D', 'A', 'B', 'C', 'D', 'A', 'C', 'B', 'A', 'D'
        };
        static string[,] luaChon = {
            { "A. http://ueh.edu.vn", "B. http://dsa.ueh.edu.vn", "C. http://student.ueh.edu.vn", "D. http://portal.ueh.edu.vn" },
            { "A. 279 Nguyễn Tri Phương", "B. 32 Nguyễn Văn Linh", "C. 15 Đường số 3", "D. 76 Hoàng Diệu" },
            { "A. Các bộ phận phòng ban", "B. DSA", "C. Giảng viên", "D. Sinh viên" },
            { "A. 2", "B. 3", "C. 4", "D. 5" },
            { "A. Hotline", "B. Email", "C. Trực tiếp tại thư viện", "D. Tất cả các cách trên" },
            { "A. 1", "B. 3", "C. 5", "D. 7" },
            { "A. Mỗi tháng", "B. Hàng quý", "C. Hằng năm", "D. Khi có thông báo" },
            { "A. Website DSA", "B. Cổng thông tin việc làm", "C. Thư điện tử", "D. Tất cả đều đúng" },
            { "A. Quận 10", "B. Quận 4", "C. Quận 7", "D. Quận 3" },
            { "A. Hotline", "B. Email", "C. Facebook", "D. Tất cả các kênh trên" },
            { "A. 2", "B. 3", "C. 4", "D. 5" },
            { "A. Học kỳ 1", "B. Học kỳ 2", "C. Hè", "D. Tất cả các thời điểm" },
            { "A. Đầu tháng 10", "B. Giữa tháng 11", "C. Cuối tháng 12", "D. Tất cả đều đúng" },
            { "A. Hoạt động tình nguyện", "B. Hoạt động văn nghệ", "C. Hoạt động thể thao", "D. Tất cả đều đúng" },
            { "A. Trực tiếp tại DSA", "B. Qua website", "C. Qua fanpage", "D. Tất cả đều đúng" },
            { "A. Tư vấn tâm lý", "B. Đánh giá học tập", "C. Tổ chức hoạt động", "D. Tất cả đều đúng" },
            { "A. Tham gia đầy đủ", "B. Đăng ký trước", "C. Có chứng nhận", "D. Không yêu cầu" },
            { "A. Học lực", "B. Hoàn cảnh gia đình", "C. Điểm thi", "D. Cả A và B" },
            { "A. 5", "B. 6", "C. 7", "D. 8" },
            { "A. Tất cả đều sai", "B. Có một số loại", "C. Chỉ miễn phí cho một số chứng chỉ", "D. Không quy định" },
            { "A. Đăng ký", "B. Nộp hồ sơ", "C. Phỏng vấn", "D. Tất cả đều đúng" },
            { "A. Tham gia tích cực", "B. Không tham gia", "C. Chỉ tham gia một lần", "D. Không yêu cầu" }
        };

        public static void ChoiTroChoi()
        {
            Random ngauNhien = new Random();
            bool daSuDung50_50 = false;
            int giaTriHienTai = 0;
            int giaTriMocCuoi = 0;

            Console.WriteLine("Chào mừng đến với trò chơi 'Ai Là Tỷ Phú'!\n");

            int[] cacCauHoiDaChon = XaoTronCauHoi(ngauNhien);
            for (int i = 0; i < cacCauHoiDaChon.Length; i++)
            {
                int chiSoCauHoi = cacCauHoiDaChon[i];
                HienThiCauHoi(chiSoCauHoi, i + 1);

                if (HỏiSuDung50_50(ref daSuDung50_50, chiSoCauHoi, ngauNhien))
                {
                    HienThi50_50(chiSoCauHoi, ngauNhien);
                }

                char dapAn = LayDapAn();
                if (KiemTraDapAn(dapAn, chiSoCauHoi, ref giaTriHienTai, ref giaTriMocCuoi, i))
                {
                    if (i == 19)
                    {
                        Console.WriteLine("Chúc mừng! Bạn đã trở thành Tỷ phú và giành được 2,000,000,000 VND!");
                    }
                }
                else
                {
                    break;
                }

                if (HỏiDừngTroChoi(giaTriHienTai))
                {
                    Console.WriteLine($"Bạn đã quyết định dừng cuộc chơi với số tiền {giaTriHienTai} VND.");
                    break;
                }
            }

            Console.WriteLine("Cảm ơn bạn đã tham gia trò chơi 'Ai Là Tỷ Phú'!");
        }

        static int[] XaoTronCauHoi(Random ngauNhien)
        {
            int[] tatCaCauHoi = new int[cauHoi.Length];
            for (int i = 0; i < tatCaCauHoi.Length; i++)
            {
                tatCaCauHoi[i] = i;
            }

            for (int i = tatCaCauHoi.Length - 1; i > 0; i--)
            {
                int j = ngauNhien.Next(i + 1);
                int temp = tatCaCauHoi[i];
                tatCaCauHoi[i] = tatCaCauHoi[j];
                tatCaCauHoi[j] = temp;
            }

            int[] cacCauHoiDaChon = new int[20];
            Array.Copy(tatCaCauHoi, cacCauHoiDaChon, 20);
            return cacCauHoiDaChon;
        }

        static void HienThiCauHoi(int chiSoCauHoi, int soCauHoi)
        {
            Console.WriteLine($"\nCâu hỏi {soCauHoi}: {cauHoi[chiSoCauHoi]}");
            for (int j = 0; j < 4; j++)
            {
                Console.WriteLine(luaChon[chiSoCauHoi, j]);
            }
        }

        static bool HỏiSuDung50_50(ref bool daSuDung50_50, int chiSoCauHoi, Random ngauNhien)
        {
            if (!daSuDung50_50)
            {
                string suDung50_50;
                do
                {
                    Console.Write("Bạn có muốn sử dụng trợ giúp 50/50 không? (y/n): ");
                    suDung50_50 = Console.ReadLine().ToLower();

                    // Kiểm tra đầu vào
                    if (suDung50_50 == "y")
                    {
                        daSuDung50_50 = true;
                        return true;
                    }
                    else if (suDung50_50 == "n")
                    {
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Vui lòng nhập 'y' hoặc 'n'.");
                    }
                } while (true); // Lặp lại cho đến khi nhập đúng
            }
            return false;
        }

        static void HienThi50_50(int chiSoCauHoi, Random ngauNhien)
        {
            int dapAnDungIndex = Array.IndexOf(dapAnDung, dapAnDung[chiSoCauHoi]);
            int[] luaChonKhac = new int[3];
            int count = 0;

            for (int i = 0; i < 4; i++)
            {
                if (i != dapAnDungIndex)
                {
                    luaChonKhac[count] = i;
                    count++;
                }
            }

            int luaChonXoa1 = luaChonKhac[ngauNhien.Next(0, 2)];
            int luaChonXoa2 = luaChonKhac[ngauNhien.Next(0, 2)];

            Console.WriteLine($"Sau khi sử dụng 50/50, các lựa chọn còn lại là:");
            Console.WriteLine(luaChon[chiSoCauHoi, dapAnDungIndex]);
            Console.WriteLine(luaChon[chiSoCauHoi, luaChonXoa1]);
        }

        static char LayDapAn()
        {
            Console.Write("Nhập đáp án của bạn (A/B/C/D): ");
            return char.ToUpper(Console.ReadKey().KeyChar);
        }

        static bool KiemTraDapAn(char dapAn, int chiSoCauHoi, ref int giaTriHienTai, ref int giaTriMocCuoi, int soCauHoi)
        {
            if (dapAn == dapAnDung[chiSoCauHoi])
            {
                giaTriHienTai = giaThuong[soCauHoi];
                if (Array.Exists(mocQuanTrong, element => element == soCauHoi + 1))
                {
                    giaTriMocCuoi = giaTriHienTai;
                    Console.WriteLine($"\nChúc mừng! Bạn đã vượt qua mốc quan trọng và hiện có {giaTriHienTai} VND.");
                }
                return true;
            }
            else
            {
                Console.WriteLine("\nRất tiếc, bạn đã sai!");
                Console.WriteLine($"Số tiền bạn nhận được là: {giaTriMocCuoi} VND.");
                return false;
            }
        }

        static bool HỏiDừngTroChoi(int giaTriHienTai)
        {
            string dừngTroChoi;
            do
            {
                Console.Write($"\nBạn có muốn dừng cuộc chơi và lấy số tiền hiện tại ({giaTriHienTai} VNĐ) không? (y/n): ");
                dừngTroChoi = Console.ReadLine()?.ToLower();

                if (dừngTroChoi != "y" && dừngTroChoi != "n")
                {
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập 'y' hoặc 'n'.");
                }

            } while (dừngTroChoi != "y" && dừngTroChoi != "n");

            return dừngTroChoi == "y";
        }

    }
}


