  static void ShowCreatMe(Character player)
  {
      Console.WriteLine("캐릭터를 생성합니다.\n");
      Console.Write("이름을 입력하세요 : ");
      player.Name = Console.ReadLine() ?? "함장";
      Console.Write("전사, 마법사, 궁수, 스파르타21 중 하나를 입력하세요 : ");
      player.Job = Console.ReadLine() ?? "전사";

      switch (player.Job)
      {
          case "전사":
              player.Attack = 10; player.Defense = 10; player.HP = 80; player.MaxHP = 80; player.MP = 5; player.MaxMP = 5; break;
          case "마법사":
              player.Attack = 5; player.Defense = 3; player.HP = 50; player.MaxHP = 50; player.MP = 12; player.MaxMP = 12; break;
          case "궁수":
              player.Attack = 15; player.Defense = 6; player.HP = 65; player.MaxHP = 65; player.MP = 8; player.MaxMP = 8; break;
          case "스파르타21":
              player.Attack = 21; player.Defense = 21; player.HP = 121; player.MaxHP = 121; player.MP = 21; player.MaxMP = 21; break;
          default:
              Console.WriteLine("잘못된 직업입니다. 전사로 설정합니다.");
              player.Job = "전사";
              player.Attack = 10; player.Defense = 10; player.HP = 80; player.MaxHP = 80; player.MP = 5; player.MaxMP = 5; break;
      }

      Console.WriteLine($"\n캐릭터 생성 완료! 이름 : {player.Name}, 직업 : {player.Job}");
      Console.WriteLine("\n0. 나가기");
      while (Console.ReadLine() != "0") ;
  }
