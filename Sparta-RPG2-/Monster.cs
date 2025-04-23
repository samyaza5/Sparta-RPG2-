namespace RPG_SJ
{

    // 👹 몬스터 클래스
    public class Monster
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int HP { get; set; }
        public bool IsDead { get; set; } // ✅ 읽기/쓰기 가능
        public int Attack { get; set; }
        public int MaxHP { get; set; }


        public Monster(string name, int level, int hp, int maxHP, int attack)

        {
            Name = name;
            Level = level;
            HP = hp;
            MaxHP = maxHP;
            Attack = attack;
            IsDead = false;
        }
    }
}

