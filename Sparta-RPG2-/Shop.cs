using System.Text.Json.Serialization;
using Sparta_RPG2_;
using static RPG_SJ.Program;

namespace RPG_SJ
{
    class Shop//상점
    {
        public List<Item> AllItems;
        public List<Expendables> expendables;

        Character character;

        public Shop(Character character)
        {
            this.character = character;
            AllItems = new List<Item>();
            

            AllItems.Add(new Item(Item.BeginnerArmor()));
            AllItems.Add(new Item(Item.IronArmor()));
            AllItems.Add(new Item(Item.SpartaArmor()));
            AllItems.Add(new Item(Item.Sparta300Armor()));
            AllItems.Add(new Item(Item.ArmorOfSpartacus()));
            AllItems.Add(new Item(Item.OldSword()));
            AllItems.Add(new Item(Item.BronzeAx()));
            AllItems.Add(new Item(Item.SpartaSphere()));
            AllItems.Add(new Item(Item.Sparta300Sphere()));
            AllItems.Add(new Item(Item.SphereOfSpartacus()));
            expendables.Add(new Expendables(Expendables.potion()));
            expendables.Add(new Expendables(Expendables.manaPotion()));
        }

        public void ShopScene()
        {
            Console.Clear();
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{character.Gold}G");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            foreach (var Item in AllItems)
            {
                Console.WriteLine(Item);
            }  
            foreach (var Item in expendables)
            {
                Console.WriteLine(expendables);
            }
            Console.WriteLine();
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
           
        }
    } 
}

