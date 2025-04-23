namespace Sparta_RPG2_
{
    class Intro
    {
        public static void Start()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;

            string[] title = {
            " _______ ________   _________            _____  _____   _____ ",
            "|__   __|  ____\\ \\ / /__   __|          |  __ \\|  __ \\ / ____|",
            "   | |  | |__   \\ V /   | |     ______  | |__) | |__) | |  __ ",
            "   | |  |  __|   > <    | |    |______| |  _  /|  ___/| | |_ |",
            "   | |  | |____ / . \\   | |             | | \\ \\| |    | |__| |",
            "   |_|  |______/_/ \\_\\  |_|             |_|  \\_\\_|     \\_____|"
        };

            foreach (string line in title)
            {
                CenteredWriteLine(line);
            }

            for (int i = 0; i < 5; i++)
                Console.WriteLine();

            Console.ResetColor();
            Thread.Sleep(1000);

            string[] introLines = {
            "어둠 속에서 눈을 떴을 때...",
            "당신은 낯선 던전의 입구에 서 있었습니다.",
            "전설에 따르면 이곳엔 고대의 힘이 잠들어 있다고 합니다.",
            "지금, 당신의 여정이 시작됩니다..."
        };

            foreach (var line in introLines)
            {
                TypeWriterCenteredLine(line, 40);  // 한 글자씩 출력
                Thread.Sleep(500);
            }

            Console.WriteLine();
            TypeWriterCenteredLine("Press any key to begin your journey...", 30);
            Console.ReadKey();
        }

        // ⭕ 가운데 정렬 + 타자기 출력
        private static void TypeWriterCenteredLine(string text, int delay = 50)
        {
            int width = Console.WindowWidth;
            int displayWidth = GetDisplayWidth(text);
            int padding = Math.Max(0, (width - displayWidth) / 2);
            Console.Write(new string(' ', padding));

            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }

        // 🔄 일반 가운데 정렬 출력
        private static void CenteredWriteLine(string text)
        {
            int width = Console.WindowWidth;
            int displayWidth = GetDisplayWidth(text);
            int padding = Math.Max(0, (width - displayWidth) / 2);
            Console.WriteLine(new string(' ', padding) + text);
        }

        // 🔍 출력 폭 계산 (한글 등 너비 2 처리)
        private static int GetDisplayWidth(string text)
        {
            int width = 0;
            foreach (char c in text)
            {
                width += IsFullWidth(c) ? 2 : 1;
            }
            return width;
        }

        private static bool IsFullWidth(char c)
        {
            return c >= 0x1100 &&
                   (c <= 0x115F || // 한글 자모
                    c == 0x2329 || c == 0x232A ||
                    (c >= 0x2E80 && c <= 0xA4CF && c != 0x303F) ||
                    (c >= 0xAC00 && c <= 0xD7A3) || // 한글 완성형
                    (c >= 0xF900 && c <= 0xFAFF) ||
                    (c >= 0xFE10 && c <= 0xFE19) ||
                    (c >= 0xFF01 && c <= 0xFF60) ||
                    (c >= 0xFFE0 && c <= 0xFFE6));
        }
    }
}
