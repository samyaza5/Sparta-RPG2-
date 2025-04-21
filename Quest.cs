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

