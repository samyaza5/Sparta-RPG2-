namespace RPG_SJ
{
    internal partial class Program
    {
        public partial class Quest
        {
            public string? Title { get; set; }
            public string? Description { get; set; }
            public bool IsAccepted { get; set; }
            public bool IsCompleted { get; set; }
            public int CurrentProgress { get; set; }
            public int Goal { get; set; }

            public class QuestManager
            {
                public List<Quest> AllQuests = new List<Quest>();

                public List<Quest> GetAvailableQuests()
                {
                    return AllQuests.Where(q => !q.IsAccepted).ToList();
                }

                public void InitQuests()
                {
                    AllQuests.Add(new Quest
                    {
                        Title = "마을을 위협하는 미니언 처치",
                        Description = "근처에 출몰하는 미니언을 5마리 처치하세요.",
                        Goal = 5,
                        CurrentProgress = 0,
                        IsAccepted = false,
                        IsCompleted = false
                    });

                    AllQuests.Add(new Quest
                    {
                        Title = "장비를 장착해보자",
                        Description = "인벤토리에서 장비를 장착해보세요.",
                        Goal = 1,
                        CurrentProgress = 0,
                        IsAccepted = false,
                        IsCompleted = false
                    });

                    AllQuests.Add(new Quest
                    {
                        Title = "더욱 더 강해지기!",
                        Description = "레벨을 3까지 올려보세요.",
                        Goal = 3,
                        CurrentProgress = 1,  // 예시: 현재 레벨이 1이라면
                        IsAccepted = false,
                        IsCompleted = false
                    });
                }

                public void ShowQuestList()
                {
                    var available = GetAvailableQuests();
                    Console.WriteLine("📜 [퀘스트 목록]");

                    for (int i = 0; i < available.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {available[i].Title}");
                    }

                    Console.WriteLine("0. 나가기");
                    Console.Write("원하시는 퀘스트를 선택해주세요.\n>> ");

                    if (int.TryParse(Console.ReadLine(), out int choice))
                    {
                        if (choice == 0)
                        {
                            Console.WriteLine("메뉴로 돌아갑니다...\n");
                            return;
                        }

                        if (choice > 0 && choice <= available.Count)
                        {
                            Quest selectedQuest = available[choice - 1];
                            Console.WriteLine($"\n📘 [{selectedQuest.Title}]");
                            Console.WriteLine($"{selectedQuest.Description}");
                            Console.WriteLine($"목표: {selectedQuest.Goal}개 / 진행: {selectedQuest.CurrentProgress}개");

                            Console.WriteLine("\n1. 수락하기");
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
                    else
                    {
                        Console.WriteLine("❌ 숫자로 입력해주세요.\n");
                    }
                }

                public void AcceptQuest(int index)
                {
                    var available = GetAvailableQuests();
                    if (index >= 0 && index < available.Count)  // ⬅ 괄호 X
                    {
                        available[index].IsAccepted = true;
                        Console.WriteLine($"\n'{available[index].Title}' 퀘스트를 수락했습니다!");
                    }
                    else
                    {
                        Console.WriteLine("❌ 유효하지 않은 번호입니다.");
                    }
                }
            }
        }
    }
}

