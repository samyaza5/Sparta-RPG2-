using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Sparta_RPG2_.Dungeon;
using static Sparta_RPG2_.Quest;

namespace Sparta_RPG2_
{
    public static class GameSaveManager
    {
        private const string SavePath = "save.json";

        public static void SaveGame(GameSaveData data)
        {
            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SavePath, json);
            Console.WriteLine("✅ 게임이 저장되었습니다.");
        }

        public static GameSaveData LoadGame()
        {
            if (!File.Exists(SavePath))
            {
                Console.WriteLine("⚠️ 저장 파일이 존재하지 않습니다.");
                return new GameSaveData();
            }

            string json = File.ReadAllText(SavePath);
            var data = JsonSerializer.Deserialize<GameSaveData>(json);
            Console.WriteLine("✅ 저장된 게임을 불러왔습니다.");
            return data ?? new GameSaveData();
        }

        public static void AutoSave(Character player, Inventory inventory, QuestManager questManager, ItemEquipped itemEquipped, DungeonManager dungeonManager)
        {
            var data = new GameSaveData
            {
                Player = player,
                Inventory = inventory.AllItems,
                Expendables = inventory.expendables,
                CompletedQuests = questManager.GetCompletedQuestTitles(),
                ClearedDungeons = dungeonManager.GetClearedDungeons(),
            };

            SaveGame(data);
            Console.WriteLine("💾 자동 저장 완료!");
        }

        public static int CalculateMaxExp(int level)
        {
            return (int)(100 * Math.Pow(1.1, level % 10) * Math.Pow(2, level / 10));
        }

        public static void Apply(GameSaveData data, Character player, Inventory inventory, QuestManager questManager, DungeonManager dungeonManager)
        {
            if (data == null) return;

            // Player
            player.Name = data.Player.Name;
            player.Level = data.Player.Level;
            player.HP = data.Player.HP;
            player.MaxHP = data.Player.MaxHP;
            player.MP = data.Player.MP;
            player.MaxMP = data.Player.MaxMP;
            player.Attack = data.Player.Attack;
            player.Defense = data.Player.Defense;
            player.Exp = data.Player.Exp;
            player.Gold = data.Player.Gold;
            player.Job = data.Player.Job;
            player.JobName = data.Player.JobName;

            // Quests
            foreach (var quest in questManager.AllQuests)
            {
                if (data.CompletedQuests.Contains(quest.Title ?? string.Empty))
                {
                    quest.IsAccepted = true;
                    quest.IsCompleted = true;
                    quest.CurrentProgress = quest.Goal;
                }
            }

            // Dungeons
            foreach (var dungeon in dungeonManager.Dungeons)
            {
                if (data.ClearedDungeons.Contains(dungeon.Name))
                {
                    dungeon.IsCleared = true;
                }
            }

            if (player.Exp >= player.MaxExp)
            {
                while (player.Exp >= player.MaxExp)
                {
                    player.Exp -= player.MaxExp;
                    player.Level++;
                    player.MaxExp = CalculateMaxExp(player.Level);
                }
                Console.WriteLine("🎉 저장된 데이터로 인해 레벨업이 반영되었습니다!");
            }

            Console.WriteLine("📂 저장된 데이터가 게임에 적용되었습니다.");
        }
    }
}

