using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;

namespace Sparta_RPG2_
{
    public class DungeonResult
    {
        Inventory inventory;
        List<Item> itemList;
        List<Expendables> expendableList;
        string[] monsterName = { "버려진 창병", "부패한 검투사", "망각의 방랑자", "타락한 궁수", "오염된 근위병" };

        //병사DB 
        List<Soldier> soldierDb = Program.soldiers;
        List<Soldier> soldierList = Program.soldierInven.soldiers;

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


        //전투하기아이템드랍
        public void DungeonItemReward(List<Monster> deadMonsterList)
        {
            //드랍아이템확률
            Random rand = new Random();

            //랜덤아이템추가 
            //List<string> gainedItem = new List<string>();
            Dictionary<string, int> gainItem = new Dictionary<string, int>();
            Dictionary<string, int> gainSoldier = new Dictionary<string, int>();


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
                    //gainedItem.Add(itemList[rareItemIdx].itemPro.ItemName);
                    if (gainItem.ContainsKey($"{itemList[rareItemIdx].itemPro.ItemName}"))
                    {
                        gainItem[itemList[rareItemIdx].itemPro.ItemName]++;
                    }
                    else gainItem[itemList[rareItemIdx].itemPro.ItemName] = 1;


                    //병사추가
                    soldierList.Add(soldierDb[9]);
                    //gainedItem.Add(soldierDb[9].soldierPro.ItemName);

                    
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
                    if (deadMonsterList[i].Attack < 6)
                    {
                        int randItem = rand.Next(0, expendableList.Count);
                        inventory.expendables.Add(expendableList[randItem]);
                        //gainedItem.Add(expendableList[randItem].expendablesPro.ItemName);

                        if (gainItem.ContainsKey($"{expendableList[randItem].expendablesPro.ItemName}"))
                        {
                            gainItem[expendableList[randItem].expendablesPro.ItemName]++;
                        }
                        else gainItem[expendableList[randItem].expendablesPro.ItemName] = 1;

                    }
                    else if (deadMonsterList[i].Attack < 7)
                    {
                        int randItem = rand.Next(0, 4);
                        inventory.AllItems.Add(itemList[randItem]);
                        //gainedItem.Add(itemList[randItem].itemPro.ItemName);
                        if (gainItem.ContainsKey($"{itemList[randItem].itemPro.ItemName}"))
                        {
                            gainItem[itemList[randItem].itemPro.ItemName]++;
                        }
                        else gainItem[itemList[randItem].itemPro.ItemName] = 1;

                    }
                    else if (deadMonsterList[i].Attack < 8)
                    {
                        int randItem = rand.Next(0, 5);
                        //병사추가
                        soldierList.Add(soldierDb[randItem]);
                        //gainedItem.Add(soldierDb[randItem].soldierPro.ItemName);
                        if (gainSoldier.ContainsKey($"{soldierDb[randItem].soldierPro.ItemName}"))
                        {
                            gainSoldier[soldierDb[randItem].soldierPro.ItemName]++;
                        }
                        else gainSoldier[soldierDb[randItem].soldierPro.ItemName] = 1;

                    }
                    else if (deadMonsterList[i].Attack < 10)
                    {
                        int randItem = rand.Next(5, 9);
                        inventory.AllItems.Add(itemList[randItem]);
                        //gainedItem.Add(itemList[randItem].itemPro.ItemName);
                        if (gainItem.ContainsKey($"{itemList[randItem].itemPro.ItemName}"))
                        {
                            gainItem[itemList[randItem].itemPro.ItemName]++;
                        }
                        else gainItem[itemList[randItem].itemPro.ItemName] = 1;
                    }
                }
            }
            //아이템 출력
            PrintRewardItem(gainItem);
            Console.WriteLine(); 
            if (gainSoldier.Count > 0)
            {
                Console.WriteLine("[사로잡은 포로]");
                foreach (var item in gainSoldier)
                {
                    Console.Write($"{item.Key}");

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(" - ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{item.Value} ");
                    Console.ResetColor();
                }
            }
            
        }

        private static void PrintRewardItem(Dictionary<string, int> gainItem)
        {
            foreach (var item in gainItem)
            {

                Console.Write($"{item.Key}");

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(" - ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{item.Value} ");
                Console.ResetColor();
            }
        }




        //아이템 출력
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




        //보병보상
        public void ArmyReward(List<Monster> deadMonsterList)
        {
            List<Soldier> soldierList = new List<Soldier>();
            soldierList.AddRange(Program.soldiers);
            Console.WriteLine($"soldierList count: {soldierList}");
            //foreach (Soldier item in Program.soldiers) 
            //{
            //    soldierList.Add(item);
            //}

            //Program.soldierInven.soldiers.Add(Program.soldiers[0]);
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
                        expendableList.Add(expendableList[randItem]);
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
    }

    //던전ItemRward
    public class DungeonReward
    {
        List<Item> itemDb = Program.allItems;
        List<Item>? Inventory = Program.inventory.AllItems;
        List<Expendables> potionDb = Program.expendables;
        List<Expendables> PotionInven = Program.inventory.expendables;
        Character? player = Program.player;

        public void Reward(Stage stage)
        {
            LevelUp(stage);
            BattleGold(stage);
            BattleItemReward(stage);
        }




        //경험치,레벨업
            
        public void LevelUp( Stage stage)
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

            for (int i = 3; i < 1000; i++)
            {
                levelUpExp.Add(levelUpExp[i] * 2);
            }

            int needExp = levelUpExp[player.Level - 1];


            switch (stage.Floor)
            {
                case FloorType.F1:
                    addExp = 5;
                    break;
                case FloorType.F2:
                    addExp = 10;
                    break;
                case FloorType.F3:
                    addExp = 20;
                    break;
                case FloorType.F4:
                    addExp = 40;
                    break;
                case FloorType.F5:
                    addExp = 100;
                    break;
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

            PrintGainExp(player, beforeExp, beforeLevel);
        }

        private static void PrintGainExp(Character player, int beforeExp, int beforeLevel)
        {
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

        //던전골드 보상
        public void BattleGold(Stage stage)
        {

            int addGold = 0;


            switch (stage.Floor)
            {
                case FloorType.F1:
                    addGold = 100;
                    break;
                case FloorType.F2:
                    addGold = 200;
                    break;
                case FloorType.F3:
                    addGold = 300;
                    break;
                case FloorType.F4:
                    addGold = 500;
                    break;
                case FloorType.F5:
                    addGold = 1000;
                    break;
            }
            //for (int i = 0; i < deadMonsterList.Count; i++)
            //{
            //    int monsterGold = 0;
            //    string deadMonsterName = deadMonsterList[i].Name;
            //        monsterGold = 90;
            //        monsterGold = 100;
            //        monsterGold = 110;
            //    int levelGold = (deadMonsterList[i].Level * 10);
            //    //int atkGold = deadMonsterList[i].Attack * 10;
            //    addGold += (levelGold + monsterGold);
            //}
            player.Gold += addGold;
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{addGold}");
            Console.ResetColor();
            Console.WriteLine(" Gold");
        }

        //던전아이템보상
        public void BattleItemReward(Stage stage)
        {
            Console.WriteLine("[아이템 획득]:");
            Console.WriteLine();
            //드랍아이템확률
            Random rand = new Random();

            int EndItemIndex = Inventory.FindIndex(item => item.itemPro.ItemName == "스파르타쿠스의 분노");
            List<string> getItem = new List<string>();
            switch (stage.Floor)
            {
                case FloorType.F1:
                    Inventory.Add(itemDb[0]);
                    Inventory.Add(itemDb[5]);
                    getItem.Add(itemDb[0].itemPro.ItemName);
                    getItem.Add(itemDb[5].itemPro.ItemName);
                    break;
                case FloorType.F2:
                    Inventory.Add(itemDb[1]);
                    Inventory.Add(itemDb[6]);
                    getItem.Add(itemDb[1].itemPro.ItemName);
                    getItem.Add(itemDb[6].itemPro.ItemName);
                    break;
                case FloorType.F3:
                    Inventory.Add(itemDb[2]);
                    Inventory.Add(itemDb[7]);
                    getItem.Add(itemDb[2].itemPro.ItemName);
                    getItem.Add(itemDb[7].itemPro.ItemName);
                    break;
                case FloorType.F4:
                    Inventory.Add(itemDb[3]);
                    Inventory.Add(itemDb[8]);
                    getItem.Add(itemDb[3].itemPro.ItemName);
                    getItem.Add(itemDb[8].itemPro.ItemName);
                    break;
                case FloorType.F5:
                    Inventory.Add(itemDb[9]);
                    Inventory.Add(itemDb[4]);
                    getItem.Add(itemDb[9].itemPro.ItemName);
                    getItem.Add(itemDb[4].itemPro.ItemName);
                    break;
            }
            //for (int i = 0; i < dungeon.Stages.Count; i++)
            //{
            //    int randItem = rand.Next(0, 3);
            //    int monsterLevel = dungeon.Stages.Count;
            //    if (monsterLevel >= 10)
            //    {
            //        Inventory.Add(itemDb[EndItemIndex]);
            //        Inventory.Add(itemDb[EndItemIndex]);
            //        getItem.Add(itemDb[EndItemIndex].itemPro.ItemName);
            //    }
            //    else if (randItem == 0)
            //    {
            //        int randReward = rand.Next(0, PotionDb.Count);
            //        PotionInven.Add(PotionDb[randReward]);
            //        getItem.Add(PotionDb[randReward].expendablesPro.ItemName);
            //    }
            //    else if (randItem == 1)
            //    {
            //        int randReward = rand.Next(0, 4);
            //        Inventory.Add(itemDb[randReward]);
            //        getItem.Add(itemDb[randReward].itemPro.ItemName);
            //    }
            //    else if (randItem == 2)
            //    {
            //        int randReward = rand.Next(5, 9);
            //        Inventory.Add(itemDb[randReward]);
            //        getItem.Add(itemDb[randReward].itemPro.ItemName);
            //    }
            //}
            //Console.WriteLine($"-{getItem[j]}");
            //Console.WriteLine(getItem.Count);
            //아이템 출력
            PrintItemReward(getItem);
            Console.WriteLine();
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
    }
}

