
using System;
using System.Collections.Generic;
using Sparta_RPG2_;

namespace RPG_SJ
{
    internal partial class Program
    {
        private static ItemEquipped itemEquipped;
        private static Program program;

        private static Buy buy { get; set; }

        // 🎯 프로그램의 진입점 (필수!)
        static void Main(string[] args)
        {
            Character player = new Character();
            player.MaxHP = player.HP;  // 시작 시 MaxHP 설정
            ShowStartMenu(player);     // 게임 시작
        }        

        // 🎮 게임 시작 메뉴
        static void ShowStartMenu(Character player)
        {
           
            Character character = new Character();
            GameUI ui = new GameUI();                // ✅ UI 객체 생성
            BattleSystem battle = new BattleSystem(); // ✅ 전투 시스템 객체 생성
            Inventory inventory = new Inventory(itemEquipped, player, program);
            Shop shop = new Shop(character, buy);
            bool playGame = true;

            Console.Clear();
            Console.WriteLine("🌟 스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("이제 전투를 시작할 수 있습니다.\n");

            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 전투 시작");
            Console.WriteLine("3. 인벤토리");
            Console.WriteLine("4. 상점");
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
                        case "0":
                            return;

                    default:
                        Console.WriteLine("\n❌ 잘못된 입력입니다.\n");
                        ShowStartMenu(player); // 잘못 입력 시 재귀 호출
                        break;
                }
            }
        }
    }
}

