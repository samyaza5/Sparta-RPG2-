namespace RPG_SJ
{
    internal partial class Program
    {
        // 👹 몬스터 클래스
        public class Monster
        {
            public string Name { get; set; }
            public int Level { get; set; }
            public int HP { get; set; }
            public int Attack { get; set; }

            public Monster(string name, int level, int hp, int attack)
            {
                Name = name;
                Level = level;
                HP = hp;
                Attack = attack;
            }

            public bool IsDead => HP <= 0;
        }
    }
}

