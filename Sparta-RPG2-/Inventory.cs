using System;
using System.Collections.Generic;
using System.Threading;
using Sparta_RPG2_;
using RPG_SJ;
using static RPG_SJ.Program;

namespace RPG_SJ
{
    public class Inventory
    {
        public List<Item> AllItems { get; private set; }
        public List<Expendables> expendables { get; private set; }

        private ItemEquipped? itemEquipped;
        private Character? player;

        public Inventory()
        {
            AllItems = new List<Item>();
            expendables = new List<Expendables>();
        }

        // 의존성 주입 메서드
        public void SetDependencies(ItemEquipped itemEquipped, Character player)
        {
            this.itemEquipped = itemEquipped;
            this.player = player;
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

                Console.WriteLine("\n1. 장착 관리");
                Console.WriteLine("0. 나가기");
                Console.Write("\n원하시는 행동을 입력해주세요: ");

                string input = Console.ReadLine();
                if (int.TryParse(input, out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            itemEquipped?.EqualsScene(); // null 체크 추가
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
