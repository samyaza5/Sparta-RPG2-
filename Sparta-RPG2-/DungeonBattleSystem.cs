using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sparta_RPG2_;

namespace Sparta_RPG2_
{
    public enum BattleResult
    {
        Victory,
        Defeat,
        Escape
    }

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

                case "올림포스의 사도":
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
            var expendables = new BattleExpendables(player, inventory);
            var context = new BattleContext(player, expendables, Program.questManager!, inventory, Program.allItems, Program.expendables);

            foreach (var stage in dungeon.Stages)
            {
                EnterStage(stage);
                var result = HandleStageBattle(stage, context);

                switch (result)
                {
                    case BattleResult.Victory:
                        WriteColoredLine($"✔ {stage.Name} 클리어!", ConsoleColor.Cyan);
                        break;

                    case BattleResult.Escape:
                        WriteColoredLine($"⚠️ {stage.Name}에서 도망쳤습니다. 던전 진행이 중단됩니다.", ConsoleColor.Yellow);
                        return;

                    case BattleResult.Defeat:
                        WriteColoredLine($"💀 {stage.Name}에서 전투에 패배했습니다.", ConsoleColor.Red);
                        return;
                }

                if (player.HP <= 0)
                    return;
            }

            if (player.HP > 0)
            {
                WriteColoredLine("🎉 던전 전체 클리어!", ConsoleColor.Green);
                dungeon.IsCleared = true;
            }
        }


        public BattleResult HandleStageBattle(Stage stage, BattleContext context)
        {
            var monsters = stage.Monsters;

            while (context.Player.HP > 0 && monsters.Any(m => !m.IsDead))
            {
                ShowBattleMenu();
                string? choice = Console.ReadLine();
                var action = HandlePlayerChoice(choice, context, monsters);

                if (action == BattleResult.Escape)
                    return BattleResult.Escape;

                ProcessEnemyCounterAttack(monsters, context.Player);
                CheckMonsterDeaths(monsters);
                PrintBattleStatus(monsters, context.Player);

                if (context.Player.HP <= 0)
                {
                    WriteColoredLine("☠️ 당신은 쓰러졌습니다...", ConsoleColor.Red);
                    return BattleResult.Defeat;
                }

                WaitForNextTurn();
            }

            return BattleResult.Victory;
        }

        private void EnterStage(Stage stage)
        {
            stage.Execute(player);
            Console.Clear();
            Console.WriteLine($"🗡 {stage.Name}에 진입합니다...");
        }

        private void ShowBattleMenu()
        {
            Console.Clear();
            Console.WriteLine("⚔️ 전투 중 - 당신의 선택은?");
            Console.WriteLine("1. 스킬");
            Console.WriteLine("2. 소모품 사용");
            Console.WriteLine("3. 도망친다");
            Console.Write(">> ");
        }

        private void WaitForNextTurn()
        {
            Console.WriteLine("\n[Enter] 키를 눌러 다음 턴으로 진행...");
            while (Console.ReadKey(true).Key != ConsoleKey.Enter) ;
        }

        private void WriteColoredLine(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }


        /// <summary>
        /// 플레이어의 전투 선택을 처리합니다. 스킬, 소모품 사용, 도망 기능을 포함합니다.
        /// </summary>
        private BattleResult HandlePlayerChoice(string? choice, BattleContext context, List<Monster> monsters)
        {
            switch (choice)
            {
                case "1":
                    DugeonSkill(context.Player, monsters);
                    return BattleResult.Victory; // 계속 진행

                case "2":
                    context.BattleExpendables.UseExpend();
                    Console.WriteLine("💡 소모품 사용 후 다음 턴으로 진행됩니다.");
                    Thread.Sleep(1000);
                    return BattleResult.Victory;

                case "3":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("🏃 당신은 도망쳤습니다.");
                    Console.ResetColor();
                    Thread.Sleep(1000);
                    return BattleResult.Escape;

                default:
                    Console.WriteLine("❌ 잘못된 입력입니다. 다시 선택해주세요.");
                    Thread.Sleep(1000);
                    return BattleResult.Victory;
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
            Console.WriteLine($"    {GenerateHpBar(player.HP, player.MaxHP, 20, showPercent: true)}");

            foreach (var monster in monsters)
            {
                bool isBoss = monster.Name.Contains("화신") || monster.Name.Contains("탈로스") || monster.Name.Contains("포보스") || monster.Name.Contains("루가에") || monster.Name.Contains("케르베르");
                int barLength = isBoss ? 30 : 20;

                Console.WriteLine($"🐺 {monster.Name} HP: {monster.HP} / {monster.MaxHP}");
                Console.WriteLine($"    {GenerateHpBar(monster.HP, monster.MaxHP, barLength, showPercent: true)}");
            }

            Console.WriteLine("------------------------\n");
        }

        private string GenerateHpBar(int current, int max, int barLength = 20, bool showPercent = false)
        {
            int filledLength = (int)((double)current / max * barLength);
            string bar = new string('█', filledLength) + new string('─', barLength - filledLength);
            int percent = (int)((double)current / max * 100);

            return showPercent
                ? $"[{bar}] {percent}%"
                : $"[{bar}]";
        }
    }
}
