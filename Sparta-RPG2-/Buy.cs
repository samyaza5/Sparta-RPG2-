using System.Numerics;
using Sparta_RPG2_;
using static RPG_SJ.Program;

namespace RPG_SJ
{
    internal partial class Program
    {
        class Buy//구매 상점
        {
            private ItemEquipped itemEquipped;
            private Inventory inventory;
            private Character character;
            private Shop shop;

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
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("상점 - 아이템 구매");
                    Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                    Console.WriteLine();
                    Console.WriteLine("[보유 골드]");
                    Console.WriteLine($"{character.Gold}G");
                    Console.WriteLine();
                    Console.WriteLine("[아이템 목록]");
                    int index = 1;
                    foreach (var Item in shop.allItems)
                    {
                        Console.WriteLine($"{index++}.{Item}");
                    }
                    foreach (var ex in shop.expendables)
                    {
                        Console.WriteLine($"{index++}.{ex}");
                    }
                    Console.WriteLine();
                    Console.WriteLine("0. 나가기");
                    Console.WriteLine();
                    Console.WriteLine("원하시는 행동을 입력해주세요.");

                    int choice;
                    while (true)
                    {
                        string input = Console.ReadLine();
                        if (int.TryParse(input, out choice))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("잘못된 입력입니다!");
                            Thread.Sleep(1000);
                            BuyScene();
                        }
                    }
                    if (choice == 0) shop.ShopScene();

                    int totalItems = shop.allItems.Count;

                    if (choice <= totalItems)
                    {
                        var selectedItem = shop.allItems[choice - 1];
                        if (!selectedItem.itemPro.IsSold && character.Gold >= selectedItem.itemPro.ItemValue)
                        {
                            character.Gold -= selectedItem.itemPro.ItemValue;
                            selectedItem.itemPro.IsSold = true;
                            inventory.AllItems.Add(selectedItem);
                            Console.WriteLine("구매 완료!");

                            Thread.Sleep(1000);
                        }
                        else if (!selectedItem.itemPro.IsSold && character.Gold < selectedItem.itemPro.ItemValue)
                        {
                            Console.WriteLine("Gold가 부족합니다.");
                            Thread.Sleep(1000);
                        }
                        else if (selectedItem.itemPro.IsSold)
                        {
                            Console.WriteLine("이미 구매한 아이템입니다.");
                            Thread.Sleep(1000);
                        }
                        else {
                            int exIndex = choice - totalItems - 1;
                            var selectedExpendable = shop.expendables[exIndex];
                            if(!selectedExpendable.expendablesPro.IsSold && character.Gold >= selectedExpendable.expendablesPro.ItemValue)
                            {
                                character.Gold -= selectedExpendable.expendablesPro.ItemValue;
                                selectedExpendable.expendablesPro.IsSold = true;
                                inventory.expendables.Add(selectedExpendable);

                                Console.WriteLine("구매 완료!");
                                Thread.Sleep(1000);
                            }
                            else if (!selectedExpendable.expendablesPro.IsSold && character.Gold < selectedExpendable.expendablesPro.ItemValue)
                            {
                                Console.WriteLine("Gold가 부족합니다.");
                                Thread.Sleep(1000);
                            }
                            else if (selectedExpendable.expendablesPro.IsSold)
                            {
                                Console.WriteLine("이미 구매한 아이템입니다.");
                                Thread.Sleep(1000);
                            }
                        }
                       
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다!");
                        Thread.Sleep(1000);
                        BuyScene();
                    }
                }

            }
        }
    }
}


