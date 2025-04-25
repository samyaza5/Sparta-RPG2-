using System.Numerics;
using System.Xml.Linq;

namespace Sparta_RPG2_
{
    public class Skill
    {
        public Character Player { get; set; }
        public PassiveSkill PassiveSkill { get; set; }

        public Skill(Character player, PassiveSkill passiveSkill)
        {
            Player = player;
            PassiveSkill = passiveSkill;
        }

        public static List<PassiveSkill> mySkill = new List<PassiveSkill>();

        public static int healAmount = 0;

        public static void SkillShop(Character player, PassiveSkill passiveSkill)
        {
            Console.Clear();
            Console.WriteLine($"스킬\n스킬을 선택하고 강화할 수 있습니다.\n\n보유 스킬포인트 : {player.SP}\n");

            foreach (PassiveSkill skill in passiveSkill.SkillList)
            {
                Console.WriteLine($"{skill.Name} - {skill.Description}");
            }

            Console.Write("\n\n0. 나가기\n1. 스킬 강화\n2. 스킬 관리\n>>");
            string? input = Console.ReadLine();
            if (input == "0")
            {
                return;
            }
            else if (input == "1")
            {
                LearnSkill(player, passiveSkill);

            }
            else if (input == "2")
            {
                SkillManager(player, passiveSkill);
            }


        }

        public static void LearnSkill(Character player, PassiveSkill passiveSkill)
        {
            Console.Clear();
            Console.WriteLine($"스킬 강화\n선택한 스킬을 강화합니다.\n\n보유 스킬포인트 : {player.SP}\n");
            while (true)
            {
                int idx = 1;
                Console.Clear();
                foreach (PassiveSkill skill in passiveSkill.SkillList)
                {
                    int lv = skill.SkillLv;
                    int max = skill.MaxSkillLv;
                    Console.WriteLine($"{idx}. {(lv == max ? "[M]" : "")}{skill.Name} - {skill.Description} ({skill.SkillLv}/{skill.MaxSkillLv})"); idx++;
                }
                Console.WriteLine("\n\n강화할 스킬을 선택해주세요.");
                Console.Write("0. 나가기\n>>");
                int input = int.Parse(Console.ReadLine());

                Console.WriteLine("\n");
                switch (input)
                {
                    case 0:
                        SkillShop(player, passiveSkill);
                        return;
                    case 1: // 1번스킬 강화
                        if (passiveSkill.SkillList[0].MasterSkill == true)
                        {
                            Console.WriteLine("이미 습득한 스킬입니다.");
                            Thread.Sleep(1000);
                            break;
                        }
                        if (player.SP > 0)
                        {
                            Console.WriteLine($"{passiveSkill.SkillList[input-1].Name}을 습득했습니다.");
                            Console.WriteLine($"최대HP {player.MaxHP} -> {player.MaxHP * passiveSkill.SkillList[input - 1].Stat} ");
                            passiveSkill.SkillList[0].MasterSkill = true;
                            player.MaxHP = (int)Math.Round(player.MaxHP * passiveSkill.SkillList[input-1].Stat);
                            player.HP = player.MaxHP;
                            player.SP--;
                            passiveSkill.SkillList[input - 1].SkillLv ++;
                            mySkill.Add(passiveSkill.SkillList[input-1]);

                        }
                        else
                        {
                            Console.WriteLine("스킬포인트가 부족합니다.");
                        }
                        break;
                    case 2: // 2번스킬 강화
                        if (passiveSkill.SkillList[1].MasterSkill == true)
                        {
                            Console.WriteLine("이미 습득한 스킬입니다.");
                            Thread.Sleep(1000);
                            break;
                        }
                        if (player.SP > 0)
                        {
                            Console.WriteLine($"{passiveSkill.SkillList[input-1].Name}을 습득했습니다.");
                            Console.WriteLine($"방어력 {player.Defense} -> {player.Defense + passiveSkill.SkillList[input - 1].Stat} ");
                            
                            player.Defense = (int)(player.Defense + passiveSkill.SkillList[input - 1].Stat);
                            player.SP--;
                            passiveSkill.SkillList[input - 1].SkillLv ++;
                            mySkill.Add(passiveSkill.SkillList[input - 1]);
                            passiveSkill.SkillList[input - 1].GetSkill = true;
                            if (passiveSkill.SkillList[input - 1].SkillLv == passiveSkill.SkillList[input - 1].MaxSkillLv)
                            {
                                passiveSkill.SkillList[input - 1].MasterSkill = true;
                            }


                        }
                        else
                        {
                            Console.WriteLine("스킬포인트가 부족합니다.");
                        }
                        break;
                    case 3: // 2번스킬 강화
                        if (passiveSkill.SkillList[input - 1].MasterSkill == true)
                        {
                            Console.WriteLine("이미 습득한 스킬입니다.");
                            Thread.Sleep(1000);
                            break;
                        }
                        if (player.SP > 0)
                        {
                            Console.WriteLine($"{passiveSkill.SkillList[input - 1].Name}을 습득했습니다.");
                            Console.WriteLine($"방어력 {player.Defense} -> {player.Defense + passiveSkill.SkillList[input - 1].Stat} ");

                            healAmount += (int)Math.Round(passiveSkill.SkillList[input - 1].Stat);
                            player.SP--;
                            passiveSkill.SkillList[input - 1].SkillLv++;
                            mySkill.Add(passiveSkill.SkillList[input - 1]);
                            passiveSkill.SkillList[input - 1].GetSkill = true;
                            if (passiveSkill.SkillList[input - 1].SkillLv == passiveSkill.SkillList[input - 1].MaxSkillLv)
                            {
                                passiveSkill.SkillList[input - 1].MasterSkill = true;
                            }


                        }
                        else
                        {
                            Console.WriteLine("스킬포인트가 부족합니다.");
                        }
                        break;


                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }


            }


        }
        private static void SkillManager(Character player, PassiveSkill passiveSkill)
        {
            var uniqueSkills = mySkill.GroupBy(skill => skill.Name)
                                     .Select(group => group.First())
                                     .ToList();
            Console.Clear();
            Console.WriteLine($"습득한 스킬\n습득한 스킬을 관리합니다.\n\n보유 스킬포인트 : {player.SP}\n");
            int idx = 1;
            foreach (PassiveSkill skill in uniqueSkills)
            {
                int lv = skill.SkillLv;
                int max = skill.MaxSkillLv;
                Console.WriteLine($"{idx}. {(lv == max ? "[M]" : "")}{skill.Name} - {skill.Description} ({skill.SkillLv}/{skill.MaxSkillLv})"); idx++;
            }

            Console.Write("\n0. 나가기\n>>");
            while (Console.ReadLine() != "0")
            {
                Console.Write("\n>> ");
            }
            SkillShop(player, passiveSkill);

        }
    }
}

