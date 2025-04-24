namespace Sparta_RPG2_
{
    internal partial class Program
    {
        public class GameUI
        {
            // 📊 상태 보기
            public void ShowStatus(Character player)
            {
                Console.WriteLine($"\n📛 Lv. {player.Level}");
                Console.WriteLine($"🧝‍ {player.Name} ({player.JobName})");
                Console.WriteLine($"⚔️ 공격력 : {player.Attack} {(player.WeaponPower == 0 ? "" : " + " + player.WeaponPower)}");
                Console.WriteLine($"🛡️ 방어력 : {player.Defense} {(player.ArmorPower == 0 ? "" : " + " + player.ArmorPower)}");
                Console.WriteLine($"🏰 군대 : 전투력 : {player.SoldierAttack} | 결집력 : {player.SoldierDefense}");
                Console.WriteLine($"❤️ 체 력 : {player.HP} / {player.MaxHP}");
                Console.WriteLine($"💰 Gold : {player.Gold:N0} G");
                ShowExpBar(player); // 🎯 경험치 게이지 출력
                                    
                Console.WriteLine("\n0. 나가기");
                Console.Write(">> ");
            }

            public void ShowExpBar(Character player)
            {
                int barLength = 20; // 경험치바의 총 길이
                int maxExp = Math.Max(player.MaxExp, 1); // 0으로 나누는 것 방지
                double ratio = (double)player.Exp / player.MaxExp;
                int filled = (int)(ratio * barLength);

                Console.Write("📊 경험치 : [");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(new string('█', filled)); // 채워진 부분
                Console.ResetColor();
                Console.Write(new string('░', barLength - filled)); // 남은 부분
                Console.Write($"]  {(int)(ratio * 100)}% ({player.Exp} / {player.MaxExp})");
                Console.WriteLine();
            }
        }
    }
}
