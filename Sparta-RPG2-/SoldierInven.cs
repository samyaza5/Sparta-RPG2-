using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_RPG2_
{
    public class SoldierInven
    {
        public List<Soldier> soldiers { get; private set; }
        private SoldierEquipped soldierEquipped;
        private Character player;

        public SoldierInven(Character player)
        {
            this.player = player;
            soldiers = new List<Soldier>();
            soldierEquipped = new SoldierEquipped(this, player);
        }

        public void InventoryScene()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("병영");
                Console.WriteLine("소속 병사들을 관리할 수 있습니다.\n");
                Console.WriteLine("[병사 목록]");

                if (soldiers.Count == 0)
                {
                    Console.WriteLine(" 소속 병사가 없습니다.");
                }
                else
                {
                    foreach (var soldier in soldiers)
                        Console.WriteLine(soldier.soldierPro.ToInventoryString());
                }

                Console.WriteLine("\n1. 병사 출정 관리");
                Console.WriteLine("2. 병사 훈련");
                Console.WriteLine("0. 나가기");
                Console.Write("\n원하시는 행동을 입력해주세요: ");

                string? input = Console.ReadLine();
                if (int.TryParse(input, out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            soldierEquipped.EqualsScene(); // null 체크 추가
                            break;
                        case 2:
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
