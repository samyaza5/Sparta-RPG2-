namespace RPG_SJ
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
        public int MaxHP { get; set; }
        public int Gold { get; set; } = 1500;
        public int Exp { get; set; }
        public int ArmorPower { get; set; } = 0;
        public int WeaponPower { get; set; } = 0;
        public int beforeHP { get; set; } = 100;
        public int MP { get; set; } = 50;
        public int MaxMP { get; set; }
    }
}
    