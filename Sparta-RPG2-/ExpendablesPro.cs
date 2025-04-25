using System.Text.Json.Serialization;

namespace Sparta_RPG2_
{
    /// <summary>
    /// 소비 아이템(회복용)의 속성과 출력을 담당하는 클래스입니다.
    /// </summary>
    public class ExpendablesPro
    {
        // 🔹 직렬화에 필요한 settable 프로퍼티
        public string ItemName { get; set; } = string.Empty;
        public int ItemStat { get; set; }
        public string ItemInfo { get; set; } = string.Empty;
        public int ItemValue { get; set; }
        public bool IsSold { get; set; } = false;

        // 🔹 기본 생성자 (역직렬화 필수)
        public ExpendablesPro() { }

        // 🔹 JsonConstructor - 매핑 이름 일치 필수
        [JsonConstructor]
        public ExpendablesPro(string itemName, int itemStat, string itemInfo, int itemValue)
        {
            ItemName = itemName;
            ItemStat = itemStat;
            ItemInfo = itemInfo;
            ItemValue = itemValue;
            IsSold = false;
        }

        /// <summary>
        /// 인벤토리 전용 출력 문자열
        /// </summary>
        public string ToInventoryString()
        {
            return $"-{ItemName} | 회복력 : {ItemStat} | {ItemInfo}";
        }

        /// <summary>
        /// 상점 판매 전용 출력 문자열
        /// </summary>
        public string ToSellString()
        {
            int price = ItemValue * 17 / 20;
            return $"-{ItemName} | 회복력 : {ItemStat} | {ItemInfo} | {price}G";
        }

        /// <summary>
        /// 기본 출력 문자열
        /// </summary>
        public override string ToString()
        {
            string status = IsSold ? "[구매 완료]" : $"{ItemValue}G";
            return $"{ItemName} | 회복력 : {ItemStat} | {ItemInfo} | {status}";
        }
    }
}
