using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sparta_RPG2_;
using static Sparta_RPG2_.Program;


namespace Sparta_RPG2_
{
    public enum FloorType
    {
        F1,   // 1층
        F2,   // 2층
        F3,   // 3층
        F4,   // 4층
        F5,   // 5층 (보스)
    }

    public enum Monstertype
    {
        N, // Normal
        B, // Boss
    }

    public class Stage
    {
        public string Name { get; set; }
        public Monstertype Type { get; set; }
        public FloorType Floor { get; set; }
        public List<Monster> Monsters { get; set; }
        public string? IntroDialogue { get; set; } // 입장 전 대사
        public string? BossArt { get; set; }       // 연출용 ASCII 아트

        public Stage(string name, FloorType floor, Monstertype type, List<Monster> monsters)
        {
            Name = name;
            Floor = floor;
            Type = type;
            Monsters = monsters;
        }

        public bool Execute(Character player)
        {
            ShowStageIntro(); // 1. ▶ 이름 (타입) 출력 + [Enter] 대기

            if (Type == Monstertype.B)
            {
                ShowBossEntrance(); // 2. 보스 등장 연출
            }

            return true;
        }

        /// <summary>
        /// 던전 스테이지 진입 인트로 출력
        /// </summary>
        private void ShowStageIntro()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"▶ {Name} ({Type})에 진입합니다.");

            Console.WriteLine();
            Console.Write("[Enter] 키를 눌러 던전 입장을 시작하세요...");
            Console.ResetColor();

            while (Console.ReadKey(true).Key != ConsoleKey.Enter) ; // 🔥 대기
        }

        /// <summary>
        /// 보스 등장 연출 출력
        /// </summary>
        private void ShowBossEntrance()
        {
            Console.Clear(); // 🔥 진짜 보스 연출은 여기서 새로 Clear

            Console.ForegroundColor = ConsoleColor.DarkRed;
            if (!string.IsNullOrEmpty(BossArt))
            {
                string[] lines = BossArt.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    Console.WriteLine(line);
                    Thread.Sleep(100);
                }
            }

            if (!string.IsNullOrEmpty(IntroDialogue))
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"『{IntroDialogue}』");
            }

            Console.ResetColor();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("[Enter] 키를 눌러 전투를 시작하세요...");
            Console.ResetColor();

            while (Console.ReadKey(true).Key != ConsoleKey.Enter) ; // 전투 시작 대기
        }


    }

    public class Dungeon
    {
        public string Name { get; set; }
        public int RequiredLevel { get; set; } = 1;
        public List<Stage> Stages { get; set; }
        public FloorType Floor { get; set; }
        public bool IsCleared { get; set; }


        public string GetFloorName(FloorType floor)
        {
            return floor switch
            {
                FloorType.F1 => "1층",
                FloorType.F2 => "2층",
                FloorType.F3 => "3층",
                FloorType.F4 => "4층",
                FloorType.F5 => "5층",
                _ => "???"
            };
        }

        

        public void Enter(Character player, Inventory inventory)
        {
            if (player.Level >= RequiredLevel)
            {
                Console.WriteLine($"⚔ {Name}에 진입합니다...");
                ShowDungeonEntranceEffect(Name);
                StartDungeon(player, inventory);
            }
            else
            {
                Console.WriteLine("레벨이 부족합니다.");
            }
        }

        private void ShowDungeonEntranceEffect(string dungeonName)
        {
            Console.Clear();
            string[] entranceArt = {
        "███████╗██████╗  █████╗ ██████╗ ████████╗ █████╗ ",
        "██╔════╝██╔══██╗██╔══██╗██╔══██╗╚══██╔══╝██╔══██╗",
        "███████╗██████╔╝███████║██████╔╝   ██║   ███████║",
        "╚════██║██╔═══╝ ██╔══██║██╔═██╝╗   ██║   ██╔══██║",
        "███████║██║     ██║  ██║██║  ██║   ██║   ██║  ██║",
        "╚══════╝╚═╝     ╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝   ╚═╝  ╚═╝",
        "",
        "             ⚔️  무너진 스파르타의 심장 앞에 서 있다...  🛡️"
    };


            foreach (string line in entranceArt)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(line);
                Thread.Sleep(100);
            }
            Console.ResetColor();

            Console.WriteLine($"\n🌀 당신은 '{dungeonName}'의 문 앞에 도착했습니다.");
            Thread.Sleep(1000);
            Console.WriteLine("🩸 무너진 벽 틈 사이로 피비린내와 전사들의 신음이 흘러나옵니다...");
            Thread.Sleep(1500);
            Console.WriteLine("💬 \"이곳이 스파르타 전사들이 남긴 마지막 흔적이군..\" 당신은 무기를 높이 듭니다.");
            Thread.Sleep(1500);
            Console.WriteLine($"\n[Enter] 키를 눌러 '{dungeonName}'에 진입하세요.");
            while (Console.ReadKey(true).Key != ConsoleKey.Enter);
        }

        private void StartDungeon(Character player, Inventory inventory)// 17 13 20 18 19 21 14 11 15 12 16
        {
            // 던전 전투 시스템 사용으로 교체
            DungeonBattleSystem dungeonBattle = new DungeonBattleSystem(this, player, inventory);
            dungeonBattle.Start(); // 던전 전용 전투 실행

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("🏁 던전 클리어!");
            Console.ResetColor();

            IsCleared = true;
        }

        // ✅ 여기! 던전을 미리 정의해둡니다.
        public static Dungeon AresTower => new Dungeon
        {
            Name = "🪦 무너진 스파르타의 심장 🪦",
            RequiredLevel = 1,
            Stages = new List<Stage>
    {
        new Stage("1층 - 붉은 폐허의 유령 '카이론'", FloorType.F1, Monstertype.B, new List<Monster>
{
    new Monster("카이론", 25, 850, 850, 65),
})
{
    IntroDialogue = "🪦 무너진 대지 위에, 잊혀진 자들의 혼이 떠돈다..",
    BossArt = @"
═════════════════════════════════════
      🪦  C H A I R O N  ⚔️
 『붉은 폐허를 배회하는 전사의 유령』
═════════════════════════════════════"
},

        new Stage("2층 - 타락한 수호자 '모라드'", FloorType.F2, Monstertype.B, new List<Monster>
{
    new Monster("모라드", 32, 950, 950, 75), // ✅ 기존보다 약간 강화된 능력치 (보스 무게감 반영)
})
{
    IntroDialogue = "🛡️ 내가 지키던 것은 스파르타였다.. 이젠 그 폐허만을 지킬 뿐이다..",
    BossArt = @"
════════════════════════════
    🛡️  M O R A D  ⚔️
   『타락한 심장의 수호자』
════════════════════════════"
},

        new Stage("3층 - 스파르타의 배신자 '칼리크'", FloorType.F3, Monstertype.B, new List<Monster>
{
    new Monster("칼리크", 38, 1000, 1000, 80),
})
{
    IntroDialogue = "🩸 그날.., 심장을 꿰뚫은 것은 적이 아니라 동료였다..",
    BossArt = @"
═══════════════════════════════
      🩸  K A L I Q  🗡️
 『스파르타를 무너뜨린 배신자』
═══════════════════════════════"
},

       new Stage("4층 - 오염된 제사장 '아트란'", FloorType.F4, Monstertype.B, new List<Monster>
    {
        new Monster("아트란", 40, 1200, 1200, 80),
    })
    {
        IntroDialogue = "📿 신의 이름으로 피를 바친다! 나의 신은 아직 죽지 않았다!",
        BossArt = @"
════════════════════════════
   🕯️   A T R A N   📿
 『피의 의식을 이은 제사장』
════════════════════════════"
    },

        new Stage("5층 - 전쟁의 망령 '사피로스'", FloorType.F5, Monstertype.B, new List<Monster>
    {
        new Monster("사피로스", 60, 1800, 1800, 150),
    })
    {
        IntroDialogue = "🔥 나는 전쟁 속에서 죽지 않았다.. 내가 곧 전쟁이었다!!",
        BossArt = @"
══════════════════════════
   🔥  S A P H I R O S  🪓
  『페르시아 전쟁의 화신』
══════════════════════════"
    }
    }
        };
    }
}
    


