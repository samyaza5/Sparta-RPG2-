using System.Text.Json.Serialization;
using RPG_SJ;

namespace RPG_SJ
{
    internal partial class Program
    {
        public class Inventory//인벤토리
        {
            public List<Item> AllItems;

            public Inventory()
            {
                this.AllItems = new List<Item>();
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

