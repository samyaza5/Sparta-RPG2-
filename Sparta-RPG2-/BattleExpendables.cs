using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_SJ;

namespace Sparta_RPG2_
{
    public class BattleExpendables
    {
        private Character player;
        Inventory inventory;

        public BattleExpendables(Character player, Inventory inventory)
        {
            this.player = player;
            this.inventory = inventory;
        }
        void UpdateStatsFromExpendables(List<Expendables> expendables)
        {
            foreach (var item in expendables)
            {

                if (item.expendablesPro.ItemName == "회복물약")
                {
                    player.HP += item.expendablesPro.ItemStat;
                    if (player.HP > player.MaxHP)
                    {
                        player.HP = player.MaxHP;
                    }

                }
                else if (item.expendablesPro.ItemName == "마나물약")
                {
                    player.MP += item.expendablesPro.ItemStat;
                    if (player.MP > player.MaxMP)
                    {
                        player.MP = player.MaxMP;
                    }
                }


            }
        }
        public void UseExpend()
        {
            var equippedExpendables = inventory.expendables.Where(x => x.IsEquipped).ToList();
            Console.Clear();
            Console.WriteLine("전투 - 소모품 사용");
            Console.WriteLine("장착중인 소모품을 사용할 수 있습니다.");
            Console.WriteLine("[소모품 목록]");
            Console.WriteLine();
            if (equippedExpendables.Count == 0)
            {
                Console.WriteLine("장착한 물약이 없습니다.");
            }
            else
            {
                for (int i = 0; i < equippedExpendables.Count; i++)
                {
                    var exp = equippedExpendables[i];
                    Console.WriteLine($"[{i + 1}] {exp.expendablesPro.ToInventoryString()}");
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
                if (inventory != null && index >= 0 && index < equippedExpendables.Count)
                {
                    var selectedItem = equippedExpendables[index];
                    UpdateStatsFromExpendables(new List<Expendables> { selectedItem });
                    Console.WriteLine($"'{selectedItem.expendablesPro.ItemName}'를 사용했습니다!");
                    inventory.expendables.Remove(selectedItem);
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
