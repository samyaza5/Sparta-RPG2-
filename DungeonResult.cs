namespace RPG_SJ;
internal partial class Program
{
    public class DungeonResult
    {
        public void LevelUp(List<Monster> deadMonsters, Character player)
        {
            int beforeExp = player.Exp;
            int beforeLevel = player.Level;
            int AddExp = 0;
            List<int> levelUpExp = new List<int>()
            {
                10,
                45,
                100,
                200
            };


            for (int i = 0; i < deadMonsters.Count; i++)
            {
                Monster monster = deadMonsters[i];
                AddExp += monster.Level * 1;
            }


            int needExp = levelUpExp[player.Level - 1];
            player.Exp += AddExp;

            if (player.Exp > needExp)
            {
                player.Level++;
                player.Attack++;
                player.Defense++;
            }

            Console.WriteLine($"Lv.{beforeLevel} -> Lv.{player.Level} {player.Name}");
            Console.WriteLine($"exp {beforeExp} -> {player.Exp}");
        }


        public void DungeonReward(List<Monster> monsters)
        {

        }
    }
}
