namespace Sparta_RPG2_
{
    public class PassiveSkill
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int SkillLv  { get; set; }
        public int MaxSkillLv { get; set; }
        public float Stat { get; set; }
        public bool GetSkill {  get; set; }
        public bool MasterSkill { get; set; }

        
        public PassiveSkill(string name, string desc, int skillLv, int maxSkillLv, float stat,bool getSkill, bool masterSkill)
        {
            Name = name;
            Description = desc;
            SkillLv = skillLv;
            MaxSkillLv = maxSkillLv;
            Stat = stat;
            GetSkill = getSkill;
            MasterSkill = masterSkill;
            

        }
        
        public List<PassiveSkill> SkillList = new List<PassiveSkill>();

        public void AddSkill()
        {
            SkillList.Add(new PassiveSkill("강인함", "최대체력이 20% 증가합니다.", 0, 1, 1.2f, false, false));
            SkillList.Add(new PassiveSkill("강철피부", "방어력이 3 증가합니다.", 0, 5, 3f, false, false));
            SkillList.Add(new PassiveSkill("재생력", "매 턴 시작 시 체력을 소량 회복합니다.", 0, 5, 1f, false, false));

        }

        // 스킬창 출력
        





    }
}

