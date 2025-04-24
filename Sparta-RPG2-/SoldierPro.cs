using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sparta_RPG2_
{
    public class SoldierPro
    { 
        public string ItemName { get; set; }
        public string ItemInfo { get; set; }
        public int ItemValue { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public bool IsEquipped { get; set; }


        public SoldierPro(string name, string info, int attack, int defense, int value)
        {
            ItemName = name;
            ItemValue = value;
            ItemInfo = info;
            IsEquipped = false;
            Attack = attack;
            Defense = defense;
        }

        public SoldierPro Clone()
        {
            return new SoldierPro(ItemName, ItemInfo, Attack, Defense, ItemValue);
        }
        // 인벤토리에서 출력용
        public string ToInventoryString()
        {
            string equipStatus = IsEquipped ? "[전투준비]" : "";

          return $"- {ItemName} | 공격력 : {Attack} | 방어력 : {Defense} | {ItemInfo}";
        }
        public string ToSellString()
        {
            string equipStatus = IsEquipped ? " [전투준비]" : "";

           return $"-{ItemName} | 공격력 : {Attack} | 방어력 : {Defense} | {ItemInfo} | {ItemValue * 17 / 20}G";
            
        }

        public override string ToString()
        {
                return $"{ItemName} | 공격력 : {Attack} | 방어력 : {Defense} | {ItemInfo} | {ItemValue}G";
        }
    }
}

