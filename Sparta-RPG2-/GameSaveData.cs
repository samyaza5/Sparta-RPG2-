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
    public class GameSaveData
    {
        public Character Player { get; set; } = new();
        public List<Item> Inventory { get; set; } = new();
        public List<Expendables> Expendables { get; set; } = new();
        public List<string> CompletedQuests { get; set; } = new();
        public List<string> ClearedDungeons { get; set; } = new();
        public string? EquippedWeaponName { get; set; }
        public string? EquippedArmorName { get; set; }
    }
}
