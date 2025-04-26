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
                            int damage = Math.Max(1, (int)(player.Attack * 2));
                            m.HP -= damage;
                            Console.WriteLine($"💥 파괴의 창격! {m.Name}에게 {damage} 피해!");
                        }
                        player.MP -= 11;
                    }
                    else
                    {
                        Console.WriteLine("❌ MP가 부족합니다!");
                    }
                    break;

                case "올림포스의 사도":
                    if (player.MP >= 0)
                    {
                        foreach (var m in monsters.Where(m => !m.IsDead))
                        {
                            int damage = Math.Max(1, (int)(player.Attack * 2));
                            m.HP -= damage;
                            Console.WriteLine($"⚡ 제우스의 천벌! {m.Name}에게 {damage}의 신의 번개가 내리쳤다!");
                        }
                        player.MP -= 0;
                    }
                    else
                    {
                        Console.WriteLine("❌ MP가 부족합니다!");
                    }
                    break;

                case "라코니아 순찰자":
                    if (player.MP >= 0)
                    {
                        foreach (var m in monsters.Where(m => !m.IsDead))
                        {
                            int damage = Math.Max(1, (int)(player.Attack * 2));
                            m.HP -= damage;
                            Console.WriteLine($"🏹 그림자 일격! {m.Name}에게 {damage} 피해!");
                        }
                        player.MP -= 0;
                    }
                    else
                    {
                        Console.WriteLine("❌ MP가 부족합니다!");
                    }
                    break;
                case "스파르타의 왕":
                    if (player.MP >= 0)
                    {
                        foreach (var m in monsters.Where(m => !m.IsDead))
                        {
                            int damage = Math.Max(1, (int)(player.Attack * 2.5));
                            m.HP -= damage;
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine($"🔱 레오니다스의 일격! {m.Name}에게 {damage}의 피해를 입혔습니다!");
                            Console.ResetColor();
                        }
                        player.MP -= 0;
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

        public void UseAwakeningSkill(Character player, List<Monster> monsters)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;

            Random rand = new Random();

            switch (player.JobName.Trim())
            {
                case "팔랑크스 중보병":
                    Console.WriteLine("💥 [궁극기: 철의 진군] 100타 돌진 공격 발동!");
                    for (int i = 0; i < 100; i++)
                    {
                        var alive = monsters.Where(m => !m.IsDead).ToList();
                        if (alive.Count == 0) break;

                        var target = alive[rand.Next(alive.Count)];
                        int damage = (int)(player.Attack * rand.NextDouble() * 0.1 + 1); // 1 ~ 10% 사이
                        target.HP -= damage;
                        if (target.HP <= 0) target.IsDead = true;

                        Console.WriteLine($"🔱 돌격 {i + 1} - {target.Name}에게 {damage} 피해");
                        Thread.Sleep(10);
                    }
                    break;

                case "올림포스의 사도":
                    Console.WriteLine("⚡ [궁극기: 신벌의 낙뢰] 80타 마법 낙뢰 발동!");
                    for (int i = 0; i < 80; i++)
                    {
                        var alive = monsters.Where(m => !m.IsDead).ToList();
                        if (alive.Count == 0) break;

                        var target = alive[rand.Next(alive.Count)];
                        int damage = (int)(player.Attack * rand.NextDouble() * 0.12 + 1); // 약간 더 센 편
                        target.HP -= damage;
                        if (target.HP <= 0) target.IsDead = true;

                        Console.WriteLine($"⚡ 낙뢰 {i + 1} - {target.Name}에게 {damage} 피해");
                        Thread.Sleep(15);
                    }
                    break;

                case "라코니아 순찰자":
                    Console.WriteLine("🏹 [궁극기: 암영의 일제사격] 120타 음영 화살 발동!");
                    for (int i = 0; i < 120; i++)
                    {
                        var alive = monsters.Where(m => !m.IsDead).ToList();
                        if (alive.Count == 0) break;

                        var target = alive[rand.Next(alive.Count)];
                        int damage = (int)(player.Attack * rand.NextDouble() * 0.08 + 1); // 빠르고 약하게
                        target.HP -= damage;
                        if (target.HP <= 0) target.IsDead = true;

                        Console.WriteLine($"🏹 화살 {i + 1} - {target.Name}에게 {damage} 피해");
                        Thread.Sleep(7);
                    }
                    break;

                case "스파르타의 왕":
                    Console.WriteLine("🔱 [궁극기: 스파르타 최후의 명령] 150타 창돌풍 발동!");
                    for (int i = 0; i < 150; i++)
                    {
                        var alive = monsters.Where(m => !m.IsDead).ToList();
                        if (alive.Count == 0) break;

                        var target = alive[rand.Next(alive.Count)];
                        int damage = (int)(player.Attack * rand.NextDouble() * 0.09 + 1); // 무난한 세기
                        target.HP -= damage;
                        if (target.HP <= 0) target.IsDead = true;

                        Console.WriteLine($"💢 진혼격 {i + 1} - {target.Name}에게 {damage} 피해");
                        Thread.Sleep(5);
                    }
                    break;

                default:
                    Console.WriteLine("⚠️ 각성기를 사용할 수 없는 직업입니다.");
                    break;
            }

            player.MP -= 0;
            Console.ResetColor();
        }

        public void Start()
        {
            var expendables = new BattleExpendables(player, inventory);
            var context = new BattleContext(player, expendables, Program.questManager!, inventory, Program.allItems, Program.expendables);

            foreach (var stage in dungeon.Stages)
            {
                EnterStage(stage);

                var result = HandleStageBattle(stage, context);

                if (!HandleStageResult(result, stage)) return; // 리턴되면 루프 종료
            }

            if (player.HP > 0)
            {
                WriteColoredLine("🎉 던전 전체 클리어!", ConsoleColor.Green);
                dungeon.IsCleared = true;
            }
        }

        private bool HandleStageResult(BattleResult result, Stage stage)
        {
            player.HP = player.MaxHP;
            DungeonReward dungeonReward = new DungeonReward(); // 리워드

            switch (result)
            {
                case BattleResult.Victory:
                    WriteColoredLine($"✔ {stage.Name} 클리어!", ConsoleColor.Cyan);
                    dungeonReward.Reward(stage);
                    return true;

                case BattleResult.Escape:
                    WriteColoredLine($"⚠️ {stage.Name}에게서 도망쳤습니다. 던전 진행이 중단됩니다.", ConsoleColor.Yellow);
                    Thread.Sleep(2000); // 연출용 약간의 대기
                    Program.ShowStartMenu(); // 🔁 메인 메뉴로 복귀
                    return false;

                case BattleResult.Defeat:
                    WriteColoredLine($"💀 {stage.Name}에서 전투에 패배했습니다.", ConsoleColor.Red);
                    return false;

                default:
                    WriteColoredLine("❓ 알 수 없는 전투 결과입니다.", ConsoleColor.DarkGray);
                    return false;
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
                    return BattleResult.Defeat; // 🛑 여기서 바로 함수 종료
                }

                WaitForNextTurn();
            }

            // while문을 정상적으로 탈출했다면
            if (context.Player.HP > 0)
                return BattleResult.Victory;
            else
                return BattleResult.Defeat;
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
            Console.WriteLine("1. 일반 스킬");
            Console.WriteLine("2. 소모품 사용");
            Console.WriteLine("3. 도망친다");
            Console.WriteLine("4. 🔱 스파르타의 각성");
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

                case "4":
                    if (context.Player.MP >= 50) // 조건은 상황에 맞게 조정 가능
                    {
                        UseAwakeningSkill(context.Player, monsters);
                        return BattleResult.Victory;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("❌ 각성기를 발동하기 위한 MP가 부족합니다!");
                        Console.ResetColor();
                        Thread.Sleep(1000);
                        return BattleResult.Victory;
                    }
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

            // 🧑 플레이어 상태 출력
            int playerHP = Math.Max(player.HP, 0); // 음수 방지
            Console.WriteLine($"👤 {player.Name} HP: {playerHP} / {player.MaxHP}");
            Console.WriteLine($"    {GenerateHpBar(playerHP, player.MaxHP, 20, showPercent: true)}");

            // 🐺 몬스터 상태 출력
            foreach (var monster in monsters)
            {
                bool isBoss = monster.Name.Contains("화신") || monster.Name.Contains("탈로스") || monster.Name.Contains("포보스") || monster.Name.Contains("루가에") || monster.Name.Contains("케르베르");
                int barLength = isBoss ? 30 : 20;

                int monsterHP = Math.Max(monster.HP, 0); // ❗ 여기서 음수 방지
                Console.WriteLine($"🐺 {monster.Name} HP: {monsterHP} / {monster.MaxHP}");
                Console.WriteLine($"    {GenerateHpBar(monsterHP, monster.MaxHP, barLength, showPercent: true)}");
            }

            Console.WriteLine("------------------------\n");
        }

        private string GenerateHpBar(int current, int max, int barLength = 20, bool showPercent = false)
        {
            if (max <= 0) max = 1;

            int filledLength = Math.Clamp((int)((double)current / max * barLength), 0, barLength);
            string bar = new string('█', filledLength) + new string('─', barLength - filledLength);
            int percent = Math.Clamp((int)((double)current / max * 100), 0, 100);
       
            return showPercent
                ? $"[{bar}] {percent}%"
                : $"[{bar}]";
        }
    }
}
