using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_RPG2_
{
    internal partial class Program
    {
       public class BuySoldier//구매 상점
        {
            private SoldierEquipped soldierEquipped;
            private SoldierInven soldierInven;
            private Character character;
            private Pub? pub;

            private List<Soldier> soldiers;

            public BuySoldier(List<Soldier> soldiers, Character character, SoldierInven soldierInven, SoldierEquipped soldierEquipped )
            {    
                this.character = character;
                this.soldierInven = soldierInven;
                this.soldierEquipped = soldierEquipped;
                this.soldiers = soldiers;
                
            }
            public void SetShop(Pub pub)
            {
                this.pub = pub;
            }


            public void BuyScene()
            {
                if (pub == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("⚠ 상점 정보가 없습니다. shop이 초기화되지 않았습니다.");
                    Console.ResetColor();
                    return;
                }

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("=== 선술집 - 병사 모집 ===");
                    Console.WriteLine($"💰 보유 골드: {character.Gold:N0} G\n");

                    Console.WriteLine("[아이템 목록]");
                    int index = 1;
                    foreach (var item in pub.soldiers)
                    {
                        Console.WriteLine($"{index++}. {item}");
                    }
                    Console.WriteLine("\n0. 나가기");
                    Console.Write("\n원하시는 항목 번호를 입력해주세요: ");

                    int choice;
                    string? input = Console.ReadLine();
                    if (!int.TryParse(input, out choice))
                    {
                        Console.WriteLine("❌ 잘못된 입력입니다. 숫자를 입력해주세요.");
                        Thread.Sleep(1000);
                        continue;
                    }
                    if (choice == 0)
                    {
                        pub.ShopScene(); // 혹은 break;
                        return;
                    }
                    if (choice < 1 || choice > pub.soldiers.Count)
                    {
                        Console.WriteLine("❌ 잘못된 번호입니다.");
                        Thread.Sleep(1000);
                        continue;
                    }
                    if (choice <= pub.soldiers.Count)
                    {
                        HandleSoldierPurchase(pub.soldiers[choice-1]);
                    }


                    Thread.Sleep(1000);
                }
            }

            private void HandleSoldierPurchase(Soldier soldier)
            {
                if (character.Gold < soldier.soldierPro.ItemValue)
                {
                    Console.WriteLine("⚠ 골드가 부족합니다.");
                }
                else
                {
                    character.Gold -= soldier.soldierPro.ItemValue;
                    Soldier newSoldier = new Soldier(soldier.soldierPro.Clone());

                    soldierInven.soldiers.Add(newSoldier);
                    Console.WriteLine($"✅ {newSoldier.soldierPro.ItemName} 고용 완료!");
                }
            }
        }
    }
}
