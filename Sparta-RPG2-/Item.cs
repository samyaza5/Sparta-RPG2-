public class Item
{
    public ItemPro itemPro;
    public Item(ItemPro itemPro)
    {
        this.itemPro = itemPro;
    }
    public static ItemPro BeginnerArmor()
    {
        return new ItemPro("가죽 갑옷", 5, "말린 소가죽으로 만든 갑옷입니다.", 1000, true, false);
    }
    public static ItemPro IronArmor()
    {
        return new ItemPro("강철 갑옷", 9, "일반 병사에게 보급되는 평범한 갑옷입니다.", 2000, true, false);
    }
    public static ItemPro SpartaArmor()
    {
        return new ItemPro("스파르타의 갑옷", 16, "스파르타의 전사들이 사용했다는 갑옷입니다.", 4000, true, false);
    } public static ItemPro Sparta300Armor()
    {
        return new ItemPro("스파르타의 최후의 갑옷", 25, "스파르타 최후의 300인의 전사가 사용한 갑옷입니다.", 8000, true, false);
    } public static ItemPro ArmorOfSpartacus()
    {
        return new ItemPro("스파르타쿠스의 의지", 40, "스파르타쿠스만이 사용할 수 있는 전설의 창입니다.", 15000, true, false);
    }
    public static ItemPro OldSword()
    {
        return new ItemPro("나무 창", 2, "나무를 뾰족하게 깎아 만든 창 입니다.", 600, false, true);
    }
    public static ItemPro BronzeAx()
    {
        return new ItemPro("강철 창", 5, "일반 병사에게 보급되는 평범한 창입니다.", 1500, false, true);
    }
    public static ItemPro SpartaSphere()
    {
        return new ItemPro("스파르타의 창", 10, "스파르타의 전사들이 사용했다는 창입니다.", 3500, false, true);
    }
    public static ItemPro Sparta300Sphere()
    {
        return new ItemPro("스파르타 최후의 창", 16, "스파르타의 최후의 300인의 전사가 사용한 창입니다", 8000, false, true);
    } 
    public static ItemPro SphereOfSpartacus()
    {
        return new ItemPro("스파르타쿠스의 분노", 30, "스파르타쿠스만이 사용할 수 있는 전설의 창입니다.", 15000, false, true);
    }
    

    public override string ToString()
    {
        return itemPro.ToString();
    }
}

