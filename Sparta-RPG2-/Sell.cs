using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_RPG2_
{
    class Sell
    {
        private Inventory inventory;
        private Character character;

        public Sell(Inventory inventory, Character character)
        {
            this.inventory = inventory; ;
            this.character = character;
        }

        public void SellScene()
        {
            Console.Clear();
            Console.WriteLine("아이템 판매");
            Console.WriteLine("현재 보유 아이템:");

            int index = 1;
            foreach (var item in inventory.AllItems)
            {
                Console.WriteLine($"{index++}. {item}");
            }

            Console.WriteLine("\n판매할 아이템 번호를 입력하거나, 0을 입력해 돌아갑니다.");

            if (!int.TryParse(Console.ReadLine()?.Trim(), out int choice) || choice < 0 || choice > inventory.AllItems.Count)
            {
                Console.WriteLine("잘못된 입력입니다!");
                Thread.Sleep(1000);
                return;
            }

            if (choice == 0)
            {
                return; // 상점으로 복귀
            }

            var selectedItem = inventory.AllItems[choice - 1];
            character.Gold += selectedItem.itemPro.SellPrice; // 판매 시 골드 획득 (SellPrice 프로퍼티 필요)
            inventory.AllItems.Remove(selectedItem);

            Console.WriteLine($"'{selectedItem.itemPro.ItemName}'을 판매했습니다! (+{selectedItem.itemPro.SellPrice}G)");
            Thread.Sleep(1000);
        }

    }
}
