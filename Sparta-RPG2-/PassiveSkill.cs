namespace Sparta_RPG2_
{
    public class PassiveSkill
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int SkillLv  { get; set; }
        public int MaxSkillLv { get; set; }
        public float Stat { get; set; }
        public bool GetSkill { get; set; }

        
        public PassiveSkill(string name, string desc, int skillLv, int maxSkillLv, float stat, bool getSkill)
        {
            Name = name;
            Description = desc;
            SkillLv = skillLv;
            MaxSkillLv = maxSkillLv;
            Stat = stat;
            GetSkill = getSkill;
            

        }
        
        public List<PassiveSkill> SkillList = new List<PassiveSkill>();

        public void AddSkill()
        {
            SkillList.Add(new PassiveSkill("강인함", "최대체력이 20% 증가합니다.", 0, 1, 1.2f, false));
            SkillList.Add(new PassiveSkill("강철피부", "방어력이 3 증가합니다.", 0, 5, 3f, false));

        }

        // 스킬창 출력
        





    }
}

