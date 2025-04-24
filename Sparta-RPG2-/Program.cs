using Sparta_RPG2_;
using System;
using System.Collections.Generic;
using static Sparta_RPG2_.Quest;

namespace Sparta_RPG2_
{
    internal partial class Program
    {
        static QuestManager questManager;
        static Character? player;
        static Inventory? inventory;
        static ItemEquipped? itemEquipped;
        static SoldierEquipped? soldierEquipped;
        static SoldierInven? soldierInven;
        static Buy? buy;
        static BuySoldier? buySoldier;
        static Shop? shop;
        static Pub? pub;
        static UseExpendables? useExpendables;
        static List<Item> allItems = new List<Item>();
        static List<Expendables> expendables = new List<Expendables>();
        static List<Soldier> soldiers = new List<Soldier>();
        static BattleExpendables battleExpendables;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            InitGame();

            
            questManager.InitQuests(); // 퀘스트 생성
            Intro.Start();// 게임 시작 인트로
            ShowCreatMe(player);
            ShowStartMenu(); // 게임 시작
        }
       
        static void InitGame()
        {
            player = new Character();
            player.MaxHP = player.HP;
            inventory = new Inventory(player, questManager); // ✅ 수정된 생성자 사용
            useExpendables = new UseExpendables(player, inventory);
            questManager = new QuestManager(player);
            itemEquipped = new ItemEquipped(player, inventory, useExpendables, questManager);
            inventory.SetItemEquipped(itemEquipped);
            battleExpendables = new BattleExpendables(player, inventory);
            soldierInven = new SoldierInven(player);
 


            // 아이템 초기화
            allItems.AddRange(new[]
            {
                new Item(Item.BeginnerArmor()),
                new Item(Item.IronArmor()),
                new Item(Item.SpartaArmor()),
                new Item(Item.Sparta300Armor()),
                new Item(Item.ArmorOfSpartacus()),
                new Item(Item.OldSword()),
                new Item(Item.BronzeAx()),
                new Item(Item.SpartaSphere()),
                new Item(Item.Sparta300Sphere()),
                new Item(Item.SphereOfSpartacus())
            });

            expendables.AddRange(new[]
            {
                Expendables.potion(),
                Expendables.manaPotion()
            });

            soldiers.AddRange(new[]
           {
                new Soldier(Soldier.Recruit()),
                new Soldier(Soldier.TrainedSoldier()),
                new Soldier(Soldier.EliteSoldier()),
                new Soldier(Soldier.ShieldNovice()),
                new Soldier(Soldier.ShieldWarrior()),
                new Soldier(Soldier.ShieldGuardian()),
                new Soldier(Soldier.SpartanWarrior()),
                new Soldier(Soldier.AresDisciple()),
                new Soldier(Soldier.AresProphet()),
                new Soldier(Soldier.AresApostle())
            });

            buy = new Buy(allItems, expendables, player, inventory, itemEquipped);
            shop = new Shop(player, allItems, expendables, buy);
            buy.SetShop(shop);

            buySoldier = new BuySoldier(soldiers,player, soldierInven, soldierEquipped);
            pub = new Pub(player, soldiers, buySoldier);
            buySoldier.SetShop(pub);
        }

        static void ShowStartMenu()
        {
            GameUI ui = new GameUI();
            BattleSystem battle = new BattleSystem();
            bool playGame = true;
            
            while (playGame)
            {
                Console.Clear();
                Console.WriteLine("🌟 스파르타 던전에 오신 여러분 환영합니다.");
                Console.WriteLine("1. 💪 상태 보기");
                Console.WriteLine("2. ⚔️ 전투 시작");
                Console.WriteLine("3. 🏺 인벤토리");
                Console.WriteLine("4. 💰 상점");
                Console.WriteLine("5. 📜 의뢰 목록");
                Console.WriteLine("6. 🏰 [던전] ⚔️ 타락한 아레스의 탑 ⚔️");
                Console.WriteLine("7. 🍺 선술집");
                Console.WriteLine("8. 🛡  병영");
                Console.WriteLine("0. ❌ 게임 종료\n");

                Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ui.ShowStatus(player!);
                        Console.ReadLine();
                        break;
                    case "2":
                        battle.StartBattle(player!, battleExpendables, questManager!, inventory!, allItems, expendables);
                        break;
                    case "3":
                        inventory!.InventoryScene();
                        break;
                    case "4":
                        shop!.ShopScene();
                        break;
                    case "5":
                        questManager?.ShowQuestMenu();
                        break;   
                    case "6":
                        Console.WriteLine("⚔ [던전] 타락한 아레스의 탑에 진입합니다...");
                        Dungeon.AresTower.Enter(player, inventory,allItems,expendables);
                        Console.ReadLine();
                        break;
                    case "7":
                        pub?.ShopScene();
                        break;   
                    case "8":
                        soldierInven?.InventoryScene();
                        break;
                    case "0":
                        playGame = false;
                        break;
                    default:
                        Console.WriteLine("❌ 잘못된 입력입니다.");
                        Console.ReadLine();
                        break;
                }
            }

            Console.WriteLine("게임을 종료합니다.");
        }
    }
}
