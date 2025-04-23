using Sparta_RPG2_;
using static RPG_SJ.Program.Quest;
using System;
using System.Collections.Generic;

namespace RPG_SJ
{
    internal partial class Program
    {
        static QuestManager? questManager;
        static Character? player;
        static Inventory? inventory;
        static ItemEquipped? itemEquipped;
        static Buy? buy;
        static Shop? shop;

        static List<Item> allItems = new List<Item>();
        static List<Expendables> expendables = new List<Expendables>();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            InitGame();

            questManager = new QuestManager(player!);
            questManager.InitQuests(); // 퀘스트 생성
            Intro.Start();// 게임 시작 인트로
            ShowCreatMe(player);
            ShowStartMenu(); // 게임 시작
        }

        static void InitGame()
        {
            player = new Character();
            player.MaxHP = player.HP;

            inventory = new Inventory(player);
            itemEquipped = new ItemEquipped(player, inventory);

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
                new Expendables(Expendables.potion()),
                new Expendables(Expendables.manaPotion())
            });

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
                        ui.ShowStatus(player!);
                        Console.ReadLine();
                        break;
                    case "2":
                        battle.StartBattle(player!);
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
