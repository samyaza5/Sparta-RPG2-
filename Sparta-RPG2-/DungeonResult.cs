using Sparta_RPG2_;
using System;
using System.Reflection;

namespace RPG_SJ;
internal partial class Program
{
    public class DungeonResult
    {

        Inventory inventory;
        List<Item> itemList;
        List<Expendables> expendableList;
        string[] monsterName = { "공허충", "미니언", "대포미니언" };

        public DungeonResult(Inventory inventory, List<Item> itemList, List<Expendables> expendables)
        {
            this.inventory = inventory;
            this.itemList = itemList;
            this.expendableList = expendables;
        }
        //경험치 보상 ,레벨업
        public void LevelUp(List<Monster> deadMonsterList, Character player)
        {
            int beforeExp = player.Exp;
            int beforeLevel = player.Level;
            int addExp = 0;
            List<int> levelUpExp = new List<int>()
            {
                10,
                45,
                100,
                200
            };

            for (int i = 0; i < deadMonsterList.Count; i++)
            {
                addExp += deadMonsterList[i].Level * 1;
            }

            int needExp = levelUpExp[player.Level - 1];
            player.Exp += addExp;

            //if (player.Exp > needExp)
            //{
            //    player.Level++;
            //    player.Attack++;
            //    player.Defense++;
            //}

            int newLevel = 1;
            for (int i = 0; i < levelUpExp.Count; i++)
            {
                if (player.Exp >= levelUpExp[i])
                    newLevel = i + 2; // 레벨은 인덱스 + 1 (0-based) + 1
                else
                    break;
            }

            if (newLevel > player.Level)
            {
                int levelGain = newLevel - player.Level;
                player.Level = newLevel;
                player.Attack += levelGain;
                player.Defense += levelGain;
            }


            Console.WriteLine($"Lv.{beforeLevel} -> Lv.{player.Level} {player.Name}");
            Console.WriteLine($"exp {beforeExp} -> {player.Exp}");
        }

        //골드 보상
        public void DungeonGold(List<Monster> deadMonsterList, Character player)
        {
            int addGold = 0;

            for (int i = 0; i < deadMonsterList.Count; i++)
            {
                int monsterGold = 0;
                string deadMonsterName = deadMonsterList[i].Name;
                if (deadMonsterName == monsterName[0])
                {
                    monsterGold = 90;
                }
                else if (deadMonsterName == monsterName[1])
                {
                    monsterGold = 100;
                }
                else if (deadMonsterName == monsterName[2])
                {
                    monsterGold = 110;
                }
                int levelGold = (deadMonsterList[i].Level * 10);
                //int atkGold = deadMonsterList[i].Attack * 10;
                addGold += (levelGold + monsterGold);
            }
            player.Gold += addGold;

            Console.WriteLine("[획득아이템]");
            Console.WriteLine($"{addGold} Gold");
        }
        //임시
        //Program program;
        //Inventory inventory;
        //Character player;
        //ItemEquipped itemEquipped;
        //List<Item> itemDb = new List<Item>();

        //아이템보상
        public void DungeonItem(List<Monster> deadMonsterList)
        {
            //inventory = new Inventory(itemEquipped, player, program); // 임시
            //itemDb.Add(new Item(Item.BeginnerArmor())); //임시데이터
            //itemDb.Add(new Item(Item.IronArmor())); // 임시데이터

            //드랍아이템확률
            Random rand = new Random();

            //랜덤아이템추가 
            List<string> getItem = new List<string>();


            for (int j = 0; j < deadMonsterList.Count; j++)
            {
                int dropNormalItemRate = 0;
                int dropRareItemRate = 0;
                int monsterLevel = deadMonsterList[j].Level;
                dropNormalItemRate += 10 + (monsterLevel * 1);
                if (monsterLevel > 4)
                {
                    dropRareItemRate = 10;
                }
                int randItemRate = rand.Next(0, 100);
                string deadMonsterName = deadMonsterList[j].Name;
                
                //int rareItemIndex = itemList.Count - 1;

                if (randItemRate < dropRareItemRate)  //레어드롭아이템추가   
                {
                    int rareRandItem = rand.Next(itemList.Count - 2, itemList.Count);
                    inventory.AllItems.Add(itemList[rareRandItem]);
                    getItem.Add(itemList[rareRandItem].itemPro.ItemName);
                    //Console.WriteLine(itemList[rareItemIndex].itemPro.ItemName);
                    ////테스트출력
                    //Console.WriteLine("테스트출력");
                    //Console.WriteLine($"rare:{dropRareItemRate} normal:{dropNormalItemRate}");
                    //for (int i = 0; i < inventory.AllItems.Count; i++)
                    //{
                    //    Console.WriteLine($": {inventory.AllItems[i].itemPro.ItemName}");
                    //}
                }
                else if (randItemRate < dropNormalItemRate)   //기본드롭아이템추가
                {
                    int randItem = rand.Next(7, itemList.Count - 2);
                    inventory.AllItems.Add(itemList[randItem]);
                    getItem.Add(itemList[randItem].itemPro.ItemName);
                    //Console.WriteLine(itemList[randItem].itemPro.ItemName);
                    ////테스트출력
                    //Console.WriteLine("테스트출력");
                    //Console.WriteLine($"rare:{dropRareItemRate} normal:{dropNormalItemRate}");
                    //for (int i = 0; i < inventory.AllItems.Count; i++)
                    //{
                    //    Console.WriteLine($": {inventory.AllItems[i].itemPro.ItemName}");
                    //}
                }
                else
                {
                    if (deadMonsterName == monsterName[0])
                    {
                        int randItem = rand.Next(0, expendableList.Count);
                        inventory.expendables.Add(expendableList[randItem]);
                        getItem.Add(expendableList[randItem].expendablesPro.ItemName);

                    }
                    else if (deadMonsterName == monsterName[1])
                    {
                        int randItem = rand.Next(0,  4);
                        inventory.AllItems.Add(itemList[randItem]);
                        getItem.Add(itemList[randItem].itemPro.ItemName);
                    }
                    else if (deadMonsterName == monsterName[2])
                    {
                        int randItem = rand.Next(4, 7);
                        inventory.AllItems.Add(itemList[randItem]);
                        getItem.Add(itemList[randItem].itemPro.ItemName);
                    }
                }
                
                Console.WriteLine($"-{getItem[j]}");
            }
        }
    }
}
