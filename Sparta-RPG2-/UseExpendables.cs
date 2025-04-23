using System;
using RPG_SJ;

namespace Sparta_RPG2_
{
    internal class UseExpendables
    {
        private Character player;
        Inventory inventory;

        public UseExpendables(Character player, Inventory inventory)
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
                else if (item.expendablesPro.ItemName == "마나물약{")
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
            Console.Clear();
            Console.WriteLine("인벤토리 - 소모품 사용");
            Console.WriteLine("보유중인 소모품을 사용할 수 있습니다.");
            Console.WriteLine("[소모품 목록]");
            Console.WriteLine();
            if (inventory.expendables.Count == 0)
            {
                Console.WriteLine(" 보유한 아이템이 없습니다.");
            }
            else
            {
                for (int i = 0; i < inventory.expendables.Count; i++)
                {
                    var exp = inventory.expendables[i];
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
                if (inventory != null && index >= 0 && index < inventory.expendables.Count)
                {
                    var selectedItem = inventory.expendables[index];
                        UpdateStatsFromExpendables(inventory.expendables);
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
    

