using System.Text.Json.Serialization;

namespace Sparta_RPG2_
{
    public class ItemPro
    {
        public string ItemName { get; set; } = string.Empty;
        public int ItemStat { get; set; }
        public string ItemInfo { get; set; } = string.Empty;
        public int ItemValue { get; set; }
        public bool IsSold { get; set; }
        public bool IsArmor { get; set; }
        public bool IsWeapon { get; set; }
        public bool IsEquipped { get; set; }

        // 🔹 기본 생성자 (필수 - 역직렬화 시 사용됨)
        public ItemPro() { }

        [JsonConstructor]
        public ItemPro(string itemName, int itemStat, string itemInfo, int itemValue, bool isArmor, bool isWeapon)
        {
            ItemName = itemName;
            ItemStat = itemStat;
            ItemValue = itemValue;
            ItemInfo = itemInfo;
            IsSold = false;
            IsArmor = isArmor;
            IsWeapon = isWeapon;
            IsEquipped = false;
        }

        // 인벤토리에서 출력용
        public string ToInventoryString()
        {
            string equipStatus = IsEquipped ? "[E]" : "";
            return IsArmor
                ? $"-{equipStatus}{ItemName} | 방어력 : {ItemStat} | {ItemInfo}"
                : $"-{equipStatus}{ItemName} | 공격력 : {ItemStat} | {ItemInfo}";
        }
        public string ToSellString()
        {
            string equipStatus = IsEquipped ? " [E]" : "";
            string statLabel = IsArmor ? "방어력" : "공격력";
            int price = ItemValue * 17 / 20;

            return $"-{ItemName} | {statLabel} : {ItemStat} | {ItemInfo} | {price}G";
        }

        public override string ToString()
        {
            string statLabel = IsArmor ? "방어력" : "공격력";
            string status = IsSold ? "[구매 완료]" : $"{ItemValue}G";

            return $"{ItemName} | {statLabel} : {ItemStat} | {ItemInfo} | {status}";
        }
    }
}

