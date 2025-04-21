
using System;
using System.Collections.Generic;

namespace ConsoleApp5
{
    internal class RtanVilliage
    {
        static void Main(string[] args)
        {
            Inven inven = new Inven();
            Character player = new Character();
            Shop shop1 = new Shop();
            BuyArms buy = new BuyArms();

            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("마을에 도착하셨습니다. 무엇을 하시겠습니까?");
                Console.WriteLine("1. 상태창 보기 2.인벤토리 열기 3.상점 방문 4.마을 떠나기");
                Console.Write("선택: ");

                int enterVill;
                if (!int.TryParse(Console.ReadLine(), out enterVill))
                {
                    Console.WriteLine("숫자를 입력해주세요.");
                    WaitForKey();
                    continue;
                }

                Console.Clear();
                switch (enterVill)
                {
                    case 1:
                        Console.WriteLine("상태 보기를 선택하셨습니다.");
                        player.ShowStatus();
                        break;

                    case 2:
                        Console.WriteLine("인벤토리 열기를 선택하셨습니다.");
                        inven.Show(player);
                        Console.WriteLine("장비를 장착하시겠습니까? (번호 입력 / 취소: 엔터)");
                        string input = Console.ReadLine();
                        if (int.TryParse(input, out int equipIndex))
                        {
                            inven.Equip(player, equipIndex - 1);
                        }
                        break;

                    case 3:
                        Console.WriteLine("상점 방문을 선택하셨습니다.");
                        shop1.ShowShopping();
                        Console.WriteLine("물건을 구입하시겠습니까? '네' 아니면 아무 키나 입력하세요.");
                        string answer = Console.ReadLine();
                        if (answer == "네")
                            buy.Buy(player, shop1);
                        else
                            Console.WriteLine("마을로 돌아갑니다.");
                        break;

                    case 4:
                        Console.WriteLine("마을을 떠납니다.");
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }

                if (isRunning)
                    WaitForKey();
            }
        }

        static void WaitForKey()
        {
            Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
            Console.ReadKey();
        }

        public class Item
        {
            public string Name;
            public string Type;
            public int Power;
            public int Def;
            public int Price;
            public bool IsEquipped = false;
            public string Description;

            public Item(string name, string type, int power, int def, int price, string description = "")
            {
                Name = name;
                Type = type;
                Power = power;
                Def = def;
                Price = price;
                Description = description;
            }
        }

        public class Character
        {
            public string Name = "Chad";
            public string Job = "전사";
            public int Level = 1;
            public int Pow = 10;
            public int Def = 5;
            public int HP = 100;
            public int Gold = 1500;
            public List<Item> Inventory = new List<Item>();

            public void ShowStatus()
            {
                int totalPower = Pow;
                int totalDef = Def;

                foreach (var item in Inventory)
                {
                    if (item.IsEquipped)
                    {
                        totalPower += item.Power;
                        totalDef += item.Def;
                    }
                }

                Console.WriteLine($"Lv. {Level}");
                Console.WriteLine($"{Name} ({Job})");
                Console.WriteLine($"공격력 : {totalPower}");
                Console.WriteLine($"방어력 : {totalDef}");
                Console.WriteLine($"체 력 : {HP}");
                Console.WriteLine($"Gold : {Gold} G");
            }
        }

        public class Shop
        {
            public List<Item> Items = new List<Item>();

            public Shop()
            {
                Items.Add(new Item("수련자 갑옷", "갑옷", 0, 5, 800, "수련에 도움을 주는 갑옷입니다."));
                Items.Add(new Item("무쇠갑옷", "갑옷", 0, 9, 1000, "무쇠로 만들어져 튼튼한 갑옷입니다."));
                Items.Add(new Item("스파르타 갑옷", "갑옷", 0, 15, 3500, "스파르타 전사들이 사용한 전설의 갑옷입니다."));
                Items.Add(new Item("낡은 검", "무기", 2, 0, 600, "흔한 낡은 검입니다."));
                Items.Add(new Item("청동 도끼", "무기", 5, 0, 800, "어디선가 사용됐던 도끼입니다."));
                Items.Add(new Item("스파르타의 창", "무기", 7, 0, 1500, "스파르타 전사들이 사용한 전설의 창입니다."));
            }

            public void ShowShopping()
            {
                Console.WriteLine("[상점 아이템 목록]");
                for (int i = 0; i < Items.Count; i++)
                {
                    var item = Items[i];
                    Console.WriteLine($"{i + 1}. {item.Name} | 공격력+{item.Power} | 방어력+{item.Def} | {item.Price} G");
                    Console.WriteLine($"   - {item.Description}");
                }
            }
        }

        public class BuyArms
        {
            public void Buy(Character player, Shop shop)
            {
                Console.WriteLine("구매할 아이템 번호를 입력하세요:");
                int input = int.Parse(Console.ReadLine());

                if (input >= 1 && input <= shop.Items.Count)
                {
                    int index = input - 1;
                    Item selectedItem = shop.Items[index];

                    if (player.Gold >= selectedItem.Price)
                    {
                        player.Gold -= selectedItem.Price;
                        player.Inventory.Add(selectedItem);
                        shop.Items.RemoveAt(index);
                        Console.WriteLine($"\"{selectedItem.Name}\"을 구매했습니다!");
                        Console.WriteLine($"남은 골드: {player.Gold} G");
                    }
                    else
                    {
                        Console.WriteLine("골드가 부족합니다.");
                    }
                }
                else
                {
                    Console.WriteLine("존재하지 않는 아이템 번호입니다.");
                }
            }
        }

        public class Inven
        {
            public void Show(Character player)
            {
                Console.WriteLine("[인벤토리]");
                if (player.Inventory.Count == 0)
                {
                    Console.WriteLine("인벤토리가 비어 있습니다.");
                }
                else
                {
                    for (int i = 0; i < player.Inventory.Count; i++)
                    {
                        var item = player.Inventory[i];
                        string equippedMark = item.IsEquipped ? " [E]" : "";
                        Console.WriteLine($"{i + 1}. {item.Name}{equippedMark} (공+{item.Power}, 방+{item.Def})");
                        Console.WriteLine($"   - {item.Description}");
                    }
                }
            }

            public void Equip(Character player, int index)
            {
                if (index < 0 || index >= player.Inventory.Count)
                {
                    Console.WriteLine("존재하지 않는 인덱스입니다.");
                    return;
                }

                foreach (var item in player.Inventory)
                {
                    item.IsEquipped = false;
                }

                player.Inventory[index].IsEquipped = true;
                Console.WriteLine($"{player.Inventory[index].Name} 장착 완료!");
            }
        }
    }
}
