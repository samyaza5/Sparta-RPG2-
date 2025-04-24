class Intro
{
    public static void Start()
    {
        Console.ReadKey();
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;       

        string[] introLines = {
           "...기원전 480년.",
   "\n",
   "스파르타의 전사 300명이 페르시아의 대군을 물리쳤다..",
   "그러나 기쁨도 잠시뿐. 전쟁은 끝이지 않고, 나라는 쇠락했다...",
   "스파르타의 적들이 사방에서 다가오고, 전사들은 모래알처럼 흩어졌다.",
   "이제 델포이 사제들이 마지막 아레스의 신탁을 전한다.",
   "\n",

   "진정한 전사가 검이 아닌 의지로 싸운다.",
   "이제 형제들이 방패로 다시 벽을 쌓고,",
   "신의 피를 이은 자, 그 혼이 다시 깨어나리라.",

   "\n",
   "당신은 레오니다스 왕의 마지막 혈통, 그 이야기가..",
   "지금 시작되려고 한다. . .",
   "\n",

        };

        Console.WriteLine("▶ 인트로 스토리를 보시려면 잠시 기다려주세요...");
        Console.WriteLine("⏩ [S] 키를 누르면 인트로를 스킵할 수 있습니다.\n");
        Thread.Sleep(2000);

        bool isSkipped = false;

        for (int i = 0; i < introLines.Length; i++)
        {
            // 사용자 입력 체크
            if (Console.KeyAvailable)
            {
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.S)
                {
                    isSkipped = true;
                    break;
                }
            }

            TypeWriterCenteredLine(introLines[i], 40);
            Thread.Sleep(500);
        }

        if (isSkipped)
        {
            Console.Clear();
            Console.WriteLine("[⏩ 인트로가 스킵되었습니다.]\n");
            Thread.Sleep(1000);
            Console.ResetColor();
            return; // ⛔ 인트로 전체 종료
        }
        else
        {
            // ✨ 먼저 대사 출력
            foreach (var line in introLines)
            {
                TypeWriterCenteredLine(line, 40);  // 한 글자씩 출력
                Thread.Sleep(500);
            }
        }

        Console.Clear();
        Console.ResetColor();

        string[] title = {


"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",
"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢿⣿⣿⣿⣿⣿⣿⣯⣯⣿⣻⣿⡿⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",
"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣽⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣯⣿⡿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",
"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣹⣿⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣝⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",
"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣝⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿",
"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⡿⣻⣿⣿⣿⣿⣿⣿⣿",
"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⠟⢱⣿⣻⡏⢻⣿⣿⣿⣿⠿⠏⠙⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣿⣿⣿⣿⣿⣿⣿",
"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⠁⠈⠀⠀⠈⠉⠁⠀⠈⠛⠋⠉⠀⠀⠀⣼⣿⣿⣿⣿⣿⣿⣯⣿⣿⣿⣿⣿⣿⣽⣿⣿⣿⣿⣿⣿",
"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⠻⣿⣿⣿⠿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣟⣿⣿⣿⣿⣿⣿",
"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠹⠧⠤⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",
"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢂⠀⢀⣀⣀⡀⢹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",
"⣿⣿⣿⣿⣿⣿⣟⣿⣿⣿⣯⣴⣶⣤⣤⣦⣤⣤⣄⣀⡀⠀⠀⢀⣀⣴⣦⣶⣿⣿⣿⠿⠿⢿⣷⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",
"⣿⣿⣿⣿⣽⣿⣿⣿⣿⡟⢻⠏⠉⠙⠻⣿⣿⣿⣿⣿⡗⠀⣴⣿⣿⣿⣿⣿⣿⣿⣿⣶⣶⣶⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢿⣿⣿⣿⣿",
"⣿⣿⣿⣇⠙⣿⣿⣿⣿⡇⢸⣿⣿⡿⣿⣿⣿⣿⣿⣿⣧⠀⠀⣿⣿⣿⣿⣿⢿⣿⣿⣿⣿⣿⣿⣿⣟⣁⣺⣿⣿⣿⣿⠟⠀⠀⣿⣿⣿⣿",
"⣿⣿⣿⣿⣮⣿⣿⣿⣿⠁⠈⠿⠛⢃⣉⣻⣿⣿⣿⠟⠃⠀⠀⠘⢿⣿⡿⢷⣿⣿⣿⣿⣿⣭⣤⣷⣽⣿⣿⣿⣿⣿⣿⣶⣦⢀⣼⣿⣿⣿",
"⣿⣿⣿⣿⣿⣿⣿⣿⣿⡀⠀⠀⠐⠛⠛⠛⠛⠉⠀⠀⠀⠀⠀⠀⠈⣯⣄⠀⠀⠉⠉⠉⠀⢀⣍⢼⣿⣿⣿⣿⣿⣿⣿⣿⡿⠈⣿⣿⣿⣿",
"⣿⣿⣿⣿⣿⣿⣿⡿⢿⣷⠀⠀⠀⠀⠀⠀⠀⢀⡔⠁⠀⠀⠀⠀⠀⠋⠻⣧⡄⠀⠀⢰⠀⠀⢻⣛⣿⣿⣿⣿⣿⣿⣿⣿⡧⣶⣿⣿⣿⣿",
"⣿⣿⣿⣿⣿⣿⣿⣿⣾⣿⠀⠀⠀⠀⠀⠀⣠⣿⡀⠀⠀⠀⠀⣠⣴⣤⣤⣿⣿⣦⣀⠸⣇⢀⣠⣿⣿⣿⣿⣿⣿⡿⢠⡟⢀⡻⣿⣿⣿⣿",
"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⠀⠀⠀⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡷⠘⠁⠘⢠⣿⣿⣿⣿",
"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠂⠀⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⡄⢠⣗⣼⣿⣿⣿⣿",
"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣅⠀⠀⣿⣿⣿⣿⣿⠿⠿⠟⠻⠟⠻⠿⡿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢏⣼⣿⣿⣿⣿⣿⣿",
"⣿⣿⣿⣿⣿⣿⣿⣿⣿⢿⣿⣿⣧⣤⣿⣿⣿⣿⣿⣄⣤⣄⣀⣀⣤⣶⣶⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣏⣼⣿⣿⣿⣿⣿⣿⣿",
"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠻⣿⣿⣿⣿⣿⣿⣿⣿",
"⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⠸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",
"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣄⢻⣿⣿⣿⣿⣿⣿⢿⣟⠙⠻⣿⣿⣿⣿⣿⠿⠁⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣻⣿⣿⣿⣿⣿⣿⣿⣿⣿",
"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⢾⣿⣿⣿⣿⣿⣿⡀⠙⠄⠀⠈⠛⠋⠉⠁⠀⢀⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⣏⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",
"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣯⢸⣿⣿⣿⣿⣿⣿⣿⣶⣤⣀⣀⣀⣀⣀⣤⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",
"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣴⡽⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",
"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣗⣸⣏⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣽⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",
"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢟⢹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",
"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⢻⡏⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",
"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣛⣻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",


              "______   ___   _____  _____   _____ ______   _____ ______   ___  ______  _____   ___  ",
    "| ___ \\ / _ \\ |  __ \\|  ___| |  _  ||  ___| /  ___|| ___ \\ / _ \\ | ___ \\|_   _| / _ \\ ",
    "|  |_/// /_\\ |  |  \\/| |__   | | | || |_    \\ `--. | |_/ // /_\\ \\| |_/ /  | |  / /_\\ \\",
    "|    / |  _  || | __ |  __|  | | | ||  _|    `--. \\|  __/ |  _  ||    /   | |  |  _  |",
    "| |\\ \\ | | | || |_\\ \\| |___  \\ \\_/ /| |     /\\__/ /| |    | | | || |\\ \\   | |  | | | |",
    " \\_| \\_|\\_| |_/ \\____/\\____/   \\___/ \\_|     \\____/ \\_|    \\_| |_/\\_| \\_|  \\_/  \\_| |_/ ",
        };

        // 🏁 타이틀 빠르게 한 줄씩 출력
        foreach (string line in title)
        {
            CenteredWriteLine(line);
            Thread.Sleep(100); // 빠르게 출력
        }

        for (int i = 0; i < 3; i++)
            Console.WriteLine();

        Console.ResetColor();
        Console.WriteLine();
        TypeWriterCenteredLine("Press any key to begin your Battle...", 30);
        Console.ReadKey();
    }

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

    private static void CenteredWriteLine(string text)
    {
        int width = Console.WindowWidth;
        int displayWidth = GetDisplayWidth(text);
        int padding = Math.Max(0, (width - displayWidth) / 2);
        Console.WriteLine(new string(' ', padding) + text);
    }

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
               (c <= 0x115F ||
                c == 0x2329 || c == 0x232A ||
                (c >= 0x2E80 && c <= 0xA4CF && c != 0x303F) ||
                (c >= 0xAC00 && c <= 0xD7A3) ||
                (c >= 0xF900 && c <= 0xFAFF) ||
                (c >= 0xFE10 && c <= 0xFE19) ||
                (c >= 0xFF01 && c <= 0xFF60) ||
                (c >= 0xFFE0 && c <= 0xFFE6));
    }
}
