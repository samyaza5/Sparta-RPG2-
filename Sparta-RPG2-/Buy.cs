using System.Numerics;
using Sparta_RPG2_;


namespace Sparta_RPG2_
{
    class Buy//구매 상점
    {
        private ItemEquipped itemEquipped;
        private Inventory inventory;
        private Character character;
        private Shop? shop;

        private List<Item> allItems;
        private List<Expendables> expendables;

        public Buy(List<Item> allItems, List<Expendables> expendables, Character character, Inventory inventory, ItemEquipped itemEquipped)
        {
            this.allItems = allItems;
            this.expendables = expendables;
            this.character = character;
            this.inventory = inventory;
            this.itemEquipped = itemEquipped;
        }
        public void SetShop(Shop shop)
        {
            this.shop = shop;
        }


        public void BuyScene()
        {
            if (shop == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("⚠ 상점 정보가 없습니다. shop이 초기화되지 않았습니다.");
                Console.ResetColor();
                return;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== 🛒 상점 - 아이템 구매 ===");
                Console.WriteLine($"💰 보유 골드: {character.Gold:N0} G\n");

                Console.WriteLine("[아이템 목록]");
                int index = 1;
                foreach (var item in shop.allItems)
                {
                    Console.WriteLine($"{index++}. {item}");
                }

                foreach (var ex in shop.expendables)
                {
                    Console.WriteLine($"{index++}. {ex}");
                }

                Console.WriteLine("\n0. 나가기");
                Console.Write("\n원하시는 항목 번호를 입력해주세요: ");

                int choice;
                string? input = Console.ReadLine();
                if (!int.TryParse(input, out choice))
                {
                    Console.WriteLine("❌ 잘못된 입력입니다. 숫자를 입력해주세요.");
                    Thread.Sleep(1000);
                    continue;
                }

                if (choice == 0)
                {
                    shop.ShopScene(); // 혹은 break;
                    return;
                }

                int totalItems = shop.allItems.Count;
                int totalOptions = totalItems + shop.expendables.Count;

                if (choice < 1 || choice > totalOptions)
                {
                    Console.WriteLine("❌ 잘못된 번호입니다.");
                    Thread.Sleep(1000);
                    continue;
                }

                if (choice <= totalItems)
                {
                    HandleItemPurchase(shop.allItems[choice - 1]);
                }
                else
                {
                    int exIndex = choice - totalItems - 1;
                    HandleExpendablePurchase(shop.expendables[exIndex]);
                }

                Thread.Sleep(1000);
            }
        }

        private void HandleItemPurchase(Item item)
        {
            if (item.itemPro.IsSold)
            {
                Console.WriteLine("⚠ 이미 구매한 아이템입니다.");
            }
            else if (character.Gold < item.itemPro.ItemValue)
            {
                Console.WriteLine("⚠ 골드가 부족합니다.");
            }
            else
            {
                character.Gold -= item.itemPro.ItemValue;
                item.itemPro.IsSold = true;
                inventory.AllItems.Add(item);
                Console.WriteLine("✅ 아이템 구매 완료!");
            }
        }
        private void HandleExpendablePurchase(Expendables ex)
        {
            if (character.Gold < ex.expendablesPro.ItemValue)
            {
                Console.WriteLine("⚠ 골드가 부족합니다.");
            }
            else
            {
                var newEx = new Expendables(new ExpendablesPro(
                  ex.expendablesPro.ItemName,
                  ex.expendablesPro.ItemStat,
                  ex.expendablesPro.ItemInfo,
                  ex.expendablesPro.ItemValue
            ));
                character.Gold -= ex.expendablesPro.ItemValue;
                inventory.expendables.Add(newEx);
                Console.WriteLine("✅ 소모품 구매 완료!");
            }
        }

    }
}
    



