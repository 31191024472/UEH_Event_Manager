using System;

namespace Event_UEH
{
    class AiLaTrieuPhu
    {
        static int[] giaThuong = { 500000, 1000000, 2000000, 3000000, 5000000, 10000000, 14000000, 22000000, 30000000, 50000000, 100000000, 200000000, 400000000, 800000000, 1000000000 };
        static int[] mocQuanTrong = { 5, 10, 15 };
        static string[] cauHoi = {
            "Bạn có thể đăng ký cấp lại thẻ sinh viên; phúc khảo các học phần; xác nhận tạm hoãn nghĩa vụ quân sự; xác nhận vay vốn sinh viên; xác nhận sinh viên; cấp bảng điểm Tiếng Việt/Tiếng Anh; bảng điểm rèn luyện; bản sao văn bằng; nộp hồ sơ tuyển sinh; Gia tăng",
"Ai có thể tham gia các chương trình tại English Zone",

"Bạn có thể liên hệ với thư viện viên bằng cách",

"Bạn có thể Tra cứu Thời khóa biểu, Hình thức giảng dạy, Lịch thi, Giảng đường tại đâu",

"Bạn tự kiểm tra đạo văn bằng ứng dụng trực tuyến tại đường dẫn",

"Bên cạnh đội ngũ tư vấn tận tâm, DSA còn",

"Các kênh thông tin để bạn có thể liên hệ với Phòng Chăm sóc và hỗ trợ người học khi gặp vấn đề cần hỗ trợ",

"Các hoạt động tại English Zone do phòng/ban nào tổ chức",

"Các loại chứng chỉ tiếng Anh quốc tế được áp dụng Miễn học phần tiếng Anh tổng quát theo quy định hiện hành, gồm",

"Các mặt đánh giá kết quả rèn luyện bao gồm",

"Các nguồn hỗ trợ tài chính gồm có",

"Các tổ chức chính trị, chính trị - xã hội tại UEH là",

"Căn cứ xét học bổng khuyến khích học tập",

"Chủ thể tiếp nhận và thực hiện UEH thành trường đại học định hướng nghiên cứu gồm",

"Chuẩn trình độ ngoại ngữ đầu ra là giống nhau giữa các chương trình chuẩn, chương trình tiếng Anh bán phần, chương trình tiếng Anh toàn phần",

"CLB/Đội/Nhóm nào sau đây là CLB/Đội/Nhóm trực thuộc Đoàn Hội cấp trường",

"Có bao kênh tư vấn tại DSA",

"Có bao nhiêu mức học bổng hỗ trợ học tập",

"Có bao nhiêu mức học bổng khuyến khích học tập",

"Có bao nhiêu tiện ích trên Cổng thông tin việc làm",

"Để nhận thông tin kịp thời từ DSA, người học cần phải",

"Để sử dụng thành thạo các tiện ích của DSA, bạn cần",

"Địa chỉ các cơ sở của UEH nằm ở đâu",

"Địa chỉ của DSA là",

"Địa chỉ Văn phòng DSA",

"Đoàn Thanh niên có nhiệm vụ gì",

"Đoàn viên thanh niên có thể tham gia các hoạt động nào",

"Giờ làm việc của DSA",

"Hỗ trợ người học qua kênh nào",

"Những hoạt động nào nằm trong kế hoạch rèn luyện của sinh viên",

"Nội dung nào là chính của chương trình tư vấn của DSA",

"Quy trình xin học bổng hỗ trợ học tập gồm những bước nào",

"Sinh viên có thể nhận học bổng từ đâu",

"Thời gian đăng ký học bổng là khi nào",

"Thời gian tổ chức các hoạt động văn nghệ thường diễn ra vào lúc nào",

"Trung tâm hỗ trợ người học của UEH được thành lập với mục đích gì",

"Vị trí của các cơ sở của UEH nằm ở đâu",

"Yêu cầu đối với sinh viên khi tham gia các hoạt động tình nguyện là gì",

"Bạn có thể tìm thông tin về học bổng tại đâu",

"Các câu lạc bộ sinh viên tại UEH thường hoạt động vào thời gian nào",

"Những hoạt động nào thường được tổ chức bởi Đoàn Thanh niên",

"Sinh viên có thể tham gia các khóa học kỹ năng mềm ở đâu",

"Chương trình giao lưu sinh viên quốc tế diễn ra vào thời gian nào",

"Những hoạt động nào là điểm nhấn trong các sự kiện của trường",

"Sinh viên có thể tham gia tư vấn nghề nghiệp ở đâu",

"Thời gian tổ chức các sự kiện lớn trong năm học thường là khi nào",

"Các chương trình học bổng của UEH thường được công bố ở đâu",

"Sinh viên có thể nhận được sự hỗ trợ nào từ DSA",

"Để tham gia vào các hoạt động của Đoàn Thanh niên, sinh viên cần đáp ứng yêu cầu gì",

            };
        static char[] dapAnDung = {
'A',
'B',
'C',
'B',
'C',
'A',
'A',
'C',
'B',
'A',
'A',
'A',
'A',
'B',
'A',
'A',
'A',
'A',
'D',
'A',
'B',
'D',
'A',
'A',
'A',
'A',
'D',
'D',
'A',
'A',
'A',
'C',
'D',
'C',
'A',
'D',
'D',
'A',
'A',
'A',
'C',
'A',
'A',
'A',
'C',
'D',
'A',
'D',
'A',
'A',
            };
        static string[,] luaChon = {
               { "A. Trang sinh viên: http://student.ueh.edu.vn/", "B. Cổng Giao dịch điện tử UEH (UEH online service): https://es.ueh.edu.vn", "C. Website Phòng Chăm sóc và hỗ trợ người học: https://dsa.ueh.edu.vn", "D. Trực tiếp tại các đơn vị có liên quan" },

{ "A. Sinh viên quốc tế", "B. Tất cả đều đúng", "C. Viên chức", "D. Sinh viên ĐHCQ" },

{ "A. Liên hệ số điện thoại (028) 3856.1249 - Ext.102", "B. Gửi email đến askusnow@ueh.edu.vn", "C. Tất cả các kênh đã nêu", "D. Sử dụng dịch vụ live chat Ask-us-now" },

{ "A. Cả [*] [**] đều đúng", "B. Website Phòng Đào tạo", "C. Tài khoản sinh viên: http://student.ueh.edu.vn", "D. Website Phòng Kế hoạch đào tạo - Khảo thí" },

{ "A. plagiarismcheckerx.com", "B. plagiarismdetector.net", "C. kiemtradaovan.ueh.edu.vn", "D. copyscape.com" },

{ "A. liên kết với các Bộ phận công tác Đoàn thanh niên - Hội sinh viên người học nhằm hỗ trợ bao quát mọi vấn đề đời sống sinh viên.", "B. liên kết với các chuyên gia là tư vấn viên tại các đơn vị chức năng, khoa, viện, nhằm hỗ trợ người học về các vấn đề và nhu cầu chuyên sâu trong từng lĩnh vực cụ thể.", "C. liên kết với Trạm Y tế nhằm hỗ trợ sức khỏe thể chất và tinh thần cho đời sống sinh viên.", "D. liên kết với các chuyên gia tại địa phương người học nhằm hỗ trợ người học về các vấn đề và nhu cầu chuyên sâu trong từng lĩnh vực cụ thể." },

{ "A. Hotline (028) 7306 1976; email: dsa@ueh.edu.vn; fanpage UEH [@DHKT.UEH]; Fanpage DSA [@DSA.UEH]", "B. Hotline (028) 3829 5299; email: dsa@ueh.edu.vn; Fanpage DSA [@DSA.UEH]", "C. Hotline (028) 7306 1976; email: dsa@ueh.edu.vn", "D. Hotline (028) 3829 5299; email: dsa@ueh.edu.vn; fanpage UEH [@DHKT.UEH]; Fanpage DSA [@DSA.UEH]" },

{ "A. Cả [] và [] đều sai", "B. Cả [] và [] đều đúng", "C. Người học bị trừ 30% điểm học phần (tỷ lệ trừ do Giảng viên thông báo từ đầu môn học)", "D. Người học phải viết lại, chỉnh sửa lại bài" },

{ "A. Phòng Marketing – Truyền thông", "B. DSA", "C. Văn phòng trường", "D. Phòng Đào tạo" },

{ "A. TOEIC; IELTS; TOEFL IBT; PTE", "B. IELTS; TOEFL; VPET", "C. TOEIC; TOEFL; VPET", "D. TOEIC; IELTS; TOEFL" },

{ "A. Tất cả các mặt đã nêu", "B. Đánh giá về ý thức học tập; Đánh giá về ý thức và kết quả chấp hành nội quy, quy chế nhà trường;", "C. Đánh giá về ý thức và kết quả tham gia các hoạt động chính trị – xã hội, văn hoá, văn nghệ, thể thao, phòng chống tệ nạn xã hội; Đánh giá về phẩm chất công dân và quan hệ với cộng đồng;", "D. Đánh giá về ý thức và kết quả tham gia phụ trách lớp, các đoàn thể, tổ chức trong nhà trường hoặc đạt được thành tích đặc biệt trong học tập, rèn luyện của sinh viên;" },

{ "A. Tất cả đều đúng", "B. Chương trình tín dụng học tập", "C. Vay vốn Ngân hàng Chính sách xã hội", "D. Chương trình học bổng Hỗ trợ học tập" },

{ "A. Tất cả các tổ chức đã nêu", "B. Đảng bộ", "C. Đoàn Thanh niên", "D. Hội Sinh viên" },

{ "A. Điểm trung bình học tập tích lũy và điểm rèn luyện của học kỳ trước học kỳ xét.", "B. Điểm trung bình học tập và điểm rèn luyện toàn khóa học tích lũy đến thời điểm xét", "C. Điểm trung bình học tập tích lũy của học kỳ trước và điểm rèn luyện toàn khóa học tích lũy đến thời điểm xét", "D. Điểm trung bình tích lũy, điểm rèn luyện của học kỳ trước, hoàn cảnh gia đình khó khăn" },

{ "A. Tất cả các đáp án đã nêu", "B. Nghiên cứu sinh, Học viên cao học", "C. Sinh viên chính quy", "D. Giảng viên UEH" },

{ "A. Đúng", "B. Sai", "C. Chỉ có quy định chuẩn đầu ra tiếng Anh đối với chương trình tiếng Anh toàn phần", "D. Không có quy định về chuẩn đầu ra tiếng Anh" },

{ "A. CLB Tiếng Anh - Bell club", "B. CLB Bất động sản", "C. Nhóm sinh viên nghiên cứu thuế", "D. CLB Nhân sự khởi nghiệp" },

{ "A. 4 (Trực tiếp tại văn phòng của DSA; Trực tuyến qua nền tảng MS. Teams; Hotline: 028.7306.1976 và Livestream, talkshow phát sóng trên trang fanpage DSA)", "B. 2 (Trực tiếp tại văn phòng của DSA; Online qua nền tảng MS. Teams)", "C. 3 (Trực tiếp tại văn phòng của DSA; Online qua nền tảng MS. Teams; Hotline: 028.7306.1976)", "D. 1 (Trực tiếp tại văn phòng của DSA)" },

{ "A. 2 mức: Xuất sắc, Giỏi", "B. 2 mức: Giỏi, Khá", "C. 2 mức: Toàn phần, bán phần", "D. Tất cả đều sai" },

{ "A. 3 mức: Xuất sắc, Giỏi, Khá", "B. 2 mức: Giỏi, Khá", "C. 3 mức: Xuất sắc, Toàn phần, bán phần", "D. Tất cả đều sai" },

{ "A. 6", "B. 5", "C. 8", "D. 10" },

{ "A. Tham gia vào Fanpage DSA", "B. Tham gia vào trang mạng xã hội DSA", "C. Tham gia vào cộng đồng người học UEH", "D. Tất cả đều đúng" },

{ "A. Đăng ký tài khoản trên hệ thống", "B. Đăng ký tài khoản trên website", "C. Đăng ký tài khoản trên trang mạng xã hội", "D. Đăng ký tài khoản trên Cổng thông tin việc làm" },

{ "A. Cả 2 trường đã nêu", "B. Đường Nguyễn Đình Chiểu, Quận 3", "C. Đường Hoàng Diệu, Quận 4", "D. Đường Hoàng Văn Thụ, Quận Tân Bình" },

{ "A. Cơ sở 1: 279 Nguyễn Tri Phương, Quận 10, TP.HCM", "B. Cơ sở 2: 32 Nguyễn Văn Linh, Quận 7, TP.HCM", "C. Cơ sở 3: 15 Đường số 3, P. 9, Quận 8, TP.HCM", "D. Cơ sở 4: 76 Hoàng Diệu, Quận 4, TP.HCM" },

{ "A. 279 Nguyễn Tri Phương, P.5, Q.10, TP.HCM", "B. 32 Nguyễn Văn Linh, Q.7, TP.HCM", "C. 15 Đường số 3, P. 9, Q.8, TP.HCM", "D. 76 Hoàng Diệu, Q.4, TP.HCM" },

{ "A. Tổ chức các hoạt động văn hóa, thể thao", "B. Tham gia vào các hoạt động tình nguyện", "C. Cả hai đều đúng", "D. Tham gia các hoạt động nghiên cứu khoa học" },

{ "A. Hoạt động tình nguyện", "B. Hoạt động ngoại khóa", "C. Hoạt động văn nghệ", "D. Tất cả đều sai" },

{ "A. Hoạt động tình nguyện", "B. Hoạt động ngoại khóa", "C. Cả hai đều đúng", "D. Tất cả đều sai" },

{ "A. Sinh viên tham gia các cuộc thi quốc gia và quốc tế", "B. Sinh viên tham gia các hoạt động văn hóa - xã hội", "C. Sinh viên tham gia các hoạt động thể thao", "D. Tất cả đều sai" },

{ "A. Đúng", "B. Sai", "C. Không có quy định xếp hạng tốt nghiệp", "D. Chỉ có loại xuất sắc bị giảm"},

{ "A. Sinh viên có thể đăng ký tham gia các hoạt động xã hội ngoài giờ học", "B. Sinh viên phải tham gia các hoạt động này theo quy định của nhà trường", "C. Sinh viên có thể tham gia vào các hoạt động này một cách tự nguyện", "D. Sinh viên không được tham gia các hoạt động ngoài giờ học" },

{ "A. Tổ chức các hoạt động văn hóa thể thao", "B. Tổ chức các hoạt động tình nguyện", "C. Tổ chức các hoạt động nghiên cứu khoa học", "D. Tất cả đều đúng" },

{ "A. Tham gia các hoạt động phong trào", "B. Tham gia các hoạt động tình nguyện", "C. Tham gia các hoạt động văn hóa nghệ thuật", "D. Tất cả đều sai" },

{ "A. Cung cấp dịch vụ thông tin và hỗ trợ người học", "B. Tư vấn tâm lý cho sinh viên", "C. Tổ chức các hoạt động ngoại khóa", "D. Tất cả đều đúng" },

{ "A. Không phải là sinh viên", "B. Có thể là sinh viên hoặc đã tốt nghiệp", "C. Chỉ là sinh viên", "D. Không rõ" },

{ "A. Tham gia các hội thảo, hội nghị", "B. Tổ chức các buổi tọa đàm", "C. Cả hai đều đúng", "D. Không tổ chức bất kỳ hoạt động nào" },

{ "A. 8%", "B. 10%", "C. 12%", "D. 15%" },

{ "A. 3/4", "B. 1/2", "C. 1/4", "D. 2/3" },

{ "A. 8%", "B. 10%", "C. 12%", "D. 15%" },

{ "A. Tham gia vào các hoạt động tình nguyện", "B. Tham gia vào các hoạt động văn hóa - xã hội", "C. Cả hai đều đúng", "D. Không tham gia hoạt động nào" },

{ "A. Đúng", "B. Sai", "C. Không có quy định xếp hạng tốt nghiệp", "D. Chỉ có loại xuất sắc bị giảm"},

{ "A. Tham gia các cuộc thi", "B. Tham gia vào các hoạt động tình nguyện", "C. Không tham gia bất kỳ hoạt động nào", "D. Tất cả đều sai" },

{ "A. Tham gia các hoạt động nghiên cứu khoa học", "B. Tổ chức các hoạt động ngoại khóa", "C. Cả hai đều đúng", "D. Không tham gia hoạt động nào" },

{ "A. Không có thông tin", "B. Có thông tin về các hoạt động", "C. Có thông tin về các dịch vụ", "D. Không rõ" },

{ "A. Tổ chức các hoạt động ngoại khóa", "B. Cung cấp dịch vụ hỗ trợ sinh viên", "C. Tư vấn cho sinh viên", "D. Tất cả đều đúng" },

{ "A. 3%", "B. 5%", "C. 10%", "D. 15%" },

{ "A. Đúng", "B. Sai", "C. Không rõ", "D. Tất cả đều sai" },

{ "A. 8%", "B. 10%", "C. 12%", "D. 15%" },

{ "A. Không có thông tin", "B. Có thông tin về các hoạt động", "C. Có thông tin về các dịch vụ", "D. Không rõ" }

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
                    // Câu trả lời đúng
                    if (i == 14)
                    {
                        Console.WriteLine("Chúc mừng! Bạn đã trở thành Tỷ phú và giành được 1,000,000,000 VND!");
                    }
                }
                else
                {
                    // Câu trả lời sai
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

            // Shuffle câu hỏi (Thuật toán Fisher-Yates)
            for (int i = tatCaCauHoi.Length - 1; i > 0; i--)
            {
                int j = ngauNhien.Next(i + 1);
                int temp = tatCaCauHoi[i];
                tatCaCauHoi[i] = tatCaCauHoi[j];
                tatCaCauHoi[j] = temp;
            }

            // Chọn 15 câu hỏi đầu tiên sau khi xáo trộn
            int[] cacCauHoiDaChon = new int[15];
            Array.Copy(tatCaCauHoi, cacCauHoiDaChon, 15);
            return cacCauHoiDaChon;
        }

        static void HienThiCauHoi(int chiSoCauHoi, int soCauHoi)
        {
            Console.WriteLine($"\nCâu hỏi {soCauHoi}: {cauHoi[chiSoCauHoi]}");
            for (int j = 0; j < 4; j++)
            {
                Console.WriteLine(luaChon[chiSoCauHoi, j]);
            }
            Console.WriteLine();
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
            // Chọn ngẫu nhiên một đáp án sai để hiển thị cùng đáp án đúng
            int dapAnSai;
            do
            {
                dapAnSai = ngauNhien.Next(0, 4);
            } while (dapAnDung[chiSoCauHoi] == (char)(dapAnSai + 'A'));

            Console.WriteLine("Trợ giúp 50/50 đã loại bỏ 2 đáp án sai:");
            if (dapAnSai < dapAnDung[chiSoCauHoi] - 'A')
            {
                Console.WriteLine(luaChon[chiSoCauHoi, dapAnSai]);
                Console.WriteLine(luaChon[chiSoCauHoi, dapAnDung[chiSoCauHoi] - 'A']);
            }
            else
            {
                Console.WriteLine(luaChon[chiSoCauHoi, dapAnDung[chiSoCauHoi] - 'A']);
                Console.WriteLine(luaChon[chiSoCauHoi, dapAnSai]);
            }
        }

        static char LayDapAn()
        {
            char dapAn;
            do
            {
                Console.Write("Câu trả lời của bạn (chọn A, B, C hoặc D): ");
                dapAn = char.ToUpper(Console.ReadLine()[0]);
            } while (dapAn < 'A' || dapAn > 'D');
            return dapAn;
        }

        static bool KiemTraDapAn(char dapAn, int chiSoCauHoi, ref int giaTriHienTai, ref int giaTriMocCuoi, int i)
        {
            if (dapAn == dapAnDung[chiSoCauHoi])
            {
                giaTriHienTai = giaThuong[i];
                Console.WriteLine($"Chúc mừng! Bạn đã giành được {giaTriHienTai} VND.");

                // Kiểm tra mốc quan trọng
                if (Array.IndexOf(mocQuanTrong, i + 1) != -1)
                {
                    giaTriMocCuoi = giaTriHienTai;
                    Console.WriteLine($"Bạn đã vượt qua mốc quan trọng và giành được số tiền {giaTriMocCuoi} VND!");
                }

                // Ký tấm séc từ câu hỏi 10 trở đi
                if (i >= 9) Console.WriteLine($"Người dẫn chương trình đã ký tấm séc trị giá {giaTriHienTai} VND.");
                return true; // Câu trả lời đúng
            }
            else
            {
                Console.WriteLine("Rất tiếc, bạn đã trả lời sai.");
                Console.WriteLine($"Đáp án đúng là {dapAnDung[chiSoCauHoi]}");
                Console.WriteLine(i < 5 ? "Bạn ra về tay trắng." : $"Bạn sẽ ra về với số tiền {giaTriMocCuoi} VND.");
                return false; // Câu trả lời sai
            }
        }

        static bool HỏiDừngTroChoi(int giaTriHienTai)
        {
            Console.Write("\nBạn có muốn dừng cuộc chơi và lấy số tiền hiện tại không? (y/n): ");
            string dừngTroChoi = Console.ReadLine();
            return dừngTroChoi.ToLower() == "y";
        }
    }
}

