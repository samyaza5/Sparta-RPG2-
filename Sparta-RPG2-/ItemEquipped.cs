using Sparta_RPG2_;
using System;
using System.Collections.Generic;
using System.Threading;
using static Sparta_RPG2_.Quest;


namespace Sparta_RPG2_
{
    public class ItemEquipped
    {
        public Inventory inventory;
        public Character player;
        public UseExpendables useExpendables;
        public QuestManager questManager;

        public ItemEquipped(Character player, Inventory inventory, UseExpendables useExpendables, QuestManager questManager)
        {
            this.player = player;
            this.inventory = inventory;
            this.useExpendables = useExpendables;
            this.questManager = questManager;
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
                // 콘솔 클리어
                Console.WriteLine("인벤토리 - 장비 장착 관리");
                Console.WriteLine("보유 중인 장비를 관리할 수 있습니다.");
                Console.WriteLine("[아이템 목록]");
                Console.WriteLine();

                if (inventory.AllItems.Count == 0 && inventory.expendables.Count == 0)
                {
                    Console.WriteLine("보유한 아이템이 없습니다.");
                }
                else
                {
                    for (int i = 0; i < inventory.AllItems.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}{inventory.AllItems[i].itemPro.ToInventoryString()}");
                    }
                }

                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요");

                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    if (input == 0)
                        return;
                    int index = input - 1;


                    if (inventory != null && index >= 0 && index < inventory.AllItems.Count)
                    {
                        var selectedItem = inventory.AllItems[index];
                        if (selectedItem.itemPro.IsEquipped)
                        {
                            Console.WriteLine("이미 장착한 아이템입니다.");
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            foreach (var item in inventory.AllItems)
                            {
                                if (item.itemPro.IsArmor && selectedItem.itemPro.IsArmor)
                                    item.itemPro.IsEquipped = false;
                                if (item.itemPro.IsWeapon && selectedItem.itemPro.IsWeapon)
                                    item.itemPro.IsEquipped = false;
                            }

                            selectedItem.itemPro.IsEquipped = true;
                            UpdateStatsFromInventory(inventory.AllItems);
                            Console.WriteLine($"'{selectedItem.itemPro.ItemName}'를 장착했습니다!");
                            Thread.Sleep(1000);

                            questManager?.OngoingQuests(QuestType.EquipItem, 1);  // ✅ 장착 퀘스트 진행 반영
                        }
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다!");
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다!");
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
