using Sparta_RPG2_;
using static RPG_SJ.Program.Quest;
using System;
using System.Collections.Generic;

namespace RPG_SJ
{
    internal partial class Program
    {
        static QuestManager? questManager;

        static Character player;
        static Inventory inventory;
        static ItemEquipped itemEquipped;
        static Buy buy;
        static Shop shop;

        static List<Item> allItems = new List<Item>();
        static List<Expendables> expendables = new List<Expendables>();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            InitGame();

            questManager = new QuestManager(player);
            questManager.InitQuests(); // 퀘스트 생성

            ShowStartMenu(); // 게임 시작
        }

        static void InitGame()
        {
            player = new Character();
            player.MaxHP = player.HP;

            inventory = new Inventory(); // ✅ 작동 가능
            itemEquipped = new ItemEquipped(player, inventory); // ✅ 이제 가능

            // 아이템과 소모품 리스트 생성
            allItems.Add(new Item(Item.BeginnerArmor()));
            allItems.Add(new Item(Item.IronArmor()));
            allItems.Add(new Item(Item.SpartaArmor()));
            allItems.Add(new Item(Item.Sparta300Armor()));
            allItems.Add(new Item(Item.ArmorOfSpartacus()));
            allItems.Add(new Item(Item.OldSword()));
            allItems.Add(new Item(Item.BronzeAx()));
            allItems.Add(new Item(Item.SpartaSphere()));
            allItems.Add(new Item(Item.Sparta300Sphere()));
            allItems.Add(new Item(Item.SphereOfSpartacus()));

            expendables.Add(new Expendables(Expendables.potion()));
            expendables.Add(new Expendables(Expendables.manaPotion()));

            buy = new Buy(allItems, expendables, player, inventory, itemEquipped);
            shop = new Shop(player, allItems, expendables, buy);
            buy.SetShop(shop);
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
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 전투 시작");
                Console.WriteLine("3. 인벤토리");
                Console.WriteLine("4. 상점");
                Console.WriteLine("5. 📜 퀘스트 목록");
                Console.WriteLine("0. 게임 종료\n");

                Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ui.ShowStatus(player);
                        Console.ReadLine();
                        break;
                    case "2":
                        battle.StartBattle(player);
                        break;
                    case "3":
                        inventory.InventoryScene();
                        break;
                    case "4":
                        shop.ShopScene();
                        break;
                    case "5":
                        questManager?.ShowQuestMenu();
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
