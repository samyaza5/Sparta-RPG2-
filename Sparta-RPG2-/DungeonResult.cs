using System.Numerics;

namespace Sparta_RPG2_
{
    public class DungeonResult
    {
        //test

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

            int[] ints = new int[100];

            List<int> levelUpExp = new List<int>()
            {
                10,
                45,
                100,
                200,
            };

            for (int i = 3; i < 100; i++)
            {
                levelUpExp.Add(levelUpExp[i] * 2);
            }
 
            int needExp = levelUpExp[player.Level - 1];

            for (int i = 0; i < deadMonsterList.Count; i++)
            {
                addExp += deadMonsterList[i].Level * 1;
            }
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


            Console.Write($"{(beforeLevel == player.Level ? "" : $"Lv.")}");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{(beforeLevel == player.Level ? "" : $"{beforeLevel}")}");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"{(beforeLevel == player.Level ? "" : $" -> ")}");
            Console.ResetColor();
            Console.Write($"{(beforeLevel == player.Level ? "" : $"Lv.")}");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{(beforeLevel == player.Level ? "" : $"{player.Level}")}");
            Console.ResetColor();
            Console.Write("exp ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{beforeExp}");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($" -> ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{player.Exp}");
            Console.ResetColor();
        }

        //배틀골드 보상
        public void BattleGold(List<Monster> deadMonsterList, Character player)
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
            Console.WriteLine();
            Console.WriteLine("[획득아이템]");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{addGold}");
            Console.ResetColor();
            Console.WriteLine(" Gold");
        }

        //던전골드보상
        public void DungeonGold(List<Monster> deadMonsterList, Character player)
        {
            int addGold = 0;

            for (int i = 0; i < deadMonsterList.Count; i++)
            {
                int monsterGold = 0;
                int levelGold = (deadMonsterList[i].Level * 50);
                //int atkGold = deadMonsterList[i].Attack * 10;
                addGold += (levelGold + monsterGold);
            }
            player.Gold += addGold;
            Console.WriteLine();
            Console.WriteLine("[획득아이템]");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{addGold}");
            Console.ResetColor();
            Console.WriteLine(" Gold");
        }

        public void DungeonItemReward(List<Monster> deadMonsterList)
        {


            //inventory = new Inventory(itemEquipped, player, program); // 임시
            //itemDb.Add(new Item(Item.BeginnerArmor())); //임시데이터
            //itemDb.Add(new Item(Item.IronArmor())); // 임시데이터

            //드랍아이템확률
            Random rand = new Random();

            //랜덤아이템추가 
            List<string> getItem = new List<string>();


            for (int i = 0; i < deadMonsterList.Count; i++)
            {
                int dropNormalItemRate = 0;
                int dropRareItemRate = 0;
                int monsterLevel = deadMonsterList[i].Level;
                dropNormalItemRate += 10 + (monsterLevel * 1);

                if (monsterLevel > 4)
                {
                    dropRareItemRate = 10;
                }
                int randItemRate = rand.Next(0, 100);
                string deadMonsterName = deadMonsterList[i].Name;

                //int rareItemIndex = itemList.Count - 1;

                if (randItemRate < dropRareItemRate)  //레어드롭아이템추가   
                {
                    int randRareItem = rand.Next(0, 2);
                    int rareItemIdx = randRareItem == 0 ? randRareItem = 4 : randRareItem = 9;

                    inventory.AllItems.Add(itemList[rareItemIdx]);
                    getItem.Add(itemList[rareItemIdx].itemPro.ItemName);
                    //Console.WriteLine(itemList[rareItemIndex].itemPro.ItemName);
                    ////테스트출력
                    //Console.WriteLine("테스트출력");
                    //Console.WriteLine($"rare:{dropRareItemRate} normal:{dropNormalItemRate}");
                    //for (int i = 0; i < inventory.AllItems.Count; i++)
                    //{
                    //    Console.WriteLine($": {inventory.AllItems[i].itemPro.ItemName}");
                    //}
                }
                else  //기본드롭아이템추가
                {
                    if (deadMonsterName == monsterName[0])
                    {
                        int randItem = rand.Next(0, expendableList.Count);
                        inventory.expendables.Add(expendableList[randItem]);
                        getItem.Add(expendableList[randItem].expendablesPro.ItemName);

                    }
                    else if (deadMonsterName == monsterName[1])
                    {
                        int randItem = rand.Next(0, 4);
                        inventory.AllItems.Add(itemList[randItem]);
                        getItem.Add(itemList[randItem].itemPro.ItemName);
                    }
                    else if (deadMonsterName == monsterName[2])
                    {
                        int randItem = rand.Next(5, 9);
                        inventory.AllItems.Add(itemList[randItem]);
                        getItem.Add(itemList[randItem].itemPro.ItemName);
                    }
                }
            }
            //아이템 출력
            PrintItemReward(getItem);

        }

        private static void PrintItemReward(List<string> getItem)
        {
            if (getItem.Count > 0)
            {
                //Console.WriteLine(getItem.Count);
                for (int i = 0; i < getItem.Count; i++) //아이템갯수출력
                {
                    int itemEA = 1;
                    for (int j = 1 + i; j < getItem.Count; j++)
                    {
                        if (getItem[i] == getItem[j])
                        {
                            itemEA++;
                            getItem.Remove(getItem[j]);
                        }
                    }
                    Console.Write($"{getItem[i]}");

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(" - ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{itemEA} ");
                    Console.ResetColor();
                }
            }
        }


        //아이템보상
        public void BattleItemReward(List<Monster> deadMonsterList)
        {
            //드랍아이템확률
            Random rand = new Random();

            int EndItemIndex = itemList.FindIndex(item => item.itemPro.ItemName == "스파르타쿠스의 분노");
            List<string> getItem = new List<string>();

            for (int i = 0; i < deadMonsterList.Count; i++)
            {
                int randItem = rand.Next(0, 3);
                int monsterLevel = deadMonsterList[i].Level;
                if (monsterLevel >= 10)
                {
                    inventory.AllItems.Add(itemList[EndItemIndex]);
                    getItem.Add(itemList[EndItemIndex].itemPro.ItemName);
                }
                else if (randItem == 0)
                {
                    int randReward = rand.Next(0, expendableList.Count);
                    inventory.expendables.Add(expendableList[randReward]);
                    getItem.Add(expendableList[randReward].expendablesPro.ItemName);
                }
                else if (randItem == 1)
                {
                    int randReward = rand.Next(0, 4);
                    inventory.AllItems.Add(itemList[randReward]);
                    getItem.Add(itemList[randReward].itemPro.ItemName);
                }
                else if (randItem == 2)
                {
                    int randReward = rand.Next(5, 9);
                    inventory.AllItems.Add(itemList[randReward]);
                    getItem.Add(itemList[randReward].itemPro.ItemName);
                }
            }
            //Console.WriteLine($"-{getItem[j]}");
            //Console.WriteLine(getItem.Count);
            //아이템 출력
            PrintItemReward(getItem);
        }
    }
}

