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

        public void Enter(Character player, Inventory inventory, List<Item> itemList, List<Expendables> expendableList)
        {
            if (player.Level >= RequiredLevel)
            {
                Console.WriteLine($"⚔ {Name}에 진입합니다...");
                ShowDungeonEntranceEffect(Name);
                StartDungeon(player, inventory, itemList, expendableList);
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
    "  ███████║ ██████╔╝ ██████╗  ╚█████",
    " ██╔══██║ ██╔██╔╝  ██╔═══╝       ██╝  ",
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

        private void StartDungeon(Character player, Inventory inventory, List<Item> itemList,List<Expendables> expendableList )
        {
            BattleExpendables expendables = new(player, inventory);

            foreach (var stage in Stages)
            {
                stage.Execute(player); // 층별 안내 출력
                StartDungeonBattle(player, stage, expendables, inventory, itemList, expendableList); // ✅ 수정된 호출
            }

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
        new Stage("1층 - 붉은 늑대: 정찰병", FloorType.F1, Monstertype.N,
            new List<Monster>
            {
                new Monster("붉은 늑대: 정찰병", 2, 30, 30, 6),
                new Monster("붉은 늑대: 정찰병", 2, 30, 30, 6),
                new Monster("붉은 늑대: 정찰병", 2, 30, 30, 6)
            }),

        new Stage("2층 - 붉은 늑대: 추적자", FloorType.F2, Monstertype.N,
            new List<Monster>
            {
                new Monster("붉은 늑대: 추적자", 3, 40, 40, 8),
                new Monster("붉은 늑대: 추적자", 3, 40, 40, 8),
                new Monster("붉은 늑대: 추적자", 3, 40, 40, 8)
            }),

        new Stage("3층 - 붉은 늑대: 포식자", FloorType.F3, Monstertype.N,
            new List<Monster>
            {
                new Monster("붉은 늑대: 포식자", 4, 45, 45, 10),
                new Monster("붉은 늑대: 포식자", 4, 45, 45, 10),
                new Monster("붉은 늑대: 포식자", 4, 45, 45, 10)
            }),

        new Stage("4층 - 붉은 늑대: 광전사", FloorType.F4, Monstertype.N,
            new List<Monster>
            {
                new Monster("붉은 늑대: 광전사", 5, 50, 50, 12),
                new Monster("붉은 늑대: 광전사", 5, 50, 50, 12),
                new Monster("붉은 늑대: 광전사", 5, 50, 50, 12)
            }),

        new Stage("5층 - 붉은 늑대: 저주받은 왕", FloorType.F5, Monstertype.B,
            new List<Monster> { new Monster("붉은 늑대: 저주받은 왕", 10, 150, 150, 30) })
    }
        };

        private void StartDungeonBattle(Character player, Stage stage, BattleExpendables expendables, Inventory inventory,List<Item>itemList, List<Expendables> expendableList)
        {
            DungeonResult dungeonResult = new DungeonResult(inventory, itemList, expendableList);
            Console.Clear();

            List<Monster> monsters = stage.Monsters;

            if (stage.Type == Monstertype.B)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("🔥 던전 보스전이 시작됩니다!");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("⚔️ 던전 일반 전투를 시작합니다.");
            }

            int beforeHP = player.HP;

            // 전투 진입 시 연출
            string message;
            if (monsters.Count == 1)
            {
                message = $"\n👀 {monsters[0].Name}이(가) 당신을 노려보고 있습니다!\n";
            }
            else
            {
                string names = string.Join(", ", monsters.Select(m => m.Name));
                message = $"\n👀 {names}들이 당신을 노려보고 있습니다!\n";
            }
            Console.WriteLine(message);

            foreach (var monster in monsters)
            {
                Console.WriteLine($"- Lv.{monster.Level} {monster.Name} (HP: {monster.HP}/{monster.MaxHP})");
            }

            Console.WriteLine($"\n❤️ {player.Name} HP: {player.HP}/{player.MaxHP}");
            Console.WriteLine("⚔️ 공격을 시작하려면 [0]을 입력하세요.");
            while (Console.ReadLine() != "0") ;

            // 전투 루프
            while (player.HP > 0 && monsters.Exists(m => !m.IsDead))
            {
                Console.Clear();

                // 살아 있는 몬스터 리스트
                var alive = monsters.Where(m => !m.IsDead).ToList();
                Random rand = new();

                foreach (var monster in alive)
                {
                    // ⚔️ 동시에 전투 시작
                    int playerDamage = player.Attack;
                    int monsterDamage = Math.Max(1, monster.Attack - player.Defense);

                    // 플레이어가 공격
                    monster.HP -= playerDamage;
                    Console.WriteLine($"🗡 {player.Name} → {monster.Name}에게 {playerDamage} 데미지!");

                    if (monster.HP <= 0)
                    {
                        monster.HP = 0;
                        monster.IsDead = true;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"💀 {monster.Name} 격파!");
                        Console.ResetColor();
                        continue; // 죽은 몬스터는 반격 불가
                    }

                    // 몬스터 반격
                    player.HP -= monsterDamage;
                    Console.WriteLine($"👹 {monster.Name} → {player.Name}에게 {monsterDamage} 데미지!");
                }

                Console.WriteLine($"\n❤️ {player.Name} HP: {player.HP}");
                Console.WriteLine("0. 다음");
                while (Console.ReadLine() != "0") ;

                Console.WriteLine($"\n🧭 {GetFloorName(stage.Floor)}의 적을 전부 처치했습니다.");
            }

            // 전투 결과 출력
            if (player.HP <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("💀 전투 실패! 당신은 쓰러졌습니다.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("🎉 전투 승리! 모든 몬스터를 처치했습니다.");
                // 경험치, 골드, 보상 아이템 등 지급 가능
                dungeonResult.LevelUp(monsters, player);
                dungeonResult.DungeonGold(monsters, player);
                dungeonResult.DungeonItemReward(monsters);

            }

            Console.ResetColor();
            Console.WriteLine("\n0. 다음");
            while (Console.ReadLine() != "0") ;
        }
    }

    public class Stage
    {
        public string Name { get; set; }
        public Monstertype Type { get; set; }
        public FloorType Floor { get; set; }
        public List<Monster> Monsters { get; set; }

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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("🔥 보스 전투 시작!");
                Console.ResetColor();
            }
            return true;
        }
    }
}



