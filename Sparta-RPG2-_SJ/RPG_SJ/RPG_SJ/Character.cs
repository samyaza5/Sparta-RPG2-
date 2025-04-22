namespace RPG_SJ
{
    internal partial class Program
    {
        // 🧍 캐릭터 클래스
        public class Character
        {
            public int Level { get; set; } = 1;
            public string Name { get; set; } = "";
            public string Job { get; set; } = "전사";
            public int Attack { get; set; } = 50;
            public int Defense { get; set; } = 5;
            public int HP { get; set; } = 100;
            public int beforeHP { get; set; } = 100;
            public int MP { get; set; } = 50;
            public int MaxHP { get; set; }
            public int MaxMP { get; set; }
            public int Gold { get; set; } = 1500;
        }
    }
}
