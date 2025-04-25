using Sparta_RPG2_;
using System;
using System.Linq;
using System.Threading;

public class SoldierEquipped
{
    public SoldierInven SoldierInven;
    public Character player;

    public SoldierEquipped(SoldierInven soldierInven, Character player)
    {
        SoldierInven = soldierInven;
        this.player = player;
    }

    public void AddToEquipped(Soldier soldier)
    {
        soldier.soldierPro.IsEquipped = true;
        UpdateStatsFromSoldierInven();
    }

    public void UpdateStatsFromSoldierInven()
    {
        player.SoldierAttack = 0;
        player.SoldierDefense = 0;

        foreach (var soldier in SoldierInven.soldiers.Where(s => s.soldierPro.IsEquipped))
        {
            player.SoldierAttack += soldier.soldierPro.Attack;
            player.SoldierDefense += soldier.soldierPro.Defense;
        }
    }

    public void EqualsScene()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("병영 - 병사 출정 관리");

            ShowSoldierSummary();

            Console.WriteLine("\n[출정 병사 선택]");
            var groupedAll = SoldierInven.soldiers.GroupBy(s => s.soldierPro.ItemName).ToList();

            for (int i = 0; i < groupedAll.Count; i++)
                Console.WriteLine($"{i + 1}. {groupedAll[i].Key} 병사 출정");

            Console.WriteLine("0. 출정 완료");
            Console.Write("\n원하는 병사 선택 : ");

            if (!int.TryParse(Console.ReadLine(), out int input))
            {
                InvalidInput();
                continue;
            }

            if (input == 0)
            {
                Console.WriteLine("출정이 완료되었습니다.");
                Thread.Sleep(1000);
                return;
            }

            if (input < 1 || input > groupedAll.Count)
            {
                InvalidInput();
                continue;
            }

            string name = groupedAll[input - 1].Key;
            var available = SoldierInven.soldiers.Where(s => s.soldierPro.ItemName == name && !s.soldierPro.IsEquipped).ToList();
            var equipped = SoldierInven.soldiers.Where(s => s.soldierPro.ItemName == name && s.soldierPro.IsEquipped).ToList();

            if (available.Count == 0)
            {
                Console.WriteLine("출정 가능한 병사가 없습니다.");
                GameSaveManager.AutoSave(player, Program.inventory!, Program.questManager!, Program.itemEquipped!, Program.dungeonManager!, SoldierInven);
                Thread.Sleep(1000);
                continue;
            }

            Console.Write($"\n몇 명을 출정시키겠습니까? (휴식 중인 병사 : {available.Count}명): ");
            if (!int.TryParse(Console.ReadLine(), out int count))
            {
                InvalidInput();
                continue;
            }

            if (count > player.Level - equipped.Count)
                Console.WriteLine($"당신의 지휘력이 부족합니다. 최대 : {player.Level}");
            else if (count > available.Count)
                Console.WriteLine($"휴식 중인 병사는 {available.Count}명 입니다.");
            else
            {
                foreach (var soldier in available.Take(count))
                    AddToEquipped(soldier);

                Console.WriteLine($"{name} 병사 {count}명 출정 완료!");
            }

            Thread.Sleep(1000);
        }
    }

    private void ShowSoldierSummary()
    {
        var grouped = SoldierInven.soldiers.GroupBy(s => s.soldierPro.ItemName);
        Console.WriteLine($"\n[병사 상태 요약] -  최대 {player.Level}명\n");

        foreach (var group in grouped)
        {
            int total = group.Count();
            int equipped = group.Count(s => s.soldierPro.IsEquipped);
            int available = total - equipped;

            Console.WriteLine($"- {group.Key}: 휴식 중 {available}명 / 출정 중 {equipped}명");
        }
    }

    private void InvalidInput()
    {
        Console.WriteLine("잘못된 입력입니다. 숫자를 입력해주세요.");
        Thread.Sleep(1000);
    }
}
