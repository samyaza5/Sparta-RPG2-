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
            expendablesPro.ItemValue); ;
        }
        public static Expendables potion()
        {
            return new Expendables(new ExpendablesPro("회복물약", 30 , "체력을 회복시켜주는 물약입니다.", 2000));
        }
        public static Expendables manaPotion()
        {
            return new Expendables(new ExpendablesPro("마나물약", 10 , "마나를 회복시켜주는 물약입니다.", 3000));
        }
       
        public override string ToString()
        {
            return expendablesPro.ToString();
        }
    }
}
