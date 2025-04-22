namespace RPG_SJ
{
    internal partial class Program
    {
        public class GameUI
        {
            // 📊 상태 보기
            public void ShowStatus(Character player)
            {
                Console.WriteLine($"\nLv. {player.Level}");
                Console.WriteLine($"{player.Name} ({player.Job})");
                Console.WriteLine($"공격력 : {player.Attack} {(player.WeaponPower == 0 ? "" : " + " + player.WeaponPower)}");
                Console.WriteLine($"방어력 : {player.Defense} {(player.ArmorPower == 0 ? "" : " + " + player.ArmorPower)}");
                Console.WriteLine($"체 력 : {player.HP} / {player.MaxHP}");
                Console.WriteLine($"Gold : {player.Gold:N0} G");

                Console.WriteLine("\n0. 나가기");
                Console.Write(">> ");
            }
        }
    }
}

