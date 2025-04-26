namespace Sparta_RPG2_
{

    /// <summary>
    /// 퀘스트의 분류를 정의하는 열거형입니다.
    /// </summary>
    public enum QuestType
    {
        MonsterKill,
        EquipItem,
        LevelUp
    }

    /// <summary>
    /// 퀘스트 클래스 - 퀘스트 하나의 모든 속성과 상태를 표현
    /// </summary>
    public class Quest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsCompleted { get; set; }
        public int CurrentProgress { get; set; }
        public int Goal { get; set; }
        public QuestType Type { get; set; }
        public int RewardEXP { get; set; }
        public int RewardGold { get; set; }
        public Character? player { get; private set; }
        public bool IsRewarded { get; set; } = false;

    }
}
    
