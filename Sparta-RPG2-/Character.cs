namespace Sparta_RPG2_
{
    // 🧍 캐릭터 클래스
    public class Character
    {
        // 🎯 기본 스탯
        public int Level { get; set; } = 1;
        public string Name { get; set; } = "";
        public string Job { get; set; } = "1";
        public string JobName { get; set; } = "팔랑크스 중보병";

        public int Attack { get; set; } = 50;
        public int Defense { get; set; } = 5;
        public int HP { get; set; } = 100;
        public int MaxHP { get; set; } = 100;
        public int MP { get; set; } = 50;
        public int MaxMP { get; set; } = 50;

        public int Gold { get; set; } = 50000;
        public int Exp { get; set; } = 0;
        public int MaxExp { get; set; } = 100;

        // 🛡️ 장비 파워
        public int ArmorPower { get; set; } = 0;
        public int WeaponPower { get; set; } = 0;

        // 💾 기타 상태 기록
        public int beforeHP { get; set; } = 100;

        // 🪖 병사 전투력
        public int SoldierAttack { get; set; } = 0;
        public int SoldierDefense { get; set; } = 0;

        // 🔧 기본 생성자 (직렬화용)
        public Character() { }

        // 🎁 레벨업 공식
        public static int CalculateMaxExp(int level)
        {
            return (int)(100 * Math.Pow(1.1, level)); // 지수 증가
        }

        // 🎯 경험치 추가 및 레벨업 처리
        public void AddExp(int amount)
        {
            Exp += amount;

            while (Exp >= MaxExp)
            {
                Exp -= MaxExp;
                Level++;

                // 능력치 성장
                Attack += 2;
                Defense += 2;

                // 경험치 요구량 재계산
                MaxExp = CalculateMaxExp(Level);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n🎉 레벨업! Lv.{Level}");
                Console.ResetColor();
            }
        }
    }
}
