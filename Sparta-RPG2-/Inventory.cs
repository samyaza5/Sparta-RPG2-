using System;
using System.Collections.Generic;
using System.Threading;
using Sparta_RPG2_;
using static Sparta_RPG2_.Quest;


namespace Sparta_RPG2_
{
    public class Inventory
    {
        public List<Item> AllItems { get; private set; }
        public List<Expendables> expendables { get; private set; }

        private ItemEquipped itemEquipped;
        private UseExpendables useExpendables;
        private ExpendablesEquipped expendablesEquipped;
        private Character player;
        public QuestManager questManager;

        public Inventory(Character player, QuestManager questManager)
        {
            expendablesEquipped = new ExpendablesEquipped(this);
            AllItems = new List<Item>();
            expendables = new List<Expendables>();
            this.player = player;
            useExpendables = new UseExpendables(player, this);
            itemEquipped = new ItemEquipped(player, this, useExpendables, questManager);  // ✅ 정상 전달
        }

        public void SetItemEquipped(ItemEquipped equipped)
        {
            this.itemEquipped = equipped;
        }

        public void InventoryScene()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
                Console.WriteLine("[아이템 목록]");

                if (AllItems.Count == 0 && expendables.Count == 0)
                {
                    Console.WriteLine(" 보유한 아이템이 없습니다.");
                }
                else
                {
                    foreach (var item in AllItems)
                        Console.WriteLine(item.itemPro.ToInventoryString());

                    foreach (var exp in expendables)
                        Console.WriteLine(exp.expendablesPro.ToInventoryString());
                }

                Console.WriteLine("\n1. 장비 장착 관리");
                Console.WriteLine("2. 소모품 장착 관리");
                Console.WriteLine("3. 소모품 사용");
                Console.WriteLine("0. 나가기");
                Console.Write("\n원하시는 행동을 입력해주세요: ");

                string? input = Console.ReadLine();
                if (int.TryParse(input, out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            itemEquipped.EqualsScene(); // null 체크 추가
                            break;
                        case 2:
                            expendablesEquipped.ExpendablesEq();
                                break;
                        case 3:
                            useExpendables.UseExpend();
                            break;
                        case 0:
                            return;
                        default:
                            Console.WriteLine("잘못된 입력입니다!");
                            Thread.Sleep(1000);
                            break;
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
