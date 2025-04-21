using static RPG_SJ.Program;

namespace RPG_SJ
{
    class Buy//구매 상점
    {
        public List<Item> AllItems;
        Inventory inventory;
        ItemEquipped itemEquipped;
        Character character;
        public Buy(List<Item> AllItems, Character character, Inventory inventory, ItemEquipped itemEquipped)
        {
            this.inventory = inventory;
            this.character = character;
            this.AllItems = AllItems;
            this.itemEquipped = itemEquipped;
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
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");

                
            }
        }
    } 
}

