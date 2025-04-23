namespace RPG_SJ
{
    internal partial class Program
    {
        class Intro
        {
            public static void Start()
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(@"
  _______ ________   _________            _____  _____   _____ 
 |__   __|  ____\ \ / /__   __|          |  __ \|  __ \ / ____|
    | |  | |__   \ V /   | |     ______  | |__) | |__) | |  __ 
    | |  |  __|   > <    | |    |______| |  _  /|  ___/| | |_ |
    | |  | |____ / . \   | |             | | \ \| |    | |__| |
    |_|  |______/_/ \_\  |_|             |_|  \_\_|     \_____|
                                                               
                                                               
                                                          
                                                          
                ");
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
                    Console.WriteLine(line);
                    Thread.Sleep(1500);
                }

                Console.WriteLine("\nPress any key to begin your journey...");
                Console.ReadKey();
            }
        }
    }
}