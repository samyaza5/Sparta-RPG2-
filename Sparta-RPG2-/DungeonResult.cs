namespace RPG_SJ;
internal partial class Program
{
    public class DungeonResult
    {
        public void LevelUp(List<Monster> deadMonsterList, Character player)
        {
            int beforeExp = player.Exp;
            int beforeLevel = player.Level;
            int addExp = 0;
            List<int> levelUpExp = new List<int>()
            {
                10,
                45,
                100,
                200
            };

            for (int i = 0; i < deadMonsterList.Count; i++)
            {
                addExp += deadMonsterList[i].Level * 1;
            }

            int needExp = levelUpExp[player.Level - 1];
            player.Exp += addExp;

            if (player.Exp > needExp)
            {
                player.Level++;
                player.Attack++;
                player.Defense++;
            }

            Console.WriteLine($"Lv.{beforeLevel} -> Lv.{player.Level} {player.Name}");
            Console.WriteLine($"exp {beforeExp} -> {player.Exp}");
        }

        public void DungeonReward(List<Monster> deadMonsterList, Character player)
        {
            int addGold = 0;

            for (int i = 0; i < deadMonsterList.Count; i++)
            {
                int levelGold = (deadMonsterList[i].Level * 50) + 100;
                int atkGold = deadMonsterList[i].Attack * 10;
                addGold = (levelGold + atkGold);
            }
            player.Gold += addGold;

            Console.WriteLine("[획득아이템]");
            Console.WriteLine($"{addGold} Gold");
        }
    }
}
