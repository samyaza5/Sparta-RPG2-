using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_RPG2_
{
    public class ExpendablesEquipped
    {
        public Inventory inventory;

        public ExpendablesEquipped(Inventory inventory)
        {
            this.inventory = inventory;
        }
        public void ExpendablesEq()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("인벤토리 - 소모품 장착 관리");
                Console.WriteLine("보유한 소모품 장착을 관리할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine($"현재 장착된 소모품 수 : {inventory.expendables.Count(x => x.IsEquipped)}/ 5");
                Console.WriteLine();
                for (int i = 0; i < inventory.expendables.Count; i++)
                {
                    var ex = inventory.expendables[i];
                    string status = ex.IsEquipped ? "[장착]" : "";
                    Console.WriteLine($"{i + 1}. {status}{ex.expendablesPro.ToInventoryString()}");
                }
                
                Console.WriteLine("\n0. 나가기");
                Console.WriteLine("장착/해제할 아이템 번호를 입력하세요 : ");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int choice))
                {
                    if (choice == 0) break;

                    int index = choice - 1;
                    if (index >= 0 && index < inventory.expendables.Count)
                    {
                        var selected = inventory.expendables[index];

                        if (!selected.IsEquipped && inventory.expendables.Count(x => x.IsEquipped) >= 5)
                        {
                            Console.WriteLine("⚠ 이미 5개의 소모품이 장착되어 있습니다.");
                            Thread.Sleep(1000);
                            continue;
                        }
                        selected.IsEquipped = !selected.IsEquipped;
                        
                    }

                    else
                    {
                        Console.WriteLine("잘못된 번호입니다.");
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    Console.WriteLine("숫자를 입력해주세요.");
                    Thread.Sleep(1000);
                }
            }
        }
    }
}

