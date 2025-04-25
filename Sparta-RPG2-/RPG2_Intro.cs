class Intro
{
    public static void Start()
    {
        Console.ReadKey();
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;       

        string[] introLines = {
   ". . .기원전 480년.",
      "\n",
     "가증스러운 페르시아인들이 그리스를 침략했다.",
     "우리의 수는 300, 적은 그 수천배. . . 그러나. . .",
     "\n",
     "스파르타 전사는 물러서지 않는다.",
     "스파르타 전사는 포기하지 않는다.",
     "스파르타 전사는 굴복하지 않는다.",
     "\n",
     "무기를 내려 놓으라고? 내 대답은 이것이다.",
     "\n",
     "\"μ ο λ ὼ ν  λ α β έ.\"",
     "\n",
     "와서 가져가라!!!"

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
