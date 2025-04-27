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
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("⛪ 구원의 성소");
        Console.ResetColor();
        Console.WriteLine("\n폐허 위, 마지막 희망이 깃든 신성한 성소가 모습을 드러냅니다.");
        Console.WriteLine("100G를 바치고, 몸과 영혼을 정화하시겠습니까?");
        Console.WriteLine("1. 예  |  2. 아니오");

        string? ansur = Console.ReadLine();
        if (ansur == "1")
        {
            if (player.Gold >= 100)
            {
                player.Gold -= 100;
                player.HP = player.MaxHP;
                player.MP = player.MaxMP;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n✨ 성스러운 빛이 몸과 마음을 감쌉니다. 생명력이 되살아났습니다!");
                Console.ResetColor();
                Console.WriteLine("\n[아무 키나 누르면 성소를 떠납니다]");
                Console.ReadKey();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n❌ 당신은 신에게 바칠 금화를 지니고 있지 않습니다...");
                Console.ResetColor();
                Thread.Sleep(1500);
            }
        }
        else
        {
            Console.WriteLine("\n🚶 당신은 성소를 지나쳐, 다시 붉은 대지를 향해 나아갑니다...");
            Thread.Sleep(1500);
        }
    }
}
