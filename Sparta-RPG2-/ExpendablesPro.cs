using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sparta_RPG2_.Dungeon;
using static Sparta_RPG2_.Quest;

namespace Sparta_RPG2_
{
    public class ExpendablesPro
    {
        public string ItemName { get; set; }
        public int ItemStat { get; set; }
        public string ItemInfo { get; set; }
        public int ItemValue { get; set; }
        public bool IsSold { get; set; }


        public ExpendablesPro(string name, int stat, string info, int value)
        {
            ItemName = name;
            ItemStat = stat;
            ItemValue = value;
            ItemInfo = info;
            IsSold = false;
            
        }

        // 인벤토리에서 출력용
     
        public string ToSellString()
        {
            return $"-{ItemName} | 회복력 : {ItemStat} | {ItemInfo} | {ItemValue * 17 / 20}G";        
        }
        public string ToInventoryString()
        {
            return $"-{ItemName} | 회복력 : {ItemStat} | {ItemInfo}";
        }

        public override string ToString()
        {          
            return $"{ItemName} | 회복력 : {ItemStat} | {ItemInfo} | {ItemValue}G";
        }        
    }
}
