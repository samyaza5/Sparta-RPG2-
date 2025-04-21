using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_RPG2_
{
    internal class Jabtem
    {
        public JabtemPro jabtemPro;


        public Jabtem(JabtemPro jabtemPro)
        {
            this.jabtemPro = jabtemPro;
        }
        public static JabtemPro slimeBall()
        {
            return new JabtemPro("슬라임방울","슬라임 머리에 달린 방울", 200);
        }
        public static JabtemPro goblinLeather()
        {
            return new JabtemPro("고블린 가죽","고블린의 냄새나는 가죽입니다.", 300);
        }

        public override string ToString()
        {
            return jabtemPro.ToString();
        }
    }
}

