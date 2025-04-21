using System.Text.Json.Serialization;
using RPG_SJ;
using Sparta_RPG2_;

namespace RPG_SJ
{
    internal partial class Program
    {
        public class Inventory//인벤토리
        {
            public List<Item> AllItems;
            public List<Expendables> expendables;

            public Inventory()
            {
                this.AllItems = new List<Item>();
                expendables = new List<Expendables>();
            }

            public void InventoryScene()
            {
                Console.Clear();
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                Console.WriteLine();
                if (AllItems.Count == 0)
                {
                    Console.WriteLine(" 보유한 아이템이 없습니다.");
                }
                else
                {
                    for (int i = 0; i < AllItems.Count; i++)
                    {
                        Console.WriteLine(AllItems[i].itemPro.ToInventoryString());
                    }
                    for (int i = 0; i < expendables.Count; i++)
                    {
                        Console.WriteLine(expendables[i].expendablesPro.ToInventorytring());
                    }
                }
                Console.WriteLine();
                Console.WriteLine("1. 장착 관리");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
              
                }
            }
        }
    }

