namespace RPG_SJ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("🌟 스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("이제 전투를 시작할 수 있습니다.\n");

            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 전투 시작\n");

            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.WriteLine("\n[상태 보기 화면으로 이동합니다...]\n");
                    ShowStatus();  // 상태 확인 함수
                    break;

                case "2":
                    Console.WriteLine("\n[전투를 시작합니다...]\n");
                    StartBattle();  // 전투 시작 함수
                    break;

                default:
                    Console.WriteLine("\n❌ 잘못된 입력입니다.\n");
                    ShowStartMenu();  // 다시 메뉴 출력
                    break;
            }
        }
    }
}
