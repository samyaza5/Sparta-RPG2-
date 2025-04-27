using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_RPG2_
{
    public class Soldier
    {
        public SoldierPro soldierPro;
        public Soldier(SoldierPro soldierPro)
        {
            this.soldierPro = soldierPro;
        }
        public static SoldierPro Recruit()
        {
            return new SoldierPro("하급병사","약하고 재능없는 병사" , 4 , 2, 2000);
        }
        public static SoldierPro TrainedSoldier()
        {
            return new SoldierPro("중급병사", "훈련으로 강해진 병사", 6, 4, 4000);
        }
        public static SoldierPro EliteSoldier()
        {
            return new SoldierPro("상급병사", "재능을 꽃피운 병사" , 10 , 6 , 8000);
        }
        public static SoldierPro ShieldNovice()
        {
            return new SoldierPro("하급방패병","맷집약한 방패병" , 1 , 5, 2000);
        }
        public static SoldierPro ShieldWarrior()
        {
            return new SoldierPro("중급방패병","어디서 좀 맞아본 방패병" , 1 , 9 , 4000);
        }
        public static SoldierPro ShieldGuardian()
        {
            return new SoldierPro("상급방패병","고통을 모르는 방패병" , 1 , 15 , 8000);
        }
        public static SoldierPro SpartanWarrior()
        {
            return new SoldierPro("스파르타 전사","스파르타의 의지를 품은 전사" , 12 , 12 , 15000);
        }
        public static SoldierPro AresDisciple()
        {
            return new SoldierPro("아레스의 사자","전쟁의 신에게 길러진 전투병기" , 25 , 12 , 30000);
        }
        public static SoldierPro AresProphet()
        {
            return new SoldierPro("아레스의 예언가","신성한 예언서를 지키는 자" , 12 , 25 , 30000);
        }
        public static SoldierPro AresApostle()
        {
            return new SoldierPro("아레스의 사도", "전쟁의 신이 몸에 깃든 전사" , 50 , 50 , 100000);
        }


        public override string ToString()
        {
            return soldierPro.ToString();
        }
    }
}
