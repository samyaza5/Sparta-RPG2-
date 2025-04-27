using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_RPG2_
{
    class ChangePlayerName
    {
        public static void ChangeName(Character player)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("📝 이름 변경");
            Console.ResetColor();
            Console.Write("\n새로운 이름을 입력하세요: ");
            string? newName = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(newName))
            {
                player.Name = newName.Trim();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n✅ 이름이 '{player.Name}'(으)로 변경되었습니다!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n❌ 이름이 유효하지 않습니다. 변경이 취소되었습니다.");
                Console.ResetColor();
            }

            Thread.Sleep(1500);

            // ✅ 입력 버퍼 비우기
            while (Console.KeyAvailable)
                Console.ReadKey(true);
        }
    }
}
