using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_RPG2_
{

    /// <summary>
    /// 전체 퀘스트 데이터 및 로직을 통합 관리하는 클래스입니다.
    /// </summary>
    public class QuestManager
    {
        public List<Quest> AllQuests = new List<Quest>();
        public Character player { get; private set; }

        /// <summary>
        /// 퀘스트 매니저를 초기화합니다.
        /// </summary>
        /// <param name="player">퀘스트 대상이 되는 플레이어</param>
        public QuestManager(Character player)
        {
            this.player = player;
        }

        /// <summary>
        /// 게임 시작 시 퀘스트 목록을 초기 등록합니다.
        /// </summary>
        public void InitQuests()
        {
            AllQuests.Add(new Quest
            {
                Title = "스파르타의 저주받은 전사 척결",
                Description = "과거의 영광을 잊은 저주받은 전사를 쓰러뜨려, 스파르타의 순혈을 지켜내십시오.",
                Goal = 1,
                Type = QuestType.MonsterKill,
                RewardEXP = 5000,
                RewardGold = 99999
            });

            AllQuests.Add(new Quest
            {
                Title = "전사의 맹세, 첫 무장을 갖춰라",
                Description = "전사는 빈 손으로 싸우지 않는다. 인벤토리에서 무장을 갖추고 전장에 설 준비를 하십시오.",
                Goal = 1,
                Type = QuestType.EquipItem,
                RewardEXP = 5000,
                RewardGold = 99999
            });

            AllQuests.Add(new Quest
            {
                Title = "강함을 증명하라!",
                Description = "스파르타의 피는 쉽게 식지 않는다. 레벨을 3까지 올려 전사로서의 자격을 증명하십시오.",
                Goal = 3,
                CurrentProgress = 1,
                Type = QuestType.LevelUp,
                RewardEXP = 5000,
                RewardGold = 999999
            });
        }

        /// <summary>
        /// 유저가 수락/진행/완료 가능한 퀘스트를 확인할 수 있는 메인 메뉴를 출력합니다.
        /// </summary>
        public void ShowQuestMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("[의뢰 목록]");
                Console.WriteLine("1. 📜 수락 가능한 의뢰 보기");
                Console.WriteLine("2. 🕐 진행 중인 의뢰 보기");
                Console.WriteLine("3. ✔ 완료 가능한 의뢰 완료하기");
                Console.WriteLine("0. 나가기");
                Console.Write("\n>> ");

                string? input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        ShowAvailableQuests();
                        break;
                    case "2":
                        ShowActiveQuests();
                        Console.WriteLine("\n엔터를 누르면 퀘스트 메뉴로 돌아갑니다.");
                        Console.ReadLine();
                        break;
                    case "3":
                        ShowCompletableQuests();
                        Console.WriteLine("\n엔터를 누르면 퀘스트 메뉴로 돌아갑니다.");
                        Console.ReadLine();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("❌ 잘못된 입력입니다.");
                        break;
                }
            }
        }

        /// <summary>
        /// 수락하지 않은 퀘스트 목록을 출력하고, 유저가 퀘스트를 수락할 수 있게 합니다.
        /// </summary>
        public void ShowAvailableQuests()
        {
            var available = AllQuests.Where(q => !q.IsAccepted).ToList();
            if (available.Count == 0)
            {
                Console.WriteLine("❌ 수락 가능한 의뢰가 없습니다.");
                return;
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("📜 [수락 가능한 의뢰 목록]");
            Console.ResetColor();
            for (int i = 0; i < available.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {available[i].Title}");
            }
            Console.WriteLine("0. 나가기");
            Console.Write("원하시는 의뢰의 번호를 선택해주세요.\n>> ");

            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= available.Count)
            {
                var selected = available[choice - 1];
                Console.Clear();
                Console.WriteLine($"\n📘 {selected.Title}\n{selected.Description}");
                Console.WriteLine("1. 수락하기\n0. 취소");
                Console.Write(">> ");
                if (Console.ReadLine() == "1")
                {
                    selected.IsAccepted = true;
                    Console.WriteLine($"\n✅ '{selected.Title}' 의뢰를 수락했습니다!\n");
                }
            }
        }

        /// <summary>
        /// 현재 진행 중인 퀘스트들을 간략히 출력합니다.
        /// </summary>
        public void ShowActiveQuests()
        {
            var active = AllQuests.Where(q => q.IsAccepted && !q.IsCompleted && q.CurrentProgress < q.Goal).ToList();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[🕐 진행 중인 의뢰 요약]");
            Console.ResetColor();

            if (active.Count == 0)
                Console.WriteLine("- 없음");
            else
            {
                foreach (var q in active)
                {
                    int display = Math.Min(q.CurrentProgress, q.Goal);
                    Console.WriteLine($"- {q.Title} ({display}/{q.Goal})");
                }
            }
        }

        /// <summary>
        /// 완료 가능한 퀘스트와 이미 완료된 퀘스트를 출력하고, 보상을 수령합니다.
        /// </summary>
        public void ShowCompletableQuests()
        {
            var completable = AllQuests.Where(q => q.IsAccepted && q.CurrentProgress >= q.Goal && !q.IsCompleted).ToList();
            var completed = AllQuests.Where(q => q.IsCompleted).ToList();

            if (completable.Count == 0 && completed.Count == 0)
            {
                Console.WriteLine("✔ 완료 가능한 의뢰가 없습니다.");
                return;
            }

            if (completed.Count > 0)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n🎯 [완료한 의뢰 목록]");
                Console.ResetColor();

                for (int i = 0; i < completed.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {completed[i].Title}");
                }

                Console.WriteLine("0. 나가기");
                Console.Write("자세히 보고 싶은 의뢰의 번호를 선택해주세요.\n>> ");

                if (int.TryParse(Console.ReadLine(), out int detailChoice) && detailChoice > 0 && detailChoice <= completed.Count)
                {
                    var selected = completed[detailChoice - 1];
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"📘 [{selected.Title}]");
                    Console.ResetColor();
                    Console.WriteLine($"{selected.Description}");
                    Console.WriteLine($"목표: {selected.Goal} / [완료]");
                    Console.WriteLine($"보상: {selected.RewardEXP} EXP, {selected.RewardGold} G");
                    Console.WriteLine("\n엔터를 누르면 보상이 수령됩니다.");
                    Console.ReadLine();

                    GiveQuestReward(selected);
                }
                else if (detailChoice != 0)
                {
                    Console.WriteLine("❌ 잘못된 입력입니다.");
                }
            }
        }

        // <summary>
        /// 지정된 퀘스트 타입의 퀘스트 진행도를 업데이트합니다.
        /// </summary>
        /// <param name="type">퀘스트 타입 (몬스터 처치, 장비 착용 등)</param>
        /// <param name="amount">증가할 진행도 수치 (기본값 1)</param>
        public void OngoingQuests(QuestType type, int amount = 1)
        {
            foreach (var quest in AllQuests.Where(q => q.IsAccepted && !q.IsCompleted))
            {
                if (quest.Type == type)
                {
                    quest.CurrentProgress += amount;
                    if (quest.CurrentProgress >= quest.Goal)
                    {
                        quest.IsCompleted = true;
                        Console.WriteLine($"\n🎉 '{quest.Title}' 의뢰를 완료했습니다!");
                    }
                }
            }
        }

        /// <summary>
        /// 퀘스트 보상을 지급하고 플레이어의 경험치와 골드를 증가시킵니다.
        /// </summary>
        /// <param name="quest">보상을 받을 퀘스트 객체</param>
        public void GiveQuestReward(Quest quest)
        {
            if (player == null)
            {
                Console.WriteLine("⚠️ 플레이어 정보가 없습니다. 보상을 지급할 수 없습니다.");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n🎁 의뢰 보상 수령: {quest.RewardEXP}EXP, {quest.RewardGold}G");
            Console.ResetColor();

            player.AddExp(quest.RewardEXP);
            player.Gold += quest.RewardGold;

            GameSaveManager.AutoSave(
                player,
                Program.inventory!,
                Program.questManager!,
                Program.itemEquipped!,
                Program.dungeonManager!,
                Program.soldierInven
);
        }

        public List<string> GetCompletedQuestTitles()
        {
            return AllQuests
                .Where(q => q.IsCompleted)
                .Select(q => q.Title ?? "제목 없음")
                .ToList();
        }
    }
}
    
