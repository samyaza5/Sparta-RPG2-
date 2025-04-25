using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_RPG2_
{
    public class DungeonManager
    {
        public Dungeon currentDungeon;               // ✅ 수정
        public int currentStageIndex = 0;

        public List<Dungeon> Dungeons { get; set; } = new();  // ✅ 수정

        public DungeonManager(Dungeon dungeon)               // ✅ 생성자도 public!
        {
            currentDungeon = dungeon;
        }

        public List<Monster> GetCurrentStageMonsters()
        {
            return currentDungeon.Stages[currentStageIndex].Monsters;
        }

        public string GetCurrentStageName()
        {
            return currentDungeon.Stages[currentStageIndex].Name;
        }

        public bool MoveToNextStage()
        {
            if (currentStageIndex < currentDungeon.Stages.Count - 1)
            {
                currentStageIndex++;
                return true;
            }
            return false; // 마지막 스테이지 도달
        }

        public bool IsBossStage()
        {
            return currentDungeon.Stages[currentStageIndex].Type == Monstertype.B;
        }

        public List<string> GetClearedDungeons()
        {
            return Dungeons
                .Where(d => d.IsCleared)
                .Select(d => d.Name)
                .ToList();
        }
    }
}
