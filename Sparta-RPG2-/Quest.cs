namespace RPG_SJ
{
    internal partial class Program
    {
        public enum QuestType
        {
            MonsterKill,
            EquipItem,
            LevelUp
        }

        public class Quest
        {
            public string? Title { get; set; }
            public string? Description { get; set; }
            public bool IsAccepted { get; set; }
            public bool IsCompleted { get; set; }
            public int CurrentProgress { get; set; }
            public int Goal { get; set; }
            public QuestType Type { get; set; }

            public class QuestManager
            {
                public List<Quest> AllQuests = new List<Quest>();

                public void InitQuests()
                {
                    AllQuests.Add(new Quest
                    {
                        Title = "마을을 위협하는 미니언 처치",
                        Description = "근처에 출몰하는 미니언을 5마리 처치하세요.",
                        Goal = 5,
                        CurrentProgress = 0,
                        IsAccepted = false,
                        IsCompleted = false,
                        Type = QuestType.MonsterKill
                    });

                    AllQuests.Add(new Quest
                    {
                        Title = "장비를 장착해보자",
                        Description = "인벤토리에서 장비를 장착해보세요.",
                        Goal = 1,
                        CurrentProgress = 0,
                        IsAccepted = false,
                        IsCompleted = false,
                        Type = QuestType.EquipItem
                    });

                    AllQuests.Add(new Quest
                    {
                        Title = "더욱 더 강해지기!",
                        Description = "레벨을 3까지 올려보세요.",
                        Goal = 3,
                        CurrentProgress = 1,
                        IsAccepted = false,
                        IsCompleted = false,
                        Type = QuestType.LevelUp

                    });
                }

                public void ShowQuestMenu()
                {
                    Console.Clear();
                    ShowActiveQuestSummary();
                    ShowCompletableQuests();
                    ShowAvailableQuests();
                }

                // 진행중인 퀘스트 함수
                private void ShowActiveQuestSummary()
                {
                    var active = AllQuests.Where(q => q.IsAccepted && !q.IsCompleted).ToList();
                    if (active.Count == 0) return;

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("[🕐 진행 중인 퀘스트 요약]");
                    Console.ResetColor();
                    foreach (var q in active)
                    {
                        int displayedProgress = Math.Min(q.CurrentProgress, q.Goal);
                        Console.Write("- " + q.Title + " ");
                        Console.WriteLine($"({displayedProgress}/{q.Goal})");
                    }
                    Console.WriteLine();
                }

                // 퀘스트 완료 함수
                private void ShowCompletableQuests()
                {
                    var completable = AllQuests.Where(q => q.IsAccepted && q.CurrentProgress >= q.Goal).ToList();
                    if (completable.Count == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("✔ 완료 가능한 퀘스트가 없습니다.\n");
                        Console.ResetColor();
                        return;
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("✔ 완료 가능한 퀘스트 목록");
                    Console.ResetColor();
                    foreach (var q in completable)
                    {
                        string extra = q.CurrentProgress > q.Goal ? "+" : "";
                        int displayedProgress = Math.Min(q.CurrentProgress, q.Goal);
                        Console.WriteLine($"- {q.Title} ({displayedProgress}/{q.Goal}){extra}");
                    }
                    Console.WriteLine();
                }

                // 퀘스트 리스트 함수
                private void ShowAvailableQuests()
                {
                    var available = AllQuests.Where(q => !q.IsAccepted).ToList();

                    Console.WriteLine("📜 [수락 가능한 퀘스트 목록]");
                    for (int i = 0; i < available.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {available[i].Title}");
                    }
                    Console.WriteLine("0. 나가기");
                    Console.Write("원하시는 퀘스트를 선택해주세요.\n>> ");

                    if (!int.TryParse(Console.ReadLine(), out int choice))
                    {
                        Console.WriteLine("❌ 숫자로 입력해주세요.\n");
                        return;
                    }

                    if (choice == 0)
                    {
                        Console.WriteLine("메뉴로 돌아갑니다...\n");
                        return;
                    }

                    if (choice > 0 && choice <= available.Count)
                    {
                        var selectedQuest = available[choice - 1];
                        int displayedProgress = Math.Min(selectedQuest.CurrentProgress, selectedQuest.Goal);

                        Console.Clear();
                        Console.WriteLine($"\n📘 [{selectedQuest.Title}]");
                        Console.WriteLine(selectedQuest.Description);
                        Console.WriteLine($"목표: {selectedQuest.Goal}개 / 진행: {selectedQuest.CurrentProgress}개\n");

                        Console.WriteLine("1. 수락하기");
                        Console.WriteLine("0. 나가기");
                        Console.Write(">> ");

                        string? confirm = Console.ReadLine();
                        if (confirm == "1")
                        {
                            selectedQuest.IsAccepted = true;
                            Console.WriteLine($"\n✅ '{selectedQuest.Title}' 퀘스트를 수락했습니다!\n");
                        }
                        else
                        {
                            Console.WriteLine("❎ 퀘스트 수락을 취소했습니다.\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("❌ 유효하지 않은 선택입니다.\n");
                    }
                }

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
                                Console.WriteLine($"\n🎉 '{quest.Title}' 퀘스트를 완료했습니다!");
                            }
                        }
                    }
                }

                public void ShowActiveQuests()
                {
                    var activeQuests = AllQuests.Where(q => q.IsAccepted && !q.IsCompleted).ToList();
                    if (activeQuests.Count == 0)
                    {
                        Console.WriteLine("❌ 현재 진행 중인 퀘스트가 없습니다.");
                        return;
                    }

                    Console.WriteLine("📘 [진행 중인 퀘스트]");
                    foreach (var q in activeQuests)
                    {
                        Console.WriteLine($"- {q.Title} ({q.CurrentProgress}/{q.Goal})");
                    }
                }
            }
        }
    }
}


