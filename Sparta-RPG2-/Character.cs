namespace Sparta_RPG2_
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
        public int Gold { get; set; } = 50000;
        public int Exp { get; set; }
        public int MaxExp { get; set; } = 100;
        public int ArmorPower { get; set; } = 0;
        public int WeaponPower { get; set; } = 0;
        public int beforeHP { get; set; } = 100;
        public int MP { get; set; } = 50;
        public int MaxMP { get; set; }

        public int SoldierAttack { get; set; } = 0;
        public int SoldierDefense { get; set; } = 0;

        public void AddExp(int amount)
        {
            Exp += amount;

            while (Exp >= MaxExp)
            {
                Exp -= MaxExp;
                Level++;
                Attack += 2;   // 예시 능력치 상승
                Defense += 2;

                // MaxExp 증가 (예: 1.1배씩 증가)
                MaxExp = (int)(MaxExp * 1.1);

                Console.WriteLine($"\n🎉 레벨업! Lv.{Level}");
            }
        }
    }
}
    