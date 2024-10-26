using System;

namespace Event_UEH
{
    public class DuongLenDinhUEH
    {
        static int[] giaThuong = {
    500000, 1000000, 2000000, 3000000,
    5000000, 10000000, 14000000, 22000000,
    30000000, 50000000, 100000000, 200000000,
    400000000, 800000000, 1000000000
}; static int[] mocQuanTrong = { 5, 10, 15, 20 };
        static string[] cauHoi = {
    "Bạn có thể liên hệ với thư viện viên bằng cách:",
    "Bạn tự kiểm tra đạo văn bằng ứng dụng trực tuyến tại đường dẫn:",
    "Các các kênh thông tin để bạn có thể liên hệ với Phòng Chăm sóc và hỗ trợ người học khi gặp vấn đề cần hỗ trợ?",
    "Các loại chứng chỉ tiếng Anh quốc tế được áp dụng Miễn học phần tiếng Anh tổng quát theo quy định hiện hành, gồm:",
    "Căn cứ xét học bổng khuyến khích học tập?",
    "Cơ sở chính của UEH là cơ sở nào?",
    "Cổng thông tin thư viện có địa chỉ URL là:",
    "Địa chỉ website sinh viên đăng nhập trước khi đăng ký học phần là ___",
    "Điểm Miễn học phần tiếng Anh tổng quát có được tính vào điểm trung bình để xét học bổng Khuyến khích học tập?",
    "English Zone nằm tại cơ sở nào?",
    "Giờ đóng cửa của Ký túc xá UEH?",
    "Hiện nay có mấy ban chuyên môn trực thuộc Đoàn - Hội UEH?",
    "Hình thức xử lý kỷ luật nào áp dụng mức phạt trừ 25% số điểm thi khi sinh viên vi phạm quy chế thi?",
    "Hoạt động Đoàn Thanh niên - Hội Sinh viên UEH chào đón Tân sinh viên hàng năm có tên là gì?",
    "Quyền lợi của sinh viên khi NCKH bao gồm?",
    "Sinh viên còn trong thời gian đào tạo kế hoạch của khóa học có được phép nghỉ học tạm thời và bảo lưu kết quả học tập không?",
    "Sinh viên ngoại trú có hộ khẩu tại TP.HCM có cần cung cấp địa chỉ ngoại trú cho Trường không?",
    "Sinh viên và Cố vấn học tập có thể trao đổi trên không gian số do UEH xây dựng và phát triển, đó là ___",
    "Sinh viên xếp loại Xuất sắc theo học kỳ, năm học, khóa học khi có mức điểm trung bình tích lũy (theo thang điểm 4) đạt ___",
    "Theo quy định hiện hành, Cố vấn học tập lớp sinh viên được phân công cho đối tượng nào phụ trách?"
};

        static char[] dapAnDung = {
    'D', 'B', 'A', 'C', 'B', 'A', 'C', 'B', 'A', 'D',
    'C', 'D', 'A', 'A', 'D', 'B', 'A', 'C', 'D', 'A'
};

        static string[,] luaChon = {
    { "A) Liên hệ số điện thoại (028) 3856.1249 - Ext.102", "B) Gửi email đến askusnow@ueh.edu.vn", "C) Sử dụng dịch vụ live chat Ask-us-now", "D) Tất cả các kênh đã nêu" },
    { "A) plagiarismdetector.net", "B) kiemtradaovan.ueh.edu.vn", "C) plagiarismcheckerx.com", "D) copyscape.com" },{ "A) Hotline (028) 7306 1976; email: dsa@ueh.edu.vn; fanpage UEH [@DHKT.UEH]; Fanpage DSA [@DSA.UEH]", "B) Hotline (028) 3829 5299; email: dsa@ueh.edu.vn; Fanpage DSA [@DSA.UEH]", "C) Hotline (028) 7306 1976; email: dsa@ueh.edu.vn", "D) Hotline (028) 3829 5299; email: dsa@ueh.edu.vn; fanpage UEH [@DHKT.UEH]; Fanpage DSA [@DSA.UEH]" },
    { "A) TOEIC; TOEFL; VPET", "B) IELTS; TOEFL; VPET", "C) TOEIC; IELTS; TOEFL IBT; PTE", "D) TOEIC; IELTS; TOEFL" },
    { "A) Điểm trung bình học tập và điểm rèn luyện toàn khóa học tích lũy đến thời điểm xét", "B) Điểm trung bình học tập tích lũy và điểm rèn luyện của học kỳ trước học kỳ xét.", "C) Điểm trung bình học tập tích lũy của học kỳ trước và điểm rèn luyện toàn khóa học tích lũy đến thời điểm xét", "D) Điểm trung bình tích lũy, điểm rèn luyện của học kỳ trước, hoàn cảnh gia đình khó khăn" },
    { "A) Cơ sở A", "B) Cơ sở B", "C) Cơ sở N", "D) Cơ sở I" },
    { "A) thuvienthongminh.ueh.edu.vn", "B) lib.ueh.edu.vn", "C) smartlib.ueh.edu.vn", "D) digital.ueh.edu.vn" },
    { "A) daotao.ueh.edu.vn", "B) student.ueh.edu.vn", "C) khdtkt.ueh.edu.vn", "D) dsa.ueh.edu.vn" },
    { "A) Không áp dụng", "B) Được áp dụng", "C) Được áp dụng 1/2 số tín chỉ", "D) Tất cả đáp án đều sai" },
    { "A) Cơ sở F", "B) Cơ sở A", "C) Cơ sở N", "D) Cơ sở B" },
    { "A) 22h00", "B) 22h30", "C) 23h00", "D) 23h30" },
    { "A) 3", "B) 4", "C) 5", "D) 6" },
    { "A) Khiển trách", "B) Cảnh cáo", "C) Đình chỉ thi", "D) Phạm lỗi một lần" },
    { "A) Nối vòng tay lớn", "B) Be UEHer", "C) Tuần lễ định hướng", "D) I am who I Choose to be" },
    { "A) Tăng cường khả năng tìm tòi, nghiên cứu, phát hiện,… dưới sự hỗ trợ của giảng viên", "B) Cơ hội tham gia giải thưởng NCKH các cấp dành cho sinh viên", "C) Được học hỏi, trải nghiệm, thực hành các kỹ năng cơ bản để nghiên cứu", "D) Tất cả các đáp án đã nêu" },
    { "A) Được, và không cần điều kiện theo quy định", "B) Được, nhưng phải đáp ứng điều kiện theo quy định", "C) Không", "D) Không, sinh viên chỉ được nghỉ học tạm thời, còn toàn bộ kết quả học tập sẽ bị hủy" },
    { "A) Bắt buộc cung cấp", "B) Không cần cung cấp", "C) Chỉ có sinh viên nhận được yêu cầu của Trường mới cần phải cung cấp", "D) Khi sinh viên học đến năm cuối mới cần cung cấp" },
    { "A) MS Teams", "B) Google Meet", "C) Nền tảng Cố vấn học tập advisor.ueh.edu.vn", "D) Zalo" },
    { "A) Từ 2,0 đến 2,5", "B) Từ 2,5 đến 3,2", "C) Từ 3,2 đến 3,6", "D) Từ 3,6 đến 4,0" },
    { "A) giảng viên", "B) chuyên viên", "C) nhân viên", "D) sinh viên" }
};

        public static void ChoiTroChoi()
        {
            Random ngauNhien = new Random();
            bool daSuDung50_50 = false;
            int giaTriHienTai = 0;
            int giaTriMocCuoi = 0;

            Console.WriteLine("Chào mừng đến với trò chơi 'Đường Lên Đỉnh UEH'!\n");

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
                        Console.WriteLine("Chúc mừng! Bạn đã giành được 2,000,000,000 VND!");
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

            Console.WriteLine("Cảm ơn bạn đã tham gia trò chơi 'Đường Lên Đỉnh UEH'!");
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


