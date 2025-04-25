using Sparta_RPG2_;

public class Recovery
{
    private Character player;
    private Inventory inventory;

    public Recovery(Character player, Inventory inventory)
    {
        this.player = player;
        this.inventory = inventory;
    }

    public void Recoverycene()
    {
        Console.WriteLine("회복하시겠습니까? 1.예 2.아니오");
        string ansur = Console.ReadLine();
        if (ansur == "1")
        {
            if (player.Gold >= 100)
            {
                player.Gold -= 100;
                player.HP = player.MaxHP;
                player.MP = player.MaxMP;
                Console.WriteLine("체력과 마나가 회복되었습니다.");
            }
            else
            {
                Console.WriteLine("골드가 부족합니다.");
            }
        }
    }
}
