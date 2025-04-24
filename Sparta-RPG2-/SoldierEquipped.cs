using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sparta_RPG2_
{
    public class SoldierEquipped
    {
        public SoldierInven SoldierInven;
        public Character player;
        private List<Soldier> equippedSoldiers;

        public void AddToEquipped(Soldier soldier)
        {
            soldier.soldierPro.IsEquipped = true;
            equippedSoldiers.Add(soldier);
        }

        public SoldierEquipped(SoldierInven soldierInven, Character player)
        {
            SoldierInven = soldierInven;
            this.player = player;
            this.equippedSoldiers = new List<Soldier>();
        }

        public void UpdateStatsFromSoldierInven(List<Soldier> soldiers)
        {
            player.SoldierAttack = 0;
            player.SoldierDefense = 0;

            foreach (var soldier in soldiers)
            {
                if (soldier.soldierPro.IsEquipped)
                {
                        player.SoldierAttack += soldier.soldierPro.Attack;
                        player.SoldierDefense += soldier.soldierPro.Defense;
                }
            }
        }

        public void EqualsScene()
        {
            var grouped = SoldierInven.soldiers.GroupBy(s => s.soldierPro.ItemName).ToList();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("병영 -  병사 출정 관리");
                Console.WriteLine("소속 병사를 관리할 수 있습니다.");
                Console.WriteLine("[병사 목록]");
                Console.WriteLine();

                if (SoldierInven.soldiers.Count == 0 && SoldierInven.soldiers.Count == 0)
                {
                    Console.WriteLine("소속 병사가 없습니다.");
                }
                else
                {
                    for (int i = 0; i < grouped.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {grouped[i].Key} {grouped[i].Count()}명");
                    }
                }

                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("출정시킬 병사의 종류를 선택하세요");

                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    if (input == 0)
                        return;
                    int index = input - 1;


                    if (SoldierInven != null && index >= 0 && index < SoldierInven.soldiers.Count)
                    {
                        var selectedGroup = grouped[index].ToList();
                        Console.WriteLine($"\n[{selectedGroup[0].soldierPro.ToInventoryString} 목록]");
                        for (int i = 0; i < selectedGroup.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {selectedGroup[i].soldierPro.ToInventoryString()}");
                        }

                        Console.Write("몇 명을 출정시키겠습니까? ");
                        int count = int.Parse(Console.ReadLine());

                        var selectedToDeploy = selectedGroup.Take(count).ToList();

                        if(selectedToDeploy.Count > selectedGroup.Count)
                        {
                            Console.WriteLine($"{selectedGroup}는 소속 인원은 {selectedGroup.Count}명 입니다.");
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            foreach (var soldier in selectedToDeploy)
                        {
                            Console.WriteLine("추가 전: " + equippedSoldiers.Count);
                            AddToEquipped(soldier);
                            Console.WriteLine("추가 후: " + equippedSoldiers.Count);
                            Console.WriteLine($"{soldier.soldierPro.ItemName} 출정!");
                            Thread.Sleep(1000);
                        }

                        }
                        // 출정 리스트에 추가
                        
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
