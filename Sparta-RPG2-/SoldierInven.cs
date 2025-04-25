using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Sparta_RPG2_
{
    public class SoldierInven
    {
        public List<Soldier> AllSoldiers => soldiers;
        public readonly List<Soldier> soldiers = new();
        public Soldier? EquippedSoldier { get; set; }

        private readonly SoldierEquipped soldierEquipped;
        private readonly SoldierUnequipper soldierUnequipper;
        private readonly Character player;

        public SoldierInven(Character player)
        {
            this.player = player ?? throw new ArgumentNullException(nameof(player));
            soldierEquipped = new SoldierEquipped(this, player);
            soldierUnequipper = new SoldierUnequipper(this, player);
        }

        public void InventoryScene()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                ShowHeader();
                ShowSoldiers();

                Console.WriteLine("\n1. 병사 출정 관리");
                Console.WriteLine("2. 병사 휴식 관리");
                Console.WriteLine("0. 나가기");
                Console.Write("\n원하시는 행동을 입력해주세요: ");

                int choice = ParseMenuChoice();
                isRunning = HandleMenuChoice(choice); // 👈 리턴값으로 루프 제어
            }
        }

        private void ShowHeader()
        {
            Console.WriteLine("병영");
            Console.WriteLine("소속 병사들을 관리할 수 있습니다.\n");
            Console.WriteLine("[병사 목록]");
        }

        private void ShowSoldiers()
        {
            if (soldiers.Count == 0)
            {
                Console.WriteLine(" 소속 병사가 없습니다.");
                return;
            }

            var grouped = soldiers.GroupBy(s => s.soldierPro.ItemName);
            foreach (var group in grouped)
            {
                var example = group.First();
                Console.WriteLine($" {example.soldierPro.ToInventoryString()} {group.Count()}명");
            }
        }

        private int ParseMenuChoice()
        {
            string? input = Console.ReadLine();
            return int.TryParse(input, out int result) ? result : -1;
        }

        private bool HandleMenuChoice(int choice)
        {
            switch (choice)
            {
                case 1:
                    soldierEquipped.EqualsScene();
                    return true;
                case 2:
                    soldierUnequipper.UnequipScene();
                    return true;
                case 0:
                    return false; // 👈 병영 루프 종료
                default:
                    Console.WriteLine("잘못된 입력입니다!");
                    Thread.Sleep(1000);
                    return true;
            }
        }
    }
}
