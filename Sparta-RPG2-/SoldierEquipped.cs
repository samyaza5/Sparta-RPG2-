using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sparta_RPG2_
{
    public class SoldierEquipped
    {
        public SoldierInven SoldierInven;
        public Character player;

        public SoldierEquipped(Character player, SoldierInven soldierInven)
        {
            this.player = player;
            this.SoldierInven = soldierInven;
        }

        public SoldierEquipped(SoldierInven soldierInven, Character player)
        {
            SoldierInven = soldierInven;
            this.player = player;
        }

        public void UpdateStatsFromSoldierInven(List<Soldier> soldiers)
        {
            player.SoldierAttack = 0;
            player.SoldierDefense = 0;

            foreach (var soldier in soldiers)
            {
                if (soldier.soldierPro.IsEquipped)
                {
                        player.SoldierAttack += soldier.soldierPro.Attack;
                        player.SoldierDefense += soldier.soldierPro.Defense;
                }
            }
        }

        public void EqualsScene()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("병영 -  병사 출정 관리");
                Console.WriteLine("소속 병사를 관리할 수 있습니다.");
                Console.WriteLine("[병사 목록]");
                Console.WriteLine();

                if (SoldierInven.soldiers.Count == 0 && SoldierInven.soldiers.Count == 0)
                {
                    Console.WriteLine("소속 병사가 없습니다.");
                }
                else
                {
                    for (int i = 0; i < SoldierInven.soldiers.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}{SoldierInven.soldiers[i].soldierPro.ToInventoryString()}");
                    }
                }

                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요");

                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    if (input == 0)
                        return;
                    int index = input - 1;


                    if (SoldierInven != null && index >= 0 && index < SoldierInven.soldiers.Count)
                    {
                        var selectedItem = SoldierInven.soldiers[index];

                        selectedItem.soldierPro.IsEquipped = true;
                        UpdateStatsFromSoldierInven(SoldierInven.soldiers);
                            Console.WriteLine($"'{selectedItem.soldierPro.ItemName}'가 준비완료 됐습니다!");
                            Thread.Sleep(1000);
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다!");
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다!");
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
