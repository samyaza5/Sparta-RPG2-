using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sparta_RPG2_
{
    /// <summary>
    /// 던전 전용 전투 시스템을 담당하는 클래스입니다.
    /// 각 스테이지를 순차적으로 진행하며, 플레이어와 몬스터 간의 전투를 처리합니다.
    /// </summary>
    class DungeonBattleSystem
    {
        private Dungeon dungeon;
        private Character player;
        private Inventory inventory;

        /// <summary>
        /// DungeonBattleSystem 생성자입니다.
        /// </summary>
        /// <param name="dungeon">진행할 던전 객체</param>
        /// <param name="player">플레이어 캐릭터</param>
        /// <param name="inventory">플레이어 인벤토리</param>
        public DungeonBattleSystem(Dungeon dungeon, Character player, Inventory inventory)
        {
            this.dungeon = dungeon;
            this.player = player;
            this.inventory = inventory;
        }

        /// <summary>
        /// 직업에 따라 다른 스킬을 발동시켜 몬스터에게 피해를 줍니다.
        /// </summary>
        /// <param name="player">플레이어 캐릭터</param>
        /// <param name="monsters">대상 몬스터 목록</param>
        public void DugeonSkill(Character player, List<Monster> monsters)
        {
            Console.WriteLine($"🌀 {player.Job}의 스킬을 발동합니다!");

            switch (player.JobName.Trim())
            {
                case "팔랑크스 중보병":
                    if (player.MP >= 1)
                    {
                        foreach (var m in monsters.Where(m => !m.IsDead))
                        {
                            int damage = Math.Max(1, (int)(player.Attack * 1.5));
                            m.HP -= damage;
                            Console.WriteLine($"💥 방패 폭풍! {m.Name}에게 {damage} 피해!");
                        }
                        player.MP -= 11;
                    }
                    else
                    {
                        Console.WriteLine("❌ MP가 부족합니다!");
                    }
                    break;

                case "아레스의 예언자":
                    if (player.MP >= 1)
                    {
                        foreach (var m in monsters.Where(m => !m.IsDead))
                        {
                            int damage = Math.Max(1, (int)(player.Attack * 1.8));
                            m.HP -= damage;
                            Console.WriteLine($"🔥 신성 불꽃진혼! {m.Name}에게 {damage} 마법 피해!");
                        }
                        player.MP -= 1;
                    }
                    else
                    {
                        Console.WriteLine("❌ MP가 부족합니다!");
                    }
                    break;

                case "라코니아 순찰자":
                    if (player.MP >= 1)
                    {
                        foreach (var m in monsters.Where(m => !m.IsDead))
                        {
                            int damage = Math.Max(1, (int)(player.Attack * 1.4));
                            m.HP -= damage;
                            Console.WriteLine($"🏹 일제 사격! {m.Name}에게 {damage} 피해!");
                        }
                        player.MP -= 1;
                    }
                    else
                    {
                        Console.WriteLine("❌ MP가 부족합니다!");
                    }
                    break;

                default:
                    Console.WriteLine("⚠️ 알 수 없는 직업입니다.");
                    break;
            }

            Thread.Sleep(1000);
        }

        /// <summary>
        /// 던전 전체를 순회하며 각 스테이지를 실행하고, 전투를 시작합니다.
        /// </summary>
        public void Start()
        {
            BattleExpendables expendables = new(player, inventory);
            BattleContext context = new(player, expendables, Program.questManager!, inventory, Program.allItems, Program.expendables);

            foreach (var stage in dungeon.Stages)
            {
                stage.Execute(player);
                Console.Clear();
                Console.WriteLine($"🗡 {stage.Name}에 진입합니다...");

                HandleStageBattle(stage, context);

                if (player.HP <= 0)
                    break;

                Console.WriteLine($"✔ {stage.Name} 클리어!");
            }

            if (player.HP > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("🎉 던전 전체 클리어!");
                Console.ResetColor();
                dungeon.IsCleared = true;
            }
        }

        /// <summary>
        /// 단일 스테이지의 전투를 처리합니다. 플레이어 선택 및 반격, 체력 업데이트까지 담당합니다.
        /// </summary>
        private void HandleStageBattle(Stage stage, BattleContext context)
        {
            List<Monster> monsters = stage.Monsters;

            while (context.Player.HP > 0 && monsters.Any(m => !m.IsDead))
            {
                Console.Clear();
                Console.WriteLine("⚔️ 전투 중 - 당신의 선택은?");
                Console.WriteLine("1. 스킬");
                Console.WriteLine("2. 소모품 사용");
                Console.WriteLine("3. 도망친다");
                Console.Write(">> ");

                string? choice = Console.ReadLine();

                if (!HandlePlayerChoice(choice, context, monsters))
                    return; // 도망 또는 잘못된 선택

                ProcessEnemyCounterAttack(monsters, context.Player);
                CheckMonsterDeaths(monsters);

                PrintBattleStatus(monsters, context.Player);

                if (context.Player.HP <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("☠️ 당신은 쓰러졌습니다...");
                    Console.ResetColor();
                    break;
                }

                Console.WriteLine("\n[Enter] 키를 눌러 다음 턴으로 진행...");
                while (Console.ReadKey(true).Key != ConsoleKey.Enter) ;
            }
        }

        /// <summary>
        /// 플레이어의 전투 선택을 처리합니다. 스킬, 소모품 사용, 도망 기능을 포함합니다.
        /// </summary>
        private bool HandlePlayerChoice(string? choice, BattleContext context, List<Monster> monsters)
        {
            switch (choice)
            {
                case "1":
                    DugeonSkill(context.Player, monsters);
                    return true;

                case "2":
                    context.BattleExpendables.UseExpend();
                    Console.WriteLine("💡 소모품 사용 후 다음 턴으로 진행됩니다.");
                    Thread.Sleep(1000);
                    return true;

                case "3":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("🏃 당신은 도망쳤습니다.");
                    Console.ResetColor();
                    Thread.Sleep(1000);
                    return false;

                default:
                    Console.WriteLine("❌ 잘못된 입력입니다. 다시 선택해주세요.");
                    Thread.Sleep(1000);
                    return true;
            }
        }

        /// <summary>
        /// 생존 중인 몬스터들의 반격을 처리합니다.
        /// </summary>
        private void ProcessEnemyCounterAttack(List<Monster> monsters, Character player)
        {
            foreach (var monster in monsters.Where(m => !m.IsDead))
            {
                int damage = Math.Max(1, monster.Attack - player.Defense);
                player.HP -= damage;
                Console.WriteLine($"💥 {monster.Name}의 반격! {damage} 피해!");
            }
        }

        /// <summary>
        /// 체력이 0 이하인 몬스터들을 죽은 상태로 표시하고 로그를 출력합니다.
        /// </summary>
        private void CheckMonsterDeaths(List<Monster> monsters)
        {
            foreach (var m in monsters.Where(m => m.HP <= 0 && !m.IsDead))
            {
                m.IsDead = true;
                Console.WriteLine($"☠️ {m.Name} 처치!");
            }
        }

        /// <summary>
        /// 플레이어와 몬스터들의 현재 HP 상태를 출력합니다.
        /// </summary>
        private void PrintBattleStatus(List<Monster> monsters, Character player)
        {
            Console.WriteLine("\n------------------------");
            Console.WriteLine($"👤 {player.Name} HP: {player.HP} / {player.MaxHP}");

            foreach (var monster in monsters)
            {
                Console.WriteLine($"🐺 {monster.Name} HP: {monster.HP} / {monster.MaxHP}");
            }
            Console.WriteLine("------------------------\n");
        }
    }
}
