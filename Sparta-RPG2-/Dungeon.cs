using RPG_SJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_RPG2_
{
    class Dungeon
    {
        public string Name { get; set; }
        public int RequiredLevel { get; set; } = 1;
        public List<Stage> Stages { get; set; }
        public bool IsCleared { get; set; }

        public void Enter(Character player)
        {
            if (player.Level >= RequiredLevel)
                StartDungeon(player);
            else
                Console.WriteLine("레벨이 부족합니다.");
        }

        private void StartDungeon(Character player)
        {
            foreach (var stage in Stages)
            {
                bool result = stage.Execute(player);
                if (!result)
                {
                    Console.WriteLine("클리어 실패. 던전 종료.");
                    return;
                }
            }

            Console.WriteLine("던전 클리어!");
            IsCleared = true;
        }

        // ✅ 여기! 타락한 아레스의 탑을 미리 정의해둡니다.
        public static Dungeon AresTower => new Dungeon
        {
            Name = "타락한 아레스의 탑",
            RequiredLevel = 1,
            Stages = new List<Stage>
            {
                new Stage("1층 - 타락한 병사"),
                new Stage("2층 - 광신자 사제"),
                new Stage("3층 - 붉은 아레스의 화신")
            }
        };
    }

    public class Stage
    {
        public string Name { get; set; }

        public Stage(string name)
        {
            Name = name;
        }

        public bool Execute(Character player)
        {
            Console.WriteLine($"{Name}을(를) 시작합니다.");
            // 몬스터 전투 또는 기믹 구현
            return true; // 임시로 클리어 성공 처리
        }
    }
}
    

    


