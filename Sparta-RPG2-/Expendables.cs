using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_RPG2_
{
    public class Expendables
    {
        public ExpendablesPro expendablesPro { get; set; }
        public bool IsEquipped { get; set; }  // 장착 여부 개별 관리

        public Expendables(ExpendablesPro expendablesPro)
        {
            this.expendablesPro = new ExpendablesPro(
            expendablesPro.ItemName,
            expendablesPro.ItemStat,
            expendablesPro.ItemInfo,
            expendablesPro.ItemValue,
            expendablesPro.ItemType); ;
        }
        public static Expendables potion()
        {
            return new Expendables(new ExpendablesPro("회복물약", 10 , "체력을 회복시켜주는 물약입니다.", 1000, "Heal"));
        }
        public static Expendables potionPlus()
        {
            return new Expendables(new ExpendablesPro("상급회복물약", 30 , "품질이 향상된 회복물약입니다.", 4000, "Heal"));
        
        }public static Expendables potionSuper()
        {
            return new Expendables(new ExpendablesPro("생명수", 50 , "놀라운 힘을 지닌 비약입니다.", 8000, "Heal"));
        }
        public static Expendables manaPotion()
        {
            return new Expendables(new ExpendablesPro("마나물약", 10 , "마나를 회복시켜주는 물약입니다.", 1000 ,"Mana"));
        }
        public static Expendables manaPotionPlus()
        {
            return new Expendables(new ExpendablesPro("상급마나물약", 30 , "품질이 향상된 마나물약입니다.", 4000 ,"Mana"));
        }
        public static Expendables manaPotionSuper()
        {
            return new Expendables(new ExpendablesPro("액화마나", 50 ,"마나 그 자체가 담겨져있는 비약입니다.", 8000 ,"Mana"));
        }
        public static Expendables attactPotion()
        {
            return new Expendables(new ExpendablesPro("광폭화물약", 30 ,"잠시 육체의 한계를 부수는 물약입니다.", 10000 ,"Attack"));
        } 
        public static Expendables defendPotion()
        {
            return new Expendables(new ExpendablesPro("경화물약", 30 ,"잠시 몸을 철과 같이 딱딱하게 하는 물약입니다.", 10000 ,"Defend"));
        }
       
        public override string ToString()
        {
            return expendablesPro.ToString();
        }
    }
}
