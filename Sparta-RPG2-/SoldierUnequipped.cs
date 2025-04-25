using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Sparta_RPG2_
{
    public class SoldierUnequipper
    {
        private SoldierInven soldierInven;
        private Character player;

        public SoldierUnequipper(SoldierInven soldierInven, Character player)
        {
            this.soldierInven = soldierInven;
            this.player = player;
        }

        public void UnequipScene()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("병영 - 병사 출정 해제");

                var equippedSoldiers = soldierInven.soldiers
                    .Where(s => s.soldierPro.IsEquipped)
                    .ToList();

                if (equippedSoldiers.Count == 0)
                {
                    Console.WriteLine("출정 중인 병사가 없습니다.");
                    Thread.Sleep(1000);
                    return;
                }

                Console.WriteLine("\n[출정 준비 중인 병사 목록]");
                for (int i = 0; i < equippedSoldiers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {equippedSoldiers[i].soldierPro.ToInventoryString()}");
                }

                Console.WriteLine("0. 나가기");
                Console.Write("\n휴식시킬 병사의 번호를 선택하세요: ");

                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    if (input == 0)
                        return;

                    int index = input - 1;

                    if (index >= 0 && index < equippedSoldiers.Count)
                    {
                        var soldier = equippedSoldiers[index];
                        soldier.soldierPro.IsEquipped = false;

                        UpdateStats();

                        Console.WriteLine($"{soldier.soldierPro.ItemName} 병사 해제 완료!");
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        Console.WriteLine("잘못된 번호입니다.");
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    Console.WriteLine("숫자를 입력해주세요.");
                    Thread.Sleep(1000);
                }
            }
        }

        private void UpdateStats()
        {
            player.SoldierAttack = 0;
            player.SoldierDefense = 0;

            foreach (var soldier in soldierInven.soldiers.Where(s => s.soldierPro.IsEquipped))
            {
                player.SoldierAttack += soldier.soldierPro.Attack;
                player.SoldierDefense += soldier.soldierPro.Defense;
            }
        }
    }
}
