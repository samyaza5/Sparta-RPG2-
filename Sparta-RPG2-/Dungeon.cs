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
            Console.WriteLine($"▶ {Name} ({Type})에 진입합니다.");

            if (Type == Monstertype.B)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;

                if (!string.IsNullOrEmpty(BossArt))
                    Console.WriteLine(BossArt);

                if (!string.IsNullOrEmpty(IntroDialogue))
                    Console.WriteLine(IntroDialogue);

                Console.ResetColor();

                Console.WriteLine(); // 줄 띄움
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("[Enter] 키를 눌러 전투를 시작하세요...");
                Console.ResetColor();

                while (Console.ReadKey(true).Key != ConsoleKey.Enter) ; // Enter 대기
            }

            return true;
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
        " ####                 #                 ####        #        ",
        " #  #                                   #  #                  ",
        " # ##  ### ####  ### ##   ### ## ##     ###  ## ## ## ## ##  ",
        " ###  #  #  ##   # # ##  #  #  ## #     # #  ## ## ##  ## #  ",
        " #    #### ##   # #  #  #  #  ## ##     # #  #  #  #  ## ##  ",
        "##    ###  #    ###  ## ##### #  ###   ## ## ##### ## #  ### ",
        "                                                              "
    };


            foreach (string line in entranceArt)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(line);
                Thread.Sleep(100);
            }
            Console.ResetColor();

            Console.WriteLine($"\n🌀 당신은 '{dungeonName}'의 문 앞에 도착했습니다.");
            Thread.Sleep(1000);
            Console.WriteLine("🩸 무너진 벽 틈 사이로 피비린내와 전사들의 신음이 흘러나옵니다...");
            Thread.Sleep(1500);
            Console.WriteLine("💬 \"이곳이... 페르시아 병사들이 남긴 마지막 흔적이군.\" 당신은 검을 높이 듭니다.");
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

        // ✅ 여기! 타락한 아레스의 탑을 미리 정의해둡니다.
        public static Dungeon AresTower => new Dungeon
        {
            Name = "페르시아의 잔재 - 저주받은 오아시스",
            RequiredLevel = 1,
            Stages = new List<Stage>
    {
        new Stage("1층 - 황야의 사냥개 '쿠라쉬'", FloorType.F1, Monstertype.B, new List<Monster>
        {
            new Monster("쿠라쉬", 20, 850, 850, 70),
        })
        {
            IntroDialogue = "🐺 메마른 대지의 분노가 이빨이 되어 너를 물어뜯는다.",
            BossArt = @"
═══════════════════════════
   🐺  K U R A S H  🌵
 『페르시아 황야의 맹수』
═══════════════════════════"

        },

        new Stage("2층 - 저주의 투창병 '나자르'", FloorType.F2, Monstertype.B, new List<Monster>
    {
        new Monster("나자르", 30, 900, 900, 70),
    })
    {
        IntroDialogue = "🏹 이 창은 죽은 자의 복수를 품고 있다. 넌 그 끝을 보게 될 것이다.",
        BossArt = @"
════════════════════════════
  🏹   N A Z A R   ⚔️
『복수의 혼을 담은 창병』
════════════════════════════"
    },

        new Stage("3층 - 그림자의 첩자 '자이르'", FloorType.F3, Monstertype.B, new List<Monster>
    {
        new Monster("자이르", 35, 950, 950, 75),
    })
    {
        IntroDialogue = "🌑 달 없는 밤, 내 단검은 침묵 속에 내리꽂힌다.",
        BossArt = @"
═════════════════════════
    🌑  Z A I R   🗡️
 『어둠 속 페르시아 첩자』
═════════════════════════"
    },

       new Stage("4층 - 오염된 제사장 '아트란'", FloorType.F4, Monstertype.B, new List<Monster>
    {
        new Monster("아트란", 40, 1200, 1200, 80),
    })
    {
        IntroDialogue = "📿 신의 이름으로 피를 바친다. 나의 신은 죽지 않았다.",
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
        IntroDialogue = "🔥 내가 죽은 건 전쟁 때문이 아니었다. 난 전쟁 그 자체였지.",
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
    


