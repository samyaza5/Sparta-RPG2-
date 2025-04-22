<<<<<<< HEAD
﻿
using System;
using System.Collections.Generic;

=======

using System;
using System.Collections.Generic;
using static RPG_SJ.Program.Quest;
                                                                           
>>>>>>> 486bdab67dfcd08bf14e76b86e77ff38bab320e4
namespace RPG_SJ
{
    internal partial class Program
    {
<<<<<<< HEAD
=======
        static Quest.QuestManager questManager = new Quest.QuestManager();

>>>>>>> 486bdab67dfcd08bf14e76b86e77ff38bab320e4
        // 🎯 프로그램의 진입점 (필수!)
        static void Main(string[] args)
        {
            Character player = new Character();
            player.MaxHP = player.HP;  // 시작 시 MaxHP 설정
<<<<<<< HEAD
            ShowStartMenu(player);     // 게임 시작
=======

            // 퀘스트 매니저 초기화
            questManager = new Quest.QuestManager();  // static 필드 선언 필요
            questManager.InitQuests();

            ShowStartMenu(player);  // 게임 시작
>>>>>>> 486bdab67dfcd08bf14e76b86e77ff38bab320e4
        }        

        // 🎮 게임 시작 메뉴
        static void ShowStartMenu(Character player)
        {
            GameUI ui = new GameUI();                // ✅ UI 객체 생성
            BattleSystem battle = new BattleSystem(); // ✅ 전투 시스템 객체 생성

            Console.WriteLine("🌟 스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("이제 전투를 시작할 수 있습니다.\n");

            Console.WriteLine("1. 상태 보기");
<<<<<<< HEAD
            Console.WriteLine("2. 전투 시작\n");

=======
            Console.WriteLine("2. 전투 시작");
            Console.WriteLine("3. 📜 퀘스트 목록\n");
>>>>>>> 486bdab67dfcd08bf14e76b86e77ff38bab320e4
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
            string? input = Console.ReadLine();

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
<<<<<<< HEAD
=======
                case "3":
                    Console.WriteLine("📜 퀘스트 목록으로 이동합니다...\n");
                    questManager.ShowQuestList();  // ✅ 인스턴스를 통해 호출
                    ShowStartMenu(player);         // 메뉴로 다시 돌아가기
                    break;
>>>>>>> 486bdab67dfcd08bf14e76b86e77ff38bab320e4

                default:
                    Console.WriteLine("\n❌ 잘못된 입력입니다.\n");
                    ShowStartMenu(player); // 잘못 입력 시 재귀 호출
<<<<<<< HEAD
                    break;
=======
                    break;                
>>>>>>> 486bdab67dfcd08bf14e76b86e77ff38bab320e4
            }
        }
    }
}

