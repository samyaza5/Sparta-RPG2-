using System.Numerics;
using Sparta_RPG2_;
using static RPG_SJ.Program;

namespace RPG_SJ
{
    internal partial class Program
    {
        class Buy//구매 상점
        {
            public List<Item> AllItems = new List<Item>();
            private ItemEquipped itemEquipped;
            public List<Expendables> expendables = new List<Expendables>();
            private Inventory inventory;
            public Character character = new Character();
            private static Buy buy;
            Shop shop;

            public Buy(List<Expendables> expendables, List<Item> AllItems, Character character, Inventory inventory, ItemEquipped itemEquipped)
            {
                this.expendables = expendables;
                this.inventory = inventory;
                this.character = character;
                this.AllItems = AllItems;
                this.itemEquipped = itemEquipped;

                shop = new Shop(character, this); // ✅ 여기서 꼭 new 해주기
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
                    for (int i = 0; i < AllItems.Count; i++)//아이템의 배열을 위부터 반복해 출력
                    {
                        Console.WriteLine($"{i + 1} {AllItems[i]}");
                    }
                    for (int i = 0; i < expendables.Count; i++)//아이템의 배열을 위부터 반복해 출력
                    {
                        Console.WriteLine($"{i + 1} {expendables[i]}");
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

                    int index = choice - 1;

                    if (index >= 0 && index < AllItems.Count + expendables.Count)
                    {
                        var selectedItem = AllItems[index];
                        var selectedExpendable = expendables[index];
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

                        if (!selectedExpendable.expendablesPro.IsSold && character.Gold >= selectedExpendable.expendablesPro.ItemValue)
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


