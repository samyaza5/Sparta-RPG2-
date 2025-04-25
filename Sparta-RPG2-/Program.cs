using Sparta_RPG2_;
using System;
using System.Collections.Generic;
using static Sparta_RPG2_.Quest;

namespace Sparta_RPG2_
{
    internal partial class Program
    {
        public static QuestManager questManager;
        public static Character? player;
        public static Inventory? inventory;
        public static ItemEquipped? itemEquipped;
        public static SoldierEquipped? soldierEquipped;
        public static SoldierInven? soldierInven;
        public static Buy? buy;
        public static BuySoldier? buySoldier;
        public static Shop? shop;
        public static Pub? pub;
        public static UseExpendables? useExpendables;
        public static List<Item> allItems = new List<Item>();
        public static List<Expendables> expendables = new List<Expendables>();
        public static List<Soldier> soldiers = new List<Soldier>();
        public static BattleExpendables battleExpendables;
        public static Recovery? recovery;
        public static DungeonManager dungeonManager;

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
            recovery = new Recovery(player!, inventory!);
            dungeonManager = new DungeonManager(Dungeon.AresTower);

            var loadedData = GameSaveManager.LoadGame();
            GameSaveManager.ApplySaveData(loadedData, player, inventory, questManager, dungeonManager, soldierInven);


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
                Expendables.manaPotion(),
                Expendables.potionPlus(),
                Expendables.manaPotionPlus(),
                Expendables.potionSuper(),
                Expendables.manaPotionPlus(),
                Expendables.attactPotion(),
                Expendables.defendPotion(),
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

        public static void ShowStartMenu()
        {
            GameUI ui = new();
            BattleSystem battle = new();
            BattleContext context = new(player!, battleExpendables, questManager!, inventory!, allItems, expendables);
            bool playGame = true;

            while (playGame)
            {
                Console.Clear();
                ShowMainMenu();

                Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
                string? input = Console.ReadLine()?.Trim();

                playGame = HandleMenuInput(input, ui, battle, context);
            }

            Console.WriteLine("\n게임을 종료합니다.");
        }

        public static void ShowMainMenu()
        {
            Console.WriteLine("🌟 스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("1. 💪 상태 보기");
            Console.WriteLine("2. ⚔️ 전투 시작");
            Console.WriteLine("3. 🏺 인벤토리");
            Console.WriteLine("4. 💰 상점");
            Console.WriteLine("5. 📜 의뢰 목록");
            Console.WriteLine("6. 🏰 [던전] ⚔️ 타락한 아레스의 탑 ⚔️");
            Console.WriteLine("7. 🍺 선술집");
            Console.WriteLine("8. 🛡  병영");
            Console.WriteLine("9. ⚕️ 치유소");
            Console.WriteLine("0. ❌ 게임 종료");
        }

        private static bool HandleMenuInput(string? input, GameUI ui, BattleSystem battle, BattleContext context)
        {
            switch (input)
            {
                case "1":
                    ui.ShowStatus(player!);
                    Console.ReadLine();
                    break;
                case "2":
                    battle.StartBattle(context);
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
                    Console.WriteLine("⚔ [던전] 페르시아의 성 - 저주받은 오아시스");
                    Dungeon.AresTower.Enter(player!, inventory!);
                    Console.ReadLine();
                    break;
                case "7":
                    pub?.ShopScene();
                    break;
                case "8":
                    soldierInven?.InventoryScene();
                    break;
                case "9":
                    recovery?.Recoverycene();
                    break;
                case "0":
                    GameSaveManager.AutoSave(
                        player!,
                        inventory!,
                        questManager!,
                        itemEquipped!,
                        dungeonManager!,
                        soldierInven!
                    );
                    Console.WriteLine("💾 게임 상태가 저장되었습니다.");
                    return false; 
                default:
                    Console.WriteLine("❌ 잘못된 입력입니다.");
                    Console.ReadLine();
                    break;
            }

            return true; // 계속 진행
        }
    }
}
