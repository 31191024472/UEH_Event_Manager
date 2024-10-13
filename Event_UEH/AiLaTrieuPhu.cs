using System;

namespace Event_UEH
{
    class AiLaTrieuPhu
    {
        static int[] giaThuong = { 500000, 1000000, 2000000, 3000000, 5000000, 10000000, 14000000, 22000000, 30000000, 50000000, 100000000, 200000000, 400000000, 800000000, 1000000000 };
        static int[] mocQuanTrong = { 5, 10, 15 };
        static string[] cauHoi = {
                "Thủ đô của Việt Nam là gì?",
                "Động vật nào được gọi là 'Vua của rừng xanh'?",
                "Ai đã viết vở kịch nổi tiếng 'Romeo và Juliet'?",
                "Hành tinh nào lớn nhất trong hệ mặt trời?",
                "Loài chim nào không biết bay?",
                "Chất nào cấu tạo nên phần lớn cơ thể con người?",
                "Biển nào lớn nhất thế giới?",
                "Nhà văn nào viết tiểu thuyết 'Chiến tranh và hòa bình'?",
                "Nước nào là nước đông dân nhất thế giới?",
                "Tác giả của bản 'Tuyên ngôn Độc lập' Hoa Kỳ là ai?",
                "Núi nào cao nhất thế giới?",
                "Tác phẩm 'Thần khúc' là của tác giả nào?",
                "Quốc gia nào có diện tích lớn nhất thế giới?",
                "Khoa học nào nghiên cứu về vũ trụ?",
                "Loài động vật nào có vú nhưng đẻ trứng?",
                "Bộ phim nào đoạt giải Oscar cho Phim hay nhất năm 1994?",
                "Cây cầu nào nổi tiếng ở San Francisco?",
                "Tác giả của tác phẩm 'Don Quixote' là ai?",
                "Loại nhạc cụ nào mà Ludwig van Beethoven chơi?",
                "Bộ xương người có bao nhiêu chiếc xương?",
                "Ai là nhà khoa học đã phát hiện ra penicillin?",
                "Đơn vị đo lường điện trở là gì?",
                "Nhà thơ nổi tiếng Nguyễn Du là tác giả của tác phẩm nào?",
                "Ai là người sáng lập hãng Microsoft?",
                "Châu lục nào nhỏ nhất trên thế giới?",
                "Sông nào dài nhất thế giới?",
                "Quốc gia nào tổ chức World Cup 2018?",
                "Quốc kỳ của nước nào có hình chiếc lá phong?",
                "Ngọn núi nào cao nhất ở Nhật Bản?",
                "Hành tinh nào gần Mặt Trời nhất?",
                "Ai là nhà khoa học đề xuất thuyết tương đối?",
                "Loại kim loại nào nhẹ nhất?",
                "Quốc gia nào có dân số lớn thứ 2 thế giới?",
                "Bộ phim nào đoạt giải Oscar cho Phim hay nhất năm 1997?",
                "Quốc gia nào sản xuất nhiều cà phê nhất thế giới?",
                "Quốc gia nào được gọi là Đất nước của Kim tự tháp?",
                "Sông nào chảy qua thành phố London?",
                "Quốc gia nào có diện tích nhỏ nhất trên thế giới?",
                "Ai là họa sĩ nổi tiếng người Tây Ban Nha?",
                "Ai là tổng thống thứ 16 của Hoa Kỳ?",
                "Quốc gia nào có hình thức chính phủ quân chủ lập hiến?",
                "Bộ môn nào là phần thi trong Olympic Mùa đông?",
                "Ai là nữ hoàng nước Anh vào thế kỷ 19?",
                "Quốc gia nào nổi tiếng với ngành công nghiệp thời trang ở Milan?",
                "Ai đã phát minh ra bóng đèn điện?",
                "Quốc gia nào nổi tiếng với món sushi?",
                "Quốc gia nào có hệ thống đường sắt dài nhất thế giới?",
                "Ngọn núi cao nhất ở châu Âu là gì?",
                "Ai đã viết bản giao hưởng thứ 9?"
            };
        static char[] dapAnDung = {
                'A', // Thủ đô của Việt Nam là gì?
                'C', // Động vật nào được gọi là 'Vua của rừng xanh'?
                'B', // Ai đã viết vở kịch nổi tiếng 'Romeo và Juliet'?
                'C', // Hành tinh nào lớn nhất trong hệ mặt trời?
                'B', // Loài chim nào không biết bay?
                'B', // Chất nào cấu tạo nên phần lớn cơ thể con người?
                'D', // Biển nào lớn nhất thế giới?
                'A', // Nhà văn nào viết tiểu thuyết 'Chiến tranh và hòa bình'?
                'C', // Nước nào là nước đông dân nhất thế giới?
                'B', // Tác giả của bản 'Tuyên ngôn Độc lập' Hoa Kỳ là ai?
                'A', // Núi nào cao nhất thế giới?
                'A', // Tác phẩm 'Thần khúc' là của tác giả nào?
                'B', // Quốc gia nào có diện tích lớn nhất thế giới?
                'C', // Khoa học nào nghiên cứu về vũ trụ?
                'D', // Loài động vật nào có vú nhưng đẻ trứng?
                'A', // Bộ phim nào đoạt giải Oscar cho Phim hay nhất năm 1994?
                'A', // Cây cầu nào nổi tiếng ở San Francisco?
                'C', // Tác giả của tác phẩm 'Don Quixote' là ai?
                'A', // Loại nhạc cụ nào mà Ludwig van Beethoven chơi?
                'B', // Bộ xương người có bao nhiêu chiếc xương?
                'B', // Ai là nhà khoa học đã phát hiện ra penicillin?
                'A', // Đơn vị đo lường điện trở là gì?
                'A', // Nhà thơ nổi tiếng Nguyễn Du là tác giả của tác phẩm nào?
                'B', // Ai là người sáng lập hãng Microsoft?
                'D', // Châu lục nào nhỏ nhất trên thế giới?
                'A', // Sông nào dài nhất thế giới?
                'A', // Quốc gia nào tổ chức World Cup 2018?
                'A', // Quốc kỳ của nước nào có hình chiếc lá phong?
                'A', // Ngọn núi nào cao nhất ở Nhật Bản?
                'C', // Hành tinh nào gần Mặt Trời nhất?
                'C', // Ai là nhà khoa học đề xuất thuyết tương đối?
                'C', // Loại kim loại nào nhẹ nhất?
                'B', // Quốc gia nào có dân số lớn thứ 2 thế giới?
                'A', // Bộ phim nào đoạt giải Oscar cho Phim hay nhất năm 1997?
                'B', // Quốc gia nào sản xuất nhiều cà phê nhất thế giới?
                'A', // Quốc gia nào được gọi là Đất nước của Kim tự tháp?
                'B', // Sông nào chảy qua thành phố London?
                'A', // Quốc gia nào có diện tích nhỏ nhất trên thế giới?
                'B', // Ai là họa sĩ nổi tiếng người Tây Ban Nha?
                'C', // Ai là tổng thống thứ 16 của Hoa Kỳ?
                'B', // Quốc gia nào có hình thức chính phủ quân chủ lập hiến?
                'A', // Bộ môn nào là phần thi trong Olympic Mùa đông?
                'A', // Ai là nữ hoàng nước Anh vào thế kỷ 19?
                'B', // Quốc gia nào nổi tiếng với ngành công nghiệp thời trang ở Milan?
                'A', // Ai đã phát minh ra bóng đèn điện?
                'B', // Quốc gia nào nổi tiếng với món sushi?
                'B', // Quốc gia nào có hệ thống đường sắt dài nhất thế giới?
                'A', // Ngọn núi cao nhất ở châu Âu là gì?
                'B'  // Ai đã viết bản giao hưởng thứ 9?
            };
        static string[,] luaChon = {
                { "A. Hà Nội", "B. TP. Hồ Chí Minh", "C. Đà Nẵng", "D. Huế" },
                { "A. Voi", "B. Hổ", "C. Sư tử", "D. Báo" },
                { "A. Lev Tolstoy", "B. William Shakespeare", "C. Mark Twain", "D. Charles Dickens" },
                { "A. Trái Đất", "B. Sao Hỏa", "C. Sao Mộc", "D. Sao Kim" },
                { "A. Đại bàng", "B. Đà điểu", "C. Chim cắt", "D. Cú" },
                { "A. Protein", "B. Nước", "C. Canxi", "D. Oxy" },
                { "A. Biển Đông", "B. Biển Đỏ", "C. Biển Caribe", "D. Thái Bình Dương" },
                { "A. Lev Tolstoy", "B. Victor Hugo", "C. Ernest Hemingway", "D. Fyodor Dostoevsky" },
                { "A. Hoa Kỳ", "B. Ấn Độ", "C. Trung Quốc", "D. Indonesia" },
                { "A. Benjamin Franklin", "B. Thomas Jefferson", "C. George Washington", "D. Abraham Lincoln" },
                { "A. Everest", "B. Kilimanjaro", "C. Phú Sĩ", "D. K2" },
                { "A. Dante Alighieri", "B. Homer", "C. Virgil", "D. John Milton" },
                { "A. Hoa Kỳ", "B. Nga", "C. Trung Quốc", "D. Canada" },
                { "A. Địa lý học", "B. Sinh học", "C. Thiên văn học", "D. Khí tượng học" },
                { "A. Thỏ", "B. Dơi", "C. Chuột chũi", "D. Thú mỏ vịt" },
                { "A. Forrest Gump", "B. Titanic", "C. Braveheart", "D. Pulp Fiction" },
                { "A. Golden Gate", "B. Brooklyn", "C. London", "D. Harbour" },
                { "A. William Shakespeare", "B. Dante Alighieri", "C. Miguel de Cervantes", "D. Charles Dickens" },
                { "A. Piano", "B. Violin", "C. Sáo", "D. Ghi-ta" },
                { "A. 205", "B. 206", "C. 207", "D. 208" },
                { "A. Albert Einstein", "B. Alexander Fleming", "C. Louis Pasteur", "D. Isaac Newton" },
                { "A. Ohm", "B. Volt", "C. Watt", "D. Joule" },
                { "A. Truyện Kiều", "B. Lục Vân Tiên", "C. Chí Phèo", "D. Số đỏ" },
                { "A. Steve Jobs", "B. Bill Gates", "C. Mark Zuckerberg", "D. Elon Musk" },
                { "A. Châu Á", "B. Châu Âu", "C. Châu Phi", "D. Châu Úc" },
                { "A. Sông Nile", "B. Sông Amazon", "C. Sông Mississippi", "D. Sông Dương Tử" },
                { "A. Nga", "B. Brazil", "C. Hoa Kỳ", "D. Qatar" },
                { "A. Canada", "B. Úc", "C. Nhật Bản", "D. Anh" },
                { "A. Núi Phú Sĩ", "B. Núi Everest", "C. Núi Kilimanjaro", "D. Núi Elbrus" },
                { "A. Sao Kim", "B. Sao Hỏa", "C. Sao Thủy", "D. Sao Mộc" },
                { "A. Isaac Newton", "B. Nikola Tesla", "C. Albert Einstein", "D. Stephen Hawking" },
                { "A. Sắt", "B. Vàng", "C. Nhôm", "D. Thủy ngân" },
                { "A. Hoa Kỳ", "B. Ấn Độ", "C. Brazil", "D. Indonesia" },
                { "A. Titanic", "B. Braveheart", "C. Gladiator", "D. Avatar" },
                { "A. Việt Nam", "B. Brazil", "C. Colombia", "D. Ethiopia" },
                { "A. Ai Cập", "B. Peru", "C. Mexico", "D. Trung Quốc" },
                { "A. Sông Seine", "B. Sông Thames", "C. Sông Volga", "D. Sông Danube" },
                { "A. Vatican", "B. Monaco", "C. Liechtenstein", "D. Luxembourg" },
                { "A. Salvador Dali", "B. Pablo Picasso", "C. Francisco Goya", "D. Joan Miro" },
                { "A. George Washington", "B. Franklin Roosevelt", "C. Abraham Lincoln", "D. John Adams" },
                { "A. Nhật Bản", "B. Thụy Điển", "C. Anh", "D. Úc" },
                { "A. Trượt tuyết", "B. Trượt ván", "C. Trượt băng", "D. Khúc côn cầu" },
                { "A. Nữ hoàng Victoria", "B. Nữ hoàng Elizabeth I", "C. Nữ hoàng Mary", "D. Nữ hoàng Anne" },
                { "A. Pháp", "B. Ý", "C. Tây Ban Nha", "D. Đức" },
                { "A. Thomas Edison", "B. Nikola Tesla", "C. Alexander Bell", "D. Albert Einstein" },
                { "A. Trung Quốc", "B. Nhật Bản", "C. Thái Lan", "D. Việt Nam" },
                { "A. Hoa Kỳ", "B. Nga", "C. Ấn Độ", "D. Canada" },
                { "A. Mont Blanc", "B. Everest", "C. Phú Sĩ", "D. Kilimanjaro" },
                { "A. Wolfgang Amadeus Mozart", "B. Ludwig van Beethoven", "C. Franz Schubert", "D. Johann Sebastian Bach" }
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
