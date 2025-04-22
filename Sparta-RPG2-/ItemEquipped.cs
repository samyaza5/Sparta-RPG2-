using Sparta_RPG2_;
using static RPG_SJ.Program;

namespace RPG_SJ
{
    internal partial class Program
    {
        public Inventory inventory;
        public Character player;
        
        public ItemEquipped(Character player, Inventory inventory)
        {
            this.player = player;
            this.inventory = inventory;
        }
        public void UpdateStatsFromInventory(List<Item> items)
        {
            int WeaponPower = 0;
            int ArmorPower = 0;

            player.WeaponPower = 0;
            player.ArmorPower = 0;

        public class ItemEquipped
        {
            private Character player;
            private Inventory inventory;

            public ItemEquipped(Character player, Inventory inventory)
            {
                this.player = player;
                this.inventory = inventory;
            }
            public void UpdateStatsFromInventory(List<Item> items)
            {
                player.WeaponPower = 0;
                player.ArmorPower = 0;

                foreach (var item in items)
                {
                    if (item.itemPro.IsEquipped)
                    {
                        if (item.itemPro.IsWeapon)
                            player.WeaponPower += item.itemPro.ItemStat;
                        if (item.itemPro.IsArmor)
                            player.ArmorPower += item.itemPro.ItemStat;
                    }
                }
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

                    if (Program.inventory?.AllItems == null || Program.inventory.AllItems.Count == 0)
                    {
                        Console.WriteLine("보유한 아이템이 없습니다.");
                    }
                    else
                    {
                        for (int i = 0; i < Program.inventory.AllItems.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}{Program.inventory.AllItems[i].itemPro.ToInventoryString()}");
                        }
                    }

                    Console.WriteLine();
                    Console.WriteLine("0. 나가기");
                    Console.WriteLine();
                    Console.WriteLine("원하시는 행동을 입력해주세요");

                    if (int.TryParse(Console.ReadLine(), out int input))
                    {
                        if (input == 0)
                            Program.inventory.InventoryScene();

                        int index = input - 1;
                        if (index >= 0 && index < Program.inventory.AllItems.Count)
                        {
                            var selectedItem = Program.inventory.AllItems[index];
                            if (selectedItem.itemPro.IsEquipped)
                            {
                                Console.WriteLine("이미 장착한 아이템입니다.");
                                Thread.Sleep(1000);
                            }
                            else
                            {
                                foreach (var item in Program.inventory.AllItems)
                                {
                                    if (item.itemPro.IsArmor && selectedItem.itemPro.IsArmor)
                                        item.itemPro.IsEquipped = false;
                                    if (item.itemPro.IsWeapon && selectedItem.itemPro.IsWeapon)
                                        item.itemPro.IsEquipped = false;
                                }


                                selectedItem.itemPro.IsEquipped = true;
                                UpdateStatsFromInventory(Program.inventory.AllItems);
                                Console.WriteLine($"'{selectedItem.itemPro.ItemName}'를 장착했습니다!");
                                Thread.Sleep(1000);
                            }
                        }
                        else
                        {
                            Console.WriteLine("잘못된 입력입니다!");
                            Thread.Sleep(1000);
                            EqualsScene();
                        }
                    }

                    else
                    {
                        Console.WriteLine("잘못된 입력입니다!");
                        Thread.Sleep(1000);
                        EqualsScene();
                    }
                }
            }
        }
    }
}

    


