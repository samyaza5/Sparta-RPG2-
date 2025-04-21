using static RPG_SJ.Program;

namespace RPG_SJ
{
    class ItemEquipped
    {
        public Inventory inventory;
        public Character player;
        
        public ItemEquipped(Character player, Inventory inventory)
        {
            this.player = player;
            this.inventory = inventory;
        }
        public void EqualsScene()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("인벤토리 - 장착 관리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine("[아이템 목록]");
                Console.WriteLine();

                if (inventory.AllItems.Count == 0)
                {
                    Console.WriteLine("보유한 아이템이 없습니다.");
                }
                else
                {
                    for (int i = 0; i < inventory.AllItems.Count; i++)
                    {
                        Console.WriteLine($"{i+1}{inventory.AllItems[i].itemPro.ToInventoryString()}");
                    }
                }

                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요");

              
            }
        }
    }
}

