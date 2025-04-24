namespace Sparta_RPG2_
{
    internal partial class Program
    {
        static void ShowCreatMe(Character player)
        {
            Console.Clear();
            Console.WriteLine("캐릭터를 생성합니다.\n");
            Console.Write("이름을 입력하세요 : ");
            player.Name = Console.ReadLine() ?? "함장";
            Console.WriteLine("직업을 선택하세요");
            Console.WriteLine("1.팔랑크스 중보병 -  긴 창과 방패, 중갑으로 무장한 선봉. 무너지지 않는 전장의 벽.");
            Console.WriteLine("2.아레스의 예언자 -  아레스 신의 계시를 받아, 신비한 힘으로 전장을 꿰뚫는 마술사.");
            Console.WriteLine("3.라코니아 순찰자 -  그림자 속에서 움직이며, 치명적인 화살로 적을 사냥하는 궁수.");
            player.Job = Console.ReadLine() ?? "1";

            switch (player.Job)
            {
                case "1":
                    player.Attack = 10; player.Defense = 10; player.HP = 80; player.MaxHP = 80; player.MP = 50; player.MaxMP = 50; player.JobName = "팔랑크스 중보병"; break;
                case "2":
                    player.Attack = 5; player.Defense = 3; player.HP = 50; player.MaxHP = 50; player.MP = 120; player.MaxMP = 120; player.JobName = "아레스의 예언자"; break;
                case "3":
                    player.Attack = 15; player.Defense = 6; player.HP = 65; player.MaxHP = 65; player.MP = 80; player.MaxMP = 80;  player.JobName = "라코니아 순찰자"; break;
                case "21":
                    player.Attack = 21; player.Defense = 21; player.HP = 121; player.MaxHP = 121; player.MP = 210; player.MaxMP = 210; player.JobName = "스파르타의 왕"; break; 

                default:
                    Console.WriteLine("잘못된 직업입니다. 팔랑크스 중보병으로 설정합니다.");
                    player.Job = "1";
                    player.Attack = 10; player.Defense = 10; player.HP = 80; player.MaxHP = 80; player.MP = 50; player.MaxMP = 50; break;
            }
            if (player.Job == "21")
            {
                Console.WriteLine("THIS IS SPARTA!!!!");
            }

            Console.WriteLine($"\n캐릭터 생성 완료! 이름 : {player.Name}, 직업 : {player.JobName}");
            Console.WriteLine("\n0. 스파르타 마을로 이동");
            while (Console.ReadLine() != "0") ;
        }
    }
}
