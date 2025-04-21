public class ItemPro
{

    public string ItemName { get; set; }
    public int ItemStat { get; set; }
    public string ItemInfo { get; set; }
    public int ItemValue { get; set; }
    public bool IsSold { get; set; }
    public bool IsArmor { get; set; }
    public bool IsWeapon { get; set; }
    public bool IsEquipped { get; set; }


    public ItemPro(string name, int stat, string info, int value, bool isArmor, bool isWeapon)
    {
        ItemName = name;
        ItemStat = stat;
        ItemValue = value;
        ItemInfo = info;
        IsSold = false;
        IsArmor = isArmor;
        IsWeapon = isWeapon;
        IsEquipped = false;
    }

    // 인벤토리에서 출력용
    public string ToInventoryString()
    {
        string equipStatus = IsEquipped ? "[E]" : "";
        if (IsArmor)
            return $"-{equipStatus}{ItemName} | 방어력 : {ItemStat} | {ItemInfo}";
        else
            return $"-{equipStatus}{ItemName} | 공격력 : {ItemStat} | {ItemInfo}";
    }
    public string ToSellString()
    {
        string equipStatus = IsEquipped ? " [E]" : "";

        if (IsSold && IsArmor)
        {
            return $"-{ItemName} | 방어력 : {ItemStat} | {ItemInfo} | {ItemValue*17/20}G";
        }
        else
        {
            return $"-{ItemName} | 공격력 : {ItemStat} | {ItemInfo} | {ItemValue * 17 / 20}G";
        }
    }

    public override string ToString()
    {
        string equipStatus = IsEquipped ? " [E]" : "";

        if (IsSold && IsArmor)
        {
            return $"{ItemName} | 방어력 : {ItemStat} | {ItemInfo} | [구매 완료]";
        }
        else if (IsSold && IsWeapon)
        {
            return $"{ItemName} | 공격력 : {ItemStat} | {ItemInfo} | [구매 완료]";
        }
        else if (!IsSold && IsArmor)
        {
            return $"{ItemName} | 방어력 : {ItemStat} | {ItemInfo} | {ItemValue}G";
        }
        else
        {
            return $"{ItemName} | 공격력 : {ItemStat} | {ItemInfo} | {ItemValue}G";
        }
    }
}

