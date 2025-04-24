using Sparta_RPG2_;
using static Sparta_RPG2_.Quest;

namespace Sparta_RPG2_
{

    // ⚔ 전투 시작
    public class BattleSystem
    {

        public void StartBattle(Character player, BattleExpendables battleExpendables, QuestManager questManager, Inventory inventory, List<Item> allItems, List<Expendables> expendables)
        {
            List<Monster> monsters = GenerateMonsters();

            player.beforeHP = player.HP;
            while (player.HP > 0 && monsters.Exists(m => !m.IsDead))
            {


                PlayerAttack(player, monsters, battleExpendables);
                if (!monsters.Exists(m => !m.IsDead)) break;

                EnemyPhase(player, monsters);
            }

            if (questManager != null)
            {
                BattleResult(player, monsters, questManager, inventory, allItems, expendables);
            }
            else
            {
                Console.WriteLine("⚠️ 퀘스트 매니저가 초기화되지 않았습니다!");
            }

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
                int maxHP = name switch
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

                list.Add(new Monster(name, level, hp, maxHP, attack));
            }

            return list;
        }

        static void PlayerAttack(Character player, List<Monster> monsters, BattleExpendables battleExpendables)
        {

            Console.Clear();
            Console.WriteLine("Battle!!\n");

            // 플레이어가 랜덤 몬스터를 공격
            Random rand = new Random();

            int evasionRate = rand.Next(1, 101);
            int criRate = rand.Next(1, 101);
            float criDamageRate = 1.6f;
            float damageRate = rand.Next(9, 12) / 10f;
            int damage = player.Attack;
            int normalDamage = (int)Math.Round(damage * damageRate);

            var alive = monsters.Where(m => !m.IsDead).ToList();
            if (alive.Count == 0) return;  // 전부 죽은 경우 예외처리

            Monster target = alive[rand.Next(alive.Count)];

            foreach (Monster monster in monsters)
            {
                Console.WriteLine($"Lv.{monster.Level} {monster.Name}  HP {(monster.IsDead ? "Dead" : $"{monster.HP}/{monster.MaxHP}")}");
            }
            Console.WriteLine($"\n\n[내정보]\nLv.{player.Level} {player.Name} ({player.Job})\nHP {player.HP}/{player.MaxHP}\nMP {player.MP}/{player.MaxMP}\n");


            while (true)
            {
                Console.Write("1. 공격\n2. 스킬\n3. 소모품 사용\n\n원하시는 행동을 입력해주세요.\n>> ");
                string? input = Console.ReadLine();
                switch (input)
                {
                    case "1": // 일반공격
                        if (evasionRate > 90) // 상대가 회피함
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"{player.Name} 의 공격!"); Console.ResetColor();
                            Console.WriteLine($"Lv.{target.Level} {target.Name}을(를) 공격했지만 아무일도 일어나지 않았습니다.");

                        }
                        else // 상대가 회피 못함
                        {
                            if (criRate > 85) // 크리터짐
                            {
                                int criDamege = (int)Math.Round(normalDamage * criDamageRate);
                                Console.WriteLine($"Lv.{target.Level} {target.Name} 을(를) 맞췄습니다. [데미지 : {criDamege}] - 치명타 공격!!\n");



                                if (target.HP - criDamege <= 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"\nLv.{target.Level} {target.Name}");
                                    Console.WriteLine($"HP {Math.Max(0, target.HP)} -> 0 (Dead)");
                                    Console.ResetColor();
                                    target.HP = 0;
                                }
                                else
                                {
                                    Console.WriteLine($"Lv.{target.Level} {target.Name}");
                                    Console.WriteLine($"HP {target.HP} - {criDamege} -> {target.HP - criDamege}");
                                }
                                target.HP -= criDamege;
                            }
                            else // 크리안터짐
                            {
                                Console.WriteLine($"Lv.{target.Level} {target.Name} 을(를) 맞췄습니다. [데미지 : {normalDamage}]\n");



                                if (target.HP - normalDamage <= 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"\nLv.{target.Level} {target.Name}");
                                    Console.WriteLine($"HP {target.HP} - {normalDamage} -> 0 (Dead)");
                                    Console.ResetColor();
                                    target.HP = 0;
                                }
                                else
                                {
                                    Console.WriteLine($"Lv.{target.Level} {target.Name}");
                                    Console.WriteLine($"HP {target.HP} - {normalDamage} -> {target.HP - normalDamage}");
                                }
                                target.HP -= normalDamage;
                            }

                        }
                        break; // 공격
                    case "2": // 스킬사용
                        switch (player.Job)
                        {
                            case "전사":
                                int mp_1 = 10;
                                int mp_2 = 15;
                                Console.Write($"1. 돌진 - MP {mp_1}\n   공격력*2로 하나의 적을 공격합니다.\n");
                                Console.Write($"2. 아레스의 포효 - MP {mp_2}\n   공격력*1.2로 3명의 적을 랜덤으로 공격합니다.\n\n원하시는 행동을 입력해주세요.\n>> ");
                                string? select = Console.ReadLine();
                                if (select == "1" && player.MP >= 10) // 스킬1
                                {
                                    float rate1 = rand.Next(9, 12) / 10f;
                                    int skillDamage_1 = (int)Math.Round(damage * rate1 * 2.0f);
                                    Console.WriteLine($"Lv.{target.Level} {target.Name}에게 돌진 을(를) 맞췄습니다. [데미지 : {skillDamage_1}]\n");
                                    Console.WriteLine($"MP {player.MP} - {mp_1} -> {player.MP - mp_1}\n");

                                    if (target.HP - skillDamage_1 <= 0)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine($"\nLv.{target.Level} {target.Name}");
                                        Console.WriteLine($"HP {target.HP} - {skillDamage_1} -> 0 (Dead)");
                                        Console.ResetColor();
                                        target.HP = 0;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Lv.{target.Level} {target.Name}");
                                        Console.WriteLine($"HP {target.HP} - {skillDamage_1} -> {target.HP - skillDamage_1}");
                                    }
                                    target.HP -= skillDamage_1;
                                    player.MP -= mp_1;
                                    break;

                                }
                                else if (select == "2" && player.MP >= 15) // 스킬2
                                {
                                    var aliveMonsters = monsters.Where(m => !m.IsDead).OrderBy(m => rand.Next()).Take(3).ToList();
                                    if (aliveMonsters.Count == 3)
                                    {
                                        float rate1 = rand.Next(9, 12) / 10f;
                                        float rate2 = rand.Next(9, 12) / 10f;
                                        float rate3 = rand.Next(9, 12) / 10f;
                                        Monster target1 = aliveMonsters[0];
                                        Monster target2 = aliveMonsters[1];
                                        Monster target3 = aliveMonsters[1];
                                        int skillDamage_2_1 = (int)Math.Round(damage * rate1 * 1.2f);
                                        int skillDamage_2_2 = (int)Math.Round(damage * rate2 * 1.2f);
                                        int skillDamage_2_3 = (int)Math.Round(damage * rate3 * 1.2f);
                                        Console.WriteLine($"Lv.{target1.Level} {target1.Name} / Lv.{target2.Level} {target2.Name} / Lv.{target3.Level} {target3.Name}에게 아레스의 포효 을(를) 맞췄습니다. [데미지 : {skillDamage_2_1} / {skillDamage_2_2} / {skillDamage_2_3}]\n");
                                        Console.WriteLine($"MP {player.MP} - {mp_2} -> {player.MP - mp_2}\n");

                                        if (target1.HP - skillDamage_2_1 <= 0)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"\nLv.{target1.Level} {target1.Name}");
                                            Console.WriteLine($"HP {target1.HP} - {skillDamage_2_1} -> 0 (Dead)");
                                            Console.ResetColor();
                                            target1.HP = 0;
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Lv.{target1.Level} {target1.Name}");
                                            Console.WriteLine($"HP {target1.HP} - {skillDamage_2_1} -> {target1.HP - skillDamage_2_1}");
                                        }
                                        if (target2.HP - skillDamage_2_2 <= 0)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"\nLv.{target2.Level} {target2.Name}");
                                            Console.WriteLine($"HP {target2.HP} - {skillDamage_2_2} -> 0 (Dead)");
                                            Console.ResetColor();
                                            target2.HP = 0;
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Lv.{target2.Level} {target2.Name}");
                                            Console.WriteLine($"HP {target2.HP} - {skillDamage_2_2} -> {target2.HP - skillDamage_2_2}");
                                        }
                                        if (target3.HP - skillDamage_2_3 <= 0)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"\nLv.{target2.Level} {target2.Name}");
                                            Console.WriteLine($"HP {target3.HP} - {skillDamage_2_3} -> 0 (Dead)");
                                            Console.ResetColor();
                                            target3.HP = 0;
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Lv.{target3.Level} {target3.Name}");
                                            Console.WriteLine($"HP {target3.HP} - {skillDamage_2_3} -> {target3.HP - skillDamage_2_3}");
                                        }
                                        target1.HP -= skillDamage_2_1;
                                        target2.HP -= skillDamage_2_2;
                                        target3.HP -= skillDamage_2_3;

                                    }

                                    if (aliveMonsters.Count == 2)
                                    {
                                        float rate1 = rand.Next(9, 12) / 10f;
                                        float rate2 = rand.Next(9, 12) / 10f;
                                        Monster target1 = aliveMonsters[0];
                                        Monster target2 = aliveMonsters[1];
                                        int skillDamage_2_1 = (int)Math.Round(damage * rate1 * 1.2f);
                                        int skillDamage_2_2 = (int)Math.Round(damage * rate2 * 1.2f);
                                        Console.WriteLine($"Lv.{target1.Level} {target1.Name} / Lv.{target2.Level} {target2.Name}에게 아레스의 포효 을(를) 맞췄습니다. [데미지 : {skillDamage_2_1} / {skillDamage_2_2}]\n");
                                        Console.WriteLine($"MP {player.MP} - {mp_2} -> {player.MP - mp_2}\n");

                                        if (target1.HP - skillDamage_2_1 <= 0)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"\nLv.{target1.Level} {target1.Name}");
                                            Console.WriteLine($"HP {target1.HP} - {skillDamage_2_1} -> 0 (Dead)");
                                            Console.ResetColor();
                                            target1.HP = 0;
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Lv.{target1.Level} {target1.Name}");
                                            Console.WriteLine($"HP {target1.HP} - {skillDamage_2_1} -> {target1.HP - skillDamage_2_1}");
                                        }
                                        if (target2.HP - skillDamage_2_2 <= 0)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"\nLv.{target2.Level} {target2.Name}");
                                            Console.WriteLine($"HP {target2.HP} - {skillDamage_2_2} -> 0 (Dead)");
                                            Console.ResetColor();
                                            target2.HP = 0;
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Lv.{target2.Level} {target2.Name}");
                                            Console.WriteLine($"HP {target2.HP} - {skillDamage_2_2} -> {target2.HP - skillDamage_2_2}");
                                        }
                                        target1.HP -= skillDamage_2_1;
                                        target2.HP -= skillDamage_2_2;

                                    }
                                    else if (aliveMonsters.Count == 1)
                                    {
                                        float rate1 = rand.Next(9, 12) / 10f;
                                        Monster target1 = aliveMonsters[0];
                                        int skillDamage_2 = (int)Math.Round(damage * rate1 * 1.5f);
                                        Console.WriteLine($"Lv.{target1.Level} {target1.Name}에게 아레스의 포효 을(를) 맞췄습니다. [데미지 : {skillDamage_2}]");
                                        Console.WriteLine($"MP {player.MP} - {mp_2} -> {player.MP - mp_2}\n");

                                        if (target1.HP - skillDamage_2 <= 0)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"\nLv.{target1.Level} {target1.Name}");
                                            Console.WriteLine($"HP {target1.HP} - {skillDamage_2} -> 0 (Dead)");
                                            Console.ResetColor();
                                            target1.HP = 0;
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Lv.{target1.Level} {target1.Name}");
                                            Console.WriteLine($"HP {target1.HP} - {skillDamage_2} -> {target1.HP - skillDamage_2}");
                                        }
                                        target1.HP -= skillDamage_2;

                                    }
                                    player.MP -= mp_2;
                                    break;

                                } // 스킬2
                                else if (select != "1" || select != "2")
                                {

                                }
                                else
                                {
                                    Console.WriteLine("MP가 부족합니다!");
                                }
                                continue; // 스킬
                            case "마법사":
                                int mp_3 = 8;
                                int mp_4 = 12;
                                Console.Write($"1. 에이르의 손길 - MP {mp_3}\n   공격력*2.5로 HP를 회복하여 자신을 치유 합니다.\n");
                                Console.Write($"2. 생텀 버스트 - MP {mp_4}\n   신성한 영역을 만들어 폭발을 일으킵니다.\n공격력*1.2로 3명의 적을 랜덤으로 공격합니다.\n\n원하시는 행동을 입력해주세요.\n>> ");
                                string? select2 = Console.ReadLine();
                                if (select2 == "1" && player.MP >= 8) // 스킬1
                                {
                                    float rate1 = rand.Next(9, 12) / 10f;
                                    int skillDamage_1 = (int)Math.Round(damage * rate1 * 2.5f);
                                    Console.WriteLine($"Lv.{player.Level} {player.Name}에게 에이르의 손길을(를) 사용했습니다. [HP회복 : {skillDamage_1}]\n");
                                    Console.WriteLine($"MP {player.MP} - {mp_3} -> {player.MP - mp_3}\n");


                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"Lv.{player.Level} {player.Name}");
                                    Console.WriteLine($"HP {player.HP} + {skillDamage_1} -> {(player.HP + skillDamage_1 > 50 ? player.MaxHP : player.HP + skillDamage_1)}");
                                    Console.ResetColor();


                                    player.HP += skillDamage_1;
                                    if (player.HP > player.MaxHP) player.HP = player.MaxHP;
                                    player.MP -= mp_3;
                                    break;

                                }
                                else if (select2 == "2" && player.MP >= 12) // 스킬2
                                {
                                    var aliveMonsters = monsters.Where(m => !m.IsDead).OrderBy(m => rand.Next()).Take(3).ToList();
                                    if (aliveMonsters.Count == 3)
                                    {
                                        float rate1 = rand.Next(9, 12) / 10f;
                                        float rate2 = rand.Next(9, 12) / 10f;
                                        float rate3 = rand.Next(9, 12) / 10f;
                                        Monster target1 = aliveMonsters[0];
                                        Monster target2 = aliveMonsters[1];
                                        Monster target3 = aliveMonsters[2];

                                        int skillDamage_2_1 = (int)Math.Round(damage * rate1 * 1.2f);
                                        int skillDamage_2_2 = (int)Math.Round(damage * rate2 * 1.2f);
                                        int skillDamage_2_3 = (int)Math.Round(damage * rate3 * 1.2f);
                                        Console.WriteLine($"Lv.{target1.Level} {target1.Name} / Lv.{target2.Level} {target2.Name} / Lv.{target3.Level} {target3.Name}에게 생텀 버스트 을(를) 맞췄습니다. [데미지 : {skillDamage_2_1} / {skillDamage_2_2} / {skillDamage_2_3}]\n");
                                        Console.WriteLine($"MP {player.MP} - {mp_4} -> {player.MP - mp_4}\n");

                                        if (target1.HP - skillDamage_2_1 <= 0)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"\nLv.{target1.Level} {target1.Name}");
                                            Console.WriteLine($"HP {target1.HP} - {skillDamage_2_1} -> 0 (Dead)");
                                            Console.ResetColor();
                                            target1.HP = 0;
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Lv.{target1.Level} {target1.Name}");
                                            Console.WriteLine($"HP {target1.HP} - {skillDamage_2_1} -> {target1.HP - skillDamage_2_1}");
                                        }
                                        if (target2.HP - skillDamage_2_2 <= 0)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"\nLv.{target2.Level} {target2.Name}");
                                            Console.WriteLine($"HP {target2.HP} - {skillDamage_2_2} -> 0 (Dead)");
                                            Console.ResetColor();
                                            target2.HP = 0;
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Lv.{target2.Level} {target2.Name}");
                                            Console.WriteLine($"HP {target2.HP} - {skillDamage_2_2} -> {target2.HP - skillDamage_2_2}");
                                        }
                                        if (target3.HP - skillDamage_2_3 <= 0)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"\nLv.{target3.Level} {target3.Name}");
                                            Console.WriteLine($"HP {target3.HP} - {skillDamage_2_3} -> 0 (Dead)");
                                            Console.ResetColor();
                                            target3.HP = 0;
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Lv.{target3.Level} {target3.Name}");
                                            Console.WriteLine($"HP {target3.HP} - {skillDamage_2_3} -> {target3.HP - skillDamage_2_3}");
                                        }
                                        target1.HP -= skillDamage_2_1;
                                        target2.HP -= skillDamage_2_2;
                                        target3.HP -= skillDamage_2_3;

                                    }
                                    else if (aliveMonsters.Count == 2)
                                    {
                                        float rate1 = rand.Next(9, 12) / 10f;
                                        float rate2 = rand.Next(9, 12) / 10f;
                                        Monster target1 = aliveMonsters[0];
                                        Monster target2 = aliveMonsters[1];

                                        int skillDamage_2_1 = (int)Math.Round(damage * rate1 * 1.2f);
                                        int skillDamage_2_2 = (int)Math.Round(damage * rate2 * 1.2f);
                                        Console.WriteLine($"Lv.{target1.Level} {target1.Name} / Lv.{target2.Level} {target2.Name}에게 생텀 버스트 을(를) 맞췄습니다. [데미지 : {skillDamage_2_1} / {skillDamage_2_2}]");
                                        Console.WriteLine($"MP {player.MP} - {mp_4} -> {player.MP - mp_4}\n");

                                        if (target1.HP - skillDamage_2_1 <= 0)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"\nLv.{target1.Level} {target1.Name}");
                                            Console.WriteLine($"HP {target1.HP} - {skillDamage_2_1} -> 0 (Dead)");
                                            Console.ResetColor();
                                            target1.HP = 0;
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Lv.{target1.Level} {target1.Name}");
                                            Console.WriteLine($"HP {target1.HP} - {skillDamage_2_1} -> {target1.HP - skillDamage_2_1}");
                                        }
                                        if (target2.HP - skillDamage_2_2 <= 0)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"\nLv.{target2.Level} {target2.Name}");
                                            Console.WriteLine($"HP {target2.HP} - {skillDamage_2_2} -> 0 (Dead)");
                                            Console.ResetColor();
                                            target2.HP = 0;
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Lv.{target2.Level} {target2.Name}");
                                            Console.WriteLine($"HP {target2.HP} - {skillDamage_2_2} -> {target2.HP - skillDamage_2_2}");
                                        }
                                        target1.HP -= skillDamage_2_1;
                                        target2.HP -= skillDamage_2_2;

                                    }
                                    else if (aliveMonsters.Count == 1)
                                    {
                                        float rate1 = rand.Next(9, 12) / 10f;
                                        Monster target1 = aliveMonsters[0];

                                        int skillDamage_2 = (int)Math.Round(damage * rate1 * 1.2f);
                                        Console.WriteLine($"Lv.{target1.Level} {target1.Name}에게 생텀 버스트 을(를) 맞췄습니다. [데미지 : {skillDamage_2}]");
                                        Console.WriteLine($"MP {player.MP} - {mp_4} -> {player.MP - mp_4}\n");

                                        if (target1.HP - skillDamage_2 <= 0)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"\nLv.{target1.Level} {target1.Name}");
                                            Console.WriteLine($"HP {target1.HP} - {skillDamage_2} -> 0 (Dead)");
                                            Console.ResetColor();
                                            target1.HP = 0;
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Lv.{target1.Level} {target1.Name}");
                                            Console.WriteLine($"HP {target1.HP} - {skillDamage_2} -> {target1.HP - skillDamage_2}");
                                        }
                                        target1.HP -= skillDamage_2;
                                    }
                                    player.MP -= mp_4;
                                    break;

                                } // 스킬2
                                else if (select2 != "1" || select2 != "2")
                                {

                                }
                                else
                                {
                                    Console.WriteLine("MP가 부족합니다!");
                                }
                                continue;
                            case "궁수":
                                int mp_5 = 10;
                                int mp_6 = 15;
                                Console.Write($"1. 관통 - MP {mp_5}\n   공격력*1.5으로 두명의 적을 관통하여 공격합니다.\n관통할때 데미지가 30% 감소합니다.\n");
                                Console.Write($"2. 팔랑크스의 화살비 - MP {mp_6}\n   공격력*1.2로 3명의 적을 랜덤으로 공격합니다.\n\n원하시는 행동을 입력해주세요.\n>> ");
                                string? select3 = Console.ReadLine();
                                if (select3 == "1" && player.MP >= 15) // 스킬1
                                {
                                    var aliveMonsters = monsters.Where(m => !m.IsDead).OrderBy(m => rand.Next()).Take(2).ToList();
                                    if (aliveMonsters.Count == 2)
                                    {
                                        float rate1 = rand.Next(9, 12) / 10f;
                                        float rate2 = rand.Next(9, 12) / 10f;
                                        Monster target1 = aliveMonsters[0];
                                        Monster target2 = aliveMonsters[1];
                                        int skillDamage_1_1 = (int)Math.Round(damage * rate1 * 1.5f);
                                        int skillDamage_1_2 = (int)Math.Round(damage * rate2 * 1.5f * 0.7f);
                                        Console.WriteLine($"Lv.{target1.Level} {target1.Name} / Lv.{target2.Level} {target2.Name}에게 관통 을(를) 맞췄습니다. [데미지 : {skillDamage_1_1} / {skillDamage_1_2}]\n");
                                        Console.WriteLine($"MP {player.MP} - {mp_6} -> {player.MP - mp_6}\n");

                                        if (target1.HP - skillDamage_1_1 <= 0)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"\nLv.{target1.Level} {target1.Name}");
                                            Console.WriteLine($"HP {target1.HP} - {skillDamage_1_1} -> 0 (Dead)");
                                            Console.ResetColor();
                                            target1.HP = 0;
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Lv.{target1.Level} {target1.Name}");
                                            Console.WriteLine($"HP {target1.HP} - {skillDamage_1_1} -> {target1.HP - skillDamage_1_1}");
                                        }
                                        if (target2.HP - skillDamage_1_2 <= 0)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"\nLv.{target2.Level} {target2.Name}");
                                            Console.WriteLine($"HP {target2.HP} - {skillDamage_1_2} -> 0 (Dead)");
                                            Console.ResetColor();
                                            target2.HP = 0;
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Lv.{target2.Level} {target2.Name}");
                                            Console.WriteLine($"HP {target2.HP} - {skillDamage_1_2} -> {target2.HP - skillDamage_1_2}");
                                        }
                                        target1.HP -= skillDamage_1_1;
                                        target2.HP -= skillDamage_1_2;

                                    }
                                    else if (aliveMonsters.Count == 1)
                                    {
                                        float rate1 = rand.Next(9, 12) / 10f;
                                        Monster target1 = aliveMonsters[0];
                                        int skillDamage_1 = (int)Math.Round(damage * 1.5f);
                                        Console.WriteLine($"Lv.{target1.Level} {target1.Name}에게 관통 을(를) 맞췄습니다. [데미지 : {skillDamage_1}]");
                                        Console.WriteLine($"MP {player.MP} - {mp_6} -> {player.MP - mp_6}\n");

                                        if (target1.HP - skillDamage_1 <= 0)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"\nLv.{target1.Level} {target1.Name}");
                                            Console.WriteLine($"HP {target1.HP} - {skillDamage_1} -> 0 (Dead)");
                                            Console.ResetColor();
                                            target1.HP = 0;
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Lv.{target1.Level} {target1.Name}");
                                            Console.WriteLine($"HP {target1.HP} - {skillDamage_1} -> {target1.HP - skillDamage_1}");
                                        }
                                        target.HP -= skillDamage_1;

                                    }
                                    player.MP -= mp_6;
                                    break;

                                }
                                else if (select3 == "2" && player.MP >= 15) // 스킬2
                                {
                                    var aliveMonsters = monsters.Where(m => !m.IsDead).OrderBy(m => rand.Next()).Take(3).ToList();
                                    if (aliveMonsters.Count == 3)
                                    {
                                        float rate1 = rand.Next(9, 12) / 10f;
                                        float rate2 = rand.Next(9, 12) / 10f;
                                        float rate3 = rand.Next(9, 12) / 10f;
                                        Monster target1 = aliveMonsters[0];
                                        Monster target2 = aliveMonsters[1];
                                        Monster target3 = aliveMonsters[2];
                                        int skillDamage_2_1 = (int)Math.Round(damage * rate1 * 1.2f);
                                        int skillDamage_2_2 = (int)Math.Round(damage * rate2 * 1.2f);
                                        int skillDamage_2_3 = (int)Math.Round(damage * rate3 * 1.2f);
                                        Console.WriteLine($"Lv.{target1.Level} {target1.Name} / Lv.{target2.Level} {target2.Name} / Lv.{target3.Level} {target3.Name} 에게 팔랑크스의 화살비 을(를) 맞췄습니다. [데미지 : {skillDamage_2_1} / {skillDamage_2_2} / {skillDamage_2_3}]\n");
                                        Console.WriteLine($"MP {player.MP} - {mp_6} -> {player.MP - mp_6}\n");

                                        if (target1.HP - skillDamage_2_1 <= 0)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"\nLv.{target1.Level} {target1.Name}");
                                            Console.WriteLine($"HP {target1.HP} - {skillDamage_2_1} -> 0 (Dead)");
                                            Console.ResetColor();
                                            target1.HP = 0;
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Lv.{target1.Level} {target1.Name}");
                                            Console.WriteLine($"HP {target1.HP} - {skillDamage_2_1} -> {target1.HP - skillDamage_2_1}");
                                        }
                                        if (target2.HP - skillDamage_2_2 <= 0)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"\nLv.{target2.Level} {target2.Name}");
                                            Console.WriteLine($"HP {target2.HP} - {skillDamage_2_2} -> 0 (Dead)");
                                            Console.ResetColor();
                                            target2.HP = 0;
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Lv.{target2.Level} {target2.Name}");
                                            Console.WriteLine($"HP {target2.HP} - {skillDamage_2_2} -> {target2.HP - skillDamage_2_2}");
                                        }
                                        if (target3.HP - skillDamage_2_3 <= 0)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"\nLv.{target3.Level} {target3.Name}");
                                            Console.WriteLine($"HP {target3.HP} - {skillDamage_2_3} -> 0 (Dead)");
                                            Console.ResetColor();
                                            target3.HP = 0;
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Lv.{target3.Level} {target3.Name}");
                                            Console.WriteLine($"HP {target3.HP} - {skillDamage_2_3} -> {target3.HP - skillDamage_2_3}");
                                        }
                                        target1.HP -= skillDamage_2_1;
                                        target2.HP -= skillDamage_2_2;
                                        target3.HP -= skillDamage_2_3;

                                    }
                                    if (aliveMonsters.Count == 2)
                                    {
                                        float rate1 = rand.Next(9, 12) / 10f;
                                        float rate2 = rand.Next(9, 12) / 10f;
                                        Monster target1 = aliveMonsters[0];
                                        Monster target2 = aliveMonsters[1];
                                        int skillDamage_2_1 = (int)Math.Round(damage * rate1 * 1.2f);
                                        int skillDamage_2_2 = (int)Math.Round(damage * rate2 * 1.2f);
                                        Console.WriteLine($"Lv.{target1.Level} {target1.Name} / Lv.{target2.Level} {target2.Name} 에게 팔랑크스의 화살비 을(를) 맞췄습니다. [데미지 : {skillDamage_2_1} / {skillDamage_2_2}]\n");
                                        Console.WriteLine($"MP {player.MP} - {mp_6} -> {player.MP - mp_6}\n");

                                        if (target1.HP - skillDamage_2_1 <= 0)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"\nLv.{target1.Level} {target1.Name}");
                                            Console.WriteLine($"HP {target1.HP} - {skillDamage_2_1} -> 0 (Dead)");
                                            Console.ResetColor();
                                            target1.HP = 0;
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Lv.{target1.Level} {target1.Name}");
                                            Console.WriteLine($"HP {target1.HP} - {skillDamage_2_1} -> {target1.HP - skillDamage_2_1}");
                                        }
                                        if (target2.HP - skillDamage_2_2 <= 0)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"\nLv.{target2.Level} {target2.Name}");
                                            Console.WriteLine($"HP {target2.HP} - {skillDamage_2_2} -> 0 (Dead)");
                                            Console.ResetColor();
                                            target2.HP = 0;
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Lv.{target2.Level} {target2.Name}");
                                            Console.WriteLine($"HP {target2.HP} - {skillDamage_2_2} -> {target2.HP - skillDamage_2_2}");
                                        }
                                        target1.HP -= skillDamage_2_1;
                                        target2.HP -= skillDamage_2_2;

                                    }
                                    if (aliveMonsters.Count == 1)
                                    {
                                        float rate1 = rand.Next(9, 12) / 10f;
                                        Monster target1 = aliveMonsters[0];
                                        int skillDamage_2_1 = (int)Math.Round(damage * rate1 * 1.2f);
                                        Console.WriteLine($"Lv.{target1.Level} {target1.Name} 에게 팔랑크스의 화살비 을(를) 맞췄습니다. [데미지 : {skillDamage_2_1}]\n");
                                        Console.WriteLine($"MP {player.MP} - {mp_6} -> {player.MP - mp_6}\n");

                                        if (target1.HP - skillDamage_2_1 <= 0)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"\nLv.{target1.Level} {target1.Name}");
                                            Console.WriteLine($"HP {target1.HP} - {skillDamage_2_1} -> 0 (Dead)");
                                            Console.ResetColor();
                                            target1.HP = 0;
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Lv.{target1.Level} {target1.Name}");
                                            Console.WriteLine($"HP {target1.HP} - {skillDamage_2_1} -> {target1.HP - skillDamage_2_1}");
                                        }
                                        target1.HP -= skillDamage_2_1;

                                    }
                                    player.MP -= mp_6;
                                    break;

                                } // 스킬2
                                else if (select3 != "1" || select3 != "2")
                                {

                                }
                                else
                                {
                                    Console.WriteLine("MP가 부족합니다!");
                                }
                                break;
                        }
                        break; // 스킬

                    case "3":
                        battleExpendables.UseExpend();
                        continue;
                    default:

                        continue;

                }//switch
                Console.WriteLine("\n0. 다음");
                Console.Write("\n>> ");
                while (Console.ReadLine() != "0")
                {
                    Console.Write("\n>> ");
                }
                break;
            }//while




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
                    monster.HP = 0;
                    monster.IsDead = true;
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
        


        private void BattleResult(Character player, List<Monster> monsters, QuestManager questManager, Inventory inventory, List<Item> allItems, List<Expendables> expendables)
        {
            DungeonResult dungeonResult = new DungeonResult(inventory, allItems, expendables); // 던전결과 클래스 초기화
            Console.Clear();
            Console.WriteLine("Battle!! - Result\n");

            if (player.HP <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You Lose\n");
                Console.ResetColor();

                Console.WriteLine($"Lv.{player.Level} {player.Name}");

                Console.WriteLine($"HP {player.HP} -> 0");

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Victory\n");
                Console.ResetColor();

                int defeatedCount = monsters.Count(m => m.IsDead);
                Console.WriteLine($"던전에서 몬스터 {monsters.Count}마리를 잡았습니다.\n");


                questManager.OngoingQuests(QuestType.MonsterKill, defeatedCount);
                Console.WriteLine($"Lv.{player.Level} {player.Name}");
                int damageTaken = player.beforeHP - player.HP;
                Console.WriteLine($"HP {player.beforeHP} -> {player.HP} (-{damageTaken})");
                Console.WriteLine($"MP {player.MP} -> {player.MP + 10} (+10)");
                //player.HP -= damageTaken;
                player.beforeHP = player.HP;
                player.MP += 10;
                if (player.MP >= player.MaxMP) player.MP = player.MaxMP;

                //던전리워드
                dungeonResult.LevelUp(monsters, player);
                dungeonResult.DungeonGold(monsters, player);
                dungeonResult.DungeonItem(monsters);


            }

            Console.WriteLine("\n0. 다음");
            while (Console.ReadLine() != "0") ;
        }
    }
}
