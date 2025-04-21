namespace RPG_SJ;
internal partial class Program
{
    public class DungeonResult
    {
        public void DungeonReword(List<Monster> deadMonsters, Character player)
        {
            int beforeExp = player.Exp;
            int beforeLevel = player.Level; 
            int AddExp = 0;
            for (int i = 0; i < deadMonsters.Count; i++)
            {
                Monster monster = deadMonsters[i];
                AddExp += monster.Level * 1;


            }
            player.Exp += AddExp;

            switch (player.Level)
            {
                case 4:
                    if (player.Exp >= 200)
                    {
                        player.Level = 5;
                        player.Attack += (int)0.5f;
                        player.Defense++;
                    }
                    break;
                case 3:
                    if (player.Exp >= 100)
                    {
                        player.Level = 4;
                        player.Attack += (int)0.5f;
                        player.Defense++;
                    }
                    break;
                case 2:
                    if (player.Exp >= 45)
                    {
                        player.Level = 3;
                        player.Attack += (int)0.5f;
                        player.Defense++;
                    }
                    break;
                case 1:
                    if (player.Exp >= 10)
                    {
                        player.Level = 2;
                        player.Attack += (int)0.5f;
                        player.Defense++;
                    }
                    break;
            }
            Console.WriteLine($"{beforeLevel} -> {player.Level}");
            Console.WriteLine($"{beforeExp} -> {player.Exp}");
        }
    }
}
