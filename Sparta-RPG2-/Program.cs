using System;
using System.Collections.Generic;
using static RPG_SJ.Program.Quest;
using Sparta_RPG2_;


namespace RPG_SJ
{
    internal partial class Program
    {
        static QuestManager? questManager;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

        static Character player;
        static Inventory inventory;
        static ItemEquipped itemEquipped;
        static Buy buy;
        static Shop shop;
        static Program program;

        static List<Item> allItems = new List<Item>();
        static List<Expendables> expendables = new List<Expendables>();

        static void InitGame()
        {
            player = new Character();
            player.MaxHP = player.HP;

            itemEquipped = new ItemEquipped(player, inventory);
            inventory = new Inventory(itemEquipped, player, program);

            // 아이템과 소모품 리스트 직접 생성
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
        // 🎯 프로그램의 진입점 (필수!)
        static void Main(string[] args)
        {
            InitGame();
            Character player = new Character();
            player.MaxHP = player.HP;  // 시작 시 MaxHP 설정

            questManager = new QuestManager(player);
            questManager.InitQuests(); // ⬅ 반드시 호출해야 퀘스트가 생성됨

            ShowStartMenu(player);     // 게임 시작


            // 퀘스트 매니저 초기화
            questManager = new Quest.QuestManager(player);  // static 필드 선언 필요
            questManager.InitQuests();

            ShowStartMenu(player);  // 게임 시작

        }        

        // 🎮 게임 시작 메뉴
        static void ShowStartMenu(Character player)
        {
            GameUI ui = new GameUI();                // ✅ UI 객체 생성
            BattleSystem battle = new BattleSystem(); // ✅ 전투 시스템 객체 생성
            bool playGame = true;

            Console.Clear();
            Console.WriteLine("🌟 스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("이제 전투를 시작할 수 있습니다.\n");

            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 전투 시작");
            Console.WriteLine("3. 인벤토리");
            Console.WriteLine("4. 상점");
            Console.WriteLine("5. 📜 퀘스트 목록");
            Console.WriteLine("0. 게임종료\n");

            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
            string? input = Console.ReadLine();
            while (playGame)
            {
                switch (input)
                {
                    case "1":
                        Console.WriteLine("\n[상태 보기 화면으로 이동합니다...]\n");
                        ui.ShowStatus(player); // ✅ 객체를 통해 호출
                        Console.ReadLine();    // 0 입력 대기
                        ShowStartMenu(player); // ✅ 다시 메뉴로 돌아가기
                        break;
                    case "2":
                        Console.WriteLine("\n[전투를 시작합니다...]\n");
                        battle.StartBattle(player); // ✅ 전투 시스템 실행
                        ShowStartMenu(player);      // ✅ 전투 끝나면 다시 메뉴
                        break;
                    case "3":
                        Console.WriteLine("인벤토리로 이동합니다...");
                        inventory.InventoryScene();
                        break;
                    case "4":
                        Console.WriteLine("상점으로 이동합니다...");
                        shop.ShopScene();
                        break;
                    case "5":
                        Console.WriteLine("📜 퀘스트 목록으로 이동합니다...\n");
                        questManager.ShowQuestMenu();  // ✅ 인스턴스를 통해 호출
                        ShowStartMenu(player);         // 메뉴로 다시 돌아가기
                        break;
                    default:
                        Console.WriteLine("\n❌ 잘못된 입력입니다.\n");
                        ShowStartMenu(player); // 잘못 입력 시 재귀 호출
                        break;
                }
            }
        }
    }
}

