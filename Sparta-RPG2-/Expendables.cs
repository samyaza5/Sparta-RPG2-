using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_RPG2_
{
    public class Expendables
    {
        public ExpendablesPro expendablesPro;


        public Expendables(ExpendablesPro expendablesPro)
        {
            this.expendablesPro = expendablesPro;
        }
        public static ExpendablesPro potion()
        {
            return new ExpendablesPro("회복물약", 30 , "체력을 회복시켜주는 물약입니다.", 2000);
        }
        public static ExpendablesPro manaPotion()
        {
            return new ExpendablesPro("마나물약", 10 , "마나를 회복시켜주는 물약입니다.", 3000);
        }
       
        public override string ToString()
        {
            return expendablesPro.ToString();
        }
    }
}
