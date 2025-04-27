using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_RPG2_
{
    internal class JabtemPro
    {

        public string ItemName { get; set; }
        public string ItemInfo { get; set; }
        public int ItemValue { get; set; }
        public bool IsSold { get; set; }


        public JabtemPro(string name, string info, int value)
        {
            ItemName = name;
            ItemValue = value;
            ItemInfo = info;
            IsSold = false;
        }

        // 인벤토리에서 출력용

        public override string ToString()
        {
            if(!IsSold)
            {
                return $"{ItemName}  | {ItemInfo} | {ItemValue}G";
            }
            else
            {
                return $"{ItemName}  | {ItemInfo} | [판매완료]";
            }
        }
    }
}

