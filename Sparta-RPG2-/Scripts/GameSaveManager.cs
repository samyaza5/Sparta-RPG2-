using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

        public static void AutoSave(Character player, Inventory inventory, QuestManager questManager, ItemEquipped itemEquipped, DungeonManager dungeonManager, SoldierInven soldierInven)
        {
            var data = new GameSaveData
            {
                Player = player,
                Inventory = inventory.AllItems,
                Expendables = inventory.expendables,
                Quests = questManager.AllQuests,
                ClearedDungeons = dungeonManager.GetClearedDungeons(),
                Soldiers = soldierInven.AllSoldiers.Select(s => s.soldierPro).ToList(),
                EquippedSoldierName = soldierInven.AllSoldiers.FirstOrDefault(s => s.soldierPro.IsEquipped)?.soldierPro.ItemName
            };

            SaveGame(data);
            Console.WriteLine("💾 자동 저장 완료!");
        }

        public static int CalculateMaxExp(int level)
        {
            return (int)(100 * Math.Pow(1.1, level % 10) * Math.Pow(2, level / 10));
        }

        public static void ApplySaveData(GameSaveData data, Character player, Inventory inventory, QuestManager questManager, DungeonManager dungeonManager, SoldierInven soldierInven, ItemEquipped itemEquipped)
        {
            if (data == null) return;

            RestorePlayerStats(data, player);
            RestoreInventory(data, inventory);
            RestoreQuests(data, questManager);
            RestoreDungeons(data, dungeonManager);
            RestoreSoldiers(data, soldierInven, player);

            player.MaxExp = Character.CalculateMaxExp(player.Level);

            itemEquipped.UpdateStatsFromInventory(inventory.AllItems);

            Console.WriteLine("📂 저장된 데이터가 게임에 적용되었습니다.");
        }

        private static void RestorePlayerStats(GameSaveData data, Character player)
        {
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
        }

        private static void RestoreInventory(GameSaveData data, Inventory inventory)
        {
            inventory.AllItems.Clear();
            if (data.Inventory != null)
            {
                foreach (var item in data.Inventory)
                    inventory.AllItems.Add(item);
            }

            inventory.expendables.Clear();
            if (data.Expendables != null)
            {
                foreach (var item in data.Expendables)
                    inventory.expendables.Add(item);
            }
        }

        private static void RestoreQuests(GameSaveData data, QuestManager questManager)
        {
            if (data.Quests != null)
            {
                questManager.AllQuests.Clear();
                foreach (var quest in data.Quests)
                {
                    questManager.AllQuests.Add(quest);
                }
            }
        }

        private static void RestoreDungeons(GameSaveData data, DungeonManager dungeonManager)
        {
            foreach (var dungeon in dungeonManager.Dungeons)
            {
                if (data.ClearedDungeons.Contains(dungeon.Name))
                {
                    dungeon.IsCleared = true;
                }
            }
        }

        private static void RestoreSoldiers(GameSaveData data, SoldierInven soldierInven, Character player)
        {
            if (data.Soldiers != null)
            {
                var soldierList = data.Soldiers.Select(p => new Soldier(p)).ToList();
                soldierInven.AllSoldiers.Clear();
                soldierInven.AllSoldiers.AddRange(soldierList);

                var equipped = soldierList.FirstOrDefault(s => s.soldierPro.ItemName == data.EquippedSoldierName);
                if (equipped != null)
                {
                    equipped.soldierPro.IsEquipped = true;
                    soldierInven.EquippedSoldier = equipped;
                }
            }

            SoldierEquipped soldierEquipped = new SoldierEquipped(soldierInven, player);
            soldierEquipped.UpdateStatsFromSoldierInven();
        }
    }
}

