
using System;
using System.Collections.Generic;

namespace RPG_SJ
{
    internal class Program
    {
        // 🎯 프로그램의 진입점 (필수!)
        static void Main(string[] args)
        {
            Character player = new Character();
            player.MaxHP = player.HP;  // 시작 시 MaxHP 설정
            ShowStartMenu(player);     // 게임 시작
        }

        // 🧍 캐릭터 클래스
        public class Character
        {
            public int Level { get; set; } = 1;
            public string Name { get; set; } = "함장";
            public string Job { get; set; } = "전사";
            public int Attack { get; set; } = 50;
            public int Defense { get; set; } = 5;
            public int HP { get; set; } = 100;
            public int MaxHP { get; set; }
            public int Gold { get; set; } = 1500;
        }

        // 👹 몬스터 클래스
        public class Monster
        {
            public string Name { get; set; }
            public int Level { get; set; }
            public int HP { get; set; }
            public int Attack { get; set; }

            public Monster(string name, int level, int hp, int attack)
            {
                Name = name;
                Level = level;
                HP = hp;
                Attack = attack;
            }

            public bool IsDead => HP <= 0;
        }

        // ⚔ 전투 시작
        public class BattleSystem
        {
            public void StartBattle(Character player)
            {
                List<Monster> monsters = GenerateMonsters();

                while (player.HP > 0 && monsters.Exists(m => !m.IsDead))
                {
                    PlayerAttack(player, monsters);
                    if (!monsters.Exists(m => !m.IsDead)) break;

                    EnemyPhase(player, monsters);
                }

                BattleResult(player, monsters);
            }

            private List<Monster> GenerateMonsters()
            {
                string[] names = { "미니언", "대포미니언", "공허충" };
                Random rand = new();
                int count = rand.Next(1, 5);
                var list = new List<Monster>();

                for (int i = 0; i < count; i++)
                {
                    string name = names[rand.Next(names.Length)];
                    int level = rand.Next(1, 6);
                    int hp = name switch
                    {
                        "미니언" => 15,
                        "대포미니언" => 25,
                        "공허충" => 10,
                        _ => 10
                    };
                    int attack = name switch
                    {
                        "미니언" => 5,
                        "대포미니언" => 8,
                        "공허충" => 9,
                        _ => 5
                    };
                    list.Add(new Monster(name, level, hp, attack));
                }

                return list;
            }

            static void PlayerAttack(Character player, List<Monster> monsters)
            {
                Console.Clear();
                Console.WriteLine("Battle!!\n");

                // 플레이어가 랜덤 몬스터를 공격
                Random rand = new Random();
                Monster target = monsters[rand.Next(monsters.Count)];

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{player.Name} 의 공격!");
                Console.ResetColor();

                int damage = player.Attack;

                Console.WriteLine($"Lv.{target.Level} {target.Name} 을(를) 맞췄습니다. [데미지 : {damage}]");

                target.HP -= damage;

                if (target.HP <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nLv.{target.Level} {target.Name}");
                    Console.WriteLine($"HP {Math.Max(0, target.HP)} -> Dead");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"Lv.{target.Level} {target.Name}");
                    Console.WriteLine($"HP {target.HP + damage} -> {target.HP}");
                }

                Console.WriteLine("\n0. 다음");
                Console.Write("\n>> ");
                while (Console.ReadLine() != "0") ;
            }

            static void EnemyPhase(Character player, List<Monster> monsters)
            {
                Console.Clear();
                Console.WriteLine("\nEnemy Phase 시작");
                Console.WriteLine("Battle!!\n");

                foreach (var monster in monsters)
                {
                    if (monster.HP <= 0)
                    {
                        continue;  // Dead 상태인 몬스터는 스킵
                    }

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Lv.{monster.Level} {monster.Name} 의 공격!");
                    Console.ResetColor();

                    Console.WriteLine($"{player.Name} 을(를) 맞췄습니다.");

                    // 피해 계산
                    int damage = Math.Max(1, monster.Level * 2);  // 예: 몬스터 레벨 기반 피해
                    int prevHP = player.HP;
                    player.HP -= damage;
                    player.HP = Math.Max(0, player.HP); // 0 이하로 내려가지 않게

                    Console.WriteLine($"\nLv.{player.Level} {player.Name}");
                    Console.WriteLine($"HP {prevHP} -> {player.HP}");

                    Console.WriteLine("\n0. 다음");
                    while (Console.ReadLine() != "0") ;
                }

                if (player.HP <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\n💀 당신은 쓰러졌습니다... 게임 오버");
                    Console.ResetColor();
                    Environment.Exit(0);
                }

                Console.WriteLine("\n📣 당신의 차례입니다!");
            }

            static void BattleResult(Character player, List<Monster> monsters)
            {
                Console.Clear();
                Console.WriteLine("Battle!! - Result\n");

                if (player.HP <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You Lose\n");
                    Console.ResetColor();

                    Console.WriteLine($"Lv.{player.Level} {player.Name}");
                    Console.WriteLine($"HP {player.MaxHP} -> 0");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Victory\n");
                    Console.ResetColor();

                    int defeatedCount = monsters.Count(m => m.IsDead);
                    Console.WriteLine($"던전에서 몬스터 {monsters.Count}마리를 잡았습니다.\n");

                    Console.WriteLine($"Lv.{player.Level} {player.Name}");
                    int damageTaken = player.MaxHP - player.HP;
                    Console.WriteLine($"HP {player.MaxHP} -> {player.HP} (-{damageTaken})");
                }

                Console.WriteLine("\n0. 다음");
                while (Console.ReadLine() != "0") ;
            }
        }

        public class GameUI
        {
            // 📊 상태 보기
            public void ShowStatus(Character player)
            {
                Console.WriteLine($"\nLv. {player.Level}");
                Console.WriteLine($"{player.Name} ({player.Job})");
                Console.WriteLine($"공격력 : {player.Attack}");
                Console.WriteLine($"방어력 : {player.Defense}");
                Console.WriteLine($"체 력 : {player.HP} / {player.MaxHP}");
                Console.WriteLine($"Gold : {player.Gold:N0} G");

                Console.WriteLine("\n0. 나가기");
                Console.Write(">> ");
            }
        }

        // 🎮 게임 시작 메뉴
        static void ShowStartMenu(Character player)
        {
            GameUI ui = new GameUI();                // ✅ UI 객체 생성
            BattleSystem battle = new BattleSystem(); // ✅ 전투 시스템 객체 생성

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
                    ui.ShowStatus(player); // ✅ 객체를 통해 호출
                    Console.ReadLine();    // 0 입력 대기
                    ShowStartMenu(player); // ✅ 다시 메뉴로 돌아가기
                    break;

                case "2":
                    Console.WriteLine("\n[전투를 시작합니다...]\n");
                    battle.StartBattle(player); // ✅ 전투 시스템 실행
                    ShowStartMenu(player);      // ✅ 전투 끝나면 다시 메뉴
                    break;

                default:
                    Console.WriteLine("\n❌ 잘못된 입력입니다.\n");
                    ShowStartMenu(player); // 잘못 입력 시 재귀 호출
                    break;
            }
        }
    }
}

