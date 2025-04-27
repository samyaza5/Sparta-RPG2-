using System.Numerics;
using System.Text.Json.Serialization;
using Sparta_RPG2_;

namespace Sparta_RPG2_
{
    class Shop // 상점
    {
        public List<Item> allItems;
        public List<Expendables> expendables;
        Buy buy;
        Sell sell;

        Character character; // 필드 선언만 남김

        public Shop(Character character, Inventory inventory, List<Expendables> expendables, Buy buy)
        {
            this.character = character;
            this.allItems = inventory.AllItems;
            this.expendables = expendables;
            this.buy = buy;
            this.sell = new Sell(inventory, character);
        }

        public void ShopScene()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{character.Gold}G");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                foreach (var item in allItems)
                {
                    Console.WriteLine(item);
                }
                foreach (var item in expendables)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine();
                Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("2. 아이템 판매");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.Write("원하시는 행동을 입력해주세요: ");

                if (!int.TryParse(Console.ReadLine()?.Trim(), out int choice))
                {
                    Console.WriteLine("잘못된 입력입니다! (숫자를 입력하세요)");
                    Thread.Sleep(1000);
                    continue; // while 루프 반복
                }

                switch (choice)
                {
                    case 1:
                        buy.BuyScene();
                        break;

                    case 2:
                        sell.SellScene(); // 🛒 판매 기능 추가 예정
                        break;

                    case 0:
                        Program.ShowStartMenu(); // 메인 메뉴로 복귀
                        return;

                    default:
                        Console.WriteLine("잘못된 선택입니다!");
                        Thread.Sleep(1000);
                        break;
                }
            }
        }


    }
}

