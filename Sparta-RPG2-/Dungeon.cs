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

    class Dungeon
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

        public class DungeonManager
        {
            private Dungeon currentDungeon;
            private int currentStageIndex = 0;

            public DungeonManager(Dungeon dungeon)
            {
                currentDungeon = dungeon;
            }

            public List<Monster> GetCurrentStageMonsters()
            {
                return currentDungeon.Stages[currentStageIndex].Monsters;
            }

            public string GetCurrentStageName()
            {
                return currentDungeon.Stages[currentStageIndex].Name;
            }

            public bool MoveToNextStage()
            {
                if (currentStageIndex < currentDungeon.Stages.Count - 1)
                {
                    currentStageIndex++;
                    return true;
                }
                return false; // 마지막 스테이지 도달
            }

            public bool IsBossStage()
            {
                return currentDungeon.Stages[currentStageIndex].Type == Monstertype.B;
            }
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
    "    █████╗   ██████╗  ███████  ██████╗",
    "   ██╔══██╗ ██╔══██╗ ██╔════╝ ██╔════╝",
    "  ███████║ ██████╔╝ ███████╗ ╚█████",
    " ██╔══██║ ██╔██╔╝  ██╔═════╝     ██╝  ",
    "██║  ██║ ██║║██╗  ███████╗║██████║",
    "╚═╝  ╚═╝ ╚═╝╚══╝ ╚══════╝╚══════╝"
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
            Console.WriteLine("🩸 내부에서는 짙은 피 냄새와 울부짖음이 퍼져나옵니다...");
            Thread.Sleep(1500);
            Console.WriteLine("💬 '돌아가기엔 늦었군...' 당신은 주먹을 꽉 쥡니다.");
            Thread.Sleep(1500);
            Console.WriteLine($"\n[Enter] 키를 눌러 '{dungeonName}'에 진입하세요.");
            while (Console.ReadKey(true).Key != ConsoleKey.Enter) ;
        }

        private void StartDungeon(Character player, Inventory inventory)
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
            Name = "타락한 아레스의 탑",
            RequiredLevel = 1,
            Stages = new List<Stage>
    {
        new Stage("1층 - 고대의 망령 케르베르", FloorType.F1, Monstertype.B, new List<Monster>
        {
            new Monster("고대의 망령 케르베르", 4, 85, 85, 14),
        })
        {
            IntroDialogue = "☠️ 지옥의 문은 열렸다. 삼두의 분노가 너를 삼키리라...",
            BossArt = @"
 ═════════════════════
⚔️  C E R B E R U S  ☠️
   『지옥의 문지기』
 ═════════════════════"

        },

        new Stage("2층 - 저주받은 창병 탈로스", FloorType.F2, Monstertype.B, new List<Monster>
        {
            new Monster("저주받은 창병 탈로스", 5, 90, 90, 15),
        })
        {
            IntroDialogue = "⚙️ 내 육체는 강철, 내 심장은 복수. 파괴는 나의 본능이다.",
            BossArt = @"
  ═══════════════════════════
    ⚙️   T A L O S   ⚙️
  『철의 창병, 살아있는 고대 병기』
  ═══════════════════════════"
        },

        new Stage("3층 - 그림자 암살자 포보스", FloorType.F3, Monstertype.B, new List<Monster>
        {
            new Monster("그림자 암살자 포보스", 6, 80, 80, 16),
        })
        {
            IntroDialogue = "🗡️ 칼날은 보이지 않는다. 너는 이미 내 그림자 속에 있다.",
            BossArt = @"
  ══════════════════════
   🗡️  P H O B O S  🌒
『공포의 그림자, 침묵의 암살자』
  ══════════════════════"
        },

        new Stage("4층 - 타락한 사제 루가에", FloorType.F4, Monstertype.B, new List<Monster>
        {
            new Monster("타락한 사제 루가에", 7, 110, 110, 13),
        })
        {
            IntroDialogue = "📖 신의 이름 아래, 타락을 선고하노라. 너는 구원받지 못하리라.",
            BossArt = @"
══════════════════════════
    🕯️  L U G A Ë  📖
『신을 배신한 자, 타락의 사제』
══════════════════════════"
        },

        new Stage("5층 - 아레스의 화신 아스칼", FloorType.F5, Monstertype.B, new List<Monster>
        {
            new Monster("아레스의 화신 아스칼", 10, 150, 150, 30),
        })
        {
            IntroDialogue = "🔥 신의 화염이 이 몸을 태웠다. 너도 함께 타오르리라!",
            BossArt = @"
════════════════════════
   🔥  A S K A L  🔥
『전쟁신의 분노, 불꽃의 화신』
════════════════════════"
        }
    }
        };
    }
}
    


