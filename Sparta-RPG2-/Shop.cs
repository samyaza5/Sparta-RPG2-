using System.Numerics;
using System.Text.Json.Serialization;
using Sparta_RPG2_;
using static RPG_SJ.Program;

namespace RPG_SJ
{
    internal partial class Program
    {
        class Shop//상점
        {
            this.character = character;
            AllItems = new List<Item>();
            expendables = new List<Expendables>(); // ✅ 반드시 초기화해야 함
            public List<Item> allItems;
            public List<Expendables> expendables;
            Program Program;
            Buy buy;

            Character character;

            public Shop(Character character, List<Item> allItems, List<Expendables> expendables, Buy buy)
            {
                this.character = character;
                this.allItems = allItems;
                this.expendables = expendables;
                this.buy = buy;
            }

            public void ShopScene()
            {
                Console.Clear();
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{character.Gold}G");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                foreach (var Item in allItems)
                {
                    Console.WriteLine(Item);
                }
                foreach (var Item in expendables)
                {
                    Console.WriteLine(Item);
                }
                Console.WriteLine();
                Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("2. 아이템 판매");
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
                       ShopScene();
                    }
                }
                if (choice == 1)
                {
                   buy.BuyScene();
                }
                else if (choice == 0)
                {
                    Program.ShowStartMenu(character); // 판매 구현 아직
                }
                else if (choice == 2)
                {
                    ShopScene();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다!");
                    Thread.Sleep(1000);
                    ShopScene();
                }
            }
        }
    }

}
