using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_RPG2_
{
    internal class UseExpendables
    {
        public List<Expendables> expendables { get; private set; }
        public void UseExpend()
        {
            Console.Clear();
            Console.WriteLine("인벤토리 - 소모품 사용");
            Console.WriteLine("보유중인 소모품을 사용할 수 있습니다.");
            Console.WriteLine("[소모품 목록]");
            Console.WriteLine();
            if (expendables.Count == 0)
            {
                Console.WriteLine(" 보유한 아이템이 없습니다.");
            }
            else
            {
                foreach (var exp in expendables)
                    Console.WriteLine(exp.expendablesPro.ToInventoryString());
            }
        }
    }
}
