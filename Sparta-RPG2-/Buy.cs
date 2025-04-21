namespace TextRPG
{
    class Buy//구매 상점
    {
        public List<Item> AllItems;
        Inventory inventory;
        ItemEquipped itemEquipped;
       Player player;
        public Buy(List<Item> AllItems, Player player, Inventory inventory, ItemEquipped itemEquipped)
        {
            this.inventory = inventory;
            this.player = player;
            this.AllItems = AllItems;
            this.itemEquipped = itemEquipped;
        }
        public Place BuyScene()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("상점 - 아이템 구매");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{player.haveGold}G");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < AllItems.Count; i++)//아이템의 배열을 위부터 반복해 출력
                {
                    Console.WriteLine($"{i + 1} {AllItems[i]}");
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
                        return Place.Buy;

                    }
                }
                if (choice == 0) return Place.Shop;

                int index = choice - 1;

                if (index >= 0 && index < AllItems.Count)
                {
                    var selectedItem = AllItems[index];
                    if (!selectedItem.itemPro.IsSold && player.haveGold >= selectedItem.itemPro.ItemValue)
                    {
                        player.haveGold -= selectedItem.itemPro.ItemValue;
                        selectedItem.itemPro.IsSold = true;
                        inventory.AllItems.Add(selectedItem);
                       
                        Console.WriteLine("구매 완료!");
                        Thread.Sleep(1000);
                    }
                    else if (!selectedItem.itemPro.IsSold && player.haveGold < selectedItem.itemPro.ItemValue)
                    {
                        Console.WriteLine("Gold가 부족합니다.");
                        Thread.Sleep(1000);
                    }
                    else if (selectedItem.itemPro.IsSold)
                    {
                        Console.WriteLine("이미 구매한 아이템입니다.");
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다!");
                    Thread.Sleep(1000);
                    return Place.Buy;
                }
            }
        }
    } 
}

