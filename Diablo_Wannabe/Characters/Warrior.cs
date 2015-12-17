using Diablo_Wannabe.Entities;
using Diablo_Wannabe.Interfaces;

namespace Diablo_Wannabe.Characters
{
    class Warrior : Player, IHero
    {
        private const int WarriorArmor = 100;
        private const int WarriorHealth = 200;
        private const int WarriorPower = 50;

        public int Armor { get; set; } = WarriorArmor;

        public int Health { get; set; } = WarriorHealth;

        public int Power { get; set; } = WarriorPower;
    }
}